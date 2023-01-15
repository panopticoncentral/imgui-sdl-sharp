using SdlSharp;
using SdlSharp.Graphics;

using Imgui = SdlSharp.Imgui;

using var application = new Application(Subsystems.Video | Subsystems.Timer | Subsystems.GameController);

var windowOptions = WindowOptions.Resizable | WindowOptions.AllowHighDpi;
using var window = Window.Create("Dear ImGui SDL2+SDL_Renderer C# example", new(Window.CenteredWindowLocation, new(1280, 720)), windowOptions);

using var renderer = Renderer.Create(window, -1, RendererOptions.PresentVSync | RendererOptions.Accelerated);

using var context = new Imgui.Context();
var io = Imgui.Io.Current;
//io.ConfigFlags |= ImGuiConfigFlags_NavEnableKeyboard;     // Enable Keyboard Controls
//io.ConfigFlags |= ImGuiConfigFlags_NavEnableGamepad;      // Enable Gamepad Controls

Imgui.Style.Current.Dark();
