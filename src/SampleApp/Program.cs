using SdlSharp;
using SdlSharp.Graphics;
using ImguiSharp;
using ImguiSharp.Platform.Sdl;
using ImguiSharp.Renderer.Sdl;

namespace SampleApp
{
    public static class Program
    {
        public static void Main()
        {
            using var application = new Application(Subsystems.Video | Subsystems.Timer | Subsystems.GameController);

            var windowOptions = SdlSharp.Graphics.WindowOptions.Resizable | SdlSharp.Graphics.WindowOptions.AllowHighDpi;
            using var window = Window.Create("Dear ImGui SDL2+SDL_Renderer C# example", new(Window.CenteredWindowLocation, new(1280, 720)), windowOptions);

            using var renderer = Renderer.Create(window, -1, RendererOptions.PresentVSync | RendererOptions.Accelerated);

            _ = Imgui.CreateContext();
            var io = Imgui.GetIo();
            //io.ConfigOptions |= ConfigOptions.NavEnableKeyboard;     // Enable Keyboard Controls
            //io.ConfigOptions |= ConfigOptions.NavEnableGamepad;      // Enable Gamepad Controls

            // Setup Dear ImGui style
            Imgui.StyleColorsDark();
            //Imgui.StyleColorsLight();

            _ = ImplSdl2.Init(window, renderer);
            _ = ImplSdlRenderer.Init(renderer);

            // Load Fonts
            // - If no fonts are loaded, dear imgui will use the default font. You can also load multiple fonts and use ImGui.PushFont()/PopFont() to select them.
            // - AddFontFromFileTtf() will return the Font so you can store it if you need to select the font among multiple.
            // - If the file cannot be loaded, the function will return null. Please handle those errors in your application (e.g. use an assertion, or display an error and quit).
            // - The fonts will be rasterized at a given size (w/ oversampling) and stored into a texture when calling FontAtlas.Build()/GetTextureDataAsXXXX(), which NewFrame below will call.
            // - Read 'docs/FONTS.md' for more instructions and details.
            //io.Fonts.AddFontDefault();
            //io.Fonts.AddFontFromFileTtf("c:\\Windows\\Fonts\\segoeui.ttf", 18.0f);
            //io.Fonts.AddFontFromFileTtf("../../misc/fonts/DroidSans.ttf", 16.0f);
            //io.Fonts.AddFontFromFileTtf("../../misc/fonts/Roboto-Medium.ttf", 16.0f);
            //io.Fonts.AddFontFromFileTtf("../../misc/fonts/Cousine-Regular.ttf", 15.0f);
            //var font = io.Fonts.AddFontFromFileTtf("c:\\Windows\\Fonts\\ArialUni.ttf", 18.0f, null, io.Fonts.GetGlyphRangesJapanese());

            var showDemoWindow = new State<bool>(true);
            var showManagedDemoWindow = new State<bool>(true);
            var showAnotherWindow = new State<bool>(false);
            var clearColor = new StateVector<float>(3, new[] { 0.45f, 0.55f, 0.60f });
            var f = new State<float>(0.0f);
            var counter = 0;

            Window.Closed += (sender, args) =>
            {
                if (((Window)sender!).Id == window.Id)
                {
                    application.Quit();
                }
            };

            while (application.DispatchEvents())
            {
                ImplSdlRenderer.NewFrame();
                ImplSdl2.NewFrame();
                Imgui.NewFrame();

                if (showDemoWindow)
                {
                    Imgui.ShowDemoWindow(showDemoWindow);
                }

                if (showManagedDemoWindow)
                {
                    DemoWindows.ShowDemoWindow(showManagedDemoWindow);
                }

                {
                    _ = Imgui.Begin("Hello, world!");

                    Imgui.Text("This is some useful text.");
                    _ = Imgui.Checkbox("Demo Window", showDemoWindow);
                    _ = Imgui.Checkbox("Managed Demo Window", showManagedDemoWindow);
                    _ = Imgui.Checkbox("Another Window", showAnotherWindow);

                    _ = Imgui.Slider("float", f, 0.0f, 1.0f);
                    _ = Imgui.ColorEdit("clear color", clearColor);

                    if (Imgui.Button("Button"))
                    {
                        counter++;
                    }
                    Imgui.SameLine();
                    Imgui.Text($"counter = {counter}");

                    Imgui.Text($"Application average {1000.0f / Imgui.GetIo().Framerate:F3} ms/frame ({Imgui.GetIo().Framerate:F1} FPS)");

                    Imgui.End();
                }

                if (showAnotherWindow)
                {
                    _ = Imgui.Begin("Another Window", showAnotherWindow);
                    Imgui.Text("Hello from another window!");
                    if (Imgui.Button("Close Me"))
                    {
                        showAnotherWindow.Value = false;
                    }
                    Imgui.End();
                }

                Imgui.Render();

                renderer.DrawColor = new((byte)(clearColor[0] * 255), (byte)(clearColor[1] * 255), (byte)(clearColor[2] * 255), 255);
                renderer.Clear();
                var drawData = Imgui.GetDrawData();
                if (drawData != null)
                {
                    ImplSdlRenderer.RenderDrawData(drawData.Value);
                }
                renderer.Present();
            }

            ImplSdlRenderer.Shutdown();
            ImplSdl2.Shutdown();
            Imgui.DestroyContext();
        }
    }
}
