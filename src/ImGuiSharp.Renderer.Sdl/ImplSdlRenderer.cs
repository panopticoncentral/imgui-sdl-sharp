using ImguiSharp;

using SdlSharp;
using SdlSharp.Graphics;

namespace ImGuiSharp.Renderer.Sdl
{
    public static unsafe class ImplSdlRenderer
    {
        private static Dictionary<nuint, Data>? s_dataDictionary;
        private static Dictionary<nuint, Data> DataDictionary => s_dataDictionary ??= new Dictionary<nuint, Data>();

        private struct Data
        {
            public SdlSharp.Graphics.Renderer _sdlRenderer;
            public Texture? _fontTexture;

            public Data(SdlSharp.Graphics.Renderer sdlRenderer)
            {
                _sdlRenderer = sdlRenderer;
            }
        };

        private static Data GetBackendData() =>
            Imgui.GetCurrentContext() != null
                ? DataDictionary[Imgui.GetIo().BackendRendererUserData]
                : throw new InvalidOperationException();

        public static bool Init(SdlSharp.Graphics.Renderer renderer)
        {
            var io = Imgui.GetIo();

            if (io.BackendRendererUserData != 0)
            {
                throw new InvalidOperationException();
            }

            var bd = new Data(renderer);
            DataDictionary[(nuint)bd.GetHashCode()] = bd;

            io.BackendRendererUserData = (nuint)bd.GetHashCode();
            io.BackendRendererName = "imgui_impl_sdlrenderer";
            io.BackendOptions |= BackendOptions.RendererHasVtxOffset;

            return true;
        }

        public static void Shutdown()
        {
            var bd = GetBackendData();
            var io = Imgui.GetIo();

            DestroyDeviceObjects();

            io.BackendRendererName = null;
            io.BackendRendererUserData = 0;
            _ = DataDictionary.Remove((nuint)bd.GetHashCode());
        }

        private static void SetupRenderState()
        {
            var bd = GetBackendData();

            bd._sdlRenderer.Viewport = null;
            bd._sdlRenderer.ClippingRectangle = null;
        }

        public static void NewFrame()
        {
            var bd = GetBackendData();

            if (bd._fontTexture == null)
            {
                _ = CreateDeviceObjects();
            }
        }

        public static void RenderDrawData(DrawData drawData)
        {
            var bd = GetBackendData();

            var (existingScaleX, existingScaleY) = bd._sdlRenderer.Scale;
            var renderScaleX = (existingScaleX == 1.0f) ? drawData.FramebufferScale.X : 1.0f;
            var renderScaleY = (existingScaleY == 1.0f) ? drawData.FramebufferScale.Y : 1.0f;

            var width = (int)(drawData.DisplaySize.Width * renderScaleX);
            var height = (int)(drawData.DisplaySize.Height * renderScaleY);
            if (width == 0 || height == 0)
            {
                return;
            }

            var old = (bd._sdlRenderer.ClippingEnabled, bd._sdlRenderer.Viewport, bd._sdlRenderer.ClippingRectangle);

            var clipOffset = drawData.DisplayPosition;
            var clipScaleX = renderScaleX;
            var clipScaleY = renderScaleY;

            SetupRenderState();
            foreach (var cmdList in drawData)
            {
                foreach (var cmd in cmdList.Commands)
                {
                    switch (cmd.Kind)
                    {
                        case DrawCommandKind.ResetRenderState:
                            SetupRenderState();
                            break;

                        case DrawCommandKind.Callback:
                            cmd.DoCallback(cmdList);
                            break;

                        case DrawCommandKind.Vertex:
                            Position clipMin = new((cmd.ClipRectangle.X1 - clipOffset.X) * clipScaleX, (cmd.ClipRectangle.Y1 - clipOffset.Y) * clipScaleY);
                            Position clipMax = new((cmd.ClipRectangle.X2 - clipOffset.X) * clipScaleX, (cmd.ClipRectangle.Y2 - clipOffset.Y) * clipScaleY);

                            if (clipMin.X < 0.0f)
                            {
                                clipMin = new(0.0f, clipMin.Y);
                            }

                            if (clipMin.Y < 0.0f)
                            {
                                clipMin = new(clipMin.X, 0.0f);
                            }

                            if (clipMax.X > width)
                            {
                                clipMax = new(width, clipMax.Y);
                            }

                            if (clipMax.Y > height)
                            {
                                clipMax = new(clipMax.X, height);
                            }

                            if (clipMax.X <= clipMin.X || clipMax.Y <= clipMin.Y)
                            {
                                continue;
                            }

                            bd._sdlRenderer.ClippingRectangle = new(new((int)clipMin.X, (int)clipMin.Y), new((int)(clipMax.X - clipMin.X), (int)(clipMax.Y - clipMin.Y)));

#if false
                            const float* xy = (const float*)(const void*)((const char*)(vtx_buffer + pcmd->VtxOffset) + IM_OFFSETOF(ImDrawVert, pos));
                            const float* uv = (const float*)(const void*)((const char*)(vtx_buffer + pcmd->VtxOffset) + IM_OFFSETOF(ImDrawVert, uv));
                            const SDL_Color* color = (const SDL_Color*)(const void*)((const char*)(vtx_buffer + pcmd->VtxOffset) + IM_OFFSETOF(ImDrawVert, col)); // SDL 2.0.19+

                            // Bind texture, Draw
                            SDL_Texture* tex = (SDL_Texture*)pcmd->GetTexID();
                            SDL_RenderGeometryRaw(bd->SDLRenderer, tex,
                                xy, (int)sizeof(ImDrawVert),
                                color, (int)sizeof(ImDrawVert),
                                uv, (int)sizeof(ImDrawVert),
                                cmd_list->VtxBuffer.Size - pcmd->VtxOffset,
                                idx_buffer + pcmd->IdxOffset, pcmd->ElemCount, sizeof(ImDrawIdx));
#endif
                            break;
                    }
                }
            }

            bd._sdlRenderer.Viewport = old.Viewport;
            bd._sdlRenderer.ClippingRectangle = old.ClippingEnabled ? old.ClippingRectangle : null;
        }

        private static bool CreateFontsTexture()
        {
            var io = Imgui.GetIo();
            var bd = GetBackendData();

            io.Fonts.GetTextureDataAsRgba32(out var pixels, out var width, out var height, out var _);
            SdlSharp.Graphics.Size size = new(width, height);

            bd._fontTexture = bd._sdlRenderer.CreateTexture(EnumeratedPixelFormat.Abgr8888, TextureAccess.Static, size);
            if (bd._fontTexture == null)
            {
                Log.Error("error creating texture");
                return false;
            }
            bd._fontTexture.Update(null, pixels, 4 * size.Width);
            bd._fontTexture.BlendMode = BlendMode.Blend;
            bd._fontTexture.ScaleMode = ScaleMode.Linear;

            io.Fonts.SetTextureId(new(bd._fontTexture.Id));

            return true;
        }

        private static void DestroyFontsTexture()
        {
            var io = Imgui.GetIo();
            var bd = GetBackendData();
            if (bd._fontTexture != null)
            {
                io.Fonts.SetTextureId(new(0));
                bd._fontTexture.Dispose();
                bd._fontTexture = null;
            }
        }

        private static bool CreateDeviceObjects() => CreateFontsTexture();

        private static void DestroyDeviceObjects() => DestroyFontsTexture();
    }
}
