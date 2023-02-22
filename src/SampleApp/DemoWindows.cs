using System.Resources;

using ImguiSharp;

namespace SampleApp
{
    public static class DemoWindows
    {
        private static void HelpMarker(string description)
        {
            Imgui.TextDisabled("(?)");
            if (Imgui.IsItemHovered(HoveredOptions.DelayShort))
            {
                Imgui.BeginTooltip();
                Imgui.PushTextWrapPosition(Imgui.GetFontSize() * 35.0f);
                Imgui.TextUnformatted(description);
                Imgui.PopTextWrapPosition();
                Imgui.EndTooltip();
            }
        }

        private static readonly State<bool> s_showAppMainMenuBar = new(false);
        private static readonly State<bool> s_showAppDocuments = new(false);
        private static readonly State<bool> s_showAppConsole = new(false);
        private static readonly State<bool> s_showAppLog = new(false);
        private static readonly State<bool> s_showAppLayout = new(false);
        private static readonly State<bool> s_showAppPropertyEditor = new(false);
        private static readonly State<bool> s_showAppLongText = new(false);
        private static readonly State<bool> s_showAppAutoResize = new(false);
        private static readonly State<bool> s_showAppConstrainedResize = new(false);
        private static readonly State<bool> s_showAppSimpleOverlay = new(false);
        private static readonly State<bool> s_showAppFullscreen = new(false);
        private static readonly State<bool> s_showAppWindowTitles = new(false);
        private static readonly State<bool> s_showAppCustomRendering = new(false);

        private static readonly State<bool> s_showAppMetrics = new(false);
        private static readonly State<bool> s_showAppDebugLog = new(false);
        private static readonly State<bool> s_showAppStackTool = new(false);
        private static readonly State<bool> s_showAppAbout = new(false);
        private static readonly State<bool> s_showAppStyleEditor = new(false);

        private static readonly State<bool> s_noTitlebar = new(false);
        private static readonly State<bool> s_noScrollbar = new(false);
        private static readonly State<bool> s_noMenu = new(false);
        private static readonly State<bool> s_noMove = new(false);
        private static readonly State<bool> s_noResize = new(false);
        private static readonly State<bool> s_noCollapse = new(false);
        private static readonly State<bool> s_noClose = new(false);
        private static readonly State<bool> s_noNav = new(false);
        private static readonly State<bool> s_noBackground = new(false);
        private static readonly State<bool> s_noBringToFront = new(false);
        private static readonly State<bool> s_unsavedDocument = new(false);

        public static void ShowDemoWindow(State<bool>? open = default)
        {
            if (s_showAppMainMenuBar)
            {
                ShowExampleAppMainMenuBar();
            }

            if (s_showAppDocuments)
            {
                ShowExampleAppDocuments(s_showAppDocuments);
            }

            if (s_showAppConsole)
            {
                ShowExampleAppConsole(s_showAppConsole);
            }

            if (s_showAppLog)
            {
                ShowExampleAppLog(s_showAppLog);
            }

            if (s_showAppLayout)
            {
                ShowExampleAppLayout(s_showAppLayout);
            }

            if (s_showAppPropertyEditor)
            {
                ShowExampleAppPropertyEditor(s_showAppPropertyEditor);
            }

            if (s_showAppLongText)
            {
                ShowExampleAppLongText(s_showAppLongText);
            }

            if (s_showAppAutoResize)
            {
                ShowExampleAppAutoResize(s_showAppAutoResize);
            }

            if (s_showAppConstrainedResize)
            {
                ShowExampleAppConstrainedResize(s_showAppConstrainedResize);
            }

            if (s_showAppSimpleOverlay)
            {
                ShowExampleAppSimpleOverlay(s_showAppSimpleOverlay);
            }

            if (s_showAppFullscreen)
            {
                ShowExampleAppFullscreen(s_showAppFullscreen);
            }

            if (s_showAppWindowTitles)
            {
                ShowExampleAppWindowTitles(s_showAppWindowTitles);
            }

            if (s_showAppCustomRendering)
            {
                ShowExampleAppCustomRendering(s_showAppCustomRendering);
            }

            if (s_showAppMetrics)
            {
                Imgui.ShowMetricsWindow(s_showAppMetrics);
            }

            if (s_showAppDebugLog)
            {
                Imgui.ShowDebugLogWindow(s_showAppDebugLog);
            }

            if (s_showAppStackTool)
            {
                Imgui.ShowStackToolWindow(s_showAppStackTool);
            }

            if (s_showAppAbout)
            {
                ShowAboutWindow(s_showAppAbout);
            }

            if (s_showAppStyleEditor)
            {
                _ = Imgui.Begin("Dear ImGui Style Editor", s_showAppStyleEditor);
                ShowStyleEditor();
                Imgui.End();
            }

            var windowOptions = WindowOptions.None
                | (s_noTitlebar ? WindowOptions.NoTitleBar : 0)
                | (s_noScrollbar ? WindowOptions.NoScrollbar : 0)
                | (!s_noMenu ? WindowOptions.MenuBar : 0)
                | (s_noMove ? WindowOptions.NoMove : 0)
                | (s_noResize ? WindowOptions.NoResize : 0)
                | (s_noCollapse ? WindowOptions.NoCollapse : 0)
                | (s_noNav ? WindowOptions.NoNav : 0)
                | (s_noBackground ? WindowOptions.NoBackground : 0)
                | (s_noBringToFront ? WindowOptions.NoBringToFrontOnFocus : 0)
                | (s_unsavedDocument ? WindowOptions.UnsavedDocument : 0);

            if (s_noClose)
            {
                open = null;
            }

            var mainViewport = Imgui.GetMainViewport();
            Imgui.SetNextWindowPosition(mainViewport.WorkPosition + new Position(650, 20), Condition.FirstUseEver);
            Imgui.SetNextWindowSize(new(550, 680), Condition.FirstUseEver);

            if (!Imgui.Begin("Dear ImGui Managed Demo", open, windowOptions))
            {
                Imgui.End();
                return;
            }

            Imgui.PushItemWidth(Imgui.GetFontSize() * -12);

            if (Imgui.BeginMenuBar())
            {
                if (Imgui.BeginMenu("Menu"))
                {
                    ShowExampleMenuFile();
                    Imgui.EndMenu();
                }
                if (Imgui.BeginMenu("Examples"))
                {
                    _ = Imgui.MenuItem("Main menu bar", null, s_showAppMainMenuBar);
                    _ = Imgui.MenuItem("Console", null, s_showAppConsole);
                    _ = Imgui.MenuItem("Log", null, s_showAppLog);
                    _ = Imgui.MenuItem("Simple layout", null, s_showAppLayout);
                    _ = Imgui.MenuItem("Property editor", null, s_showAppPropertyEditor);
                    _ = Imgui.MenuItem("Long text display", null, s_showAppLongText);
                    _ = Imgui.MenuItem("Auto-resizing window", null, s_showAppAutoResize);
                    _ = Imgui.MenuItem("Constrained-resizing window", null, s_showAppConstrainedResize);
                    _ = Imgui.MenuItem("Simple overlay", null, s_showAppSimpleOverlay);
                    _ = Imgui.MenuItem("Fullscreen window", null, s_showAppFullscreen);
                    _ = Imgui.MenuItem("Manipulating window titles", null, s_showAppWindowTitles);
                    _ = Imgui.MenuItem("Custom rendering", null, s_showAppCustomRendering);
                    _ = Imgui.MenuItem("Documents", null, s_showAppDocuments);
                    Imgui.EndMenu();
                }
                //if (Imgui.MenuItem("MenuItem")) {} // You can also use MenuItem() inside a menu bar!
                if (Imgui.BeginMenu("Tools"))
                {
                    _ = Imgui.MenuItem("Metrics/Debugger", null, s_showAppMetrics);
                    _ = Imgui.MenuItem("Debug Log", null, s_showAppDebugLog);
                    _ = Imgui.MenuItem("Stack Tool", null, s_showAppStackTool);
                    _ = Imgui.MenuItem("Style Editor", null, s_showAppStyleEditor);
                    _ = Imgui.MenuItem("About Dear ImGui", null, s_showAppAbout);
                    Imgui.EndMenu();
                }
                Imgui.EndMenuBar();
            }

            Imgui.Text($"managed dear imgui says hello! ({Imgui.GetVersion()})");
            Imgui.Spacing();

            if (Imgui.CollapsingHeader("Help"))
            {
                Imgui.Text("ABOUT THIS DEMO:");
                Imgui.BulletText("Sections below are demonstrating many aspects of the library.");
                Imgui.BulletText("The \"Examples\" menu above leads to more demo contents.");
                Imgui.BulletText("The \"Tools\" menu above gives access to: About Box, Style Editor,\n" +
                                 "and Metrics/Debugger (general purpose Dear ImGui debugging tool).");
                Imgui.Separator();

                Imgui.Text("PROGRAMMER GUIDE:");
                Imgui.BulletText("See the ShowDemoWindow() code in DemoWindow.cs. <- you are here!");
                Imgui.BulletText("Read the FAQ at http://www.dearimgui.org/faq/");
                Imgui.BulletText("Set 'io.ConfigOptions |= ConfigOptions.NavEnableKeyboard' for keyboard controls.");
                Imgui.BulletText("Set 'io.ConfigOptions |= ConfigOptions.NavEnableGamepad' for gamepad controls.");
                Imgui.Separator();

                Imgui.Text("USER GUIDE:");
                ShowUserGuide();
            }

            if (Imgui.CollapsingHeader("Configuration"))
            {
                var io = Imgui.GetIo();

                if (Imgui.TreeNode("Configuration##2"))
                {
                    //Imgui.CheckboxFlags("io.ConfigFlags: NavEnableKeyboard", &io.ConfigFlags, ImGuiConfigFlags_NavEnableKeyboard);
                    //Imgui.SameLine(); HelpMarker("Enable keyboard controls.");
                    //Imgui.CheckboxFlags("io.ConfigFlags: NavEnableGamepad", &io.ConfigFlags, ImGuiConfigFlags_NavEnableGamepad);
                    //Imgui.SameLine(); HelpMarker("Enable gamepad controls. Require backend to set io.BackendFlags |= ImGuiBackendFlags_HasGamepad.\n\nRead instructions in imgui.cpp for details.");
                    //Imgui.CheckboxFlags("io.ConfigFlags: NavEnableSetMousePos", &io.ConfigFlags, ImGuiConfigFlags_NavEnableSetMousePos);
                    //Imgui.SameLine(); HelpMarker("Instruct navigation to move the mouse cursor. See comment for ImGuiConfigFlags_NavEnableSetMousePos.");
                    //Imgui.CheckboxFlags("io.ConfigFlags: NoMouse", &io.ConfigFlags, ImGuiConfigFlags_NoMouse);
                    //if ((io.ConfigOptions & ConfigOptions.NoMouse) != 0)
                    //{
                    //    // The "NoMouse" option can get us stuck with a disabled mouse! Let's provide an alternative way to fix it:
                    //    if (((float)Imgui.GetTime() % 0.40f) < 0.20f)
                    //    {
                    //        Imgui.SameLine();
                    //        Imgui.Text("<<PRESS SPACE TO DISABLE>>");
                    //    }
                    //    if (Imgui.IsKeyPressed(Key.Space))
                    //    {
                    //        io.ConfigOptions &= ~ConfigOptions.NoMouse;
                    //    }
                    //}
                    //Imgui.CheckboxFlags("io.ConfigFlags: NoMouseCursorChange", &io.ConfigFlags, ImGuiConfigFlags_NoMouseCursorChange);
                    //Imgui.SameLine(); HelpMarker("Instruct backend to not alter mouse cursor shape and visibility.");
                    //Imgui.Checkbox("io.ConfigInputTrickleEventQueue", &io.ConfigInputTrickleEventQueue);
                    //Imgui.SameLine(); HelpMarker("Enable input queue trickling: some types of events submitted during the same frame (e.g. button down + up) will be spread over multiple frames, improving interactions with low framerates.");
                    //Imgui.Checkbox("io.ConfigInputTextCursorBlink", &io.ConfigInputTextCursorBlink);
                    //Imgui.SameLine(); HelpMarker("Enable blinking cursor (optional as some users consider it to be distracting).");
                    //Imgui.Checkbox("io.ConfigInputTextEnterKeepActive", &io.ConfigInputTextEnterKeepActive);
                    //Imgui.SameLine(); HelpMarker("Pressing Enter will keep item active and select contents (single-line only).");
                    //Imgui.Checkbox("io.ConfigDragClickToInputText", &io.ConfigDragClickToInputText);
                    //Imgui.SameLine(); HelpMarker("Enable turning DragXXX widgets into text input with a simple mouse click-release (without moving).");
                    //Imgui.Checkbox("io.ConfigWindowsResizeFromEdges", &io.ConfigWindowsResizeFromEdges);
                    //Imgui.SameLine(); HelpMarker("Enable resizing of windows from their edges and from the lower-left corner.\nThis requires (io.BackendFlags & ImGuiBackendFlags_HasMouseCursors) because it needs mouse cursor feedback.");
                    //Imgui.Checkbox("io.ConfigWindowsMoveFromTitleBarOnly", &io.ConfigWindowsMoveFromTitleBarOnly);
                    //Imgui.Checkbox("io.MouseDrawCursor", &io.MouseDrawCursor);
                    //Imgui.SameLine(); HelpMarker("Instruct Dear ImGui to render a mouse cursor itself. Note that a mouse cursor rendered via your application GPU rendering path will feel more laggy than hardware cursor, but will be more in sync with your other visuals.\n\nSome desktop applications may use both kinds of cursors (e.g. enable software cursor only when resizing/dragging something).");
                    //Imgui.Text("Also see Style->Rendering for rendering options.");
                    Imgui.TreePop();
                    Imgui.Separator();
                }

                if (Imgui.TreeNode("Backend Flags"))
                {
                    //HelpMarker(
                    //    "Those flags are set by the backends (imgui_impl_xxx files) to specify their capabilities.\n"

                    //    "Here we expose them as read-only fields to avoid breaking interactions with your backend.");

                    //// Make a local copy to avoid modifying actual backend flags.
                    //// FIXME: We don't use BeginDisabled() to keep label bright, maybe we need a BeginReadonly() equivalent..
                    //ImGuiBackendFlags backend_flags = io.BackendFlags;
                    //Imgui.CheckboxFlags("io.BackendFlags: HasGamepad", &backend_flags, ImGuiBackendFlags_HasGamepad);
                    //Imgui.CheckboxFlags("io.BackendFlags: HasMouseCursors", &backend_flags, ImGuiBackendFlags_HasMouseCursors);
                    //Imgui.CheckboxFlags("io.BackendFlags: HasSetMousePos", &backend_flags, ImGuiBackendFlags_HasSetMousePos);
                    //Imgui.CheckboxFlags("io.BackendFlags: RendererHasVtxOffset", &backend_flags, ImGuiBackendFlags_RendererHasVtxOffset);
                    Imgui.TreePop();
                    Imgui.Separator();
                }

                if (Imgui.TreeNode("Style"))
                {
                    HelpMarker("The same contents can be accessed in 'Tools->Style Editor' or by calling the ShowStyleEditor() function.");
                    ShowStyleEditor();
                    Imgui.TreePop();
                    Imgui.Separator();
                }

                if (Imgui.TreeNode("Capture/Logging"))
                {
                    HelpMarker(
                        "The logging API redirects all text output so you can easily capture the content of " +
                        "a window or a block. Tree nodes can be automatically expanded.\n" +
                        "Try opening any of the contents below in this window and then click one of the \"Log To\" button.");
                    Imgui.LogButtons();

                    HelpMarker("You can also call Imgui.LogText() to output directly to the log without a visual output.");
                    if (Imgui.Button("Copy \"Hello, world!\" to clipboard"))
                    {
                        Imgui.LogToClipboard();
                        Imgui.LogText("Hello, world!");
                        Imgui.LogFinish();
                    }
                    Imgui.TreePop();
                }
            }

            if (Imgui.CollapsingHeader("Window options"))
            {
                if (Imgui.BeginTable("split", 3))
                {
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No titlebar", s_noTitlebar);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No scrollbar", s_noScrollbar);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No menu", s_noMenu);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No move", s_noMove);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No resize", s_noResize);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No collapse", s_noCollapse);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No close", s_noClose);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No nav", s_noNav);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No background", s_noBackground);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("No bring to front", s_noBringToFront);
                    _ = Imgui.TableNextColumn(); _ = Imgui.Checkbox("Unsaved document", s_unsavedDocument);
                    Imgui.EndTable();
                }
            }

            ShowDemoWindowWidgets();
            ShowDemoWindowLayout();
            ShowDemoWindowPopups();
            ShowDemoWindowTables();
            ShowDemoWindowInputs();

            Imgui.PopItemWidth();
            Imgui.End();
        }

        private static readonly State<bool> s_disableAll = new(false);
        private static int s_clicked;
        private static readonly State<bool> s_check = new(true);
        private static readonly State<int> s_e = new(0);
        private static int s_counter;
        private static readonly State<int> s_itemCurrent = new(0);
        private static readonly StateText s_str0 = new(128, "Hello, world!");
        private static readonly StateText s_str1 = new(128);
        private static readonly State<int> s_i0 = new(123);
        private static readonly State<float> s_f0 = new(0.001f);
        private static readonly State<double> s_d0 = new(999999.00000001);
        private static readonly State<float> s_f1 = new(1.0e10f);
        private static readonly StateVector<float> s_vec3a = new(3, new[] { 0.10f, 0.20f, 0.30f });
        private static readonly State<int> s_i1 = new(50), s_i2 = new(42);
        private static readonly State<float> s_f2 = new(1.00f), s_f3 = new(0.0067f);
        private static readonly State<int> s_i3 = new(0);
        private static readonly State<float> s_f4 = new(0.123f), s_f5 = new(0.0f);
        private static readonly State<float> s_angle = new(0.0f);
        private enum Element { Fire, Earth, Air, Water, Count };
        private static readonly State<int> s_elem = new((int)Element.Fire);
        private static readonly string[] s_elemNames = { "Fire", "Earth", "Air", "Water" };

        private static void ShowDemoWindowWidgets()
        {
            if (!Imgui.CollapsingHeader("Widgets"))
            {
                return;
            }

            if (s_disableAll)
            {
                Imgui.BeginDisabled();
            }

            if (Imgui.TreeNode("Basic"))
            {
                if (Imgui.Button("Button"))
                {
                    s_clicked++;
                }
                if ((s_clicked & 1) != 0)
                {
                    Imgui.SameLine();
                    Imgui.Text("Thanks for clicking me!");
                }

                _ = Imgui.Checkbox("checkbox", s_check);

                _ = Imgui.RadioButton("radio a", s_e, 0);
                Imgui.SameLine();
                _ = Imgui.RadioButton("radio b", s_e, 1);
                Imgui.SameLine();
                _ = Imgui.RadioButton("radio c", s_e, 2);

                for (var i = 0; i < 7; i++)
                {
                    if (i > 0)
                    {
                        Imgui.SameLine();
                    }
                    Imgui.PushId(i);
                    Imgui.PushStyleColor(StyleColor.Button, Color.FromHsv(i / 7.0f, 0.6f, 0.6f));
                    Imgui.PushStyleColor(StyleColor.ButtonHovered, Color.FromHsv(i / 7.0f, 0.7f, 0.7f));
                    Imgui.PushStyleColor(StyleColor.ButtonActive, Color.FromHsv(i / 7.0f, 0.8f, 0.8f));
                    _ = Imgui.Button("Click");
                    Imgui.PopStyleColor(3);
                    Imgui.PopId();
                }

                Imgui.AlignTextToFramePadding();
                Imgui.Text("Hold to repeat:");
                Imgui.SameLine();

                var spacing = Imgui.GetStyle().ItemInnerSpacing.Width;
                Imgui.PushButtonRepeat(true);
                if (Imgui.ArrowButton("##left", Direction.Left))
                {
                    s_counter--;
                }
                Imgui.SameLine(0.0f, spacing);
                if (Imgui.ArrowButton("##right", Direction.Right))
                {
                    s_counter++;
                }
                Imgui.PopButtonRepeat();
                Imgui.SameLine();
                Imgui.Text($"{s_counter}");

                Imgui.Separator();
                Imgui.LabelText("label", "Value");

                {
                    _ = Imgui.Combo("combo", s_itemCurrent, new[] { "AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIIIIII", "JJJJ", "KKKKKKK" });
                    Imgui.SameLine();
                    HelpMarker("Using the simplified one-liner Combo API here.\nRefer to the \"Combo\" section below for an explanation of how to use the more flexible and general BeginCombo/EndCombo API.");
                }

                {
                    _ = Imgui.InputText("input text", s_str0);
                    Imgui.SameLine();
                    HelpMarker(
                        "USER:\n" +
                        "Hold SHIFT or use mouse to select text.\n" +
                        "CTRL+Left/Right to word jump.\n" +
                        "CTRL+A or Double-Click to select all.\n" +
                        "CTRL+X,CTRL+C,CTRL+V clipboard.\n" +
                        "CTRL+Z,CTRL+Y undo/redo.\n" +
                        "ESCAPE to revert.\n\n" +
                        "PROGRAMMER:\n" +
                        "You can use the Resize facility if you need to wire InputText() " +
                        "to a dynamic string type. See misc/cpp/imgui_stdlib.h for an example (this is not demonstrated " +
                        "in imgui_demo.cpp).");

                    _ = Imgui.InputText("input text (w/ hint)", "enter text here", s_str1);

                    _ = Imgui.Input("input int", s_i0);

                    _ = Imgui.Input("input float", s_f0, 0.01f, 1.0f, "%.3f");

                    _ = Imgui.Input("input double", s_d0, 0.01f, 1.0f, "%.8f");

                    _ = Imgui.Input("input scientific", s_f1, 0.0f, 0.0f, "%e");
                    Imgui.SameLine();
                    HelpMarker(
                        "You can input value using the scientific notation,\n" +
                        "  e.g. \"1e+8\" becomes \"100000000\".");

                    _ = Imgui.Input("input float3", s_vec3a);
                }

                {
                    _ = Imgui.Drag("drag int", s_i1, 1);
                    Imgui.SameLine();
                    HelpMarker(
                        "Click and drag to edit value.\n" +
                        "Hold SHIFT/ALT for faster/slower edit.\n" +
                        "Double-click or CTRL+click to input value.");

                    _ = Imgui.Drag("drag int 0..100", s_i2, 1, 0, 100, "%d%%", SliderOptions.AlwaysClamp);

                    _ = Imgui.Drag("drag float", s_f2, 0.005f);
                    _ = Imgui.Drag("drag small float", s_f3, 0.0001f, 0.0f, 0.0f, "%.06f ns");
                }

                {
                    _ = Imgui.Slider("slider int", s_i3, -1, 3);
                    Imgui.SameLine();
                    HelpMarker("CTRL+click to input value.");

                    _ = Imgui.Slider("slider float", s_f4, 0.0f, 1.0f, "ratio = %.3f");
                    _ = Imgui.Slider("slider float (log)", s_f5, -10.0f, 10.0f, "%.4f", SliderOptions.Logarithmic);

                    _ = Imgui.SliderAngle("slider angle", s_angle);

                    var elemName = (s_elem >= 0 && s_elem < (int)Element.Count) ? s_elemNames[s_elem] : "Unknown";
                    _ = Imgui.Slider("slider enum", s_elem, 0, (int)Element.Count - 1, elemName);
                    Imgui.SameLine(); HelpMarker("Using the format string parameter to display a name instead of the underlying integer.");
                }

                //        {
                //            IMGUI_DEMO_MARKER("Widgets/Basic/ColorEdit3, ColorEdit4");
                //            static float col1[3] = { 1.0f, 0.0f, 0.2f };
                //            static float col2[4] = { 0.4f, 0.7f, 0.0f, 0.5f };
                //            Imgui.ColorEdit3("color 1", col1);
                //            Imgui.SameLine(); HelpMarker(
                //                "Click on the color square to open a color picker.\n"
                //                "Click and hold to use drag and drop.\n"
                //                "Right-click on the color square to show options.\n"
                //                "CTRL+click on individual component to input value.\n");
                //
                //            Imgui.ColorEdit4("color 2", col2);
                //        }
                //
                //        {
                //            // Using the _simplified_ one-liner ListBox() api here
                //            // See "List boxes" section for examples of how to use the more flexible BeginListBox()/EndListBox() api.
                //            IMGUI_DEMO_MARKER("Widgets/Basic/ListBox");
                //            const char* items[] = { "Apple", "Banana", "Cherry", "Kiwi", "Mango", "Orange", "Pineapple", "Strawberry", "Watermelon" };
                //            static int item_current = 1;
                //            Imgui.ListBox("listbox", &item_current, items, IM_ARRAYSIZE(items), 4);
                //            Imgui.SameLine(); HelpMarker(
                //                "Using the simplified one-liner ListBox API here.\nRefer to the \"List boxes\" section below for an explanation of how to use the more flexible and general BeginListBox/EndListBox API.");
                //        }
                //
                //        {
                //            // Tooltips
                //            IMGUI_DEMO_MARKER("Widgets/Basic/Tooltips");
                //            Imgui.AlignTextToFramePadding();
                //            Imgui.Text("Tooltips:");
                //
                //            Imgui.SameLine();
                //            Imgui.Button("Button");
                //            if (Imgui.IsItemHovered())
                //                Imgui.SetTooltip("I am a tooltip");
                //
                //            Imgui.SameLine();
                //            Imgui.Button("Fancy");
                //            if (Imgui.IsItemHovered())
                //            {
                //                Imgui.BeginTooltip();
                //                Imgui.Text("I am a fancy tooltip");
                //                static float arr[] = { 0.6f, 0.1f, 1.0f, 0.5f, 0.92f, 0.1f, 0.2f };
                //                Imgui.PlotLines("Curve", arr, IM_ARRAYSIZE(arr));
                //                Imgui.Text("Sin(time) = %f", sinf((float)Imgui.GetTime()));
                //                Imgui.EndTooltip();
                //            }
                //
                //            Imgui.SameLine();
                //            Imgui.Button("Delayed");
                //            if (Imgui.IsItemHovered(ImGuiHoveredFlags_DelayNormal)) // Delay best used on items that highlight on hover, so this not a great example!
                //                Imgui.SetTooltip("I am a tooltip with a delay.");
                //
                //            Imgui.SameLine();
                //            HelpMarker(
                //                "Tooltip are created by using the IsItemHovered() function over any kind of item.");
                //
                //        }

                Imgui.TreePop();
            }

            //    // Testing ImGuiOnceUponAFrame helper.
            //    //static ImGuiOnceUponAFrame once;
            //    //for (int i = 0; i < 5; i++)
            //    //    if (once)
            //    //        Imgui.Text("This will be displayed only once.");
            //
            //    IMGUI_DEMO_MARKER("Widgets/Trees");
            //    if (Imgui.TreeNode("Trees"))
            //    {
            //        IMGUI_DEMO_MARKER("Widgets/Trees/Basic trees");
            //        if (Imgui.TreeNode("Basic trees"))
            //        {
            //            for (int i = 0; i < 5; i++)
            //            {
            //                // Use SetNextItemOpen() so set the default state of a node to be open. We could
            //                // also use TreeNodeEx() with the ImGuiTreeNodeFlags_DefaultOpen flag to achieve the same thing!
            //                if (i == 0)
            //                    Imgui.SetNextItemOpen(true, ImGuiCond_Once);
            //
            //                if (Imgui.TreeNode((void*)(intptr_t)i, "Child %d", i))
            //                {
            //                    Imgui.Text("blah blah");
            //                    Imgui.SameLine();
            //                    if (Imgui.SmallButton("button")) {}
            //                    Imgui.TreePop();
            //                }
            //            }
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Trees/Advanced, with Selectable nodes");
            //        if (Imgui.TreeNode("Advanced, with Selectable nodes"))
            //        {
            //            HelpMarker(
            //                "This is a more typical looking tree with selectable nodes.\n"
            //                "Click to select, CTRL+Click to toggle, click on arrows or double-click to open.");
            //            static ImGuiTreeNodeFlags base_flags = ImGuiTreeNodeFlags_OpenOnArrow | ImGuiTreeNodeFlags_OpenOnDoubleClick | ImGuiTreeNodeFlags_SpanAvailWidth;
            //            static bool align_label_with_current_x_position = false;
            //            static bool test_drag_and_drop = false;
            //            Imgui.CheckboxFlags("ImGuiTreeNodeFlags_OpenOnArrow",       &base_flags, ImGuiTreeNodeFlags_OpenOnArrow);
            //            Imgui.CheckboxFlags("ImGuiTreeNodeFlags_OpenOnDoubleClick", &base_flags, ImGuiTreeNodeFlags_OpenOnDoubleClick);
            //            Imgui.CheckboxFlags("ImGuiTreeNodeFlags_SpanAvailWidth",    &base_flags, ImGuiTreeNodeFlags_SpanAvailWidth); Imgui.SameLine(); HelpMarker("Extend hit area to all available width instead of allowing more items to be laid out after the node.");
            //            Imgui.CheckboxFlags("ImGuiTreeNodeFlags_SpanFullWidth",     &base_flags, ImGuiTreeNodeFlags_SpanFullWidth);
            //            Imgui.Checkbox("Align label with current X position", &align_label_with_current_x_position);
            //            Imgui.Checkbox("Test tree node as drag source", &test_drag_and_drop);
            //            Imgui.Text("Hello!");
            //            if (align_label_with_current_x_position)
            //                Imgui.Unindent(Imgui.GetTreeNodeToLabelSpacing());
            //
            //            // 'selection_mask' is dumb representation of what may be user-side selection state.
            //            //  You may retain selection state inside or outside your objects in whatever format you see fit.
            //            // 'node_clicked' is temporary storage of what node we have clicked to process selection at the end
            //            /// of the loop. May be a pointer to your own node type, etc.
            //            static int selection_mask = (1 << 2);
            //            int node_clicked = -1;
            //            for (int i = 0; i < 6; i++)
            //            {
            //                // Disable the default "open on single-click behavior" + set Selected flag according to our selection.
            //                // To alter selection we use IsItemClicked() && !IsItemToggledOpen(), so clicking on an arrow doesn't alter selection.
            //                ImGuiTreeNodeFlags node_flags = base_flags;
            //                const bool is_selected = (selection_mask & (1 << i)) != 0;
            //                if (is_selected)
            //                    node_flags |= ImGuiTreeNodeFlags_Selected;
            //                if (i < 3)
            //                {
            //                    // Items 0..2 are Tree Node
            //                    bool node_open = Imgui.TreeNodeEx((void*)(intptr_t)i, node_flags, "Selectable Node %d", i);
            //                    if (Imgui.IsItemClicked() && !Imgui.IsItemToggledOpen())
            //                        node_clicked = i;
            //                    if (test_drag_and_drop && Imgui.BeginDragDropSource())
            //                    {
            //                        Imgui.SetDragDropPayload("_TREENODE", NULL, 0);
            //                        Imgui.Text("This is a drag and drop source");
            //                        Imgui.EndDragDropSource();
            //                    }
            //                    if (node_open)
            //                    {
            //                        Imgui.BulletText("Blah blah\nBlah Blah");
            //                        Imgui.TreePop();
            //                    }
            //                }
            //                else
            //                {
            //                    // Items 3..5 are Tree Leaves
            //                    // The only reason we use TreeNode at all is to allow selection of the leaf. Otherwise we can
            //                    // use BulletText() or advance the cursor by GetTreeNodeToLabelSpacing() and call Text().
            //                    node_flags |= ImGuiTreeNodeFlags_Leaf | ImGuiTreeNodeFlags_NoTreePushOnOpen; // ImGuiTreeNodeFlags_Bullet
            //                    Imgui.TreeNodeEx((void*)(intptr_t)i, node_flags, "Selectable Leaf %d", i);
            //                    if (Imgui.IsItemClicked() && !Imgui.IsItemToggledOpen())
            //                        node_clicked = i;
            //                    if (test_drag_and_drop && Imgui.BeginDragDropSource())
            //                    {
            //                        Imgui.SetDragDropPayload("_TREENODE", NULL, 0);
            //                        Imgui.Text("This is a drag and drop source");
            //                        Imgui.EndDragDropSource();
            //                    }
            //                }
            //            }
            //            if (node_clicked != -1)
            //            {
            //                // Update selection state
            //                // (process outside of tree loop to avoid visual inconsistencies during the clicking frame)
            //                if (Imgui.GetIO().KeyCtrl)
            //                    selection_mask ^= (1 << node_clicked);          // CTRL+click to toggle
            //                else //if (!(selection_mask & (1 << node_clicked))) // Depending on selection behavior you want, may want to preserve selection when clicking on item that is part of the selection
            //                    selection_mask = (1 << node_clicked);           // Click to single-select
            //            }
            //            if (align_label_with_current_x_position)
            //                Imgui.Indent(Imgui.GetTreeNodeToLabelSpacing());
            //            Imgui.TreePop();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Collapsing Headers");
            //    if (Imgui.TreeNode("Collapsing Headers"))
            //    {
            //        static bool closable_group = true;
            //        Imgui.Checkbox("Show 2nd header", &closable_group);
            //        if (Imgui.CollapsingHeader("Header", ImGuiTreeNodeFlags_None))
            //        {
            //            Imgui.Text("IsItemHovered: %d", Imgui.IsItemHovered());
            //            for (int i = 0; i < 5; i++)
            //                Imgui.Text("Some content %d", i);
            //        }
            //        if (Imgui.CollapsingHeader("Header with a close button", &closable_group))
            //        {
            //            Imgui.Text("IsItemHovered: %d", Imgui.IsItemHovered());
            //            for (int i = 0; i < 5; i++)
            //                Imgui.Text("More content %d", i);
            //        }
            //        /*
            //        if (Imgui.CollapsingHeader("Header with a bullet", ImGuiTreeNodeFlags_Bullet))
            //            Imgui.Text("IsItemHovered: %d", Imgui.IsItemHovered());
            //        */
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Bullets");
            //    if (Imgui.TreeNode("Bullets"))
            //    {
            //        Imgui.BulletText("Bullet point 1");
            //        Imgui.BulletText("Bullet point 2\nOn multiple lines");
            //        if (Imgui.TreeNode("Tree node"))
            //        {
            //            Imgui.BulletText("Another bullet point");
            //            Imgui.TreePop();
            //        }
            //        Imgui.Bullet(); Imgui.Text("Bullet point 3 (two calls)");
            //        Imgui.Bullet(); Imgui.SmallButton("Button");
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Text");
            //    if (Imgui.TreeNode("Text"))
            //    {
            //        IMGUI_DEMO_MARKER("Widgets/Text/Colored Text");
            //        if (Imgui.TreeNode("Colorful Text"))
            //        {
            //            // Using shortcut. You can use PushStyleColor()/PopStyleColor() for more flexibility.
            //            Imgui.TextColored(ImVec4(1.0f, 0.0f, 1.0f, 1.0f), "Pink");
            //            Imgui.TextColored(ImVec4(1.0f, 1.0f, 0.0f, 1.0f), "Yellow");
            //            Imgui.TextDisabled("Disabled");
            //            Imgui.SameLine(); HelpMarker("The TextDisabled color is stored in ImGuiStyle.");
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Text/Word Wrapping");
            //        if (Imgui.TreeNode("Word Wrapping"))
            //        {
            //            // Using shortcut. You can use PushTextWrapPos()/PopTextWrapPos() for more flexibility.
            //            Imgui.TextWrapped(
            //                "This text should automatically wrap on the edge of the window. The current implementation "
            //                "for text wrapping follows simple rules suitable for English and possibly other languages.");
            //            Imgui.Spacing();
            //
            //            static float wrap_width = 200.0f;
            //            Imgui.SliderFloat("Wrap width", &wrap_width, -20, 600, "%.0f");
            //
            //            ImDrawList* draw_list = Imgui.GetWindowDrawList();
            //            for (int n = 0; n < 2; n++)
            //            {
            //                Imgui.Text("Test paragraph %d:", n);
            //                ImVec2 pos = Imgui.GetCursorScreenPos();
            //                ImVec2 marker_min = ImVec2(pos.x + wrap_width, pos.y);
            //                ImVec2 marker_max = ImVec2(pos.x + wrap_width + 10, pos.y + Imgui.GetTextLineHeight());
            //                Imgui.PushTextWrapPos(Imgui.GetCursorPos().x + wrap_width);
            //                if (n == 0)
            //                    Imgui.Text("The lazy dog is a good dog. This paragraph should fit within %.0f pixels. Testing a 1 character word. The quick brown fox jumps over the lazy dog.", wrap_width);
            //                else
            //                    Imgui.Text("aaaaaaaa bbbbbbbb, c cccccccc,dddddddd. d eeeeeeee   ffffffff. gggggggg!hhhhhhhh");
            //
            //                // Draw actual text bounding box, following by marker of our expected limit (should not overlap!)
            //                draw_list->AddRect(Imgui.GetItemRectMin(), Imgui.GetItemRectMax(), IM_COL32(255, 255, 0, 255));
            //                draw_list->AddRectFilled(marker_min, marker_max, IM_COL32(255, 0, 255, 255));
            //                Imgui.PopTextWrapPos();
            //            }
            //
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Text/UTF-8 Text");
            //        if (Imgui.TreeNode("UTF-8 Text"))
            //        {
            //            // UTF-8 test with Japanese characters
            //            // (Needs a suitable font? Try "Google Noto" or "Arial Unicode". See docs/FONTS.md for details.)
            //            // - From C++11 you can use the u8"my text" syntax to encode literal strings as UTF-8
            //            // - For earlier compiler, you may be able to encode your sources as UTF-8 (e.g. in Visual Studio, you
            //            //   can save your source files as 'UTF-8 without signature').
            //            // - FOR THIS DEMO FILE ONLY, BECAUSE WE WANT TO SUPPORT OLD COMPILERS, WE ARE *NOT* INCLUDING RAW UTF-8
            //            //   CHARACTERS IN THIS SOURCE FILE. Instead we are encoding a few strings with hexadecimal constants.
            //            //   Don't do this in your application! Please use u8"text in any language" in your application!
            //            // Note that characters values are preserved even by InputText() if the font cannot be displayed,
            //            // so you can safely copy & paste garbled characters into another application.
            //            Imgui.TextWrapped(
            //                "CJK text will only appear if the font was loaded with the appropriate CJK character ranges. "
            //                "Call io.Fonts->AddFontFromFileTTF() manually to load extra character ranges. "
            //                "Read docs/FONTS.md for details.");
            //            Imgui.Text("Hiragana: \xe3\x81\x8b\xe3\x81\x8d\xe3\x81\x8f\xe3\x81\x91\xe3\x81\x93 (kakikukeko)"); // Normally we would use u8"blah blah" with the proper characters directly in the string.
            //            Imgui.Text("Kanjis: \xe6\x97\xa5\xe6\x9c\xac\xe8\xaa\x9e (nihongo)");
            //            static char buf[32] = "\xe6\x97\xa5\xe6\x9c\xac\xe8\xaa\x9e";
            //            //static char buf[32] = u8"NIHONGO"; // <- this is how you would write it with C++11, using real kanjis
            //            Imgui.InputText("UTF-8 input", buf, IM_ARRAYSIZE(buf));
            //            Imgui.TreePop();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Images");
            //    if (Imgui.TreeNode("Images"))
            //    {
            //        ImGuiIO& io = Imgui.GetIO();
            //        Imgui.TextWrapped(
            //            "Below we are displaying the font texture (which is the only texture we have access to in this demo). "
            //            "Use the 'ImTextureID' type as storage to pass pointers or identifier to your own texture data. "
            //            "Hover the texture for a zoomed view!");
            //
            //        // Below we are displaying the font texture because it is the only texture we have access to inside the demo!
            //        // Remember that ImTextureID is just storage for whatever you want it to be. It is essentially a value that
            //        // will be passed to the rendering backend via the ImDrawCmd structure.
            //        // If you use one of the default imgui_impl_XXXX.cpp rendering backend, they all have comments at the top
            //        // of their respective source file to specify what they expect to be stored in ImTextureID, for example:
            //        // - The imgui_impl_dx11.cpp renderer expect a 'ID3D11ShaderResourceView*' pointer
            //        // - The imgui_impl_opengl3.cpp renderer expect a GLuint OpenGL texture identifier, etc.
            //        // More:
            //        // - If you decided that ImTextureID = MyEngineTexture*, then you can pass your MyEngineTexture* pointers
            //        //   to Imgui.Image(), and gather width/height through your own functions, etc.
            //        // - You can use ShowMetricsWindow() to inspect the draw data that are being passed to your renderer,
            //        //   it will help you debug issues if you are confused about it.
            //        // - Consider using the lower-level ImDrawList::AddImage() API, via Imgui.GetWindowDrawList()->AddImage().
            //        // - Read https://github.com/ocornut/imgui/blob/master/docs/FAQ.md
            //        // - Read https://github.com/ocornut/imgui/wiki/Image-Loading-and-Displaying-Examples
            //        ImTextureID my_tex_id = io.Fonts->TexID;
            //        float my_tex_w = (float)io.Fonts->TexWidth;
            //        float my_tex_h = (float)io.Fonts->TexHeight;
            //        {
            //            Imgui.Text("%.0fx%.0f", my_tex_w, my_tex_h);
            //            ImVec2 pos = Imgui.GetCursorScreenPos();
            //            ImVec2 uv_min = ImVec2(0.0f, 0.0f);                 // Top-left
            //            ImVec2 uv_max = ImVec2(1.0f, 1.0f);                 // Lower-right
            //            ImVec4 tint_col = ImVec4(1.0f, 1.0f, 1.0f, 1.0f);   // No tint
            //            ImVec4 border_col = ImVec4(1.0f, 1.0f, 1.0f, 0.5f); // 50% opaque white
            //            Imgui.Image(my_tex_id, ImVec2(my_tex_w, my_tex_h), uv_min, uv_max, tint_col, border_col);
            //            if (Imgui.IsItemHovered())
            //            {
            //                Imgui.BeginTooltip();
            //                float region_sz = 32.0f;
            //                float region_x = io.MousePos.x - pos.x - region_sz * 0.5f;
            //                float region_y = io.MousePos.y - pos.y - region_sz * 0.5f;
            //                float zoom = 4.0f;
            //                if (region_x < 0.0f) { region_x = 0.0f; }
            //                else if (region_x > my_tex_w - region_sz) { region_x = my_tex_w - region_sz; }
            //                if (region_y < 0.0f) { region_y = 0.0f; }
            //                else if (region_y > my_tex_h - region_sz) { region_y = my_tex_h - region_sz; }
            //                Imgui.Text("Min: (%.2f, %.2f)", region_x, region_y);
            //                Imgui.Text("Max: (%.2f, %.2f)", region_x + region_sz, region_y + region_sz);
            //                ImVec2 uv0 = ImVec2((region_x) / my_tex_w, (region_y) / my_tex_h);
            //                ImVec2 uv1 = ImVec2((region_x + region_sz) / my_tex_w, (region_y + region_sz) / my_tex_h);
            //                Imgui.Image(my_tex_id, ImVec2(region_sz * zoom, region_sz * zoom), uv0, uv1, tint_col, border_col);
            //                Imgui.EndTooltip();
            //            }
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Images/Textured buttons");
            //        Imgui.TextWrapped("And now some textured buttons..");
            //        static int pressed_count = 0;
            //        for (int i = 0; i < 8; i++)
            //        {
            //            // UV coordinates are often (0.0f, 0.0f) and (1.0f, 1.0f) to display an entire textures.
            //            // Here are trying to display only a 32x32 pixels area of the texture, hence the UV computation.
            //            // Read about UV coordinates here: https://github.com/ocornut/imgui/wiki/Image-Loading-and-Displaying-Examples
            //            Imgui.PushID(i);
            //            if (i > 0)
            //                Imgui.PushStyleVar(ImGuiStyleVar_FramePadding, ImVec2(i - 1.0f, i - 1.0f));
            //            ImVec2 size = ImVec2(32.0f, 32.0f);                         // Size of the image we want to make visible
            //            ImVec2 uv0 = ImVec2(0.0f, 0.0f);                            // UV coordinates for lower-left
            //            ImVec2 uv1 = ImVec2(32.0f / my_tex_w, 32.0f / my_tex_h);    // UV coordinates for (32,32) in our texture
            //            ImVec4 bg_col = ImVec4(0.0f, 0.0f, 0.0f, 1.0f);             // Black background
            //            ImVec4 tint_col = ImVec4(1.0f, 1.0f, 1.0f, 1.0f);           // No tint
            //            if (Imgui.ImageButton("", my_tex_id, size, uv0, uv1, bg_col, tint_col))
            //                pressed_count += 1;
            //            if (i > 0)
            //                Imgui.PopStyleVar();
            //            Imgui.PopID();
            //            Imgui.SameLine();
            //        }
            //        Imgui.NewLine();
            //        Imgui.Text("Pressed %d times.", pressed_count);
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Combo");
            //    if (Imgui.TreeNode("Combo"))
            //    {
            //        // Combo Boxes are also called "Dropdown" in other systems
            //        // Expose flags as checkbox for the demo
            //        static ImGuiComboFlags flags = 0;
            //        Imgui.CheckboxFlags("ImGuiComboFlags_PopupAlignLeft", &flags, ImGuiComboFlags_PopupAlignLeft);
            //        Imgui.SameLine(); HelpMarker("Only makes a difference if the popup is larger than the combo");
            //        if (Imgui.CheckboxFlags("ImGuiComboFlags_NoArrowButton", &flags, ImGuiComboFlags_NoArrowButton))
            //            flags &= ~ImGuiComboFlags_NoPreview;     // Clear the other flag, as we cannot combine both
            //        if (Imgui.CheckboxFlags("ImGuiComboFlags_NoPreview", &flags, ImGuiComboFlags_NoPreview))
            //            flags &= ~ImGuiComboFlags_NoArrowButton; // Clear the other flag, as we cannot combine both
            //
            //        // Using the generic BeginCombo() API, you have full control over how to display the combo contents.
            //        // (your selection data could be an index, a pointer to the object, an id for the object, a flag intrusively
            //        // stored in the object itself, etc.)
            //        const char* items[] = { "AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIII", "JJJJ", "KKKK", "LLLLLLL", "MMMM", "OOOOOOO" };
            //        static int item_current_idx = 0; // Here we store our selection data as an index.
            //        const char* combo_preview_value = items[item_current_idx];  // Pass in the preview value visible before opening the combo (it could be anything)
            //        if (Imgui.BeginCombo("combo 1", combo_preview_value, flags))
            //        {
            //            for (int n = 0; n < IM_ARRAYSIZE(items); n++)
            //            {
            //                const bool is_selected = (item_current_idx == n);
            //                if (Imgui.Selectable(items[n], is_selected))
            //                    item_current_idx = n;
            //
            //                // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
            //                if (is_selected)
            //                    Imgui.SetItemDefaultFocus();
            //            }
            //            Imgui.EndCombo();
            //        }
            //
            //        // Simplified one-liner Combo() API, using values packed in a single constant string
            //        // This is a convenience for when the selection set is small and known at compile-time.
            //        static int item_current_2 = 0;
            //        Imgui.Combo("combo 2 (one-liner)", &item_current_2, "aaaa\0bbbb\0cccc\0dddd\0eeee\0\0");
            //
            //        // Simplified one-liner Combo() using an array of const char*
            //        // This is not very useful (may obsolete): prefer using BeginCombo()/EndCombo() for full control.
            //        static int item_current_3 = -1; // If the selection isn't within 0..count, Combo won't display a preview
            //        Imgui.Combo("combo 3 (array)", &item_current_3, items, IM_ARRAYSIZE(items));
            //
            //        // Simplified one-liner Combo() using an accessor function
            //        struct Funcs { static bool ItemGetter(void* data, int n, const char** out_str) { *out_str = ((const char**)data)[n]; return true; } };
            //        static int item_current_4 = 0;
            //        Imgui.Combo("combo 4 (function)", &item_current_4, &Funcs::ItemGetter, items, IM_ARRAYSIZE(items));
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/List Boxes");
            //    if (Imgui.TreeNode("List boxes"))
            //    {
            //        // Using the generic BeginListBox() API, you have full control over how to display the combo contents.
            //        // (your selection data could be an index, a pointer to the object, an id for the object, a flag intrusively
            //        // stored in the object itself, etc.)
            //        const char* items[] = { "AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIII", "JJJJ", "KKKK", "LLLLLLL", "MMMM", "OOOOOOO" };
            //        static int item_current_idx = 0; // Here we store our selection data as an index.
            //        if (Imgui.BeginListBox("listbox 1"))
            //        {
            //            for (int n = 0; n < IM_ARRAYSIZE(items); n++)
            //            {
            //                const bool is_selected = (item_current_idx == n);
            //                if (Imgui.Selectable(items[n], is_selected))
            //                    item_current_idx = n;
            //
            //                // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
            //                if (is_selected)
            //                    Imgui.SetItemDefaultFocus();
            //            }
            //            Imgui.EndListBox();
            //        }
            //
            //        // Custom size: use all width, 5 items tall
            //        Imgui.Text("Full-width:");
            //        if (Imgui.BeginListBox("##listbox 2", ImVec2(-FLT_MIN, 5 * Imgui.GetTextLineHeightWithSpacing())))
            //        {
            //            for (int n = 0; n < IM_ARRAYSIZE(items); n++)
            //            {
            //                const bool is_selected = (item_current_idx == n);
            //                if (Imgui.Selectable(items[n], is_selected))
            //                    item_current_idx = n;
            //
            //                // Set the initial focus when opening the combo (scrolling + keyboard navigation focus)
            //                if (is_selected)
            //                    Imgui.SetItemDefaultFocus();
            //            }
            //            Imgui.EndListBox();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Selectables");
            //    if (Imgui.TreeNode("Selectables"))
            //    {
            //        // Selectable() has 2 overloads:
            //        // - The one taking "bool selected" as a read-only selection information.
            //        //   When Selectable() has been clicked it returns true and you can alter selection state accordingly.
            //        // - The one taking "bool* p_selected" as a read-write selection information (convenient in some cases)
            //        // The earlier is more flexible, as in real application your selection may be stored in many different ways
            //        // and not necessarily inside a bool value (e.g. in flags within objects, as an external list, etc).
            //        IMGUI_DEMO_MARKER("Widgets/Selectables/Basic");
            //        if (Imgui.TreeNode("Basic"))
            //        {
            //            static bool selection[5] = { false, true, false, false, false };
            //            Imgui.Selectable("1. I am selectable", &selection[0]);
            //            Imgui.Selectable("2. I am selectable", &selection[1]);
            //            Imgui.Text("(I am not selectable)");
            //            Imgui.Selectable("4. I am selectable", &selection[3]);
            //            if (Imgui.Selectable("5. I am double clickable", selection[4], ImGuiSelectableFlags_AllowDoubleClick))
            //                if (Imgui.IsMouseDoubleClicked(0))
            //                    selection[4] = !selection[4];
            //            Imgui.TreePop();
            //        }
            //        IMGUI_DEMO_MARKER("Widgets/Selectables/Single Selection");
            //        if (Imgui.TreeNode("Selection State: Single Selection"))
            //        {
            //            static int selected = -1;
            //            for (int n = 0; n < 5; n++)
            //            {
            //                char buf[32];
            //                sprintf(buf, "Object %d", n);
            //                if (Imgui.Selectable(buf, selected == n))
            //                    selected = n;
            //            }
            //            Imgui.TreePop();
            //        }
            //        IMGUI_DEMO_MARKER("Widgets/Selectables/Multiple Selection");
            //        if (Imgui.TreeNode("Selection State: Multiple Selection"))
            //        {
            //            HelpMarker("Hold CTRL and click to select multiple items.");
            //            static bool selection[5] = { false, false, false, false, false };
            //            for (int n = 0; n < 5; n++)
            //            {
            //                char buf[32];
            //                sprintf(buf, "Object %d", n);
            //                if (Imgui.Selectable(buf, selection[n]))
            //                {
            //                    if (!Imgui.GetIO().KeyCtrl)    // Clear selection when CTRL is not held
            //                        memset(selection, 0, sizeof(selection));
            //                    selection[n] ^= 1;
            //                }
            //            }
            //            Imgui.TreePop();
            //        }
            //        IMGUI_DEMO_MARKER("Widgets/Selectables/Rendering more text into the same line");
            //        if (Imgui.TreeNode("Rendering more text into the same line"))
            //        {
            //            // Using the Selectable() override that takes "bool* p_selected" parameter,
            //            // this function toggle your bool value automatically.
            //            static bool selected[3] = { false, false, false };
            //            Imgui.Selectable("main.c",    &selected[0]); Imgui.SameLine(300); Imgui.Text(" 2,345 bytes");
            //            Imgui.Selectable("Hello.cpp", &selected[1]); Imgui.SameLine(300); Imgui.Text("12,345 bytes");
            //            Imgui.Selectable("Hello.h",   &selected[2]); Imgui.SameLine(300); Imgui.Text(" 2,345 bytes");
            //            Imgui.TreePop();
            //        }
            //        IMGUI_DEMO_MARKER("Widgets/Selectables/In columns");
            //        if (Imgui.TreeNode("In columns"))
            //        {
            //            static bool selected[10] = {};
            //
            //            if (Imgui.BeginTable("split1", 3, ImGuiTableFlags_Resizable | ImGuiTableFlags_NoSavedSettings | ImGuiTableFlags_Borders))
            //            {
            //                for (int i = 0; i < 10; i++)
            //                {
            //                    char label[32];
            //                    sprintf(label, "Item %d", i);
            //                    Imgui.TableNextColumn();
            //                    Imgui.Selectable(label, &selected[i]); // FIXME-TABLE: Selection overlap
            //                }
            //                Imgui.EndTable();
            //            }
            //            Imgui.Spacing();
            //            if (Imgui.BeginTable("split2", 3, ImGuiTableFlags_Resizable | ImGuiTableFlags_NoSavedSettings | ImGuiTableFlags_Borders))
            //            {
            //                for (int i = 0; i < 10; i++)
            //                {
            //                    char label[32];
            //                    sprintf(label, "Item %d", i);
            //                    Imgui.TableNextRow();
            //                    Imgui.TableNextColumn();
            //                    Imgui.Selectable(label, &selected[i], ImGuiSelectableFlags_SpanAllColumns);
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("Some other contents");
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("123456");
            //                }
            //                Imgui.EndTable();
            //            }
            //            Imgui.TreePop();
            //        }
            //        IMGUI_DEMO_MARKER("Widgets/Selectables/Grid");
            //        if (Imgui.TreeNode("Grid"))
            //        {
            //            static char selected[4][4] = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            //
            //            // Add in a bit of silly fun...
            //            const float time = (float)Imgui.GetTime();
            //            const bool winning_state = memchr(selected, 0, sizeof(selected)) == NULL; // If all cells are selected...
            //            if (winning_state)
            //                Imgui.PushStyleVar(ImGuiStyleVar_SelectableTextAlign, ImVec2(0.5f + 0.5f * cosf(time * 2.0f), 0.5f + 0.5f * sinf(time * 3.0f)));
            //
            //            for (int y = 0; y < 4; y++)
            //                for (int x = 0; x < 4; x++)
            //                {
            //                    if (x > 0)
            //                        Imgui.SameLine();
            //                    Imgui.PushID(y * 4 + x);
            //                    if (Imgui.Selectable("Sailor", selected[y][x] != 0, 0, ImVec2(50, 50)))
            //                    {
            //                        // Toggle clicked cell + toggle neighbors
            //                        selected[y][x] ^= 1;
            //                        if (x > 0) { selected[y][x - 1] ^= 1; }
            //                        if (x < 3) { selected[y][x + 1] ^= 1; }
            //                        if (y > 0) { selected[y - 1][x] ^= 1; }
            //                        if (y < 3) { selected[y + 1][x] ^= 1; }
            //                    }
            //                    Imgui.PopID();
            //                }
            //
            //            if (winning_state)
            //                Imgui.PopStyleVar();
            //            Imgui.TreePop();
            //        }
            //        IMGUI_DEMO_MARKER("Widgets/Selectables/Alignment");
            //        if (Imgui.TreeNode("Alignment"))
            //        {
            //            HelpMarker(
            //                "By default, Selectables uses style.SelectableTextAlign but it can be overridden on a per-item "
            //                "basis using PushStyleVar(). You'll probably want to always keep your default situation to "
            //                "left-align otherwise it becomes difficult to layout multiple items on a same line");
            //            static bool selected[3 * 3] = { true, false, true, false, true, false, true, false, true };
            //            for (int y = 0; y < 3; y++)
            //            {
            //                for (int x = 0; x < 3; x++)
            //                {
            //                    ImVec2 alignment = ImVec2((float)x / 2.0f, (float)y / 2.0f);
            //                    char name[32];
            //                    sprintf(name, "(%.1f,%.1f)", alignment.x, alignment.y);
            //                    if (x > 0) Imgui.SameLine();
            //                    Imgui.PushStyleVar(ImGuiStyleVar_SelectableTextAlign, alignment);
            //                    Imgui.Selectable(name, &selected[3 * y + x], ImGuiSelectableFlags_None, ImVec2(80, 80));
            //                    Imgui.PopStyleVar();
            //                }
            //            }
            //            Imgui.TreePop();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    // To wire InputText() with std::string or any other custom string type,
            //    // see the "Text Input > Resize Callback" section of this demo, and the misc/cpp/imgui_stdlib.h file.
            //    IMGUI_DEMO_MARKER("Widgets/Text Input");
            //    if (Imgui.TreeNode("Text Input"))
            //    {
            //        IMGUI_DEMO_MARKER("Widgets/Text Input/Multi-line Text Input");
            //        if (Imgui.TreeNode("Multi-line Text Input"))
            //        {
            //            // Note: we are using a fixed-sized buffer for simplicity here. See ImGuiInputTextFlags_CallbackResize
            //            // and the code in misc/cpp/imgui_stdlib.h for how to setup InputText() for dynamically resizing strings.
            //            static char text[1024 * 16] =
            //                "/*\n"
            //                " The Pentium F00F bug, shorthand for F0 0F C7 C8,\n"
            //                " the hexadecimal encoding of one offending instruction,\n"
            //                " more formally, the invalid operand with locked CMPXCHG8B\n"
            //                " instruction bug, is a design flaw in the majority of\n"
            //                " Intel Pentium, Pentium MMX, and Pentium OverDrive\n"
            //                " processors (all in the P5 microarchitecture).\n"
            //                "*/\n\n"
            //                "label:\n"
            //                "\tlock cmpxchg8b eax\n";
            //
            //            static ImGuiInputTextFlags flags = ImGuiInputTextFlags_AllowTabInput;
            //            HelpMarker("You can use the ImGuiInputTextFlags_CallbackResize facility if you need to wire InputTextMultiline() to a dynamic string type. See misc/cpp/imgui_stdlib.h for an example. (This is not demonstrated in imgui_demo.cpp because we don't want to include <string> in here)");
            //            Imgui.CheckboxFlags("ImGuiInputTextFlags_ReadOnly", &flags, ImGuiInputTextFlags_ReadOnly);
            //            Imgui.CheckboxFlags("ImGuiInputTextFlags_AllowTabInput", &flags, ImGuiInputTextFlags_AllowTabInput);
            //            Imgui.CheckboxFlags("ImGuiInputTextFlags_CtrlEnterForNewLine", &flags, ImGuiInputTextFlags_CtrlEnterForNewLine);
            //            Imgui.InputTextMultiline("##source", text, IM_ARRAYSIZE(text), ImVec2(-FLT_MIN, Imgui.GetTextLineHeight() * 16), flags);
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Text Input/Filtered Text Input");
            //        if (Imgui.TreeNode("Filtered Text Input"))
            //        {
            //            struct TextFilters
            //            {
            //                // Return 0 (pass) if the character is 'i' or 'm' or 'g' or 'u' or 'i'
            //                static int FilterImGuiLetters(ImGuiInputTextCallbackData* data)
            //                {
            //                    if (data->EventChar < 256 && strchr("imgui", (char)data->EventChar))
            //                        return 0;
            //                    return 1;
            //                }
            //            };
            //
            //            static char buf1[64] = ""; Imgui.InputText("default",     buf1, 64);
            //            static char buf2[64] = ""; Imgui.InputText("decimal",     buf2, 64, ImGuiInputTextFlags_CharsDecimal);
            //            static char buf3[64] = ""; Imgui.InputText("hexadecimal", buf3, 64, ImGuiInputTextFlags_CharsHexadecimal | ImGuiInputTextFlags_CharsUppercase);
            //            static char buf4[64] = ""; Imgui.InputText("uppercase",   buf4, 64, ImGuiInputTextFlags_CharsUppercase);
            //            static char buf5[64] = ""; Imgui.InputText("no blank",    buf5, 64, ImGuiInputTextFlags_CharsNoBlank);
            //            static char buf6[64] = ""; Imgui.InputText("\"imgui\" letters", buf6, 64, ImGuiInputTextFlags_CallbackCharFilter, TextFilters::FilterImGuiLetters);
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Text Input/Password input");
            //        if (Imgui.TreeNode("Password Input"))
            //        {
            //            static char password[64] = "password123";
            //            Imgui.InputText("password", password, IM_ARRAYSIZE(password), ImGuiInputTextFlags_Password);
            //            Imgui.SameLine(); HelpMarker("Display all characters as '*'.\nDisable clipboard cut and copy.\nDisable logging.\n");
            //            Imgui.InputTextWithHint("password (w/ hint)", "<password>", password, IM_ARRAYSIZE(password), ImGuiInputTextFlags_Password);
            //            Imgui.InputText("password (clear)", password, IM_ARRAYSIZE(password));
            //            Imgui.TreePop();
            //        }
            //
            //        if (Imgui.TreeNode("Completion, History, Edit Callbacks"))
            //        {
            //            struct Funcs
            //            {
            //                static int MyCallback(ImGuiInputTextCallbackData* data)
            //                {
            //                    if (data->EventFlag == ImGuiInputTextFlags_CallbackCompletion)
            //                    {
            //                        data->InsertChars(data->CursorPos, "..");
            //                    }
            //                    else if (data->EventFlag == ImGuiInputTextFlags_CallbackHistory)
            //                    {
            //                        if (data->EventKey == ImGuiKey_UpArrow)
            //                        {
            //                            data->DeleteChars(0, data->BufTextLen);
            //                            data->InsertChars(0, "Pressed Up!");
            //                            data->SelectAll();
            //                        }
            //                        else if (data->EventKey == ImGuiKey_DownArrow)
            //                        {
            //                            data->DeleteChars(0, data->BufTextLen);
            //                            data->InsertChars(0, "Pressed Down!");
            //                            data->SelectAll();
            //                        }
            //                    }
            //                    else if (data->EventFlag == ImGuiInputTextFlags_CallbackEdit)
            //                    {
            //                        // Toggle casing of first character
            //                        char c = data->Buf[0];
            //                        if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) data->Buf[0] ^= 32;
            //                        data->BufDirty = true;
            //
            //                        // Increment a counter
            //                        int* p_int = (int*)data->UserData;
            //                        *p_int = *p_int + 1;
            //                    }
            //                    return 0;
            //                }
            //            };
            //            static char buf1[64];
            //            Imgui.InputText("Completion", buf1, 64, ImGuiInputTextFlags_CallbackCompletion, Funcs::MyCallback);
            //            Imgui.SameLine(); HelpMarker("Here we append \"..\" each time Tab is pressed. See 'Examples>Console' for a more meaningful demonstration of using this callback.");
            //
            //            static char buf2[64];
            //            Imgui.InputText("History", buf2, 64, ImGuiInputTextFlags_CallbackHistory, Funcs::MyCallback);
            //            Imgui.SameLine(); HelpMarker("Here we replace and select text each time Up/Down are pressed. See 'Examples>Console' for a more meaningful demonstration of using this callback.");
            //
            //            static char buf3[64];
            //            static int edit_count = 0;
            //            Imgui.InputText("Edit", buf3, 64, ImGuiInputTextFlags_CallbackEdit, Funcs::MyCallback, (void*)&edit_count);
            //            Imgui.SameLine(); HelpMarker("Here we toggle the casing of the first character on every edit + count edits.");
            //            Imgui.SameLine(); Imgui.Text("(%d)", edit_count);
            //
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Text Input/Resize Callback");
            //        if (Imgui.TreeNode("Resize Callback"))
            //        {
            //            // To wire InputText() with std::string or any other custom string type,
            //            // you can use the ImGuiInputTextFlags_CallbackResize flag + create a custom Imgui.InputText() wrapper
            //            // using your preferred type. See misc/cpp/imgui_stdlib.h for an implementation of this using std::string.
            //            HelpMarker(
            //                "Using ImGuiInputTextFlags_CallbackResize to wire your custom string type to InputText().\n\n"
            //                "See misc/cpp/imgui_stdlib.h for an implementation of this for std::string.");
            //            struct Funcs
            //            {
            //                static int MyResizeCallback(ImGuiInputTextCallbackData* data)
            //                {
            //                    if (data->EventFlag == ImGuiInputTextFlags_CallbackResize)
            //                    {
            //                        ImVector<char>* my_str = (ImVector<char>*)data->UserData;
            //                        IM_ASSERT(my_str->begin() == data->Buf);
            //                        my_str->resize(data->BufSize); // NB: On resizing calls, generally data->BufSize == data->BufTextLen + 1
            //                        data->Buf = my_str->begin();
            //                    }
            //                    return 0;
            //                }
            //
            //                // Note: Because Imgui. is a namespace you would typically add your own function into the namespace.
            //                // For example, you code may declare a function 'Imgui.InputText(const char* label, MyString* my_str)'
            //                static bool MyInputTextMultiline(const char* label, ImVector<char>* my_str, const ImVec2& size = ImVec2(0, 0), ImGuiInputTextFlags flags = 0)
            //                {
            //                    IM_ASSERT((flags & ImGuiInputTextFlags_CallbackResize) == 0);
            //                    return Imgui.InputTextMultiline(label, my_str->begin(), (size_t)my_str->size(), size, flags | ImGuiInputTextFlags_CallbackResize, Funcs::MyResizeCallback, (void*)my_str);
            //                }
            //            };
            //
            //            // For this demo we are using ImVector as a string container.
            //            // Note that because we need to store a terminating zero character, our size/capacity are 1 more
            //            // than usually reported by a typical string class.
            //            static ImVector<char> my_str;
            //            if (my_str.empty())
            //                my_str.push_back(0);
            //            Funcs::MyInputTextMultiline("##MyStr", &my_str, ImVec2(-FLT_MIN, Imgui.GetTextLineHeight() * 16));
            //            Imgui.Text("Data: %p\nSize: %d\nCapacity: %d", (void*)my_str.begin(), my_str.size(), my_str.capacity());
            //            Imgui.TreePop();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    // Tabs
            //    IMGUI_DEMO_MARKER("Widgets/Tabs");
            //    if (Imgui.TreeNode("Tabs"))
            //    {
            //        IMGUI_DEMO_MARKER("Widgets/Tabs/Basic");
            //        if (Imgui.TreeNode("Basic"))
            //        {
            //            ImGuiTabBarFlags tab_bar_flags = ImGuiTabBarFlags_None;
            //            if (Imgui.BeginTabBar("MyTabBar", tab_bar_flags))
            //            {
            //                if (Imgui.BeginTabItem("Avocado"))
            //                {
            //                    Imgui.Text("This is the Avocado tab!\nblah blah blah blah blah");
            //                    Imgui.EndTabItem();
            //                }
            //                if (Imgui.BeginTabItem("Broccoli"))
            //                {
            //                    Imgui.Text("This is the Broccoli tab!\nblah blah blah blah blah");
            //                    Imgui.EndTabItem();
            //                }
            //                if (Imgui.BeginTabItem("Cucumber"))
            //                {
            //                    Imgui.Text("This is the Cucumber tab!\nblah blah blah blah blah");
            //                    Imgui.EndTabItem();
            //                }
            //                Imgui.EndTabBar();
            //            }
            //            Imgui.Separator();
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Tabs/Advanced & Close Button");
            //        if (Imgui.TreeNode("Advanced & Close Button"))
            //        {
            //            // Expose a couple of the available flags. In most cases you may just call BeginTabBar() with no flags (0).
            //            static ImGuiTabBarFlags tab_bar_flags = ImGuiTabBarFlags_Reorderable;
            //            Imgui.CheckboxFlags("ImGuiTabBarFlags_Reorderable", &tab_bar_flags, ImGuiTabBarFlags_Reorderable);
            //            Imgui.CheckboxFlags("ImGuiTabBarFlags_AutoSelectNewTabs", &tab_bar_flags, ImGuiTabBarFlags_AutoSelectNewTabs);
            //            Imgui.CheckboxFlags("ImGuiTabBarFlags_TabListPopupButton", &tab_bar_flags, ImGuiTabBarFlags_TabListPopupButton);
            //            Imgui.CheckboxFlags("ImGuiTabBarFlags_NoCloseWithMiddleMouseButton", &tab_bar_flags, ImGuiTabBarFlags_NoCloseWithMiddleMouseButton);
            //            if ((tab_bar_flags & ImGuiTabBarFlags_FittingPolicyMask_) == 0)
            //                tab_bar_flags |= ImGuiTabBarFlags_FittingPolicyDefault_;
            //            if (Imgui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyResizeDown", &tab_bar_flags, ImGuiTabBarFlags_FittingPolicyResizeDown))
            //                tab_bar_flags &= ~(ImGuiTabBarFlags_FittingPolicyMask_ ^ ImGuiTabBarFlags_FittingPolicyResizeDown);
            //            if (Imgui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyScroll", &tab_bar_flags, ImGuiTabBarFlags_FittingPolicyScroll))
            //                tab_bar_flags &= ~(ImGuiTabBarFlags_FittingPolicyMask_ ^ ImGuiTabBarFlags_FittingPolicyScroll);
            //
            //            // Tab Bar
            //            const char* names[4] = { "Artichoke", "Beetroot", "Celery", "Daikon" };
            //            static bool opened[4] = { true, true, true, true }; // Persistent user state
            //            for (int n = 0; n < IM_ARRAYSIZE(opened); n++)
            //            {
            //                if (n > 0) { Imgui.SameLine(); }
            //                Imgui.Checkbox(names[n], &opened[n]);
            //            }
            //
            //            // Passing a bool* to BeginTabItem() is similar to passing one to Begin():
            //            // the underlying bool will be set to false when the tab is closed.
            //            if (Imgui.BeginTabBar("MyTabBar", tab_bar_flags))
            //            {
            //                for (int n = 0; n < IM_ARRAYSIZE(opened); n++)
            //                    if (opened[n] && Imgui.BeginTabItem(names[n], &opened[n], ImGuiTabItemFlags_None))
            //                    {
            //                        Imgui.Text("This is the %s tab!", names[n]);
            //                        if (n & 1)
            //                            Imgui.Text("I am an odd tab.");
            //                        Imgui.EndTabItem();
            //                    }
            //                Imgui.EndTabBar();
            //            }
            //            Imgui.Separator();
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Tabs/TabItemButton & Leading-Trailing flags");
            //        if (Imgui.TreeNode("TabItemButton & Leading/Trailing flags"))
            //        {
            //            static ImVector<int> active_tabs;
            //            static int next_tab_id = 0;
            //            if (next_tab_id == 0) // Initialize with some default tabs
            //                for (int i = 0; i < 3; i++)
            //                    active_tabs.push_back(next_tab_id++);
            //
            //            // TabItemButton() and Leading/Trailing flags are distinct features which we will demo together.
            //            // (It is possible to submit regular tabs with Leading/Trailing flags, or TabItemButton tabs without Leading/Trailing flags...
            //            // but they tend to make more sense together)
            //            static bool show_leading_button = true;
            //            static bool show_trailing_button = true;
            //            Imgui.Checkbox("Show Leading TabItemButton()", &show_leading_button);
            //            Imgui.Checkbox("Show Trailing TabItemButton()", &show_trailing_button);
            //
            //            // Expose some other flags which are useful to showcase how they interact with Leading/Trailing tabs
            //            static ImGuiTabBarFlags tab_bar_flags = ImGuiTabBarFlags_AutoSelectNewTabs | ImGuiTabBarFlags_Reorderable | ImGuiTabBarFlags_FittingPolicyResizeDown;
            //            Imgui.CheckboxFlags("ImGuiTabBarFlags_TabListPopupButton", &tab_bar_flags, ImGuiTabBarFlags_TabListPopupButton);
            //            if (Imgui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyResizeDown", &tab_bar_flags, ImGuiTabBarFlags_FittingPolicyResizeDown))
            //                tab_bar_flags &= ~(ImGuiTabBarFlags_FittingPolicyMask_ ^ ImGuiTabBarFlags_FittingPolicyResizeDown);
            //            if (Imgui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyScroll", &tab_bar_flags, ImGuiTabBarFlags_FittingPolicyScroll))
            //                tab_bar_flags &= ~(ImGuiTabBarFlags_FittingPolicyMask_ ^ ImGuiTabBarFlags_FittingPolicyScroll);
            //
            //            if (Imgui.BeginTabBar("MyTabBar", tab_bar_flags))
            //            {
            //                // Demo a Leading TabItemButton(): click the "?" button to open a menu
            //                if (show_leading_button)
            //                    if (Imgui.TabItemButton("?", ImGuiTabItemFlags_Leading | ImGuiTabItemFlags_NoTooltip))
            //                        Imgui.OpenPopup("MyHelpMenu");
            //                if (Imgui.BeginPopup("MyHelpMenu"))
            //                {
            //                    Imgui.Selectable("Hello!");
            //                    Imgui.EndPopup();
            //                }
            //
            //                // Demo Trailing Tabs: click the "+" button to add a new tab (in your app you may want to use a font icon instead of the "+")
            //                // Note that we submit it before the regular tabs, but because of the ImGuiTabItemFlags_Trailing flag it will always appear at the end.
            //                if (show_trailing_button)
            //                    if (Imgui.TabItemButton("+", ImGuiTabItemFlags_Trailing | ImGuiTabItemFlags_NoTooltip))
            //                        active_tabs.push_back(next_tab_id++); // Add new tab
            //
            //                // Submit our regular tabs
            //                for (int n = 0; n < active_tabs.Size; )
            //                {
            //                    bool open = true;
            //                    char name[16];
            //                    snprintf(name, IM_ARRAYSIZE(name), "%04d", active_tabs[n]);
            //                    if (Imgui.BeginTabItem(name, &open, ImGuiTabItemFlags_None))
            //                    {
            //                        Imgui.Text("This is the %s tab!", name);
            //                        Imgui.EndTabItem();
            //                    }
            //
            //                    if (!open)
            //                        active_tabs.erase(active_tabs.Data + n);
            //                    else
            //                        n++;
            //                }
            //
            //                Imgui.EndTabBar();
            //            }
            //            Imgui.Separator();
            //            Imgui.TreePop();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    // Plot/Graph widgets are not very good.
            //    // Consider using a third-party library such as ImPlot: https://github.com/epezent/implot
            //    // (see others https://github.com/ocornut/imgui/wiki/Useful-Extensions)
            //    IMGUI_DEMO_MARKER("Widgets/Plotting");
            //    if (Imgui.TreeNode("Plotting"))
            //    {
            //        static bool animate = true;
            //        Imgui.Checkbox("Animate", &animate);
            //
            //        // Plot as lines and plot as histogram
            //        IMGUI_DEMO_MARKER("Widgets/Plotting/PlotLines, PlotHistogram");
            //        static float arr[] = { 0.6f, 0.1f, 1.0f, 0.5f, 0.92f, 0.1f, 0.2f };
            //        Imgui.PlotLines("Frame Times", arr, IM_ARRAYSIZE(arr));
            //        Imgui.PlotHistogram("Histogram", arr, IM_ARRAYSIZE(arr), 0, NULL, 0.0f, 1.0f, ImVec2(0, 80.0f));
            //
            //        // Fill an array of contiguous float values to plot
            //        // Tip: If your float aren't contiguous but part of a structure, you can pass a pointer to your first float
            //        // and the sizeof() of your structure in the "stride" parameter.
            //        static float values[90] = {};
            //        static int values_offset = 0;
            //        static double refresh_time = 0.0;
            //        if (!animate || refresh_time == 0.0)
            //            refresh_time = Imgui.GetTime();
            //        while (refresh_time < Imgui.GetTime()) // Create data at fixed 60 Hz rate for the demo
            //        {
            //            static float phase = 0.0f;
            //            values[values_offset] = cosf(phase);
            //            values_offset = (values_offset + 1) % IM_ARRAYSIZE(values);
            //            phase += 0.10f * values_offset;
            //            refresh_time += 1.0f / 60.0f;
            //        }
            //
            //        // Plots can display overlay texts
            //        // (in this example, we will display an average value)
            //        {
            //            float average = 0.0f;
            //            for (int n = 0; n < IM_ARRAYSIZE(values); n++)
            //                average += values[n];
            //            average /= (float)IM_ARRAYSIZE(values);
            //            char overlay[32];
            //            sprintf(overlay, "avg %f", average);
            //            Imgui.PlotLines("Lines", values, IM_ARRAYSIZE(values), values_offset, overlay, -1.0f, 1.0f, ImVec2(0, 80.0f));
            //        }
            //
            //        // Use functions to generate output
            //        // FIXME: This is rather awkward because current plot API only pass in indices.
            //        // We probably want an API passing floats and user provide sample rate/count.
            //        struct Funcs
            //        {
            //            static float Sin(void*, int i) { return sinf(i * 0.1f); }
            //            static float Saw(void*, int i) { return (i & 1) ? 1.0f : -1.0f; }
            //        };
            //        static int func_type = 0, display_count = 70;
            //        Imgui.Separator();
            //        Imgui.SetNextItemWidth(Imgui.GetFontSize() * 8);
            //        Imgui.Combo("func", &func_type, "Sin\0Saw\0");
            //        Imgui.SameLine();
            //        Imgui.SliderInt("Sample count", &display_count, 1, 400);
            //        float (*func)(void*, int) = (func_type == 0) ? Funcs::Sin : Funcs::Saw;
            //        Imgui.PlotLines("Lines", func, NULL, display_count, 0, NULL, -1.0f, 1.0f, ImVec2(0, 80));
            //        Imgui.PlotHistogram("Histogram", func, NULL, display_count, 0, NULL, -1.0f, 1.0f, ImVec2(0, 80));
            //        Imgui.Separator();
            //
            //        // Animate a simple progress bar
            //        IMGUI_DEMO_MARKER("Widgets/Plotting/ProgressBar");
            //        static float progress = 0.0f, progress_dir = 1.0f;
            //        if (animate)
            //        {
            //            progress += progress_dir * 0.4f * Imgui.GetIO().DeltaTime;
            //            if (progress >= +1.1f) { progress = +1.1f; progress_dir *= -1.0f; }
            //            if (progress <= -0.1f) { progress = -0.1f; progress_dir *= -1.0f; }
            //        }
            //
            //        // Typically we would use ImVec2(-1.0f,0.0f) or ImVec2(-FLT_MIN,0.0f) to use all available width,
            //        // or ImVec2(width,0.0f) for a specified width. ImVec2(0.0f,0.0f) uses ItemWidth.
            //        Imgui.ProgressBar(progress, ImVec2(0.0f, 0.0f));
            //        Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.x);
            //        Imgui.Text("Progress Bar");
            //
            //        float progress_saturated = IM_CLAMP(progress, 0.0f, 1.0f);
            //        char buf[32];
            //        sprintf(buf, "%d/%d", (int)(progress_saturated * 1753), 1753);
            //        Imgui.ProgressBar(progress, ImVec2(0.f, 0.f), buf);
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Color");
            //    if (Imgui.TreeNode("Color/Picker Widgets"))
            //    {
            //        static ImVec4 color = ImVec4(114.0f / 255.0f, 144.0f / 255.0f, 154.0f / 255.0f, 200.0f / 255.0f);
            //
            //        static bool alpha_preview = true;
            //        static bool alpha_half_preview = false;
            //        static bool drag_and_drop = true;
            //        static bool options_menu = true;
            //        static bool hdr = false;
            //        Imgui.Checkbox("With Alpha Preview", &alpha_preview);
            //        Imgui.Checkbox("With Half Alpha Preview", &alpha_half_preview);
            //        Imgui.Checkbox("With Drag and Drop", &drag_and_drop);
            //        Imgui.Checkbox("With Options Menu", &options_menu); Imgui.SameLine(); HelpMarker("Right-click on the individual color widget to show options.");
            //        Imgui.Checkbox("With HDR", &hdr); Imgui.SameLine(); HelpMarker("Currently all this does is to lift the 0..1 limits on dragging widgets.");
            //        ImGuiColorEditFlags misc_flags = (hdr ? ImGuiColorEditFlags_HDR : 0) | (drag_and_drop ? 0 : ImGuiColorEditFlags_NoDragDrop) | (alpha_half_preview ? ImGuiColorEditFlags_AlphaPreviewHalf : (alpha_preview ? ImGuiColorEditFlags_AlphaPreview : 0)) | (options_menu ? 0 : ImGuiColorEditFlags_NoOptions);
            //
            //        IMGUI_DEMO_MARKER("Widgets/Color/ColorEdit");
            //        Imgui.Text("Color widget:");
            //        Imgui.SameLine(); HelpMarker(
            //            "Click on the color square to open a color picker.\n"
            //            "CTRL+click on individual component to input value.\n");
            //        Imgui.ColorEdit3("MyColor##1", (float*)&color, misc_flags);
            //
            //        IMGUI_DEMO_MARKER("Widgets/Color/ColorEdit (HSV, with Alpha)");
            //        Imgui.Text("Color widget HSV with Alpha:");
            //        Imgui.ColorEdit4("MyColor##2", (float*)&color, ImGuiColorEditFlags_DisplayHSV | misc_flags);
            //
            //        IMGUI_DEMO_MARKER("Widgets/Color/ColorEdit (float display)");
            //        Imgui.Text("Color widget with Float Display:");
            //        Imgui.ColorEdit4("MyColor##2f", (float*)&color, ImGuiColorEditFlags_Float | misc_flags);
            //
            //        IMGUI_DEMO_MARKER("Widgets/Color/ColorButton (with Picker)");
            //        Imgui.Text("Color button with Picker:");
            //        Imgui.SameLine(); HelpMarker(
            //            "With the ImGuiColorEditFlags_NoInputs flag you can hide all the slider/text inputs.\n"
            //            "With the ImGuiColorEditFlags_NoLabel flag you can pass a non-empty label which will only "
            //            "be used for the tooltip and picker popup.");
            //        Imgui.ColorEdit4("MyColor##3", (float*)&color, ImGuiColorEditFlags_NoInputs | ImGuiColorEditFlags_NoLabel | misc_flags);
            //
            //        IMGUI_DEMO_MARKER("Widgets/Color/ColorButton (with custom Picker popup)");
            //        Imgui.Text("Color button with Custom Picker Popup:");
            //
            //        // Generate a default palette. The palette will persist and can be edited.
            //        static bool saved_palette_init = true;
            //        static ImVec4 saved_palette[32] = {};
            //        if (saved_palette_init)
            //        {
            //            for (int n = 0; n < IM_ARRAYSIZE(saved_palette); n++)
            //            {
            //                Imgui.ColorConvertHSVtoRGB(n / 31.0f, 0.8f, 0.8f,
            //                    saved_palette[n].x, saved_palette[n].y, saved_palette[n].z);
            //                saved_palette[n].w = 1.0f; // Alpha
            //            }
            //            saved_palette_init = false;
            //        }
            //
            //        static ImVec4 backup_color;
            //        bool open_popup = Imgui.ColorButton("MyColor##3b", color, misc_flags);
            //        Imgui.SameLine(0, Imgui.GetStyle().ItemInnerSpacing.x);
            //        open_popup |= Imgui.Button("Palette");
            //        if (open_popup)
            //        {
            //            Imgui.OpenPopup("mypicker");
            //            backup_color = color;
            //        }
            //        if (Imgui.BeginPopup("mypicker"))
            //        {
            //            Imgui.Text("MY CUSTOM COLOR PICKER WITH AN AMAZING PALETTE!");
            //            Imgui.Separator();
            //            Imgui.ColorPicker4("##picker", (float*)&color, misc_flags | ImGuiColorEditFlags_NoSidePreview | ImGuiColorEditFlags_NoSmallPreview);
            //            Imgui.SameLine();
            //
            //            Imgui.BeginGroup(); // Lock X position
            //            Imgui.Text("Current");
            //            Imgui.ColorButton("##current", color, ImGuiColorEditFlags_NoPicker | ImGuiColorEditFlags_AlphaPreviewHalf, ImVec2(60, 40));
            //            Imgui.Text("Previous");
            //            if (Imgui.ColorButton("##previous", backup_color, ImGuiColorEditFlags_NoPicker | ImGuiColorEditFlags_AlphaPreviewHalf, ImVec2(60, 40)))
            //                color = backup_color;
            //            Imgui.Separator();
            //            Imgui.Text("Palette");
            //            for (int n = 0; n < IM_ARRAYSIZE(saved_palette); n++)
            //            {
            //                Imgui.PushID(n);
            //                if ((n % 8) != 0)
            //                    Imgui.SameLine(0.0f, Imgui.GetStyle().ItemSpacing.y);
            //
            //                ImGuiColorEditFlags palette_button_flags = ImGuiColorEditFlags_NoAlpha | ImGuiColorEditFlags_NoPicker | ImGuiColorEditFlags_NoTooltip;
            //                if (Imgui.ColorButton("##palette", saved_palette[n], palette_button_flags, ImVec2(20, 20)))
            //                    color = ImVec4(saved_palette[n].x, saved_palette[n].y, saved_palette[n].z, color.w); // Preserve alpha!
            //
            //                // Allow user to drop colors into each palette entry. Note that ColorButton() is already a
            //                // drag source by default, unless specifying the ImGuiColorEditFlags_NoDragDrop flag.
            //                if (Imgui.BeginDragDropTarget())
            //                {
            //                    if (const ImGuiPayload* payload = Imgui.AcceptDragDropPayload(IMGUI_PAYLOAD_TYPE_COLOR_3F))
            //                        memcpy((float*)&saved_palette[n], payload->Data, sizeof(float) * 3);
            //                    if (const ImGuiPayload* payload = Imgui.AcceptDragDropPayload(IMGUI_PAYLOAD_TYPE_COLOR_4F))
            //                        memcpy((float*)&saved_palette[n], payload->Data, sizeof(float) * 4);
            //                    Imgui.EndDragDropTarget();
            //                }
            //
            //                Imgui.PopID();
            //            }
            //            Imgui.EndGroup();
            //            Imgui.EndPopup();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Color/ColorButton (simple)");
            //        Imgui.Text("Color button only:");
            //        static bool no_border = false;
            //        Imgui.Checkbox("ImGuiColorEditFlags_NoBorder", &no_border);
            //        Imgui.ColorButton("MyColor##3c", *(ImVec4*)&color, misc_flags | (no_border ? ImGuiColorEditFlags_NoBorder : 0), ImVec2(80, 80));
            //
            //        IMGUI_DEMO_MARKER("Widgets/Color/ColorPicker");
            //        Imgui.Text("Color picker:");
            //        static bool alpha = true;
            //        static bool alpha_bar = true;
            //        static bool side_preview = true;
            //        static bool ref_color = false;
            //        static ImVec4 ref_color_v(1.0f, 0.0f, 1.0f, 0.5f);
            //        static int display_mode = 0;
            //        static int picker_mode = 0;
            //        Imgui.Checkbox("With Alpha", &alpha);
            //        Imgui.Checkbox("With Alpha Bar", &alpha_bar);
            //        Imgui.Checkbox("With Side Preview", &side_preview);
            //        if (side_preview)
            //        {
            //            Imgui.SameLine();
            //            Imgui.Checkbox("With Ref Color", &ref_color);
            //            if (ref_color)
            //            {
            //                Imgui.SameLine();
            //                Imgui.ColorEdit4("##RefColor", &ref_color_v.x, ImGuiColorEditFlags_NoInputs | misc_flags);
            //            }
            //        }
            //        Imgui.Combo("Display Mode", &display_mode, "Auto/Current\0None\0RGB Only\0HSV Only\0Hex Only\0");
            //        Imgui.SameLine(); HelpMarker(
            //            "ColorEdit defaults to displaying RGB inputs if you don't specify a display mode, "
            //            "but the user can change it with a right-click on those inputs.\n\nColorPicker defaults to displaying RGB+HSV+Hex "
            //            "if you don't specify a display mode.\n\nYou can change the defaults using SetColorEditOptions().");
            //        Imgui.SameLine(); HelpMarker("When not specified explicitly (Auto/Current mode), user can right-click the picker to change mode.");
            //        ImGuiColorEditFlags flags = misc_flags;
            //        if (!alpha)            flags |= ImGuiColorEditFlags_NoAlpha;        // This is by default if you call ColorPicker3() instead of ColorPicker4()
            //        if (alpha_bar)         flags |= ImGuiColorEditFlags_AlphaBar;
            //        if (!side_preview)     flags |= ImGuiColorEditFlags_NoSidePreview;
            //        if (picker_mode == 1)  flags |= ImGuiColorEditFlags_PickerHueBar;
            //        if (picker_mode == 2)  flags |= ImGuiColorEditFlags_PickerHueWheel;
            //        if (display_mode == 1) flags |= ImGuiColorEditFlags_NoInputs;       // Disable all RGB/HSV/Hex displays
            //        if (display_mode == 2) flags |= ImGuiColorEditFlags_DisplayRGB;     // Override display mode
            //        if (display_mode == 3) flags |= ImGuiColorEditFlags_DisplayHSV;
            //        if (display_mode == 4) flags |= ImGuiColorEditFlags_DisplayHex;
            //        Imgui.ColorPicker4("MyColor##4", (float*)&color, flags, ref_color ? &ref_color_v.x : NULL);
            //
            //        Imgui.Text("Set defaults in code:");
            //        Imgui.SameLine(); HelpMarker(
            //            "SetColorEditOptions() is designed to allow you to set boot-time default.\n"
            //            "We don't have Push/Pop functions because you can force options on a per-widget basis if needed,"
            //            "and the user can change non-forced ones with the options menu.\nWe don't have a getter to avoid"
            //            "encouraging you to persistently save values that aren't forward-compatible.");
            //        if (Imgui.Button("Default: Uint8 + HSV + Hue Bar"))
            //            Imgui.SetColorEditOptions(ImGuiColorEditFlags_Uint8 | ImGuiColorEditFlags_DisplayHSV | ImGuiColorEditFlags_PickerHueBar);
            //        if (Imgui.Button("Default: Float + HDR + Hue Wheel"))
            //            Imgui.SetColorEditOptions(ImGuiColorEditFlags_Float | ImGuiColorEditFlags_HDR | ImGuiColorEditFlags_PickerHueWheel);
            //
            //        // Always both a small version of both types of pickers (to make it more visible in the demo to people who are skimming quickly through it)
            //        Imgui.Text("Both types:");
            //        float w = (Imgui.GetContentRegionAvail().x - Imgui.GetStyle().ItemSpacing.y) * 0.40f;
            //        Imgui.SetNextItemWidth(w);
            //        Imgui.ColorPicker3("##MyColor##5", (float*)&color, ImGuiColorEditFlags_PickerHueBar | ImGuiColorEditFlags_NoSidePreview | ImGuiColorEditFlags_NoInputs | ImGuiColorEditFlags_NoAlpha);
            //        Imgui.SameLine();
            //        Imgui.SetNextItemWidth(w);
            //        Imgui.ColorPicker3("##MyColor##6", (float*)&color, ImGuiColorEditFlags_PickerHueWheel | ImGuiColorEditFlags_NoSidePreview | ImGuiColorEditFlags_NoInputs | ImGuiColorEditFlags_NoAlpha);
            //
            //        // HSV encoded support (to avoid RGB<>HSV round trips and singularities when S==0 or V==0)
            //        static ImVec4 color_hsv(0.23f, 1.0f, 1.0f, 1.0f); // Stored as HSV!
            //        Imgui.Spacing();
            //        Imgui.Text("HSV encoded colors");
            //        Imgui.SameLine(); HelpMarker(
            //            "By default, colors are given to ColorEdit and ColorPicker in RGB, but ImGuiColorEditFlags_InputHSV"
            //            "allows you to store colors as HSV and pass them to ColorEdit and ColorPicker as HSV. This comes with the"
            //            "added benefit that you can manipulate hue values with the picker even when saturation or value are zero.");
            //        Imgui.Text("Color widget with InputHSV:");
            //        Imgui.ColorEdit4("HSV shown as RGB##1", (float*)&color_hsv, ImGuiColorEditFlags_DisplayRGB | ImGuiColorEditFlags_InputHSV | ImGuiColorEditFlags_Float);
            //        Imgui.ColorEdit4("HSV shown as HSV##1", (float*)&color_hsv, ImGuiColorEditFlags_DisplayHSV | ImGuiColorEditFlags_InputHSV | ImGuiColorEditFlags_Float);
            //        Imgui.DragFloat4("Raw HSV values", (float*)&color_hsv, 0.01f, 0.0f, 1.0f);
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Drag and Slider Flags");
            //    if (Imgui.TreeNode("Drag/Slider Flags"))
            //    {
            //        // Demonstrate using advanced flags for DragXXX and SliderXXX functions. Note that the flags are the same!
            //        static ImGuiSliderFlags flags = ImGuiSliderFlags_None;
            //        Imgui.CheckboxFlags("ImGuiSliderFlags_AlwaysClamp", &flags, ImGuiSliderFlags_AlwaysClamp);
            //        Imgui.SameLine(); HelpMarker("Always clamp value to min/max bounds (if any) when input manually with CTRL+Click.");
            //        Imgui.CheckboxFlags("ImGuiSliderFlags_Logarithmic", &flags, ImGuiSliderFlags_Logarithmic);
            //        Imgui.SameLine(); HelpMarker("Enable logarithmic editing (more precision for small values).");
            //        Imgui.CheckboxFlags("ImGuiSliderFlags_NoRoundToFormat", &flags, ImGuiSliderFlags_NoRoundToFormat);
            //        Imgui.SameLine(); HelpMarker("Disable rounding underlying value to match precision of the format string (e.g. %.3f values are rounded to those 3 digits).");
            //        Imgui.CheckboxFlags("ImGuiSliderFlags_NoInput", &flags, ImGuiSliderFlags_NoInput);
            //        Imgui.SameLine(); HelpMarker("Disable CTRL+Click or Enter key allowing to input text directly into the widget.");
            //
            //        // Drags
            //        static float drag_f = 0.5f;
            //        static int drag_i = 50;
            //        Imgui.Text("Underlying float value: %f", drag_f);
            //        Imgui.DragFloat("DragFloat (0 -> 1)", &drag_f, 0.005f, 0.0f, 1.0f, "%.3f", flags);
            //        Imgui.DragFloat("DragFloat (0 -> +inf)", &drag_f, 0.005f, 0.0f, FLT_MAX, "%.3f", flags);
            //        Imgui.DragFloat("DragFloat (-inf -> 1)", &drag_f, 0.005f, -FLT_MAX, 1.0f, "%.3f", flags);
            //        Imgui.DragFloat("DragFloat (-inf -> +inf)", &drag_f, 0.005f, -FLT_MAX, +FLT_MAX, "%.3f", flags);
            //        Imgui.DragInt("DragInt (0 -> 100)", &drag_i, 0.5f, 0, 100, "%d", flags);
            //
            //        // Sliders
            //        static float slider_f = 0.5f;
            //        static int slider_i = 50;
            //        Imgui.Text("Underlying float value: %f", slider_f);
            //        Imgui.SliderFloat("SliderFloat (0 -> 1)", &slider_f, 0.0f, 1.0f, "%.3f", flags);
            //        Imgui.SliderInt("SliderInt (0 -> 100)", &slider_i, 0, 100, "%d", flags);
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Range Widgets");
            //    if (Imgui.TreeNode("Range Widgets"))
            //    {
            //        static float begin = 10, end = 90;
            //        static int begin_i = 100, end_i = 1000;
            //        Imgui.DragFloatRange2("range float", &begin, &end, 0.25f, 0.0f, 100.0f, "Min: %.1f %%", "Max: %.1f %%", ImGuiSliderFlags_AlwaysClamp);
            //        Imgui.DragIntRange2("range int", &begin_i, &end_i, 5, 0, 1000, "Min: %d units", "Max: %d units");
            //        Imgui.DragIntRange2("range int (no bounds)", &begin_i, &end_i, 5, 0, 0, "Min: %d units", "Max: %d units");
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Data Types");
            //    if (Imgui.TreeNode("Data Types"))
            //    {
            //        // DragScalar/InputScalar/SliderScalar functions allow various data types
            //        // - signed/unsigned
            //        // - 8/16/32/64-bits
            //        // - integer/float/double
            //        // To avoid polluting the public API with all possible combinations, we use the ImGuiDataType enum
            //        // to pass the type, and passing all arguments by pointer.
            //        // This is the reason the test code below creates local variables to hold "zero" "one" etc. for each type.
            //        // In practice, if you frequently use a given type that is not covered by the normal API entry points,
            //        // you can wrap it yourself inside a 1 line function which can take typed argument as value instead of void*,
            //        // and then pass their address to the generic function. For example:
            //        //   bool MySliderU64(const char *label, u64* value, u64 min = 0, u64 max = 0, const char* format = "%lld")
            //        //   {
            //        //      return SliderScalar(label, ImGuiDataType_U64, value, &min, &max, format);
            //        //   }
            //
            //        // Setup limits (as helper variables so we can take their address, as explained above)
            //        // Note: SliderScalar() functions have a maximum usable range of half the natural type maximum, hence the /2.
            //        #ifndef LLONG_MIN
            //        ImS64 LLONG_MIN = -9223372036854775807LL - 1;
            //        ImS64 LLONG_MAX = 9223372036854775807LL;
            //        ImU64 ULLONG_MAX = (2ULL * 9223372036854775807LL + 1);
            //        #endif
            //        const char    s8_zero  = 0,   s8_one  = 1,   s8_fifty  = 50, s8_min  = -128,        s8_max = 127;
            //        const ImU8    u8_zero  = 0,   u8_one  = 1,   u8_fifty  = 50, u8_min  = 0,           u8_max = 255;
            //        const short   s16_zero = 0,   s16_one = 1,   s16_fifty = 50, s16_min = -32768,      s16_max = 32767;
            //        const ImU16   u16_zero = 0,   u16_one = 1,   u16_fifty = 50, u16_min = 0,           u16_max = 65535;
            //        const ImS32   s32_zero = 0,   s32_one = 1,   s32_fifty = 50, s32_min = INT_MIN/2,   s32_max = INT_MAX/2,    s32_hi_a = INT_MAX/2 - 100,    s32_hi_b = INT_MAX/2;
            //        const ImU32   u32_zero = 0,   u32_one = 1,   u32_fifty = 50, u32_min = 0,           u32_max = UINT_MAX/2,   u32_hi_a = UINT_MAX/2 - 100,   u32_hi_b = UINT_MAX/2;
            //        const ImS64   s64_zero = 0,   s64_one = 1,   s64_fifty = 50, s64_min = LLONG_MIN/2, s64_max = LLONG_MAX/2,  s64_hi_a = LLONG_MAX/2 - 100,  s64_hi_b = LLONG_MAX/2;
            //        const ImU64   u64_zero = 0,   u64_one = 1,   u64_fifty = 50, u64_min = 0,           u64_max = ULLONG_MAX/2, u64_hi_a = ULLONG_MAX/2 - 100, u64_hi_b = ULLONG_MAX/2;
            //        const float   f32_zero = 0.f, f32_one = 1.f, f32_lo_a = -10000000000.0f, f32_hi_a = +10000000000.0f;
            //        const double  f64_zero = 0.,  f64_one = 1.,  f64_lo_a = -1000000000000000.0, f64_hi_a = +1000000000000000.0;
            //
            //        // State
            //        static char   s8_v  = 127;
            //        static ImU8   u8_v  = 255;
            //        static short  s16_v = 32767;
            //        static ImU16  u16_v = 65535;
            //        static ImS32  s32_v = -1;
            //        static ImU32  u32_v = (ImU32)-1;
            //        static ImS64  s64_v = -1;
            //        static ImU64  u64_v = (ImU64)-1;
            //        static float  f32_v = 0.123f;
            //        static double f64_v = 90000.01234567890123456789;
            //
            //        const float drag_speed = 0.2f;
            //        static bool drag_clamp = false;
            //        IMGUI_DEMO_MARKER("Widgets/Data Types/Drags");
            //        Imgui.Text("Drags:");
            //        Imgui.Checkbox("Clamp integers to 0..50", &drag_clamp);
            //        Imgui.SameLine(); HelpMarker(
            //            "As with every widget in dear imgui, we never modify values unless there is a user interaction.\n"
            //            "You can override the clamping limits by using CTRL+Click to input a value.");
            //        Imgui.DragScalar("drag s8",        ImGuiDataType_S8,     &s8_v,  drag_speed, drag_clamp ? &s8_zero  : NULL, drag_clamp ? &s8_fifty  : NULL);
            //        Imgui.DragScalar("drag u8",        ImGuiDataType_U8,     &u8_v,  drag_speed, drag_clamp ? &u8_zero  : NULL, drag_clamp ? &u8_fifty  : NULL, "%u ms");
            //        Imgui.DragScalar("drag s16",       ImGuiDataType_S16,    &s16_v, drag_speed, drag_clamp ? &s16_zero : NULL, drag_clamp ? &s16_fifty : NULL);
            //        Imgui.DragScalar("drag u16",       ImGuiDataType_U16,    &u16_v, drag_speed, drag_clamp ? &u16_zero : NULL, drag_clamp ? &u16_fifty : NULL, "%u ms");
            //        Imgui.DragScalar("drag s32",       ImGuiDataType_S32,    &s32_v, drag_speed, drag_clamp ? &s32_zero : NULL, drag_clamp ? &s32_fifty : NULL);
            //        Imgui.DragScalar("drag s32 hex",   ImGuiDataType_S32,    &s32_v, drag_speed, drag_clamp ? &s32_zero : NULL, drag_clamp ? &s32_fifty : NULL, "0x%08X");
            //        Imgui.DragScalar("drag u32",       ImGuiDataType_U32,    &u32_v, drag_speed, drag_clamp ? &u32_zero : NULL, drag_clamp ? &u32_fifty : NULL, "%u ms");
            //        Imgui.DragScalar("drag s64",       ImGuiDataType_S64,    &s64_v, drag_speed, drag_clamp ? &s64_zero : NULL, drag_clamp ? &s64_fifty : NULL);
            //        Imgui.DragScalar("drag u64",       ImGuiDataType_U64,    &u64_v, drag_speed, drag_clamp ? &u64_zero : NULL, drag_clamp ? &u64_fifty : NULL);
            //        Imgui.DragScalar("drag float",     ImGuiDataType_Float,  &f32_v, 0.005f,  &f32_zero, &f32_one, "%f");
            //        Imgui.DragScalar("drag float log", ImGuiDataType_Float,  &f32_v, 0.005f,  &f32_zero, &f32_one, "%f", ImGuiSliderFlags_Logarithmic);
            //        Imgui.DragScalar("drag double",    ImGuiDataType_Double, &f64_v, 0.0005f, &f64_zero, NULL,     "%.10f grams");
            //        Imgui.DragScalar("drag double log",ImGuiDataType_Double, &f64_v, 0.0005f, &f64_zero, &f64_one, "0 < %.10f < 1", ImGuiSliderFlags_Logarithmic);
            //
            //        IMGUI_DEMO_MARKER("Widgets/Data Types/Sliders");
            //        Imgui.Text("Sliders");
            //        Imgui.SliderScalar("slider s8 full",       ImGuiDataType_S8,     &s8_v,  &s8_min,   &s8_max,   "%d");
            //        Imgui.SliderScalar("slider u8 full",       ImGuiDataType_U8,     &u8_v,  &u8_min,   &u8_max,   "%u");
            //        Imgui.SliderScalar("slider s16 full",      ImGuiDataType_S16,    &s16_v, &s16_min,  &s16_max,  "%d");
            //        Imgui.SliderScalar("slider u16 full",      ImGuiDataType_U16,    &u16_v, &u16_min,  &u16_max,  "%u");
            //        Imgui.SliderScalar("slider s32 low",       ImGuiDataType_S32,    &s32_v, &s32_zero, &s32_fifty,"%d");
            //        Imgui.SliderScalar("slider s32 high",      ImGuiDataType_S32,    &s32_v, &s32_hi_a, &s32_hi_b, "%d");
            //        Imgui.SliderScalar("slider s32 full",      ImGuiDataType_S32,    &s32_v, &s32_min,  &s32_max,  "%d");
            //        Imgui.SliderScalar("slider s32 hex",       ImGuiDataType_S32,    &s32_v, &s32_zero, &s32_fifty, "0x%04X");
            //        Imgui.SliderScalar("slider u32 low",       ImGuiDataType_U32,    &u32_v, &u32_zero, &u32_fifty,"%u");
            //        Imgui.SliderScalar("slider u32 high",      ImGuiDataType_U32,    &u32_v, &u32_hi_a, &u32_hi_b, "%u");
            //        Imgui.SliderScalar("slider u32 full",      ImGuiDataType_U32,    &u32_v, &u32_min,  &u32_max,  "%u");
            //        Imgui.SliderScalar("slider s64 low",       ImGuiDataType_S64,    &s64_v, &s64_zero, &s64_fifty,"%" IM_PRId64);
            //        Imgui.SliderScalar("slider s64 high",      ImGuiDataType_S64,    &s64_v, &s64_hi_a, &s64_hi_b, "%" IM_PRId64);
            //        Imgui.SliderScalar("slider s64 full",      ImGuiDataType_S64,    &s64_v, &s64_min,  &s64_max,  "%" IM_PRId64);
            //        Imgui.SliderScalar("slider u64 low",       ImGuiDataType_U64,    &u64_v, &u64_zero, &u64_fifty,"%" IM_PRIu64 " ms");
            //        Imgui.SliderScalar("slider u64 high",      ImGuiDataType_U64,    &u64_v, &u64_hi_a, &u64_hi_b, "%" IM_PRIu64 " ms");
            //        Imgui.SliderScalar("slider u64 full",      ImGuiDataType_U64,    &u64_v, &u64_min,  &u64_max,  "%" IM_PRIu64 " ms");
            //        Imgui.SliderScalar("slider float low",     ImGuiDataType_Float,  &f32_v, &f32_zero, &f32_one);
            //        Imgui.SliderScalar("slider float low log", ImGuiDataType_Float,  &f32_v, &f32_zero, &f32_one,  "%.10f", ImGuiSliderFlags_Logarithmic);
            //        Imgui.SliderScalar("slider float high",    ImGuiDataType_Float,  &f32_v, &f32_lo_a, &f32_hi_a, "%e");
            //        Imgui.SliderScalar("slider double low",    ImGuiDataType_Double, &f64_v, &f64_zero, &f64_one,  "%.10f grams");
            //        Imgui.SliderScalar("slider double low log",ImGuiDataType_Double, &f64_v, &f64_zero, &f64_one,  "%.10f", ImGuiSliderFlags_Logarithmic);
            //        Imgui.SliderScalar("slider double high",   ImGuiDataType_Double, &f64_v, &f64_lo_a, &f64_hi_a, "%e grams");
            //
            //        Imgui.Text("Sliders (reverse)");
            //        Imgui.SliderScalar("slider s8 reverse",    ImGuiDataType_S8,   &s8_v,  &s8_max,    &s8_min,   "%d");
            //        Imgui.SliderScalar("slider u8 reverse",    ImGuiDataType_U8,   &u8_v,  &u8_max,    &u8_min,   "%u");
            //        Imgui.SliderScalar("slider s32 reverse",   ImGuiDataType_S32,  &s32_v, &s32_fifty, &s32_zero, "%d");
            //        Imgui.SliderScalar("slider u32 reverse",   ImGuiDataType_U32,  &u32_v, &u32_fifty, &u32_zero, "%u");
            //        Imgui.SliderScalar("slider s64 reverse",   ImGuiDataType_S64,  &s64_v, &s64_fifty, &s64_zero, "%" IM_PRId64);
            //        Imgui.SliderScalar("slider u64 reverse",   ImGuiDataType_U64,  &u64_v, &u64_fifty, &u64_zero, "%" IM_PRIu64 " ms");
            //
            //        IMGUI_DEMO_MARKER("Widgets/Data Types/Inputs");
            //        static bool inputs_step = true;
            //        Imgui.Text("Inputs");
            //        Imgui.Checkbox("Show step buttons", &inputs_step);
            //        Imgui.InputScalar("input s8",      ImGuiDataType_S8,     &s8_v,  inputs_step ? &s8_one  : NULL, NULL, "%d");
            //        Imgui.InputScalar("input u8",      ImGuiDataType_U8,     &u8_v,  inputs_step ? &u8_one  : NULL, NULL, "%u");
            //        Imgui.InputScalar("input s16",     ImGuiDataType_S16,    &s16_v, inputs_step ? &s16_one : NULL, NULL, "%d");
            //        Imgui.InputScalar("input u16",     ImGuiDataType_U16,    &u16_v, inputs_step ? &u16_one : NULL, NULL, "%u");
            //        Imgui.InputScalar("input s32",     ImGuiDataType_S32,    &s32_v, inputs_step ? &s32_one : NULL, NULL, "%d");
            //        Imgui.InputScalar("input s32 hex", ImGuiDataType_S32,    &s32_v, inputs_step ? &s32_one : NULL, NULL, "%04X");
            //        Imgui.InputScalar("input u32",     ImGuiDataType_U32,    &u32_v, inputs_step ? &u32_one : NULL, NULL, "%u");
            //        Imgui.InputScalar("input u32 hex", ImGuiDataType_U32,    &u32_v, inputs_step ? &u32_one : NULL, NULL, "%08X");
            //        Imgui.InputScalar("input s64",     ImGuiDataType_S64,    &s64_v, inputs_step ? &s64_one : NULL);
            //        Imgui.InputScalar("input u64",     ImGuiDataType_U64,    &u64_v, inputs_step ? &u64_one : NULL);
            //        Imgui.InputScalar("input float",   ImGuiDataType_Float,  &f32_v, inputs_step ? &f32_one : NULL);
            //        Imgui.InputScalar("input double",  ImGuiDataType_Double, &f64_v, inputs_step ? &f64_one : NULL);
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Multi-component Widgets");
            //    if (Imgui.TreeNode("Multi-component Widgets"))
            //    {
            //        static float vec4f[4] = { 0.10f, 0.20f, 0.30f, 0.44f };
            //        static int vec4i[4] = { 1, 5, 100, 255 };
            //
            //        Imgui.InputFloat2("input float2", vec4f);
            //        Imgui.DragFloat2("drag float2", vec4f, 0.01f, 0.0f, 1.0f);
            //        Imgui.SliderFloat2("slider float2", vec4f, 0.0f, 1.0f);
            //        Imgui.InputInt2("input int2", vec4i);
            //        Imgui.DragInt2("drag int2", vec4i, 1, 0, 255);
            //        Imgui.SliderInt2("slider int2", vec4i, 0, 255);
            //        Imgui.Spacing();
            //
            //        Imgui.InputFloat3("input float3", vec4f);
            //        Imgui.DragFloat3("drag float3", vec4f, 0.01f, 0.0f, 1.0f);
            //        Imgui.SliderFloat3("slider float3", vec4f, 0.0f, 1.0f);
            //        Imgui.InputInt3("input int3", vec4i);
            //        Imgui.DragInt3("drag int3", vec4i, 1, 0, 255);
            //        Imgui.SliderInt3("slider int3", vec4i, 0, 255);
            //        Imgui.Spacing();
            //
            //        Imgui.InputFloat4("input float4", vec4f);
            //        Imgui.DragFloat4("drag float4", vec4f, 0.01f, 0.0f, 1.0f);
            //        Imgui.SliderFloat4("slider float4", vec4f, 0.0f, 1.0f);
            //        Imgui.InputInt4("input int4", vec4i);
            //        Imgui.DragInt4("drag int4", vec4i, 1, 0, 255);
            //        Imgui.SliderInt4("slider int4", vec4i, 0, 255);
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Vertical Sliders");
            //    if (Imgui.TreeNode("Vertical Sliders"))
            //    {
            //        const float spacing = 4;
            //        Imgui.PushStyleVar(ImGuiStyleVar_ItemSpacing, ImVec2(spacing, spacing));
            //
            //        static int int_value = 0;
            //        Imgui.VSliderInt("##int", ImVec2(18, 160), &int_value, 0, 5);
            //        Imgui.SameLine();
            //
            //        static float values[7] = { 0.0f, 0.60f, 0.35f, 0.9f, 0.70f, 0.20f, 0.0f };
            //        Imgui.PushID("set1");
            //        for (int i = 0; i < 7; i++)
            //        {
            //            if (i > 0) Imgui.SameLine();
            //            Imgui.PushID(i);
            //            Imgui.PushStyleColor(ImGuiCol_FrameBg, (ImVec4)ImColor::HSV(i / 7.0f, 0.5f, 0.5f));
            //            Imgui.PushStyleColor(ImGuiCol_FrameBgHovered, (ImVec4)ImColor::HSV(i / 7.0f, 0.6f, 0.5f));
            //            Imgui.PushStyleColor(ImGuiCol_FrameBgActive, (ImVec4)ImColor::HSV(i / 7.0f, 0.7f, 0.5f));
            //            Imgui.PushStyleColor(ImGuiCol_SliderGrab, (ImVec4)ImColor::HSV(i / 7.0f, 0.9f, 0.9f));
            //            Imgui.VSliderFloat("##v", ImVec2(18, 160), &values[i], 0.0f, 1.0f, "");
            //            if (Imgui.IsItemActive() || Imgui.IsItemHovered())
            //                Imgui.SetTooltip("%.3f", values[i]);
            //            Imgui.PopStyleColor(4);
            //            Imgui.PopID();
            //        }
            //        Imgui.PopID();
            //
            //        Imgui.SameLine();
            //        Imgui.PushID("set2");
            //        static float values2[4] = { 0.20f, 0.80f, 0.40f, 0.25f };
            //        const int rows = 3;
            //        const ImVec2 small_slider_size(18, (float)(int)((160.0f - (rows - 1) * spacing) / rows));
            //        for (int nx = 0; nx < 4; nx++)
            //        {
            //            if (nx > 0) Imgui.SameLine();
            //            Imgui.BeginGroup();
            //            for (int ny = 0; ny < rows; ny++)
            //            {
            //                Imgui.PushID(nx * rows + ny);
            //                Imgui.VSliderFloat("##v", small_slider_size, &values2[nx], 0.0f, 1.0f, "");
            //                if (Imgui.IsItemActive() || Imgui.IsItemHovered())
            //                    Imgui.SetTooltip("%.3f", values2[nx]);
            //                Imgui.PopID();
            //            }
            //            Imgui.EndGroup();
            //        }
            //        Imgui.PopID();
            //
            //        Imgui.SameLine();
            //        Imgui.PushID("set3");
            //        for (int i = 0; i < 4; i++)
            //        {
            //            if (i > 0) Imgui.SameLine();
            //            Imgui.PushID(i);
            //            Imgui.PushStyleVar(ImGuiStyleVar_GrabMinSize, 40);
            //            Imgui.VSliderFloat("##v", ImVec2(40, 160), &values[i], 0.0f, 1.0f, "%.2f\nsec");
            //            Imgui.PopStyleVar();
            //            Imgui.PopID();
            //        }
            //        Imgui.PopID();
            //        Imgui.PopStyleVar();
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Drag and drop");
            //    if (Imgui.TreeNode("Drag and Drop"))
            //    {
            //        IMGUI_DEMO_MARKER("Widgets/Drag and drop/Standard widgets");
            //        if (Imgui.TreeNode("Drag and drop in standard widgets"))
            //        {
            //            // ColorEdit widgets automatically act as drag source and drag target.
            //            // They are using standardized payload strings IMGUI_PAYLOAD_TYPE_COLOR_3F and IMGUI_PAYLOAD_TYPE_COLOR_4F
            //            // to allow your own widgets to use colors in their drag and drop interaction.
            //            // Also see 'Demo->Widgets->Color/Picker Widgets->Palette' demo.
            //            HelpMarker("You can drag from the color squares.");
            //            static float col1[3] = { 1.0f, 0.0f, 0.2f };
            //            static float col2[4] = { 0.4f, 0.7f, 0.0f, 0.5f };
            //            Imgui.ColorEdit3("color 1", col1);
            //            Imgui.ColorEdit4("color 2", col2);
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Drag and drop/Copy-swap items");
            //        if (Imgui.TreeNode("Drag and drop to copy/swap items"))
            //        {
            //            enum Mode
            //            {
            //                Mode_Copy,
            //                Mode_Move,
            //                Mode_Swap
            //            };
            //            static int mode = 0;
            //            if (Imgui.RadioButton("Copy", mode == Mode_Copy)) { mode = Mode_Copy; } Imgui.SameLine();
            //            if (Imgui.RadioButton("Move", mode == Mode_Move)) { mode = Mode_Move; } Imgui.SameLine();
            //            if (Imgui.RadioButton("Swap", mode == Mode_Swap)) { mode = Mode_Swap; }
            //            static const char* names[9] =
            //            {
            //                "Bobby", "Beatrice", "Betty",
            //                "Brianna", "Barry", "Bernard",
            //                "Bibi", "Blaine", "Bryn"
            //            };
            //            for (int n = 0; n < IM_ARRAYSIZE(names); n++)
            //            {
            //                Imgui.PushID(n);
            //                if ((n % 3) != 0)
            //                    Imgui.SameLine();
            //                Imgui.Button(names[n], ImVec2(60, 60));
            //
            //                // Our buttons are both drag sources and drag targets here!
            //                if (Imgui.BeginDragDropSource(ImGuiDragDropFlags_None))
            //                {
            //                    // Set payload to carry the index of our item (could be anything)
            //                    Imgui.SetDragDropPayload("DND_DEMO_CELL", &n, sizeof(int));
            //
            //                    // Display preview (could be anything, e.g. when dragging an image we could decide to display
            //                    // the filename and a small preview of the image, etc.)
            //                    if (mode == Mode_Copy) { Imgui.Text("Copy %s", names[n]); }
            //                    if (mode == Mode_Move) { Imgui.Text("Move %s", names[n]); }
            //                    if (mode == Mode_Swap) { Imgui.Text("Swap %s", names[n]); }
            //                    Imgui.EndDragDropSource();
            //                }
            //                if (Imgui.BeginDragDropTarget())
            //                {
            //                    if (const ImGuiPayload* payload = Imgui.AcceptDragDropPayload("DND_DEMO_CELL"))
            //                    {
            //                        IM_ASSERT(payload->DataSize == sizeof(int));
            //                        int payload_n = *(const int*)payload->Data;
            //                        if (mode == Mode_Copy)
            //                        {
            //                            names[n] = names[payload_n];
            //                        }
            //                        if (mode == Mode_Move)
            //                        {
            //                            names[n] = names[payload_n];
            //                            names[payload_n] = "";
            //                        }
            //                        if (mode == Mode_Swap)
            //                        {
            //                            const char* tmp = names[n];
            //                            names[n] = names[payload_n];
            //                            names[payload_n] = tmp;
            //                        }
            //                    }
            //                    Imgui.EndDragDropTarget();
            //                }
            //                Imgui.PopID();
            //            }
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Widgets/Drag and Drop/Drag to reorder items (simple)");
            //        if (Imgui.TreeNode("Drag to reorder items (simple)"))
            //        {
            //            // Simple reordering
            //            HelpMarker(
            //                "We don't use the drag and drop api at all here! "
            //                "Instead we query when the item is held but not hovered, and order items accordingly.");
            //            static const char* item_names[] = { "Item One", "Item Two", "Item Three", "Item Four", "Item Five" };
            //            for (int n = 0; n < IM_ARRAYSIZE(item_names); n++)
            //            {
            //                const char* item = item_names[n];
            //                Imgui.Selectable(item);
            //
            //                if (Imgui.IsItemActive() && !Imgui.IsItemHovered())
            //                {
            //                    int n_next = n + (Imgui.GetMouseDragDelta(0).y < 0.f ? -1 : 1);
            //                    if (n_next >= 0 && n_next < IM_ARRAYSIZE(item_names))
            //                    {
            //                        item_names[n] = item_names[n_next];
            //                        item_names[n_next] = item;
            //                        Imgui.ResetMouseDragDelta();
            //                    }
            //                }
            //            }
            //            Imgui.TreePop();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Querying Item Status (Edited,Active,Hovered etc.)");
            //    if (Imgui.TreeNode("Querying Item Status (Edited/Active/Hovered etc.)"))
            //    {
            //        // Select an item type
            //        const char* item_names[] =
            //        {
            //            "Text", "Button", "Button (w/ repeat)", "Checkbox", "SliderFloat", "InputText", "InputTextMultiline", "InputFloat",
            //            "InputFloat3", "ColorEdit4", "Selectable", "MenuItem", "TreeNode", "TreeNode (w/ double-click)", "Combo", "ListBox"
            //        };
            //        static int item_type = 4;
            //        static bool item_disabled = false;
            //        Imgui.Combo("Item Type", &item_type, item_names, IM_ARRAYSIZE(item_names), IM_ARRAYSIZE(item_names));
            //        Imgui.SameLine();
            //        HelpMarker("Testing how various types of items are interacting with the IsItemXXX functions. Note that the bool return value of most ImGui function is generally equivalent to calling Imgui.IsItemHovered().");
            //        Imgui.Checkbox("Item Disabled",  &item_disabled);
            //
            //        // Submit selected items so we can query their status in the code following it.
            //        bool ret = false;
            //        static bool b = false;
            //        static float col4f[4] = { 1.0f, 0.5, 0.0f, 1.0f };
            //        static char str[16] = {};
            //        if (item_disabled)
            //            Imgui.BeginDisabled(true);
            //        if (item_type == 0) { Imgui.Text("ITEM: Text"); }                                              // Testing text items with no identifier/interaction
            //        if (item_type == 1) { ret = Imgui.Button("ITEM: Button"); }                                    // Testing button
            //        if (item_type == 2) { Imgui.PushButtonRepeat(true); ret = Imgui.Button("ITEM: Button"); Imgui.PopButtonRepeat(); } // Testing button (with repeater)
            //        if (item_type == 3) { ret = Imgui.Checkbox("ITEM: Checkbox", &b); }                            // Testing checkbox
            //        if (item_type == 4) { ret = Imgui.SliderFloat("ITEM: SliderFloat", &col4f[0], 0.0f, 1.0f); }   // Testing basic item
            //        if (item_type == 5) { ret = Imgui.InputText("ITEM: InputText", &str[0], IM_ARRAYSIZE(str)); }  // Testing input text (which handles tabbing)
            //        if (item_type == 6) { ret = Imgui.InputTextMultiline("ITEM: InputTextMultiline", &str[0], IM_ARRAYSIZE(str)); } // Testing input text (which uses a child window)
            //        if (item_type == 7) { ret = Imgui.InputFloat("ITEM: InputFloat", col4f, 1.0f); }               // Testing +/- buttons on scalar input
            //        if (item_type == 8) { ret = Imgui.InputFloat3("ITEM: InputFloat3", col4f); }                   // Testing multi-component items (IsItemXXX flags are reported merged)
            //        if (item_type == 9) { ret = Imgui.ColorEdit4("ITEM: ColorEdit4", col4f); }                     // Testing multi-component items (IsItemXXX flags are reported merged)
            //        if (item_type == 10){ ret = Imgui.Selectable("ITEM: Selectable"); }                            // Testing selectable item
            //        if (item_type == 11){ ret = Imgui.MenuItem("ITEM: MenuItem"); }                                // Testing menu item (they use ImGuiButtonFlags_PressedOnRelease button policy)
            //        if (item_type == 12){ ret = Imgui.TreeNode("ITEM: TreeNode"); if (ret) Imgui.TreePop(); }     // Testing tree node
            //        if (item_type == 13){ ret = Imgui.TreeNodeEx("ITEM: TreeNode w/ ImGuiTreeNodeFlags_OpenOnDoubleClick", ImGuiTreeNodeFlags_OpenOnDoubleClick | ImGuiTreeNodeFlags_NoTreePushOnOpen); } // Testing tree node with ImGuiButtonFlags_PressedOnDoubleClick button policy.
            //        if (item_type == 14){ const char* items[] = { "Apple", "Banana", "Cherry", "Kiwi" }; static int current = 1; ret = Imgui.Combo("ITEM: Combo", &current, items, IM_ARRAYSIZE(items)); }
            //        if (item_type == 15){ const char* items[] = { "Apple", "Banana", "Cherry", "Kiwi" }; static int current = 1; ret = Imgui.ListBox("ITEM: ListBox", &current, items, IM_ARRAYSIZE(items), IM_ARRAYSIZE(items)); }
            //
            //        bool hovered_delay_none = Imgui.IsItemHovered();
            //        bool hovered_delay_short = Imgui.IsItemHovered(ImGuiHoveredFlags_DelayShort);
            //        bool hovered_delay_normal = Imgui.IsItemHovered(ImGuiHoveredFlags_DelayNormal);
            //
            //        // Display the values of IsItemHovered() and other common item state functions.
            //        // Note that the ImGuiHoveredFlags_XXX flags can be combined.
            //        // Because BulletText is an item itself and that would affect the output of IsItemXXX functions,
            //        // we query every state in a single call to avoid storing them and to simplify the code.
            //        Imgui.BulletText(
            //            "Return value = %d\n"
            //            "IsItemFocused() = %d\n"
            //            "IsItemHovered() = %d\n"
            //            "IsItemHovered(_AllowWhenBlockedByPopup) = %d\n"
            //            "IsItemHovered(_AllowWhenBlockedByActiveItem) = %d\n"
            //            "IsItemHovered(_AllowWhenOverlapped) = %d\n"
            //            "IsItemHovered(_AllowWhenDisabled) = %d\n"
            //            "IsItemHovered(_RectOnly) = %d\n"
            //            "IsItemActive() = %d\n"
            //            "IsItemEdited() = %d\n"
            //            "IsItemActivated() = %d\n"
            //            "IsItemDeactivated() = %d\n"
            //            "IsItemDeactivatedAfterEdit() = %d\n"
            //            "IsItemVisible() = %d\n"
            //            "IsItemClicked() = %d\n"
            //            "IsItemToggledOpen() = %d\n"
            //            "GetItemRectMin() = (%.1f, %.1f)\n"
            //            "GetItemRectMax() = (%.1f, %.1f)\n"
            //            "GetItemRectSize() = (%.1f, %.1f)",
            //            ret,
            //            Imgui.IsItemFocused(),
            //            Imgui.IsItemHovered(),
            //            Imgui.IsItemHovered(ImGuiHoveredFlags_AllowWhenBlockedByPopup),
            //            Imgui.IsItemHovered(ImGuiHoveredFlags_AllowWhenBlockedByActiveItem),
            //            Imgui.IsItemHovered(ImGuiHoveredFlags_AllowWhenOverlapped),
            //            Imgui.IsItemHovered(ImGuiHoveredFlags_AllowWhenDisabled),
            //            Imgui.IsItemHovered(ImGuiHoveredFlags_RectOnly),
            //            Imgui.IsItemActive(),
            //            Imgui.IsItemEdited(),
            //            Imgui.IsItemActivated(),
            //            Imgui.IsItemDeactivated(),
            //            Imgui.IsItemDeactivatedAfterEdit(),
            //            Imgui.IsItemVisible(),
            //            Imgui.IsItemClicked(),
            //            Imgui.IsItemToggledOpen(),
            //            Imgui.GetItemRectMin().x, Imgui.GetItemRectMin().y,
            //            Imgui.GetItemRectMax().x, Imgui.GetItemRectMax().y,
            //            Imgui.GetItemRectSize().x, Imgui.GetItemRectSize().y
            //        );
            //        Imgui.BulletText(
            //            "w/ Hovering Delay: None = %d, Fast %d, Normal = %d", hovered_delay_none, hovered_delay_short, hovered_delay_normal);
            //
            //        if (item_disabled)
            //            Imgui.EndDisabled();
            //
            //        char buf[1] = "";
            //        Imgui.InputText("unused", buf, IM_ARRAYSIZE(buf), ImGuiInputTextFlags_ReadOnly);
            //        Imgui.SameLine();
            //        HelpMarker("This widget is only here to be able to tab-out of the widgets above and see e.g. Deactivated() status.");
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Widgets/Querying Window Status (Focused,Hovered etc.)");
            //    if (Imgui.TreeNode("Querying Window Status (Focused/Hovered etc.)"))
            //    {
            //        static bool embed_all_inside_a_child_window = false;
            //        Imgui.Checkbox("Embed everything inside a child window for testing _RootWindow flag.", &embed_all_inside_a_child_window);
            //        if (embed_all_inside_a_child_window)
            //            Imgui.BeginChild("outer_child", ImVec2(0, Imgui.GetFontSize() * 20.0f), true);
            //
            //        // Testing IsWindowFocused() function with its various flags.
            //        Imgui.BulletText(
            //            "IsWindowFocused() = %d\n"
            //            "IsWindowFocused(_ChildWindows) = %d\n"
            //            "IsWindowFocused(_ChildWindows|_NoPopupHierarchy) = %d\n"
            //            "IsWindowFocused(_ChildWindows|_RootWindow) = %d\n"
            //            "IsWindowFocused(_ChildWindows|_RootWindow|_NoPopupHierarchy) = %d\n"
            //            "IsWindowFocused(_RootWindow) = %d\n"
            //            "IsWindowFocused(_RootWindow|_NoPopupHierarchy) = %d\n"
            //            "IsWindowFocused(_AnyWindow) = %d\n",
            //            Imgui.IsWindowFocused(),
            //            Imgui.IsWindowFocused(ImGuiFocusedFlags_ChildWindows),
            //            Imgui.IsWindowFocused(ImGuiFocusedFlags_ChildWindows | ImGuiFocusedFlags_NoPopupHierarchy),
            //            Imgui.IsWindowFocused(ImGuiFocusedFlags_ChildWindows | ImGuiFocusedFlags_RootWindow),
            //            Imgui.IsWindowFocused(ImGuiFocusedFlags_ChildWindows | ImGuiFocusedFlags_RootWindow | ImGuiFocusedFlags_NoPopupHierarchy),
            //            Imgui.IsWindowFocused(ImGuiFocusedFlags_RootWindow),
            //            Imgui.IsWindowFocused(ImGuiFocusedFlags_RootWindow | ImGuiFocusedFlags_NoPopupHierarchy),
            //            Imgui.IsWindowFocused(ImGuiFocusedFlags_AnyWindow));
            //
            //        // Testing IsWindowHovered() function with its various flags.
            //        Imgui.BulletText(
            //            "IsWindowHovered() = %d\n"
            //            "IsWindowHovered(_AllowWhenBlockedByPopup) = %d\n"
            //            "IsWindowHovered(_AllowWhenBlockedByActiveItem) = %d\n"
            //            "IsWindowHovered(_ChildWindows) = %d\n"
            //            "IsWindowHovered(_ChildWindows|_NoPopupHierarchy) = %d\n"
            //            "IsWindowHovered(_ChildWindows|_RootWindow) = %d\n"
            //            "IsWindowHovered(_ChildWindows|_RootWindow|_NoPopupHierarchy) = %d\n"
            //            "IsWindowHovered(_RootWindow) = %d\n"
            //            "IsWindowHovered(_RootWindow|_NoPopupHierarchy) = %d\n"
            //            "IsWindowHovered(_ChildWindows|_AllowWhenBlockedByPopup) = %d\n"
            //            "IsWindowHovered(_AnyWindow) = %d\n",
            //            Imgui.IsWindowHovered(),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_AllowWhenBlockedByPopup),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_AllowWhenBlockedByActiveItem),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_ChildWindows),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_ChildWindows | ImGuiHoveredFlags_NoPopupHierarchy),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_ChildWindows | ImGuiHoveredFlags_RootWindow),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_ChildWindows | ImGuiHoveredFlags_RootWindow | ImGuiHoveredFlags_NoPopupHierarchy),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_RootWindow),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_RootWindow | ImGuiHoveredFlags_NoPopupHierarchy),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_ChildWindows | ImGuiHoveredFlags_AllowWhenBlockedByPopup),
            //            Imgui.IsWindowHovered(ImGuiHoveredFlags_AnyWindow));
            //
            //        Imgui.BeginChild("child", ImVec2(0, 50), true);
            //        Imgui.Text("This is another child window for testing the _ChildWindows flag.");
            //        Imgui.EndChild();
            //        if (embed_all_inside_a_child_window)
            //            Imgui.EndChild();
            //
            //        // Calling IsItemHovered() after begin returns the hovered status of the title bar.
            //        // This is useful in particular if you want to create a context menu associated to the title bar of a window.
            //        static bool test_window = false;
            //        Imgui.Checkbox("Hovered/Active tests after Begin() for title bar testing", &test_window);
            //        if (test_window)
            //        {
            //            Imgui.Begin("Title bar Hovered/Active tests", &test_window);
            //            if (Imgui.BeginPopupContextItem()) // <-- This is using IsItemHovered()
            //            {
            //                if (Imgui.MenuItem("Close")) { test_window = false; }
            //                Imgui.EndPopup();
            //            }
            //            Imgui.Text(
            //                "IsItemHovered() after begin = %d (== is title bar hovered)\n"
            //                "IsItemActive() after begin = %d (== is window being clicked/moved)\n",
            //                Imgui.IsItemHovered(), Imgui.IsItemActive());
            //            Imgui.End();
            //        }
            //
            //        Imgui.TreePop();
            //    }

            // Demonstrate BeginDisabled/EndDisabled using a checkbox located at the bottom of the section (which is a bit odd:
            // logically we'd have this checkbox at the top of the section, but we don't want this feature to steal that space)
            if (s_disableAll)
            {
                Imgui.EndDisabled();
            }

            if (Imgui.TreeNode("Disable block"))
            {
                _ = Imgui.Checkbox("Disable entire section above", s_disableAll);
                Imgui.SameLine();
                HelpMarker("Demonstrate using BeginDisabled()/EndDisabled() across this section.");
                Imgui.TreePop();
            }

            //    IMGUI_DEMO_MARKER("Widgets/Text Filter");
            //    if (Imgui.TreeNode("Text Filter"))
            //    {
            //        // Helper class to easy setup a text filter.
            //        // You may want to implement a more feature-full filtering scheme in your own application.
            //        HelpMarker("Not a widget per-se, but ImGuiTextFilter is a helper to perform simple filtering on text strings.");
            //        static ImGuiTextFilter filter;
            //        Imgui.Text("Filter usage:\n"
            //            "  \"\"         display all lines\n"
            //            "  \"xxx\"      display lines containing \"xxx\"\n"
            //            "  \"xxx,yyy\"  display lines containing \"xxx\" or \"yyy\"\n"
            //            "  \"-xxx\"     hide lines containing \"xxx\"");
            //        filter.Draw();
            //        const char* lines[] = { "aaa1.c", "bbb1.c", "ccc1.c", "aaa2.cpp", "bbb2.cpp", "ccc2.cpp", "abc.h", "hello, world" };
            //        for (int i = 0; i < IM_ARRAYSIZE(lines); i++)
            //            if (filter.PassFilter(lines[i]))
            //                Imgui.BulletText("%s", lines[i]);
            //        Imgui.TreePop();
            //    }
        }

        private static void ShowDemoWindowLayout()
        {
            //    IMGUI_DEMO_MARKER("Layout");
            //    if (!Imgui.CollapsingHeader("Layout & Scrolling"))
            //        return;
            //
            //    IMGUI_DEMO_MARKER("Layout/Child windows");
            //    if (Imgui.TreeNode("Child windows"))
            //    {
            //        HelpMarker("Use child windows to begin into a self-contained independent scrolling/clipping regions within a host window.");
            //        static bool disable_mouse_wheel = false;
            //        static bool disable_menu = false;
            //        Imgui.Checkbox("Disable Mouse Wheel", &disable_mouse_wheel);
            //        Imgui.Checkbox("Disable Menu", &disable_menu);
            //
            //        // Child 1: no border, enable horizontal scrollbar
            //        {
            //            ImGuiWindowFlags window_flags = ImGuiWindowFlags_HorizontalScrollbar;
            //            if (disable_mouse_wheel)
            //                window_flags |= ImGuiWindowFlags_NoScrollWithMouse;
            //            Imgui.BeginChild("ChildL", ImVec2(Imgui.GetContentRegionAvail().x * 0.5f, 260), false, window_flags);
            //            for (int i = 0; i < 100; i++)
            //                Imgui.Text("%04d: scrollable region", i);
            //            Imgui.EndChild();
            //        }
            //
            //        Imgui.SameLine();
            //
            //        // Child 2: rounded border
            //        {
            //            ImGuiWindowFlags window_flags = ImGuiWindowFlags_None;
            //            if (disable_mouse_wheel)
            //                window_flags |= ImGuiWindowFlags_NoScrollWithMouse;
            //            if (!disable_menu)
            //                window_flags |= ImGuiWindowFlags_MenuBar;
            //            Imgui.PushStyleVar(ImGuiStyleVar_ChildRounding, 5.0f);
            //            Imgui.BeginChild("ChildR", ImVec2(0, 260), true, window_flags);
            //            if (!disable_menu && Imgui.BeginMenuBar())
            //            {
            //                if (Imgui.BeginMenu("Menu"))
            //                {
            //                    ShowExampleMenuFile();
            //                    Imgui.EndMenu();
            //                }
            //                Imgui.EndMenuBar();
            //            }
            //            if (Imgui.BeginTable("split", 2, ImGuiTableFlags_Resizable | ImGuiTableFlags_NoSavedSettings))
            //            {
            //                for (int i = 0; i < 100; i++)
            //                {
            //                    char buf[32];
            //                    sprintf(buf, "%03d", i);
            //                    Imgui.TableNextColumn();
            //                    Imgui.Button(buf, ImVec2(-FLT_MIN, 0.0f));
            //                }
            //                Imgui.EndTable();
            //            }
            //            Imgui.EndChild();
            //            Imgui.PopStyleVar();
            //        }
            //
            //        Imgui.Separator();
            //
            //        // Demonstrate a few extra things
            //        // - Changing ImGuiCol_ChildBg (which is transparent black in default styles)
            //        // - Using SetCursorPos() to position child window (the child window is an item from the POV of parent window)
            //        //   You can also call SetNextWindowPos() to position the child window. The parent window will effectively
            //        //   layout from this position.
            //        // - Using Imgui.GetItemRectMin/Max() to query the "item" state (because the child window is an item from
            //        //   the POV of the parent window). See 'Demo->Querying Status (Edited/Active/Hovered etc.)' for details.
            //        {
            //            static int offset_x = 0;
            //            Imgui.SetNextItemWidth(Imgui.GetFontSize() * 8);
            //            Imgui.DragInt("Offset X", &offset_x, 1.0f, -1000, 1000);
            //
            //            Imgui.SetCursorPosX(Imgui.GetCursorPosX() + (float)offset_x);
            //            Imgui.PushStyleColor(ImGuiCol_ChildBg, IM_COL32(255, 0, 0, 100));
            //            Imgui.BeginChild("Red", ImVec2(200, 100), true, ImGuiWindowFlags_None);
            //            for (int n = 0; n < 50; n++)
            //                Imgui.Text("Some test %d", n);
            //            Imgui.EndChild();
            //            bool child_is_hovered = Imgui.IsItemHovered();
            //            ImVec2 child_rect_min = Imgui.GetItemRectMin();
            //            ImVec2 child_rect_max = Imgui.GetItemRectMax();
            //            Imgui.PopStyleColor();
            //            Imgui.Text("Hovered: %d", child_is_hovered);
            //            Imgui.Text("Rect of child window is: (%.0f,%.0f) (%.0f,%.0f)", child_rect_min.x, child_rect_min.y, child_rect_max.x, child_rect_max.y);
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Layout/Widgets Width");
            //    if (Imgui.TreeNode("Widgets Width"))
            //    {
            //        static float f = 0.0f;
            //        static bool show_indented_items = true;
            //        Imgui.Checkbox("Show indented items", &show_indented_items);
            //
            //        // Use SetNextItemWidth() to set the width of a single upcoming item.
            //        // Use PushItemWidth()/PopItemWidth() to set the width of a group of items.
            //        // In real code use you'll probably want to choose width values that are proportional to your font size
            //        // e.g. Using '20.0f * GetFontSize()' as width instead of '200.0f', etc.
            //
            //        Imgui.Text("SetNextItemWidth/PushItemWidth(100)");
            //        Imgui.SameLine(); HelpMarker("Fixed width.");
            //        Imgui.PushItemWidth(100);
            //        Imgui.DragFloat("float##1b", &f);
            //        if (show_indented_items)
            //        {
            //            Imgui.Indent();
            //            Imgui.DragFloat("float (indented)##1b", &f);
            //            Imgui.Unindent();
            //        }
            //        Imgui.PopItemWidth();
            //
            //        Imgui.Text("SetNextItemWidth/PushItemWidth(-100)");
            //        Imgui.SameLine(); HelpMarker("Align to right edge minus 100");
            //        Imgui.PushItemWidth(-100);
            //        Imgui.DragFloat("float##2a", &f);
            //        if (show_indented_items)
            //        {
            //            Imgui.Indent();
            //            Imgui.DragFloat("float (indented)##2b", &f);
            //            Imgui.Unindent();
            //        }
            //        Imgui.PopItemWidth();
            //
            //        Imgui.Text("SetNextItemWidth/PushItemWidth(GetContentRegionAvail().x * 0.5f)");
            //        Imgui.SameLine(); HelpMarker("Half of available width.\n(~ right-cursor_pos)\n(works within a column set)");
            //        Imgui.PushItemWidth(Imgui.GetContentRegionAvail().x * 0.5f);
            //        Imgui.DragFloat("float##3a", &f);
            //        if (show_indented_items)
            //        {
            //            Imgui.Indent();
            //            Imgui.DragFloat("float (indented)##3b", &f);
            //            Imgui.Unindent();
            //        }
            //        Imgui.PopItemWidth();
            //
            //        Imgui.Text("SetNextItemWidth/PushItemWidth(-GetContentRegionAvail().x * 0.5f)");
            //        Imgui.SameLine(); HelpMarker("Align to right edge minus half");
            //        Imgui.PushItemWidth(-Imgui.GetContentRegionAvail().x * 0.5f);
            //        Imgui.DragFloat("float##4a", &f);
            //        if (show_indented_items)
            //        {
            //            Imgui.Indent();
            //            Imgui.DragFloat("float (indented)##4b", &f);
            //            Imgui.Unindent();
            //        }
            //        Imgui.PopItemWidth();
            //
            //        // Demonstrate using PushItemWidth to surround three items.
            //        // Calling SetNextItemWidth() before each of them would have the same effect.
            //        Imgui.Text("SetNextItemWidth/PushItemWidth(-FLT_MIN)");
            //        Imgui.SameLine(); HelpMarker("Align to right edge");
            //        Imgui.PushItemWidth(-FLT_MIN);
            //        Imgui.DragFloat("##float5a", &f);
            //        if (show_indented_items)
            //        {
            //            Imgui.Indent();
            //            Imgui.DragFloat("float (indented)##5b", &f);
            //            Imgui.Unindent();
            //        }
            //        Imgui.PopItemWidth();
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Layout/Basic Horizontal Layout");
            //    if (Imgui.TreeNode("Basic Horizontal Layout"))
            //    {
            //        Imgui.TextWrapped("(Use Imgui.SameLine() to keep adding items to the right of the preceding item)");
            //
            //        // Text
            //        IMGUI_DEMO_MARKER("Layout/Basic Horizontal Layout/SameLine");
            //        Imgui.Text("Two items: Hello"); Imgui.SameLine();
            //        Imgui.TextColored(ImVec4(1,1,0,1), "Sailor");
            //
            //        // Adjust spacing
            //        Imgui.Text("More spacing: Hello"); Imgui.SameLine(0, 20);
            //        Imgui.TextColored(ImVec4(1,1,0,1), "Sailor");
            //
            //        // Button
            //        Imgui.AlignTextToFramePadding();
            //        Imgui.Text("Normal buttons"); Imgui.SameLine();
            //        Imgui.Button("Banana"); Imgui.SameLine();
            //        Imgui.Button("Apple"); Imgui.SameLine();
            //        Imgui.Button("Corniflower");
            //
            //        // Button
            //        Imgui.Text("Small buttons"); Imgui.SameLine();
            //        Imgui.SmallButton("Like this one"); Imgui.SameLine();
            //        Imgui.Text("can fit within a text block.");
            //
            //        // Aligned to arbitrary position. Easy/cheap column.
            //        IMGUI_DEMO_MARKER("Layout/Basic Horizontal Layout/SameLine (with offset)");
            //        Imgui.Text("Aligned");
            //        Imgui.SameLine(150); Imgui.Text("x=150");
            //        Imgui.SameLine(300); Imgui.Text("x=300");
            //        Imgui.Text("Aligned");
            //        Imgui.SameLine(150); Imgui.SmallButton("x=150");
            //        Imgui.SameLine(300); Imgui.SmallButton("x=300");
            //
            //        // Checkbox
            //        IMGUI_DEMO_MARKER("Layout/Basic Horizontal Layout/SameLine (more)");
            //        static bool c1 = false, c2 = false, c3 = false, c4 = false;
            //        Imgui.Checkbox("My", &c1); Imgui.SameLine();
            //        Imgui.Checkbox("Tailor", &c2); Imgui.SameLine();
            //        Imgui.Checkbox("Is", &c3); Imgui.SameLine();
            //        Imgui.Checkbox("Rich", &c4);
            //
            //        // Various
            //        static float f0 = 1.0f, f1 = 2.0f, f2 = 3.0f;
            //        Imgui.PushItemWidth(80);
            //        const char* items[] = { "AAAA", "BBBB", "CCCC", "DDDD" };
            //        static int item = -1;
            //        Imgui.Combo("Combo", &item, items, IM_ARRAYSIZE(items)); Imgui.SameLine();
            //        Imgui.SliderFloat("X", &f0, 0.0f, 5.0f); Imgui.SameLine();
            //        Imgui.SliderFloat("Y", &f1, 0.0f, 5.0f); Imgui.SameLine();
            //        Imgui.SliderFloat("Z", &f2, 0.0f, 5.0f);
            //        Imgui.PopItemWidth();
            //
            //        Imgui.PushItemWidth(80);
            //        Imgui.Text("Lists:");
            //        static int selection[4] = { 0, 1, 2, 3 };
            //        for (int i = 0; i < 4; i++)
            //        {
            //            if (i > 0) Imgui.SameLine();
            //            Imgui.PushID(i);
            //            Imgui.ListBox("", &selection[i], items, IM_ARRAYSIZE(items));
            //            Imgui.PopID();
            //            //if (Imgui.IsItemHovered()) Imgui.SetTooltip("ListBox %d hovered", i);
            //        }
            //        Imgui.PopItemWidth();
            //
            //        // Dummy
            //        IMGUI_DEMO_MARKER("Layout/Basic Horizontal Layout/Dummy");
            //        ImVec2 button_sz(40, 40);
            //        Imgui.Button("A", button_sz); Imgui.SameLine();
            //        Imgui.Dummy(button_sz); Imgui.SameLine();
            //        Imgui.Button("B", button_sz);
            //
            //        // Manually wrapping
            //        // (we should eventually provide this as an automatic layout feature, but for now you can do it manually)
            //        IMGUI_DEMO_MARKER("Layout/Basic Horizontal Layout/Manual wrapping");
            //        Imgui.Text("Manual wrapping:");
            //        ImGuiStyle& style = Imgui.GetStyle();
            //        int buttons_count = 20;
            //        float window_visible_x2 = Imgui.GetWindowPos().x + Imgui.GetWindowContentRegionMax().x;
            //        for (int n = 0; n < buttons_count; n++)
            //        {
            //            Imgui.PushID(n);
            //            Imgui.Button("Box", button_sz);
            //            float last_button_x2 = Imgui.GetItemRectMax().x;
            //            float next_button_x2 = last_button_x2 + style.ItemSpacing.x + button_sz.x; // Expected position if next button was on same line
            //            if (n + 1 < buttons_count && next_button_x2 < window_visible_x2)
            //                Imgui.SameLine();
            //            Imgui.PopID();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Layout/Groups");
            //    if (Imgui.TreeNode("Groups"))
            //    {
            //        HelpMarker(
            //            "BeginGroup() basically locks the horizontal position for new line. "
            //            "EndGroup() bundles the whole group so that you can use \"item\" functions such as "
            //            "IsItemHovered()/IsItemActive() or SameLine() etc. on the whole group.");
            //        Imgui.BeginGroup();
            //        {
            //            Imgui.BeginGroup();
            //            Imgui.Button("AAA");
            //            Imgui.SameLine();
            //            Imgui.Button("BBB");
            //            Imgui.SameLine();
            //            Imgui.BeginGroup();
            //            Imgui.Button("CCC");
            //            Imgui.Button("DDD");
            //            Imgui.EndGroup();
            //            Imgui.SameLine();
            //            Imgui.Button("EEE");
            //            Imgui.EndGroup();
            //            if (Imgui.IsItemHovered())
            //                Imgui.SetTooltip("First group hovered");
            //        }
            //        // Capture the group size and create widgets using the same size
            //        ImVec2 size = Imgui.GetItemRectSize();
            //        const float values[5] = { 0.5f, 0.20f, 0.80f, 0.60f, 0.25f };
            //        Imgui.PlotHistogram("##values", values, IM_ARRAYSIZE(values), 0, NULL, 0.0f, 1.0f, size);
            //
            //        Imgui.Button("ACTION", ImVec2((size.x - Imgui.GetStyle().ItemSpacing.x) * 0.5f, size.y));
            //        Imgui.SameLine();
            //        Imgui.Button("REACTION", ImVec2((size.x - Imgui.GetStyle().ItemSpacing.x) * 0.5f, size.y));
            //        Imgui.EndGroup();
            //        Imgui.SameLine();
            //
            //        Imgui.Button("LEVERAGE\nBUZZWORD", size);
            //        Imgui.SameLine();
            //
            //        if (Imgui.BeginListBox("List", size))
            //        {
            //            Imgui.Selectable("Selected", true);
            //            Imgui.Selectable("Not Selected", false);
            //            Imgui.EndListBox();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Layout/Text Baseline Alignment");
            //    if (Imgui.TreeNode("Text Baseline Alignment"))
            //    {
            //        {
            //            Imgui.BulletText("Text baseline:");
            //            Imgui.SameLine(); HelpMarker(
            //                "This is testing the vertical alignment that gets applied on text to keep it aligned with widgets. "
            //                "Lines only composed of text or \"small\" widgets use less vertical space than lines with framed widgets.");
            //            Imgui.Indent();
            //
            //            Imgui.Text("KO Blahblah"); Imgui.SameLine();
            //            Imgui.Button("Some framed item"); Imgui.SameLine();
            //            HelpMarker("Baseline of button will look misaligned with text..");
            //
            //            // If your line starts with text, call AlignTextToFramePadding() to align text to upcoming widgets.
            //            // (because we don't know what's coming after the Text() statement, we need to move the text baseline
            //            // down by FramePadding.y ahead of time)
            //            Imgui.AlignTextToFramePadding();
            //            Imgui.Text("OK Blahblah"); Imgui.SameLine();
            //            Imgui.Button("Some framed item"); Imgui.SameLine();
            //            HelpMarker("We call AlignTextToFramePadding() to vertically align the text baseline by +FramePadding.y");
            //
            //            // SmallButton() uses the same vertical padding as Text
            //            Imgui.Button("TEST##1"); Imgui.SameLine();
            //            Imgui.Text("TEST"); Imgui.SameLine();
            //            Imgui.SmallButton("TEST##2");
            //
            //            // If your line starts with text, call AlignTextToFramePadding() to align text to upcoming widgets.
            //            Imgui.AlignTextToFramePadding();
            //            Imgui.Text("Text aligned to framed item"); Imgui.SameLine();
            //            Imgui.Button("Item##1"); Imgui.SameLine();
            //            Imgui.Text("Item"); Imgui.SameLine();
            //            Imgui.SmallButton("Item##2"); Imgui.SameLine();
            //            Imgui.Button("Item##3");
            //
            //            Imgui.Unindent();
            //        }
            //
            //        Imgui.Spacing();
            //
            //        {
            //            Imgui.BulletText("Multi-line text:");
            //            Imgui.Indent();
            //            Imgui.Text("One\nTwo\nThree"); Imgui.SameLine();
            //            Imgui.Text("Hello\nWorld"); Imgui.SameLine();
            //            Imgui.Text("Banana");
            //
            //            Imgui.Text("Banana"); Imgui.SameLine();
            //            Imgui.Text("Hello\nWorld"); Imgui.SameLine();
            //            Imgui.Text("One\nTwo\nThree");
            //
            //            Imgui.Button("HOP##1"); Imgui.SameLine();
            //            Imgui.Text("Banana"); Imgui.SameLine();
            //            Imgui.Text("Hello\nWorld"); Imgui.SameLine();
            //            Imgui.Text("Banana");
            //
            //            Imgui.Button("HOP##2"); Imgui.SameLine();
            //            Imgui.Text("Hello\nWorld"); Imgui.SameLine();
            //            Imgui.Text("Banana");
            //            Imgui.Unindent();
            //        }
            //
            //        Imgui.Spacing();
            //
            //        {
            //            Imgui.BulletText("Misc items:");
            //            Imgui.Indent();
            //
            //            // SmallButton() sets FramePadding to zero. Text baseline is aligned to match baseline of previous Button.
            //            Imgui.Button("80x80", ImVec2(80, 80));
            //            Imgui.SameLine();
            //            Imgui.Button("50x50", ImVec2(50, 50));
            //            Imgui.SameLine();
            //            Imgui.Button("Button()");
            //            Imgui.SameLine();
            //            Imgui.SmallButton("SmallButton()");
            //
            //            // Tree
            //            const float spacing = Imgui.GetStyle().ItemInnerSpacing.x;
            //            Imgui.Button("Button##1");
            //            Imgui.SameLine(0.0f, spacing);
            //            if (Imgui.TreeNode("Node##1"))
            //            {
            //                // Placeholder tree data
            //                for (int i = 0; i < 6; i++)
            //                    Imgui.BulletText("Item %d..", i);
            //                Imgui.TreePop();
            //            }
            //
            //            // Vertically align text node a bit lower so it'll be vertically centered with upcoming widget.
            //            // Otherwise you can use SmallButton() (smaller fit).
            //            Imgui.AlignTextToFramePadding();
            //
            //            // Common mistake to avoid: if we want to SameLine after TreeNode we need to do it before we add
            //            // other contents below the node.
            //            bool node_open = Imgui.TreeNode("Node##2");
            //            Imgui.SameLine(0.0f, spacing); Imgui.Button("Button##2");
            //            if (node_open)
            //            {
            //                // Placeholder tree data
            //                for (int i = 0; i < 6; i++)
            //                    Imgui.BulletText("Item %d..", i);
            //                Imgui.TreePop();
            //            }
            //
            //            // Bullet
            //            Imgui.Button("Button##3");
            //            Imgui.SameLine(0.0f, spacing);
            //            Imgui.BulletText("Bullet text");
            //
            //            Imgui.AlignTextToFramePadding();
            //            Imgui.BulletText("Node");
            //            Imgui.SameLine(0.0f, spacing); Imgui.Button("Button##4");
            //            Imgui.Unindent();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Layout/Scrolling");
            //    if (Imgui.TreeNode("Scrolling"))
            //    {
            //        // Vertical scroll functions
            //        IMGUI_DEMO_MARKER("Layout/Scrolling/Vertical");
            //        HelpMarker("Use SetScrollHereY() or SetScrollFromPosY() to scroll to a given vertical position.");
            //
            //        static int track_item = 50;
            //        static bool enable_track = true;
            //        static bool enable_extra_decorations = false;
            //        static float scroll_to_off_px = 0.0f;
            //        static float scroll_to_pos_px = 200.0f;
            //
            //        Imgui.Checkbox("Decoration", &enable_extra_decorations);
            //
            //        Imgui.Checkbox("Track", &enable_track);
            //        Imgui.PushItemWidth(100);
            //        Imgui.SameLine(140); enable_track |= Imgui.DragInt("##item", &track_item, 0.25f, 0, 99, "Item = %d");
            //
            //        bool scroll_to_off = Imgui.Button("Scroll Offset");
            //        Imgui.SameLine(140); scroll_to_off |= Imgui.DragFloat("##off", &scroll_to_off_px, 1.00f, 0, FLT_MAX, "+%.0f px");
            //
            //        bool scroll_to_pos = Imgui.Button("Scroll To Pos");
            //        Imgui.SameLine(140); scroll_to_pos |= Imgui.DragFloat("##pos", &scroll_to_pos_px, 1.00f, -10, FLT_MAX, "X/Y = %.0f px");
            //        Imgui.PopItemWidth();
            //
            //        if (scroll_to_off || scroll_to_pos)
            //            enable_track = false;
            //
            //        ImGuiStyle& style = Imgui.GetStyle();
            //        float child_w = (Imgui.GetContentRegionAvail().x - 4 * style.ItemSpacing.x) / 5;
            //        if (child_w < 1.0f)
            //            child_w = 1.0f;
            //        Imgui.PushID("##VerticalScrolling");
            //        for (int i = 0; i < 5; i++)
            //        {
            //            if (i > 0) Imgui.SameLine();
            //            Imgui.BeginGroup();
            //            const char* names[] = { "Top", "25%", "Center", "75%", "Bottom" };
            //            Imgui.TextUnformatted(names[i]);
            //
            //            const ImGuiWindowFlags child_flags = enable_extra_decorations ? ImGuiWindowFlags_MenuBar : 0;
            //            const ImGuiID child_id = Imgui.GetID((void*)(intptr_t)i);
            //            const bool child_is_visible = Imgui.BeginChild(child_id, ImVec2(child_w, 200.0f), true, child_flags);
            //            if (Imgui.BeginMenuBar())
            //            {
            //                Imgui.TextUnformatted("abc");
            //                Imgui.EndMenuBar();
            //            }
            //            if (scroll_to_off)
            //                Imgui.SetScrollY(scroll_to_off_px);
            //            if (scroll_to_pos)
            //                Imgui.SetScrollFromPosY(Imgui.GetCursorStartPos().y + scroll_to_pos_px, i * 0.25f);
            //            if (child_is_visible) // Avoid calling SetScrollHereY when running with culled items
            //            {
            //                for (int item = 0; item < 100; item++)
            //                {
            //                    if (enable_track && item == track_item)
            //                    {
            //                        Imgui.TextColored(ImVec4(1, 1, 0, 1), "Item %d", item);
            //                        Imgui.SetScrollHereY(i * 0.25f); // 0.0f:top, 0.5f:center, 1.0f:bottom
            //                    }
            //                    else
            //                    {
            //                        Imgui.Text("Item %d", item);
            //                    }
            //                }
            //            }
            //            float scroll_y = Imgui.GetScrollY();
            //            float scroll_max_y = Imgui.GetScrollMaxY();
            //            Imgui.EndChild();
            //            Imgui.Text("%.0f/%.0f", scroll_y, scroll_max_y);
            //            Imgui.EndGroup();
            //        }
            //        Imgui.PopID();
            //
            //        // Horizontal scroll functions
            //        IMGUI_DEMO_MARKER("Layout/Scrolling/Horizontal");
            //        Imgui.Spacing();
            //        HelpMarker(
            //            "Use SetScrollHereX() or SetScrollFromPosX() to scroll to a given horizontal position.\n\n"
            //            "Because the clipping rectangle of most window hides half worth of WindowPadding on the "
            //            "left/right, using SetScrollFromPosX(+1) will usually result in clipped text whereas the "
            //            "equivalent SetScrollFromPosY(+1) wouldn't.");
            //        Imgui.PushID("##HorizontalScrolling");
            //        for (int i = 0; i < 5; i++)
            //        {
            //            float child_height = Imgui.GetTextLineHeight() + style.ScrollbarSize + style.WindowPadding.y * 2.0f;
            //            ImGuiWindowFlags child_flags = ImGuiWindowFlags_HorizontalScrollbar | (enable_extra_decorations ? ImGuiWindowFlags_AlwaysVerticalScrollbar : 0);
            //            ImGuiID child_id = Imgui.GetID((void*)(intptr_t)i);
            //            bool child_is_visible = Imgui.BeginChild(child_id, ImVec2(-100, child_height), true, child_flags);
            //            if (scroll_to_off)
            //                Imgui.SetScrollX(scroll_to_off_px);
            //            if (scroll_to_pos)
            //                Imgui.SetScrollFromPosX(Imgui.GetCursorStartPos().x + scroll_to_pos_px, i * 0.25f);
            //            if (child_is_visible) // Avoid calling SetScrollHereY when running with culled items
            //            {
            //                for (int item = 0; item < 100; item++)
            //                {
            //                    if (item > 0)
            //                        Imgui.SameLine();
            //                    if (enable_track && item == track_item)
            //                    {
            //                        Imgui.TextColored(ImVec4(1, 1, 0, 1), "Item %d", item);
            //                        Imgui.SetScrollHereX(i * 0.25f); // 0.0f:left, 0.5f:center, 1.0f:right
            //                    }
            //                    else
            //                    {
            //                        Imgui.Text("Item %d", item);
            //                    }
            //                }
            //            }
            //            float scroll_x = Imgui.GetScrollX();
            //            float scroll_max_x = Imgui.GetScrollMaxX();
            //            Imgui.EndChild();
            //            Imgui.SameLine();
            //            const char* names[] = { "Left", "25%", "Center", "75%", "Right" };
            //            Imgui.Text("%s\n%.0f/%.0f", names[i], scroll_x, scroll_max_x);
            //            Imgui.Spacing();
            //        }
            //        Imgui.PopID();
            //
            //        // Miscellaneous Horizontal Scrolling Demo
            //        IMGUI_DEMO_MARKER("Layout/Scrolling/Horizontal (more)");
            //        HelpMarker(
            //            "Horizontal scrolling for a window is enabled via the ImGuiWindowFlags_HorizontalScrollbar flag.\n\n"
            //            "You may want to also explicitly specify content width by using SetNextWindowContentWidth() before Begin().");
            //        static int lines = 7;
            //        Imgui.SliderInt("Lines", &lines, 1, 15);
            //        Imgui.PushStyleVar(ImGuiStyleVar_FrameRounding, 3.0f);
            //        Imgui.PushStyleVar(ImGuiStyleVar_FramePadding, ImVec2(2.0f, 1.0f));
            //        ImVec2 scrolling_child_size = ImVec2(0, Imgui.GetFrameHeightWithSpacing() * 7 + 30);
            //        Imgui.BeginChild("scrolling", scrolling_child_size, true, ImGuiWindowFlags_HorizontalScrollbar);
            //        for (int line = 0; line < lines; line++)
            //        {
            //            // Display random stuff. For the sake of this trivial demo we are using basic Button() + SameLine()
            //            // If you want to create your own time line for a real application you may be better off manipulating
            //            // the cursor position yourself, aka using SetCursorPos/SetCursorScreenPos to position the widgets
            //            // yourself. You may also want to use the lower-level ImDrawList API.
            //            int num_buttons = 10 + ((line & 1) ? line * 9 : line * 3);
            //            for (int n = 0; n < num_buttons; n++)
            //            {
            //                if (n > 0) Imgui.SameLine();
            //                Imgui.PushID(n + line * 1000);
            //                char num_buf[16];
            //                sprintf(num_buf, "%d", n);
            //                const char* label = (!(n % 15)) ? "FizzBuzz" : (!(n % 3)) ? "Fizz" : (!(n % 5)) ? "Buzz" : num_buf;
            //                float hue = n * 0.05f;
            //                Imgui.PushStyleColor(ImGuiCol_Button, (ImVec4)ImColor::HSV(hue, 0.6f, 0.6f));
            //                Imgui.PushStyleColor(ImGuiCol_ButtonHovered, (ImVec4)ImColor::HSV(hue, 0.7f, 0.7f));
            //                Imgui.PushStyleColor(ImGuiCol_ButtonActive, (ImVec4)ImColor::HSV(hue, 0.8f, 0.8f));
            //                Imgui.Button(label, ImVec2(40.0f + sinf((float)(line + n)) * 20.0f, 0.0f));
            //                Imgui.PopStyleColor(3);
            //                Imgui.PopID();
            //            }
            //        }
            //        float scroll_x = Imgui.GetScrollX();
            //        float scroll_max_x = Imgui.GetScrollMaxX();
            //        Imgui.EndChild();
            //        Imgui.PopStyleVar(2);
            //        float scroll_x_delta = 0.0f;
            //        Imgui.SmallButton("<<");
            //        if (Imgui.IsItemActive())
            //            scroll_x_delta = -Imgui.GetIO().DeltaTime * 1000.0f;
            //        Imgui.SameLine();
            //        Imgui.Text("Scroll from code"); Imgui.SameLine();
            //        Imgui.SmallButton(">>");
            //        if (Imgui.IsItemActive())
            //            scroll_x_delta = +Imgui.GetIO().DeltaTime * 1000.0f;
            //        Imgui.SameLine();
            //        Imgui.Text("%.0f/%.0f", scroll_x, scroll_max_x);
            //        if (scroll_x_delta != 0.0f)
            //        {
            //            // Demonstrate a trick: you can use Begin to set yourself in the context of another window
            //            // (here we are already out of your child window)
            //            Imgui.BeginChild("scrolling");
            //            Imgui.SetScrollX(Imgui.GetScrollX() + scroll_x_delta);
            //            Imgui.EndChild();
            //        }
            //        Imgui.Spacing();
            //
            //        static bool show_horizontal_contents_size_demo_window = false;
            //        Imgui.Checkbox("Show Horizontal contents size demo window", &show_horizontal_contents_size_demo_window);
            //
            //        if (show_horizontal_contents_size_demo_window)
            //        {
            //            static bool show_h_scrollbar = true;
            //            static bool show_button = true;
            //            static bool show_tree_nodes = true;
            //            static bool show_text_wrapped = false;
            //            static bool show_columns = true;
            //            static bool show_tab_bar = true;
            //            static bool show_child = false;
            //            static bool explicit_content_size = false;
            //            static float contents_size_x = 300.0f;
            //            if (explicit_content_size)
            //                Imgui.SetNextWindowContentSize(ImVec2(contents_size_x, 0.0f));
            //            Imgui.Begin("Horizontal contents size demo window", &show_horizontal_contents_size_demo_window, show_h_scrollbar ? ImGuiWindowFlags_HorizontalScrollbar : 0);
            //            IMGUI_DEMO_MARKER("Layout/Scrolling/Horizontal contents size demo window");
            //            Imgui.PushStyleVar(ImGuiStyleVar_ItemSpacing, ImVec2(2, 0));
            //            Imgui.PushStyleVar(ImGuiStyleVar_FramePadding, ImVec2(2, 0));
            //            HelpMarker("Test of different widgets react and impact the work rectangle growing when horizontal scrolling is enabled.\n\nUse 'Metrics->Tools->Show windows rectangles' to visualize rectangles.");
            //            Imgui.Checkbox("H-scrollbar", &show_h_scrollbar);
            //            Imgui.Checkbox("Button", &show_button);            // Will grow contents size (unless explicitly overwritten)
            //            Imgui.Checkbox("Tree nodes", &show_tree_nodes);    // Will grow contents size and display highlight over full width
            //            Imgui.Checkbox("Text wrapped", &show_text_wrapped);// Will grow and use contents size
            //            Imgui.Checkbox("Columns", &show_columns);          // Will use contents size
            //            Imgui.Checkbox("Tab bar", &show_tab_bar);          // Will use contents size
            //            Imgui.Checkbox("Child", &show_child);              // Will grow and use contents size
            //            Imgui.Checkbox("Explicit content size", &explicit_content_size);
            //            Imgui.Text("Scroll %.1f/%.1f %.1f/%.1f", Imgui.GetScrollX(), Imgui.GetScrollMaxX(), Imgui.GetScrollY(), Imgui.GetScrollMaxY());
            //            if (explicit_content_size)
            //            {
            //                Imgui.SameLine();
            //                Imgui.SetNextItemWidth(100);
            //                Imgui.DragFloat("##csx", &contents_size_x);
            //                ImVec2 p = Imgui.GetCursorScreenPos();
            //                Imgui.GetWindowDrawList()->AddRectFilled(p, ImVec2(p.x + 10, p.y + 10), IM_COL32_WHITE);
            //                Imgui.GetWindowDrawList()->AddRectFilled(ImVec2(p.x + contents_size_x - 10, p.y), ImVec2(p.x + contents_size_x, p.y + 10), IM_COL32_WHITE);
            //                Imgui.Dummy(ImVec2(0, 10));
            //            }
            //            Imgui.PopStyleVar(2);
            //            Imgui.Separator();
            //            if (show_button)
            //            {
            //                Imgui.Button("this is a 300-wide button", ImVec2(300, 0));
            //            }
            //            if (show_tree_nodes)
            //            {
            //                bool open = true;
            //                if (Imgui.TreeNode("this is a tree node"))
            //                {
            //                    if (Imgui.TreeNode("another one of those tree node..."))
            //                    {
            //                        Imgui.Text("Some tree contents");
            //                        Imgui.TreePop();
            //                    }
            //                    Imgui.TreePop();
            //                }
            //                Imgui.CollapsingHeader("CollapsingHeader", &open);
            //            }
            //            if (show_text_wrapped)
            //            {
            //                Imgui.TextWrapped("This text should automatically wrap on the edge of the work rectangle.");
            //            }
            //            if (show_columns)
            //            {
            //                Imgui.Text("Tables:");
            //                if (Imgui.BeginTable("table", 4, ImGuiTableFlags_Borders))
            //                {
            //                    for (int n = 0; n < 4; n++)
            //                    {
            //                        Imgui.TableNextColumn();
            //                        Imgui.Text("Width %.2f", Imgui.GetContentRegionAvail().x);
            //                    }
            //                    Imgui.EndTable();
            //                }
            //                Imgui.Text("Columns:");
            //                Imgui.Columns(4);
            //                for (int n = 0; n < 4; n++)
            //                {
            //                    Imgui.Text("Width %.2f", Imgui.GetColumnWidth());
            //                    Imgui.NextColumn();
            //                }
            //                Imgui.Columns(1);
            //            }
            //            if (show_tab_bar && Imgui.BeginTabBar("Hello"))
            //            {
            //                if (Imgui.BeginTabItem("OneOneOne")) { Imgui.EndTabItem(); }
            //                if (Imgui.BeginTabItem("TwoTwoTwo")) { Imgui.EndTabItem(); }
            //                if (Imgui.BeginTabItem("ThreeThreeThree")) { Imgui.EndTabItem(); }
            //                if (Imgui.BeginTabItem("FourFourFour")) { Imgui.EndTabItem(); }
            //                Imgui.EndTabBar();
            //            }
            //            if (show_child)
            //            {
            //                Imgui.BeginChild("child", ImVec2(0, 0), true);
            //                Imgui.EndChild();
            //            }
            //            Imgui.End();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Layout/Clipping");
            //    if (Imgui.TreeNode("Clipping"))
            //    {
            //        static ImVec2 size(100.0f, 100.0f);
            //        static ImVec2 offset(30.0f, 30.0f);
            //        Imgui.DragFloat2("size", (float*)&size, 0.5f, 1.0f, 200.0f, "%.0f");
            //        Imgui.TextWrapped("(Click and drag to scroll)");
            //
            //        HelpMarker(
            //            "(Left) Using Imgui.PushClipRect():\n"
            //            "Will alter ImGui hit-testing logic + ImDrawList rendering.\n"
            //            "(use this if you want your clipping rectangle to affect interactions)\n\n"
            //            "(Center) Using ImDrawList::PushClipRect():\n"
            //            "Will alter ImDrawList rendering only.\n"
            //            "(use this as a shortcut if you are only using ImDrawList calls)\n\n"
            //            "(Right) Using ImDrawList::AddText() with a fine ClipRect:\n"
            //            "Will alter only this specific ImDrawList::AddText() rendering.\n"
            //            "This is often used internally to avoid altering the clipping rectangle and minimize draw calls.");
            //
            //        for (int n = 0; n < 3; n++)
            //        {
            //            if (n > 0)
            //                Imgui.SameLine();
            //
            //            Imgui.PushID(n);
            //            Imgui.InvisibleButton("##canvas", size);
            //            if (Imgui.IsItemActive() && Imgui.IsMouseDragging(ImGuiMouseButton_Left))
            //            {
            //                offset.x += Imgui.GetIO().MouseDelta.x;
            //                offset.y += Imgui.GetIO().MouseDelta.y;
            //            }
            //            Imgui.PopID();
            //            if (!Imgui.IsItemVisible()) // Skip rendering as ImDrawList elements are not clipped.
            //                continue;
            //
            //            const ImVec2 p0 = Imgui.GetItemRectMin();
            //            const ImVec2 p1 = Imgui.GetItemRectMax();
            //            const char* text_str = "Line 1 hello\nLine 2 clip me!";
            //            const ImVec2 text_pos = ImVec2(p0.x + offset.x, p0.y + offset.y);
            //            ImDrawList* draw_list = Imgui.GetWindowDrawList();
            //            switch (n)
            //            {
            //            case 0:
            //                Imgui.PushClipRect(p0, p1, true);
            //                draw_list->AddRectFilled(p0, p1, IM_COL32(90, 90, 120, 255));
            //                draw_list->AddText(text_pos, IM_COL32_WHITE, text_str);
            //                Imgui.PopClipRect();
            //                break;
            //            case 1:
            //                draw_list->PushClipRect(p0, p1, true);
            //                draw_list->AddRectFilled(p0, p1, IM_COL32(90, 90, 120, 255));
            //                draw_list->AddText(text_pos, IM_COL32_WHITE, text_str);
            //                draw_list->PopClipRect();
            //                break;
            //            case 2:
            //                ImVec4 clip_rect(p0.x, p0.y, p1.x, p1.y); // AddText() takes a ImVec4* here so let's convert.
            //                draw_list->AddRectFilled(p0, p1, IM_COL32(90, 90, 120, 255));
            //                draw_list->AddText(Imgui.GetFont(), Imgui.GetFontSize(), text_pos, IM_COL32_WHITE, text_str, NULL, 0.0f, &clip_rect);
            //                break;
            //            }
            //        }
            //
            //        Imgui.TreePop();
            //    }
        }

        private static void ShowDemoWindowPopups()
        {
            //    IMGUI_DEMO_MARKER("Popups");
            //    if (!Imgui.CollapsingHeader("Popups & Modal windows"))
            //        return;
            //
            //    // The properties of popups windows are:
            //    // - They block normal mouse hovering detection outside them. (*)
            //    // - Unless modal, they can be closed by clicking anywhere outside them, or by pressing ESCAPE.
            //    // - Their visibility state (~bool) is held internally by Dear ImGui instead of being held by the programmer as
            //    //   we are used to with regular Begin() calls. User can manipulate the visibility state by calling OpenPopup().
            //    // (*) One can use IsItemHovered(ImGuiHoveredFlags_AllowWhenBlockedByPopup) to bypass it and detect hovering even
            //    //     when normally blocked by a popup.
            //    // Those three properties are connected. The library needs to hold their visibility state BECAUSE it can close
            //    // popups at any time.
            //
            //    // Typical use for regular windows:
            //    //   bool my_tool_is_active = false; if (Imgui.Button("Open")) my_tool_is_active = true; [...] if (my_tool_is_active) Begin("My Tool", &my_tool_is_active) { [...] } End();
            //    // Typical use for popups:
            //    //   if (Imgui.Button("Open")) Imgui.OpenPopup("MyPopup"); if (Imgui.BeginPopup("MyPopup") { [...] EndPopup(); }
            //
            //    // With popups we have to go through a library call (here OpenPopup) to manipulate the visibility state.
            //    // This may be a bit confusing at first but it should quickly make sense. Follow on the examples below.
            //
            //    IMGUI_DEMO_MARKER("Popups/Popups");
            //    if (Imgui.TreeNode("Popups"))
            //    {
            //        Imgui.TextWrapped(
            //            "When a popup is active, it inhibits interacting with windows that are behind the popup. "
            //            "Clicking outside the popup closes it.");
            //
            //        static int selected_fish = -1;
            //        const char* names[] = { "Bream", "Haddock", "Mackerel", "Pollock", "Tilefish" };
            //        static bool toggles[] = { true, false, false, false, false };
            //
            //        // Simple selection popup (if you want to show the current selection inside the Button itself,
            //        // you may want to build a string using the "###" operator to preserve a constant ID with a variable label)
            //        if (Imgui.Button("Select.."))
            //            Imgui.OpenPopup("my_select_popup");
            //        Imgui.SameLine();
            //        Imgui.TextUnformatted(selected_fish == -1 ? "<None>" : names[selected_fish]);
            //        if (Imgui.BeginPopup("my_select_popup"))
            //        {
            //            Imgui.Text("Aquarium");
            //            Imgui.Separator();
            //            for (int i = 0; i < IM_ARRAYSIZE(names); i++)
            //                if (Imgui.Selectable(names[i]))
            //                    selected_fish = i;
            //            Imgui.EndPopup();
            //        }
            //
            //        // Showing a menu with toggles
            //        if (Imgui.Button("Toggle.."))
            //            Imgui.OpenPopup("my_toggle_popup");
            //        if (Imgui.BeginPopup("my_toggle_popup"))
            //        {
            //            for (int i = 0; i < IM_ARRAYSIZE(names); i++)
            //                Imgui.MenuItem(names[i], "", &toggles[i]);
            //            if (Imgui.BeginMenu("Sub-menu"))
            //            {
            //                Imgui.MenuItem("Click me");
            //                Imgui.EndMenu();
            //            }
            //
            //            Imgui.Separator();
            //            Imgui.Text("Tooltip here");
            //            if (Imgui.IsItemHovered())
            //                Imgui.SetTooltip("I am a tooltip over a popup");
            //
            //            if (Imgui.Button("Stacked Popup"))
            //                Imgui.OpenPopup("another popup");
            //            if (Imgui.BeginPopup("another popup"))
            //            {
            //                for (int i = 0; i < IM_ARRAYSIZE(names); i++)
            //                    Imgui.MenuItem(names[i], "", &toggles[i]);
            //                if (Imgui.BeginMenu("Sub-menu"))
            //                {
            //                    Imgui.MenuItem("Click me");
            //                    if (Imgui.Button("Stacked Popup"))
            //                        Imgui.OpenPopup("another popup");
            //                    if (Imgui.BeginPopup("another popup"))
            //                    {
            //                        Imgui.Text("I am the last one here.");
            //                        Imgui.EndPopup();
            //                    }
            //                    Imgui.EndMenu();
            //                }
            //                Imgui.EndPopup();
            //            }
            //            Imgui.EndPopup();
            //        }
            //
            //        // Call the more complete ShowExampleMenuFile which we use in various places of this demo
            //        if (Imgui.Button("With a menu.."))
            //            Imgui.OpenPopup("my_file_popup");
            //        if (Imgui.BeginPopup("my_file_popup", ImGuiWindowFlags_MenuBar))
            //        {
            //            if (Imgui.BeginMenuBar())
            //            {
            //                if (Imgui.BeginMenu("File"))
            //                {
            //                    ShowExampleMenuFile();
            //                    Imgui.EndMenu();
            //                }
            //                if (Imgui.BeginMenu("Edit"))
            //                {
            //                    Imgui.MenuItem("Dummy");
            //                    Imgui.EndMenu();
            //                }
            //                Imgui.EndMenuBar();
            //            }
            //            Imgui.Text("Hello from popup!");
            //            Imgui.Button("This is a dummy button..");
            //            Imgui.EndPopup();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Popups/Context menus");
            //    if (Imgui.TreeNode("Context menus"))
            //    {
            //        HelpMarker("\"Context\" functions are simple helpers to associate a Popup to a given Item or Window identifier.");
            //
            //        // BeginPopupContextItem() is a helper to provide common/simple popup behavior of essentially doing:
            //        //     if (id == 0)
            //        //         id = GetItemID(); // Use last item id
            //        //     if (IsItemHovered() && IsMouseReleased(ImGuiMouseButton_Right))
            //        //         OpenPopup(id);
            //        //     return BeginPopup(id);
            //        // For advanced uses you may want to replicate and customize this code.
            //        // See more details in BeginPopupContextItem().
            //
            //        // Example 1
            //        // When used after an item that has an ID (e.g. Button), we can skip providing an ID to BeginPopupContextItem(),
            //        // and BeginPopupContextItem() will use the last item ID as the popup ID.
            //        {
            //            const char* names[5] = { "Label1", "Label2", "Label3", "Label4", "Label5" };
            //            static int selected = -1;
            //            for (int n = 0; n < 5; n++)
            //            {
            //                if (Imgui.Selectable(names[n], selected == n))
            //                    selected = n;
            //                if (Imgui.BeginPopupContextItem()) // <-- use last item id as popup id
            //                {
            //                    selected = n;
            //                    Imgui.Text("This a popup for \"%s\"!", names[n]);
            //                    if (Imgui.Button("Close"))
            //                        Imgui.CloseCurrentPopup();
            //                    Imgui.EndPopup();
            //                }
            //                if (Imgui.IsItemHovered())
            //                    Imgui.SetTooltip("Right-click to open popup");
            //            }
            //        }
            //
            //        // Example 2
            //        // Popup on a Text() element which doesn't have an identifier: we need to provide an identifier to BeginPopupContextItem().
            //        // Using an explicit identifier is also convenient if you want to activate the popups from different locations.
            //        {
            //            HelpMarker("Text() elements don't have stable identifiers so we need to provide one.");
            //            static float value = 0.5f;
            //            Imgui.Text("Value = %.3f <-- (1) right-click this text", value);
            //            if (Imgui.BeginPopupContextItem("my popup"))
            //            {
            //                if (Imgui.Selectable("Set to zero")) value = 0.0f;
            //                if (Imgui.Selectable("Set to PI")) value = 3.1415f;
            //                Imgui.SetNextItemWidth(-FLT_MIN);
            //                Imgui.DragFloat("##Value", &value, 0.1f, 0.0f, 0.0f);
            //                Imgui.EndPopup();
            //            }
            //
            //            // We can also use OpenPopupOnItemClick() to toggle the visibility of a given popup.
            //            // Here we make it that right-clicking this other text element opens the same popup as above.
            //            // The popup itself will be submitted by the code above.
            //            Imgui.Text("(2) Or right-click this text");
            //            Imgui.OpenPopupOnItemClick("my popup", ImGuiPopupFlags_MouseButtonRight);
            //
            //            // Back to square one: manually open the same popup.
            //            if (Imgui.Button("(3) Or click this button"))
            //                Imgui.OpenPopup("my popup");
            //        }
            //
            //        // Example 3
            //        // When using BeginPopupContextItem() with an implicit identifier (NULL == use last item ID),
            //        // we need to make sure your item identifier is stable.
            //        // In this example we showcase altering the item label while preserving its identifier, using the ### operator (see FAQ).
            //        {
            //            HelpMarker("Showcase using a popup ID linked to item ID, with the item having a changing label + stable ID using the ### operator.");
            //            static char name[32] = "Label1";
            //            char buf[64];
            //            sprintf(buf, "Button: %s###Button", name); // ### operator override ID ignoring the preceding label
            //            Imgui.Button(buf);
            //            if (Imgui.BeginPopupContextItem())
            //            {
            //                Imgui.Text("Edit name:");
            //                Imgui.InputText("##edit", name, IM_ARRAYSIZE(name));
            //                if (Imgui.Button("Close"))
            //                    Imgui.CloseCurrentPopup();
            //                Imgui.EndPopup();
            //            }
            //            Imgui.SameLine(); Imgui.Text("(<-- right-click here)");
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Popups/Modals");
            //    if (Imgui.TreeNode("Modals"))
            //    {
            //        Imgui.TextWrapped("Modal windows are like popups but the user cannot close them by clicking outside.");
            //
            //        if (Imgui.Button("Delete.."))
            //            Imgui.OpenPopup("Delete?");
            //
            //        // Always center this window when appearing
            //        ImVec2 center = Imgui.GetMainViewport()->GetCenter();
            //        Imgui.SetNextWindowPos(center, ImGuiCond_Appearing, ImVec2(0.5f, 0.5f));
            //
            //        if (Imgui.BeginPopupModal("Delete?", NULL, ImGuiWindowFlags_AlwaysAutoResize))
            //        {
            //            Imgui.Text("All those beautiful files will be deleted.\nThis operation cannot be undone!\n\n");
            //            Imgui.Separator();
            //
            //            //static int unused_i = 0;
            //            //Imgui.Combo("Combo", &unused_i, "Delete\0Delete harder\0");
            //
            //            static bool dont_ask_me_next_time = false;
            //            Imgui.PushStyleVar(ImGuiStyleVar_FramePadding, ImVec2(0, 0));
            //            Imgui.Checkbox("Don't ask me next time", &dont_ask_me_next_time);
            //            Imgui.PopStyleVar();
            //
            //            if (Imgui.Button("OK", ImVec2(120, 0))) { Imgui.CloseCurrentPopup(); }
            //            Imgui.SetItemDefaultFocus();
            //            Imgui.SameLine();
            //            if (Imgui.Button("Cancel", ImVec2(120, 0))) { Imgui.CloseCurrentPopup(); }
            //            Imgui.EndPopup();
            //        }
            //
            //        if (Imgui.Button("Stacked modals.."))
            //            Imgui.OpenPopup("Stacked 1");
            //        if (Imgui.BeginPopupModal("Stacked 1", NULL, ImGuiWindowFlags_MenuBar))
            //        {
            //            if (Imgui.BeginMenuBar())
            //            {
            //                if (Imgui.BeginMenu("File"))
            //                {
            //                    if (Imgui.MenuItem("Some menu item")) {}
            //                    Imgui.EndMenu();
            //                }
            //                Imgui.EndMenuBar();
            //            }
            //            Imgui.Text("Hello from Stacked The First\nUsing style.Colors[ImGuiCol_ModalWindowDimBg] behind it.");
            //
            //            // Testing behavior of widgets stacking their own regular popups over the modal.
            //            static int item = 1;
            //            static float color[4] = { 0.4f, 0.7f, 0.0f, 0.5f };
            //            Imgui.Combo("Combo", &item, "aaaa\0bbbb\0cccc\0dddd\0eeee\0\0");
            //            Imgui.ColorEdit4("color", color);
            //
            //            if (Imgui.Button("Add another modal.."))
            //                Imgui.OpenPopup("Stacked 2");
            //
            //            // Also demonstrate passing a bool* to BeginPopupModal(), this will create a regular close button which
            //            // will close the popup. Note that the visibility state of popups is owned by imgui, so the input value
            //            // of the bool actually doesn't matter here.
            //            bool unused_open = true;
            //            if (Imgui.BeginPopupModal("Stacked 2", &unused_open))
            //            {
            //                Imgui.Text("Hello from Stacked The Second!");
            //                if (Imgui.Button("Close"))
            //                    Imgui.CloseCurrentPopup();
            //                Imgui.EndPopup();
            //            }
            //
            //            if (Imgui.Button("Close"))
            //                Imgui.CloseCurrentPopup();
            //            Imgui.EndPopup();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Popups/Menus inside a regular window");
            //    if (Imgui.TreeNode("Menus inside a regular window"))
            //    {
            //        Imgui.TextWrapped("Below we are testing adding menu items to a regular window. It's rather unusual but should work!");
            //        Imgui.Separator();
            //
            //        Imgui.MenuItem("Menu item", "CTRL+M");
            //        if (Imgui.BeginMenu("Menu inside a regular window"))
            //        {
            //            ShowExampleMenuFile();
            //            Imgui.EndMenu();
            //        }
            //        Imgui.Separator();
            //        Imgui.TreePop();
            //    }
        }

        //// Dummy data structure that we use for the Table demo.
        //// (pre-C++11 doesn't allow us to instantiate ImVector<MyItem> template if this structure is defined inside the demo function)
        //namespace
        //{
        //// We are passing our own identifier to TableSetupColumn() to facilitate identifying columns in the sorting code.
        //// This identifier will be passed down into ImGuiTableSortSpec::ColumnUserID.
        //// But it is possible to omit the user id parameter of TableSetupColumn() and just use the column index instead! (ImGuiTableSortSpec::ColumnIndex)
        //// If you don't use sorting, you will generally never care about giving column an ID!
        //enum MyItemColumnID
        //{
        //    MyItemColumnID_ID,
        //    MyItemColumnID_Name,
        //    MyItemColumnID_Action,
        //    MyItemColumnID_Quantity,
        //    MyItemColumnID_Description
        //};
        //
        //struct MyItem
        //{
        //    int         ID;
        //    const char* Name;
        //    int         Quantity;
        //
        //    // We have a problem which is affecting _only this demo_ and should not affect your code:
        //    // As we don't rely on std:: or other third-party library to compile dear imgui, we only have reliable access to qsort(),
        //    // however qsort doesn't allow passing user data to comparing function.
        //    // As a workaround, we are storing the sort specs in a static/global for the comparing function to access.
        //    // In your own use case you would probably pass the sort specs to your sorting/comparing functions directly and not use a global.
        //    // We could technically call Imgui.TableGetSortSpecs() in CompareWithSortSpecs(), but considering that this function is called
        //    // very often by the sorting algorithm it would be a little wasteful.
        //    static const ImGuiTableSortSpecs* s_current_sort_specs;
        //
        //    // Compare function to be used by qsort()
        //    static int IMGUI_CDECL CompareWithSortSpecs(const void* lhs, const void* rhs)
        //    {
        //        const MyItem* a = (const MyItem*)lhs;
        //        const MyItem* b = (const MyItem*)rhs;
        //        for (int n = 0; n < s_current_sort_specs->SpecsCount; n++)
        //        {
        //            // Here we identify columns using the ColumnUserID value that we ourselves passed to TableSetupColumn()
        //            // We could also choose to identify columns based on their index (sort_spec->ColumnIndex), which is simpler!
        //            const ImGuiTableColumnSortSpecs* sort_spec = &s_current_sort_specs->Specs[n];
        //            int delta = 0;
        //            switch (sort_spec->ColumnUserID)
        //            {
        //            case MyItemColumnID_ID:             delta = (a->ID - b->ID);                break;
        //            case MyItemColumnID_Name:           delta = (strcmp(a->Name, b->Name));     break;
        //            case MyItemColumnID_Quantity:       delta = (a->Quantity - b->Quantity);    break;
        //            case MyItemColumnID_Description:    delta = (strcmp(a->Name, b->Name));     break;
        //            default: IM_ASSERT(0); break;
        //            }
        //            if (delta > 0)
        //                return (sort_spec->SortDirection == ImGuiSortDirection_Ascending) ? +1 : -1;
        //            if (delta < 0)
        //                return (sort_spec->SortDirection == ImGuiSortDirection_Ascending) ? -1 : +1;
        //        }
        //
        //        // qsort() is instable so always return a way to differenciate items.
        //        // Your own compare function may want to avoid fallback on implicit sort specs e.g. a Name compare if it wasn't already part of the sort specs.
        //        return (a->ID - b->ID);
        //    }
        //};
        //const ImGuiTableSortSpecs* MyItem::s_current_sort_specs = NULL;
        //}
        //
        //// Make the UI compact because there are so many fields
        //static void PushStyleCompact()
        //{
        //    ImGuiStyle& style = Imgui.GetStyle();
        //    Imgui.PushStyleVar(ImGuiStyleVar_FramePadding, ImVec2(style.FramePadding.x, (float)(int)(style.FramePadding.y * 0.60f)));
        //    Imgui.PushStyleVar(ImGuiStyleVar_ItemSpacing, ImVec2(style.ItemSpacing.x, (float)(int)(style.ItemSpacing.y * 0.60f)));
        //}
        //
        //static void PopStyleCompact()
        //{
        //    Imgui.PopStyleVar(2);
        //}
        //
        //// Show a combo box with a choice of sizing policies
        //static void EditTableSizingFlags(ImGuiTableFlags* p_flags)
        //{
        //    struct EnumDesc { ImGuiTableFlags Value; const char* Name; const char* Tooltip; };
        //    static const EnumDesc policies[] =
        //    {
        //        { ImGuiTableFlags_None,               "Default",                            "Use default sizing policy:\n- ImGuiTableFlags_SizingFixedFit if ScrollX is on or if host window has ImGuiWindowFlags_AlwaysAutoResize.\n- ImGuiTableFlags_SizingStretchSame otherwise." },
        //        { ImGuiTableFlags_SizingFixedFit,     "ImGuiTableFlags_SizingFixedFit",     "Columns default to _WidthFixed (if resizable) or _WidthAuto (if not resizable), matching contents width." },
        //        { ImGuiTableFlags_SizingFixedSame,    "ImGuiTableFlags_SizingFixedSame",    "Columns are all the same width, matching the maximum contents width.\nImplicitly disable ImGuiTableFlags_Resizable and enable ImGuiTableFlags_NoKeepColumnsVisible." },
        //        { ImGuiTableFlags_SizingStretchProp,  "ImGuiTableFlags_SizingStretchProp",  "Columns default to _WidthStretch with weights proportional to their widths." },
        //        { ImGuiTableFlags_SizingStretchSame,  "ImGuiTableFlags_SizingStretchSame",  "Columns default to _WidthStretch with same weights." }
        //    };
        //    int idx;
        //    for (idx = 0; idx < IM_ARRAYSIZE(policies); idx++)
        //        if (policies[idx].Value == (*p_flags & ImGuiTableFlags_SizingMask_))
        //            break;
        //    const char* preview_text = (idx < IM_ARRAYSIZE(policies)) ? policies[idx].Name + (idx > 0 ? strlen("ImGuiTableFlags") : 0) : "";
        //    if (Imgui.BeginCombo("Sizing Policy", preview_text))
        //    {
        //        for (int n = 0; n < IM_ARRAYSIZE(policies); n++)
        //            if (Imgui.Selectable(policies[n].Name, idx == n))
        //                *p_flags = (*p_flags & ~ImGuiTableFlags_SizingMask_) | policies[n].Value;
        //        Imgui.EndCombo();
        //    }
        //    Imgui.SameLine();
        //    Imgui.TextDisabled("(?)");
        //    if (Imgui.IsItemHovered())
        //    {
        //        Imgui.BeginTooltip();
        //        Imgui.PushTextWrapPos(Imgui.GetFontSize() * 50.0f);
        //        for (int m = 0; m < IM_ARRAYSIZE(policies); m++)
        //        {
        //            Imgui.Separator();
        //            Imgui.Text("%s:", policies[m].Name);
        //            Imgui.Separator();
        //            Imgui.SetCursorPosX(Imgui.GetCursorPosX() + Imgui.GetStyle().IndentSpacing * 0.5f);
        //            Imgui.TextUnformatted(policies[m].Tooltip);
        //        }
        //        Imgui.PopTextWrapPos();
        //        Imgui.EndTooltip();
        //    }
        //}
        //
        //static void EditTableColumnsFlags(ImGuiTableColumnFlags* p_flags)
        //{
        //    Imgui.CheckboxFlags("_Disabled", p_flags, ImGuiTableColumnFlags_Disabled); Imgui.SameLine(); HelpMarker("Master disable flag (also hide from context menu)");
        //    Imgui.CheckboxFlags("_DefaultHide", p_flags, ImGuiTableColumnFlags_DefaultHide);
        //    Imgui.CheckboxFlags("_DefaultSort", p_flags, ImGuiTableColumnFlags_DefaultSort);
        //    if (Imgui.CheckboxFlags("_WidthStretch", p_flags, ImGuiTableColumnFlags_WidthStretch))
        //        *p_flags &= ~(ImGuiTableColumnFlags_WidthMask_ ^ ImGuiTableColumnFlags_WidthStretch);
        //    if (Imgui.CheckboxFlags("_WidthFixed", p_flags, ImGuiTableColumnFlags_WidthFixed))
        //        *p_flags &= ~(ImGuiTableColumnFlags_WidthMask_ ^ ImGuiTableColumnFlags_WidthFixed);
        //    Imgui.CheckboxFlags("_NoResize", p_flags, ImGuiTableColumnFlags_NoResize);
        //    Imgui.CheckboxFlags("_NoReorder", p_flags, ImGuiTableColumnFlags_NoReorder);
        //    Imgui.CheckboxFlags("_NoHide", p_flags, ImGuiTableColumnFlags_NoHide);
        //    Imgui.CheckboxFlags("_NoClip", p_flags, ImGuiTableColumnFlags_NoClip);
        //    Imgui.CheckboxFlags("_NoSort", p_flags, ImGuiTableColumnFlags_NoSort);
        //    Imgui.CheckboxFlags("_NoSortAscending", p_flags, ImGuiTableColumnFlags_NoSortAscending);
        //    Imgui.CheckboxFlags("_NoSortDescending", p_flags, ImGuiTableColumnFlags_NoSortDescending);
        //    Imgui.CheckboxFlags("_NoHeaderLabel", p_flags, ImGuiTableColumnFlags_NoHeaderLabel);
        //    Imgui.CheckboxFlags("_NoHeaderWidth", p_flags, ImGuiTableColumnFlags_NoHeaderWidth);
        //    Imgui.CheckboxFlags("_PreferSortAscending", p_flags, ImGuiTableColumnFlags_PreferSortAscending);
        //    Imgui.CheckboxFlags("_PreferSortDescending", p_flags, ImGuiTableColumnFlags_PreferSortDescending);
        //    Imgui.CheckboxFlags("_IndentEnable", p_flags, ImGuiTableColumnFlags_IndentEnable); Imgui.SameLine(); HelpMarker("Default for column 0");
        //    Imgui.CheckboxFlags("_IndentDisable", p_flags, ImGuiTableColumnFlags_IndentDisable); Imgui.SameLine(); HelpMarker("Default for column >0");
        //}
        //
        //static void ShowTableColumnsStatusFlags(ImGuiTableColumnFlags flags)
        //{
        //    Imgui.CheckboxFlags("_IsEnabled", &flags, ImGuiTableColumnFlags_IsEnabled);
        //    Imgui.CheckboxFlags("_IsVisible", &flags, ImGuiTableColumnFlags_IsVisible);
        //    Imgui.CheckboxFlags("_IsSorted", &flags, ImGuiTableColumnFlags_IsSorted);
        //    Imgui.CheckboxFlags("_IsHovered", &flags, ImGuiTableColumnFlags_IsHovered);
        //}

        private static void ShowDemoWindowTables()
        {
            //    //Imgui.SetNextItemOpen(true, ImGuiCond_Once);
            //    IMGUI_DEMO_MARKER("Tables");
            //    if (!Imgui.CollapsingHeader("Tables & Columns"))
            //        return;
            //
            //    // Using those as a base value to create width/height that are factor of the size of our font
            //    const float TEXT_BASE_WIDTH = Imgui.CalcTextSize("A").x;
            //    const float TEXT_BASE_HEIGHT = Imgui.GetTextLineHeightWithSpacing();
            //
            //    Imgui.PushID("Tables");
            //
            //    int open_action = -1;
            //    if (Imgui.Button("Open all"))
            //        open_action = 1;
            //    Imgui.SameLine();
            //    if (Imgui.Button("Close all"))
            //        open_action = 0;
            //    Imgui.SameLine();
            //
            //    // Options
            //    static bool disable_indent = false;
            //    Imgui.Checkbox("Disable tree indentation", &disable_indent);
            //    Imgui.SameLine();
            //    HelpMarker("Disable the indenting of tree nodes so demo tables can use the full window width.");
            //    Imgui.Separator();
            //    if (disable_indent)
            //        Imgui.PushStyleVar(ImGuiStyleVar_IndentSpacing, 0.0f);
            //
            //    // About Styling of tables
            //    // Most settings are configured on a per-table basis via the flags passed to BeginTable() and TableSetupColumns APIs.
            //    // There are however a few settings that a shared and part of the ImGuiStyle structure:
            //    //   style.CellPadding                          // Padding within each cell
            //    //   style.Colors[ImGuiCol_TableHeaderBg]       // Table header background
            //    //   style.Colors[ImGuiCol_TableBorderStrong]   // Table outer and header borders
            //    //   style.Colors[ImGuiCol_TableBorderLight]    // Table inner borders
            //    //   style.Colors[ImGuiCol_TableRowBg]          // Table row background when ImGuiTableFlags_RowBg is enabled (even rows)
            //    //   style.Colors[ImGuiCol_TableRowBgAlt]       // Table row background when ImGuiTableFlags_RowBg is enabled (odds rows)
            //
            //    // Demos
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Basic");
            //    if (Imgui.TreeNode("Basic"))
            //    {
            //        // Here we will showcase three different ways to output a table.
            //        // They are very simple variations of a same thing!
            //
            //        // [Method 1] Using TableNextRow() to create a new row, and TableSetColumnIndex() to select the column.
            //        // In many situations, this is the most flexible and easy to use pattern.
            //        HelpMarker("Using TableNextRow() + calling TableSetColumnIndex() _before_ each cell, in a loop.");
            //        if (Imgui.BeginTable("table1", 3))
            //        {
            //            for (int row = 0; row < 4; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("Row %d Column %d", row, column);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        // [Method 2] Using TableNextColumn() called multiple times, instead of using a for loop + TableSetColumnIndex().
            //        // This is generally more convenient when you have code manually submitting the contents of each column.
            //        HelpMarker("Using TableNextRow() + calling TableNextColumn() _before_ each cell, manually.");
            //        if (Imgui.BeginTable("table2", 3))
            //        {
            //            for (int row = 0; row < 4; row++)
            //            {
            //                Imgui.TableNextRow();
            //                Imgui.TableNextColumn();
            //                Imgui.Text("Row %d", row);
            //                Imgui.TableNextColumn();
            //                Imgui.Text("Some contents");
            //                Imgui.TableNextColumn();
            //                Imgui.Text("123.456");
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        // [Method 3] We call TableNextColumn() _before_ each cell. We never call TableNextRow(),
            //        // as TableNextColumn() will automatically wrap around and create new rows as needed.
            //        // This is generally more convenient when your cells all contains the same type of data.
            //        HelpMarker(
            //            "Only using TableNextColumn(), which tends to be convenient for tables where every cell contains the same type of contents.\n"
            //            "This is also more similar to the old NextColumn() function of the Columns API, and provided to facilitate the Columns->Tables API transition.");
            //        if (Imgui.BeginTable("table3", 3))
            //        {
            //            for (int item = 0; item < 14; item++)
            //            {
            //                Imgui.TableNextColumn();
            //                Imgui.Text("Item %d", item);
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Borders, background");
            //    if (Imgui.TreeNode("Borders, background"))
            //    {
            //        // Expose a few Borders related flags interactively
            //        enum ContentsType { CT_Text, CT_FillButton };
            //        static ImGuiTableFlags flags = ImGuiTableFlags_Borders | ImGuiTableFlags_RowBg;
            //        static bool display_headers = false;
            //        static int contents_type = CT_Text;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_RowBg", &flags, ImGuiTableFlags_RowBg);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Borders", &flags, ImGuiTableFlags_Borders);
            //        Imgui.SameLine(); HelpMarker("ImGuiTableFlags_Borders\n = ImGuiTableFlags_BordersInnerV\n | ImGuiTableFlags_BordersOuterV\n | ImGuiTableFlags_BordersInnerV\n | ImGuiTableFlags_BordersOuterH");
            //        Imgui.Indent();
            //
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersH", &flags, ImGuiTableFlags_BordersH);
            //        Imgui.Indent();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersOuterH", &flags, ImGuiTableFlags_BordersOuterH);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersInnerH", &flags, ImGuiTableFlags_BordersInnerH);
            //        Imgui.Unindent();
            //
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersV", &flags, ImGuiTableFlags_BordersV);
            //        Imgui.Indent();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersOuterV", &flags, ImGuiTableFlags_BordersOuterV);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersInnerV", &flags, ImGuiTableFlags_BordersInnerV);
            //        Imgui.Unindent();
            //
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersOuter", &flags, ImGuiTableFlags_BordersOuter);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersInner", &flags, ImGuiTableFlags_BordersInner);
            //        Imgui.Unindent();
            //
            //        Imgui.AlignTextToFramePadding(); Imgui.Text("Cell contents:");
            //        Imgui.SameLine(); Imgui.RadioButton("Text", &contents_type, CT_Text);
            //        Imgui.SameLine(); Imgui.RadioButton("FillButton", &contents_type, CT_FillButton);
            //        Imgui.Checkbox("Display headers", &display_headers);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoBordersInBody", &flags, ImGuiTableFlags_NoBordersInBody); Imgui.SameLine(); HelpMarker("Disable vertical borders in columns Body (borders will always appear in Headers");
            //        PopStyleCompact();
            //
            //        if (Imgui.BeginTable("table1", 3, flags))
            //        {
            //            // Display headers so we can inspect their interaction with borders.
            //            // (Headers are not the main purpose of this section of the demo, so we are not elaborating on them too much. See other sections for details)
            //            if (display_headers)
            //            {
            //                Imgui.TableSetupColumn("One");
            //                Imgui.TableSetupColumn("Two");
            //                Imgui.TableSetupColumn("Three");
            //                Imgui.TableHeadersRow();
            //            }
            //
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    char buf[32];
            //                    sprintf(buf, "Hello %d,%d", column, row);
            //                    if (contents_type == CT_Text)
            //                        Imgui.TextUnformatted(buf);
            //                    else if (contents_type == CT_FillButton)
            //                        Imgui.Button(buf, ImVec2(-FLT_MIN, 0.0f));
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Resizable, stretch");
            //    if (Imgui.TreeNode("Resizable, stretch"))
            //    {
            //        // By default, if we don't enable ScrollX the sizing policy for each column is "Stretch"
            //        // All columns maintain a sizing weight, and they will occupy all available width.
            //        static ImGuiTableFlags flags = ImGuiTableFlags_SizingStretchSame | ImGuiTableFlags_Resizable | ImGuiTableFlags_BordersOuter | ImGuiTableFlags_BordersV | ImGuiTableFlags_ContextMenuInBody;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Resizable", &flags, ImGuiTableFlags_Resizable);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersV", &flags, ImGuiTableFlags_BordersV);
            //        Imgui.SameLine(); HelpMarker("Using the _Resizable flag automatically enables the _BordersInnerV flag as well, this is why the resize borders are still showing when unchecking this.");
            //        PopStyleCompact();
            //
            //        if (Imgui.BeginTable("table1", 3, flags))
            //        {
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("Hello %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Resizable, fixed");
            //    if (Imgui.TreeNode("Resizable, fixed"))
            //    {
            //        // Here we use ImGuiTableFlags_SizingFixedFit (even though _ScrollX is not set)
            //        // So columns will adopt the "Fixed" policy and will maintain a fixed width regardless of the whole available width (unless table is small)
            //        // If there is not enough available width to fit all columns, they will however be resized down.
            //        // FIXME-TABLE: Providing a stretch-on-init would make sense especially for tables which don't have saved settings
            //        HelpMarker(
            //            "Using _Resizable + _SizingFixedFit flags.\n"
            //            "Fixed-width columns generally makes more sense if you want to use horizontal scrolling.\n\n"
            //            "Double-click a column border to auto-fit the column to its contents.");
            //        PushStyleCompact();
            //        static ImGuiTableFlags flags = ImGuiTableFlags_SizingFixedFit | ImGuiTableFlags_Resizable | ImGuiTableFlags_BordersOuter | ImGuiTableFlags_BordersV | ImGuiTableFlags_ContextMenuInBody;
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoHostExtendX", &flags, ImGuiTableFlags_NoHostExtendX);
            //        PopStyleCompact();
            //
            //        if (Imgui.BeginTable("table1", 3, flags))
            //        {
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("Hello %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Resizable, mixed");
            //    if (Imgui.TreeNode("Resizable, mixed"))
            //    {
            //        HelpMarker(
            //            "Using TableSetupColumn() to alter resizing policy on a per-column basis.\n\n"
            //            "When combining Fixed and Stretch columns, generally you only want one, maybe two trailing columns to use _WidthStretch.");
            //        static ImGuiTableFlags flags = ImGuiTableFlags_SizingFixedFit | ImGuiTableFlags_RowBg | ImGuiTableFlags_Borders | ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable;
            //
            //        if (Imgui.BeginTable("table1", 3, flags))
            //        {
            //            Imgui.TableSetupColumn("AAA", ImGuiTableColumnFlags_WidthFixed);
            //            Imgui.TableSetupColumn("BBB", ImGuiTableColumnFlags_WidthFixed);
            //            Imgui.TableSetupColumn("CCC", ImGuiTableColumnFlags_WidthStretch);
            //            Imgui.TableHeadersRow();
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("%s %d,%d", (column == 2) ? "Stretch" : "Fixed", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        if (Imgui.BeginTable("table2", 6, flags))
            //        {
            //            Imgui.TableSetupColumn("AAA", ImGuiTableColumnFlags_WidthFixed);
            //            Imgui.TableSetupColumn("BBB", ImGuiTableColumnFlags_WidthFixed);
            //            Imgui.TableSetupColumn("CCC", ImGuiTableColumnFlags_WidthFixed | ImGuiTableColumnFlags_DefaultHide);
            //            Imgui.TableSetupColumn("DDD", ImGuiTableColumnFlags_WidthStretch);
            //            Imgui.TableSetupColumn("EEE", ImGuiTableColumnFlags_WidthStretch);
            //            Imgui.TableSetupColumn("FFF", ImGuiTableColumnFlags_WidthStretch | ImGuiTableColumnFlags_DefaultHide);
            //            Imgui.TableHeadersRow();
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 6; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("%s %d,%d", (column >= 3) ? "Stretch" : "Fixed", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Reorderable, hideable, with headers");
            //    if (Imgui.TreeNode("Reorderable, hideable, with headers"))
            //    {
            //        HelpMarker(
            //            "Click and drag column headers to reorder columns.\n\n"
            //            "Right-click on a header to open a context menu.");
            //        static ImGuiTableFlags flags = ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable | ImGuiTableFlags_BordersOuter | ImGuiTableFlags_BordersV;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Resizable", &flags, ImGuiTableFlags_Resizable);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Reorderable", &flags, ImGuiTableFlags_Reorderable);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Hideable", &flags, ImGuiTableFlags_Hideable);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoBordersInBody", &flags, ImGuiTableFlags_NoBordersInBody);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoBordersInBodyUntilResize", &flags, ImGuiTableFlags_NoBordersInBodyUntilResize); Imgui.SameLine(); HelpMarker("Disable vertical borders in columns Body until hovered for resize (borders will always appear in Headers)");
            //        PopStyleCompact();
            //
            //        if (Imgui.BeginTable("table1", 3, flags))
            //        {
            //            // Submit columns name with TableSetupColumn() and call TableHeadersRow() to create a row with a header in each column.
            //            // (Later we will show how TableSetupColumn() has other uses, optional flags, sizing weight etc.)
            //            Imgui.TableSetupColumn("One");
            //            Imgui.TableSetupColumn("Two");
            //            Imgui.TableSetupColumn("Three");
            //            Imgui.TableHeadersRow();
            //            for (int row = 0; row < 6; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("Hello %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        // Use outer_size.x == 0.0f instead of default to make the table as tight as possible (only valid when no scrolling and no stretch column)
            //        if (Imgui.BeginTable("table2", 3, flags | ImGuiTableFlags_SizingFixedFit, ImVec2(0.0f, 0.0f)))
            //        {
            //            Imgui.TableSetupColumn("One");
            //            Imgui.TableSetupColumn("Two");
            //            Imgui.TableSetupColumn("Three");
            //            Imgui.TableHeadersRow();
            //            for (int row = 0; row < 6; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("Fixed %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Padding");
            //    if (Imgui.TreeNode("Padding"))
            //    {
            //        // First example: showcase use of padding flags and effect of BorderOuterV/BorderInnerV on X padding.
            //        // We don't expose BorderOuterH/BorderInnerH here because they have no effect on X padding.
            //        HelpMarker(
            //            "We often want outer padding activated when any using features which makes the edges of a column visible:\n"
            //            "e.g.:\n"
            //            "- BorderOuterV\n"
            //            "- any form of row selection\n"
            //            "Because of this, activating BorderOuterV sets the default to PadOuterX. Using PadOuterX or NoPadOuterX you can override the default.\n\n"
            //            "Actual padding values are using style.CellPadding.\n\n"
            //            "In this demo we don't show horizontal borders to emphasize how they don't affect default horizontal padding.");
            //
            //        static ImGuiTableFlags flags1 = ImGuiTableFlags_BordersV;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_PadOuterX", &flags1, ImGuiTableFlags_PadOuterX);
            //        Imgui.SameLine(); HelpMarker("Enable outer-most padding (default if ImGuiTableFlags_BordersOuterV is set)");
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoPadOuterX", &flags1, ImGuiTableFlags_NoPadOuterX);
            //        Imgui.SameLine(); HelpMarker("Disable outer-most padding (default if ImGuiTableFlags_BordersOuterV is not set)");
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoPadInnerX", &flags1, ImGuiTableFlags_NoPadInnerX);
            //        Imgui.SameLine(); HelpMarker("Disable inner padding between columns (double inner padding if BordersOuterV is on, single inner padding if BordersOuterV is off)");
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersOuterV", &flags1, ImGuiTableFlags_BordersOuterV);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersInnerV", &flags1, ImGuiTableFlags_BordersInnerV);
            //        static bool show_headers = false;
            //        Imgui.Checkbox("show_headers", &show_headers);
            //        PopStyleCompact();
            //
            //        if (Imgui.BeginTable("table_padding", 3, flags1))
            //        {
            //            if (show_headers)
            //            {
            //                Imgui.TableSetupColumn("One");
            //                Imgui.TableSetupColumn("Two");
            //                Imgui.TableSetupColumn("Three");
            //                Imgui.TableHeadersRow();
            //            }
            //
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    if (row == 0)
            //                    {
            //                        Imgui.Text("Avail %.2f", Imgui.GetContentRegionAvail().x);
            //                    }
            //                    else
            //                    {
            //                        char buf[32];
            //                        sprintf(buf, "Hello %d,%d", column, row);
            //                        Imgui.Button(buf, ImVec2(-FLT_MIN, 0.0f));
            //                    }
            //                    //if (Imgui.TableGetColumnFlags() & ImGuiTableColumnFlags_IsHovered)
            //                    //    Imgui.TableSetBgColor(ImGuiTableBgTarget_CellBg, IM_COL32(0, 100, 0, 255));
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        // Second example: set style.CellPadding to (0.0) or a custom value.
            //        // FIXME-TABLE: Vertical border effectively not displayed the same way as horizontal one...
            //        HelpMarker("Setting style.CellPadding to (0,0) or a custom value.");
            //        static ImGuiTableFlags flags2 = ImGuiTableFlags_Borders | ImGuiTableFlags_RowBg;
            //        static ImVec2 cell_padding(0.0f, 0.0f);
            //        static bool show_widget_frame_bg = true;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Borders", &flags2, ImGuiTableFlags_Borders);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersH", &flags2, ImGuiTableFlags_BordersH);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersV", &flags2, ImGuiTableFlags_BordersV);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersInner", &flags2, ImGuiTableFlags_BordersInner);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersOuter", &flags2, ImGuiTableFlags_BordersOuter);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_RowBg", &flags2, ImGuiTableFlags_RowBg);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Resizable", &flags2, ImGuiTableFlags_Resizable);
            //        Imgui.Checkbox("show_widget_frame_bg", &show_widget_frame_bg);
            //        Imgui.SliderFloat2("CellPadding", &cell_padding.x, 0.0f, 10.0f, "%.0f");
            //        PopStyleCompact();
            //
            //        Imgui.PushStyleVar(ImGuiStyleVar_CellPadding, cell_padding);
            //        if (Imgui.BeginTable("table_padding_2", 3, flags2))
            //        {
            //            static char text_bufs[3 * 5][16]; // Mini text storage for 3x5 cells
            //            static bool init = true;
            //            if (!show_widget_frame_bg)
            //                Imgui.PushStyleColor(ImGuiCol_FrameBg, 0);
            //            for (int cell = 0; cell < 3 * 5; cell++)
            //            {
            //                Imgui.TableNextColumn();
            //                if (init)
            //                    strcpy(text_bufs[cell], "edit me");
            //                Imgui.SetNextItemWidth(-FLT_MIN);
            //                Imgui.PushID(cell);
            //                Imgui.InputText("##cell", text_bufs[cell], IM_ARRAYSIZE(text_bufs[cell]));
            //                Imgui.PopID();
            //            }
            //            if (!show_widget_frame_bg)
            //                Imgui.PopStyleColor();
            //            init = false;
            //            Imgui.EndTable();
            //        }
            //        Imgui.PopStyleVar();
            //
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Explicit widths");
            //    if (Imgui.TreeNode("Sizing policies"))
            //    {
            //        static ImGuiTableFlags flags1 = ImGuiTableFlags_BordersV | ImGuiTableFlags_BordersOuterH | ImGuiTableFlags_RowBg | ImGuiTableFlags_ContextMenuInBody;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Resizable", &flags1, ImGuiTableFlags_Resizable);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoHostExtendX", &flags1, ImGuiTableFlags_NoHostExtendX);
            //        PopStyleCompact();
            //
            //        static ImGuiTableFlags sizing_policy_flags[4] = { ImGuiTableFlags_SizingFixedFit, ImGuiTableFlags_SizingFixedSame, ImGuiTableFlags_SizingStretchProp, ImGuiTableFlags_SizingStretchSame };
            //        for (int table_n = 0; table_n < 4; table_n++)
            //        {
            //            Imgui.PushID(table_n);
            //            Imgui.SetNextItemWidth(TEXT_BASE_WIDTH * 30);
            //            EditTableSizingFlags(&sizing_policy_flags[table_n]);
            //
            //            // To make it easier to understand the different sizing policy,
            //            // For each policy: we display one table where the columns have equal contents width, and one where the columns have different contents width.
            //            if (Imgui.BeginTable("table1", 3, sizing_policy_flags[table_n] | flags1))
            //            {
            //                for (int row = 0; row < 3; row++)
            //                {
            //                    Imgui.TableNextRow();
            //                    Imgui.TableNextColumn(); Imgui.Text("Oh dear");
            //                    Imgui.TableNextColumn(); Imgui.Text("Oh dear");
            //                    Imgui.TableNextColumn(); Imgui.Text("Oh dear");
            //                }
            //                Imgui.EndTable();
            //            }
            //            if (Imgui.BeginTable("table2", 3, sizing_policy_flags[table_n] | flags1))
            //            {
            //                for (int row = 0; row < 3; row++)
            //                {
            //                    Imgui.TableNextRow();
            //                    Imgui.TableNextColumn(); Imgui.Text("AAAA");
            //                    Imgui.TableNextColumn(); Imgui.Text("BBBBBBBB");
            //                    Imgui.TableNextColumn(); Imgui.Text("CCCCCCCCCCCC");
            //                }
            //                Imgui.EndTable();
            //            }
            //            Imgui.PopID();
            //        }
            //
            //        Imgui.Spacing();
            //        Imgui.TextUnformatted("Advanced");
            //        Imgui.SameLine();
            //        HelpMarker("This section allows you to interact and see the effect of various sizing policies depending on whether Scroll is enabled and the contents of your columns.");
            //
            //        enum ContentsType { CT_ShowWidth, CT_ShortText, CT_LongText, CT_Button, CT_FillButton, CT_InputText };
            //        static ImGuiTableFlags flags = ImGuiTableFlags_ScrollY | ImGuiTableFlags_Borders | ImGuiTableFlags_RowBg | ImGuiTableFlags_Resizable;
            //        static int contents_type = CT_ShowWidth;
            //        static int column_count = 3;
            //
            //        PushStyleCompact();
            //        Imgui.PushID("Advanced");
            //        Imgui.PushItemWidth(TEXT_BASE_WIDTH * 30);
            //        EditTableSizingFlags(&flags);
            //        Imgui.Combo("Contents", &contents_type, "Show width\0Short Text\0Long Text\0Button\0Fill Button\0InputText\0");
            //        if (contents_type == CT_FillButton)
            //        {
            //            Imgui.SameLine();
            //            HelpMarker("Be mindful that using right-alignment (e.g. size.x = -FLT_MIN) creates a feedback loop where contents width can feed into auto-column width can feed into contents width.");
            //        }
            //        Imgui.DragInt("Columns", &column_count, 0.1f, 1, 64, "%d", ImGuiSliderFlags_AlwaysClamp);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Resizable", &flags, ImGuiTableFlags_Resizable);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_PreciseWidths", &flags, ImGuiTableFlags_PreciseWidths);
            //        Imgui.SameLine(); HelpMarker("Disable distributing remainder width to stretched columns (width allocation on a 100-wide table with 3 columns: Without this flag: 33,33,34. With this flag: 33,33,33). With larger number of columns, resizing will appear to be less smooth.");
            //        Imgui.CheckboxFlags("ImGuiTableFlags_ScrollX", &flags, ImGuiTableFlags_ScrollX);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_ScrollY", &flags, ImGuiTableFlags_ScrollY);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoClip", &flags, ImGuiTableFlags_NoClip);
            //        Imgui.PopItemWidth();
            //        Imgui.PopID();
            //        PopStyleCompact();
            //
            //        if (Imgui.BeginTable("table2", column_count, flags, ImVec2(0.0f, TEXT_BASE_HEIGHT * 7)))
            //        {
            //            for (int cell = 0; cell < 10 * column_count; cell++)
            //            {
            //                Imgui.TableNextColumn();
            //                int column = Imgui.TableGetColumnIndex();
            //                int row = Imgui.TableGetRowIndex();
            //
            //                Imgui.PushID(cell);
            //                char label[32];
            //                static char text_buf[32] = "";
            //                sprintf(label, "Hello %d,%d", column, row);
            //                switch (contents_type)
            //                {
            //                case CT_ShortText:  Imgui.TextUnformatted(label); break;
            //                case CT_LongText:   Imgui.Text("Some %s text %d,%d\nOver two lines..", column == 0 ? "long" : "longeeer", column, row); break;
            //                case CT_ShowWidth:  Imgui.Text("W: %.1f", Imgui.GetContentRegionAvail().x); break;
            //                case CT_Button:     Imgui.Button(label); break;
            //                case CT_FillButton: Imgui.Button(label, ImVec2(-FLT_MIN, 0.0f)); break;
            //                case CT_InputText:  Imgui.SetNextItemWidth(-FLT_MIN); Imgui.InputText("##", text_buf, IM_ARRAYSIZE(text_buf)); break;
            //                }
            //                Imgui.PopID();
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Vertical scrolling, with clipping");
            //    if (Imgui.TreeNode("Vertical scrolling, with clipping"))
            //    {
            //        HelpMarker("Here we activate ScrollY, which will create a child window container to allow hosting scrollable contents.\n\nWe also demonstrate using ImGuiListClipper to virtualize the submission of many items.");
            //        static ImGuiTableFlags flags = ImGuiTableFlags_ScrollY | ImGuiTableFlags_RowBg | ImGuiTableFlags_BordersOuter | ImGuiTableFlags_BordersV | ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_ScrollY", &flags, ImGuiTableFlags_ScrollY);
            //        PopStyleCompact();
            //
            //        // When using ScrollX or ScrollY we need to specify a size for our table container!
            //        // Otherwise by default the table will fit all available space, like a BeginChild() call.
            //        ImVec2 outer_size = ImVec2(0.0f, TEXT_BASE_HEIGHT * 8);
            //        if (Imgui.BeginTable("table_scrolly", 3, flags, outer_size))
            //        {
            //            Imgui.TableSetupScrollFreeze(0, 1); // Make top row always visible
            //            Imgui.TableSetupColumn("One", ImGuiTableColumnFlags_None);
            //            Imgui.TableSetupColumn("Two", ImGuiTableColumnFlags_None);
            //            Imgui.TableSetupColumn("Three", ImGuiTableColumnFlags_None);
            //            Imgui.TableHeadersRow();
            //
            //            // Demonstrate using clipper for large vertical lists
            //            ImGuiListClipper clipper;
            //            clipper.Begin(1000);
            //            while (clipper.Step())
            //            {
            //                for (int row = clipper.DisplayStart; row < clipper.DisplayEnd; row++)
            //                {
            //                    Imgui.TableNextRow();
            //                    for (int column = 0; column < 3; column++)
            //                    {
            //                        Imgui.TableSetColumnIndex(column);
            //                        Imgui.Text("Hello %d,%d", column, row);
            //                    }
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Horizontal scrolling");
            //    if (Imgui.TreeNode("Horizontal scrolling"))
            //    {
            //        HelpMarker(
            //            "When ScrollX is enabled, the default sizing policy becomes ImGuiTableFlags_SizingFixedFit, "
            //            "as automatically stretching columns doesn't make much sense with horizontal scrolling.\n\n"
            //            "Also note that as of the current version, you will almost always want to enable ScrollY along with ScrollX,"
            //            "because the container window won't automatically extend vertically to fix contents (this may be improved in future versions).");
            //        static ImGuiTableFlags flags = ImGuiTableFlags_ScrollX | ImGuiTableFlags_ScrollY | ImGuiTableFlags_RowBg | ImGuiTableFlags_BordersOuter | ImGuiTableFlags_BordersV | ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable;
            //        static int freeze_cols = 1;
            //        static int freeze_rows = 1;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Resizable", &flags, ImGuiTableFlags_Resizable);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_ScrollX", &flags, ImGuiTableFlags_ScrollX);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_ScrollY", &flags, ImGuiTableFlags_ScrollY);
            //        Imgui.SetNextItemWidth(Imgui.GetFrameHeight());
            //        Imgui.DragInt("freeze_cols", &freeze_cols, 0.2f, 0, 9, NULL, ImGuiSliderFlags_NoInput);
            //        Imgui.SetNextItemWidth(Imgui.GetFrameHeight());
            //        Imgui.DragInt("freeze_rows", &freeze_rows, 0.2f, 0, 9, NULL, ImGuiSliderFlags_NoInput);
            //        PopStyleCompact();
            //
            //        // When using ScrollX or ScrollY we need to specify a size for our table container!
            //        // Otherwise by default the table will fit all available space, like a BeginChild() call.
            //        ImVec2 outer_size = ImVec2(0.0f, TEXT_BASE_HEIGHT * 8);
            //        if (Imgui.BeginTable("table_scrollx", 7, flags, outer_size))
            //        {
            //            Imgui.TableSetupScrollFreeze(freeze_cols, freeze_rows);
            //            Imgui.TableSetupColumn("Line #", ImGuiTableColumnFlags_NoHide); // Make the first column not hideable to match our use of TableSetupScrollFreeze()
            //            Imgui.TableSetupColumn("One");
            //            Imgui.TableSetupColumn("Two");
            //            Imgui.TableSetupColumn("Three");
            //            Imgui.TableSetupColumn("Four");
            //            Imgui.TableSetupColumn("Five");
            //            Imgui.TableSetupColumn("Six");
            //            Imgui.TableHeadersRow();
            //            for (int row = 0; row < 20; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 7; column++)
            //                {
            //                    // Both TableNextColumn() and TableSetColumnIndex() return true when a column is visible or performing width measurement.
            //                    // Because here we know that:
            //                    // - A) all our columns are contributing the same to row height
            //                    // - B) column 0 is always visible,
            //                    // We only always submit this one column and can skip others.
            //                    // More advanced per-column clipping behaviors may benefit from polling the status flags via TableGetColumnFlags().
            //                    if (!Imgui.TableSetColumnIndex(column) && column > 0)
            //                        continue;
            //                    if (column == 0)
            //                        Imgui.Text("Line %d", row);
            //                    else
            //                        Imgui.Text("Hello world %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        Imgui.Spacing();
            //        Imgui.TextUnformatted("Stretch + ScrollX");
            //        Imgui.SameLine();
            //        HelpMarker(
            //            "Showcase using Stretch columns + ScrollX together: "
            //            "this is rather unusual and only makes sense when specifying an 'inner_width' for the table!\n"
            //            "Without an explicit value, inner_width is == outer_size.x and therefore using Stretch columns + ScrollX together doesn't make sense.");
            //        static ImGuiTableFlags flags2 = ImGuiTableFlags_SizingStretchSame | ImGuiTableFlags_ScrollX | ImGuiTableFlags_ScrollY | ImGuiTableFlags_BordersOuter | ImGuiTableFlags_RowBg | ImGuiTableFlags_ContextMenuInBody;
            //        static float inner_width = 1000.0f;
            //        PushStyleCompact();
            //        Imgui.PushID("flags3");
            //        Imgui.PushItemWidth(TEXT_BASE_WIDTH * 30);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_ScrollX", &flags2, ImGuiTableFlags_ScrollX);
            //        Imgui.DragFloat("inner_width", &inner_width, 1.0f, 0.0f, FLT_MAX, "%.1f");
            //        Imgui.PopItemWidth();
            //        Imgui.PopID();
            //        PopStyleCompact();
            //        if (Imgui.BeginTable("table2", 7, flags2, outer_size, inner_width))
            //        {
            //            for (int cell = 0; cell < 20 * 7; cell++)
            //            {
            //                Imgui.TableNextColumn();
            //                Imgui.Text("Hello world %d,%d", Imgui.TableGetColumnIndex(), Imgui.TableGetRowIndex());
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Columns flags");
            //    if (Imgui.TreeNode("Columns flags"))
            //    {
            //        // Create a first table just to show all the options/flags we want to make visible in our example!
            //        const int column_count = 3;
            //        const char* column_names[column_count] = { "One", "Two", "Three" };
            //        static ImGuiTableColumnFlags column_flags[column_count] = { ImGuiTableColumnFlags_DefaultSort, ImGuiTableColumnFlags_None, ImGuiTableColumnFlags_DefaultHide };
            //        static ImGuiTableColumnFlags column_flags_out[column_count] = { 0, 0, 0 }; // Output from TableGetColumnFlags()
            //
            //        if (Imgui.BeginTable("table_columns_flags_checkboxes", column_count, ImGuiTableFlags_None))
            //        {
            //            PushStyleCompact();
            //            for (int column = 0; column < column_count; column++)
            //            {
            //                Imgui.TableNextColumn();
            //                Imgui.PushID(column);
            //                Imgui.AlignTextToFramePadding(); // FIXME-TABLE: Workaround for wrong text baseline propagation across columns
            //                Imgui.Text("'%s'", column_names[column]);
            //                Imgui.Spacing();
            //                Imgui.Text("Input flags:");
            //                EditTableColumnsFlags(&column_flags[column]);
            //                Imgui.Spacing();
            //                Imgui.Text("Output flags:");
            //                Imgui.BeginDisabled();
            //                ShowTableColumnsStatusFlags(column_flags_out[column]);
            //                Imgui.EndDisabled();
            //                Imgui.PopID();
            //            }
            //            PopStyleCompact();
            //            Imgui.EndTable();
            //        }
            //
            //        // Create the real table we care about for the example!
            //        // We use a scrolling table to be able to showcase the difference between the _IsEnabled and _IsVisible flags above, otherwise in
            //        // a non-scrolling table columns are always visible (unless using ImGuiTableFlags_NoKeepColumnsVisible + resizing the parent window down)
            //        const ImGuiTableFlags flags
            //            = ImGuiTableFlags_SizingFixedFit | ImGuiTableFlags_ScrollX | ImGuiTableFlags_ScrollY
            //            | ImGuiTableFlags_RowBg | ImGuiTableFlags_BordersOuter | ImGuiTableFlags_BordersV
            //            | ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable | ImGuiTableFlags_Sortable;
            //        ImVec2 outer_size = ImVec2(0.0f, TEXT_BASE_HEIGHT * 9);
            //        if (Imgui.BeginTable("table_columns_flags", column_count, flags, outer_size))
            //        {
            //            for (int column = 0; column < column_count; column++)
            //                Imgui.TableSetupColumn(column_names[column], column_flags[column]);
            //            Imgui.TableHeadersRow();
            //            for (int column = 0; column < column_count; column++)
            //                column_flags_out[column] = Imgui.TableGetColumnFlags(column);
            //            float indent_step = (float)((int)TEXT_BASE_WIDTH / 2);
            //            for (int row = 0; row < 8; row++)
            //            {
            //                Imgui.Indent(indent_step); // Add some indentation to demonstrate usage of per-column IndentEnable/IndentDisable flags.
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < column_count; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("%s %s", (column == 0) ? "Indented" : "Hello", Imgui.TableGetColumnName(column));
            //                }
            //            }
            //            Imgui.Unindent(indent_step * 8.0f);
            //
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Columns widths");
            //    if (Imgui.TreeNode("Columns widths"))
            //    {
            //        HelpMarker("Using TableSetupColumn() to setup default width.");
            //
            //        static ImGuiTableFlags flags1 = ImGuiTableFlags_Borders | ImGuiTableFlags_NoBordersInBodyUntilResize;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Resizable", &flags1, ImGuiTableFlags_Resizable);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoBordersInBodyUntilResize", &flags1, ImGuiTableFlags_NoBordersInBodyUntilResize);
            //        PopStyleCompact();
            //        if (Imgui.BeginTable("table1", 3, flags1))
            //        {
            //            // We could also set ImGuiTableFlags_SizingFixedFit on the table and all columns will default to ImGuiTableColumnFlags_WidthFixed.
            //            Imgui.TableSetupColumn("one", ImGuiTableColumnFlags_WidthFixed, 100.0f); // Default to 100.0f
            //            Imgui.TableSetupColumn("two", ImGuiTableColumnFlags_WidthFixed, 200.0f); // Default to 200.0f
            //            Imgui.TableSetupColumn("three", ImGuiTableColumnFlags_WidthFixed);       // Default to auto
            //            Imgui.TableHeadersRow();
            //            for (int row = 0; row < 4; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    if (row == 0)
            //                        Imgui.Text("(w: %5.1f)", Imgui.GetContentRegionAvail().x);
            //                    else
            //                        Imgui.Text("Hello %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        HelpMarker("Using TableSetupColumn() to setup explicit width.\n\nUnless _NoKeepColumnsVisible is set, fixed columns with set width may still be shrunk down if there's not enough space in the host.");
            //
            //        static ImGuiTableFlags flags2 = ImGuiTableFlags_None;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoKeepColumnsVisible", &flags2, ImGuiTableFlags_NoKeepColumnsVisible);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersInnerV", &flags2, ImGuiTableFlags_BordersInnerV);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_BordersOuterV", &flags2, ImGuiTableFlags_BordersOuterV);
            //        PopStyleCompact();
            //        if (Imgui.BeginTable("table2", 4, flags2))
            //        {
            //            // We could also set ImGuiTableFlags_SizingFixedFit on the table and all columns will default to ImGuiTableColumnFlags_WidthFixed.
            //            Imgui.TableSetupColumn("", ImGuiTableColumnFlags_WidthFixed, 100.0f);
            //            Imgui.TableSetupColumn("", ImGuiTableColumnFlags_WidthFixed, TEXT_BASE_WIDTH * 15.0f);
            //            Imgui.TableSetupColumn("", ImGuiTableColumnFlags_WidthFixed, TEXT_BASE_WIDTH * 30.0f);
            //            Imgui.TableSetupColumn("", ImGuiTableColumnFlags_WidthFixed, TEXT_BASE_WIDTH * 15.0f);
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 4; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    if (row == 0)
            //                        Imgui.Text("(w: %5.1f)", Imgui.GetContentRegionAvail().x);
            //                    else
            //                        Imgui.Text("Hello %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Nested tables");
            //    if (Imgui.TreeNode("Nested tables"))
            //    {
            //        HelpMarker("This demonstrates embedding a table into another table cell.");
            //
            //        if (Imgui.BeginTable("table_nested1", 2, ImGuiTableFlags_Borders | ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable))
            //        {
            //            Imgui.TableSetupColumn("A0");
            //            Imgui.TableSetupColumn("A1");
            //            Imgui.TableHeadersRow();
            //
            //            Imgui.TableNextColumn();
            //            Imgui.Text("A0 Row 0");
            //            {
            //                float rows_height = TEXT_BASE_HEIGHT * 2;
            //                if (Imgui.BeginTable("table_nested2", 2, ImGuiTableFlags_Borders | ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable))
            //                {
            //                    Imgui.TableSetupColumn("B0");
            //                    Imgui.TableSetupColumn("B1");
            //                    Imgui.TableHeadersRow();
            //
            //                    Imgui.TableNextRow(ImGuiTableRowFlags_None, rows_height);
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("B0 Row 0");
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("B1 Row 0");
            //                    Imgui.TableNextRow(ImGuiTableRowFlags_None, rows_height);
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("B0 Row 1");
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("B1 Row 1");
            //
            //                    Imgui.EndTable();
            //                }
            //            }
            //            Imgui.TableNextColumn(); Imgui.Text("A1 Row 0");
            //            Imgui.TableNextColumn(); Imgui.Text("A0 Row 1");
            //            Imgui.TableNextColumn(); Imgui.Text("A1 Row 1");
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Row height");
            //    if (Imgui.TreeNode("Row height"))
            //    {
            //        HelpMarker("You can pass a 'min_row_height' to TableNextRow().\n\nRows are padded with 'style.CellPadding.y' on top and bottom, so effectively the minimum row height will always be >= 'style.CellPadding.y * 2.0f'.\n\nWe cannot honor a _maximum_ row height as that would require a unique clipping rectangle per row.");
            //        if (Imgui.BeginTable("table_row_height", 1, ImGuiTableFlags_BordersOuter | ImGuiTableFlags_BordersInnerV))
            //        {
            //            for (int row = 0; row < 10; row++)
            //            {
            //                float min_row_height = (float)(int)(TEXT_BASE_HEIGHT * 0.30f * row);
            //                Imgui.TableNextRow(ImGuiTableRowFlags_None, min_row_height);
            //                Imgui.TableNextColumn();
            //                Imgui.Text("min_row_height = %.2f", min_row_height);
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Outer size");
            //    if (Imgui.TreeNode("Outer size"))
            //    {
            //        // Showcasing use of ImGuiTableFlags_NoHostExtendX and ImGuiTableFlags_NoHostExtendY
            //        // Important to that note how the two flags have slightly different behaviors!
            //        Imgui.Text("Using NoHostExtendX and NoHostExtendY:");
            //        PushStyleCompact();
            //        static ImGuiTableFlags flags = ImGuiTableFlags_Borders | ImGuiTableFlags_Resizable | ImGuiTableFlags_ContextMenuInBody | ImGuiTableFlags_RowBg | ImGuiTableFlags_SizingFixedFit | ImGuiTableFlags_NoHostExtendX;
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoHostExtendX", &flags, ImGuiTableFlags_NoHostExtendX);
            //        Imgui.SameLine(); HelpMarker("Make outer width auto-fit to columns, overriding outer_size.x value.\n\nOnly available when ScrollX/ScrollY are disabled and Stretch columns are not used.");
            //        Imgui.CheckboxFlags("ImGuiTableFlags_NoHostExtendY", &flags, ImGuiTableFlags_NoHostExtendY);
            //        Imgui.SameLine(); HelpMarker("Make outer height stop exactly at outer_size.y (prevent auto-extending table past the limit).\n\nOnly available when ScrollX/ScrollY are disabled. Data below the limit will be clipped and not visible.");
            //        PopStyleCompact();
            //
            //        ImVec2 outer_size = ImVec2(0.0f, TEXT_BASE_HEIGHT * 5.5f);
            //        if (Imgui.BeginTable("table1", 3, flags, outer_size))
            //        {
            //            for (int row = 0; row < 10; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("Cell %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.SameLine();
            //        Imgui.Text("Hello!");
            //
            //        Imgui.Spacing();
            //
            //        Imgui.Text("Using explicit size:");
            //        if (Imgui.BeginTable("table2", 3, ImGuiTableFlags_Borders | ImGuiTableFlags_RowBg, ImVec2(TEXT_BASE_WIDTH * 30, 0.0f)))
            //        {
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("Cell %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.SameLine();
            //        if (Imgui.BeginTable("table3", 3, ImGuiTableFlags_Borders | ImGuiTableFlags_RowBg, ImVec2(TEXT_BASE_WIDTH * 30, 0.0f)))
            //        {
            //            for (int row = 0; row < 3; row++)
            //            {
            //                Imgui.TableNextRow(0, TEXT_BASE_HEIGHT * 1.5f);
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("Cell %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Background color");
            //    if (Imgui.TreeNode("Background color"))
            //    {
            //        static ImGuiTableFlags flags = ImGuiTableFlags_RowBg;
            //        static int row_bg_type = 1;
            //        static int row_bg_target = 1;
            //        static int cell_bg_type = 1;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_Borders", &flags, ImGuiTableFlags_Borders);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_RowBg", &flags, ImGuiTableFlags_RowBg);
            //        Imgui.SameLine(); HelpMarker("ImGuiTableFlags_RowBg automatically sets RowBg0 to alternative colors pulled from the Style.");
            //        Imgui.Combo("row bg type", (int*)&row_bg_type, "None\0Red\0Gradient\0");
            //        Imgui.Combo("row bg target", (int*)&row_bg_target, "RowBg0\0RowBg1\0"); Imgui.SameLine(); HelpMarker("Target RowBg0 to override the alternating odd/even colors,\nTarget RowBg1 to blend with them.");
            //        Imgui.Combo("cell bg type", (int*)&cell_bg_type, "None\0Blue\0"); Imgui.SameLine(); HelpMarker("We are colorizing cells to B1->C2 here.");
            //        IM_ASSERT(row_bg_type >= 0 && row_bg_type <= 2);
            //        IM_ASSERT(row_bg_target >= 0 && row_bg_target <= 1);
            //        IM_ASSERT(cell_bg_type >= 0 && cell_bg_type <= 1);
            //        PopStyleCompact();
            //
            //        if (Imgui.BeginTable("table1", 5, flags))
            //        {
            //            for (int row = 0; row < 6; row++)
            //            {
            //                Imgui.TableNextRow();
            //
            //                // Demonstrate setting a row background color with 'Imgui.TableSetBgColor(ImGuiTableBgTarget_RowBgX, ...)'
            //                // We use a transparent color so we can see the one behind in case our target is RowBg1 and RowBg0 was already targeted by the ImGuiTableFlags_RowBg flag.
            //                if (row_bg_type != 0)
            //                {
            //                    ImU32 row_bg_color = Imgui.GetColorU32(row_bg_type == 1 ? ImVec4(0.7f, 0.3f, 0.3f, 0.65f) : ImVec4(0.2f + row * 0.1f, 0.2f, 0.2f, 0.65f)); // Flat or Gradient?
            //                    Imgui.TableSetBgColor(ImGuiTableBgTarget_RowBg0 + row_bg_target, row_bg_color);
            //                }
            //
            //                // Fill cells
            //                for (int column = 0; column < 5; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("%c%c", 'A' + row, '0' + column);
            //
            //                    // Change background of Cells B1->C2
            //                    // Demonstrate setting a cell background color with 'Imgui.TableSetBgColor(ImGuiTableBgTarget_CellBg, ...)'
            //                    // (the CellBg color will be blended over the RowBg and ColumnBg colors)
            //                    // We can also pass a column number as a third parameter to TableSetBgColor() and do this outside the column loop.
            //                    if (row >= 1 && row <= 2 && column >= 1 && column <= 2 && cell_bg_type == 1)
            //                    {
            //                        ImU32 cell_bg_color = Imgui.GetColorU32(ImVec4(0.3f, 0.3f, 0.7f, 0.65f));
            //                        Imgui.TableSetBgColor(ImGuiTableBgTarget_CellBg, cell_bg_color);
            //                    }
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Tree view");
            //    if (Imgui.TreeNode("Tree view"))
            //    {
            //        static ImGuiTableFlags flags = ImGuiTableFlags_BordersV | ImGuiTableFlags_BordersOuterH | ImGuiTableFlags_Resizable | ImGuiTableFlags_RowBg | ImGuiTableFlags_NoBordersInBody;
            //
            //        if (Imgui.BeginTable("3ways", 3, flags))
            //        {
            //            // The first column will use the default _WidthStretch when ScrollX is Off and _WidthFixed when ScrollX is On
            //            Imgui.TableSetupColumn("Name", ImGuiTableColumnFlags_NoHide);
            //            Imgui.TableSetupColumn("Size", ImGuiTableColumnFlags_WidthFixed, TEXT_BASE_WIDTH * 12.0f);
            //            Imgui.TableSetupColumn("Type", ImGuiTableColumnFlags_WidthFixed, TEXT_BASE_WIDTH * 18.0f);
            //            Imgui.TableHeadersRow();
            //
            //            // Simple storage to output a dummy file-system.
            //            struct MyTreeNode
            //            {
            //                const char*     Name;
            //                const char*     Type;
            //                int             Size;
            //                int             ChildIdx;
            //                int             ChildCount;
            //                static void DisplayNode(const MyTreeNode* node, const MyTreeNode* all_nodes)
            //                {
            //                    Imgui.TableNextRow();
            //                    Imgui.TableNextColumn();
            //                    const bool is_folder = (node->ChildCount > 0);
            //                    if (is_folder)
            //                    {
            //                        bool open = Imgui.TreeNodeEx(node->Name, ImGuiTreeNodeFlags_SpanFullWidth);
            //                        Imgui.TableNextColumn();
            //                        Imgui.TextDisabled("--");
            //                        Imgui.TableNextColumn();
            //                        Imgui.TextUnformatted(node->Type);
            //                        if (open)
            //                        {
            //                            for (int child_n = 0; child_n < node->ChildCount; child_n++)
            //                                DisplayNode(&all_nodes[node->ChildIdx + child_n], all_nodes);
            //                            Imgui.TreePop();
            //                        }
            //                    }
            //                    else
            //                    {
            //                        Imgui.TreeNodeEx(node->Name, ImGuiTreeNodeFlags_Leaf | ImGuiTreeNodeFlags_Bullet | ImGuiTreeNodeFlags_NoTreePushOnOpen | ImGuiTreeNodeFlags_SpanFullWidth);
            //                        Imgui.TableNextColumn();
            //                        Imgui.Text("%d", node->Size);
            //                        Imgui.TableNextColumn();
            //                        Imgui.TextUnformatted(node->Type);
            //                    }
            //                }
            //            };
            //            static const MyTreeNode nodes[] =
            //            {
            //                { "Root",                         "Folder",       -1,       1, 3    }, // 0
            //                { "Music",                        "Folder",       -1,       4, 2    }, // 1
            //                { "Textures",                     "Folder",       -1,       6, 3    }, // 2
            //                { "desktop.ini",                  "System file",  1024,    -1,-1    }, // 3
            //                { "File1_a.wav",                  "Audio file",   123000,  -1,-1    }, // 4
            //                { "File1_b.wav",                  "Audio file",   456000,  -1,-1    }, // 5
            //                { "Image001.png",                 "Image file",   203128,  -1,-1    }, // 6
            //                { "Copy of Image001.png",         "Image file",   203256,  -1,-1    }, // 7
            //                { "Copy of Image001 (Final2).png","Image file",   203512,  -1,-1    }, // 8
            //            };
            //
            //            MyTreeNode::DisplayNode(&nodes[0], nodes);
            //
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Item width");
            //    if (Imgui.TreeNode("Item width"))
            //    {
            //        HelpMarker(
            //            "Showcase using PushItemWidth() and how it is preserved on a per-column basis.\n\n"
            //            "Note that on auto-resizing non-resizable fixed columns, querying the content width for e.g. right-alignment doesn't make sense.");
            //        if (Imgui.BeginTable("table_item_width", 3, ImGuiTableFlags_Borders))
            //        {
            //            Imgui.TableSetupColumn("small");
            //            Imgui.TableSetupColumn("half");
            //            Imgui.TableSetupColumn("right-align");
            //            Imgui.TableHeadersRow();
            //
            //            for (int row = 0; row < 3; row++)
            //            {
            //                Imgui.TableNextRow();
            //                if (row == 0)
            //                {
            //                    // Setup ItemWidth once (instead of setting up every time, which is also possible but less efficient)
            //                    Imgui.TableSetColumnIndex(0);
            //                    Imgui.PushItemWidth(TEXT_BASE_WIDTH * 3.0f); // Small
            //                    Imgui.TableSetColumnIndex(1);
            //                    Imgui.PushItemWidth(-Imgui.GetContentRegionAvail().x * 0.5f);
            //                    Imgui.TableSetColumnIndex(2);
            //                    Imgui.PushItemWidth(-FLT_MIN); // Right-aligned
            //                }
            //
            //                // Draw our contents
            //                static float dummy_f = 0.0f;
            //                Imgui.PushID(row);
            //                Imgui.TableSetColumnIndex(0);
            //                Imgui.SliderFloat("float0", &dummy_f, 0.0f, 1.0f);
            //                Imgui.TableSetColumnIndex(1);
            //                Imgui.SliderFloat("float1", &dummy_f, 0.0f, 1.0f);
            //                Imgui.TableSetColumnIndex(2);
            //                Imgui.SliderFloat("##float2", &dummy_f, 0.0f, 1.0f); // No visible label since right-aligned
            //                Imgui.PopID();
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    // Demonstrate using TableHeader() calls instead of TableHeadersRow()
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Custom headers");
            //    if (Imgui.TreeNode("Custom headers"))
            //    {
            //        const int COLUMNS_COUNT = 3;
            //        if (Imgui.BeginTable("table_custom_headers", COLUMNS_COUNT, ImGuiTableFlags_Borders | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable))
            //        {
            //            Imgui.TableSetupColumn("Apricot");
            //            Imgui.TableSetupColumn("Banana");
            //            Imgui.TableSetupColumn("Cherry");
            //
            //            // Dummy entire-column selection storage
            //            // FIXME: It would be nice to actually demonstrate full-featured selection using those checkbox.
            //            static bool column_selected[3] = {};
            //
            //            // Instead of calling TableHeadersRow() we'll submit custom headers ourselves
            //            Imgui.TableNextRow(ImGuiTableRowFlags_Headers);
            //            for (int column = 0; column < COLUMNS_COUNT; column++)
            //            {
            //                Imgui.TableSetColumnIndex(column);
            //                const char* column_name = Imgui.TableGetColumnName(column); // Retrieve name passed to TableSetupColumn()
            //                Imgui.PushID(column);
            //                Imgui.PushStyleVar(ImGuiStyleVar_FramePadding, ImVec2(0, 0));
            //                Imgui.Checkbox("##checkall", &column_selected[column]);
            //                Imgui.PopStyleVar();
            //                Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.x);
            //                Imgui.TableHeader(column_name);
            //                Imgui.PopID();
            //            }
            //
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    char buf[32];
            //                    sprintf(buf, "Cell %d,%d", column, row);
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Selectable(buf, column_selected[column]);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    // Demonstrate creating custom context menus inside columns, while playing it nice with context menus provided by TableHeadersRow()/TableHeader()
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Context menus");
            //    if (Imgui.TreeNode("Context menus"))
            //    {
            //        HelpMarker("By default, right-clicking over a TableHeadersRow()/TableHeader() line will open the default context-menu.\nUsing ImGuiTableFlags_ContextMenuInBody we also allow right-clicking over columns body.");
            //        static ImGuiTableFlags flags1 = ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable | ImGuiTableFlags_Borders | ImGuiTableFlags_ContextMenuInBody;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_ContextMenuInBody", &flags1, ImGuiTableFlags_ContextMenuInBody);
            //        PopStyleCompact();
            //
            //        // Context Menus: first example
            //        // [1.1] Right-click on the TableHeadersRow() line to open the default table context menu.
            //        // [1.2] Right-click in columns also open the default table context menu (if ImGuiTableFlags_ContextMenuInBody is set)
            //        const int COLUMNS_COUNT = 3;
            //        if (Imgui.BeginTable("table_context_menu", COLUMNS_COUNT, flags1))
            //        {
            //            Imgui.TableSetupColumn("One");
            //            Imgui.TableSetupColumn("Two");
            //            Imgui.TableSetupColumn("Three");
            //
            //            // [1.1]] Right-click on the TableHeadersRow() line to open the default table context menu.
            //            Imgui.TableHeadersRow();
            //
            //            // Submit dummy contents
            //            for (int row = 0; row < 4; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < COLUMNS_COUNT; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("Cell %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        // Context Menus: second example
            //        // [2.1] Right-click on the TableHeadersRow() line to open the default table context menu.
            //        // [2.2] Right-click on the ".." to open a custom popup
            //        // [2.3] Right-click in columns to open another custom popup
            //        HelpMarker("Demonstrate mixing table context menu (over header), item context button (over button) and custom per-colum context menu (over column body).");
            //        ImGuiTableFlags flags2 = ImGuiTableFlags_Resizable | ImGuiTableFlags_SizingFixedFit | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable | ImGuiTableFlags_Borders;
            //        if (Imgui.BeginTable("table_context_menu_2", COLUMNS_COUNT, flags2))
            //        {
            //            Imgui.TableSetupColumn("One");
            //            Imgui.TableSetupColumn("Two");
            //            Imgui.TableSetupColumn("Three");
            //
            //            // [2.1] Right-click on the TableHeadersRow() line to open the default table context menu.
            //            Imgui.TableHeadersRow();
            //            for (int row = 0; row < 4; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < COLUMNS_COUNT; column++)
            //                {
            //                    // Submit dummy contents
            //                    Imgui.TableSetColumnIndex(column);
            //                    Imgui.Text("Cell %d,%d", column, row);
            //                    Imgui.SameLine();
            //
            //                    // [2.2] Right-click on the ".." to open a custom popup
            //                    Imgui.PushID(row * COLUMNS_COUNT + column);
            //                    Imgui.SmallButton("..");
            //                    if (Imgui.BeginPopupContextItem())
            //                    {
            //                        Imgui.Text("This is the popup for Button(\"..\") in Cell %d,%d", column, row);
            //                        if (Imgui.Button("Close"))
            //                            Imgui.CloseCurrentPopup();
            //                        Imgui.EndPopup();
            //                    }
            //                    Imgui.PopID();
            //                }
            //            }
            //
            //            // [2.3] Right-click anywhere in columns to open another custom popup
            //            // (instead of testing for !IsAnyItemHovered() we could also call OpenPopup() with ImGuiPopupFlags_NoOpenOverExistingPopup
            //            // to manage popup priority as the popups triggers, here "are we hovering a column" are overlapping)
            //            int hovered_column = -1;
            //            for (int column = 0; column < COLUMNS_COUNT + 1; column++)
            //            {
            //                Imgui.PushID(column);
            //                if (Imgui.TableGetColumnFlags(column) & ImGuiTableColumnFlags_IsHovered)
            //                    hovered_column = column;
            //                if (hovered_column == column && !Imgui.IsAnyItemHovered() && Imgui.IsMouseReleased(1))
            //                    Imgui.OpenPopup("MyPopup");
            //                if (Imgui.BeginPopup("MyPopup"))
            //                {
            //                    if (column == COLUMNS_COUNT)
            //                        Imgui.Text("This is a custom popup for unused space after the last column.");
            //                    else
            //                        Imgui.Text("This is a custom popup for Column %d", column);
            //                    if (Imgui.Button("Close"))
            //                        Imgui.CloseCurrentPopup();
            //                    Imgui.EndPopup();
            //                }
            //                Imgui.PopID();
            //            }
            //
            //            Imgui.EndTable();
            //            Imgui.Text("Hovered column: %d", hovered_column);
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    // Demonstrate creating multiple tables with the same ID
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Synced instances");
            //    if (Imgui.TreeNode("Synced instances"))
            //    {
            //        HelpMarker("Multiple tables with the same identifier will share their settings, width, visibility, order etc.");
            //
            //        static ImGuiTableFlags flags = ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable | ImGuiTableFlags_Borders | ImGuiTableFlags_SizingFixedFit | ImGuiTableFlags_NoSavedSettings;
            //        Imgui.CheckboxFlags("ImGuiTableFlags_ScrollY", &flags, ImGuiTableFlags_ScrollY);
            //        Imgui.CheckboxFlags("ImGuiTableFlags_SizingFixedFit", &flags, ImGuiTableFlags_SizingFixedFit);
            //        for (int n = 0; n < 3; n++)
            //        {
            //            char buf[32];
            //            sprintf(buf, "Synced Table %d", n);
            //            bool open = Imgui.CollapsingHeader(buf, ImGuiTreeNodeFlags_DefaultOpen);
            //            if (open && Imgui.BeginTable("Table", 3, flags, ImVec2(0.0f, Imgui.GetTextLineHeightWithSpacing() * 5)))
            //            {
            //                Imgui.TableSetupColumn("One");
            //                Imgui.TableSetupColumn("Two");
            //                Imgui.TableSetupColumn("Three");
            //                Imgui.TableHeadersRow();
            //                const int cell_count = (n == 1) ? 27 : 9; // Make second table have a scrollbar to verify that additional decoration is not affecting column positions.
            //                for (int cell = 0; cell < cell_count; cell++)
            //                {
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("this cell %d", cell);
            //                }
            //                Imgui.EndTable();
            //            }
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    // Demonstrate using Sorting facilities
            //    // This is a simplified version of the "Advanced" example, where we mostly focus on the code necessary to handle sorting.
            //    // Note that the "Advanced" example also showcase manually triggering a sort (e.g. if item quantities have been modified)
            //    static const char* template_items_names[] =
            //    {
            //        "Banana", "Apple", "Cherry", "Watermelon", "Grapefruit", "Strawberry", "Mango",
            //        "Kiwi", "Orange", "Pineapple", "Blueberry", "Plum", "Coconut", "Pear", "Apricot"
            //    };
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Sorting");
            //    if (Imgui.TreeNode("Sorting"))
            //    {
            //        // Create item list
            //        static ImVector<MyItem> items;
            //        if (items.Size == 0)
            //        {
            //            items.resize(50, MyItem());
            //            for (int n = 0; n < items.Size; n++)
            //            {
            //                const int template_n = n % IM_ARRAYSIZE(template_items_names);
            //                MyItem& item = items[n];
            //                item.ID = n;
            //                item.Name = template_items_names[template_n];
            //                item.Quantity = (n * n - n) % 20; // Assign default quantities
            //            }
            //        }
            //
            //        // Options
            //        static ImGuiTableFlags flags =
            //            ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable | ImGuiTableFlags_Sortable | ImGuiTableFlags_SortMulti
            //            | ImGuiTableFlags_RowBg | ImGuiTableFlags_BordersOuter | ImGuiTableFlags_BordersV | ImGuiTableFlags_NoBordersInBody
            //            | ImGuiTableFlags_ScrollY;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("ImGuiTableFlags_SortMulti", &flags, ImGuiTableFlags_SortMulti);
            //        Imgui.SameLine(); HelpMarker("When sorting is enabled: hold shift when clicking headers to sort on multiple column. TableGetSortSpecs() may return specs where (SpecsCount > 1).");
            //        Imgui.CheckboxFlags("ImGuiTableFlags_SortTristate", &flags, ImGuiTableFlags_SortTristate);
            //        Imgui.SameLine(); HelpMarker("When sorting is enabled: allow no sorting, disable default sorting. TableGetSortSpecs() may return specs where (SpecsCount == 0).");
            //        PopStyleCompact();
            //
            //        if (Imgui.BeginTable("table_sorting", 4, flags, ImVec2(0.0f, TEXT_BASE_HEIGHT * 15), 0.0f))
            //        {
            //            // Declare columns
            //            // We use the "user_id" parameter of TableSetupColumn() to specify a user id that will be stored in the sort specifications.
            //            // This is so our sort function can identify a column given our own identifier. We could also identify them based on their index!
            //            // Demonstrate using a mixture of flags among available sort-related flags:
            //            // - ImGuiTableColumnFlags_DefaultSort
            //            // - ImGuiTableColumnFlags_NoSort / ImGuiTableColumnFlags_NoSortAscending / ImGuiTableColumnFlags_NoSortDescending
            //            // - ImGuiTableColumnFlags_PreferSortAscending / ImGuiTableColumnFlags_PreferSortDescending
            //            Imgui.TableSetupColumn("ID",       ImGuiTableColumnFlags_DefaultSort          | ImGuiTableColumnFlags_WidthFixed,   0.0f, MyItemColumnID_ID);
            //            Imgui.TableSetupColumn("Name",                                                  ImGuiTableColumnFlags_WidthFixed,   0.0f, MyItemColumnID_Name);
            //            Imgui.TableSetupColumn("Action",   ImGuiTableColumnFlags_NoSort               | ImGuiTableColumnFlags_WidthFixed,   0.0f, MyItemColumnID_Action);
            //            Imgui.TableSetupColumn("Quantity", ImGuiTableColumnFlags_PreferSortDescending | ImGuiTableColumnFlags_WidthStretch, 0.0f, MyItemColumnID_Quantity);
            //            Imgui.TableSetupScrollFreeze(0, 1); // Make row always visible
            //            Imgui.TableHeadersRow();
            //
            //            // Sort our data if sort specs have been changed!
            //            if (ImGuiTableSortSpecs* sorts_specs = Imgui.TableGetSortSpecs())
            //                if (sorts_specs->SpecsDirty)
            //                {
            //                    MyItem::s_current_sort_specs = sorts_specs; // Store in variable accessible by the sort function.
            //                    if (items.Size > 1)
            //                        qsort(&items[0], (size_t)items.Size, sizeof(items[0]), MyItem::CompareWithSortSpecs);
            //                    MyItem::s_current_sort_specs = NULL;
            //                    sorts_specs->SpecsDirty = false;
            //                }
            //
            //            // Demonstrate using clipper for large vertical lists
            //            ImGuiListClipper clipper;
            //            clipper.Begin(items.Size);
            //            while (clipper.Step())
            //                for (int row_n = clipper.DisplayStart; row_n < clipper.DisplayEnd; row_n++)
            //                {
            //                    // Display a data item
            //                    MyItem* item = &items[row_n];
            //                    Imgui.PushID(item->ID);
            //                    Imgui.TableNextRow();
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("%04d", item->ID);
            //                    Imgui.TableNextColumn();
            //                    Imgui.TextUnformatted(item->Name);
            //                    Imgui.TableNextColumn();
            //                    Imgui.SmallButton("None");
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("%d", item->Quantity);
            //                    Imgui.PopID();
            //                }
            //            Imgui.EndTable();
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    // In this example we'll expose most table flags and settings.
            //    // For specific flags and settings refer to the corresponding section for more detailed explanation.
            //    // This section is mostly useful to experiment with combining certain flags or settings with each others.
            //    //Imgui.SetNextItemOpen(true, ImGuiCond_Once); // [DEBUG]
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Advanced");
            //    if (Imgui.TreeNode("Advanced"))
            //    {
            //        static ImGuiTableFlags flags =
            //            ImGuiTableFlags_Resizable | ImGuiTableFlags_Reorderable | ImGuiTableFlags_Hideable
            //            | ImGuiTableFlags_Sortable | ImGuiTableFlags_SortMulti
            //            | ImGuiTableFlags_RowBg | ImGuiTableFlags_Borders | ImGuiTableFlags_NoBordersInBody
            //            | ImGuiTableFlags_ScrollX | ImGuiTableFlags_ScrollY
            //            | ImGuiTableFlags_SizingFixedFit;
            //
            //        enum ContentsType { CT_Text, CT_Button, CT_SmallButton, CT_FillButton, CT_Selectable, CT_SelectableSpanRow };
            //        static int contents_type = CT_SelectableSpanRow;
            //        const char* contents_type_names[] = { "Text", "Button", "SmallButton", "FillButton", "Selectable", "Selectable (span row)" };
            //        static int freeze_cols = 1;
            //        static int freeze_rows = 1;
            //        static int items_count = IM_ARRAYSIZE(template_items_names) * 2;
            //        static ImVec2 outer_size_value = ImVec2(0.0f, TEXT_BASE_HEIGHT * 12);
            //        static float row_min_height = 0.0f; // Auto
            //        static float inner_width_with_scroll = 0.0f; // Auto-extend
            //        static bool outer_size_enabled = true;
            //        static bool show_headers = true;
            //        static bool show_wrapped_text = false;
            //        //static ImGuiTextFilter filter;
            //        //Imgui.SetNextItemOpen(true, ImGuiCond_Once); // FIXME-TABLE: Enabling this results in initial clipped first pass on table which tend to affect column sizing
            //        if (Imgui.TreeNode("Options"))
            //        {
            //            // Make the UI compact because there are so many fields
            //            PushStyleCompact();
            //            Imgui.PushItemWidth(TEXT_BASE_WIDTH * 28.0f);
            //
            //            if (Imgui.TreeNodeEx("Features:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.CheckboxFlags("ImGuiTableFlags_Resizable", &flags, ImGuiTableFlags_Resizable);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_Reorderable", &flags, ImGuiTableFlags_Reorderable);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_Hideable", &flags, ImGuiTableFlags_Hideable);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_Sortable", &flags, ImGuiTableFlags_Sortable);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_NoSavedSettings", &flags, ImGuiTableFlags_NoSavedSettings);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_ContextMenuInBody", &flags, ImGuiTableFlags_ContextMenuInBody);
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Decorations:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.CheckboxFlags("ImGuiTableFlags_RowBg", &flags, ImGuiTableFlags_RowBg);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_BordersV", &flags, ImGuiTableFlags_BordersV);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_BordersOuterV", &flags, ImGuiTableFlags_BordersOuterV);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_BordersInnerV", &flags, ImGuiTableFlags_BordersInnerV);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_BordersH", &flags, ImGuiTableFlags_BordersH);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_BordersOuterH", &flags, ImGuiTableFlags_BordersOuterH);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_BordersInnerH", &flags, ImGuiTableFlags_BordersInnerH);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_NoBordersInBody", &flags, ImGuiTableFlags_NoBordersInBody); Imgui.SameLine(); HelpMarker("Disable vertical borders in columns Body (borders will always appear in Headers");
            //                Imgui.CheckboxFlags("ImGuiTableFlags_NoBordersInBodyUntilResize", &flags, ImGuiTableFlags_NoBordersInBodyUntilResize); Imgui.SameLine(); HelpMarker("Disable vertical borders in columns Body until hovered for resize (borders will always appear in Headers)");
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Sizing:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                EditTableSizingFlags(&flags);
            //                Imgui.SameLine(); HelpMarker("In the Advanced demo we override the policy of each column so those table-wide settings have less effect that typical.");
            //                Imgui.CheckboxFlags("ImGuiTableFlags_NoHostExtendX", &flags, ImGuiTableFlags_NoHostExtendX);
            //                Imgui.SameLine(); HelpMarker("Make outer width auto-fit to columns, overriding outer_size.x value.\n\nOnly available when ScrollX/ScrollY are disabled and Stretch columns are not used.");
            //                Imgui.CheckboxFlags("ImGuiTableFlags_NoHostExtendY", &flags, ImGuiTableFlags_NoHostExtendY);
            //                Imgui.SameLine(); HelpMarker("Make outer height stop exactly at outer_size.y (prevent auto-extending table past the limit).\n\nOnly available when ScrollX/ScrollY are disabled. Data below the limit will be clipped and not visible.");
            //                Imgui.CheckboxFlags("ImGuiTableFlags_NoKeepColumnsVisible", &flags, ImGuiTableFlags_NoKeepColumnsVisible);
            //                Imgui.SameLine(); HelpMarker("Only available if ScrollX is disabled.");
            //                Imgui.CheckboxFlags("ImGuiTableFlags_PreciseWidths", &flags, ImGuiTableFlags_PreciseWidths);
            //                Imgui.SameLine(); HelpMarker("Disable distributing remainder width to stretched columns (width allocation on a 100-wide table with 3 columns: Without this flag: 33,33,34. With this flag: 33,33,33). With larger number of columns, resizing will appear to be less smooth.");
            //                Imgui.CheckboxFlags("ImGuiTableFlags_NoClip", &flags, ImGuiTableFlags_NoClip);
            //                Imgui.SameLine(); HelpMarker("Disable clipping rectangle for every individual columns (reduce draw command count, items will be able to overflow into other columns). Generally incompatible with ScrollFreeze options.");
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Padding:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.CheckboxFlags("ImGuiTableFlags_PadOuterX", &flags, ImGuiTableFlags_PadOuterX);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_NoPadOuterX", &flags, ImGuiTableFlags_NoPadOuterX);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_NoPadInnerX", &flags, ImGuiTableFlags_NoPadInnerX);
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Scrolling:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.CheckboxFlags("ImGuiTableFlags_ScrollX", &flags, ImGuiTableFlags_ScrollX);
            //                Imgui.SameLine();
            //                Imgui.SetNextItemWidth(Imgui.GetFrameHeight());
            //                Imgui.DragInt("freeze_cols", &freeze_cols, 0.2f, 0, 9, NULL, ImGuiSliderFlags_NoInput);
            //                Imgui.CheckboxFlags("ImGuiTableFlags_ScrollY", &flags, ImGuiTableFlags_ScrollY);
            //                Imgui.SameLine();
            //                Imgui.SetNextItemWidth(Imgui.GetFrameHeight());
            //                Imgui.DragInt("freeze_rows", &freeze_rows, 0.2f, 0, 9, NULL, ImGuiSliderFlags_NoInput);
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Sorting:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.CheckboxFlags("ImGuiTableFlags_SortMulti", &flags, ImGuiTableFlags_SortMulti);
            //                Imgui.SameLine(); HelpMarker("When sorting is enabled: hold shift when clicking headers to sort on multiple column. TableGetSortSpecs() may return specs where (SpecsCount > 1).");
            //                Imgui.CheckboxFlags("ImGuiTableFlags_SortTristate", &flags, ImGuiTableFlags_SortTristate);
            //                Imgui.SameLine(); HelpMarker("When sorting is enabled: allow no sorting, disable default sorting. TableGetSortSpecs() may return specs where (SpecsCount == 0).");
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Other:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.Checkbox("show_headers", &show_headers);
            //                Imgui.Checkbox("show_wrapped_text", &show_wrapped_text);
            //
            //                Imgui.DragFloat2("##OuterSize", &outer_size_value.x);
            //                Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.x);
            //                Imgui.Checkbox("outer_size", &outer_size_enabled);
            //                Imgui.SameLine();
            //                HelpMarker("If scrolling is disabled (ScrollX and ScrollY not set):\n"
            //                    "- The table is output directly in the parent window.\n"
            //                    "- OuterSize.x < 0.0f will right-align the table.\n"
            //                    "- OuterSize.x = 0.0f will narrow fit the table unless there are any Stretch columns.\n"
            //                    "- OuterSize.y then becomes the minimum size for the table, which will extend vertically if there are more rows (unless NoHostExtendY is set).");
            //
            //                // From a user point of view we will tend to use 'inner_width' differently depending on whether our table is embedding scrolling.
            //                // To facilitate toying with this demo we will actually pass 0.0f to the BeginTable() when ScrollX is disabled.
            //                Imgui.DragFloat("inner_width (when ScrollX active)", &inner_width_with_scroll, 1.0f, 0.0f, FLT_MAX);
            //
            //                Imgui.DragFloat("row_min_height", &row_min_height, 1.0f, 0.0f, FLT_MAX);
            //                Imgui.SameLine(); HelpMarker("Specify height of the Selectable item.");
            //
            //                Imgui.DragInt("items_count", &items_count, 0.1f, 0, 9999);
            //                Imgui.Combo("items_type (first column)", &contents_type, contents_type_names, IM_ARRAYSIZE(contents_type_names));
            //                //filter.Draw("filter");
            //                Imgui.TreePop();
            //            }
            //
            //            Imgui.PopItemWidth();
            //            PopStyleCompact();
            //            Imgui.Spacing();
            //            Imgui.TreePop();
            //        }
            //
            //        // Update item list if we changed the number of items
            //        static ImVector<MyItem> items;
            //        static ImVector<int> selection;
            //        static bool items_need_sort = false;
            //        if (items.Size != items_count)
            //        {
            //            items.resize(items_count, MyItem());
            //            for (int n = 0; n < items_count; n++)
            //            {
            //                const int template_n = n % IM_ARRAYSIZE(template_items_names);
            //                MyItem& item = items[n];
            //                item.ID = n;
            //                item.Name = template_items_names[template_n];
            //                item.Quantity = (template_n == 3) ? 10 : (template_n == 4) ? 20 : 0; // Assign default quantities
            //            }
            //        }
            //
            //        const ImDrawList* parent_draw_list = Imgui.GetWindowDrawList();
            //        const int parent_draw_list_draw_cmd_count = parent_draw_list->CmdBuffer.Size;
            //        ImVec2 table_scroll_cur, table_scroll_max; // For debug display
            //        const ImDrawList* table_draw_list = NULL;  // "
            //
            //        // Submit table
            //        const float inner_width_to_use = (flags & ImGuiTableFlags_ScrollX) ? inner_width_with_scroll : 0.0f;
            //        if (Imgui.BeginTable("table_advanced", 6, flags, outer_size_enabled ? outer_size_value : ImVec2(0, 0), inner_width_to_use))
            //        {
            //            // Declare columns
            //            // We use the "user_id" parameter of TableSetupColumn() to specify a user id that will be stored in the sort specifications.
            //            // This is so our sort function can identify a column given our own identifier. We could also identify them based on their index!
            //            Imgui.TableSetupColumn("ID",           ImGuiTableColumnFlags_DefaultSort | ImGuiTableColumnFlags_WidthFixed | ImGuiTableColumnFlags_NoHide, 0.0f, MyItemColumnID_ID);
            //            Imgui.TableSetupColumn("Name",         ImGuiTableColumnFlags_WidthFixed, 0.0f, MyItemColumnID_Name);
            //            Imgui.TableSetupColumn("Action",       ImGuiTableColumnFlags_NoSort | ImGuiTableColumnFlags_WidthFixed, 0.0f, MyItemColumnID_Action);
            //            Imgui.TableSetupColumn("Quantity",     ImGuiTableColumnFlags_PreferSortDescending, 0.0f, MyItemColumnID_Quantity);
            //            Imgui.TableSetupColumn("Description",  (flags & ImGuiTableFlags_NoHostExtendX) ? 0 : ImGuiTableColumnFlags_WidthStretch, 0.0f, MyItemColumnID_Description);
            //            Imgui.TableSetupColumn("Hidden",       ImGuiTableColumnFlags_DefaultHide | ImGuiTableColumnFlags_NoSort);
            //            Imgui.TableSetupScrollFreeze(freeze_cols, freeze_rows);
            //
            //            // Sort our data if sort specs have been changed!
            //            ImGuiTableSortSpecs* sorts_specs = Imgui.TableGetSortSpecs();
            //            if (sorts_specs && sorts_specs->SpecsDirty)
            //                items_need_sort = true;
            //            if (sorts_specs && items_need_sort && items.Size > 1)
            //            {
            //                MyItem::s_current_sort_specs = sorts_specs; // Store in variable accessible by the sort function.
            //                qsort(&items[0], (size_t)items.Size, sizeof(items[0]), MyItem::CompareWithSortSpecs);
            //                MyItem::s_current_sort_specs = NULL;
            //                sorts_specs->SpecsDirty = false;
            //            }
            //            items_need_sort = false;
            //
            //            // Take note of whether we are currently sorting based on the Quantity field,
            //            // we will use this to trigger sorting when we know the data of this column has been modified.
            //            const bool sorts_specs_using_quantity = (Imgui.TableGetColumnFlags(3) & ImGuiTableColumnFlags_IsSorted) != 0;
            //
            //            // Show headers
            //            if (show_headers)
            //                Imgui.TableHeadersRow();
            //
            //            // Show data
            //            // FIXME-TABLE FIXME-NAV: How we can get decent up/down even though we have the buttons here?
            //            Imgui.PushButtonRepeat(true);
            //#if 1
            //            // Demonstrate using clipper for large vertical lists
            //            ImGuiListClipper clipper;
            //            clipper.Begin(items.Size);
            //            while (clipper.Step())
            //            {
            //                for (int row_n = clipper.DisplayStart; row_n < clipper.DisplayEnd; row_n++)
            //#else
            //            // Without clipper
            //            {
            //                for (int row_n = 0; row_n < items.Size; row_n++)
            //#endif
            //                {
            //                    MyItem* item = &items[row_n];
            //                    //if (!filter.PassFilter(item->Name))
            //                    //    continue;
            //
            //                    const bool item_is_selected = selection.contains(item->ID);
            //                    Imgui.PushID(item->ID);
            //                    Imgui.TableNextRow(ImGuiTableRowFlags_None, row_min_height);
            //
            //                    // For the demo purpose we can select among different type of items submitted in the first column
            //                    Imgui.TableSetColumnIndex(0);
            //                    char label[32];
            //                    sprintf(label, "%04d", item->ID);
            //                    if (contents_type == CT_Text)
            //                        Imgui.TextUnformatted(label);
            //                    else if (contents_type == CT_Button)
            //                        Imgui.Button(label);
            //                    else if (contents_type == CT_SmallButton)
            //                        Imgui.SmallButton(label);
            //                    else if (contents_type == CT_FillButton)
            //                        Imgui.Button(label, ImVec2(-FLT_MIN, 0.0f));
            //                    else if (contents_type == CT_Selectable || contents_type == CT_SelectableSpanRow)
            //                    {
            //                        ImGuiSelectableFlags selectable_flags = (contents_type == CT_SelectableSpanRow) ? ImGuiSelectableFlags_SpanAllColumns | ImGuiSelectableFlags_AllowItemOverlap : ImGuiSelectableFlags_None;
            //                        if (Imgui.Selectable(label, item_is_selected, selectable_flags, ImVec2(0, row_min_height)))
            //                        {
            //                            if (Imgui.GetIO().KeyCtrl)
            //                            {
            //                                if (item_is_selected)
            //                                    selection.find_erase_unsorted(item->ID);
            //                                else
            //                                    selection.push_back(item->ID);
            //                            }
            //                            else
            //                            {
            //                                selection.clear();
            //                                selection.push_back(item->ID);
            //                            }
            //                        }
            //                    }
            //
            //                    if (Imgui.TableSetColumnIndex(1))
            //                        Imgui.TextUnformatted(item->Name);
            //
            //                    // Here we demonstrate marking our data set as needing to be sorted again if we modified a quantity,
            //                    // and we are currently sorting on the column showing the Quantity.
            //                    // To avoid triggering a sort while holding the button, we only trigger it when the button has been released.
            //                    // You will probably need a more advanced system in your code if you want to automatically sort when a specific entry changes.
            //                    if (Imgui.TableSetColumnIndex(2))
            //                    {
            //                        if (Imgui.SmallButton("Chop")) { item->Quantity += 1; }
            //                        if (sorts_specs_using_quantity && Imgui.IsItemDeactivated()) { items_need_sort = true; }
            //                        Imgui.SameLine();
            //                        if (Imgui.SmallButton("Eat")) { item->Quantity -= 1; }
            //                        if (sorts_specs_using_quantity && Imgui.IsItemDeactivated()) { items_need_sort = true; }
            //                    }
            //
            //                    if (Imgui.TableSetColumnIndex(3))
            //                        Imgui.Text("%d", item->Quantity);
            //
            //                    Imgui.TableSetColumnIndex(4);
            //                    if (show_wrapped_text)
            //                        Imgui.TextWrapped("Lorem ipsum dolor sit amet");
            //                    else
            //                        Imgui.Text("Lorem ipsum dolor sit amet");
            //
            //                    if (Imgui.TableSetColumnIndex(5))
            //                        Imgui.Text("1234");
            //
            //                    Imgui.PopID();
            //                }
            //            }
            //            Imgui.PopButtonRepeat();
            //
            //            // Store some info to display debug details below
            //            table_scroll_cur = ImVec2(Imgui.GetScrollX(), Imgui.GetScrollY());
            //            table_scroll_max = ImVec2(Imgui.GetScrollMaxX(), Imgui.GetScrollMaxY());
            //            table_draw_list = Imgui.GetWindowDrawList();
            //            Imgui.EndTable();
            //        }
            //        static bool show_debug_details = false;
            //        Imgui.Checkbox("Debug details", &show_debug_details);
            //        if (show_debug_details && table_draw_list)
            //        {
            //            Imgui.SameLine(0.0f, 0.0f);
            //            const int table_draw_list_draw_cmd_count = table_draw_list->CmdBuffer.Size;
            //            if (table_draw_list == parent_draw_list)
            //                Imgui.Text(": DrawCmd: +%d (in same window)",
            //                    table_draw_list_draw_cmd_count - parent_draw_list_draw_cmd_count);
            //            else
            //                Imgui.Text(": DrawCmd: +%d (in child window), Scroll: (%.f/%.f) (%.f/%.f)",
            //                    table_draw_list_draw_cmd_count - 1, table_scroll_cur.x, table_scroll_max.x, table_scroll_cur.y, table_scroll_max.y);
            //        }
            //        Imgui.TreePop();
            //    }
            //
            //    Imgui.PopID();
            //
            //    ShowDemoWindowColumns();
            //
            //    if (disable_indent)
            //        Imgui.PopStyleVar();
        }

        // Demonstrate old/legacy Columns API!
        // [2020: Columns are under-featured and not maintained. Prefer using the more flexible and powerful BeginTable() API!]
        private static void ShowDemoWindowColumns()
        {
            //    IMGUI_DEMO_MARKER("Columns (legacy API)");
            //    bool open = Imgui.TreeNode("Legacy Columns API");
            //    Imgui.SameLine();
            //    HelpMarker("Columns() is an old API! Prefer using the more flexible and powerful BeginTable() API!");
            //    if (!open)
            //        return;
            //
            //    // Basic columns
            //    IMGUI_DEMO_MARKER("Columns (legacy API)/Basic");
            //    if (Imgui.TreeNode("Basic"))
            //    {
            //        Imgui.Text("Without border:");
            //        Imgui.Columns(3, "mycolumns3", false);  // 3-ways, no border
            //        Imgui.Separator();
            //        for (int n = 0; n < 14; n++)
            //        {
            //            char label[32];
            //            sprintf(label, "Item %d", n);
            //            if (Imgui.Selectable(label)) {}
            //            //if (Imgui.Button(label, ImVec2(-FLT_MIN,0.0f))) {}
            //            Imgui.NextColumn();
            //        }
            //        Imgui.Columns(1);
            //        Imgui.Separator();
            //
            //        Imgui.Text("With border:");
            //        Imgui.Columns(4, "mycolumns"); // 4-ways, with border
            //        Imgui.Separator();
            //        Imgui.Text("ID"); Imgui.NextColumn();
            //        Imgui.Text("Name"); Imgui.NextColumn();
            //        Imgui.Text("Path"); Imgui.NextColumn();
            //        Imgui.Text("Hovered"); Imgui.NextColumn();
            //        Imgui.Separator();
            //        const char* names[3] = { "One", "Two", "Three" };
            //        const char* paths[3] = { "/path/one", "/path/two", "/path/three" };
            //        static int selected = -1;
            //        for (int i = 0; i < 3; i++)
            //        {
            //            char label[32];
            //            sprintf(label, "%04d", i);
            //            if (Imgui.Selectable(label, selected == i, ImGuiSelectableFlags_SpanAllColumns))
            //                selected = i;
            //            bool hovered = Imgui.IsItemHovered();
            //            Imgui.NextColumn();
            //            Imgui.Text(names[i]); Imgui.NextColumn();
            //            Imgui.Text(paths[i]); Imgui.NextColumn();
            //            Imgui.Text("%d", hovered); Imgui.NextColumn();
            //        }
            //        Imgui.Columns(1);
            //        Imgui.Separator();
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Columns (legacy API)/Borders");
            //    if (Imgui.TreeNode("Borders"))
            //    {
            //        // NB: Future columns API should allow automatic horizontal borders.
            //        static bool h_borders = true;
            //        static bool v_borders = true;
            //        static int columns_count = 4;
            //        const int lines_count = 3;
            //        Imgui.SetNextItemWidth(Imgui.GetFontSize() * 8);
            //        Imgui.DragInt("##columns_count", &columns_count, 0.1f, 2, 10, "%d columns");
            //        if (columns_count < 2)
            //            columns_count = 2;
            //        Imgui.SameLine();
            //        Imgui.Checkbox("horizontal", &h_borders);
            //        Imgui.SameLine();
            //        Imgui.Checkbox("vertical", &v_borders);
            //        Imgui.Columns(columns_count, NULL, v_borders);
            //        for (int i = 0; i < columns_count * lines_count; i++)
            //        {
            //            if (h_borders && Imgui.GetColumnIndex() == 0)
            //                Imgui.Separator();
            //            Imgui.Text("%c%c%c", 'a' + i, 'a' + i, 'a' + i);
            //            Imgui.Text("Width %.2f", Imgui.GetColumnWidth());
            //            Imgui.Text("Avail %.2f", Imgui.GetContentRegionAvail().x);
            //            Imgui.Text("Offset %.2f", Imgui.GetColumnOffset());
            //            Imgui.Text("Long text that is likely to clip");
            //            Imgui.Button("Button", ImVec2(-FLT_MIN, 0.0f));
            //            Imgui.NextColumn();
            //        }
            //        Imgui.Columns(1);
            //        if (h_borders)
            //            Imgui.Separator();
            //        Imgui.TreePop();
            //    }
            //
            //    // Create multiple items in a same cell before switching to next column
            //    IMGUI_DEMO_MARKER("Columns (legacy API)/Mixed items");
            //    if (Imgui.TreeNode("Mixed items"))
            //    {
            //        Imgui.Columns(3, "mixed");
            //        Imgui.Separator();
            //
            //        Imgui.Text("Hello");
            //        Imgui.Button("Banana");
            //        Imgui.NextColumn();
            //
            //        Imgui.Text("ImGui");
            //        Imgui.Button("Apple");
            //        static float foo = 1.0f;
            //        Imgui.InputFloat("red", &foo, 0.05f, 0, "%.3f");
            //        Imgui.Text("An extra line here.");
            //        Imgui.NextColumn();
            //
            //        Imgui.Text("Sailor");
            //        Imgui.Button("Corniflower");
            //        static float bar = 1.0f;
            //        Imgui.InputFloat("blue", &bar, 0.05f, 0, "%.3f");
            //        Imgui.NextColumn();
            //
            //        if (Imgui.CollapsingHeader("Category A")) { Imgui.Text("Blah blah blah"); } Imgui.NextColumn();
            //        if (Imgui.CollapsingHeader("Category B")) { Imgui.Text("Blah blah blah"); } Imgui.NextColumn();
            //        if (Imgui.CollapsingHeader("Category C")) { Imgui.Text("Blah blah blah"); } Imgui.NextColumn();
            //        Imgui.Columns(1);
            //        Imgui.Separator();
            //        Imgui.TreePop();
            //    }
            //
            //    // Word wrapping
            //    IMGUI_DEMO_MARKER("Columns (legacy API)/Word-wrapping");
            //    if (Imgui.TreeNode("Word-wrapping"))
            //    {
            //        Imgui.Columns(2, "word-wrapping");
            //        Imgui.Separator();
            //        Imgui.TextWrapped("The quick brown fox jumps over the lazy dog.");
            //        Imgui.TextWrapped("Hello Left");
            //        Imgui.NextColumn();
            //        Imgui.TextWrapped("The quick brown fox jumps over the lazy dog.");
            //        Imgui.TextWrapped("Hello Right");
            //        Imgui.Columns(1);
            //        Imgui.Separator();
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Columns (legacy API)/Horizontal Scrolling");
            //    if (Imgui.TreeNode("Horizontal Scrolling"))
            //    {
            //        Imgui.SetNextWindowContentSize(ImVec2(1500.0f, 0.0f));
            //        ImVec2 child_size = ImVec2(0, Imgui.GetFontSize() * 20.0f);
            //        Imgui.BeginChild("##ScrollingRegion", child_size, false, ImGuiWindowFlags_HorizontalScrollbar);
            //        Imgui.Columns(10);
            //
            //        // Also demonstrate using clipper for large vertical lists
            //        int ITEMS_COUNT = 2000;
            //        ImGuiListClipper clipper;
            //        clipper.Begin(ITEMS_COUNT);
            //        while (clipper.Step())
            //        {
            //            for (int i = clipper.DisplayStart; i < clipper.DisplayEnd; i++)
            //                for (int j = 0; j < 10; j++)
            //                {
            //                    Imgui.Text("Line %d Column %d...", i, j);
            //                    Imgui.NextColumn();
            //                }
            //        }
            //        Imgui.Columns(1);
            //        Imgui.EndChild();
            //        Imgui.TreePop();
            //    }
            //
            //    IMGUI_DEMO_MARKER("Columns (legacy API)/Tree");
            //    if (Imgui.TreeNode("Tree"))
            //    {
            //        Imgui.Columns(2, "tree", true);
            //        for (int x = 0; x < 3; x++)
            //        {
            //            bool open1 = Imgui.TreeNode((void*)(intptr_t)x, "Node%d", x);
            //            Imgui.NextColumn();
            //            Imgui.Text("Node contents");
            //            Imgui.NextColumn();
            //            if (open1)
            //            {
            //                for (int y = 0; y < 3; y++)
            //                {
            //                    bool open2 = Imgui.TreeNode((void*)(intptr_t)y, "Node%d.%d", x, y);
            //                    Imgui.NextColumn();
            //                    Imgui.Text("Node contents");
            //                    if (open2)
            //                    {
            //                        Imgui.Text("Even more contents");
            //                        if (Imgui.TreeNode("Tree in column"))
            //                        {
            //                            Imgui.Text("The quick brown fox jumps over the lazy dog");
            //                            Imgui.TreePop();
            //                        }
            //                    }
            //                    Imgui.NextColumn();
            //                    if (open2)
            //                        Imgui.TreePop();
            //                }
            //                Imgui.TreePop();
            //            }
            //        }
            //        Imgui.Columns(1);
            //        Imgui.TreePop();
            //    }
            //
            //    Imgui.TreePop();
        }

        //namespace ImGui { extern ImGuiKeyData* GetKeyData(ImGuiKey key); }

        private static void ShowDemoWindowInputs()
        {
            //    IMGUI_DEMO_MARKER("Inputs & Focus");
            //    if (Imgui.CollapsingHeader("Inputs & Focus"))
            //    {
            //        ImGuiIO& io = Imgui.GetIO();
            //
            //        // Display inputs submitted to ImGuiIO
            //        IMGUI_DEMO_MARKER("Inputs & Focus/Inputs");
            //        Imgui.SetNextItemOpen(true, ImGuiCond_Once);
            //        if (Imgui.TreeNode("Inputs"))
            //        {
            //            HelpMarker(
            //                "This is a simplified view. See more detailed input state:\n"
            //                "- in 'Tools->Metrics/Debugger->Inputs'.\n"
            //                "- in 'Tools->Debug Log->IO'.");
            //            if (Imgui.IsMousePosValid())
            //                Imgui.Text("Mouse pos: (%g, %g)", io.MousePos.x, io.MousePos.y);
            //            else
            //                Imgui.Text("Mouse pos: <INVALID>");
            //            Imgui.Text("Mouse delta: (%g, %g)", io.MouseDelta.x, io.MouseDelta.y);
            //            Imgui.Text("Mouse down:");
            //            for (int i = 0; i < IM_ARRAYSIZE(io.MouseDown); i++) if (Imgui.IsMouseDown(i)) { Imgui.SameLine(); Imgui.Text("b%d (%.02f secs)", i, io.MouseDownDuration[i]); }
            //            Imgui.Text("Mouse wheel: %.1f", io.MouseWheel);
            //
            //            // We iterate both legacy native range and named ImGuiKey ranges, which is a little odd but this allows displaying the data for old/new backends.
            //            // User code should never have to go through such hoops: old code may use native keycodes, new code may use ImGuiKey codes.
            //#ifdef IMGUI_DISABLE_OBSOLETE_KEYIO
            //            struct funcs { static bool IsLegacyNativeDupe(ImGuiKey) { return false; } };
            //#else
            //            struct funcs { static bool IsLegacyNativeDupe(ImGuiKey key) { return key < 512 && Imgui.GetIO().KeyMap[key] != -1; } }; // Hide Native<>ImGuiKey duplicates when both exists in the array
            //#endif
            //            Imgui.Text("Keys down:");         for (ImGuiKey key = ImGuiKey_KeysData_OFFSET; key < ImGuiKey_COUNT; key = (ImGuiKey)(key + 1)) { if (funcs::IsLegacyNativeDupe(key) || !Imgui.IsKeyDown(key)) continue; Imgui.SameLine(); Imgui.Text((key < ImGuiKey_NamedKey_BEGIN) ? "\"%s\"" : "\"%s\" %d", Imgui.GetKeyName(key), key); Imgui.SameLine(); Imgui.Text("(%.02f)", Imgui.GetKeyData(key)->DownDuration); }
            //            Imgui.Text("Keys mods: %s%s%s%s", io.KeyCtrl ? "CTRL " : "", io.KeyShift ? "SHIFT " : "", io.KeyAlt ? "ALT " : "", io.KeySuper ? "SUPER " : "");
            //            Imgui.Text("Chars queue:");       for (int i = 0; i < io.InputQueueCharacters.Size; i++) { ImWchar c = io.InputQueueCharacters[i]; Imgui.SameLine();  Imgui.Text("\'%c\' (0x%04X)", (c > ' ' && c <= 255) ? (char)c : '?', c); } // FIXME: We should convert 'c' to UTF-8 here but the functions are not public.
            //
            //            Imgui.TreePop();
            //        }
            //
            //        // Display ImGuiIO output flags
            //        IMGUI_DEMO_MARKER("Inputs & Focus/Outputs");
            //        Imgui.SetNextItemOpen(true, ImGuiCond_Once);
            //        if (Imgui.TreeNode("Outputs"))
            //        {
            //            HelpMarker(
            //                "The value of io.WantCaptureMouse and io.WantCaptureKeyboard are normally set by Dear ImGui "
            //                "to instruct your application of how to route inputs. Typically, when a value is true, it means "
            //                "Dear ImGui wants the corresponding inputs and we expect the underlying application to ignore them.\n\n"
            //                "The most typical case is: when hovering a window, Dear ImGui set io.WantCaptureMouse to true, "
            //                "and underlying application should ignore mouse inputs (in practice there are many and more subtle "
            //                "rules leading to how those flags are set).");
            //            Imgui.Text("io.WantCaptureMouse: %d", io.WantCaptureMouse);
            //            Imgui.Text("io.WantCaptureMouseUnlessPopupClose: %d", io.WantCaptureMouseUnlessPopupClose);
            //            Imgui.Text("io.WantCaptureKeyboard: %d", io.WantCaptureKeyboard);
            //            Imgui.Text("io.WantTextInput: %d", io.WantTextInput);
            //            Imgui.Text("io.WantSetMousePos: %d", io.WantSetMousePos);
            //            Imgui.Text("io.NavActive: %d, io.NavVisible: %d", io.NavActive, io.NavVisible);
            //
            //            IMGUI_DEMO_MARKER("Inputs & Focus/Outputs/WantCapture override");
            //            if (Imgui.TreeNode("WantCapture override"))
            //            {
            //                HelpMarker(
            //                    "Hovering the colored canvas will override io.WantCaptureXXX fields.\n"
            //                    "Notice how normally (when set to none), the value of io.WantCaptureKeyboard would be false when hovering and true when clicking.");
            //                static int capture_override_mouse = -1;
            //                static int capture_override_keyboard = -1;
            //                const char* capture_override_desc[] = { "None", "Set to false", "Set to true" };
            //                Imgui.SetNextItemWidth(Imgui.GetFontSize() * 15);
            //                Imgui.SliderInt("SetNextFrameWantCaptureMouse() on hover", &capture_override_mouse, -1, +1, capture_override_desc[capture_override_mouse + 1], ImGuiSliderFlags_AlwaysClamp);
            //                Imgui.SetNextItemWidth(Imgui.GetFontSize() * 15);
            //                Imgui.SliderInt("SetNextFrameWantCaptureKeyboard() on hover", &capture_override_keyboard, -1, +1, capture_override_desc[capture_override_keyboard + 1], ImGuiSliderFlags_AlwaysClamp);
            //
            //                Imgui.ColorButton("##panel", ImVec4(0.7f, 0.1f, 0.7f, 1.0f), ImGuiColorEditFlags_NoTooltip | ImGuiColorEditFlags_NoDragDrop, ImVec2(128.0f, 96.0f)); // Dummy item
            //                if (Imgui.IsItemHovered() && capture_override_mouse != -1)
            //                    Imgui.SetNextFrameWantCaptureMouse(capture_override_mouse == 1);
            //                if (Imgui.IsItemHovered() && capture_override_keyboard != -1)
            //                    Imgui.SetNextFrameWantCaptureKeyboard(capture_override_keyboard == 1);
            //
            //                Imgui.TreePop();
            //            }
            //            Imgui.TreePop();
            //        }
            //
            //        // Display mouse cursors
            //        IMGUI_DEMO_MARKER("Inputs & Focus/Mouse Cursors");
            //        if (Imgui.TreeNode("Mouse Cursors"))
            //        {
            //            const char* mouse_cursors_names[] = { "Arrow", "TextInput", "ResizeAll", "ResizeNS", "ResizeEW", "ResizeNESW", "ResizeNWSE", "Hand", "NotAllowed" };
            //            IM_ASSERT(IM_ARRAYSIZE(mouse_cursors_names) == ImGuiMouseCursor_COUNT);
            //
            //            ImGuiMouseCursor current = Imgui.GetMouseCursor();
            //            Imgui.Text("Current mouse cursor = %d: %s", current, mouse_cursors_names[current]);
            //            Imgui.BeginDisabled(true);
            //            Imgui.CheckboxFlags("io.BackendFlags: HasMouseCursors", &io.BackendFlags, ImGuiBackendFlags_HasMouseCursors);
            //            Imgui.EndDisabled();
            //
            //            Imgui.Text("Hover to see mouse cursors:");
            //            Imgui.SameLine(); HelpMarker(
            //                "Your application can render a different mouse cursor based on what Imgui.GetMouseCursor() returns. "
            //                "If software cursor rendering (io.MouseDrawCursor) is set ImGui will draw the right cursor for you, "
            //                "otherwise your backend needs to handle it.");
            //            for (int i = 0; i < ImGuiMouseCursor_COUNT; i++)
            //            {
            //                char label[32];
            //                sprintf(label, "Mouse cursor %d: %s", i, mouse_cursors_names[i]);
            //                Imgui.Bullet(); Imgui.Selectable(label, false);
            //                if (Imgui.IsItemHovered())
            //                    Imgui.SetMouseCursor(i);
            //            }
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Inputs & Focus/Tabbing");
            //        if (Imgui.TreeNode("Tabbing"))
            //        {
            //            Imgui.Text("Use TAB/SHIFT+TAB to cycle through keyboard editable fields.");
            //            static char buf[32] = "hello";
            //            Imgui.InputText("1", buf, IM_ARRAYSIZE(buf));
            //            Imgui.InputText("2", buf, IM_ARRAYSIZE(buf));
            //            Imgui.InputText("3", buf, IM_ARRAYSIZE(buf));
            //            Imgui.PushAllowKeyboardFocus(false);
            //            Imgui.InputText("4 (tab skip)", buf, IM_ARRAYSIZE(buf));
            //            Imgui.SameLine(); HelpMarker("Item won't be cycled through when using TAB or Shift+Tab.");
            //            Imgui.PopAllowKeyboardFocus();
            //            Imgui.InputText("5", buf, IM_ARRAYSIZE(buf));
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Inputs & Focus/Focus from code");
            //        if (Imgui.TreeNode("Focus from code"))
            //        {
            //            bool focus_1 = Imgui.Button("Focus on 1"); Imgui.SameLine();
            //            bool focus_2 = Imgui.Button("Focus on 2"); Imgui.SameLine();
            //            bool focus_3 = Imgui.Button("Focus on 3");
            //            int has_focus = 0;
            //            static char buf[128] = "click on a button to set focus";
            //
            //            if (focus_1) Imgui.SetKeyboardFocusHere();
            //            Imgui.InputText("1", buf, IM_ARRAYSIZE(buf));
            //            if (Imgui.IsItemActive()) has_focus = 1;
            //
            //            if (focus_2) Imgui.SetKeyboardFocusHere();
            //            Imgui.InputText("2", buf, IM_ARRAYSIZE(buf));
            //            if (Imgui.IsItemActive()) has_focus = 2;
            //
            //            Imgui.PushAllowKeyboardFocus(false);
            //            if (focus_3) Imgui.SetKeyboardFocusHere();
            //            Imgui.InputText("3 (tab skip)", buf, IM_ARRAYSIZE(buf));
            //            if (Imgui.IsItemActive()) has_focus = 3;
            //            Imgui.SameLine(); HelpMarker("Item won't be cycled through when using TAB or Shift+Tab.");
            //            Imgui.PopAllowKeyboardFocus();
            //
            //            if (has_focus)
            //                Imgui.Text("Item with focus: %d", has_focus);
            //            else
            //                Imgui.Text("Item with focus: <none>");
            //
            //            // Use >= 0 parameter to SetKeyboardFocusHere() to focus an upcoming item
            //            static float f3[3] = { 0.0f, 0.0f, 0.0f };
            //            int focus_ahead = -1;
            //            if (Imgui.Button("Focus on X")) { focus_ahead = 0; } Imgui.SameLine();
            //            if (Imgui.Button("Focus on Y")) { focus_ahead = 1; } Imgui.SameLine();
            //            if (Imgui.Button("Focus on Z")) { focus_ahead = 2; }
            //            if (focus_ahead != -1) Imgui.SetKeyboardFocusHere(focus_ahead);
            //            Imgui.SliderFloat3("Float3", &f3[0], 0.0f, 1.0f);
            //
            //            Imgui.TextWrapped("NB: Cursor & selection are preserved when refocusing last used item in code.");
            //            Imgui.TreePop();
            //        }
            //
            //        IMGUI_DEMO_MARKER("Inputs & Focus/Dragging");
            //        if (Imgui.TreeNode("Dragging"))
            //        {
            //            Imgui.TextWrapped("You can use Imgui.GetMouseDragDelta(0) to query for the dragged amount on any widget.");
            //            for (int button = 0; button < 3; button++)
            //            {
            //                Imgui.Text("IsMouseDragging(%d):", button);
            //                Imgui.Text("  w/ default threshold: %d,", Imgui.IsMouseDragging(button));
            //                Imgui.Text("  w/ zero threshold: %d,", Imgui.IsMouseDragging(button, 0.0f));
            //                Imgui.Text("  w/ large threshold: %d,", Imgui.IsMouseDragging(button, 20.0f));
            //            }
            //
            //            Imgui.Button("Drag Me");
            //            if (Imgui.IsItemActive())
            //                Imgui.GetForegroundDrawList()->AddLine(io.MouseClickedPos[0], io.MousePos, Imgui.GetColorU32(ImGuiCol_Button), 4.0f); // Draw a line between the button and the mouse cursor
            //
            //            // Drag operations gets "unlocked" when the mouse has moved past a certain threshold
            //            // (the default threshold is stored in io.MouseDragThreshold). You can request a lower or higher
            //            // threshold using the second parameter of IsMouseDragging() and GetMouseDragDelta().
            //            ImVec2 value_raw = Imgui.GetMouseDragDelta(0, 0.0f);
            //            ImVec2 value_with_lock_threshold = Imgui.GetMouseDragDelta(0);
            //            ImVec2 mouse_delta = io.MouseDelta;
            //            Imgui.Text("GetMouseDragDelta(0):");
            //            Imgui.Text("  w/ default threshold: (%.1f, %.1f)", value_with_lock_threshold.x, value_with_lock_threshold.y);
            //            Imgui.Text("  w/ zero threshold: (%.1f, %.1f)", value_raw.x, value_raw.y);
            //            Imgui.Text("io.MouseDelta: (%.1f, %.1f)", mouse_delta.x, mouse_delta.y);
            //            Imgui.TreePop();
            //        }
            //    }
        }

        public static void ShowAboutWindow(State<bool> p_open)
        {
            //    if (!Imgui.Begin("About Dear ImGui", p_open, ImGuiWindowFlags_AlwaysAutoResize))
            //    {
            //        Imgui.End();
            //        return;
            //    }
            //    IMGUI_DEMO_MARKER("Tools/About Dear ImGui");
            //    Imgui.Text("Dear ImGui %s", Imgui.GetVersion());
            //    Imgui.Separator();
            //    Imgui.Text("By Omar Cornut and all Dear ImGui contributors.");
            //    Imgui.Text("Dear ImGui is licensed under the MIT License, see LICENSE for more information.");
            //
            //    static bool show_config_info = false;
            //    Imgui.Checkbox("Config/Build Information", &show_config_info);
            //    if (show_config_info)
            //    {
            //        ImGuiIO& io = Imgui.GetIO();
            //        ImGuiStyle& style = Imgui.GetStyle();
            //
            //        bool copy_to_clipboard = Imgui.Button("Copy to clipboard");
            //        ImVec2 child_size = ImVec2(0, Imgui.GetTextLineHeightWithSpacing() * 18);
            //        Imgui.BeginChildFrame(Imgui.GetID("cfg_infos"), child_size, ImGuiWindowFlags_NoMove);
            //        if (copy_to_clipboard)
            //        {
            //            Imgui.LogToClipboard();
            //            Imgui.LogText("```\n"); // Back quotes will make text appears without formatting when pasting on GitHub
            //        }
            //
            //        Imgui.Text("Dear ImGui %s (%d)", IMGUI_VERSION, IMGUI_VERSION_NUM);
            //        Imgui.Separator();
            //        Imgui.Text("sizeof(size_t): %d, sizeof(ImDrawIdx): %d, sizeof(ImDrawVert): %d", (int)sizeof(size_t), (int)sizeof(ImDrawIdx), (int)sizeof(ImDrawVert));
            //        Imgui.Text("define: __cplusplus=%d", (int)__cplusplus);
            //#ifdef IMGUI_DISABLE_OBSOLETE_FUNCTIONS
            //        Imgui.Text("define: IMGUI_DISABLE_OBSOLETE_FUNCTIONS");
            //#endif
            //#ifdef IMGUI_DISABLE_OBSOLETE_KEYIO
            //        Imgui.Text("define: IMGUI_DISABLE_OBSOLETE_KEYIO");
            //#endif
            //#ifdef IMGUI_DISABLE_WIN32_DEFAULT_CLIPBOARD_FUNCTIONS
            //        Imgui.Text("define: IMGUI_DISABLE_WIN32_DEFAULT_CLIPBOARD_FUNCTIONS");
            //#endif
            //#ifdef IMGUI_DISABLE_WIN32_DEFAULT_IME_FUNCTIONS
            //        Imgui.Text("define: IMGUI_DISABLE_WIN32_DEFAULT_IME_FUNCTIONS");
            //#endif
            //#ifdef IMGUI_DISABLE_WIN32_FUNCTIONS
            //        Imgui.Text("define: IMGUI_DISABLE_WIN32_FUNCTIONS");
            //#endif
            //#ifdef IMGUI_DISABLE_DEFAULT_FORMAT_FUNCTIONS
            //        Imgui.Text("define: IMGUI_DISABLE_DEFAULT_FORMAT_FUNCTIONS");
            //#endif
            //#ifdef IMGUI_DISABLE_DEFAULT_MATH_FUNCTIONS
            //        Imgui.Text("define: IMGUI_DISABLE_DEFAULT_MATH_FUNCTIONS");
            //#endif
            //#ifdef IMGUI_DISABLE_DEFAULT_FILE_FUNCTIONS
            //        Imgui.Text("define: IMGUI_DISABLE_DEFAULT_FILE_FUNCTIONS");
            //#endif
            //#ifdef IMGUI_DISABLE_FILE_FUNCTIONS
            //        Imgui.Text("define: IMGUI_DISABLE_FILE_FUNCTIONS");
            //#endif
            //#ifdef IMGUI_DISABLE_DEFAULT_ALLOCATORS
            //        Imgui.Text("define: IMGUI_DISABLE_DEFAULT_ALLOCATORS");
            //#endif
            //#ifdef IMGUI_USE_BGRA_PACKED_COLOR
            //        Imgui.Text("define: IMGUI_USE_BGRA_PACKED_COLOR");
            //#endif
            //#ifdef _WIN32
            //        Imgui.Text("define: _WIN32");
            //#endif
            //#ifdef _WIN64
            //        Imgui.Text("define: _WIN64");
            //#endif
            //#ifdef __linux__
            //        Imgui.Text("define: __linux__");
            //#endif
            //#ifdef __APPLE__
            //        Imgui.Text("define: __APPLE__");
            //#endif
            //#ifdef _MSC_VER
            //        Imgui.Text("define: _MSC_VER=%d", _MSC_VER);
            //#endif
            //#ifdef _MSVC_LANG
            //        Imgui.Text("define: _MSVC_LANG=%d", (int)_MSVC_LANG);
            //#endif
            //#ifdef __MINGW32__
            //        Imgui.Text("define: __MINGW32__");
            //#endif
            //#ifdef __MINGW64__
            //        Imgui.Text("define: __MINGW64__");
            //#endif
            //#ifdef __GNUC__
            //        Imgui.Text("define: __GNUC__=%d", (int)__GNUC__);
            //#endif
            //#ifdef __clang_version__
            //        Imgui.Text("define: __clang_version__=%s", __clang_version__);
            //#endif
            //        Imgui.Separator();
            //        Imgui.Text("io.BackendPlatformName: %s", io.BackendPlatformName ? io.BackendPlatformName : "NULL");
            //        Imgui.Text("io.BackendRendererName: %s", io.BackendRendererName ? io.BackendRendererName : "NULL");
            //        Imgui.Text("io.ConfigFlags: 0x%08X", io.ConfigFlags);
            //        if (io.ConfigFlags & ImGuiConfigFlags_NavEnableKeyboard)        Imgui.Text(" NavEnableKeyboard");
            //        if (io.ConfigFlags & ImGuiConfigFlags_NavEnableGamepad)         Imgui.Text(" NavEnableGamepad");
            //        if (io.ConfigFlags & ImGuiConfigFlags_NavEnableSetMousePos)     Imgui.Text(" NavEnableSetMousePos");
            //        if (io.ConfigFlags & ImGuiConfigFlags_NavNoCaptureKeyboard)     Imgui.Text(" NavNoCaptureKeyboard");
            //        if (io.ConfigFlags & ImGuiConfigFlags_NoMouse)                  Imgui.Text(" NoMouse");
            //        if (io.ConfigFlags & ImGuiConfigFlags_NoMouseCursorChange)      Imgui.Text(" NoMouseCursorChange");
            //        if (io.MouseDrawCursor)                                         Imgui.Text("io.MouseDrawCursor");
            //        if (io.ConfigMacOSXBehaviors)                                   Imgui.Text("io.ConfigMacOSXBehaviors");
            //        if (io.ConfigInputTextCursorBlink)                              Imgui.Text("io.ConfigInputTextCursorBlink");
            //        if (io.ConfigWindowsResizeFromEdges)                            Imgui.Text("io.ConfigWindowsResizeFromEdges");
            //        if (io.ConfigWindowsMoveFromTitleBarOnly)                       Imgui.Text("io.ConfigWindowsMoveFromTitleBarOnly");
            //        if (io.ConfigMemoryCompactTimer >= 0.0f)                        Imgui.Text("io.ConfigMemoryCompactTimer = %.1f", io.ConfigMemoryCompactTimer);
            //        Imgui.Text("io.BackendFlags: 0x%08X", io.BackendFlags);
            //        if (io.BackendFlags & ImGuiBackendFlags_HasGamepad)             Imgui.Text(" HasGamepad");
            //        if (io.BackendFlags & ImGuiBackendFlags_HasMouseCursors)        Imgui.Text(" HasMouseCursors");
            //        if (io.BackendFlags & ImGuiBackendFlags_HasSetMousePos)         Imgui.Text(" HasSetMousePos");
            //        if (io.BackendFlags & ImGuiBackendFlags_RendererHasVtxOffset)   Imgui.Text(" RendererHasVtxOffset");
            //        Imgui.Separator();
            //        Imgui.Text("io.Fonts: %d fonts, Flags: 0x%08X, TexSize: %d,%d", io.Fonts->Fonts.Size, io.Fonts->Flags, io.Fonts->TexWidth, io.Fonts->TexHeight);
            //        Imgui.Text("io.DisplaySize: %.2f,%.2f", io.DisplaySize.x, io.DisplaySize.y);
            //        Imgui.Text("io.DisplayFramebufferScale: %.2f,%.2f", io.DisplayFramebufferScale.x, io.DisplayFramebufferScale.y);
            //        Imgui.Separator();
            //        Imgui.Text("style.WindowPadding: %.2f,%.2f", style.WindowPadding.x, style.WindowPadding.y);
            //        Imgui.Text("style.WindowBorderSize: %.2f", style.WindowBorderSize);
            //        Imgui.Text("style.FramePadding: %.2f,%.2f", style.FramePadding.x, style.FramePadding.y);
            //        Imgui.Text("style.FrameRounding: %.2f", style.FrameRounding);
            //        Imgui.Text("style.FrameBorderSize: %.2f", style.FrameBorderSize);
            //        Imgui.Text("style.ItemSpacing: %.2f,%.2f", style.ItemSpacing.x, style.ItemSpacing.y);
            //        Imgui.Text("style.ItemInnerSpacing: %.2f,%.2f", style.ItemInnerSpacing.x, style.ItemInnerSpacing.y);
            //
            //        if (copy_to_clipboard)
            //        {
            //            Imgui.LogText("\n```\n");
            //            Imgui.LogFinish();
            //        }
            //        Imgui.EndChildFrame();
            //    }
            //    Imgui.End();
        }

        //// Forward declare ShowFontAtlas() which isn't worth putting in public API yet
        //namespace ImGui { IMGUI_API void ShowFontAtlas(ImFontAtlas* atlas); }
        //
        //// Demo helper function to select among loaded fonts.
        //// Here we use the regular BeginCombo()/EndCombo() api which is the more flexible one.
        //void Imgui.ShowFontSelector(const char* label)
        //{
        //    ImGuiIO& io = Imgui.GetIO();
        //    ImFont* font_current = Imgui.GetFont();
        //    if (Imgui.BeginCombo(label, font_current->GetDebugName()))
        //    {
        //        for (int n = 0; n < io.Fonts->Fonts.Size; n++)
        //        {
        //            ImFont* font = io.Fonts->Fonts[n];
        //            Imgui.PushID((void*)font);
        //            if (Imgui.Selectable(font->GetDebugName(), font == font_current))
        //                io.FontDefault = font;
        //            Imgui.PopID();
        //        }
        //        Imgui.EndCombo();
        //    }
        //    Imgui.SameLine();
        //    HelpMarker(
        //        "- Load additional fonts with io.Fonts->AddFontFromFileTTF().\n"
        //        "- The font atlas is built when calling io.Fonts->GetTexDataAsXXXX() or io.Fonts->Build().\n"
        //        "- Read FAQ and docs/FONTS.md for more details.\n"
        //        "- If you need to add/remove fonts at runtime (e.g. for DPI change), do it before calling NewFrame().");
        //}
        //
        //// Demo helper function to select among default colors. See ShowStyleEditor() for more advanced options.
        //// Here we use the simplified Combo() api that packs items into a single literal string.
        //// Useful for quick combo boxes where the choices are known locally.
        //bool Imgui.ShowStyleSelector(const char* label)
        //{
        //    static int style_idx = -1;
        //    if (Imgui.Combo(label, &style_idx, "Dark\0Light\0Classic\0"))
        //    {
        //        switch (style_idx)
        //        {
        //        case 0: Imgui.StyleColorsDark(); break;
        //        case 1: Imgui.StyleColorsLight(); break;
        //        case 2: Imgui.StyleColorsClassic(); break;
        //        }
        //        return true;
        //    }
        //    return false;
        //}

        public static void ShowStyleEditor(Style? style = default)
        {
            //    IMGUI_DEMO_MARKER("Tools/Style Editor");
            //    // You can pass in a reference ImGuiStyle structure to compare to, revert to and save to
            //    // (without a reference style pointer, we will use one compared locally as a reference)
            //    ImGuiStyle& style = Imgui.GetStyle();
            //    static ImGuiStyle ref_saved_style;
            //
            //    // Default to using internal storage as reference
            //    static bool init = true;
            //    if (init && ref == NULL)
            //        ref_saved_style = style;
            //    init = false;
            //    if (ref == NULL)
            //        ref = &ref_saved_style;
            //
            //    Imgui.PushItemWidth(Imgui.GetWindowWidth() * 0.50f);
            //
            //    if (Imgui.ShowStyleSelector("Colors##Selector"))
            //        ref_saved_style = style;
            //    Imgui.ShowFontSelector("Fonts##Selector");
            //
            //    // Simplified Settings (expose floating-pointer border sizes as boolean representing 0.0f or 1.0f)
            //    if (Imgui.SliderFloat("FrameRounding", &style.FrameRounding, 0.0f, 12.0f, "%.0f"))
            //        style.GrabRounding = style.FrameRounding; // Make GrabRounding always the same value as FrameRounding
            //    { bool border = (style.WindowBorderSize > 0.0f); if (Imgui.Checkbox("WindowBorder", &border)) { style.WindowBorderSize = border ? 1.0f : 0.0f; } }
            //    Imgui.SameLine();
            //    { bool border = (style.FrameBorderSize > 0.0f);  if (Imgui.Checkbox("FrameBorder",  &border)) { style.FrameBorderSize  = border ? 1.0f : 0.0f; } }
            //    Imgui.SameLine();
            //    { bool border = (style.PopupBorderSize > 0.0f);  if (Imgui.Checkbox("PopupBorder",  &border)) { style.PopupBorderSize  = border ? 1.0f : 0.0f; } }
            //
            //    // Save/Revert button
            //    if (Imgui.Button("Save Ref"))
            //        *ref = ref_saved_style = style;
            //    Imgui.SameLine();
            //    if (Imgui.Button("Revert Ref"))
            //        style = *ref;
            //    Imgui.SameLine();
            //    HelpMarker(
            //        "Save/Revert in local non-persistent storage. Default Colors definition are not affected. "
            //        "Use \"Export\" below to save them somewhere.");
            //
            //    Imgui.Separator();
            //
            //    if (Imgui.BeginTabBar("##tabs", ImGuiTabBarFlags_None))
            //    {
            //        if (Imgui.BeginTabItem("Sizes"))
            //        {
            //            Imgui.Text("Main");
            //            Imgui.SliderFloat2("WindowPadding", (float*)&style.WindowPadding, 0.0f, 20.0f, "%.0f");
            //            Imgui.SliderFloat2("FramePadding", (float*)&style.FramePadding, 0.0f, 20.0f, "%.0f");
            //            Imgui.SliderFloat2("CellPadding", (float*)&style.CellPadding, 0.0f, 20.0f, "%.0f");
            //            Imgui.SliderFloat2("ItemSpacing", (float*)&style.ItemSpacing, 0.0f, 20.0f, "%.0f");
            //            Imgui.SliderFloat2("ItemInnerSpacing", (float*)&style.ItemInnerSpacing, 0.0f, 20.0f, "%.0f");
            //            Imgui.SliderFloat2("TouchExtraPadding", (float*)&style.TouchExtraPadding, 0.0f, 10.0f, "%.0f");
            //            Imgui.SliderFloat("IndentSpacing", &style.IndentSpacing, 0.0f, 30.0f, "%.0f");
            //            Imgui.SliderFloat("ScrollbarSize", &style.ScrollbarSize, 1.0f, 20.0f, "%.0f");
            //            Imgui.SliderFloat("GrabMinSize", &style.GrabMinSize, 1.0f, 20.0f, "%.0f");
            //            Imgui.Text("Borders");
            //            Imgui.SliderFloat("WindowBorderSize", &style.WindowBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.SliderFloat("ChildBorderSize", &style.ChildBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.SliderFloat("PopupBorderSize", &style.PopupBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.SliderFloat("FrameBorderSize", &style.FrameBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.SliderFloat("TabBorderSize", &style.TabBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.Text("Rounding");
            //            Imgui.SliderFloat("WindowRounding", &style.WindowRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.SliderFloat("ChildRounding", &style.ChildRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.SliderFloat("FrameRounding", &style.FrameRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.SliderFloat("PopupRounding", &style.PopupRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.SliderFloat("ScrollbarRounding", &style.ScrollbarRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.SliderFloat("GrabRounding", &style.GrabRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.SliderFloat("LogSliderDeadzone", &style.LogSliderDeadzone, 0.0f, 12.0f, "%.0f");
            //            Imgui.SliderFloat("TabRounding", &style.TabRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.Text("Alignment");
            //            Imgui.SliderFloat2("WindowTitleAlign", (float*)&style.WindowTitleAlign, 0.0f, 1.0f, "%.2f");
            //            int window_menu_button_position = style.WindowMenuButtonPosition + 1;
            //            if (Imgui.Combo("WindowMenuButtonPosition", (int*)&window_menu_button_position, "None\0Left\0Right\0"))
            //                style.WindowMenuButtonPosition = window_menu_button_position - 1;
            //            Imgui.Combo("ColorButtonPosition", (int*)&style.ColorButtonPosition, "Left\0Right\0");
            //            Imgui.SliderFloat2("ButtonTextAlign", (float*)&style.ButtonTextAlign, 0.0f, 1.0f, "%.2f");
            //            Imgui.SameLine(); HelpMarker("Alignment applies when a button is larger than its text content.");
            //            Imgui.SliderFloat2("SelectableTextAlign", (float*)&style.SelectableTextAlign, 0.0f, 1.0f, "%.2f");
            //            Imgui.SameLine(); HelpMarker("Alignment applies when a selectable is larger than its text content.");
            //            Imgui.Text("Safe Area Padding");
            //            Imgui.SameLine(); HelpMarker("Adjust if you cannot see the edges of your screen (e.g. on a TV where scaling has not been configured).");
            //            Imgui.SliderFloat2("DisplaySafeAreaPadding", (float*)&style.DisplaySafeAreaPadding, 0.0f, 30.0f, "%.0f");
            //            Imgui.EndTabItem();
            //        }
            //
            //        if (Imgui.BeginTabItem("Colors"))
            //        {
            //            static int output_dest = 0;
            //            static bool output_only_modified = true;
            //            if (Imgui.Button("Export"))
            //            {
            //                if (output_dest == 0)
            //                    Imgui.LogToClipboard();
            //                else
            //                    Imgui.LogToTTY();
            //                Imgui.LogText("ImVec4* colors = Imgui.GetStyle().Colors;" IM_NEWLINE);
            //                for (int i = 0; i < ImGuiCol_COUNT; i++)
            //                {
            //                    const ImVec4& col = style.Colors[i];
            //                    const char* name = Imgui.GetStyleColorName(i);
            //                    if (!output_only_modified || memcmp(&col, &ref->Colors[i], sizeof(ImVec4)) != 0)
            //                        Imgui.LogText("colors[ImGuiCol_%s]%*s= ImVec4(%.2ff, %.2ff, %.2ff, %.2ff);" IM_NEWLINE,
            //                            name, 23 - (int)strlen(name), "", col.x, col.y, col.z, col.w);
            //                }
            //                Imgui.LogFinish();
            //            }
            //            Imgui.SameLine(); Imgui.SetNextItemWidth(120); Imgui.Combo("##output_type", &output_dest, "To Clipboard\0To TTY\0");
            //            Imgui.SameLine(); Imgui.Checkbox("Only Modified Colors", &output_only_modified);
            //
            //            static ImGuiTextFilter filter;
            //            filter.Draw("Filter colors", Imgui.GetFontSize() * 16);
            //
            //            static ImGuiColorEditFlags alpha_flags = 0;
            //            if (Imgui.RadioButton("Opaque", alpha_flags == ImGuiColorEditFlags_None))             { alpha_flags = ImGuiColorEditFlags_None; } Imgui.SameLine();
            //            if (Imgui.RadioButton("Alpha",  alpha_flags == ImGuiColorEditFlags_AlphaPreview))     { alpha_flags = ImGuiColorEditFlags_AlphaPreview; } Imgui.SameLine();
            //            if (Imgui.RadioButton("Both",   alpha_flags == ImGuiColorEditFlags_AlphaPreviewHalf)) { alpha_flags = ImGuiColorEditFlags_AlphaPreviewHalf; } Imgui.SameLine();
            //            HelpMarker(
            //                "In the color list:\n"
            //                "Left-click on color square to open color picker,\n"
            //                "Right-click to open edit options menu.");
            //
            //            Imgui.BeginChild("##colors", ImVec2(0, 0), true, ImGuiWindowFlags_AlwaysVerticalScrollbar | ImGuiWindowFlags_AlwaysHorizontalScrollbar | ImGuiWindowFlags_NavFlattened);
            //            Imgui.PushItemWidth(-160);
            //            for (int i = 0; i < ImGuiCol_COUNT; i++)
            //            {
            //                const char* name = Imgui.GetStyleColorName(i);
            //                if (!filter.PassFilter(name))
            //                    continue;
            //                Imgui.PushID(i);
            //                Imgui.ColorEdit4("##color", (float*)&style.Colors[i], ImGuiColorEditFlags_AlphaBar | alpha_flags);
            //                if (memcmp(&style.Colors[i], &ref->Colors[i], sizeof(ImVec4)) != 0)
            //                {
            //                    // Tips: in a real user application, you may want to merge and use an icon font into the main font,
            //                    // so instead of "Save"/"Revert" you'd use icons!
            //                    // Read the FAQ and docs/FONTS.md about using icon fonts. It's really easy and super convenient!
            //                    Imgui.SameLine(0.0f, style.ItemInnerSpacing.x); if (Imgui.Button("Save")) { ref->Colors[i] = style.Colors[i]; }
            //                    Imgui.SameLine(0.0f, style.ItemInnerSpacing.x); if (Imgui.Button("Revert")) { style.Colors[i] = ref->Colors[i]; }
            //                }
            //                Imgui.SameLine(0.0f, style.ItemInnerSpacing.x);
            //                Imgui.TextUnformatted(name);
            //                Imgui.PopID();
            //            }
            //            Imgui.PopItemWidth();
            //            Imgui.EndChild();
            //
            //            Imgui.EndTabItem();
            //        }
            //
            //        if (Imgui.BeginTabItem("Fonts"))
            //        {
            //            ImGuiIO& io = Imgui.GetIO();
            //            ImFontAtlas* atlas = io.Fonts;
            //            HelpMarker("Read FAQ and docs/FONTS.md for details on font loading.");
            //            Imgui.ShowFontAtlas(atlas);
            //
            //            // Post-baking font scaling. Note that this is NOT the nice way of scaling fonts, read below.
            //            // (we enforce hard clamping manually as by default DragFloat/SliderFloat allows CTRL+Click text to get out of bounds).
            //            const float MIN_SCALE = 0.3f;
            //            const float MAX_SCALE = 2.0f;
            //            HelpMarker(
            //                "Those are old settings provided for convenience.\n"
            //                "However, the _correct_ way of scaling your UI is currently to reload your font at the designed size, "
            //                "rebuild the font atlas, and call style.ScaleAllSizes() on a reference ImGuiStyle structure.\n"
            //                "Using those settings here will give you poor quality results.");
            //            static float window_scale = 1.0f;
            //            Imgui.PushItemWidth(Imgui.GetFontSize() * 8);
            //            if (Imgui.DragFloat("window scale", &window_scale, 0.005f, MIN_SCALE, MAX_SCALE, "%.2f", ImGuiSliderFlags_AlwaysClamp)) // Scale only this window
            //                Imgui.SetWindowFontScale(window_scale);
            //            Imgui.DragFloat("global scale", &io.FontGlobalScale, 0.005f, MIN_SCALE, MAX_SCALE, "%.2f", ImGuiSliderFlags_AlwaysClamp); // Scale everything
            //            Imgui.PopItemWidth();
            //
            //            Imgui.EndTabItem();
            //        }
            //
            //        if (Imgui.BeginTabItem("Rendering"))
            //        {
            //            Imgui.Checkbox("Anti-aliased lines", &style.AntiAliasedLines);
            //            Imgui.SameLine();
            //            HelpMarker("When disabling anti-aliasing lines, you'll probably want to disable borders in your style as well.");
            //
            //            Imgui.Checkbox("Anti-aliased lines use texture", &style.AntiAliasedLinesUseTex);
            //            Imgui.SameLine();
            //            HelpMarker("Faster lines using texture data. Require backend to render with bilinear filtering (not point/nearest filtering).");
            //
            //            Imgui.Checkbox("Anti-aliased fill", &style.AntiAliasedFill);
            //            Imgui.PushItemWidth(Imgui.GetFontSize() * 8);
            //            Imgui.DragFloat("Curve Tessellation Tolerance", &style.CurveTessellationTol, 0.02f, 0.10f, 10.0f, "%.2f");
            //            if (style.CurveTessellationTol < 0.10f) style.CurveTessellationTol = 0.10f;
            //
            //            // When editing the "Circle Segment Max Error" value, draw a preview of its effect on auto-tessellated circles.
            //            Imgui.DragFloat("Circle Tessellation Max Error", &style.CircleTessellationMaxError , 0.005f, 0.10f, 5.0f, "%.2f", ImGuiSliderFlags_AlwaysClamp);
            //            if (Imgui.IsItemActive())
            //            {
            //                Imgui.SetNextWindowPos(Imgui.GetCursorScreenPos());
            //                Imgui.BeginTooltip();
            //                Imgui.TextUnformatted("(R = radius, N = number of segments)");
            //                Imgui.Spacing();
            //                ImDrawList* draw_list = Imgui.GetWindowDrawList();
            //                const float min_widget_width = Imgui.CalcTextSize("N: MMM\nR: MMM").x;
            //                for (int n = 0; n < 8; n++)
            //                {
            //                    const float RAD_MIN = 5.0f;
            //                    const float RAD_MAX = 70.0f;
            //                    const float rad = RAD_MIN + (RAD_MAX - RAD_MIN) * (float)n / (8.0f - 1.0f);
            //
            //                    Imgui.BeginGroup();
            //
            //                    Imgui.Text("R: %.f\nN: %d", rad, draw_list->_CalcCircleAutoSegmentCount(rad));
            //
            //                    const float canvas_width = IM_MAX(min_widget_width, rad * 2.0f);
            //                    const float offset_x     = floorf(canvas_width * 0.5f);
            //                    const float offset_y     = floorf(RAD_MAX);
            //
            //                    const ImVec2 p1 = Imgui.GetCursorScreenPos();
            //                    draw_list->AddCircle(ImVec2(p1.x + offset_x, p1.y + offset_y), rad, Imgui.GetColorU32(ImGuiCol_Text));
            //                    Imgui.Dummy(ImVec2(canvas_width, RAD_MAX * 2));
            //
            //                    /*
            //                    const ImVec2 p2 = Imgui.GetCursorScreenPos();
            //                    draw_list->AddCircleFilled(ImVec2(p2.x + offset_x, p2.y + offset_y), rad, Imgui.GetColorU32(ImGuiCol_Text));
            //                    Imgui.Dummy(ImVec2(canvas_width, RAD_MAX * 2));
            //                    */
            //
            //                    Imgui.EndGroup();
            //                    Imgui.SameLine();
            //                }
            //                Imgui.EndTooltip();
            //            }
            //            Imgui.SameLine();
            //            HelpMarker("When drawing circle primitives with \"num_segments == 0\" tesselation will be calculated automatically.");
            //
            //            Imgui.DragFloat("Global Alpha", &style.Alpha, 0.005f, 0.20f, 1.0f, "%.2f"); // Not exposing zero here so user doesn't "lose" the UI (zero alpha clips all widgets). But application code could have a toggle to switch between zero and non-zero.
            //            Imgui.DragFloat("Disabled Alpha", &style.DisabledAlpha, 0.005f, 0.0f, 1.0f, "%.2f"); Imgui.SameLine(); HelpMarker("Additional alpha multiplier for disabled items (multiply over current value of Alpha).");
            //            Imgui.PopItemWidth();
            //
            //            Imgui.EndTabItem();
            //        }
            //
            //        Imgui.EndTabBar();
            //    }
            //
            //    Imgui.PopItemWidth();
        }

        private static void ShowUserGuide()
        {
            var io = Imgui.GetIo();
            Imgui.BulletText("Double-click on title bar to collapse window.");
            Imgui.BulletText(
                "Click and drag on lower corner to resize window\n" +
                "(double-click to auto fit window to its contents).");
            Imgui.BulletText("CTRL+Click on a slider or drag box to input value as text.");
            Imgui.BulletText("TAB/SHIFT+TAB to cycle through keyboard editable fields.");
            Imgui.BulletText("CTRL+Tab to select a window.");
            if (io.FontAllowUserScaling)
            {
                Imgui.BulletText("CTRL+Mouse Wheel to zoom window contents.");
            }
            Imgui.BulletText("While inputing text:\n");
            Imgui.Indent();
            Imgui.BulletText("CTRL+Left/Right to word jump.");
            Imgui.BulletText("CTRL+A or double-click to select all.");
            Imgui.BulletText("CTRL+X/C/V to use clipboard cut/copy/paste.");
            Imgui.BulletText("CTRL+Z,CTRL+Y to undo/redo.");
            Imgui.BulletText("ESCAPE to revert.");
            Imgui.Unindent();
            Imgui.BulletText("With keyboard navigation enabled:");
            Imgui.Indent();
            Imgui.BulletText("Arrow keys to navigate.");
            Imgui.BulletText("Space to activate a widget.");
            Imgui.BulletText("Return to input text into a widget.");
            Imgui.BulletText("Escape to deactivate a widget, close popup, exit child window.");
            Imgui.BulletText("Alt to jump to the menu layer of a window.");
            Imgui.Unindent();
        }

        private static void ShowExampleAppMainMenuBar()
        {
            //    if (Imgui.BeginMainMenuBar())
            //    {
            //        if (Imgui.BeginMenu("File"))
            //        {
            //            ShowExampleMenuFile();
            //            Imgui.EndMenu();
            //        }
            //        if (Imgui.BeginMenu("Edit"))
            //        {
            //            if (Imgui.MenuItem("Undo", "CTRL+Z")) {}
            //            if (Imgui.MenuItem("Redo", "CTRL+Y", false, false)) {}  // Disabled item
            //            Imgui.Separator();
            //            if (Imgui.MenuItem("Cut", "CTRL+X")) {}
            //            if (Imgui.MenuItem("Copy", "CTRL+C")) {}
            //            if (Imgui.MenuItem("Paste", "CTRL+V")) {}
            //            Imgui.EndMenu();
            //        }
            //        Imgui.EndMainMenuBar();
            //    }
        }

        private static readonly State<bool> s_enabled = new(true);
        private static readonly State<float> s_f = new(0.5f);
        private static readonly State<int> s_n = new(0);
        private static readonly State<bool> s_b = new(true);

        private static void ShowExampleMenuFile()
        {
            _ = Imgui.MenuItem("(demo menu)", null, false, false);
            if (Imgui.MenuItem("New")) { }
            if (Imgui.MenuItem("Open", "Ctrl+O")) { }
            if (Imgui.BeginMenu("Open Recent"))
            {
                _ = Imgui.MenuItem("fish_hat.c");
                _ = Imgui.MenuItem("fish_hat.inl");
                _ = Imgui.MenuItem("fish_hat.h");
                if (Imgui.BeginMenu("More.."))
                {
                    _ = Imgui.MenuItem("Hello");
                    _ = Imgui.MenuItem("Sailor");
                    if (Imgui.BeginMenu("Recurse.."))
                    {
                        ShowExampleMenuFile();
                        Imgui.EndMenu();
                    }
                    Imgui.EndMenu();
                }
                Imgui.EndMenu();
            }
            if (Imgui.MenuItem("Save", "Ctrl+S")) { }
            if (Imgui.MenuItem("Save As..")) { }

            Imgui.Separator();
            if (Imgui.BeginMenu("Options"))
            {
                _ = Imgui.MenuItem("Enabled", "", s_enabled);
                _ = Imgui.BeginChild("child", new(0, 60), true);
                for (var i = 0; i < 10; i++)
                {
                    Imgui.Text($"Scrolling Text {i}");
                }
                Imgui.EndChild();
                _ = Imgui.Slider("Value", s_f, 0.0f, 1.0f);
                _ = Imgui.Input("Input", s_f, 0.1f);
                _ = Imgui.Combo("Combo", s_n, "Yes\0No\0Maybe\0\0");
                Imgui.EndMenu();
            }

            if (Imgui.BeginMenu("Colors"))
            {
                var sz = Imgui.GetTextLineHeight();
                for (StyleColor color = 0; color < StyleColor.Count; color++)
                {
                    var name = Imgui.GetStyleColorName(color);
                    var p = Imgui.GetCursorScreenPosition();
                    Imgui.GetWindowDrawList()?.AddRectFilled(new(p, new(p.X + sz, p.Y + sz)), Imgui.GetColor(color));
                    Imgui.Dummy(new(sz, sz));
                    Imgui.SameLine();
                    _ = Imgui.MenuItem(name);
                }

                Imgui.EndMenu();
            }

            if (Imgui.BeginMenu("Options"))
            {
                _ = Imgui.Checkbox("SomeOption", s_b);
                Imgui.EndMenu();
            }

            if (Imgui.BeginMenu("Disabled", false))
            {
                throw new InvalidOperationException();
            }

            if (Imgui.MenuItem("Checked", null, true)) { }
            if (Imgui.MenuItem("Quit", "Alt+F4")) { }
        }

        //struct ExampleAppConsole
        //{
        //    char                  InputBuf[256];
        //    ImVector<char*>       Items;
        //    ImVector<const char*> Commands;
        //    ImVector<char*>       History;
        //    int                   HistoryPos;    // -1: new line, 0..History.Size-1 browsing history.
        //    ImGuiTextFilter       Filter;
        //    bool                  AutoScroll;
        //    bool                  ScrollToBottom;
        //
        //    ExampleAppConsole()
        //    {
        //        IMGUI_DEMO_MARKER("Examples/Console");
        //        ClearLog();
        //        memset(InputBuf, 0, sizeof(InputBuf));
        //        HistoryPos = -1;
        //
        //        // "CLASSIFY" is here to provide the test case where "C"+[tab] completes to "CL" and display multiple matches.
        //        Commands.push_back("HELP");
        //        Commands.push_back("HISTORY");
        //        Commands.push_back("CLEAR");
        //        Commands.push_back("CLASSIFY");
        //        AutoScroll = true;
        //        ScrollToBottom = false;
        //        AddLog("Welcome to Dear ImGui!");
        //    }
        //    ~ExampleAppConsole()
        //    {
        //        ClearLog();
        //        for (int i = 0; i < History.Size; i++)
        //            free(History[i]);
        //    }
        //
        //    // Portable helpers
        //    static int   Stricmp(const char* s1, const char* s2)         { int d; while ((d = toupper(*s2) - toupper(*s1)) == 0 && *s1) { s1++; s2++; } return d; }
        //    static int   Strnicmp(const char* s1, const char* s2, int n) { int d = 0; while (n > 0 && (d = toupper(*s2) - toupper(*s1)) == 0 && *s1) { s1++; s2++; n--; } return d; }
        //    static char* Strdup(const char* s)                           { IM_ASSERT(s); size_t len = strlen(s) + 1; void* buf = malloc(len); IM_ASSERT(buf); return (char*)memcpy(buf, (const void*)s, len); }
        //    static void  Strtrim(char* s)                                { char* str_end = s + strlen(s); while (str_end > s && str_end[-1] == ' ') str_end--; *str_end = 0; }
        //
        //    void    ClearLog()
        //    {
        //        for (int i = 0; i < Items.Size; i++)
        //            free(Items[i]);
        //        Items.clear();
        //    }
        //
        //    void    AddLog(const char* fmt, ...) IM_FMTARGS(2)
        //    {
        //        // FIXME-OPT
        //        char buf[1024];
        //        va_list args;
        //        va_start(args, fmt);
        //        vsnprintf(buf, IM_ARRAYSIZE(buf), fmt, args);
        //        buf[IM_ARRAYSIZE(buf)-1] = 0;
        //        va_end(args);
        //        Items.push_back(Strdup(buf));
        //    }
        //
        //    void    Draw(const char* title, bool* p_open)
        //    {
        //        Imgui.SetNextWindowSize(ImVec2(520, 600), ImGuiCond_FirstUseEver);
        //        if (!Imgui.Begin(title, p_open))
        //        {
        //            Imgui.End();
        //            return;
        //        }
        //
        //        // As a specific feature guaranteed by the library, after calling Begin() the last Item represent the title bar.
        //        // So e.g. IsItemHovered() will return true when hovering the title bar.
        //        // Here we create a context menu only available from the title bar.
        //        if (Imgui.BeginPopupContextItem())
        //        {
        //            if (Imgui.MenuItem("Close Console"))
        //                *p_open = false;
        //            Imgui.EndPopup();
        //        }
        //
        //        Imgui.TextWrapped(
        //            "This example implements a console with basic coloring, completion (TAB key) and history (Up/Down keys). A more elaborate "
        //            "implementation may want to store entries along with extra data such as timestamp, emitter, etc.");
        //        Imgui.TextWrapped("Enter 'HELP' for help.");
        //
        //        // TODO: display items starting from the bottom
        //
        //        if (Imgui.SmallButton("Add Debug Text"))  { AddLog("%d some text", Items.Size); AddLog("some more text"); AddLog("display very important message here!"); }
        //        Imgui.SameLine();
        //        if (Imgui.SmallButton("Add Debug Error")) { AddLog("[error] something went wrong"); }
        //        Imgui.SameLine();
        //        if (Imgui.SmallButton("Clear"))           { ClearLog(); }
        //        Imgui.SameLine();
        //        bool copy_to_clipboard = Imgui.SmallButton("Copy");
        //        //static float t = 0.0f; if (Imgui.GetTime() - t > 0.02f) { t = Imgui.GetTime(); AddLog("Spam %f", t); }
        //
        //        Imgui.Separator();
        //
        //        // Options menu
        //        if (Imgui.BeginPopup("Options"))
        //        {
        //            Imgui.Checkbox("Auto-scroll", &AutoScroll);
        //            Imgui.EndPopup();
        //        }
        //
        //        // Options, Filter
        //        if (Imgui.Button("Options"))
        //            Imgui.OpenPopup("Options");
        //        Imgui.SameLine();
        //        Filter.Draw("Filter (\"incl,-excl\") (\"error\")", 180);
        //        Imgui.Separator();
        //
        //        // Reserve enough left-over height for 1 separator + 1 input text
        //        const float footer_height_to_reserve = Imgui.GetStyle().ItemSpacing.y + Imgui.GetFrameHeightWithSpacing();
        //        if (Imgui.BeginChild("ScrollingRegion", ImVec2(0, -footer_height_to_reserve), false, ImGuiWindowFlags_HorizontalScrollbar))
        //        {
        //            if (Imgui.BeginPopupContextWindow())
        //            {
        //                if (Imgui.Selectable("Clear")) ClearLog();
        //                Imgui.EndPopup();
        //            }
        //
        //            // Display every line as a separate entry so we can change their color or add custom widgets.
        //            // If you only want raw text you can use Imgui.TextUnformatted(log.begin(), log.end());
        //            // NB- if you have thousands of entries this approach may be too inefficient and may require user-side clipping
        //            // to only process visible items. The clipper will automatically measure the height of your first item and then
        //            // "seek" to display only items in the visible area.
        //            // To use the clipper we can replace your standard loop:
        //            //      for (int i = 0; i < Items.Size; i++)
        //            //   With:
        //            //      ImGuiListClipper clipper;
        //            //      clipper.Begin(Items.Size);
        //            //      while (clipper.Step())
        //            //         for (int i = clipper.DisplayStart; i < clipper.DisplayEnd; i++)
        //            // - That your items are evenly spaced (same height)
        //            // - That you have cheap random access to your elements (you can access them given their index,
        //            //   without processing all the ones before)
        //            // You cannot this code as-is if a filter is active because it breaks the 'cheap random-access' property.
        //            // We would need random-access on the post-filtered list.
        //            // A typical application wanting coarse clipping and filtering may want to pre-compute an array of indices
        //            // or offsets of items that passed the filtering test, recomputing this array when user changes the filter,
        //            // and appending newly elements as they are inserted. This is left as a task to the user until we can manage
        //            // to improve this example code!
        //            // If your items are of variable height:
        //            // - Split them into same height items would be simpler and facilitate random-seeking into your list.
        //            // - Consider using manual call to IsRectVisible() and skipping extraneous decoration from your items.
        //            Imgui.PushStyleVar(ImGuiStyleVar_ItemSpacing, ImVec2(4, 1)); // Tighten spacing
        //            if (copy_to_clipboard)
        //                Imgui.LogToClipboard();
        //            for (int i = 0; i < Items.Size; i++)
        //            {
        //                const char* item = Items[i];
        //                if (!Filter.PassFilter(item))
        //                    continue;
        //
        //                // Normally you would store more information in your item than just a string.
        //                // (e.g. make Items[] an array of structure, store color/type etc.)
        //                ImVec4 color;
        //                bool has_color = false;
        //                if (strstr(item, "[error]")) { color = ImVec4(1.0f, 0.4f, 0.4f, 1.0f); has_color = true; }
        //                else if (strncmp(item, "# ", 2) == 0) { color = ImVec4(1.0f, 0.8f, 0.6f, 1.0f); has_color = true; }
        //                if (has_color)
        //                    Imgui.PushStyleColor(ImGuiCol_Text, color);
        //                Imgui.TextUnformatted(item);
        //                if (has_color)
        //                    Imgui.PopStyleColor();
        //            }
        //            if (copy_to_clipboard)
        //                Imgui.LogFinish();
        //
        //            // Keep up at the bottom of the scroll region if we were already at the bottom at the beginning of the frame.
        //            // Using a scrollbar or mouse-wheel will take away from the bottom edge.
        //            if (ScrollToBottom || (AutoScroll && Imgui.GetScrollY() >= Imgui.GetScrollMaxY()))
        //                Imgui.SetScrollHereY(1.0f);
        //            ScrollToBottom = false;
        //
        //            Imgui.PopStyleVar();
        //        }
        //        Imgui.EndChild();
        //        Imgui.Separator();
        //
        //        // Command-line
        //        bool reclaim_focus = false;
        //        ImGuiInputTextFlags input_text_flags = ImGuiInputTextFlags_EnterReturnsTrue | ImGuiInputTextFlags_EscapeClearsAll | ImGuiInputTextFlags_CallbackCompletion | ImGuiInputTextFlags_CallbackHistory;
        //        if (Imgui.InputText("Input", InputBuf, IM_ARRAYSIZE(InputBuf), input_text_flags, &TextEditCallbackStub, (void*)this))
        //        {
        //            char* s = InputBuf;
        //            Strtrim(s);
        //            if (s[0])
        //                ExecCommand(s);
        //            strcpy(s, "");
        //            reclaim_focus = true;
        //        }
        //
        //        // Auto-focus on window apparition
        //        Imgui.SetItemDefaultFocus();
        //        if (reclaim_focus)
        //            Imgui.SetKeyboardFocusHere(-1); // Auto focus previous widget
        //
        //        Imgui.End();
        //    }
        //
        //    void    ExecCommand(const char* command_line)
        //    {
        //        AddLog("# %s\n", command_line);
        //
        //        // Insert into history. First find match and delete it so it can be pushed to the back.
        //        // This isn't trying to be smart or optimal.
        //        HistoryPos = -1;
        //        for (int i = History.Size - 1; i >= 0; i--)
        //            if (Stricmp(History[i], command_line) == 0)
        //            {
        //                free(History[i]);
        //                History.erase(History.begin() + i);
        //                break;
        //            }
        //        History.push_back(Strdup(command_line));
        //
        //        // Process command
        //        if (Stricmp(command_line, "CLEAR") == 0)
        //        {
        //            ClearLog();
        //        }
        //        else if (Stricmp(command_line, "HELP") == 0)
        //        {
        //            AddLog("Commands:");
        //            for (int i = 0; i < Commands.Size; i++)
        //                AddLog("- %s", Commands[i]);
        //        }
        //        else if (Stricmp(command_line, "HISTORY") == 0)
        //        {
        //            int first = History.Size - 10;
        //            for (int i = first > 0 ? first : 0; i < History.Size; i++)
        //                AddLog("%3d: %s\n", i, History[i]);
        //        }
        //        else
        //        {
        //            AddLog("Unknown command: '%s'\n", command_line);
        //        }
        //
        //        // On command input, we scroll to bottom even if AutoScroll==false
        //        ScrollToBottom = true;
        //    }
        //
        //    // In C++11 you'd be better off using lambdas for this sort of forwarding callbacks
        //    static int TextEditCallbackStub(ImGuiInputTextCallbackData* data)
        //    {
        //        ExampleAppConsole* console = (ExampleAppConsole*)data->UserData;
        //        return console->TextEditCallback(data);
        //    }
        //
        //    int     TextEditCallback(ImGuiInputTextCallbackData* data)
        //    {
        //        //AddLog("cursor: %d, selection: %d-%d", data->CursorPos, data->SelectionStart, data->SelectionEnd);
        //        switch (data->EventFlag)
        //        {
        //        case ImGuiInputTextFlags_CallbackCompletion:
        //            {
        //                // Example of TEXT COMPLETION
        //
        //                // Locate beginning of current word
        //                const char* word_end = data->Buf + data->CursorPos;
        //                const char* word_start = word_end;
        //                while (word_start > data->Buf)
        //                {
        //                    const char c = word_start[-1];
        //                    if (c == ' ' || c == '\t' || c == ',' || c == ';')
        //                        break;
        //                    word_start--;
        //                }
        //
        //                // Build a list of candidates
        //                ImVector<const char*> candidates;
        //                for (int i = 0; i < Commands.Size; i++)
        //                    if (Strnicmp(Commands[i], word_start, (int)(word_end - word_start)) == 0)
        //                        candidates.push_back(Commands[i]);
        //
        //                if (candidates.Size == 0)
        //                {
        //                    // No match
        //                    AddLog("No match for \"%.*s\"!\n", (int)(word_end - word_start), word_start);
        //                }
        //                else if (candidates.Size == 1)
        //                {
        //                    // Single match. Delete the beginning of the word and replace it entirely so we've got nice casing.
        //                    data->DeleteChars((int)(word_start - data->Buf), (int)(word_end - word_start));
        //                    data->InsertChars(data->CursorPos, candidates[0]);
        //                    data->InsertChars(data->CursorPos, " ");
        //                }
        //                else
        //                {
        //                    // Multiple matches. Complete as much as we can..
        //                    // So inputing "C"+Tab will complete to "CL" then display "CLEAR" and "CLASSIFY" as matches.
        //                    int match_len = (int)(word_end - word_start);
        //                    for (;;)
        //                    {
        //                        int c = 0;
        //                        bool all_candidates_matches = true;
        //                        for (int i = 0; i < candidates.Size && all_candidates_matches; i++)
        //                            if (i == 0)
        //                                c = toupper(candidates[i][match_len]);
        //                            else if (c == 0 || c != toupper(candidates[i][match_len]))
        //                                all_candidates_matches = false;
        //                        if (!all_candidates_matches)
        //                            break;
        //                        match_len++;
        //                    }
        //
        //                    if (match_len > 0)
        //                    {
        //                        data->DeleteChars((int)(word_start - data->Buf), (int)(word_end - word_start));
        //                        data->InsertChars(data->CursorPos, candidates[0], candidates[0] + match_len);
        //                    }
        //
        //                    // List matches
        //                    AddLog("Possible matches:\n");
        //                    for (int i = 0; i < candidates.Size; i++)
        //                        AddLog("- %s\n", candidates[i]);
        //                }
        //
        //                break;
        //            }
        //        case ImGuiInputTextFlags_CallbackHistory:
        //            {
        //                // Example of HISTORY
        //                const int prev_history_pos = HistoryPos;
        //                if (data->EventKey == ImGuiKey_UpArrow)
        //                {
        //                    if (HistoryPos == -1)
        //                        HistoryPos = History.Size - 1;
        //                    else if (HistoryPos > 0)
        //                        HistoryPos--;
        //                }
        //                else if (data->EventKey == ImGuiKey_DownArrow)
        //                {
        //                    if (HistoryPos != -1)
        //                        if (++HistoryPos >= History.Size)
        //                            HistoryPos = -1;
        //                }
        //
        //                // A better implementation would preserve the data on the current input line along with cursor position.
        //                if (prev_history_pos != HistoryPos)
        //                {
        //                    const char* history_str = (HistoryPos >= 0) ? History[HistoryPos] : "";
        //                    data->DeleteChars(0, data->BufTextLen);
        //                    data->InsertChars(0, history_str);
        //                }
        //            }
        //        }
        //        return 0;
        //    }
        //};

        private static void ShowExampleAppConsole(State<bool> p_open)
        {
            //    static ExampleAppConsole console;
            //    console.Draw("Example: Console", p_open);
        }

        //// Usage:
        ////  static ExampleAppLog my_log;
        ////  my_log.AddLog("Hello %d world\n", 123);
        ////  my_log.Draw("title");
        //struct ExampleAppLog
        //{
        //    ImGuiTextBuffer     Buf;
        //    ImGuiTextFilter     Filter;
        //    ImVector<int>       LineOffsets; // Index to lines offset. We maintain this with AddLog() calls.
        //    bool                AutoScroll;  // Keep scrolling if already at the bottom.
        //
        //    ExampleAppLog()
        //    {
        //        AutoScroll = true;
        //        Clear();
        //    }
        //
        //    void    Clear()
        //    {
        //        Buf.clear();
        //        LineOffsets.clear();
        //        LineOffsets.push_back(0);
        //    }
        //
        //    void    AddLog(const char* fmt, ...) IM_FMTARGS(2)
        //    {
        //        int old_size = Buf.size();
        //        va_list args;
        //        va_start(args, fmt);
        //        Buf.appendfv(fmt, args);
        //        va_end(args);
        //        for (int new_size = Buf.size(); old_size < new_size; old_size++)
        //            if (Buf[old_size] == '\n')
        //                LineOffsets.push_back(old_size + 1);
        //    }
        //
        //    void    Draw(const char* title, bool* p_open = NULL)
        //    {
        //        if (!Imgui.Begin(title, p_open))
        //        {
        //            Imgui.End();
        //            return;
        //        }
        //
        //        // Options menu
        //        if (Imgui.BeginPopup("Options"))
        //        {
        //            Imgui.Checkbox("Auto-scroll", &AutoScroll);
        //            Imgui.EndPopup();
        //        }
        //
        //        // Main window
        //        if (Imgui.Button("Options"))
        //            Imgui.OpenPopup("Options");
        //        Imgui.SameLine();
        //        bool clear = Imgui.Button("Clear");
        //        Imgui.SameLine();
        //        bool copy = Imgui.Button("Copy");
        //        Imgui.SameLine();
        //        Filter.Draw("Filter", -100.0f);
        //
        //        Imgui.Separator();
        //
        //        if (Imgui.BeginChild("scrolling", ImVec2(0, 0), false, ImGuiWindowFlags_HorizontalScrollbar))
        //        {
        //            if (clear)
        //                Clear();
        //            if (copy)
        //                Imgui.LogToClipboard();
        //
        //            Imgui.PushStyleVar(ImGuiStyleVar_ItemSpacing, ImVec2(0, 0));
        //            const char* buf = Buf.begin();
        //            const char* buf_end = Buf.end();
        //            if (Filter.IsActive())
        //            {
        //                // In this example we don't use the clipper when Filter is enabled.
        //                // This is because we don't have random access to the result of our filter.
        //                // A real application processing logs with ten of thousands of entries may want to store the result of
        //                // search/filter.. especially if the filtering function is not trivial (e.g. reg-exp).
        //                for (int line_no = 0; line_no < LineOffsets.Size; line_no++)
        //                {
        //                    const char* line_start = buf + LineOffsets[line_no];
        //                    const char* line_end = (line_no + 1 < LineOffsets.Size) ? (buf + LineOffsets[line_no + 1] - 1) : buf_end;
        //                    if (Filter.PassFilter(line_start, line_end))
        //                        Imgui.TextUnformatted(line_start, line_end);
        //                }
        //            }
        //            else
        //            {
        //                // The simplest and easy way to display the entire buffer:
        //                //   Imgui.TextUnformatted(buf_begin, buf_end);
        //                // And it'll just work. TextUnformatted() has specialization for large blob of text and will fast-forward
        //                // to skip non-visible lines. Here we instead demonstrate using the clipper to only process lines that are
        //                // within the visible area.
        //                // If you have tens of thousands of items and their processing cost is non-negligible, coarse clipping them
        //                // on your side is recommended. Using ImGuiListClipper requires
        //                // - A) random access into your data
        //                // - B) items all being the  same height,
        //                // both of which we can handle since we have an array pointing to the beginning of each line of text.
        //                // When using the filter (in the block of code above) we don't have random access into the data to display
        //                // anymore, which is why we don't use the clipper. Storing or skimming through the search result would make
        //                // it possible (and would be recommended if you want to search through tens of thousands of entries).
        //                ImGuiListClipper clipper;
        //                clipper.Begin(LineOffsets.Size);
        //                while (clipper.Step())
        //                {
        //                    for (int line_no = clipper.DisplayStart; line_no < clipper.DisplayEnd; line_no++)
        //                    {
        //                        const char* line_start = buf + LineOffsets[line_no];
        //                        const char* line_end = (line_no + 1 < LineOffsets.Size) ? (buf + LineOffsets[line_no + 1] - 1) : buf_end;
        //                        Imgui.TextUnformatted(line_start, line_end);
        //                    }
        //                }
        //                clipper.End();
        //            }
        //            Imgui.PopStyleVar();
        //
        //            // Keep up at the bottom of the scroll region if we were already at the bottom at the beginning of the frame.
        //            // Using a scrollbar or mouse-wheel will take away from the bottom edge.
        //            if (AutoScroll && Imgui.GetScrollY() >= Imgui.GetScrollMaxY())
        //                Imgui.SetScrollHereY(1.0f);
        //        }
        //        Imgui.EndChild();
        //        Imgui.End();
        //    }
        //};

        private static void ShowExampleAppLog(State<bool> p_open)
        {
            //    static ExampleAppLog log;
            //
            //    // For the demo: add a debug button _BEFORE_ the normal log window contents
            //    // We take advantage of a rarely used feature: multiple calls to Begin()/End() are appending to the _same_ window.
            //    // Most of the contents of the window will be added by the log.Draw() call.
            //    Imgui.SetNextWindowSize(ImVec2(500, 400), ImGuiCond_FirstUseEver);
            //    Imgui.Begin("Example: Log", p_open);
            //    IMGUI_DEMO_MARKER("Examples/Log");
            //    if (Imgui.SmallButton("[Debug] Add 5 entries"))
            //    {
            //        static int counter = 0;
            //        const char* categories[3] = { "info", "warn", "error" };
            //        const char* words[] = { "Bumfuzzled", "Cattywampus", "Snickersnee", "Abibliophobia", "Absquatulate", "Nincompoop", "Pauciloquent" };
            //        for (int n = 0; n < 5; n++)
            //        {
            //            const char* category = categories[counter % IM_ARRAYSIZE(categories)];
            //            const char* word = words[counter % IM_ARRAYSIZE(words)];
            //            log.AddLog("[%05d] [%s] Hello, current time is %.1f, here's a word: '%s'\n",
            //                Imgui.GetFrameCount(), category, Imgui.GetTime(), word);
            //            counter++;
            //        }
            //    }
            //    Imgui.End();
            //
            //    // Actually call in the regular Log helper (which will Begin() into the same window as we just did)
            //    log.Draw("Example: Log", p_open);
        }

        private static void ShowExampleAppLayout(State<bool> p_open)
        {
            //    Imgui.SetNextWindowSize(ImVec2(500, 440), ImGuiCond_FirstUseEver);
            //    if (Imgui.Begin("Example: Simple layout", p_open, ImGuiWindowFlags_MenuBar))
            //    {
            //        IMGUI_DEMO_MARKER("Examples/Simple layout");
            //        if (Imgui.BeginMenuBar())
            //        {
            //            if (Imgui.BeginMenu("File"))
            //            {
            //                if (Imgui.MenuItem("Close", "Ctrl+W")) { *p_open = false; }
            //                Imgui.EndMenu();
            //            }
            //            Imgui.EndMenuBar();
            //        }
            //
            //        // Left
            //        static int selected = 0;
            //        {
            //            Imgui.BeginChild("left pane", ImVec2(150, 0), true);
            //            for (int i = 0; i < 100; i++)
            //            {
            //                // FIXME: Good candidate to use ImGuiSelectableFlags_SelectOnNav
            //                char label[128];
            //                sprintf(label, "MyObject %d", i);
            //                if (Imgui.Selectable(label, selected == i))
            //                    selected = i;
            //            }
            //            Imgui.EndChild();
            //        }
            //        Imgui.SameLine();
            //
            //        // Right
            //        {
            //            Imgui.BeginGroup();
            //            Imgui.BeginChild("item view", ImVec2(0, -Imgui.GetFrameHeightWithSpacing())); // Leave room for 1 line below us
            //            Imgui.Text("MyObject: %d", selected);
            //            Imgui.Separator();
            //            if (Imgui.BeginTabBar("##Tabs", ImGuiTabBarFlags_None))
            //            {
            //                if (Imgui.BeginTabItem("Description"))
            //                {
            //                    Imgui.TextWrapped("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ");
            //                    Imgui.EndTabItem();
            //                }
            //                if (Imgui.BeginTabItem("Details"))
            //                {
            //                    Imgui.Text("ID: 0123456789");
            //                    Imgui.EndTabItem();
            //                }
            //                Imgui.EndTabBar();
            //            }
            //            Imgui.EndChild();
            //            if (Imgui.Button("Revert")) {}
            //            Imgui.SameLine();
            //            if (Imgui.Button("Save")) {}
            //            Imgui.EndGroup();
            //        }
            //    }
            //    Imgui.End();
        }

        //static void ShowPlaceholderObject(const char* prefix, int uid)
        //{
        //    // Use object uid as identifier. Most commonly you could also use the object pointer as a base ID.
        //    Imgui.PushID(uid);
        //
        //    // Text and Tree nodes are less high than framed widgets, using AlignTextToFramePadding() we add vertical spacing to make the tree lines equal high.
        //    Imgui.TableNextRow();
        //    Imgui.TableSetColumnIndex(0);
        //    Imgui.AlignTextToFramePadding();
        //    bool node_open = Imgui.TreeNode("Object", "%s_%u", prefix, uid);
        //    Imgui.TableSetColumnIndex(1);
        //    Imgui.Text("my sailor is rich");
        //
        //    if (node_open)
        //    {
        //        static float placeholder_members[8] = { 0.0f, 0.0f, 1.0f, 3.1416f, 100.0f, 999.0f };
        //        for (int i = 0; i < 8; i++)
        //        {
        //            Imgui.PushID(i); // Use field index as identifier.
        //            if (i < 2)
        //            {
        //                ShowPlaceholderObject("Child", 424242);
        //            }
        //            else
        //            {
        //                // Here we use a TreeNode to highlight on hover (we could use e.g. Selectable as well)
        //                Imgui.TableNextRow();
        //                Imgui.TableSetColumnIndex(0);
        //                Imgui.AlignTextToFramePadding();
        //                ImGuiTreeNodeFlags flags = ImGuiTreeNodeFlags_Leaf | ImGuiTreeNodeFlags_NoTreePushOnOpen | ImGuiTreeNodeFlags_Bullet;
        //                Imgui.TreeNodeEx("Field", flags, "Field_%d", i);
        //
        //                Imgui.TableSetColumnIndex(1);
        //                Imgui.SetNextItemWidth(-FLT_MIN);
        //                if (i >= 5)
        //                    Imgui.InputFloat("##value", &placeholder_members[i], 1.0f);
        //                else
        //                    Imgui.DragFloat("##value", &placeholder_members[i], 0.01f);
        //                Imgui.NextColumn();
        //            }
        //            Imgui.PopID();
        //        }
        //        Imgui.TreePop();
        //    }
        //    Imgui.PopID();
        //}

        private static void ShowExampleAppPropertyEditor(State<bool> p_open)
        {
            //    Imgui.SetNextWindowSize(ImVec2(430, 450), ImGuiCond_FirstUseEver);
            //    if (!Imgui.Begin("Example: Property editor", p_open))
            //    {
            //        Imgui.End();
            //        return;
            //    }
            //    IMGUI_DEMO_MARKER("Examples/Property Editor");
            //
            //    HelpMarker(
            //        "This example shows how you may implement a property editor using two columns.\n"
            //        "All objects/fields data are dummies here.\n"
            //        "Remember that in many simple cases, you can use Imgui.SameLine(xxx) to position\n"
            //        "your cursor horizontally instead of using the Columns() API.");
            //
            //    Imgui.PushStyleVar(ImGuiStyleVar_FramePadding, ImVec2(2, 2));
            //    if (Imgui.BeginTable("split", 2, ImGuiTableFlags_BordersOuter | ImGuiTableFlags_Resizable))
            //    {
            //        // Iterate placeholder objects (all the same data)
            //        for (int obj_i = 0; obj_i < 4; obj_i++)
            //        {
            //            ShowPlaceholderObject("Object", obj_i);
            //            //Imgui.Separator();
            //        }
            //        Imgui.EndTable();
            //    }
            //    Imgui.PopStyleVar();
            //    Imgui.End();
        }

        // Demonstrate/test rendering huge amount of text, and the incidence of clipping.
        private static void ShowExampleAppLongText(State<bool> p_open)
        {
            //    Imgui.SetNextWindowSize(ImVec2(520, 600), ImGuiCond_FirstUseEver);
            //    if (!Imgui.Begin("Example: Long text display", p_open))
            //    {
            //        Imgui.End();
            //        return;
            //    }
            //    IMGUI_DEMO_MARKER("Examples/Long text display");
            //
            //    static int test_type = 0;
            //    static ImGuiTextBuffer log;
            //    static int lines = 0;
            //    Imgui.Text("Printing unusually long amount of text.");
            //    Imgui.Combo("Test type", &test_type,
            //        "Single call to TextUnformatted()\0"
            //        "Multiple calls to Text(), clipped\0"
            //        "Multiple calls to Text(), not clipped (slow)\0");
            //    Imgui.Text("Buffer contents: %d lines, %d bytes", lines, log.size());
            //    if (Imgui.Button("Clear")) { log.clear(); lines = 0; }
            //    Imgui.SameLine();
            //    if (Imgui.Button("Add 1000 lines"))
            //    {
            //        for (int i = 0; i < 1000; i++)
            //            log.appendf("%i The quick brown fox jumps over the lazy dog\n", lines + i);
            //        lines += 1000;
            //    }
            //    Imgui.BeginChild("Log");
            //    switch (test_type)
            //    {
            //    case 0:
            //        // Single call to TextUnformatted() with a big buffer
            //        Imgui.TextUnformatted(log.begin(), log.end());
            //        break;
            //    case 1:
            //        {
            //            // Multiple calls to Text(), manually coarsely clipped - demonstrate how to use the ImGuiListClipper helper.
            //            Imgui.PushStyleVar(ImGuiStyleVar_ItemSpacing, ImVec2(0, 0));
            //            ImGuiListClipper clipper;
            //            clipper.Begin(lines);
            //            while (clipper.Step())
            //                for (int i = clipper.DisplayStart; i < clipper.DisplayEnd; i++)
            //                    Imgui.Text("%i The quick brown fox jumps over the lazy dog", i);
            //            Imgui.PopStyleVar();
            //            break;
            //        }
            //    case 2:
            //        // Multiple calls to Text(), not clipped (slow)
            //        Imgui.PushStyleVar(ImGuiStyleVar_ItemSpacing, ImVec2(0, 0));
            //        for (int i = 0; i < lines; i++)
            //            Imgui.Text("%i The quick brown fox jumps over the lazy dog", i);
            //        Imgui.PopStyleVar();
            //        break;
            //    }
            //    Imgui.EndChild();
            //    Imgui.End();
        }

        private static void ShowExampleAppAutoResize(State<bool> p_open)
        {
            //    if (!Imgui.Begin("Example: Auto-resizing window", p_open, ImGuiWindowFlags_AlwaysAutoResize))
            //    {
            //        Imgui.End();
            //        return;
            //    }
            //    IMGUI_DEMO_MARKER("Examples/Auto-resizing window");
            //
            //    static int lines = 10;
            //    Imgui.TextUnformatted(
            //        "Window will resize every-frame to the size of its content.\n"
            //        "Note that you probably don't want to query the window size to\n"
            //        "output your content because that would create a feedback loop.");
            //    Imgui.SliderInt("Number of lines", &lines, 1, 20);
            //    for (int i = 0; i < lines; i++)
            //        Imgui.Text("%*sThis is line %d", i * 4, "", i); // Pad with space to extend size horizontally
            //    Imgui.End();
        }

        private static void ShowExampleAppConstrainedResize(State<bool> p_open)
        {
            //    struct CustomConstraints
            //    {
            //        // Helper functions to demonstrate programmatic constraints
            //        // FIXME: This doesn't take account of decoration size (e.g. title bar), library should make this easier.
            //        static void AspectRatio(ImGuiSizeCallbackData* data)    { float aspect_ratio = *(float*)data->UserData; data->DesiredSize.x = IM_MAX(data->CurrentSize.x, data->CurrentSize.y); data->DesiredSize.y = (float)(int)(data->DesiredSize.x / aspect_ratio); }
            //        static void Square(ImGuiSizeCallbackData* data)         { data->DesiredSize.x = data->DesiredSize.y = IM_MAX(data->CurrentSize.x, data->CurrentSize.y); }
            //        static void Step(ImGuiSizeCallbackData* data)           { float step = *(float*)data->UserData; data->DesiredSize = ImVec2((int)(data->CurrentSize.x / step + 0.5f) * step, (int)(data->CurrentSize.y / step + 0.5f) * step); }
            //    };
            //
            //    const char* test_desc[] =
            //    {
            //        "Between 100x100 and 500x500",
            //        "At least 100x100",
            //        "Resize vertical only",
            //        "Resize horizontal only",
            //        "Width Between 400 and 500",
            //        "Custom: Aspect Ratio 16:9",
            //        "Custom: Always Square",
            //        "Custom: Fixed Steps (100)",
            //    };
            //
            //    // Options
            //    static bool auto_resize = false;
            //    static bool window_padding = true;
            //    static int type = 5; // Aspect Ratio
            //    static int display_lines = 10;
            //
            //    // Submit constraint
            //    float aspect_ratio = 16.0f / 9.0f;
            //    float fixed_step = 100.0f;
            //    if (type == 0) Imgui.SetNextWindowSizeConstraints(ImVec2(100, 100), ImVec2(500, 500));         // Between 100x100 and 500x500
            //    if (type == 1) Imgui.SetNextWindowSizeConstraints(ImVec2(100, 100), ImVec2(FLT_MAX, FLT_MAX)); // Width > 100, Height > 100
            //    if (type == 2) Imgui.SetNextWindowSizeConstraints(ImVec2(-1, 0),    ImVec2(-1, FLT_MAX));      // Vertical only
            //    if (type == 3) Imgui.SetNextWindowSizeConstraints(ImVec2(0, -1),    ImVec2(FLT_MAX, -1));      // Horizontal only
            //    if (type == 4) Imgui.SetNextWindowSizeConstraints(ImVec2(400, -1),  ImVec2(500, -1));          // Width Between and 400 and 500
            //    if (type == 5) Imgui.SetNextWindowSizeConstraints(ImVec2(0, 0),     ImVec2(FLT_MAX, FLT_MAX), CustomConstraints::AspectRatio, (void*)&aspect_ratio);   // Aspect ratio
            //    if (type == 6) Imgui.SetNextWindowSizeConstraints(ImVec2(0, 0),     ImVec2(FLT_MAX, FLT_MAX), CustomConstraints::Square);                              // Always Square
            //    if (type == 7) Imgui.SetNextWindowSizeConstraints(ImVec2(0, 0),     ImVec2(FLT_MAX, FLT_MAX), CustomConstraints::Step, (void*)&fixed_step);            // Fixed Step
            //
            //    // Submit window
            //    if (!window_padding)
            //        Imgui.PushStyleVar(ImGuiStyleVar_WindowPadding, ImVec2(0.0f, 0.0f));
            //    const ImGuiWindowFlags window_flags = auto_resize ? ImGuiWindowFlags_AlwaysAutoResize : 0;
            //    const bool window_open = Imgui.Begin("Example: Constrained Resize", p_open, window_flags);
            //    if (!window_padding)
            //        Imgui.PopStyleVar();
            //    if (window_open)
            //    {
            //        IMGUI_DEMO_MARKER("Examples/Constrained Resizing window");
            //        if (Imgui.GetIO().KeyShift)
            //        {
            //            // Display a dummy viewport (in your real app you would likely use ImageButton() to display a texture.
            //            ImVec2 avail_size = Imgui.GetContentRegionAvail();
            //            ImVec2 pos = Imgui.GetCursorScreenPos();
            //            Imgui.ColorButton("viewport", ImVec4(0.5f, 0.2f, 0.5f, 1.0f), ImGuiColorEditFlags_NoTooltip | ImGuiColorEditFlags_NoDragDrop, avail_size);
            //            Imgui.SetCursorScreenPos(ImVec2(pos.x + 10, pos.y + 10));
            //            Imgui.Text("%.2f x %.2f", avail_size.x, avail_size.y);
            //        }
            //        else
            //        {
            //            Imgui.Text("(Hold SHIFT to display a dummy viewport)");
            //            if (Imgui.Button("Set 200x200")) { Imgui.SetWindowSize(ImVec2(200, 200)); } Imgui.SameLine();
            //            if (Imgui.Button("Set 500x500")) { Imgui.SetWindowSize(ImVec2(500, 500)); } Imgui.SameLine();
            //            if (Imgui.Button("Set 800x200")) { Imgui.SetWindowSize(ImVec2(800, 200)); }
            //            Imgui.SetNextItemWidth(Imgui.GetFontSize() * 20);
            //            Imgui.Combo("Constraint", &type, test_desc, IM_ARRAYSIZE(test_desc));
            //            Imgui.SetNextItemWidth(Imgui.GetFontSize() * 20);
            //            Imgui.DragInt("Lines", &display_lines, 0.2f, 1, 100);
            //            Imgui.Checkbox("Auto-resize", &auto_resize);
            //            Imgui.Checkbox("Window padding", &window_padding);
            //            for (int i = 0; i < display_lines; i++)
            //                Imgui.Text("%*sHello, sailor! Making this line long enough for the example.", i * 4, "");
            //        }
            //    }
            //    Imgui.End();
        }

        private static void ShowExampleAppSimpleOverlay(State<bool> p_open)
        {
            //    static int location = 0;
            //    ImGuiIO& io = Imgui.GetIO();
            //    ImGuiWindowFlags window_flags = ImGuiWindowFlags_NoDecoration | ImGuiWindowFlags_AlwaysAutoResize | ImGuiWindowFlags_NoSavedSettings | ImGuiWindowFlags_NoFocusOnAppearing | ImGuiWindowFlags_NoNav;
            //    if (location >= 0)
            //    {
            //        const float PAD = 10.0f;
            //        const ImGuiViewport* viewport = Imgui.GetMainViewport();
            //        ImVec2 work_pos = viewport->WorkPos; // Use work area to avoid menu-bar/task-bar, if any!
            //        ImVec2 work_size = viewport->WorkSize;
            //        ImVec2 window_pos, window_pos_pivot;
            //        window_pos.x = (location & 1) ? (work_pos.x + work_size.x - PAD) : (work_pos.x + PAD);
            //        window_pos.y = (location & 2) ? (work_pos.y + work_size.y - PAD) : (work_pos.y + PAD);
            //        window_pos_pivot.x = (location & 1) ? 1.0f : 0.0f;
            //        window_pos_pivot.y = (location & 2) ? 1.0f : 0.0f;
            //        Imgui.SetNextWindowPos(window_pos, ImGuiCond_Always, window_pos_pivot);
            //        window_flags |= ImGuiWindowFlags_NoMove;
            //    }
            //    else if (location == -2)
            //    {
            //        // Center window
            //        Imgui.SetNextWindowPos(Imgui.GetMainViewport()->GetCenter(), ImGuiCond_Always, ImVec2(0.5f, 0.5f));
            //        window_flags |= ImGuiWindowFlags_NoMove;
            //    }
            //    Imgui.SetNextWindowBgAlpha(0.35f); // Transparent background
            //    if (Imgui.Begin("Example: Simple overlay", p_open, window_flags))
            //    {
            //        IMGUI_DEMO_MARKER("Examples/Simple Overlay");
            //        Imgui.Text("Simple overlay\n" "(right-click to change position)");
            //        Imgui.Separator();
            //        if (Imgui.IsMousePosValid())
            //            Imgui.Text("Mouse Position: (%.1f,%.1f)", io.MousePos.x, io.MousePos.y);
            //        else
            //            Imgui.Text("Mouse Position: <invalid>");
            //        if (Imgui.BeginPopupContextWindow())
            //        {
            //            if (Imgui.MenuItem("Custom",       NULL, location == -1)) location = -1;
            //            if (Imgui.MenuItem("Center",       NULL, location == -2)) location = -2;
            //            if (Imgui.MenuItem("Top-left",     NULL, location == 0)) location = 0;
            //            if (Imgui.MenuItem("Top-right",    NULL, location == 1)) location = 1;
            //            if (Imgui.MenuItem("Bottom-left",  NULL, location == 2)) location = 2;
            //            if (Imgui.MenuItem("Bottom-right", NULL, location == 3)) location = 3;
            //            if (p_open && Imgui.MenuItem("Close")) *p_open = false;
            //            Imgui.EndPopup();
            //        }
            //    }
            //    Imgui.End();
        }

        private static void ShowExampleAppFullscreen(State<bool> p_open)
        {
            //    static bool use_work_area = true;
            //    static ImGuiWindowFlags flags = ImGuiWindowFlags_NoDecoration | ImGuiWindowFlags_NoMove | ImGuiWindowFlags_NoSavedSettings;
            //
            //    // We demonstrate using the full viewport area or the work area (without menu-bars, task-bars etc.)
            //    // Based on your use case you may want one of the other.
            //    const ImGuiViewport* viewport = Imgui.GetMainViewport();
            //    Imgui.SetNextWindowPos(use_work_area ? viewport->WorkPos : viewport->Pos);
            //    Imgui.SetNextWindowSize(use_work_area ? viewport->WorkSize : viewport->Size);
            //
            //    if (Imgui.Begin("Example: Fullscreen window", p_open, flags))
            //    {
            //        Imgui.Checkbox("Use work area instead of main area", &use_work_area);
            //        Imgui.SameLine();
            //        HelpMarker("Main Area = entire viewport,\nWork Area = entire viewport minus sections used by the main menu bars, task bars etc.\n\nEnable the main-menu bar in Examples menu to see the difference.");
            //
            //        Imgui.CheckboxFlags("ImGuiWindowFlags_NoBackground", &flags, ImGuiWindowFlags_NoBackground);
            //        Imgui.CheckboxFlags("ImGuiWindowFlags_NoDecoration", &flags, ImGuiWindowFlags_NoDecoration);
            //        Imgui.Indent();
            //        Imgui.CheckboxFlags("ImGuiWindowFlags_NoTitleBar", &flags, ImGuiWindowFlags_NoTitleBar);
            //        Imgui.CheckboxFlags("ImGuiWindowFlags_NoCollapse", &flags, ImGuiWindowFlags_NoCollapse);
            //        Imgui.CheckboxFlags("ImGuiWindowFlags_NoScrollbar", &flags, ImGuiWindowFlags_NoScrollbar);
            //        Imgui.Unindent();
            //
            //        if (p_open && Imgui.Button("Close this window"))
            //            *p_open = false;
            //    }
            //    Imgui.End();
        }

        private static void ShowExampleAppWindowTitles(State<bool> unused)
        {
            //    const ImGuiViewport* viewport = Imgui.GetMainViewport();
            //    const ImVec2 base_pos = viewport->Pos;
            //
            //    // By default, Windows are uniquely identified by their title.
            //    // You can use the "##" and "###" markers to manipulate the display/ID.
            //
            //    // Using "##" to display same title but have unique identifier.
            //    Imgui.SetNextWindowPos(ImVec2(base_pos.x + 100, base_pos.y + 100), ImGuiCond_FirstUseEver);
            //    Imgui.Begin("Same title as another window##1");
            //    IMGUI_DEMO_MARKER("Examples/Manipulating window titles");
            //    Imgui.Text("This is window 1.\nMy title is the same as window 2, but my identifier is unique.");
            //    Imgui.End();
            //
            //    Imgui.SetNextWindowPos(ImVec2(base_pos.x + 100, base_pos.y + 200), ImGuiCond_FirstUseEver);
            //    Imgui.Begin("Same title as another window##2");
            //    Imgui.Text("This is window 2.\nMy title is the same as window 1, but my identifier is unique.");
            //    Imgui.End();
            //
            //    // Using "###" to display a changing title but keep a static identifier "AnimatedTitle"
            //    char buf[128];
            //    sprintf(buf, "Animated title %c %d###AnimatedTitle", "|/-\\"[(int)(Imgui.GetTime() / 0.25f) & 3], Imgui.GetFrameCount());
            //    Imgui.SetNextWindowPos(ImVec2(base_pos.x + 100, base_pos.y + 300), ImGuiCond_FirstUseEver);
            //    Imgui.Begin(buf);
            //    Imgui.Text("This window has a changing title.");
            //    Imgui.End();
        }

        private static void ShowExampleAppCustomRendering(State<bool> p_open)
        {
            //    if (!Imgui.Begin("Example: Custom rendering", p_open))
            //    {
            //        Imgui.End();
            //        return;
            //    }
            //    IMGUI_DEMO_MARKER("Examples/Custom Rendering");
            //
            //    // Tip: If you do a lot of custom rendering, you probably want to use your own geometrical types and benefit of
            //    // overloaded operators, etc. Define IM_VEC2_CLASS_EXTRA in imconfig.h to create implicit conversions between your
            //    // types and ImVec2/ImVec4. Dear ImGui defines overloaded operators but they are internal to imgui.cpp and not
            //    // exposed outside (to avoid messing with your types) In this example we are not using the maths operators!
            //
            //    if (Imgui.BeginTabBar("##TabBar"))
            //    {
            //        if (Imgui.BeginTabItem("Primitives"))
            //        {
            //            Imgui.PushItemWidth(-Imgui.GetFontSize() * 15);
            //            ImDrawList* draw_list = Imgui.GetWindowDrawList();
            //
            //            // Draw gradients
            //            // (note that those are currently exacerbating our sRGB/Linear issues)
            //            // Calling Imgui.GetColorU32() multiplies the given colors by the current Style Alpha, but you may pass the IM_COL32() directly as well..
            //            Imgui.Text("Gradients");
            //            ImVec2 gradient_size = ImVec2(Imgui.CalcItemWidth(), Imgui.GetFrameHeight());
            //            {
            //                ImVec2 p0 = Imgui.GetCursorScreenPos();
            //                ImVec2 p1 = ImVec2(p0.x + gradient_size.x, p0.y + gradient_size.y);
            //                ImU32 col_a = Imgui.GetColorU32(IM_COL32(0, 0, 0, 255));
            //                ImU32 col_b = Imgui.GetColorU32(IM_COL32(255, 255, 255, 255));
            //                draw_list->AddRectFilledMultiColor(p0, p1, col_a, col_b, col_b, col_a);
            //                Imgui.InvisibleButton("##gradient1", gradient_size);
            //            }
            //            {
            //                ImVec2 p0 = Imgui.GetCursorScreenPos();
            //                ImVec2 p1 = ImVec2(p0.x + gradient_size.x, p0.y + gradient_size.y);
            //                ImU32 col_a = Imgui.GetColorU32(IM_COL32(0, 255, 0, 255));
            //                ImU32 col_b = Imgui.GetColorU32(IM_COL32(255, 0, 0, 255));
            //                draw_list->AddRectFilledMultiColor(p0, p1, col_a, col_b, col_b, col_a);
            //                Imgui.InvisibleButton("##gradient2", gradient_size);
            //            }
            //
            //            // Draw a bunch of primitives
            //            Imgui.Text("All primitives");
            //            static float sz = 36.0f;
            //            static float thickness = 3.0f;
            //            static int ngon_sides = 6;
            //            static bool circle_segments_override = false;
            //            static int circle_segments_override_v = 12;
            //            static bool curve_segments_override = false;
            //            static int curve_segments_override_v = 8;
            //            static ImVec4 colf = ImVec4(1.0f, 1.0f, 0.4f, 1.0f);
            //            Imgui.DragFloat("Size", &sz, 0.2f, 2.0f, 100.0f, "%.0f");
            //            Imgui.DragFloat("Thickness", &thickness, 0.05f, 1.0f, 8.0f, "%.02f");
            //            Imgui.SliderInt("N-gon sides", &ngon_sides, 3, 12);
            //            Imgui.Checkbox("##circlesegmentoverride", &circle_segments_override);
            //            Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.x);
            //            circle_segments_override |= Imgui.SliderInt("Circle segments override", &circle_segments_override_v, 3, 40);
            //            Imgui.Checkbox("##curvessegmentoverride", &curve_segments_override);
            //            Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.x);
            //            curve_segments_override |= Imgui.SliderInt("Curves segments override", &curve_segments_override_v, 3, 40);
            //            Imgui.ColorEdit4("Color", &colf.x);
            //
            //            const ImVec2 p = Imgui.GetCursorScreenPos();
            //            const ImU32 col = ImColor(colf);
            //            const float spacing = 10.0f;
            //            const ImDrawFlags corners_tl_br = ImDrawFlags_RoundCornersTopLeft | ImDrawFlags_RoundCornersBottomRight;
            //            const float rounding = sz / 5.0f;
            //            const int circle_segments = circle_segments_override ? circle_segments_override_v : 0;
            //            const int curve_segments = curve_segments_override ? curve_segments_override_v : 0;
            //            float x = p.x + 4.0f;
            //            float y = p.y + 4.0f;
            //            for (int n = 0; n < 2; n++)
            //            {
            //                // First line uses a thickness of 1.0f, second line uses the configurable thickness
            //                float th = (n == 0) ? 1.0f : thickness;
            //                draw_list->AddNgon(ImVec2(x + sz*0.5f, y + sz*0.5f), sz*0.5f, col, ngon_sides, th);                 x += sz + spacing;  // N-gon
            //                draw_list->AddCircle(ImVec2(x + sz*0.5f, y + sz*0.5f), sz*0.5f, col, circle_segments, th);          x += sz + spacing;  // Circle
            //                draw_list->AddRect(ImVec2(x, y), ImVec2(x + sz, y + sz), col, 0.0f, ImDrawFlags_None, th);          x += sz + spacing;  // Square
            //                draw_list->AddRect(ImVec2(x, y), ImVec2(x + sz, y + sz), col, rounding, ImDrawFlags_None, th);      x += sz + spacing;  // Square with all rounded corners
            //                draw_list->AddRect(ImVec2(x, y), ImVec2(x + sz, y + sz), col, rounding, corners_tl_br, th);         x += sz + spacing;  // Square with two rounded corners
            //                draw_list->AddTriangle(ImVec2(x+sz*0.5f,y), ImVec2(x+sz, y+sz-0.5f), ImVec2(x, y+sz-0.5f), col, th);x += sz + spacing;  // Triangle
            //                //draw_list->AddTriangle(ImVec2(x+sz*0.2f,y), ImVec2(x, y+sz-0.5f), ImVec2(x+sz*0.4f, y+sz-0.5f), col, th);x+= sz*0.4f + spacing; // Thin triangle
            //                draw_list->AddLine(ImVec2(x, y), ImVec2(x + sz, y), col, th);                                       x += sz + spacing;  // Horizontal line (note: drawing a filled rectangle will be faster!)
            //                draw_list->AddLine(ImVec2(x, y), ImVec2(x, y + sz), col, th);                                       x += spacing;       // Vertical line (note: drawing a filled rectangle will be faster!)
            //                draw_list->AddLine(ImVec2(x, y), ImVec2(x + sz, y + sz), col, th);                                  x += sz + spacing;  // Diagonal line
            //
            //                // Quadratic Bezier Curve (3 control points)
            //                ImVec2 cp3[3] = { ImVec2(x, y + sz * 0.6f), ImVec2(x + sz * 0.5f, y - sz * 0.4f), ImVec2(x + sz, y + sz) };
            //                draw_list->AddBezierQuadratic(cp3[0], cp3[1], cp3[2], col, th, curve_segments); x += sz + spacing;
            //
            //                // Cubic Bezier Curve (4 control points)
            //                ImVec2 cp4[4] = { ImVec2(x, y), ImVec2(x + sz * 1.3f, y + sz * 0.3f), ImVec2(x + sz - sz * 1.3f, y + sz - sz * 0.3f), ImVec2(x + sz, y + sz) };
            //                draw_list->AddBezierCubic(cp4[0], cp4[1], cp4[2], cp4[3], col, th, curve_segments);
            //
            //                x = p.x + 4;
            //                y += sz + spacing;
            //            }
            //            draw_list->AddNgonFilled(ImVec2(x + sz * 0.5f, y + sz * 0.5f), sz*0.5f, col, ngon_sides);               x += sz + spacing;  // N-gon
            //            draw_list->AddCircleFilled(ImVec2(x + sz*0.5f, y + sz*0.5f), sz*0.5f, col, circle_segments);            x += sz + spacing;  // Circle
            //            draw_list->AddRectFilled(ImVec2(x, y), ImVec2(x + sz, y + sz), col);                                    x += sz + spacing;  // Square
            //            draw_list->AddRectFilled(ImVec2(x, y), ImVec2(x + sz, y + sz), col, 10.0f);                             x += sz + spacing;  // Square with all rounded corners
            //            draw_list->AddRectFilled(ImVec2(x, y), ImVec2(x + sz, y + sz), col, 10.0f, corners_tl_br);              x += sz + spacing;  // Square with two rounded corners
            //            draw_list->AddTriangleFilled(ImVec2(x+sz*0.5f,y), ImVec2(x+sz, y+sz-0.5f), ImVec2(x, y+sz-0.5f), col);  x += sz + spacing;  // Triangle
            //            //draw_list->AddTriangleFilled(ImVec2(x+sz*0.2f,y), ImVec2(x, y+sz-0.5f), ImVec2(x+sz*0.4f, y+sz-0.5f), col); x += sz*0.4f + spacing; // Thin triangle
            //            draw_list->AddRectFilled(ImVec2(x, y), ImVec2(x + sz, y + thickness), col);                             x += sz + spacing;  // Horizontal line (faster than AddLine, but only handle integer thickness)
            //            draw_list->AddRectFilled(ImVec2(x, y), ImVec2(x + thickness, y + sz), col);                             x += spacing * 2.0f;// Vertical line (faster than AddLine, but only handle integer thickness)
            //            draw_list->AddRectFilled(ImVec2(x, y), ImVec2(x + 1, y + 1), col);                                      x += sz;            // Pixel (faster than AddLine)
            //            draw_list->AddRectFilledMultiColor(ImVec2(x, y), ImVec2(x + sz, y + sz), IM_COL32(0, 0, 0, 255), IM_COL32(255, 0, 0, 255), IM_COL32(255, 255, 0, 255), IM_COL32(0, 255, 0, 255));
            //
            //            Imgui.Dummy(ImVec2((sz + spacing) * 10.2f, (sz + spacing) * 3.0f));
            //            Imgui.PopItemWidth();
            //            Imgui.EndTabItem();
            //        }
            //
            //        if (Imgui.BeginTabItem("Canvas"))
            //        {
            //            static ImVector<ImVec2> points;
            //            static ImVec2 scrolling(0.0f, 0.0f);
            //            static bool opt_enable_grid = true;
            //            static bool opt_enable_context_menu = true;
            //            static bool adding_line = false;
            //
            //            Imgui.Checkbox("Enable grid", &opt_enable_grid);
            //            Imgui.Checkbox("Enable context menu", &opt_enable_context_menu);
            //            Imgui.Text("Mouse Left: drag to add lines,\nMouse Right: drag to scroll, click for context menu.");
            //
            //            // Typically you would use a BeginChild()/EndChild() pair to benefit from a clipping region + own scrolling.
            //            // Here we demonstrate that this can be replaced by simple offsetting + custom drawing + PushClipRect/PopClipRect() calls.
            //            // To use a child window instead we could use, e.g:
            //            //      Imgui.PushStyleVar(ImGuiStyleVar_WindowPadding, ImVec2(0, 0));      // Disable padding
            //            //      Imgui.PushStyleColor(ImGuiCol_ChildBg, IM_COL32(50, 50, 50, 255));  // Set a background color
            //            //      Imgui.BeginChild("canvas", ImVec2(0.0f, 0.0f), true, ImGuiWindowFlags_NoMove);
            //            //      Imgui.PopStyleColor();
            //            //      Imgui.PopStyleVar();
            //            //      [...]
            //            //      Imgui.EndChild();
            //
            //            // Using InvisibleButton() as a convenience 1) it will advance the layout cursor and 2) allows us to use IsItemHovered()/IsItemActive()
            //            ImVec2 canvas_p0 = Imgui.GetCursorScreenPos();      // ImDrawList API uses screen coordinates!
            //            ImVec2 canvas_sz = Imgui.GetContentRegionAvail();   // Resize canvas to what's available
            //            if (canvas_sz.x < 50.0f) canvas_sz.x = 50.0f;
            //            if (canvas_sz.y < 50.0f) canvas_sz.y = 50.0f;
            //            ImVec2 canvas_p1 = ImVec2(canvas_p0.x + canvas_sz.x, canvas_p0.y + canvas_sz.y);
            //
            //            // Draw border and background color
            //            ImGuiIO& io = Imgui.GetIO();
            //            ImDrawList* draw_list = Imgui.GetWindowDrawList();
            //            draw_list->AddRectFilled(canvas_p0, canvas_p1, IM_COL32(50, 50, 50, 255));
            //            draw_list->AddRect(canvas_p0, canvas_p1, IM_COL32(255, 255, 255, 255));
            //
            //            // This will catch our interactions
            //            Imgui.InvisibleButton("canvas", canvas_sz, ImGuiButtonFlags_MouseButtonLeft | ImGuiButtonFlags_MouseButtonRight);
            //            const bool is_hovered = Imgui.IsItemHovered(); // Hovered
            //            const bool is_active = Imgui.IsItemActive();   // Held
            //            const ImVec2 origin(canvas_p0.x + scrolling.x, canvas_p0.y + scrolling.y); // Lock scrolled origin
            //            const ImVec2 mouse_pos_in_canvas(io.MousePos.x - origin.x, io.MousePos.y - origin.y);
            //
            //            // Add first and second point
            //            if (is_hovered && !adding_line && Imgui.IsMouseClicked(ImGuiMouseButton_Left))
            //            {
            //                points.push_back(mouse_pos_in_canvas);
            //                points.push_back(mouse_pos_in_canvas);
            //                adding_line = true;
            //            }
            //            if (adding_line)
            //            {
            //                points.back() = mouse_pos_in_canvas;
            //                if (!Imgui.IsMouseDown(ImGuiMouseButton_Left))
            //                    adding_line = false;
            //            }
            //
            //            // Pan (we use a zero mouse threshold when there's no context menu)
            //            // You may decide to make that threshold dynamic based on whether the mouse is hovering something etc.
            //            const float mouse_threshold_for_pan = opt_enable_context_menu ? -1.0f : 0.0f;
            //            if (is_active && Imgui.IsMouseDragging(ImGuiMouseButton_Right, mouse_threshold_for_pan))
            //            {
            //                scrolling.x += io.MouseDelta.x;
            //                scrolling.y += io.MouseDelta.y;
            //            }
            //
            //            // Context menu (under default mouse threshold)
            //            ImVec2 drag_delta = Imgui.GetMouseDragDelta(ImGuiMouseButton_Right);
            //            if (opt_enable_context_menu && drag_delta.x == 0.0f && drag_delta.y == 0.0f)
            //                Imgui.OpenPopupOnItemClick("context", ImGuiPopupFlags_MouseButtonRight);
            //            if (Imgui.BeginPopup("context"))
            //            {
            //                if (adding_line)
            //                    points.resize(points.size() - 2);
            //                adding_line = false;
            //                if (Imgui.MenuItem("Remove one", NULL, false, points.Size > 0)) { points.resize(points.size() - 2); }
            //                if (Imgui.MenuItem("Remove all", NULL, false, points.Size > 0)) { points.clear(); }
            //                Imgui.EndPopup();
            //            }
            //
            //            // Draw grid + all lines in the canvas
            //            draw_list->PushClipRect(canvas_p0, canvas_p1, true);
            //            if (opt_enable_grid)
            //            {
            //                const float GRID_STEP = 64.0f;
            //                for (float x = fmodf(scrolling.x, GRID_STEP); x < canvas_sz.x; x += GRID_STEP)
            //                    draw_list->AddLine(ImVec2(canvas_p0.x + x, canvas_p0.y), ImVec2(canvas_p0.x + x, canvas_p1.y), IM_COL32(200, 200, 200, 40));
            //                for (float y = fmodf(scrolling.y, GRID_STEP); y < canvas_sz.y; y += GRID_STEP)
            //                    draw_list->AddLine(ImVec2(canvas_p0.x, canvas_p0.y + y), ImVec2(canvas_p1.x, canvas_p0.y + y), IM_COL32(200, 200, 200, 40));
            //            }
            //            for (int n = 0; n < points.Size; n += 2)
            //                draw_list->AddLine(ImVec2(origin.x + points[n].x, origin.y + points[n].y), ImVec2(origin.x + points[n + 1].x, origin.y + points[n + 1].y), IM_COL32(255, 255, 0, 255), 2.0f);
            //            draw_list->PopClipRect();
            //
            //            Imgui.EndTabItem();
            //        }
            //
            //        if (Imgui.BeginTabItem("BG/FG draw lists"))
            //        {
            //            static bool draw_bg = true;
            //            static bool draw_fg = true;
            //            Imgui.Checkbox("Draw in Background draw list", &draw_bg);
            //            Imgui.SameLine(); HelpMarker("The Background draw list will be rendered below every Dear ImGui windows.");
            //            Imgui.Checkbox("Draw in Foreground draw list", &draw_fg);
            //            Imgui.SameLine(); HelpMarker("The Foreground draw list will be rendered over every Dear ImGui windows.");
            //            ImVec2 window_pos = Imgui.GetWindowPos();
            //            ImVec2 window_size = Imgui.GetWindowSize();
            //            ImVec2 window_center = ImVec2(window_pos.x + window_size.x * 0.5f, window_pos.y + window_size.y * 0.5f);
            //            if (draw_bg)
            //                Imgui.GetBackgroundDrawList()->AddCircle(window_center, window_size.x * 0.6f, IM_COL32(255, 0, 0, 200), 0, 10 + 4);
            //            if (draw_fg)
            //                Imgui.GetForegroundDrawList()->AddCircle(window_center, window_size.y * 0.6f, IM_COL32(0, 255, 0, 200), 0, 10);
            //            Imgui.EndTabItem();
            //        }
            //
            //        Imgui.EndTabBar();
            //    }
            //
            //    Imgui.End();
        }

        //// Simplified structure to mimic a Document model
        //struct MyDocument
        //{
        //    const char* Name;       // Document title
        //    bool        Open;       // Set when open (we keep an array of all available documents to simplify demo code!)
        //    bool        OpenPrev;   // Copy of Open from last update.
        //    bool        Dirty;      // Set when the document has been modified
        //    bool        WantClose;  // Set when the document
        //    ImVec4      Color;      // An arbitrary variable associated to the document
        //
        //    MyDocument(const char* name, bool open = true, const ImVec4& color = ImVec4(1.0f, 1.0f, 1.0f, 1.0f))
        //    {
        //        Name = name;
        //        Open = OpenPrev = open;
        //        Dirty = false;
        //        WantClose = false;
        //        Color = color;
        //    }
        //    void DoOpen()       { Open = true; }
        //    void DoQueueClose() { WantClose = true; }
        //    void DoForceClose() { Open = false; Dirty = false; }
        //    void DoSave()       { Dirty = false; }
        //
        //    // Display placeholder contents for the Document
        //    static void DisplayContents(MyDocument* doc)
        //    {
        //        Imgui.PushID(doc);
        //        Imgui.Text("Document \"%s\"", doc->Name);
        //        Imgui.PushStyleColor(ImGuiCol_Text, doc->Color);
        //        Imgui.TextWrapped("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
        //        Imgui.PopStyleColor();
        //        if (Imgui.Button("Modify", ImVec2(100, 0)))
        //            doc->Dirty = true;
        //        Imgui.SameLine();
        //        if (Imgui.Button("Save", ImVec2(100, 0)))
        //            doc->DoSave();
        //        Imgui.ColorEdit3("color", &doc->Color.x);  // Useful to test drag and drop and hold-dragged-to-open-tab behavior.
        //        Imgui.PopID();
        //    }
        //
        //    // Display context menu for the Document
        //    static void DisplayContextMenu(MyDocument* doc)
        //    {
        //        if (!Imgui.BeginPopupContextItem())
        //            return;
        //
        //        char buf[256];
        //        sprintf(buf, "Save %s", doc->Name);
        //        if (Imgui.MenuItem(buf, "CTRL+S", false, doc->Open))
        //            doc->DoSave();
        //        if (Imgui.MenuItem("Close", "CTRL+W", false, doc->Open))
        //            doc->DoQueueClose();
        //        Imgui.EndPopup();
        //    }
        //};
        //
        //struct ExampleAppDocuments
        //{
        //    ImVector<MyDocument> Documents;
        //
        //    ExampleAppDocuments()
        //    {
        //        Documents.push_back(MyDocument("Lettuce",             true,  ImVec4(0.4f, 0.8f, 0.4f, 1.0f)));
        //        Documents.push_back(MyDocument("Eggplant",            true,  ImVec4(0.8f, 0.5f, 1.0f, 1.0f)));
        //        Documents.push_back(MyDocument("Carrot",              true,  ImVec4(1.0f, 0.8f, 0.5f, 1.0f)));
        //        Documents.push_back(MyDocument("Tomato",              false, ImVec4(1.0f, 0.3f, 0.4f, 1.0f)));
        //        Documents.push_back(MyDocument("A Rather Long Title", false));
        //        Documents.push_back(MyDocument("Some Document",       false));
        //    }
        //};
        //
        //// [Optional] Notify the system of Tabs/Windows closure that happened outside the regular tab interface.
        //// If a tab has been closed programmatically (aka closed from another source such as the Checkbox() in the demo,
        //// as opposed to clicking on the regular tab closing button) and stops being submitted, it will take a frame for
        //// the tab bar to notice its absence. During this frame there will be a gap in the tab bar, and if the tab that has
        //// disappeared was the selected one, the tab bar will report no selected tab during the frame. This will effectively
        //// give the impression of a flicker for one frame.
        //// We call SetTabItemClosed() to manually notify the Tab Bar or Docking system of removed tabs to avoid this glitch.
        //// Note that this completely optional, and only affect tab bars with the ImGuiTabBarFlags_Reorderable flag.
        //static void NotifyOfDocumentsClosedElsewhere(ExampleAppDocuments& app)
        //{
        //    for (int doc_n = 0; doc_n < app.Documents.Size; doc_n++)
        //    {
        //        MyDocument* doc = &app.Documents[doc_n];
        //        if (!doc->Open && doc->OpenPrev)
        //            Imgui.SetTabItemClosed(doc->Name);
        //        doc->OpenPrev = doc->Open;
        //    }
        //}

        private static void ShowExampleAppDocuments(State<bool> p_open)
        {
            //    static ExampleAppDocuments app;
            //
            //    // Options
            //    static bool opt_reorderable = true;
            //    static ImGuiTabBarFlags opt_fitting_flags = ImGuiTabBarFlags_FittingPolicyDefault_;
            //
            //    bool window_contents_visible = Imgui.Begin("Example: Documents", p_open, ImGuiWindowFlags_MenuBar);
            //    if (!window_contents_visible)
            //    {
            //        Imgui.End();
            //        return;
            //    }
            //
            //    // Menu
            //    if (Imgui.BeginMenuBar())
            //    {
            //        if (Imgui.BeginMenu("File"))
            //        {
            //            int open_count = 0;
            //            for (int doc_n = 0; doc_n < app.Documents.Size; doc_n++)
            //                open_count += app.Documents[doc_n].Open ? 1 : 0;
            //
            //            if (Imgui.BeginMenu("Open", open_count < app.Documents.Size))
            //            {
            //                for (int doc_n = 0; doc_n < app.Documents.Size; doc_n++)
            //                {
            //                    MyDocument* doc = &app.Documents[doc_n];
            //                    if (!doc->Open)
            //                        if (Imgui.MenuItem(doc->Name))
            //                            doc->DoOpen();
            //                }
            //                Imgui.EndMenu();
            //            }
            //            if (Imgui.MenuItem("Close All Documents", NULL, false, open_count > 0))
            //                for (int doc_n = 0; doc_n < app.Documents.Size; doc_n++)
            //                    app.Documents[doc_n].DoQueueClose();
            //            if (Imgui.MenuItem("Exit", "Ctrl+F4") && p_open)
            //                *p_open = false;
            //            Imgui.EndMenu();
            //        }
            //        Imgui.EndMenuBar();
            //    }
            //
            //    // [Debug] List documents with one checkbox for each
            //    for (int doc_n = 0; doc_n < app.Documents.Size; doc_n++)
            //    {
            //        MyDocument* doc = &app.Documents[doc_n];
            //        if (doc_n > 0)
            //            Imgui.SameLine();
            //        Imgui.PushID(doc);
            //        if (Imgui.Checkbox(doc->Name, &doc->Open))
            //            if (!doc->Open)
            //                doc->DoForceClose();
            //        Imgui.PopID();
            //    }
            //
            //    Imgui.Separator();
            //
            //    // About the ImGuiWindowFlags_UnsavedDocument / ImGuiTabItemFlags_UnsavedDocument flags.
            //    // They have multiple effects:
            //    // - Display a dot next to the title.
            //    // - Tab is selected when clicking the X close button.
            //    // - Closure is not assumed (will wait for user to stop submitting the tab).
            //    //   Otherwise closure is assumed when pressing the X, so if you keep submitting the tab may reappear at end of tab bar.
            //    //   We need to assume closure by default otherwise waiting for "lack of submission" on the next frame would leave an empty
            //    //   hole for one-frame, both in the tab-bar and in tab-contents when closing a tab/window.
            //    //   The rarely used SetTabItemClosed() function is a way to notify of programmatic closure to avoid the one-frame hole.
            //
            //    // Submit Tab Bar and Tabs
            //    {
            //        ImGuiTabBarFlags tab_bar_flags = (opt_fitting_flags) | (opt_reorderable ? ImGuiTabBarFlags_Reorderable : 0);
            //        if (Imgui.BeginTabBar("##tabs", tab_bar_flags))
            //        {
            //            if (opt_reorderable)
            //                NotifyOfDocumentsClosedElsewhere(app);
            //
            //            // [DEBUG] Stress tests
            //            //if ((Imgui.GetFrameCount() % 30) == 0) docs[1].Open ^= 1;            // [DEBUG] Automatically show/hide a tab. Test various interactions e.g. dragging with this on.
            //            //if (Imgui.GetIO().KeyCtrl) Imgui.SetTabItemSelected(docs[1].Name);  // [DEBUG] Test SetTabItemSelected(), probably not very useful as-is anyway..
            //
            //            // Submit Tabs
            //            for (int doc_n = 0; doc_n < app.Documents.Size; doc_n++)
            //            {
            //                MyDocument* doc = &app.Documents[doc_n];
            //                if (!doc->Open)
            //                    continue;
            //
            //                ImGuiTabItemFlags tab_flags = (doc->Dirty ? ImGuiTabItemFlags_UnsavedDocument : 0);
            //                bool visible = Imgui.BeginTabItem(doc->Name, &doc->Open, tab_flags);
            //
            //                // Cancel attempt to close when unsaved add to save queue so we can display a popup.
            //                if (!doc->Open && doc->Dirty)
            //                {
            //                    doc->Open = true;
            //                    doc->DoQueueClose();
            //                }
            //
            //                MyDocument::DisplayContextMenu(doc);
            //                if (visible)
            //                {
            //                    MyDocument::DisplayContents(doc);
            //                    Imgui.EndTabItem();
            //                }
            //            }
            //
            //            Imgui.EndTabBar();
            //        }
            //    }
            //
            //    // Update closing queue
            //    static ImVector<MyDocument*> close_queue;
            //    if (close_queue.empty())
            //    {
            //        // Close queue is locked once we started a popup
            //        for (int doc_n = 0; doc_n < app.Documents.Size; doc_n++)
            //        {
            //            MyDocument* doc = &app.Documents[doc_n];
            //            if (doc->WantClose)
            //            {
            //                doc->WantClose = false;
            //                close_queue.push_back(doc);
            //            }
            //        }
            //    }
            //
            //    // Display closing confirmation UI
            //    if (!close_queue.empty())
            //    {
            //        int close_queue_unsaved_documents = 0;
            //        for (int n = 0; n < close_queue.Size; n++)
            //            if (close_queue[n]->Dirty)
            //                close_queue_unsaved_documents++;
            //
            //        if (close_queue_unsaved_documents == 0)
            //        {
            //            // Close documents when all are unsaved
            //            for (int n = 0; n < close_queue.Size; n++)
            //                close_queue[n]->DoForceClose();
            //            close_queue.clear();
            //        }
            //        else
            //        {
            //            if (!Imgui.IsPopupOpen("Save?"))
            //                Imgui.OpenPopup("Save?");
            //            if (Imgui.BeginPopupModal("Save?", NULL, ImGuiWindowFlags_AlwaysAutoResize))
            //            {
            //                Imgui.Text("Save change to the following items?");
            //                float item_height = Imgui.GetTextLineHeightWithSpacing();
            //                if (Imgui.BeginChildFrame(Imgui.GetID("frame"), ImVec2(-FLT_MIN, 6.25f * item_height)))
            //                {
            //                    for (int n = 0; n < close_queue.Size; n++)
            //                        if (close_queue[n]->Dirty)
            //                            Imgui.Text("%s", close_queue[n]->Name);
            //                    Imgui.EndChildFrame();
            //                }
            //
            //                ImVec2 button_size(Imgui.GetFontSize() * 7.0f, 0.0f);
            //                if (Imgui.Button("Yes", button_size))
            //                {
            //                    for (int n = 0; n < close_queue.Size; n++)
            //                    {
            //                        if (close_queue[n]->Dirty)
            //                            close_queue[n]->DoSave();
            //                        close_queue[n]->DoForceClose();
            //                    }
            //                    close_queue.clear();
            //                    Imgui.CloseCurrentPopup();
            //                }
            //                Imgui.SameLine();
            //                if (Imgui.Button("No", button_size))
            //                {
            //                    for (int n = 0; n < close_queue.Size; n++)
            //                        close_queue[n]->DoForceClose();
            //                    close_queue.clear();
            //                    Imgui.CloseCurrentPopup();
            //                }
            //                Imgui.SameLine();
            //                if (Imgui.Button("Cancel", button_size))
            //                {
            //                    close_queue.clear();
            //                    Imgui.CloseCurrentPopup();
            //                }
            //                Imgui.EndPopup();
            //            }
            //        }
            //    }
            //
            //    Imgui.End();
        }
    }
}
