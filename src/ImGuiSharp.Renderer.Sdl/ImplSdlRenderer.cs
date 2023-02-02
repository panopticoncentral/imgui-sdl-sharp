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
            public SdlSharp.Graphics.Renderer _renderer;
            public Dictionary<TextureId, Texture> _textures;
            public TextureId _fontTextureId;

            public Data(SdlSharp.Graphics.Renderer sdlRenderer)
            {
                _fontTextureId = default;
                _renderer = sdlRenderer;
                _textures = new Dictionary<TextureId, Texture>();
            }
        };

        private static Data BackendData =>
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
            var bd = BackendData;
            var io = Imgui.GetIo();

            DestroyDeviceObjects();

            io.BackendRendererName = null;
            io.BackendRendererUserData = 0;
            _ = DataDictionary.Remove((nuint)bd.GetHashCode());
        }

        public static TextureId RegisterTexture(Texture texture)
        {
            var bd = BackendData;

            var id = new TextureId(texture.Id);
            bd._textures[id] = texture;
            return id;
        }

        public static Texture? UnregisterTexture(TextureId id)
        {
            var bd = BackendData;

            if (bd._textures.TryGetValue(id, out var texture))
            {
                _ = bd._textures.Remove(id);
                return texture;
            }

            return null;
        }

        private static void SetupRenderState()
        {
            var bd = BackendData;

            bd._renderer.Viewport = null;
            bd._renderer.ClippingRectangle = null;
        }

        public static void NewFrame()
        {
            var bd = BackendData;

            if (bd._fontTextureId == default)
            {
                _ = CreateDeviceObjects();
            }
        }

        public static void RenderDrawData(DrawData drawData)
        {
            var bd = BackendData;

            var (existingScaleX, existingScaleY) = bd._renderer.Scale;
            var renderScaleX = (existingScaleX == 1.0f) ? drawData.FramebufferScale.X : 1.0f;
            var renderScaleY = (existingScaleY == 1.0f) ? drawData.FramebufferScale.Y : 1.0f;

            var width = (int)(drawData.DisplaySize.Width * renderScaleX);
            var height = (int)(drawData.DisplaySize.Height * renderScaleY);
            if (width == 0 || height == 0)
            {
                return;
            }

            var old = (bd._renderer.ClippingEnabled, bd._renderer.Viewport, bd._renderer.ClippingRectangle);

            var clipOffset = drawData.DisplayPosition;
            var clipScaleX = renderScaleX;
            var clipScaleY = renderScaleY;

            var rawDescriptor = new RawGeometryDescriptor<DrawVertex, DrawIndex>("_xy", "_color", "_uv");

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

                            bd._renderer.ClippingRectangle = new(new((int)clipMin.X, (int)clipMin.Y), new((int)(clipMax.X - clipMin.X), (int)(clipMax.Y - clipMin.Y)));
                            bd._renderer.DrawGeometry(
                                cmd.TextureId == default ? null : bd._textures[cmd.TextureId],
                                new Span<DrawVertex>(cmdList.Vertices.ToNative() + cmd.VertexOffset,
                                (int)(cmdList.Vertices.Count - cmd.VertexOffset)),
                                new Span<DrawIndex>(cmdList.Indexes.ToNative() + cmd.IndexOffset,
                                (int)cmd.ElementCount),
                                rawDescriptor);
                            break;
                    }
                }
            }

            bd._renderer.Viewport = old.Viewport;
            bd._renderer.ClippingRectangle = old.ClippingEnabled ? old.ClippingRectangle : null;
        }

        private static bool CreateFontsTexture()
        {
            var io = Imgui.GetIo();
            var bd = BackendData;

            io.Fonts.GetTextureDataAsRgba32(out var pixels, out var width, out var height, out var _);
            SdlSharp.Graphics.Size size = new(width, height);

            var texture = bd._renderer.CreateTexture(EnumeratedPixelFormat.Abgr8888, TextureAccess.Static, size);
            if (texture == null)
            {
                Log.Error("error creating texture");
                return false;
            }
            texture.Update(null, pixels, 4 * size.Width);
            texture.BlendMode = BlendMode.Blend;
            texture.ScaleMode = ScaleMode.Linear;

            io.Fonts.SetTextureId(RegisterTexture(texture));

            return true;
        }

        private static void DestroyFontsTexture()
        {
            var io = Imgui.GetIo();
            var bd = BackendData;
            if (bd._fontTextureId != default)
            {
                io.Fonts.SetTextureId(default);
                var texture = UnregisterTexture(bd._fontTextureId);
                texture?.Dispose();
                bd._fontTextureId = default;
            }
        }

        private static bool CreateDeviceObjects() => CreateFontsTexture();

        private static void DestroyDeviceObjects() => DestroyFontsTexture();
    }
}
