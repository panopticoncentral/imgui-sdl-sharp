using SdlSharp;
using SdlSharp.Graphics;
using ImguiSharp;
using ImguiSharp.Platform.Sdl;

using var application = new Application(Subsystems.Video | Subsystems.Timer | Subsystems.GameController);

var windowOptions = SdlSharp.Graphics.WindowOptions.Resizable | SdlSharp.Graphics.WindowOptions.AllowHighDpi;
using var window = Window.Create("Dear ImGui SDL2+SDL_Renderer C# example", new(Window.CenteredWindowLocation, new(1280, 720)), windowOptions);

using var renderer = Renderer.Create(window, -1, RendererOptions.PresentVSync | RendererOptions.Accelerated);

#if false
    // Setup Dear ImGui context
    IMGUI_CHECKVERSION();
#endif

Imgui.CreateContext();
var io = Imgui.GetIo();

#if false
    //io.ConfigFlags |= ImGuiConfigFlags_NavEnableKeyboard;     // Enable Keyboard Controls
    //io.ConfigFlags |= ImGuiConfigFlags_NavEnableGamepad;      // Enable Gamepad Controls
#endif

// Setup Dear ImGui style
Imgui.StyleColorsDark();
// Imgui.StyleColorsLight();

ImplSdl2.Init(window, renderer);
#if false
    ImGui_ImplSDLRenderer_Init(renderer);

    // Load Fonts
    // - If no fonts are loaded, dear imgui will use the default font. You can also load multiple fonts and use ImGui::PushFont()/PopFont() to select them.
    // - AddFontFromFileTTF() will return the ImFont* so you can store it if you need to select the font among multiple.
    // - If the file cannot be loaded, the function will return NULL. Please handle those errors in your application (e.g. use an assertion, or display an error and quit).
    // - The fonts will be rasterized at a given size (w/ oversampling) and stored into a texture when calling ImFontAtlas::Build()/GetTexDataAsXXXX(), which ImGui_ImplXXXX_NewFrame below will call.
    // - Use '#define IMGUI_ENABLE_FREETYPE' in your imconfig file to use Freetype for higher quality font rendering.
    // - Read 'docs/FONTS.md' for more instructions and details.
    // - Remember that in C/C++ if you want to include a backslash \ in a string literal you need to write a double backslash \\ !
    //io.Fonts->AddFontDefault();
    //io.Fonts->AddFontFromFileTTF("c:\\Windows\\Fonts\\segoeui.ttf", 18.0f);
    //io.Fonts->AddFontFromFileTTF("../../misc/fonts/DroidSans.ttf", 16.0f);
    //io.Fonts->AddFontFromFileTTF("../../misc/fonts/Roboto-Medium.ttf", 16.0f);
    //io.Fonts->AddFontFromFileTTF("../../misc/fonts/Cousine-Regular.ttf", 15.0f);
    //ImFont* font = io.Fonts->AddFontFromFileTTF("c:\\Windows\\Fonts\\ArialUni.ttf", 18.0f, NULL, io.Fonts->GetGlyphRangesJapanese());
    //IM_ASSERT(font != NULL);

#endif

// Our state
var showDemoWindow = new State<bool>(true);
var showAnotherWindow = new State<bool>(false);
var clearColor = new ImguiSharp.Color(0.45f, 0.55f, 0.60f, 1.00f);

Window.Closed += (sender, args) =>
{
    if (((Window)sender!).Id == window.Id)
    {
        application.Quit();
    }
};

while (application.DispatchEvents())
{
#if false
        ImGui_ImplSDLRenderer_NewFrame();
#endif
    ImplSdl2.NewFrame();
    Imgui.NewFrame();

    if (showDemoWindow)
    {
        Imgui.ShowDemoWindow(showDemoWindow);
    }

#if false

    // 2. Show a simple window that we create ourselves. We use a Begin/End pair to create a named window.
    {
            static float f = 0.0f;
            static int counter = 0;

            ImGui::Begin("Hello, world!");                          // Create a window called "Hello, world!" and append into it.

            ImGui::Text("This is some useful text.");               // Display some text (you can use a format strings too)
            ImGui::Checkbox("Demo Window", &show_demo_window);      // Edit bools storing our window open/close state
            ImGui::Checkbox("Another Window", &show_another_window);

            ImGui::SliderFloat("float", &f, 0.0f, 1.0f);            // Edit 1 float using a slider from 0.0f to 1.0f
            ImGui::ColorEdit3("clear color", (float*)&clear_color); // Edit 3 floats representing a color

            if (ImGui::Button("Button"))                            // Buttons return true when clicked (most widgets return true when edited/activated)
                counter++;
            ImGui::SameLine();
            ImGui::Text("counter = %d", counter);

            ImGui::Text("Application average %.3f ms/frame (%.1f FPS)", 1000.0f / ImGui::GetIO().Framerate, ImGui::GetIO().Framerate);
            ImGui::End();
        }

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
#if false
        ImGui_ImplSDLRenderer_RenderDrawData(ImGui::GetDrawData());
#endif
    renderer.Present();
}

#if false
    // Cleanup
    ImGui_ImplSDLRenderer_Shutdown();
#endif
ImplSdl2.Shutdown();
Imgui.DestroyContext();
