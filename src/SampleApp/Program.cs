using SdlSharp;
using SdlSharp.Graphics;
using ImguiSharp;
using ImguiSharp.Platform.Sdl;
using ImGuiSharp.Renderer.Sdl;

using var application = new Application(Subsystems.Video | Subsystems.Timer | Subsystems.GameController);

var windowOptions = SdlSharp.Graphics.WindowOptions.Resizable | SdlSharp.Graphics.WindowOptions.AllowHighDpi;
using var window = Window.Create("Dear ImGui SDL2+SDL_Renderer C# example", new(Window.CenteredWindowLocation, new(1280, 720)), windowOptions);

using var renderer = Renderer.Create(window, -1, RendererOptions.PresentVSync | RendererOptions.Accelerated);

Imgui.CreateContext();
var io = Imgui.GetIo();
//io.ConfigOptions |= ConfigOptions.NavEnableKeyboard;     // Enable Keyboard Controls
//io.ConfigOptions |= ConfigOptions.NavEnableGamepad;      // Enable Gamepad Controls

// Setup Dear ImGui style
Imgui.StyleColorsDark();
//Imgui.StyleColorsLight();

ImplSdl2.Init(window, renderer);
ImplSdlRenderer.Init(renderer);

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
var showAnotherWindow = new State<bool>(false);
var clearColor = new ImguiSharp.Color(0.45f, 0.55f, 0.60f, 1.00f);
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

    {
        Imgui.Begin("Hello, world!");

        Imgui.Text("This is some useful text.");
        Imgui.Checkbox("Demo Window", showDemoWindow);      // Edit bools storing our window open/close state
        Imgui.Checkbox("Another Window", showAnotherWindow);

        Imgui.Slider("float", f, 0.0f, 1.0f);
#if false
            ImGui::ColorEdit3("clear color", (float*)&clear_color); // Edit 3 floats representing a color
#endif

        if (Imgui.Button("Button"))
        {
            counter++;
        }
        Imgui.SameLine();
        Imgui.Text($"counter = {counter}");

        Imgui.Text($"Application average {1000.0f / Imgui.GetIo().Framerate:F3} ms/frame ({Imgui.GetIo().Framerate:F1} FPS)");

        Imgui.End();
    }

#if false
    // 3. Show another simple window.
    if (show_another_window)
        {
            ImGui::Begin("Another Window", &show_another_window);   // Pass a pointer to our bool variable (the window will have a closing button that will clear the bool when clicked)
            ImGui::Text("Hello from another window!");
            if (ImGui::Button("Close Me"))
                show_another_window = false;
            ImGui::End();
        }

#endif
    Imgui.Render();

    renderer.DrawColor = new((byte)(clearColor.Red * 255), (byte)(clearColor.Green * 255), (byte)(clearColor.Blue * 255), (byte)(clearColor.Alpha * 255));
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
