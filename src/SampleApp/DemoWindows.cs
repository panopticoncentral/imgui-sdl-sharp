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
            Imgui.SetNextWindowPosition(mainViewport.WorkPosition + new PositionF(650, 20), Condition.FirstUseEver);
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
        private static readonly State<int> s_itemCurrent1 = new(0);
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
        private static readonly State<ColorF> s_col1 = new(new(1.0f, 0.0f, 0.2f));
        private static readonly State<ColorF> s_col2 = new(new(0.4f, 0.7f, 0.0f, 0.5f));
        private static readonly State<int> s_itemCurrent2 = new(1);
        private static readonly StateOption<TreeNodeOptions> s_baseFlags = new(TreeNodeOptions.OpenOnArrow | TreeNodeOptions.OpenOnDoubleClick | TreeNodeOptions.SpanAvailWidth);
        private static readonly State<bool> s_alignLabelWithCurrentXPosition = new(false);
        private static readonly State<bool> s_testDragAndDrop = new(false);
        private static int s_selectionMask = 1 << 2;
        private static readonly State<bool> s_closableGroup = new(true);
        private static readonly State<float> s_wrapWidth = new(200.0f);
        private static int s_pressedCount;
        private static readonly StateOption<ComboOptions> s_flags = new(0);
        private static int s_itemCurrentIdx;
        private static readonly State<int> s_itemCurrent3 = new(0);
        private static readonly State<int> s_itemCurrent4 = new(-1);
        private static int s_itemCurrentIdx2;
        private static readonly StateVector<bool> s_selected = new(5, new[] { false, true, false, false, false });
        private static int s_selected2 = -1;
        private static readonly StateVector<bool> s_selected3 = new(5, new[] { false, false, false, false, false });
        private static readonly StateVector<bool> s_selected4 = new(3, new[] { false, false, false });
        private static readonly StateVector<bool> s_selected5 = new(10);
        private static readonly StateVector<bool>[] s_selected6 = new StateVector<bool>[]
        {
            new(4, new[] { true, false, false, false }),
            new(4, new[] { false, true, false, false }),
            new(4, new[] { false, false, true, false }),
            new(4, new[] { false, false, false, true })
        };
        private static readonly StateVector<bool> s_selected7 = new(3 * 3, new[] { true, false, true, false, true, false, true, false, true });
        private static readonly StateText s_text = new(1024 * 16,
            "/*\n" +
            " The Pentium F00F bug, shorthand for F0 0F C7 C8,\n" +
            " the hexadecimal encoding of one offending instruction,\n" +
            " more formally, the invalid operand with locked CMPXCHG8B\n" +
            " instruction bug, is a design flaw in the majority of\n" +
            " Intel Pentium, Pentium MMX, and Pentium OverDrive\n" +
            " processors (all in the P5 microarchitecture).\n" +
            "*/\n\n" +
            "label:\n" +
            "\tlock cmpxchg8b eax\n");
        private static readonly StateOption<InputTextOptions> s_flags2 = new(InputTextOptions.AllowTabInput);
        private static readonly StateText s_buf1 = new(64, "");
        private static readonly StateText s_buf2 = new(64, "");
        private static readonly StateText s_buf3 = new(64, "");
        private static readonly StateText s_buf4 = new(64, "");
        private static readonly StateText s_buf5 = new(64, "");
        private static readonly StateText s_buf6 = new(64, "");
        private static readonly StateText s_password = new(64, "password123");
        private static readonly StateText s_buf7 = new(64, "");
        private static readonly StateText s_buf8 = new(64, "");
        private static readonly StateText s_buf9 = new(64, "");
        private static int s_editCount;
        private static readonly StateText s_myStr = new(1, "");
        private static readonly StateOption<TabBarOptions> s_tabBarOptions = new(TabBarOptions.Reorderable);
        private static readonly StateVector<bool> s_opened = new(4, new[] { true, true, true, true });
        private static readonly List<int> s_activeTabs = new();
        private static int s_nextTabId;
        private static readonly State<bool> s_showLeadingButton = new(true);
        private static readonly State<bool> s_showTrailingButton = new(true);
        private static readonly StateOption<TabBarOptions> s_tabBarOptions2 = new(TabBarOptions.AutoSelectNewTabs | TabBarOptions.Reorderable | TabBarOptions.FittingPolicyResizeDown);
        private static readonly State<bool> s_animate = new(true);
        private static readonly float[] s_values = new float[90];
        private static int s_valuesOffset;
        private static double s_refreshTime;
        private static float s_phase;
        private static readonly State<int> s_funcType = new(0);
        private static readonly State<int> s_displayCount = new(70);
        private static float s_progress;
        private static float s_progressDir = 1.0f;
        private static readonly State<ColorF> s_color = new(new(114.0f / 255.0f, 144.0f / 255.0f, 154.0f / 255.0f, 200.0f / 255.0f));
        private static readonly State<bool> s_alphaPreview = new(true);
        private static readonly State<bool> s_alphaHalfPreview = new(false);
        private static readonly State<bool> s_dragAndDrop = new(true);
        private static readonly State<bool> s_optionsMenu = new(true);
        private static readonly State<bool> s_hdr = new(false);
        private static bool s_savedPaletteInit = true;
        private static readonly ColorF[] s_savedPalette = new ColorF[32];
        private static ColorF s_backupColor;
        private static readonly State<bool> s_noBorder = new(false);
        private static readonly State<bool> s_alpha = new(true);
        private static readonly State<bool> s_alphaBar = new(true);
        private static readonly State<bool> s_sidePreview = new(true);
        private static readonly State<bool> s_refColor = new(false);
        private static readonly State<ColorF> s_refColorV = new(new(1.0f, 0.0f, 1.0f, 0.5f));
        private static readonly State<int> s_displayMode = new(0);
        private static readonly State<int> s_pickerMode = new(0);
        private static readonly State<ColorF> s_colorHsv = new(new(0.23f, 1.0f, 1.0f, 1.0f));
        private static readonly StateOption<SliderOptions> s_flags3 = new(SliderOptions.None);
        private static readonly State<float> s_dragF = new(0.5f);
        private static readonly State<int> s_dragI = new(50);
        private static readonly State<float> s_sliderF = new(0.5f);
        private static readonly State<int> s_sliderI = new(50);
        private static readonly State<float> s_begin = new(10), s_end = new(90);
        private static readonly State<int> s_beginI = new(100), s_endI = new(1000);
        private static readonly State<sbyte> s_sbyteValue = new(127);
        private static readonly State<byte> s_byteValue = new(255);
        private static readonly State<short> s_shortValue = new(32767);
        private static readonly State<ushort> s_ushortValue = new(65535);
        private static readonly State<int> s_intValue = new(-1);
        private static readonly State<uint> s_uintValue = new(0xFFFFFFFF);
        private static readonly State<long> s_longValue = new(-1);
        private static readonly State<ulong> s_ulongValue = new(0xFFFFFFFFFFFFFFFF);
        private static readonly State<float> s_singleValue = new(0.123f);
        private static readonly State<double> s_doubleValue = new(90000.01234567890123456789);
        private static readonly State<bool> s_dragClamp = new(false);
        private static readonly State<bool> s_inputsStep = new(true);
        private static readonly StateVector<float> s_vec2F = new(2, new[] { 0.10f, 0.20f });
        private static readonly StateVector<int> s_vec2I = new(2, new[] { 1, 5 });
        private static readonly StateVector<float> s_vec3F = new(3, new[] { 0.10f, 0.20f, 0.30f });
        private static readonly StateVector<int> s_vec3I = new(3, new[] { 1, 5, 100 });
        private static readonly StateVector<float> s_vec4F = new(4, new[] { 0.10f, 0.20f, 0.30f, 0.44f });
        private static readonly StateVector<int> s_vec4I = new(4, new[] { 1, 5, 100, 255 });
        private static readonly State<int> s_intValue2 = new(0);
        private static readonly StateVector<float> s_values2 = new(7, new[] { 0.0f, 0.60f, 0.35f, 0.9f, 0.70f, 0.20f, 0.0f });
        private static readonly StateVector<float> s_values3 = new(4, new[] { 0.20f, 0.80f, 0.40f, 0.25f });
        private static readonly State<ColorF> s_col3 = new(new(1.0f, 0.0f, 0.2f));
        private static readonly State<ColorF> s_col4 = new(new(0.4f, 0.7f, 0.0f, 0.5f));
        private enum Mode
        {
            Copy,
            Move,
            Swap
        };
        private static Mode s_mode = Mode.Copy;
        private static readonly string[] s_names = new[] { "Bobby", "Beatrice", "Betty", "Brianna", "Barry", "Bernard", "Bibi", "Blaine", "Bryn" };
        private static readonly string[] s_itemNames = new[] { "Item One", "Item Two", "Item Three", "Item Four", "Item Five" };
        private static readonly State<int> s_itemType = new(4);
        private static readonly State<bool> s_itemDisabled = new(false);
        private static readonly State<bool> s_b = new(false);
        private static readonly StateVector<float> s_col4f = new(4, new[] { 1.0f, 0.5f, 0.0f, 1.0f });
        private static readonly State<ColorF> s_col4f2 = new(new(1.0f, 0.5f, 0.0f, 1.0f));
        private static readonly StateText s_str = new(16);
        private static readonly State<int> s_current = new(1);
        private static readonly State<int> s_current2 = new(1);
        private static readonly StateText s_empty = new(1);
        private static readonly State<bool> s_embedAllInsideAChildWindow = new(false);
        private static readonly State<bool> s_testWindow = new(false);
        private static readonly TextFilter s_filter = new();

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
                    Imgui.PushStyleColor(StyleColor.Button, ColorF.FromHsv(i / 7.0f, 0.6f, 0.6f));
                    Imgui.PushStyleColor(StyleColor.ButtonHovered, ColorF.FromHsv(i / 7.0f, 0.7f, 0.7f));
                    Imgui.PushStyleColor(StyleColor.ButtonActive, ColorF.FromHsv(i / 7.0f, 0.8f, 0.8f));
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
                    _ = Imgui.Combo("combo", s_itemCurrent1, new[] { "AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIIIIII", "JJJJ", "KKKKKKK" });
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

                {
                    _ = Imgui.ColorEdit("color 1", s_col1);
                    Imgui.SameLine();
                    HelpMarker(
                        "Click on the color square to open a color picker.\n" +
                        "Click and hold to use drag and drop.\n" +
                        "Right-click on the color square to show options.\n" +
                        "CTRL+click on individual component to input value.\n");

                    _ = Imgui.ColorEdit("color 2", s_col2);
                }

                {
                    _ = Imgui.ListBox("listbox", s_itemCurrent2, new[] { "Apple", "Banana", "Cherry", "Kiwi", "Mango", "Orange", "Pineapple", "Strawberry", "Watermelon" }, 4);
                    Imgui.SameLine(); HelpMarker(
                        "Using the simplified one-liner ListBox API here.\nRefer to the \"List boxes\" section below for an explanation of how to use the more flexible and general BeginListBox/EndListBox API.");
                }

                {
                    Imgui.AlignTextToFramePadding();
                    Imgui.Text("Tooltips:");

                    Imgui.SameLine();
                    _ = Imgui.Button("Button");
                    if (Imgui.IsItemHovered())
                    {
                        Imgui.SetTooltip("I am a tooltip");
                    }

                    Imgui.SameLine();
                    _ = Imgui.Button("Fancy");
                    if (Imgui.IsItemHovered())
                    {
                        Imgui.BeginTooltip();
                        Imgui.Text("I am a fancy tooltip");
                        Imgui.PlotLines("Curve", new[] { 0.6f, 0.1f, 1.0f, 0.5f, 0.92f, 0.1f, 0.2f });
                        Imgui.Text($"Sin(time) = {Math.Sin(Imgui.GetTime())}");
                        Imgui.EndTooltip();
                    }

                    Imgui.SameLine();
                    _ = Imgui.Button("Delayed");
                    if (Imgui.IsItemHovered(HoveredOptions.DelayNormal))
                    {
                        Imgui.SetTooltip("I am a tooltip with a delay.");
                    }

                    Imgui.SameLine();
                    HelpMarker(
                        "Tooltip are created by using the IsItemHovered() function over any kind of item.");
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Trees"))
            {
                if (Imgui.TreeNode("Basic trees"))
                {
                    for (var i = 0; i < 5; i++)
                    {
                        if (i == 0)
                        {
                            Imgui.SetNextItemOpen(true, Condition.Once);
                        }

                        if (Imgui.TreeNode((nuint)i, $"Child {i}"))
                        {
                            Imgui.Text("blah blah");
                            Imgui.SameLine();
                            if (Imgui.SmallButton("button")) { }
                            Imgui.TreePop();
                        }
                    }
                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Advanced, with Selectable nodes"))
                {
                    HelpMarker(
                        "This is a more typical looking tree with selectable nodes.\n" +
                        "Click to select, CTRL+Click to toggle, click on arrows or double-click to open.");
                    _ = Imgui.CheckboxFlags("TreeNodeOptions.OpenOnArrow", s_baseFlags, TreeNodeOptions.OpenOnArrow);
                    _ = Imgui.CheckboxFlags("TreeNodeOptions.OpenOnDoubleClick", s_baseFlags, TreeNodeOptions.OpenOnDoubleClick);
                    _ = Imgui.CheckboxFlags("TreeNodeOptions.SpanAvailWidth", s_baseFlags, TreeNodeOptions.SpanAvailWidth);
                    Imgui.SameLine();
                    HelpMarker("Extend hit area to all available width instead of allowing more items to be laid out after the node.");
                    _ = Imgui.CheckboxFlags("TreeNodeOptions.SpanFullWidth", s_baseFlags, TreeNodeOptions.SpanFullWidth);
                    _ = Imgui.Checkbox("Align label with current X position", s_alignLabelWithCurrentXPosition);
                    _ = Imgui.Checkbox("Test tree node as drag source", s_testDragAndDrop);
                    Imgui.Text("Hello!");
                    if (s_alignLabelWithCurrentXPosition)
                    {
                        Imgui.Unindent(Imgui.GetTreeNodeToLabelSpacing());
                    }

                    var nodeClicked = -1;
                    for (var i = 0; i < 6; i++)
                    {
                        var nodeFlags = (TreeNodeOptions)s_baseFlags;
                        var isSelected = (s_selectionMask & (1 << i)) != 0;
                        if (isSelected)
                        {
                            nodeFlags |= TreeNodeOptions.Selected;
                        }

                        if (i < 3)
                        {
                            var node_open = Imgui.TreeNode((nuint)i, nodeFlags, $"Selectable Node {i}");
                            if (Imgui.IsItemClicked() && !Imgui.IsItemToggledOpen())
                            {
                                nodeClicked = i;
                            }

                            if (s_testDragAndDrop && Imgui.BeginDragDropSource())
                            {
                                _ = Imgui.SetDragDropPayload<byte>("_TREENODE", null, 0);
                                Imgui.Text("This is a drag and drop source");
                                Imgui.EndDragDropSource();
                            }
                            if (node_open)
                            {
                                Imgui.BulletText("Blah blah\nBlah Blah");
                                Imgui.TreePop();
                            }
                        }
                        else
                        {
                            nodeFlags |= TreeNodeOptions.Leaf | TreeNodeOptions.NoTreePushOnOpen;
                            _ = Imgui.TreeNode((nuint)i, nodeFlags, $"Selectable Leaf {i}");
                            if (Imgui.IsItemClicked() && !Imgui.IsItemToggledOpen())
                            {
                                nodeClicked = i;
                            }

                            if (s_testDragAndDrop && Imgui.BeginDragDropSource())
                            {
                                _ = Imgui.SetDragDropPayload<byte>("_TREENODE", null, 0);
                                Imgui.Text("This is a drag and drop source");
                                Imgui.EndDragDropSource();
                            }
                        }
                    }
                    if (nodeClicked != -1)
                    {
                        if (Imgui.GetIo().ControlKeyDown)
                        {
                            s_selectionMask ^= 1 << nodeClicked;
                        }
                        else
                        {
                            s_selectionMask = 1 << nodeClicked;           // Click to single-select
                        }
                    }
                    if (s_alignLabelWithCurrentXPosition)
                    {
                        Imgui.Indent(Imgui.GetTreeNodeToLabelSpacing());
                    }

                    Imgui.TreePop();
                }
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Collapsing Headers"))
            {
                _ = Imgui.Checkbox("Show 2nd header", s_closableGroup);
                if (Imgui.CollapsingHeader("Header", TreeNodeOptions.None))
                {
                    Imgui.Text($"IsItemHovered: {Imgui.IsItemHovered()}");
                    for (var i = 0; i < 5; i++)
                    {
                        Imgui.Text($"Some content {i}");
                    }
                }
                if (Imgui.CollapsingHeader("Header with a close button", s_closableGroup))
                {
                    Imgui.Text($"IsItemHovered: {Imgui.IsItemHovered()}");
                    for (var i = 0; i < 5; i++)
                    {
                        Imgui.Text($"More content {i}");
                    }
                }
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Bullets"))
            {
                Imgui.BulletText("Bullet point 1");
                Imgui.BulletText("Bullet point 2\nOn multiple lines");
                if (Imgui.TreeNode("Tree node"))
                {
                    Imgui.BulletText("Another bullet point");
                    Imgui.TreePop();
                }
                Imgui.Bullet();
                Imgui.Text("Bullet point 3 (two calls)");
                Imgui.Bullet();
                _ = Imgui.SmallButton("Button");
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Text"))
            {
                if (Imgui.TreeNode("Colorful Text"))
                {
                    Imgui.TextColored(new(1.0f, 0.0f, 1.0f, 1.0f), "Pink");
                    Imgui.TextColored(new(1.0f, 1.0f, 0.0f, 1.0f), "Yellow");
                    Imgui.TextDisabled("Disabled");
                    Imgui.SameLine(); HelpMarker("The TextDisabled color is stored in ImGuiStyle.");
                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Word Wrapping"))
                {
                    Imgui.TextWrapped(
                        "This text should automatically wrap on the edge of the window. The current implementation " +
                        "for text wrapping follows simple rules suitable for English and possibly other languages.");
                    Imgui.Spacing();

                    _ = Imgui.Slider("Wrap width", s_wrapWidth, -20, 600, "%.0f");

                    var drawList = Imgui.GetWindowDrawList()!.Value;
                    for (var n = 0; n < 2; n++)
                    {
                        Imgui.Text($"Test paragraph {n}:");
                        var pos = Imgui.GetCursorScreenPosition();
                        RectangleF marker = new(new(pos.X + s_wrapWidth, pos.Y), new(pos.X + s_wrapWidth + 10, pos.Y + Imgui.GetTextLineHeight()));
                        Imgui.PushTextWrapPosition(Imgui.GetCursorPosition().X + s_wrapWidth);
                        if (n == 0)
                        {
                            Imgui.Text($"The lazy dog is a good dog. This paragraph should fit within {(float)s_wrapWidth} pixels. Testing a 1 character word. The quick brown fox jumps over the lazy dog.");
                        }
                        else
                        {
                            Imgui.Text("aaaaaaaa bbbbbbbb, c cccccccc,dddddddd. d eeeeeeee   ffffffff. gggggggg!hhhhhhhh");
                        }

                        drawList.AddRectangle(Imgui.GetItemRectangle(), new Color(255, 255, 0, 255));
                        drawList.AddRectangleFilled(marker, new Color(255, 0, 255, 255));
                        Imgui.PopTextWrapPosition();
                    }

                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Unicode Text"))
                {
                    Imgui.TextWrapped(
                        "CJK text will only appear if the font was loaded with the appropriate CJK character ranges. " +
                        "Call io.Fonts.AddFontFromFileTtf() manually to load extra character ranges. " +
                        "Read docs/FONTS.md for details.");
                    Imgui.Text("Hiragana: かきくけこ (kakikukeko)");
                    Imgui.Text("Kanjis: 日本語 (nihongo)");
                    _ = Imgui.InputText("UTF-8 input", new(32, "日本語"));
                    Imgui.TreePop();
                }
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Images"))
            {
                var io = Imgui.GetIo();
                Imgui.TextWrapped(
                    "Below we are displaying the font texture (which is the only texture we have access to in this demo). " +
                    "Use the 'TextureId' type as storage to pass pointers or identifier to your own texture data. " +
                    "Hover the texture for a zoomed view!");

                var myTextureId = io.Fonts.TextureId;
                io.Fonts.GetTextureDataAsRgba32(out _, out var textureSize, out _);
                var myTextureSize = (SizeF)textureSize;
                {
                    Imgui.Text($"{myTextureSize.Width}x{myTextureSize.Height}");
                    var position = Imgui.GetCursorScreenPosition();
                    TextureRectangle rect = new(new(0.0f, 0.0f), new(1.0f, 1.0f));
                    ColorF tint_col = new(1.0f, 1.0f, 1.0f, 1.0f);
                    ColorF border_col = new(1.0f, 1.0f, 1.0f, 0.5f);
                    Imgui.Image(myTextureId, myTextureSize, rect, tint_col, border_col);
                    if (Imgui.IsItemHovered())
                    {
                        Imgui.BeginTooltip();
                        var regionSize = 32.0f;
                        var regionX = io.MousePosition.X - position.X - (regionSize * 0.5f);
                        var regionY = io.MousePosition.Y - position.Y - (regionSize * 0.5f);
                        var zoom = 4.0f;
                        if (regionX < 0.0f)
                        {
                            regionX = 0.0f;
                        }
                        else if (regionX > myTextureSize.Width - regionSize)
                        {
                            regionX = myTextureSize.Width - regionSize;
                        }
                        if (regionY < 0.0f)
                        {
                            regionY = 0.0f;
                        }
                        else if (regionY > myTextureSize.Height - regionSize)
                        {
                            regionY = myTextureSize.Height - regionSize;
                        }
                        Imgui.Text($"Min: ({regionX}, {regionY})");
                        Imgui.Text($"Max: ({regionX + regionSize}, {regionY + regionSize})");
                        TexturePosition uv0 = new(regionX / myTextureSize.Width, regionY / myTextureSize.Height);
                        TexturePosition uv1 = new((regionX + regionSize) / myTextureSize.Width, (regionY + regionSize) / myTextureSize.Height);
                        Imgui.Image(myTextureId, new(regionSize * zoom, regionSize * zoom), new(uv0, uv1), tint_col, border_col);
                        Imgui.EndTooltip();
                    }
                }

                Imgui.TextWrapped("And now some textured buttons..");
                for (var i = 0; i < 8; i++)
                {
                    Imgui.PushId(i);
                    if (i > 0)
                    {
                        Imgui.PushStyleVariable(StyleVariable.FramePadding, new SizeF(i - 1.0f, i - 1.0f));
                    }

                    SizeF size = new(32.0f, 32.0f);
                    TextureRectangle rect = new(new(0.0f, 0.0f), new(32.0f / myTextureSize.Width, 32.0f / myTextureSize.Height));
                    ColorF bg_col = new(0.0f, 0.0f, 0.0f, 1.0f);
                    ColorF tint_col = new(1.0f, 1.0f, 1.0f, 1.0f);
                    if (Imgui.ImageButton("", myTextureId, size, rect, bg_col, tint_col))
                    {
                        s_pressedCount += 1;
                    }

                    if (i > 0)
                    {
                        Imgui.PopStyleVariable();
                    }

                    Imgui.PopId();
                    Imgui.SameLine();
                }
                Imgui.NewLine();
                Imgui.Text($"Pressed {s_pressedCount} times.");
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Combo"))
            {
                _ = Imgui.CheckboxFlags("ComboOptions.PopupAlignLeft", s_flags, ComboOptions.PopupAlignLeft);
                Imgui.SameLine();
                HelpMarker("Only makes a difference if the popup is larger than the combo");
                if (Imgui.CheckboxFlags("ComboOptions.NoArrowButton", s_flags, ComboOptions.NoArrowButton))
                {
                    s_flags.Value &= ~ComboOptions.NoPreview;
                }

                if (Imgui.CheckboxFlags("ComboOptions.NoPreview", s_flags, ComboOptions.NoPreview))
                {
                    s_flags.Value &= ~ComboOptions.NoArrowButton;
                }

                var items = new[] { "AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIII", "JJJJ", "KKKK", "LLLLLLL", "MMMM", "OOOOOOO" };
                if (Imgui.BeginCombo("combo 1", items[s_itemCurrentIdx], s_flags))
                {
                    for (var n = 0; n < items.Length; n++)
                    {
                        var isSelected = s_itemCurrentIdx == n;
                        if (Imgui.Selectable(items[n], isSelected))
                        {
                            s_itemCurrentIdx = n;
                        }

                        if (isSelected)
                        {
                            Imgui.SetItemDefaultFocus();
                        }
                    }
                    Imgui.EndCombo();
                }

                _ = Imgui.Combo("combo 2 (one-liner)", s_itemCurrent3, "aaaa\0bbbb\0cccc\0dddd\0eeee\0\0");

                _ = Imgui.Combo("combo 3 (array)", s_itemCurrent4, items);

                // Missing function callback due to string free issues

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("List boxes"))
            {
                var items = new[] { "AAAA", "BBBB", "CCCC", "DDDD", "EEEE", "FFFF", "GGGG", "HHHH", "IIII", "JJJJ", "KKKK", "LLLLLLL", "MMMM", "OOOOOOO" };
                if (Imgui.BeginListBox("listbox 1"))
                {
                    for (var n = 0; n < items.Length; n++)
                    {
                        var isSelected = s_itemCurrentIdx2 == n;
                        if (Imgui.Selectable(items[n], isSelected))
                        {
                            s_itemCurrentIdx2 = n;
                        }

                        if (isSelected)
                        {
                            Imgui.SetItemDefaultFocus();
                        }
                    }
                    Imgui.EndListBox();
                }

                Imgui.Text("Full-width:");
                if (Imgui.BeginListBox("##listbox 2", new(-SizeF.MinNormalizedValue, 5 * Imgui.GetTextLineHeightWithSpacing())))
                {
                    for (var n = 0; n < items.Length; n++)
                    {
                        var isSelected = s_itemCurrentIdx2 == n;
                        if (Imgui.Selectable(items[n], isSelected))
                        {
                            s_itemCurrentIdx2 = n;
                        }

                        if (isSelected)
                        {
                            Imgui.SetItemDefaultFocus();
                        }
                    }
                    Imgui.EndListBox();
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Selectables"))
            {
                if (Imgui.TreeNode("Basic"))
                {
                    _ = Imgui.Selectable("1. I am selectable", s_selected.GetStateOfElement(0));
                    _ = Imgui.Selectable("2. I am selectable", s_selected.GetStateOfElement(1));
                    Imgui.Text("(I am not selectable)");
                    _ = Imgui.Selectable("4. I am selectable", s_selected.GetStateOfElement(3));
                    if (Imgui.Selectable("5. I am double clickable", s_selected[4], SelectableOptions.AllowDoubleClick))
                    {
                        if (Imgui.IsMouseDoubleClicked(0))
                        {
                            s_selected[4] = !s_selected[4];
                        }
                    }

                    Imgui.TreePop();
                }
                if (Imgui.TreeNode("Selection State: Single Selection"))
                {
                    for (var n = 0; n < 5; n++)
                    {
                        if (Imgui.Selectable($"Object {n}", s_selected2 == n))
                        {
                            s_selected2 = n;
                        }
                    }
                    Imgui.TreePop();
                }
                if (Imgui.TreeNode("Selection State: Multiple Selection"))
                {
                    HelpMarker("Hold CTRL and click to select multiple items.");
                    for (var n = 0; n < 5; n++)
                    {
                        if (Imgui.Selectable($"Object {n}", s_selected3[n]))
                        {
                            if (!Imgui.GetIo().ControlKeyDown)
                            {
                                for (var i = 0; i < 5; i++)
                                {
                                    s_selected3[i] = false;
                                }
                            }
                            s_selected3[n] ^= true;
                        }
                    }
                    Imgui.TreePop();
                }
                if (Imgui.TreeNode("Rendering more text into the same line"))
                {
                    _ = Imgui.Selectable("main.c", s_selected4.GetStateOfElement(0));
                    Imgui.SameLine(300);
                    Imgui.Text(" 2,345 bytes");
                    _ = Imgui.Selectable("Hello.cpp", s_selected4.GetStateOfElement(1));
                    Imgui.SameLine(300);
                    Imgui.Text("12,345 bytes");
                    _ = Imgui.Selectable("Hello.h", s_selected.GetStateOfElement(2));
                    Imgui.SameLine(300);
                    Imgui.Text(" 2,345 bytes");
                    Imgui.TreePop();
                }
                if (Imgui.TreeNode("In columns"))
                {
                    if (Imgui.BeginTable("split1", 3, TableOptions.Resizable | TableOptions.NoSavedSettings | TableOptions.Borders))
                    {
                        for (var i = 0; i < 10; i++)
                        {
                            _ = Imgui.TableNextColumn();
                            _ = Imgui.Selectable($"Item {i}", s_selected5.GetStateOfElement(i)); // FIXME-TABLE: Selection overlap
                        }
                        Imgui.EndTable();
                    }
                    Imgui.Spacing();
                    if (Imgui.BeginTable("split2", 3, TableOptions.Resizable | TableOptions.NoSavedSettings | TableOptions.Borders))
                    {
                        for (var i = 0; i < 10; i++)
                        {
                            Imgui.TableNextRow();
                            _ = Imgui.TableNextColumn();
                            _ = Imgui.Selectable($"Item {i}", s_selected5.GetStateOfElement(i), SelectableOptions.SpanAllColumns);
                            _ = Imgui.TableNextColumn();
                            Imgui.Text("Some other contents");
                            _ = Imgui.TableNextColumn();
                            Imgui.Text("123456");
                        }
                        Imgui.EndTable();
                    }
                    Imgui.TreePop();
                }
                if (Imgui.TreeNode("Grid"))
                {
                    var time = (float)Imgui.GetTime();
                    var winning_state = s_selected6.All(a => a.All(v => v));
                    if (winning_state)
                    {
                        Imgui.PushStyleVariable(StyleVariable.SelectableTextAlign, new SizeF(0.5f + (0.5f * (float)Math.Cos(time * 2.0f)), 0.5f + (0.5f * (float)Math.Sin(time * 3.0f))));
                    }

                    for (var y = 0; y < 4; y++)
                    {
                        for (var x = 0; x < 4; x++)
                        {
                            if (x > 0)
                            {
                                Imgui.SameLine();
                            }

                            Imgui.PushId((y * 4) + x);
                            if (Imgui.Selectable("Sailor", s_selected6[y][x], 0, new SizeF(50, 50)))
                            {
                                s_selected6[y][x] ^= true;
                                if (x > 0)
                                {
                                    s_selected6[y][x - 1] ^= true;
                                }
                                if (x < 3)
                                {
                                    s_selected6[y][x + 1] ^= true;
                                }
                                if (y > 0)
                                {
                                    s_selected6[y - 1][x] ^= true;
                                }
                                if (y < 3)
                                {
                                    s_selected6[y + 1][x] ^= true;
                                }
                            }
                            Imgui.PopId();
                        }
                    }

                    if (winning_state)
                    {
                        Imgui.PopStyleVariable();
                    }

                    Imgui.TreePop();
                }
                if (Imgui.TreeNode("Alignment"))
                {
                    HelpMarker(
                        "By default, Selectables uses style.SelectableTextAlign but it can be overridden on a per-item " +
                        "basis using PushStyleVariable(). You'll probably want to always keep your default situation to " +
                        "left-align otherwise it becomes difficult to layout multiple items on a same line");
                    for (var y = 0; y < 3; y++)
                    {
                        for (var x = 0; x < 3; x++)
                        {
                            PositionF alignment = new(x / 2.0f, y / 2.0f);
                            if (x > 0)
                            {
                                Imgui.SameLine();
                            }

                            Imgui.PushStyleVariable(StyleVariable.SelectableTextAlign, alignment);
                            _ = Imgui.Selectable($"({alignment.X:F1},{alignment.Y:F1})", s_selected7.GetStateOfElement((3 * y) + x), SelectableOptions.None, new(80, 80));
                            Imgui.PopStyleVariable();
                        }
                    }
                    Imgui.TreePop();
                }
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Text Input"))
            {
                if (Imgui.TreeNode("Multi-line Text Input"))
                {
                    HelpMarker("You can use the ImGuiInputTextFlags_CallbackResize facility if you need to wire InputTextMultiline() to a dynamic string type. See misc/cpp/imgui_stdlib.h for an example. (This is not demonstrated in imgui_demo.cpp because we don't want to include <string> in here)");
                    _ = Imgui.CheckboxFlags("InputTextOptions.ReadOnly", s_flags2, InputTextOptions.ReadOnly);
                    _ = Imgui.CheckboxFlags("InputTextOptions.AllowTabInput", s_flags2, InputTextOptions.AllowTabInput);
                    _ = Imgui.CheckboxFlags("InputTextOptions.CtrlEnterForNewLine", s_flags2, InputTextOptions.CtrlEnterForNewLine);
                    _ = Imgui.InputTextMultiline("##source", s_text, new(-SizeF.MinNormalizedValue, Imgui.GetTextLineHeight() * 16), s_flags2);
                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Filtered Text Input"))
                {
                    _ = Imgui.InputText("default", s_buf1);
                    _ = Imgui.InputText("decimal", s_buf2, InputTextOptions.CharsDecimal);
                    _ = Imgui.InputText("hexadecimal", s_buf3, InputTextOptions.CharsHexadecimal | InputTextOptions.CharsUppercase);
                    _ = Imgui.InputText("uppercase", s_buf4, InputTextOptions.CharsUppercase);
                    _ = Imgui.InputText("no blank", s_buf5, InputTextOptions.CharsNoBlank);
                    _ = Imgui.InputText("\"imgui\" letters", s_buf6, 0, new(Filter: c => c switch
                    {
                        'i' or 'm' or 'g' or 'u' => c,
                        _ => null
                    }));
                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Password Input"))
                {
                    _ = Imgui.InputText("password", s_password, InputTextOptions.Password);
                    Imgui.SameLine();
                    HelpMarker("Display all characters as '*'.\nDisable clipboard cut and copy.\nDisable logging.\n");
                    _ = Imgui.InputText("password (w/ hint)", "<password>", s_password, InputTextOptions.Password);
                    _ = Imgui.InputText("password (clear)", s_password);
                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Completion, History, Edit Callbacks"))
                {
                    _ = Imgui.InputText("Completion", s_buf7, default, new(Completion: (key, state) => state.InsertChars(state.CursorPosition, "..")));
                    Imgui.SameLine();
                    HelpMarker("Here we append \"..\" each time Tab is pressed. See 'Examples>Console' for a more meaningful demonstration of using this callback.");

                    _ = Imgui.InputText("History", s_buf8, default, new(History: (key, state) =>
                    {
                        if (key == Key.UpArrow)
                        {
                            state.DeleteChars(0, state.Length);
                            state.InsertChars(0, "Pressed Up!");
                            state.SelectAll();
                        }
                        else if (key == Key.DownArrow)
                        {
                            state.DeleteChars(0, state.Length);
                            state.InsertChars(0, "Pressed Down!");
                            state.SelectAll();
                        }
                    }));
                    Imgui.SameLine();
                    HelpMarker("Here we replace and select text each time Up/Down are pressed. See 'Examples>Console' for a more meaningful demonstration of using this callback.");

                    _ = Imgui.InputText("Edit", s_buf9, default, new(Edit: state =>
                    {
                        var selectionStart = state.SelectionStart;
                        var selectionEnd = state.SelectionEnd;
                        var cursor = state.CursorPosition;
                        var before = state.Text;
                        var after = $"{(char.IsLetter(before[0]) ? char.ToUpperInvariant(before[0]) : before[0])}{before[1..]}";
                        state.DeleteChars(0, state.Length);
                        state.InsertChars(0, after);
                        state.CursorPosition = cursor;
                        state.SelectionStart = selectionStart;
                        state.SelectionEnd = selectionEnd;
                        s_editCount++;

                    }));
                    Imgui.SameLine();
                    HelpMarker("Here we toggle the casing of the first character on every edit + count edits.");
                    Imgui.SameLine();
                    Imgui.Text($"({s_editCount})");

                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Resize Callback"))
                {
                    HelpMarker("Using InputTextCallbacks.Resize to resize your StateText in InputText().");
                    _ = Imgui.InputTextMultiline("##MyStr", s_myStr, new(-SizeF.MinNormalizedValue, Imgui.GetTextLineHeight() * 16), default, new(Resize: (text, newSize) =>
                    {
                        if (newSize > text.Capacity)
                        {
                            text.Resize(Math.Max(newSize, text.Capacity + (text.Capacity / 2)));
                        }
                    }));
                    Imgui.Text($"Data: {s_myStr}\nSize: {s_myStr.ToString().Length}\nCapacity: {s_myStr.Capacity}");
                    Imgui.TreePop();
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Tabs"))
            {
                if (Imgui.TreeNode("Basic"))
                {
                    var tab_bar_flags = TabBarOptions.None;
                    if (Imgui.BeginTabBar("MyTabBar", tab_bar_flags))
                    {
                        if (Imgui.BeginTabItem("Avocado"))
                        {
                            Imgui.Text("This is the Avocado tab!\nblah blah blah blah blah");
                            Imgui.EndTabItem();
                        }
                        if (Imgui.BeginTabItem("Broccoli"))
                        {
                            Imgui.Text("This is the Broccoli tab!\nblah blah blah blah blah");
                            Imgui.EndTabItem();
                        }
                        if (Imgui.BeginTabItem("Cucumber"))
                        {
                            Imgui.Text("This is the Cucumber tab!\nblah blah blah blah blah");
                            Imgui.EndTabItem();
                        }
                        Imgui.EndTabBar();
                    }
                    Imgui.Separator();
                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Advanced & Close Button"))
                {
                    _ = Imgui.CheckboxFlags("TabBarOptions.Reorderable", s_tabBarOptions, TabBarOptions.Reorderable);
                    _ = Imgui.CheckboxFlags("TabBarOptions.AutoSelectNewTabs", s_tabBarOptions, TabBarOptions.AutoSelectNewTabs);
                    _ = Imgui.CheckboxFlags("TabBarOptions.TabListPopupButton", s_tabBarOptions, TabBarOptions.TabListPopupButton);
                    _ = Imgui.CheckboxFlags("TabBarOptions.NoCloseWithMiddleMouseButton", s_tabBarOptions, TabBarOptions.NoCloseWithMiddleMouseButton);
                    if ((s_tabBarOptions & TabBarOptions.FittingPolicyMask) == 0)
                    {
                        s_tabBarOptions.Value |= TabBarOptions.FittingPolicyDefault;
                    }

                    if (Imgui.CheckboxFlags("TabBarOptions.FittingPolicyResizeDown", s_tabBarOptions, TabBarOptions.FittingPolicyResizeDown))
                    {
                        s_tabBarOptions.Value &= ~(TabBarOptions.FittingPolicyMask ^ TabBarOptions.FittingPolicyResizeDown);
                    }

                    if (Imgui.CheckboxFlags("TabBarOptions.FittingPolicyScroll", s_tabBarOptions, TabBarOptions.FittingPolicyScroll))
                    {
                        s_tabBarOptions.Value &= ~(TabBarOptions.FittingPolicyMask ^ TabBarOptions.FittingPolicyScroll);
                    }

                    var names = new[] { "Artichoke", "Beetroot", "Celery", "Daikon" };
                    for (var n = 0; n < s_opened.Length; n++)
                    {
                        if (n > 0)
                        {
                            Imgui.SameLine();
                        }
                        _ = Imgui.Checkbox(names[n], s_opened.GetStateOfElement(n));
                    }

                    if (Imgui.BeginTabBar("MyTabBar", s_tabBarOptions))
                    {
                        for (var n = 0; n < s_opened.Length; n++)
                        {
                            if (s_opened[n] && Imgui.BeginTabItem(names[n], s_opened.GetStateOfElement(n), TabItemOptions.None))
                            {
                                Imgui.Text($"This is the {names[n]} tab!");
                                if ((n & 1) == 1)
                                {
                                    Imgui.Text("I am an odd tab.");
                                }

                                Imgui.EndTabItem();
                            }
                        }

                        Imgui.EndTabBar();
                    }
                    Imgui.Separator();
                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("TabItemButton & Leading/Trailing flags"))
                {
                    if (s_nextTabId == 0)
                    {
                        for (var i = 0; i < 3; i++)
                        {
                            s_activeTabs.Add(s_nextTabId++);
                        }
                    }

                    _ = Imgui.Checkbox("Show Leading TabItemButton()", s_showLeadingButton);
                    _ = Imgui.Checkbox("Show Trailing TabItemButton()", s_showTrailingButton);

                    _ = Imgui.CheckboxFlags("TabBarOptions.TabListPopupButton", s_tabBarOptions2, TabBarOptions.TabListPopupButton);
                    if (Imgui.CheckboxFlags("TabBarOptions.FittingPolicyResizeDown", s_tabBarOptions2, TabBarOptions.FittingPolicyResizeDown))
                    {
                        s_tabBarOptions2.Value &= ~(TabBarOptions.FittingPolicyMask ^ TabBarOptions.FittingPolicyResizeDown);
                    }

                    if (Imgui.CheckboxFlags("TabBarOptions.FittingPolicyScroll", s_tabBarOptions2, TabBarOptions.FittingPolicyScroll))
                    {
                        s_tabBarOptions2.Value &= ~(TabBarOptions.FittingPolicyMask ^ TabBarOptions.FittingPolicyScroll);
                    }

                    if (Imgui.BeginTabBar("MyTabBar", s_tabBarOptions2))
                    {
                        if (s_showLeadingButton)
                        {
                            if (Imgui.TabItemButton("?", TabItemOptions.Leading | TabItemOptions.NoTooltip))
                            {
                                Imgui.OpenPopup("MyHelpMenu");
                            }
                        }

                        if (Imgui.BeginPopup("MyHelpMenu"))
                        {
                            _ = Imgui.Selectable("Hello!");
                            Imgui.EndPopup();
                        }

                        if (s_showTrailingButton)
                        {
                            if (Imgui.TabItemButton("+", TabItemOptions.Trailing | TabItemOptions.NoTooltip))
                            {
                                s_activeTabs.Add(s_nextTabId++);
                            }
                        }

                        // Submit our regular tabs
                        for (var n = 0; n < s_activeTabs.Count;)
                        {
                            State<bool> open = new(true);
                            if (Imgui.BeginTabItem($"{s_activeTabs[n]:D4}", open, TabItemOptions.None))
                            {
                                Imgui.Text($"This is the {s_activeTabs[n]:D4} tab!");
                                Imgui.EndTabItem();
                            }

                            if (!open)
                            {
                                _ = s_activeTabs.Remove(n);
                            }
                            else
                            {
                                n++;
                            }
                        }

                        Imgui.EndTabBar();
                    }
                    Imgui.Separator();
                    Imgui.TreePop();
                }
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Plotting"))
            {
                _ = Imgui.Checkbox("Animate", s_animate);

                var arr = new[] { 0.6f, 0.1f, 1.0f, 0.5f, 0.92f, 0.1f, 0.2f };
                Imgui.PlotLines("Frame Times", arr);
                Imgui.PlotHistogram("Histogram", arr, 0, null, 0.0f, 1.0f, new(0, 80.0f));

                if (!s_animate || s_refreshTime == 0.0)
                {
                    s_refreshTime = Imgui.GetTime();
                }

                while (s_refreshTime < Imgui.GetTime())
                {
                    s_values[s_valuesOffset] = (float)Math.Cos(s_phase);
                    s_valuesOffset = (s_valuesOffset + 1) % s_values.Length;
                    s_phase += 0.10f * s_valuesOffset;
                    s_refreshTime += 1.0f / 60.0f;
                }

                {
                    var average = 0.0f;
                    for (var n = 0; n < s_values.Length; n++)
                    {
                        average += s_values[n];
                    }

                    average /= s_values.Length;
                    Imgui.PlotLines("Lines", s_values, s_valuesOffset, $"avg {average:F6}", -1.0f, 1.0f, new(0, 80.0f));
                }

                static float Sin(int i) => (float)Math.Sin(i * 0.1F);
                static float Saw(int i) => ((i & 1) == 1) ? 1.0f : -1.0f;

                Imgui.Separator();
                Imgui.SetNextItemWidth(Imgui.GetFontSize() * 8);
                _ = Imgui.Combo("func", s_funcType, "Sin\0Saw\0");
                Imgui.SameLine();
                _ = Imgui.Slider("Sample count", s_displayCount, 1, 400);
                Func<int, float> func = s_funcType == 0 ? Sin : Saw;
                Imgui.PlotLines("Lines", func, s_displayCount, 0, null, -1.0f, 1.0f, new(0, 80));
                Imgui.PlotHistogram("Histogram", func, s_displayCount, 0, null, -1.0f, 1.0f, new(0, 80));
                Imgui.Separator();

                // Animate a simple progress bar
                if (s_animate)
                {
                    s_progress += s_progressDir * 0.4f * Imgui.GetIo().DeltaTime;
                    if (s_progress >= +1.1f)
                    {
                        s_progress = +1.1f;
                        s_progressDir *= -1.0f;
                    }

                    if (s_progress <= -0.1f)
                    {
                        s_progress = -0.1f;
                        s_progressDir *= -1.0f;
                    }
                }

                Imgui.ProgressBar(s_progress, new(0.0f, 0.0f));
                Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.Width);
                Imgui.Text("Progress Bar");

                var progress_saturated = Math.Clamp(s_progress, 0.0f, 1.0f);
                Imgui.ProgressBar(s_progress, new(0.0f, 0.0f), $"{(int)(progress_saturated * 1753)}/{1753}");
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Color/Picker Widgets"))
            {
                _ = Imgui.Checkbox("With Alpha Preview", s_alphaPreview);
                _ = Imgui.Checkbox("With Half Alpha Preview", s_alphaHalfPreview);
                _ = Imgui.Checkbox("With Drag and Drop", s_dragAndDrop);
                _ = Imgui.Checkbox("With Options Menu", s_optionsMenu);
                Imgui.SameLine();
                HelpMarker("Right-click on the individual color widget to show options.");
                _ = Imgui.Checkbox("With HDR", s_hdr);
                Imgui.SameLine();
                HelpMarker("Currently all this does is to lift the 0..1 limits on dragging widgets.");
                var misc_flags = (s_hdr ? ColorEditOptions.HDR : 0) | (s_dragAndDrop ? 0 : ColorEditOptions.NoDragDrop) | (s_alphaHalfPreview ? ColorEditOptions.AlphaPreviewHalf : (s_alphaPreview ? ColorEditOptions.AlphaPreview : 0)) | (s_optionsMenu ? 0 : ColorEditOptions.NoOptions);

                Imgui.Text("Color widget:");
                Imgui.SameLine();
                HelpMarker(
                    "Click on the color square to open a color picker.\n" +
                    "CTRL+click on individual component to input value.\n");
                _ = Imgui.ColorEdit("MyColor##1", s_color, misc_flags);

                Imgui.Text("Color widget HSV with Alpha:");
                _ = Imgui.ColorEditAlpha("MyColor##2", s_color, ColorEditOptions.DisplayHSV | misc_flags);

                Imgui.Text("Color widget with Float Display:");
                _ = Imgui.ColorEditAlpha("MyColor##2f", s_color, ColorEditOptions.Float | misc_flags);

                Imgui.Text("Color button with Picker:");
                Imgui.SameLine(); HelpMarker(
                    "With the ColorEditOptions.NoInputs flag you can hide all the slider/text inputs.\n" +
                    "With the ColorEditOptions.NoLabel flag you can pass a non-empty label which will only " +
                    "be used for the tooltip and picker popup.");
                _ = Imgui.ColorEditAlpha("MyColor##3", s_color, ColorEditOptions.NoInputs | ColorEditOptions.NoLabel | misc_flags);

                Imgui.Text("Color button with Custom Picker Popup:");

                if (s_savedPaletteInit)
                {
                    for (var n = 0; n < s_savedPalette.Length; n++)
                    {
                        s_savedPalette[n] = Imgui.ColorConvertHsvToRgb(n / 31.0f, 0.8f, 0.8f);
                    }
                    s_savedPaletteInit = false;
                }

                var open_popup = Imgui.ColorButton("MyColor##3b", s_color, misc_flags);
                Imgui.SameLine(0, Imgui.GetStyle().ItemInnerSpacing.Width);
                open_popup |= Imgui.Button("Palette");
                if (open_popup)
                {
                    Imgui.OpenPopup("mypicker");
                    s_backupColor = s_color;
                }
                if (Imgui.BeginPopup("mypicker"))
                {
                    Imgui.Text("MY CUSTOM COLOR PICKER WITH AN AMAZING PALETTE!");
                    Imgui.Separator();
                    _ = Imgui.ColorPickerAlpha("##picker", s_color, misc_flags | ColorEditOptions.NoSidePreview | ColorEditOptions.NoSmallPreview);
                    Imgui.SameLine();

                    Imgui.BeginGroup();
                    Imgui.Text("Current");
                    _ = Imgui.ColorButton("##current", s_color, ColorEditOptions.NoPicker | ColorEditOptions.AlphaPreviewHalf, new(60, 40));
                    Imgui.Text("Previous");
                    if (Imgui.ColorButton("##previous", s_backupColor, ColorEditOptions.NoPicker | ColorEditOptions.AlphaPreviewHalf, new(60, 40)))
                    {
                        s_color.Value = s_backupColor;
                    }

                    Imgui.Separator();
                    Imgui.Text("Palette");
                    for (var n = 0; n < s_savedPalette.Length; n++)
                    {
                        Imgui.PushId(n);
                        if ((n % 8) != 0)
                        {
                            Imgui.SameLine(0.0f, Imgui.GetStyle().ItemSpacing.Height);
                        }

                        var palette_button_flags = ColorEditOptions.NoAlpha | ColorEditOptions.NoPicker | ColorEditOptions.NoTooltip;
                        if (Imgui.ColorButton("##palette", s_savedPalette[n], palette_button_flags, new(20, 20)))
                        {
                            s_color.Value = new(s_savedPalette[n].Red, s_savedPalette[n].Green, s_savedPalette[n].Blue, s_color.Value.Alpha);
                        }

                        // Allow user to drop colors into each palette entry. Note that ColorButton() is already a
                        // drag source by default, unless specifying the ColorEditOptions.NoDragDrop flag.
                        if (Imgui.BeginDragDropTarget())
                        {
                            var payload = Imgui.AcceptDragDropPayload(Payload.ColorType);
                            if (payload != null)
                            {
                                var data = payload.Value.GetData<float>();
                                s_savedPalette[n] = new(data[0], data[1], data[2]);
                            }
                            payload = Imgui.AcceptDragDropPayload(Payload.ColorAlphaType);
                            if (payload != null)
                            {
                                var data = payload.Value.GetData<float>();
                                s_savedPalette[n] = new(data[0], data[1], data[2], data[3]);
                            }
                            Imgui.EndDragDropTarget();
                        }

                        Imgui.PopId();
                    }
                    Imgui.EndGroup();
                    Imgui.EndPopup();
                }


                Imgui.Text("Color button only:");
                _ = Imgui.Checkbox("ColorEditOptions.NoBorder", s_noBorder);
                _ = Imgui.ColorButton("MyColor##3c", s_color, misc_flags | (s_noBorder ? ColorEditOptions.NoBorder : 0), new(80, 80));

                Imgui.Text("Color picker:");
                _ = Imgui.Checkbox("With Alpha", s_alpha);
                _ = Imgui.Checkbox("With Alpha Bar", s_alphaBar);
                _ = Imgui.Checkbox("With Side Preview", s_sidePreview);
                if (s_sidePreview)
                {
                    Imgui.SameLine();
                    _ = Imgui.Checkbox("With Ref Color", s_refColor);
                    if (s_refColor)
                    {
                        Imgui.SameLine();
                        _ = Imgui.ColorEditAlpha("##RefColor", s_refColorV, ColorEditOptions.NoInputs | misc_flags);
                    }
                }
                _ = Imgui.Combo("Display Mode", s_displayMode, "Auto/Current\0None\0RGB Only\0HSV Only\0Hex Only\0");
                Imgui.SameLine();
                HelpMarker(
                    "ColorEdit defaults to displaying RGB inputs if you don't specify a display mode, " +
                    "but the user can change it with a right-click on those inputs.\n\nColorPicker defaults to displaying RGB+HSV+Hex " +
                    "if you don't specify a display mode.\n\nYou can change the defaults using SetColorEditOptions().");
                Imgui.SameLine();
                HelpMarker("When not specified explicitly (Auto/Current mode), user can right-click the picker to change mode.");
                var flags = misc_flags;
                if (!s_alpha)
                {
                    flags |= ColorEditOptions.NoAlpha;
                }

                if (s_alphaBar)
                {
                    flags |= ColorEditOptions.AlphaBar;
                }

                if (!s_sidePreview)
                {
                    flags |= ColorEditOptions.NoSidePreview;
                }

                if (s_pickerMode == 1)
                {
                    flags |= ColorEditOptions.PickerHueBar;
                }

                if (s_pickerMode == 2)
                {
                    flags |= ColorEditOptions.PickerHueWheel;
                }

                if (s_displayMode == 1)
                {
                    flags |= ColorEditOptions.NoInputs;
                }

                if (s_displayMode == 2)
                {
                    flags |= ColorEditOptions.DisplayRGB;
                }

                if (s_displayMode == 3)
                {
                    flags |= ColorEditOptions.DisplayHSV;
                }

                if (s_displayMode == 4)
                {
                    flags |= ColorEditOptions.DisplayHex;
                }

                _ = Imgui.ColorPickerAlpha("MyColor##4", s_color, flags, s_refColor ? s_refColorV : null);

                Imgui.Text("Set defaults in code:");
                Imgui.SameLine();
                HelpMarker(
                    "SetColorEditOptions() is designed to allow you to set boot-time default.\n" +
                    "We don't have Push/Pop functions because you can force options on a per-widget basis if needed," +
                    "and the user can change non-forced ones with the options menu.\nWe don't have a getter to avoid" +
                    "encouraging you to persistently save values that aren't forward-compatible.");
                if (Imgui.Button("Default: Uint8 + HSV + Hue Bar"))
                {
                    Imgui.SetColorEditOptions(ColorEditOptions.Uint8 | ColorEditOptions.DisplayHSV | ColorEditOptions.PickerHueBar);
                }

                if (Imgui.Button("Default: Float + HDR + Hue Wheel"))
                {
                    Imgui.SetColorEditOptions(ColorEditOptions.Float | ColorEditOptions.HDR | ColorEditOptions.PickerHueWheel);
                }

                // Always both a small version of both types of pickers (to make it more visible in the demo to people who are skimming quickly through it)
                Imgui.Text("Both types:");
                var w = (Imgui.GetContentRegionAvailable().Width - Imgui.GetStyle().ItemSpacing.Height) * 0.40f;
                Imgui.SetNextItemWidth(w);
                _ = Imgui.ColorPicker("##MyColor##5", s_color, ColorEditOptions.PickerHueBar | ColorEditOptions.NoSidePreview | ColorEditOptions.NoInputs | ColorEditOptions.NoAlpha);
                Imgui.SameLine();
                Imgui.SetNextItemWidth(w);
                _ = Imgui.ColorPicker("##MyColor##6", s_color, ColorEditOptions.PickerHueWheel | ColorEditOptions.NoSidePreview | ColorEditOptions.NoInputs | ColorEditOptions.NoAlpha);

                Imgui.Spacing();
                Imgui.Text("HSV encoded colors");
                Imgui.SameLine();
                HelpMarker(
                    "By default, colors are given to ColorEdit and ColorPicker in RGB, but ColorEditOptions.InputHSV" +
                    "allows you to store colors as HSV and pass them to ColorEdit and ColorPicker as HSV. This comes with the" +
                    "added benefit that you can manipulate hue values with the picker even when saturation or value are zero.");
                Imgui.Text("Color widget with InputHSV:");
                _ = Imgui.ColorEditAlpha("HSV shown as RGB##1", s_colorHsv, ColorEditOptions.DisplayRGB | ColorEditOptions.InputHSV | ColorEditOptions.Float);
                _ = Imgui.ColorEditAlpha("HSV shown as HSV##1", s_colorHsv, ColorEditOptions.DisplayHSV | ColorEditOptions.InputHSV | ColorEditOptions.Float);
                _ = Imgui.Drag("Raw HSV values", s_colorHsv.ToVector(), 0.01f, 0.0f, 1.0f);

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Drag/Slider Flags"))
            {
                _ = Imgui.CheckboxFlags("SliderOptions.AlwaysClamp", s_flags3, SliderOptions.AlwaysClamp);
                Imgui.SameLine(); HelpMarker("Always clamp value to min/max bounds (if any) when input manually with CTRL+Click.");
                _ = Imgui.CheckboxFlags("SliderOptions.Logarithmic", s_flags3, SliderOptions.Logarithmic);
                Imgui.SameLine(); HelpMarker("Enable logarithmic editing (more precision for small values).");
                _ = Imgui.CheckboxFlags("SliderOptions.NoRoundToFormat", s_flags3, SliderOptions.NoRoundToFormat);
                Imgui.SameLine(); HelpMarker("Disable rounding underlying value to match precision of the format string (e.g. %.3f values are rounded to those 3 digits).");
                _ = Imgui.CheckboxFlags("SliderOptions.NoInput", s_flags3, SliderOptions.NoInput);
                Imgui.SameLine(); HelpMarker("Disable CTRL+Click or Enter key allowing to input text directly into the widget.");

                // Drags
                Imgui.Text($"Underlying float value: {s_dragF.Value}");
                _ = Imgui.Drag("Drag (0 -> 1)", s_dragF, 0.005f, 0.0f, 1.0f, "%.3f", s_flags3);
                _ = Imgui.Drag("Drag (0 -> +inf)", s_dragF, 0.005f, 0.0f, float.MaxValue, "%.3f", s_flags3);
                _ = Imgui.Drag("Drag (-inf -> 1)", s_dragF, 0.005f, -float.MaxValue, 1.0f, "%.3f", s_flags3);
                _ = Imgui.Drag("Drag (-inf -> +inf)", s_dragF, 0.005f, -float.MaxValue, +float.MaxValue, "%.3f", s_flags3);
                _ = Imgui.Drag("Drag (0 -> 100)", s_dragI, 0.5f, 0, 100, "%d", s_flags3);

                // Sliders
                Imgui.Text($"Underlying float value: {s_sliderF.Value}");
                _ = Imgui.Slider("Slider (0 -> 1)", s_sliderF, 0.0f, 1.0f, "%.3f", s_flags3);
                _ = Imgui.Slider("Slider (0 -> 100)", s_sliderI, 0, 100, "%d", s_flags3);

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Range Widgets"))
            {
                _ = Imgui.DragRange("range float", s_begin, s_end, 0.25f, 0.0f, 100.0f, "Min: %.1f %%", "Max: %.1f %%", SliderOptions.AlwaysClamp);
                _ = Imgui.DragRange("range int", s_beginI, s_endI, 5, 0, 1000, "Min: %d units", "Max: %d units");
                _ = Imgui.DragRange("range int (no bounds)", s_beginI, s_endI, 5, 0, 0, "Min: %d units", "Max: %d units");
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Data Types"))
            {
                const float DragSpeed = 0.2f;
                Imgui.Text("Drags:");
                _ = Imgui.Checkbox("Clamp integers to 0..50", s_dragClamp);
                Imgui.SameLine();
                HelpMarker(
                    "As with every widget in dear imgui, we never modify values unless there is a user interaction.\n" +
                    "You can override the clamping limits by using CTRL+Click to input a value.");
                _ = Imgui.Drag("drag s8", s_sbyteValue, DragSpeed, s_dragClamp ? 0 : null, s_dragClamp ? 50 : null);
                _ = Imgui.Drag("drag u8", s_byteValue, DragSpeed, s_dragClamp ? 0 : null, s_dragClamp ? 50 : null, "%u ms");
                _ = Imgui.Drag("drag s16", s_shortValue, DragSpeed, s_dragClamp ? 0 : null, s_dragClamp ? 50 : null);
                _ = Imgui.Drag("drag u16", s_ushortValue, DragSpeed, s_dragClamp ? 0 : null, s_dragClamp ? 50 : null, "%u ms");
                _ = Imgui.Drag("drag s32", s_intValue, DragSpeed, s_dragClamp ? 0 : null, s_dragClamp ? 50 : null);
                _ = Imgui.Drag("drag s32 hex", s_intValue, DragSpeed, s_dragClamp ? 0 : null, s_dragClamp ? 50 : null, "0x%08X");
                _ = Imgui.Drag("drag u32", s_uintValue, DragSpeed, s_dragClamp ? 0 : null, s_dragClamp ? 50 : null, "%u ms");
                _ = Imgui.Drag("drag s64", s_longValue, DragSpeed, s_dragClamp ? 0 : null, s_dragClamp ? 50 : null);
                _ = Imgui.Drag("drag u64", s_ulongValue, DragSpeed, s_dragClamp ? 0 : null, s_dragClamp ? 50 : null);
                _ = Imgui.Drag("drag float", s_singleValue, 0.005f, 0, 1, "%f");
                _ = Imgui.Drag("drag float log", s_singleValue, 0.005f, 0, 1, "%f", SliderOptions.Logarithmic);
                _ = Imgui.Drag("drag double", s_doubleValue, 0.0005f, 0, null, "%.10f grams");
                _ = Imgui.Drag("drag double log", s_doubleValue, 0.0005f, 0, 1, "0 < %.10f < 1", SliderOptions.Logarithmic);

                Imgui.Text("Sliders");
                _ = Imgui.Slider("slider s8 full", s_sbyteValue, sbyte.MinValue, sbyte.MaxValue, "%d");
                _ = Imgui.Slider("slider u8 full", s_byteValue, byte.MinValue, byte.MaxValue, "%u");
                _ = Imgui.Slider("slider s16 full", s_shortValue, short.MinValue, short.MaxValue, "%d");
                _ = Imgui.Slider("slider u16 full", s_ushortValue, ushort.MinValue, ushort.MaxValue, "%u");
                _ = Imgui.Slider("slider s32 low", s_intValue, 0, 50, "%d");
                _ = Imgui.Slider("slider s32 high", s_intValue, (int.MaxValue / 2) - 100, int.MaxValue / 2, "%d");
                _ = Imgui.Slider("slider s32 full", s_intValue, int.MinValue / 2, int.MaxValue / 2, "%d");
                _ = Imgui.Slider("slider s32 hex", s_intValue, 0, 50, "0x%04X");
                _ = Imgui.Slider("slider u32 low", s_uintValue, 0, 50, "%u");
                _ = Imgui.Slider("slider u32 high", s_uintValue, (uint.MaxValue / 2) - 100, uint.MaxValue / 2, "%u");
                _ = Imgui.Slider("slider u32 full", s_uintValue, uint.MinValue, uint.MaxValue / 2, "%u");
                _ = Imgui.Slider("slider s64 low", s_longValue, 0, 50, "%I64d");
                _ = Imgui.Slider("slider s64 high", s_longValue, (long.MaxValue / 2) - 100, long.MaxValue / 2, "%I64d");
                _ = Imgui.Slider("slider s64 full", s_longValue, long.MinValue / 2, long.MaxValue / 2, "%I64d");
                _ = Imgui.Slider("slider u64 low", s_ulongValue, 0, 50, "%I64u ms");
                _ = Imgui.Slider("slider u64 high", s_ulongValue, (ulong.MaxValue / 2) - 100, ulong.MaxValue / 2, "%I64u ms");
                _ = Imgui.Slider("slider u64 full", s_ulongValue, ulong.MinValue / 2, ulong.MaxValue / 2, "%I64u ms");
                _ = Imgui.Slider("slider float low", s_singleValue, 0, 1);
                _ = Imgui.Slider("slider float low log", s_singleValue, 0, 1, "%.10f", SliderOptions.Logarithmic);
                _ = Imgui.Slider("slider float high", s_singleValue, -10000000000.0f, +10000000000.0f, "%e");
                _ = Imgui.Slider("slider double low", s_doubleValue, 0, 1, "%.10f grams");
                _ = Imgui.Slider("slider double low log", s_doubleValue, 0, 1, "%.10f", SliderOptions.Logarithmic);
                _ = Imgui.Slider("slider double high", s_doubleValue, -1000000000000000.0, +1000000000000000.0, "%e grams");

                Imgui.Text("Sliders (reverse)");
                _ = Imgui.Slider("slider s8 reverse", s_sbyteValue, sbyte.MaxValue, sbyte.MinValue, "%d");
                _ = Imgui.Slider("slider u8 reverse", s_byteValue, byte.MaxValue, byte.MinValue, "%u");
                _ = Imgui.Slider("slider s32 reverse", s_intValue, 50, 0, "%d");
                _ = Imgui.Slider("slider u32 reverse", s_uintValue, 50, 0, "%u");
                _ = Imgui.Slider("slider s64 reverse", s_longValue, 50, 0, "%I64d");
                _ = Imgui.Slider("slider u64 reverse", s_ulongValue, 50, 0, "%I64u ms");

                Imgui.Text("Inputs");
                _ = Imgui.Checkbox("Show step buttons", s_inputsStep);
                _ = Imgui.Input("input s8", s_sbyteValue, s_inputsStep ? (sbyte)1 : (sbyte)0, 0, "%d");
                _ = Imgui.Input("input u8", s_byteValue, s_inputsStep ? (byte)1 : (byte)0, 0, "%u");
                _ = Imgui.Input("input s16", s_shortValue, s_inputsStep ? (short)1 : (short)0, 0, "%d");
                _ = Imgui.Input("input u16", s_ushortValue, s_inputsStep ? (ushort)1 : (ushort)0, 0, "%u");
                _ = Imgui.Input("input s32", s_intValue, s_inputsStep ? 1 : 0, 0, "%d");
                _ = Imgui.Input("input s32 hex", s_intValue, s_inputsStep ? 1 : 0, 0, "%04X");
                _ = Imgui.Input("input u32", s_uintValue, s_inputsStep ? 1 : (uint)0, 0, "%u");
                _ = Imgui.Input("input u32 hex", s_uintValue, s_inputsStep ? 1 : (uint)0, 0, "%08X");
                _ = Imgui.Input("input s64", s_longValue, s_inputsStep ? 1 : 0);
                _ = Imgui.Input("input u64", s_ulongValue, s_inputsStep ? 1 : (ulong)0);
                _ = Imgui.Input("input float", s_singleValue, s_inputsStep ? 1 : 0);
                _ = Imgui.Input("input double", s_doubleValue, s_inputsStep ? 1 : 0);

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Multi-component Widgets"))
            {
                _ = Imgui.Input("input float2", s_vec2F);
                _ = Imgui.Drag("drag float2", s_vec2F, 0.01f, 0.0f, 1.0f);
                _ = Imgui.Slider("slider float2", s_vec2F, 0.0f, 1.0f);
                _ = Imgui.Input("input int2", s_vec2I);
                _ = Imgui.Drag("drag int2", s_vec2I, 1, 0, 255);
                _ = Imgui.Slider("slider int2", s_vec2I, 0, 255);
                Imgui.Spacing();

                _ = Imgui.Input("input float3", s_vec3F);
                _ = Imgui.Drag("drag float3", s_vec3F, 0.01f, 0.0f, 1.0f);
                _ = Imgui.Slider("slider float3", s_vec3F, 0.0f, 1.0f);
                _ = Imgui.Input("input int3", s_vec3I);
                _ = Imgui.Drag("drag int3", s_vec3I, 1, 0, 255);
                _ = Imgui.Slider("slider int3", s_vec3I, 0, 255);
                Imgui.Spacing();

                _ = Imgui.Input("input float4", s_vec4F);
                _ = Imgui.Drag("drag float4", s_vec4F, 0.01f, 0.0f, 1.0f);
                _ = Imgui.Slider("slider float4", s_vec4F, 0.0f, 1.0f);
                _ = Imgui.Input("input int4", s_vec4I);
                _ = Imgui.Drag("drag int4", s_vec4I, 1, 0, 255);
                _ = Imgui.Slider("slider int4", s_vec4I, 0, 255);

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Vertical Sliders"))
            {
                const float Spacing = 4;
                Imgui.PushStyleVariable(StyleVariable.ItemSpacing, new SizeF(Spacing, Spacing));

                _ = Imgui.VerticalSlider("##int", new(18, 160), s_intValue2, 0, 5);
                Imgui.SameLine();

                Imgui.PushId("set1");
                for (var i = 0; i < 7; i++)
                {
                    if (i > 0)
                    {
                        Imgui.SameLine();
                    }

                    Imgui.PushId(i);
                    Imgui.PushStyleColor(StyleColor.FrameBackground, ColorF.FromHsv(i / 7.0f, 0.5f, 0.5f));
                    Imgui.PushStyleColor(StyleColor.FrameBackgroundHovered, ColorF.FromHsv(i / 7.0f, 0.6f, 0.5f));
                    Imgui.PushStyleColor(StyleColor.FrameBackgroundActive, ColorF.FromHsv(i / 7.0f, 0.7f, 0.5f));
                    Imgui.PushStyleColor(StyleColor.SliderGrab, ColorF.FromHsv(i / 7.0f, 0.9f, 0.9f));
                    _ = Imgui.VerticalSlider("##v", new SizeF(18, 160), s_values2.GetStateOfElement(i), 0.0f, 1.0f, "");
                    if (Imgui.IsItemActive() || Imgui.IsItemHovered())
                    {
                        Imgui.SetTooltip($"{s_values2[i]:F3}");
                    }

                    Imgui.PopStyleColor(4);
                    Imgui.PopId();
                }
                Imgui.PopId();

                Imgui.SameLine();
                Imgui.PushId("set2");
                const int Rows = 3;
                SizeF small_slider_size = new(18, (int)((160.0f - ((Rows - 1) * Spacing)) / Rows));
                for (var nx = 0; nx < 4; nx++)
                {
                    if (nx > 0)
                    {
                        Imgui.SameLine();
                    }

                    Imgui.BeginGroup();
                    for (var ny = 0; ny < Rows; ny++)
                    {
                        Imgui.PushId((nx * Rows) + ny);
                        _ = Imgui.VerticalSlider("##v", small_slider_size, s_values3.GetStateOfElement(nx), 0.0f, 1.0f, "");
                        if (Imgui.IsItemActive() || Imgui.IsItemHovered())
                        {
                            Imgui.SetTooltip($"{s_values3[nx]:F3}");
                        }

                        Imgui.PopId();
                    }
                    Imgui.EndGroup();
                }
                Imgui.PopId();

                Imgui.SameLine();
                Imgui.PushId("set3");
                for (var i = 0; i < 4; i++)
                {
                    if (i > 0)
                    {
                        Imgui.SameLine();
                    }

                    Imgui.PushId(i);
                    Imgui.PushStyleVariable(StyleVariable.GrabMinSize, 40);
                    _ = Imgui.VerticalSlider("##v", new SizeF(40, 160), s_values2.GetStateOfElement(i), 0.0f, 1.0f, "%.2f\nsec");
                    Imgui.PopStyleVariable();
                    Imgui.PopId();
                }
                Imgui.PopId();
                Imgui.PopStyleVariable();
                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Drag and Drop"))
            {
                if (Imgui.TreeNode("Drag and drop in standard widgets"))
                {
                    HelpMarker("You can drag from the color squares.");
                    _ = Imgui.ColorEdit("color 1", s_col3);
                    _ = Imgui.ColorEditAlpha("color 2", s_col4);
                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Drag and drop to copy/swap items"))
                {
                    if (Imgui.RadioButton("Copy", s_mode == Mode.Copy))
                    {
                        s_mode = Mode.Copy;
                    }
                    Imgui.SameLine();
                    if (Imgui.RadioButton("Move", s_mode == Mode.Move))
                    {
                        s_mode = Mode.Move;
                    }
                    Imgui.SameLine();
                    if (Imgui.RadioButton("Swap", s_mode == Mode.Swap))
                    {
                        s_mode = Mode.Swap;
                    }
                    for (var n = 0; n < s_names.Length; n++)
                    {
                        Imgui.PushId(n);
                        if ((n % 3) != 0)
                        {
                            Imgui.SameLine();
                        }

                        _ = Imgui.Button(s_names[n], new(60, 60));

                        if (Imgui.BeginDragDropSource(DragDropOptions.None))
                        {
                            _ = Imgui.SetDragDropPayload("DND_DEMO_CELL", new Span<int>(ref n));

                            if (s_mode == Mode.Copy)
                            {
                                Imgui.Text($"Copy {s_names[n]}");
                            }
                            if (s_mode == Mode.Move)
                            {
                                Imgui.Text($"Move {s_names[n]}");
                            }
                            if (s_mode == Mode.Swap)
                            {
                                Imgui.Text($"Swap {s_names[n]}");
                            }
                            Imgui.EndDragDropSource();
                        }
                        if (Imgui.BeginDragDropTarget())
                        {
                            var payload = Imgui.AcceptDragDropPayload("DND_DEMO_CELL");
                            if (payload != null)
                            {
                                var payload_n = payload.Value.GetData<int>()[0];
                                if (s_mode == Mode.Copy)
                                {
                                    s_names[n] = s_names[payload_n];
                                }
                                if (s_mode == Mode.Move)
                                {
                                    s_names[n] = s_names[payload_n];
                                    s_names[payload_n] = "";
                                }
                                if (s_mode == Mode.Swap)
                                {
                                    (s_names[payload_n], s_names[n]) = (s_names[n], s_names[payload_n]);
                                }
                            }
                            Imgui.EndDragDropTarget();
                        }
                        Imgui.PopId();
                    }
                    Imgui.TreePop();
                }

                if (Imgui.TreeNode("Drag to reorder items (simple)"))
                {
                    HelpMarker(
                        "We don't use the drag and drop api at all here! " +
                        "Instead we query when the item is held but not hovered, and order items accordingly.");
                    for (var n = 0; n < s_itemNames.Length; n++)
                    {
                        var item = s_itemNames[n];
                        _ = Imgui.Selectable(item);

                        if (Imgui.IsItemActive() && !Imgui.IsItemHovered())
                        {
                            var n_next = n + (Imgui.GetMouseDragDelta(0).Height < 0.0f ? -1 : 1);
                            if (n_next >= 0 && n_next < s_itemNames.Length)
                            {
                                s_itemNames[n] = s_itemNames[n_next];
                                s_itemNames[n_next] = item;
                                Imgui.ResetMouseDragDelta();
                            }
                        }
                    }
                    Imgui.TreePop();
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Querying Item Status (Edited/Active/Hovered etc.)"))
            {
                var item_names = new[]
                {
                    "Text", "Button", "Button (w/ repeat)", "Checkbox", "SliderFloat", "InputText", "InputTextMultiline", "InputFloat",
                    "InputFloat3", "ColorEdit4", "Selectable", "MenuItem", "TreeNode", "TreeNode (w/ double-click)", "Combo", "ListBox"
                };
                _ = Imgui.Combo("Item Type", s_itemType, item_names, item_names.Length);
                Imgui.SameLine();
                HelpMarker("Testing how various types of items are interacting with the IsItemXXX functions. Note that the bool return value of most ImGui function is generally equivalent to calling Imgui.IsItemHovered().");
                _ = Imgui.Checkbox("Item Disabled", s_itemDisabled);

                // Submit selected items so we can query their status in the code following it.
                var ret = false;
                if (s_itemDisabled)
                {
                    Imgui.BeginDisabled(true);
                }

                switch (s_itemType)
                {
                    case 0:
                        Imgui.Text("ITEM: Text");
                        break;
                    case 1:
                        ret = Imgui.Button("ITEM: Button");
                        break;
                    case 2:
                        Imgui.PushButtonRepeat(true);
                        ret = Imgui.Button("ITEM: Button");
                        Imgui.PopButtonRepeat();
                        break;
                    case 3:
                        ret = Imgui.Checkbox("ITEM: Checkbox", s_b);
                        break;
                    case 4:
                        ret = Imgui.Slider("ITEM: Slider", s_col4f.GetStateOfElement(0), 0.0f, 1.0f);
                        break;
                    case 5:
                        ret = Imgui.InputText("ITEM: InputText", s_str);
                        break;
                    case 6:
                        ret = Imgui.InputTextMultiline("ITEM: InputTextMultiline", s_str);
                        break;
                    case 7:
                        ret = Imgui.Input("ITEM: InputFloat", s_col4f.GetStateOfElement(0), 1.0f);
                        break;
                    case 8:
                        ret = Imgui.Input("ITEM: InputFloat4", s_col4f);
                        break;
                    case 9:
                        ret = Imgui.ColorEditAlpha("ITEM: ColorEdit4", s_col4f2);
                        break;
                    case 10:
                        ret = Imgui.Selectable("ITEM: Selectable");
                        break;
                    case 11:
                        ret = Imgui.MenuItem("ITEM: MenuItem");
                        break;
                    case 12:
                        ret = Imgui.TreeNode("ITEM: TreeNode");
                        if (ret)
                        {
                            Imgui.TreePop();
                        }
                        break;
                    case 13:
                        ret = Imgui.TreeNode("ITEM: TreeNode w/ TreeNodeOptions.OpenOnDoubleClick", TreeNodeOptions.OpenOnDoubleClick | TreeNodeOptions.NoTreePushOnOpen);
                        break;
                    case 14:
                        {
                            var items = new[] { "Apple", "Banana", "Cherry", "Kiwi" };
                            ret = Imgui.Combo("ITEM: Combo", s_current, items);
                        }
                        break;
                    case 15:
                        {
                            var items = new[] { "Apple", "Banana", "Cherry", "Kiwi" };
                            ret = Imgui.ListBox("ITEM: ListBox", s_current2, items, items.Length);
                        }
                        break;
                }

                var hovered_delay_none = Imgui.IsItemHovered();
                var hovered_delay_short = Imgui.IsItemHovered(HoveredOptions.DelayShort);
                var hovered_delay_normal = Imgui.IsItemHovered(HoveredOptions.DelayNormal);

                Imgui.BulletText(
                    $"Return value = {ret}\n" +
                    $"IsItemFocused() = {Imgui.IsItemFocused()}\n" +
                    $"IsItemHovered() = {Imgui.IsItemHovered()}\n" +
                    $"IsItemHovered(AllowWhenBlockedByPopup) = {Imgui.IsItemHovered(HoveredOptions.AllowWhenBlockedByPopup)}\n" +
                    $"IsItemHovered(AllowWhenBlockedByActiveItem) = {Imgui.IsItemHovered(HoveredOptions.AllowWhenBlockedByActiveItem)}\n" +
                    $"IsItemHovered(AllowWhenOverlapped) = {Imgui.IsItemHovered(HoveredOptions.AllowWhenOverlapped)}\n" +
                    $"IsItemHovered(AllowWhenDisabled) = {Imgui.IsItemHovered(HoveredOptions.AllowWhenDisabled)}\n" +
                    $"IsItemHovered(RectOnly) = {Imgui.IsItemHovered(HoveredOptions.RectOnly)}\n" +
                    $"IsItemActive() = {Imgui.IsItemActive()}\n" +
                    $"IsItemEdited() = {Imgui.IsItemEdited()}\n" +
                    $"IsItemActivated() = {Imgui.IsItemActivated()}\n" +
                    $"IsItemDeactivated() = {Imgui.IsItemDeactivated()}\n" +
                    $"IsItemDeactivatedAfterEdit() = {Imgui.IsItemDeactivatedAfterEdit()}\n" +
                    $"IsItemVisible() = {Imgui.IsItemVisible()}\n" +
                    $"IsItemClicked() = {Imgui.IsItemClicked()}\n" +
                    $"IsItemToggledOpen() = {Imgui.IsItemToggledOpen()}\n" +
                    $"GetItemRectangle().Min = ({Imgui.GetItemRectangle().Min.X:F1}, {Imgui.GetItemRectangle().Min.Y:F1})\n" +
                    $"GetItemRectangle().Max = ({Imgui.GetItemRectangle().Max.X:F1}, {Imgui.GetItemRectangle().Max.Y:F1})\n" +
                    $"GetItemRectSize() = ({Imgui.GetItemRectangleSize().Width:F1}, {Imgui.GetItemRectangleSize().Height:F1})"
                );
                Imgui.BulletText($"w/ Hovering Delay: None = {hovered_delay_none}, Fast {hovered_delay_short}, Normal = {hovered_delay_normal}");

                if (s_itemDisabled)
                {
                    Imgui.EndDisabled();
                }

                _ = Imgui.InputText("unused", s_empty, InputTextOptions.ReadOnly);
                Imgui.SameLine();
                HelpMarker("This widget is only here to be able to tab-out of the widgets above and see e.g. Deactivated() status.");

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Querying Window Status (Focused/Hovered etc.)"))
            {
                _ = Imgui.Checkbox("Embed everything inside a child window for testing RootWindow flag.", s_embedAllInsideAChildWindow);
                if (s_embedAllInsideAChildWindow)
                {
                    _ = Imgui.BeginChild("outer_child", new(0, Imgui.GetFontSize() * 20.0f), true);
                }

                Imgui.BulletText(
                    $"IsWindowFocused() = {Imgui.IsWindowFocused()}\n" +
                    $"IsWindowFocused(ChildWindows) = {Imgui.IsWindowFocused(FocusedOptions.ChildWindows)}\n" +
                    $"IsWindowFocused(ChildWindows|NoPopupHierarchy) = {Imgui.IsWindowFocused(FocusedOptions.ChildWindows | FocusedOptions.NoPopupHierarchy)}\n" +
                    $"IsWindowFocused(ChildWindows|RootWindow) = {Imgui.IsWindowFocused(FocusedOptions.ChildWindows | FocusedOptions.RootWindow)}\n" +
                    $"IsWindowFocused(ChildWindows|RootWindow|NoPopupHierarchy) = {Imgui.IsWindowFocused(FocusedOptions.ChildWindows | FocusedOptions.RootWindow | FocusedOptions.NoPopupHierarchy)}\n" +
                    $"IsWindowFocused(RootWindow) = {Imgui.IsWindowFocused(FocusedOptions.RootWindow)}\n" +
                    $"IsWindowFocused(RootWindow|NoPopupHierarchy) = {Imgui.IsWindowFocused(FocusedOptions.RootWindow | FocusedOptions.NoPopupHierarchy)}\n" +
                    $"IsWindowFocused(AnyWindow) = {Imgui.IsWindowFocused(FocusedOptions.AnyWindow)}\n"
                    );

                Imgui.BulletText(
                    $"IsWindowHovered() = {Imgui.IsWindowHovered()}\n" +
                    $"IsWindowHovered(AllowWhenBlockedByPopup) = {Imgui.IsWindowHovered(HoveredOptions.AllowWhenBlockedByPopup)}\n" +
                    $"IsWindowHovered(AllowWhenBlockedByActiveItem) = {Imgui.IsWindowHovered(HoveredOptions.AllowWhenBlockedByActiveItem)}\n" +
                    $"IsWindowHovered(ChildWindows) = {Imgui.IsWindowHovered(HoveredOptions.ChildWindows)}\n" +
                    $"IsWindowHovered(ChildWindows|NoPopupHierarchy) = {Imgui.IsWindowHovered(HoveredOptions.ChildWindows | HoveredOptions.NoPopupHierarchy)}\n" +
                    $"IsWindowHovered(ChildWindows|RootWindow) = {Imgui.IsWindowHovered(HoveredOptions.ChildWindows | HoveredOptions.RootWindow)}\n" +
                    $"IsWindowHovered(ChildWindows|RootWindow|NoPopupHierarchy) = {Imgui.IsWindowHovered(HoveredOptions.ChildWindows | HoveredOptions.RootWindow | HoveredOptions.NoPopupHierarchy)}\n" +
                    $"IsWindowHovered(RootWindow) = {Imgui.IsWindowHovered(HoveredOptions.RootWindow)}\n" +
                    $"IsWindowHovered(RootWindow|NoPopupHierarchy) = {Imgui.IsWindowHovered(HoveredOptions.RootWindow | HoveredOptions.NoPopupHierarchy)}\n" +
                    $"IsWindowHovered(ChildWindows|AllowWhenBlockedByPopup) = {Imgui.IsWindowHovered(HoveredOptions.ChildWindows | HoveredOptions.AllowWhenBlockedByPopup)}\n" +
                    $"IsWindowHovered(AnyWindow) = {Imgui.IsWindowHovered(HoveredOptions.AnyWindow)}\n"
                    );

                _ = Imgui.BeginChild("child", new(0, 50), true);
                Imgui.Text("This is another child window for testing the ChildWindows flag.");
                Imgui.EndChild();
                if (s_embedAllInsideAChildWindow)
                {
                    Imgui.EndChild();
                }

                _ = Imgui.Checkbox("Hovered/Active tests after Begin() for title bar testing", s_testWindow);
                if (s_testWindow)
                {
                    _ = Imgui.Begin("Title bar Hovered/Active tests", s_testWindow);
                    if (Imgui.BeginPopupContextItem())
                    {
                        if (Imgui.MenuItem("Close"))
                        {
                            s_testWindow.Value = false;
                        }
                        Imgui.EndPopup();
                    }
                    Imgui.Text(
                        $"IsItemHovered() after begin = {Imgui.IsItemHovered()} (== is title bar hovered)\n" +
                        $"IsItemActive() after begin = {Imgui.IsItemActive()} (== is window being clicked/moved)\n"
                        );
                    Imgui.End();
                }

                Imgui.TreePop();
            }

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

            if (Imgui.TreeNode("Text Filter"))
            {
                HelpMarker("Not a widget per-se, but ImGuiTextFilter is a helper to perform simple filtering on text strings.");
                Imgui.Text("Filter usage:\n" +
                    "  \"\"         display all lines\n" +
                    "  \"xxx\"      display lines containing \"xxx\"\n" +
                    "  \"xxx,yyy\"  display lines containing \"xxx\" or \"yyy\"\n" +
                    "  \"-xxx\"     hide lines containing \"xxx\"");
                _ = s_filter.Draw();
                var lines = new[] { "aaa1.c", "bbb1.c", "ccc1.c", "aaa2.cpp", "bbb2.cpp", "ccc2.cpp", "abc.h", "hello, world" };
                for (var i = 0; i < lines.Length; i++)
                {
                    if (s_filter.PassFilter(lines[i]))
                    {
                        Imgui.BulletText(lines[i]);
                    }
                }

                Imgui.TreePop();
            }
        }

        private static readonly State<bool> s_disableMouseWheel = new(false);
        private static readonly State<bool> s_disableMenu = new(false);
        private static readonly State<int> s_offsetX = new(0);
        private static readonly State<float> s_f6 = new(0.0f);
        private static readonly State<bool> s_showIndentedItems = new(true);
        private static readonly State<bool> s_c1 = new(false), s_c2 = new(false), s_c3 = new(false), s_c4 = new(false);
        private static readonly State<float> s_f7 = new(1.0f), s_f8 = new(2.0f), s_f9 = new(3.0f);
        private static readonly State<int> s_item = new(-1);
        private static readonly StateVector<int> s_selection = new(4, new[] { 0, 1, 2, 3 });
        private static readonly State<int> s_trackItem = new(50);
        private static readonly State<bool> s_enableTrack = new(true);
        private static readonly State<bool> s_enableExtraDecorations = new(false);
        private static readonly State<float> s_scrollToOffPx = new(0.0f);
        private static readonly State<float> s_scrollToPosPx = new(200.0f);
        private static readonly State<int> s_lines = new(7);
        private static readonly State<bool> s_showHorizontalContentsSizeDemoWindow = new(false);
        private static readonly State<bool> s_showHScrollbar = new(true);
        private static readonly State<bool> s_showButton = new(true);
        private static readonly State<bool> s_showTreeNodes = new(true);
        private static readonly State<bool> s_showTextWrapped = new(false);
        private static readonly State<bool> s_showColumns = new(true);
        private static readonly State<bool> s_showTabBar = new(true);
        private static readonly State<bool> s_showChild = new(false);
        private static readonly State<bool> s_explicitContentSize = new(false);
        private static readonly State<float> s_contentsSizeX = new(300.0f);
        private static readonly State<bool> s_open = new(true);
        private static readonly StateVector<float> s_size = new(2, new[] { 100.0f, 100.0f });
        private static readonly State<float> s_offsetX2 = new(30.0f);
        private static readonly State<float> s_offsetY2 = new(30.0f);

        private static void ShowDemoWindowLayout()
        {
            if (!Imgui.CollapsingHeader("Layout & Scrolling"))
            {
                return;
            }

            if (Imgui.TreeNode("Child windows"))
            {
                HelpMarker("Use child windows to begin into a self-contained independent scrolling/clipping regions within a host window.");
                _ = Imgui.Checkbox("Disable Mouse Wheel", s_disableMouseWheel);
                _ = Imgui.Checkbox("Disable Menu", s_disableMenu);

                {
                    var window_flags = WindowOptions.HorizontalScrollbar;
                    if (s_disableMouseWheel)
                    {
                        window_flags |= WindowOptions.NoScrollWithMouse;
                    }

                    _ = Imgui.BeginChild("ChildL", new(Imgui.GetContentRegionAvailable().Width * 0.5f, 260), false, window_flags);
                    for (var i = 0; i < 100; i++)
                    {
                        Imgui.Text($"{i:D4}: scrollable region");
                    }

                    Imgui.EndChild();
                }

                Imgui.SameLine();

                {
                    var window_flags = WindowOptions.None;
                    if (s_disableMouseWheel)
                    {
                        window_flags |= WindowOptions.NoScrollWithMouse;
                    }

                    if (!s_disableMenu)
                    {
                        window_flags |= WindowOptions.MenuBar;
                    }

                    Imgui.PushStyleVariable(StyleVariable.ChildRounding, 5.0f);
                    _ = Imgui.BeginChild("ChildR", new(0, 260), true, window_flags);
                    if (!s_disableMenu && Imgui.BeginMenuBar())
                    {
                        if (Imgui.BeginMenu("Menu"))
                        {
                            ShowExampleMenuFile();
                            Imgui.EndMenu();
                        }
                        Imgui.EndMenuBar();
                    }
                    if (Imgui.BeginTable("split", 2, TableOptions.Resizable | TableOptions.NoSavedSettings))
                    {
                        for (var i = 0; i < 100; i++)
                        {
                            _ = Imgui.TableNextColumn();
                            _ = Imgui.Button($"{i:D3}", new(-SizeF.MinNormalizedValue, 0.0f));
                        }
                        Imgui.EndTable();
                    }
                    Imgui.EndChild();
                    Imgui.PopStyleVariable();
                }

                Imgui.Separator();

                {
                    Imgui.SetNextItemWidth(Imgui.GetFontSize() * 8);
                    _ = Imgui.Drag("Offset X", s_offsetX, 1.0f, -1000, 1000);

                    Imgui.SetCursorPosX(Imgui.GetCursorPosX() + (float)s_offsetX);
                    Imgui.PushStyleColor(StyleColor.ChildBackground, new Color(255, 0, 0, 100));
                    _ = Imgui.BeginChild("Red", new(200, 100), true, WindowOptions.None);
                    for (var n = 0; n < 50; n++)
                    {
                        Imgui.Text($"Some test {n}");
                    }

                    Imgui.EndChild();
                    var child_is_hovered = Imgui.IsItemHovered();
                    var child_rect_min = Imgui.GetItemRectangle().Min;
                    var child_rect_max = Imgui.GetItemRectangle().Max;
                    Imgui.PopStyleColor();
                    Imgui.Text($"Hovered: {child_is_hovered}");
                    Imgui.Text($"Rect of child window is: ({child_rect_min.X:F0},{child_rect_min.Y:F0}) ({child_rect_max.X:F0},{child_rect_max.Y:F0})");
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Widgets Width"))
            {
                _ = Imgui.Checkbox("Show indented items", s_showIndentedItems);

                Imgui.Text("SetNextItemWidth/PushItemWidth(100)");
                Imgui.SameLine();
                HelpMarker("Fixed width.");
                Imgui.PushItemWidth(100);
                _ = Imgui.Drag("float##1b", s_f6);
                if (s_showIndentedItems)
                {
                    Imgui.Indent();
                    _ = Imgui.Drag("float (indented)##1b", s_f6);
                    Imgui.Unindent();
                }
                Imgui.PopItemWidth();

                Imgui.Text("SetNextItemWidth/PushItemWidth(-100)");
                Imgui.SameLine();
                HelpMarker("Align to right edge minus 100");
                Imgui.PushItemWidth(-100);
                _ = Imgui.Drag("float##2a", s_f6);
                if (s_showIndentedItems)
                {
                    Imgui.Indent();
                    _ = Imgui.Drag("float (indented)##2b", s_f6);
                    Imgui.Unindent();
                }
                Imgui.PopItemWidth();

                Imgui.Text("SetNextItemWidth/PushItemWidth(GetContentRegionAvailable().Width * 0.5f)");
                Imgui.SameLine()
                    ; HelpMarker("Half of available width.\n(~ right-cursor_pos)\n(works within a column set)");
                Imgui.PushItemWidth(Imgui.GetContentRegionAvailable().Width * 0.5f);
                _ = Imgui.Drag("float##3a", s_f6);
                if (s_showIndentedItems)
                {
                    Imgui.Indent();
                    _ = Imgui.Drag("float (indented)##3b", s_f6);
                    Imgui.Unindent();
                }
                Imgui.PopItemWidth();

                Imgui.Text("SetNextItemWidth/PushItemWidth(-GetContentRegionAvailable().Width * 0.5f)");
                Imgui.SameLine();
                HelpMarker("Align to right edge minus half");
                Imgui.PushItemWidth(-Imgui.GetContentRegionAvailable().Width * 0.5f);
                _ = Imgui.Drag("float##4a", s_f6);
                if (s_showIndentedItems)
                {
                    Imgui.Indent();
                    _ = Imgui.Drag("float (indented)##4b", s_f6);
                    Imgui.Unindent();
                }
                Imgui.PopItemWidth();

                Imgui.Text("SetNextItemWidth/PushItemWidth(-FLT_MIN)");
                Imgui.SameLine();
                HelpMarker("Align to right edge");
                Imgui.PushItemWidth(-SizeF.MinNormalizedValue);
                _ = Imgui.Drag("##float5a", s_f6);
                if (s_showIndentedItems)
                {
                    Imgui.Indent();
                    _ = Imgui.Drag("float (indented)##5b", s_f6);
                    Imgui.Unindent();
                }
                Imgui.PopItemWidth();

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Basic Horizontal Layout"))
            {
                Imgui.TextWrapped("(Use Imgui.SameLine() to keep adding items to the right of the preceding item)");

                Imgui.Text("Two items: Hello");
                Imgui.SameLine();
                Imgui.TextColored(new(1, 1, 0, 1), "Sailor");

                Imgui.Text("More spacing: Hello");
                Imgui.SameLine(0, 20);
                Imgui.TextColored(new(1, 1, 0, 1), "Sailor");

                Imgui.AlignTextToFramePadding();
                Imgui.Text("Normal buttons");
                Imgui.SameLine();
                _ = Imgui.Button("Banana");
                Imgui.SameLine();
                _ = Imgui.Button("Apple");
                Imgui.SameLine();
                _ = Imgui.Button("Corniflower");

                Imgui.Text("Small buttons");
                Imgui.SameLine();
                _ = Imgui.SmallButton("Like this one");
                Imgui.SameLine();
                Imgui.Text("can fit within a text block.");

                Imgui.Text("Aligned");
                Imgui.SameLine(150);
                Imgui.Text("x=150");
                Imgui.SameLine(300);
                Imgui.Text("x=300");
                Imgui.Text("Aligned");
                Imgui.SameLine(150);
                _ = Imgui.SmallButton("x=150");
                Imgui.SameLine(300);
                _ = Imgui.SmallButton("x=300");

                _ = Imgui.Checkbox("My", s_c1);
                Imgui.SameLine();
                _ = Imgui.Checkbox("Tailor", s_c2);
                Imgui.SameLine();
                _ = Imgui.Checkbox("Is", s_c3);
                Imgui.SameLine();
                _ = Imgui.Checkbox("Rich", s_c4);

                Imgui.PushItemWidth(80);
                var items = new[] { "AAAA", "BBBB", "CCCC", "DDDD" };
                _ = Imgui.Combo("Combo", s_item, items);
                Imgui.SameLine();
                _ = Imgui.Slider("X", s_f7, 0.0f, 5.0f);
                Imgui.SameLine();
                _ = Imgui.Slider("Y", s_f8, 0.0f, 5.0f);
                Imgui.SameLine();
                _ = Imgui.Slider("Z", s_f9, 0.0f, 5.0f);
                Imgui.PopItemWidth();

                Imgui.PushItemWidth(80);
                Imgui.Text("Lists:");
                for (var i = 0; i < 4; i++)
                {
                    if (i > 0)
                    {
                        Imgui.SameLine();
                    }

                    Imgui.PushId(i);
                    _ = Imgui.ListBox("", s_selection.GetStateOfElement(i), items, items.Length);
                    Imgui.PopId();
                }
                Imgui.PopItemWidth();

                SizeF buttonSz = new(40, 40);
                _ = Imgui.Button("A", buttonSz);
                Imgui.SameLine();
                Imgui.Dummy(buttonSz);
                Imgui.SameLine();
                _ = Imgui.Button("B", buttonSz);

                Imgui.Text("Manual wrapping:");
                var style = Imgui.GetStyle();
                var buttons_count = 20;
                var window_visible_x2 = Imgui.GetWindowPosition().X + Imgui.GetWindowContentRegionMax().X;
                for (var n = 0; n < buttons_count; n++)
                {
                    Imgui.PushId(n);
                    _ = Imgui.Button("Box", buttonSz);
                    var last_button_x2 = Imgui.GetItemRectangle().Max.X;
                    var next_button_x2 = last_button_x2 + style.ItemSpacing.Width + buttonSz.Width;
                    if (n + 1 < buttons_count && next_button_x2 < window_visible_x2)
                    {
                        Imgui.SameLine();
                    }

                    Imgui.PopId();
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Groups"))
            {
                HelpMarker(
                    "BeginGroup() basically locks the horizontal position for new line. " +
                    "EndGroup() bundles the whole group so that you can use \"item\" functions such as " +
                    "IsItemHovered()/IsItemActive() or SameLine() etc. on the whole group.");
                Imgui.BeginGroup();
                {
                    Imgui.BeginGroup();
                    _ = Imgui.Button("AAA");
                    Imgui.SameLine();
                    _ = Imgui.Button("BBB");
                    Imgui.SameLine();
                    Imgui.BeginGroup();
                    _ = Imgui.Button("CCC");
                    _ = Imgui.Button("DDD");
                    Imgui.EndGroup();
                    Imgui.SameLine();
                    _ = Imgui.Button("EEE");
                    Imgui.EndGroup();
                    if (Imgui.IsItemHovered())
                    {
                        Imgui.SetTooltip("First group hovered");
                    }
                }
                // Capture the group size and create widgets using the same size
                var size = Imgui.GetItemRectangleSize();
                var values = new float[] { 0.5f, 0.20f, 0.80f, 0.60f, 0.25f };
                Imgui.PlotHistogram("##values", values, 0, null, 0.0f, 1.0f, size);

                _ = Imgui.Button("ACTION", new((size.Width - Imgui.GetStyle().ItemSpacing.Width) * 0.5f, size.Height));
                Imgui.SameLine();
                _ = Imgui.Button("REACTION", new((size.Width - Imgui.GetStyle().ItemSpacing.Width) * 0.5f, size.Height));
                Imgui.EndGroup();
                Imgui.SameLine();

                _ = Imgui.Button("LEVERAGE\nBUZZWORD", size);
                Imgui.SameLine();

                if (Imgui.BeginListBox("List", size))
                {
                    _ = Imgui.Selectable("Selected", true);
                    _ = Imgui.Selectable("Not Selected", false);
                    Imgui.EndListBox();
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Text Baseline Alignment"))
            {
                {
                    Imgui.BulletText("Text baseline:");
                    Imgui.SameLine(); HelpMarker(
                        "This is testing the vertical alignment that gets applied on text to keep it aligned with widgets. " +
                        "Lines only composed of text or \"small\" widgets use less vertical space than lines with framed widgets.");
                    Imgui.Indent();

                    Imgui.Text("KO Blahblah");
                    Imgui.SameLine();
                    _ = Imgui.Button("Some framed item");
                    Imgui.SameLine();
                    HelpMarker("Baseline of button will look misaligned with text..");

                    Imgui.AlignTextToFramePadding();
                    Imgui.Text("OK Blahblah");
                    Imgui.SameLine();
                    _ = Imgui.Button("Some framed item");
                    Imgui.SameLine();
                    HelpMarker("We call AlignTextToFramePadding() to vertically align the text baseline by +FramePadding.Y");

                    _ = Imgui.Button("TEST##1");
                    Imgui.SameLine();
                    Imgui.Text("TEST");
                    Imgui.SameLine();
                    _ = Imgui.SmallButton("TEST##2");

                    Imgui.AlignTextToFramePadding();
                    Imgui.Text("Text aligned to framed item");
                    Imgui.SameLine();
                    _ = Imgui.Button("Item##1");
                    Imgui.SameLine();
                    Imgui.Text("Item");
                    Imgui.SameLine();
                    _ = Imgui.SmallButton("Item##2");
                    Imgui.SameLine();
                    _ = Imgui.Button("Item##3");

                    Imgui.Unindent();
                }

                Imgui.Spacing();

                {
                    Imgui.BulletText("Multi-line text:");
                    Imgui.Indent();
                    Imgui.Text("One\nTwo\nThree");
                    Imgui.SameLine();
                    Imgui.Text("Hello\nWorld");
                    Imgui.SameLine();
                    Imgui.Text("Banana");

                    Imgui.Text("Banana");
                    Imgui.SameLine();
                    Imgui.Text("Hello\nWorld");
                    Imgui.SameLine();
                    Imgui.Text("One\nTwo\nThree");

                    _ = Imgui.Button("HOP##1");
                    Imgui.SameLine();
                    Imgui.Text("Banana");
                    Imgui.SameLine();
                    Imgui.Text("Hello\nWorld");
                    Imgui.SameLine();
                    Imgui.Text("Banana");

                    _ = Imgui.Button("HOP##2");
                    Imgui.SameLine();
                    Imgui.Text("Hello\nWorld");
                    Imgui.SameLine();
                    Imgui.Text("Banana");
                    Imgui.Unindent();
                }

                Imgui.Spacing();

                {
                    Imgui.BulletText("Misc items:");
                    Imgui.Indent();

                    _ = Imgui.Button("80x80", new(80, 80));
                    Imgui.SameLine();
                    _ = Imgui.Button("50x50", new(50, 50));
                    Imgui.SameLine();
                    _ = Imgui.Button("Button()");
                    Imgui.SameLine();
                    _ = Imgui.SmallButton("SmallButton()");

                    var spacing = Imgui.GetStyle().ItemInnerSpacing.Width;
                    _ = Imgui.Button("Button##1");
                    Imgui.SameLine(0.0f, spacing);
                    if (Imgui.TreeNode("Node##1"))
                    {
                        for (var i = 0; i < 6; i++)
                        {
                            Imgui.BulletText($"Item {i}..");
                        }

                        Imgui.TreePop();
                    }

                    Imgui.AlignTextToFramePadding();

                    var node_open = Imgui.TreeNode("Node##2");
                    Imgui.SameLine(0.0f, spacing); _ = Imgui.Button("Button##2");
                    if (node_open)
                    {
                        for (var i = 0; i < 6; i++)
                        {
                            Imgui.BulletText($"Item {i}..");
                        }

                        Imgui.TreePop();
                    }

                    _ = Imgui.Button("Button##3");
                    Imgui.SameLine(0.0f, spacing);
                    Imgui.BulletText("Bullet text");

                    Imgui.AlignTextToFramePadding();
                    Imgui.BulletText("Node");
                    Imgui.SameLine(0.0f, spacing); _ = Imgui.Button("Button##4");
                    Imgui.Unindent();
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Scrolling"))
            {
                HelpMarker("Use SetScrollHereY() or SetScrollFromPosY() to scroll to a given vertical position.");

                _ = Imgui.Checkbox("Decoration", s_enableExtraDecorations);

                _ = Imgui.Checkbox("Track", s_enableTrack);
                Imgui.PushItemWidth(100);
                Imgui.SameLine(140); s_enableTrack.Value |= Imgui.Drag("##item", s_trackItem, 0.25f, 0, 99, "Item = %d");

                var scroll_to_off = Imgui.Button("Scroll Offset");
                Imgui.SameLine(140); scroll_to_off |= Imgui.Drag("##off", s_scrollToOffPx, 1.00f, 0, float.MaxValue, "+%.0f px");

                var scroll_to_pos = Imgui.Button("Scroll To Pos");
                Imgui.SameLine(140); scroll_to_pos |= Imgui.Drag("##pos", s_scrollToPosPx, 1.00f, -10, float.MaxValue, "X/Y = %.0f px");
                Imgui.PopItemWidth();

                if (scroll_to_off || scroll_to_pos)
                {
                    s_enableTrack.Value = false;
                }

                var style = Imgui.GetStyle();
                var child_w = (Imgui.GetContentRegionAvailable().Width - (4 * style.ItemSpacing.Width)) / 5;
                if (child_w < 1.0f)
                {
                    child_w = 1.0f;
                }

                Imgui.PushId("##VerticalScrolling");
                for (var i = 0; i < 5; i++)
                {
                    if (i > 0)
                    {
                        Imgui.SameLine();
                    }

                    Imgui.BeginGroup();
                    var names = new[] { "Top", "25%", "Center", "75%", "Bottom" };
                    Imgui.TextUnformatted(names[i]);

                    var child_flags = s_enableExtraDecorations ? WindowOptions.MenuBar : 0;
                    var child_id = Imgui.GetId((nuint)i);
                    var child_is_visible = Imgui.BeginChild(child_id, new(child_w, 200.0f), true, child_flags);
                    if (Imgui.BeginMenuBar())
                    {
                        Imgui.TextUnformatted("abc");
                        Imgui.EndMenuBar();
                    }
                    if (scroll_to_off)
                    {
                        Imgui.SetScrollY(s_scrollToOffPx);
                    }

                    if (scroll_to_pos)
                    {
                        Imgui.SetScrollFromPositionY(Imgui.GetCursorStartPosition().Y + s_scrollToPosPx, i * 0.25f);
                    }

                    if (child_is_visible) // Avoid calling SetScrollHereY when running with culled items
                    {
                        for (var item = 0; item < 100; item++)
                        {
                            if (s_enableTrack && item == s_trackItem)
                            {
                                Imgui.TextColored(new(1, 1, 0, 1), $"Item {item}");
                                Imgui.SetScrollHereY(i * 0.25f);
                            }
                            else
                            {
                                Imgui.Text($"Item {item}");
                            }
                        }
                    }
                    var scroll_y = Imgui.GetScrollY();
                    var scroll_max_y = Imgui.GetScrollMaxY();
                    Imgui.EndChild();
                    Imgui.Text($"{scroll_y:F0}/{scroll_max_y:F0}");
                    Imgui.EndGroup();
                }
                Imgui.PopId();

                Imgui.Spacing();
                HelpMarker(
                    "Use SetScrollHereX() or SetScrollFromPosX() to scroll to a given horizontal position.\n\n" +
                    "Because the clipping rectangle of most window hides half worth of WindowPadding on the " +
                    "left/right, using SetScrollFromPosX(+1) will usually result in clipped text whereas the " +
                    "equivalent SetScrollFromPosY(+1) wouldn't.");
                Imgui.PushId("##HorizontalScrolling");
                for (var i = 0; i < 5; i++)
                {
                    var child_height = Imgui.GetTextLineHeight() + style.ScrollbarSize + (style.WindowPadding.Width * 2.0f);
                    var child_flags = WindowOptions.HorizontalScrollbar | (s_enableExtraDecorations ? WindowOptions.AlwaysVerticalScrollbar : 0);
                    var child_id = Imgui.GetId((nuint)i);
                    var child_is_visible = Imgui.BeginChild(child_id, new(-100, child_height), true, child_flags);
                    if (scroll_to_off)
                    {
                        Imgui.SetScrollX(s_scrollToOffPx);
                    }

                    if (scroll_to_pos)
                    {
                        Imgui.SetScrollFromPositionX(Imgui.GetCursorStartPosition().X + s_scrollToPosPx, i * 0.25f);
                    }

                    if (child_is_visible)
                    {
                        for (var item = 0; item < 100; item++)
                        {
                            if (item > 0)
                            {
                                Imgui.SameLine();
                            }

                            if (s_enableTrack && item == s_trackItem)
                            {
                                Imgui.TextColored(new(1, 1, 0, 1), $"Item {item}");
                                Imgui.SetScrollHereX(i * 0.25f);
                            }
                            else
                            {
                                Imgui.Text($"Item {item}");
                            }
                        }
                    }
                    Imgui.EndChild();
                    Imgui.SameLine();
                    var names = new[] { "Left", "25%", "Center", "75%", "Right" };
                    Imgui.Text($"{names[i]}\n{Imgui.GetScrollX():F0}/{Imgui.GetScrollMaxX():F0}");
                    Imgui.Spacing();
                }
                Imgui.PopId();

                HelpMarker(
                    "Horizontal scrolling for a window is enabled via the WindowOptions.HorizontalScrollbar flag.\n\n" +
                    "You may want to also explicitly specify content width by using SetNextWindowContentWidth() before Begin().");
                _ = Imgui.Slider("Lines", s_lines, 1, 15);
                Imgui.PushStyleVariable(StyleVariable.FrameRounding, 3.0f);
                Imgui.PushStyleVariable(StyleVariable.FramePadding, new SizeF(2.0f, 1.0f));
                SizeF scrolling_child_size = new(0, (Imgui.GetFrameHeightWithSpacing() * 7) + 30);
                _ = Imgui.BeginChild("scrolling", scrolling_child_size, true, WindowOptions.HorizontalScrollbar);
                for (var line = 0; line < s_lines; line++)
                {
                    var num_buttons = 10 + ((line & 1) == 1 ? line * 9 : line * 3);
                    for (var n = 0; n < num_buttons; n++)
                    {
                        if (n > 0)
                        {
                            Imgui.SameLine();
                        }

                        Imgui.PushId(n + (line * 1000));
                        var label = ((n % 15) == 0) ? "FizzBuzz" : ((n % 3) == 0) ? "Fizz" : ((n % 5) == 0) ? "Buzz" : $"{n}";
                        var hue = n * 0.05f;
                        Imgui.PushStyleColor(StyleColor.Button, ColorF.FromHsv(hue, 0.6f, 0.6f));
                        Imgui.PushStyleColor(StyleColor.ButtonHovered, ColorF.FromHsv(hue, 0.7f, 0.7f));
                        Imgui.PushStyleColor(StyleColor.ButtonActive, ColorF.FromHsv(hue, 0.8f, 0.8f));
                        _ = Imgui.Button(label, new(40.0f + ((float)Math.Sin(line + n) * 20.0f), 0.0f));
                        Imgui.PopStyleColor(3);
                        Imgui.PopId();
                    }
                }
                var scroll_x = Imgui.GetScrollX();
                var scroll_max_x = Imgui.GetScrollMaxX();
                Imgui.EndChild();
                Imgui.PopStyleVariable(2);
                var scroll_x_delta = 0.0f;
                _ = Imgui.SmallButton("<<");
                if (Imgui.IsItemActive())
                {
                    scroll_x_delta = -Imgui.GetIo().DeltaTime * 1000.0f;
                }

                Imgui.SameLine();
                Imgui.Text("Scroll from code");
                Imgui.SameLine();
                _ = Imgui.SmallButton(">>");
                if (Imgui.IsItemActive())
                {
                    scroll_x_delta = +Imgui.GetIo().DeltaTime * 1000.0f;
                }

                Imgui.SameLine();
                Imgui.Text($"{scroll_x:F0}/{scroll_max_x:F0}");
                if (scroll_x_delta != 0.0f)
                {
                    _ = Imgui.BeginChild("scrolling");
                    Imgui.SetScrollX(Imgui.GetScrollX() + scroll_x_delta);
                    Imgui.EndChild();
                }
                Imgui.Spacing();

                _ = Imgui.Checkbox("Show Horizontal contents size demo window", s_showHorizontalContentsSizeDemoWindow);

                if (s_showHorizontalContentsSizeDemoWindow)
                {
                    if (s_explicitContentSize)
                    {
                        Imgui.SetNextWindowContentSize(new(s_contentsSizeX, 0.0f));
                    }

                    _ = Imgui.Begin("Horizontal contents size demo window", s_showHorizontalContentsSizeDemoWindow, s_showHScrollbar ? WindowOptions.HorizontalScrollbar : 0);
                    Imgui.PushStyleVariable(StyleVariable.ItemSpacing, new SizeF(2, 0));
                    Imgui.PushStyleVariable(StyleVariable.FramePadding, new SizeF(2, 0));
                    HelpMarker("Test of different widgets react and impact the work rectangle growing when horizontal scrolling is enabled.\n\nUse 'Metrics->Tools->Show windows rectangles' to visualize rectangles.");
                    _ = Imgui.Checkbox("H-scrollbar", s_showHScrollbar);
                    _ = Imgui.Checkbox("Button", s_showButton);
                    _ = Imgui.Checkbox("Tree nodes", s_showTreeNodes);
                    _ = Imgui.Checkbox("Text wrapped", s_showTextWrapped);
                    _ = Imgui.Checkbox("Columns", s_showColumns);
                    _ = Imgui.Checkbox("Tab bar", s_showTabBar);
                    _ = Imgui.Checkbox("Child", s_showChild);
                    _ = Imgui.Checkbox("Explicit content size", s_explicitContentSize);
                    Imgui.Text($"Scroll {Imgui.GetScrollX():F1}/{Imgui.GetScrollMaxX():F1} {Imgui.GetScrollY():F1}/{Imgui.GetScrollMaxY():F1}");
                    if (s_explicitContentSize)
                    {
                        Imgui.SameLine();
                        Imgui.SetNextItemWidth(100);
                        _ = Imgui.Drag("##csx", s_contentsSizeX);
                        var p = Imgui.GetCursorScreenPosition();
                        Imgui.GetWindowDrawList()!.Value.AddRectangleFilled(new RectangleF(p, new PositionF(p.X + 10, p.Y + 10)), Color.White);
                        Imgui.GetWindowDrawList()!.Value.AddRectangleFilled(new RectangleF(new PositionF(p.X + s_contentsSizeX - 10, p.Y), new PositionF(p.X + s_contentsSizeX, p.Y + 10)), Color.White);
                        Imgui.Dummy(new(0, 10));
                    }
                    Imgui.PopStyleVariable(2);
                    Imgui.Separator();
                    if (s_showButton)
                    {
                        _ = Imgui.Button("this is a 300-wide button", new(300, 0));
                    }
                    if (s_showTreeNodes)
                    {
                        if (Imgui.TreeNode("this is a tree node"))
                        {
                            if (Imgui.TreeNode("another one of those tree node..."))
                            {
                                Imgui.Text("Some tree contents");
                                Imgui.TreePop();
                            }
                            Imgui.TreePop();
                        }
                        _ = Imgui.CollapsingHeader("CollapsingHeader", s_open);
                    }
                    if (s_showTextWrapped)
                    {
                        Imgui.TextWrapped("This text should automatically wrap on the edge of the work rectangle.");
                    }
                    if (s_showColumns)
                    {
                        Imgui.Text("Tables:");
                        if (Imgui.BeginTable("table", 4, TableOptions.Borders))
                        {
                            for (var n = 0; n < 4; n++)
                            {
                                _ = Imgui.TableNextColumn();
                                Imgui.Text($"Width {Imgui.GetContentRegionAvailable().Width:F2}");
                            }
                            Imgui.EndTable();
                        }
                        Imgui.Text("Columns:");
                        Imgui.Columns(4);
                        for (var n = 0; n < 4; n++)
                        {
                            Imgui.Text($"Width {Imgui.GetColumnWidth():F2}");
                            Imgui.NextColumn();
                        }
                        Imgui.Columns(1);
                    }
                    if (s_showTabBar && Imgui.BeginTabBar("Hello"))
                    {
                        if (Imgui.BeginTabItem("OneOneOne"))
                        {
                            Imgui.EndTabItem();
                        }
                        if (Imgui.BeginTabItem("TwoTwoTwo"))
                        {
                            Imgui.EndTabItem();
                        }
                        if (Imgui.BeginTabItem("ThreeThreeThree"))
                        {
                            Imgui.EndTabItem();
                        }
                        if (Imgui.BeginTabItem("FourFourFour"))
                        {
                            Imgui.EndTabItem();
                        }
                        Imgui.EndTabBar();
                    }
                    if (s_showChild)
                    {
                        _ = Imgui.BeginChild("child", new(0, 0), true);
                        Imgui.EndChild();
                    }
                    Imgui.End();
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Clipping"))
            {
                _ = Imgui.Drag("size", s_size, 0.5f, 1.0f, 200.0f, "%.0f");
                Imgui.TextWrapped("(Click and drag to scroll)");

                HelpMarker(
                    "(Left) Using Imgui.PushClipRect():\n" +
                    "Will alter ImGui hit-testing logic + ImDrawList rendering.\n" +
                    "(use this if you want your clipping rectangle to affect interactions)\n\n" +
                    "(Center) Using ImDrawList::PushClipRect():\n" +
                    "Will alter ImDrawList rendering only.\n" +
                    "(use this as a shortcut if you are only using ImDrawList calls)\n\n" +
                    "(Right) Using ImDrawList::AddText() with a fine ClipRect:\n" +
                    "Will alter only this specific ImDrawList::AddText() rendering.\n" +
                    "This is often used internally to avoid altering the clipping rectangle and minimize draw calls.");

                for (var n = 0; n < 3; n++)
                {
                    if (n > 0)
                    {
                        Imgui.SameLine();
                    }

                    Imgui.PushId(n);
                    _ = Imgui.InvisibleButton("##canvas", new(s_size[0], s_size[1]));
                    if (Imgui.IsItemActive() && Imgui.IsMouseDragging(MouseButton.Left))
                    {
                        s_offsetX2.Value += Imgui.GetIo().MouseDelta.Width;
                        s_offsetY2.Value += Imgui.GetIo().MouseDelta.Height;
                    }
                    Imgui.PopId();
                    if (!Imgui.IsItemVisible()) // Skip rendering as ImDrawList elements are not clipped.
                    {
                        continue;
                    }

                    var p0 = Imgui.GetItemRectangle().Min;
                    var p1 = Imgui.GetItemRectangle().Max;
                    var text_str = "Line 1 hello\nLine 2 clip me!";
                    var text_pos = new PositionF(p0.X + s_offsetX2, p0.Y + s_offsetY2);
                    var draw_list = Imgui.GetWindowDrawList()!.Value;
                    switch (n)
                    {
                        case 0:
                            Imgui.PushClipRectangle(new(p0, p1), true);
                            draw_list.AddRectangleFilled(new(p0, p1), new(90, 90, 120, 255));
                            draw_list.AddText(text_pos, Color.White, text_str);
                            Imgui.PopClipRectangle();
                            break;
                        case 1:
                            draw_list.PushClipRectangle(new(p0, p1), true);
                            draw_list.AddRectangleFilled(new(p0, p1), new(90, 90, 120, 255));
                            draw_list.AddText(text_pos, Color.White, text_str);
                            draw_list.PopClipRectangle();
                            break;
                        case 2:
                            var clip_rect = new RectangleF(p0, p1);
                            draw_list.AddRectangleFilled(new(p0, p1), new(90, 90, 120, 255));
                            draw_list.AddText(Imgui.GetFont()!.Value, Imgui.GetFontSize(), text_pos, Color.White, text_str, 0.0f, clip_rect);
                            break;
                    }
                }

                Imgui.TreePop();
            }
        }

        private static int s_selectedFish = -1;
        private static readonly StateVector<bool> s_toggles = new(5, new[] { true, false, false, false, false });
        private static int s_selected8 = -1;
        private static readonly State<float> s_value = new(0.5f);
        private static readonly StateText s_name = new(32, "Label1");
        private static readonly State<bool> s_dontAskMeNextTime = new(false);
        private static readonly State<int> s_item2 = new(1);
        private static readonly State<ColorF> s_color2 = new(new(0.4f, 0.7f, 0.0f, 0.5f));
        private static readonly State<bool> s_unusedOpen = new(true);

        private static void ShowDemoWindowPopups()
        {
            if (!Imgui.CollapsingHeader("Popups & Modal windows"))
            {
                return;
            }

            if (Imgui.TreeNode("Popups"))
            {
                Imgui.TextWrapped(
                    "When a popup is active, it inhibits interacting with windows that are behind the popup. " +
                    "Clicking outside the popup closes it.");

                var names = new[] { "Bream", "Haddock", "Mackerel", "Pollock", "Tilefish" };

                if (Imgui.Button("Select.."))
                {
                    Imgui.OpenPopup("my_select_popup");
                }

                Imgui.SameLine();
                Imgui.TextUnformatted(s_selectedFish == -1 ? "<None>" : names[s_selectedFish]);
                if (Imgui.BeginPopup("my_select_popup"))
                {
                    Imgui.Text("Aquarium");
                    Imgui.Separator();
                    for (var i = 0; i < names.Length; i++)
                    {
                        if (Imgui.Selectable(names[i]))
                        {
                            s_selectedFish = i;
                        }
                    }

                    Imgui.EndPopup();
                }

                if (Imgui.Button("Toggle.."))
                {
                    Imgui.OpenPopup("my_toggle_popup");
                }

                if (Imgui.BeginPopup("my_toggle_popup"))
                {
                    for (var i = 0; i < names.Length; i++)
                    {
                        _ = Imgui.MenuItem(names[i], "", s_toggles.GetStateOfElement(i));
                    }

                    if (Imgui.BeginMenu("Sub-menu"))
                    {
                        _ = Imgui.MenuItem("Click me");
                        Imgui.EndMenu();
                    }

                    Imgui.Separator();
                    Imgui.Text("Tooltip here");
                    if (Imgui.IsItemHovered())
                    {
                        Imgui.SetTooltip("I am a tooltip over a popup");
                    }

                    if (Imgui.Button("Stacked Popup"))
                    {
                        Imgui.OpenPopup("another popup");
                    }

                    if (Imgui.BeginPopup("another popup"))
                    {
                        for (var i = 0; i < names.Length; i++)
                        {
                            _ = Imgui.MenuItem(names[i], "", s_toggles.GetStateOfElement(i));
                        }

                        if (Imgui.BeginMenu("Sub-menu"))
                        {
                            _ = Imgui.MenuItem("Click me");
                            if (Imgui.Button("Stacked Popup"))
                            {
                                Imgui.OpenPopup("another popup");
                            }

                            if (Imgui.BeginPopup("another popup"))
                            {
                                Imgui.Text("I am the last one here.");
                                Imgui.EndPopup();
                            }
                            Imgui.EndMenu();
                        }
                        Imgui.EndPopup();
                    }
                    Imgui.EndPopup();
                }

                if (Imgui.Button("With a menu.."))
                {
                    Imgui.OpenPopup("my_file_popup");
                }

                if (Imgui.BeginPopup("my_file_popup", WindowOptions.MenuBar))
                {
                    if (Imgui.BeginMenuBar())
                    {
                        if (Imgui.BeginMenu("File"))
                        {
                            ShowExampleMenuFile();
                            Imgui.EndMenu();
                        }
                        if (Imgui.BeginMenu("Edit"))
                        {
                            _ = Imgui.MenuItem("Dummy");
                            Imgui.EndMenu();
                        }
                        Imgui.EndMenuBar();
                    }
                    Imgui.Text("Hello from popup!");
                    _ = Imgui.Button("This is a dummy button..");
                    Imgui.EndPopup();
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Context menus"))
            {
                HelpMarker("\"Context\" functions are simple helpers to associate a Popup to a given Item or Window identifier.");

                {
                    var names = new[] { "Label1", "Label2", "Label3", "Label4", "Label5" };
                    for (var n = 0; n < 5; n++)
                    {
                        if (Imgui.Selectable(names[n], s_selected8 == n))
                        {
                            s_selected8 = n;
                        }

                        if (Imgui.BeginPopupContextItem())
                        {
                            s_selected8 = n;
                            Imgui.Text($"This a popup for \"{names[n]}\"!");
                            if (Imgui.Button("Close"))
                            {
                                Imgui.CloseCurrentPopup();
                            }

                            Imgui.EndPopup();
                        }
                        if (Imgui.IsItemHovered())
                        {
                            Imgui.SetTooltip("Right-click to open popup");
                        }
                    }
                }

                {
                    HelpMarker("Text() elements don't have stable identifiers so we need to provide one.");
                    Imgui.Text($"Value = {s_value.Value:F3} <-- (1) right-click this text");
                    if (Imgui.BeginPopupContextItem("my popup"))
                    {
                        if (Imgui.Selectable("Set to zero"))
                        {
                            s_value.Value = 0.0f;
                        }

                        if (Imgui.Selectable("Set to PI"))
                        {
                            s_value.Value = 3.1415f;
                        }

                        Imgui.SetNextItemWidth(-SizeF.MinNormalizedValue);
                        _ = Imgui.Drag("##Value", s_value, 0.1f, 0.0f, 0.0f);
                        Imgui.EndPopup();
                    }

                    Imgui.Text("(2) Or right-click this text");
                    Imgui.OpenPopupOnItemClick("my popup", PopupOptions.MouseButtonRight);

                    if (Imgui.Button("(3) Or click this button"))
                    {
                        Imgui.OpenPopup("my popup");
                    }
                }

                {
                    HelpMarker("Showcase using a popup ID linked to item ID, with the item having a changing label + stable ID using the ### operator.");
                    _ = Imgui.Button($"Button: {s_name}###Button");
                    if (Imgui.BeginPopupContextItem())
                    {
                        Imgui.Text("Edit name:");
                        _ = Imgui.InputText("##edit", s_name);
                        if (Imgui.Button("Close"))
                        {
                            Imgui.CloseCurrentPopup();
                        }

                        Imgui.EndPopup();
                    }
                    Imgui.SameLine();
                    Imgui.Text("(<-- right-click here)");
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Modals"))
            {
                Imgui.TextWrapped("Modal windows are like popups but the user cannot close them by clicking outside.");

                if (Imgui.Button("Delete.."))
                {
                    Imgui.OpenPopup("Delete?");
                }

                var center = Imgui.GetMainViewport().Center;
                Imgui.SetNextWindowPosition(center, Condition.Appearing, new(0.5f, 0.5f));

                if (Imgui.BeginPopupModal("Delete?", null, WindowOptions.AlwaysAutoResize))
                {
                    Imgui.Text("All those beautiful files will be deleted.\nThis operation cannot be undone!\n\n");
                    Imgui.Separator();

                    Imgui.PushStyleVariable(StyleVariable.FramePadding, new SizeF(0, 0));
                    _ = Imgui.Checkbox("Don't ask me next time", s_dontAskMeNextTime);
                    Imgui.PopStyleVariable();

                    if (Imgui.Button("OK", new(120, 0)))
                    {
                        Imgui.CloseCurrentPopup();
                    }
                    Imgui.SetItemDefaultFocus();
                    Imgui.SameLine();
                    if (Imgui.Button("Cancel", new(120, 0)))
                    {
                        Imgui.CloseCurrentPopup();
                    }
                    Imgui.EndPopup();
                }

                if (Imgui.Button("Stacked modals.."))
                {
                    Imgui.OpenPopup("Stacked 1");
                }

                if (Imgui.BeginPopupModal("Stacked 1", null, WindowOptions.MenuBar))
                {
                    if (Imgui.BeginMenuBar())
                    {
                        if (Imgui.BeginMenu("File"))
                        {
                            if (Imgui.MenuItem("Some menu item")) { }
                            Imgui.EndMenu();
                        }
                        Imgui.EndMenuBar();
                    }
                    Imgui.Text("Hello from Stacked The First\nUsing style.Colors[StyleColor.ModalWindowDimBg] behind it.");

                    _ = Imgui.Combo("Combo", s_item2, "aaaa\0bbbb\0cccc\0dddd\0eeee\0\0");
                    _ = Imgui.ColorEdit("color", s_color2);

                    if (Imgui.Button("Add another modal.."))
                    {
                        Imgui.OpenPopup("Stacked 2");
                    }

                    if (Imgui.BeginPopupModal("Stacked 2", s_unusedOpen))
                    {
                        Imgui.Text("Hello from Stacked The Second!");
                        if (Imgui.Button("Close"))
                        {
                            Imgui.CloseCurrentPopup();
                        }

                        Imgui.EndPopup();
                    }

                    if (Imgui.Button("Close"))
                    {
                        Imgui.CloseCurrentPopup();
                    }

                    Imgui.EndPopup();
                }

                Imgui.TreePop();
            }

            if (Imgui.TreeNode("Menus inside a regular window"))
            {
                Imgui.TextWrapped("Below we are testing adding menu items to a regular window. It's rather unusual but should work!");
                Imgui.Separator();

                _ = Imgui.MenuItem("Menu item", "CTRL+M");
                if (Imgui.BeginMenu("Menu inside a regular window"))
                {
                    ShowExampleMenuFile();
                    Imgui.EndMenu();
                }
                Imgui.Separator();
                Imgui.TreePop();
            }
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

        private static void PushStyleCompact()
        {
            var style = Imgui.GetStyle();
            Imgui.PushStyleVariable(StyleVariable.FramePadding, new SizeF(style.FramePadding.Width, (int)(style.FramePadding.Height * 0.60f)));
            Imgui.PushStyleVariable(StyleVariable.ItemSpacing, new SizeF(style.ItemSpacing.Width, (int)(style.ItemSpacing.Height * 0.60f)));
        }

        private static void PopStyleCompact() => Imgui.PopStyleVariable(2);

        private static void EditTableSizingFlags(StateOption<TableOptions> p_flags)
        {
            var policies = new (TableOptions Value, string Name, string Tooltip)[]
            {
                new(TableOptions.None,               "Default",                         "Use default sizing policy:\n- TableOptions.SizingFixedFit if ScrollX is on or if host window has WindowOptions.AlwaysAutoResize.\n- TableOptions.SizingStretchSame otherwise."),
                new(TableOptions.SizingFixedFit,     "TableOptions.SizingFixedFit",     "Columns default to _WidthFixed (if resizable) or _WidthAuto (if not resizable), matching contents width."),
                new(TableOptions.SizingFixedSame,    "TableOptions.SizingFixedSame",    "Columns are all the same width, matching the maximum contents width.\nImplicitly disable TableOptions.Resizable and enable TableOptions.NoKeepColumnsVisible."),
                new(TableOptions.SizingStretchProp,  "TableOptions.SizingStretchProp",  "Columns default to _WidthStretch with weights proportional to their widths."),
                new(TableOptions.SizingStretchSame,  "TableOptions.SizingStretchSame",  "Columns default to _WidthStretch with same weights.")
            };

            int idx;
            for (idx = 0; idx < policies.Length; idx++)
            {
                if (policies[idx].Value == (p_flags & TableOptions.SizingMask))
                {
                    break;
                }
            }

            var preview_text = (idx < policies.Length) ? policies[idx].Name[(idx > 0 ? "TableOptions".Length : 0)..] : "";
            if (Imgui.BeginCombo("Sizing Policy", preview_text))
            {
                for (var n = 0; n < policies.Length; n++)
                {
                    if (Imgui.Selectable(policies[n].Name, idx == n))
                    {
                        p_flags.Value = (p_flags & ~TableOptions.SizingMask) | policies[n].Value;
                    }
                }

                Imgui.EndCombo();
            }
            Imgui.SameLine();
            Imgui.TextDisabled("(?)");
            if (Imgui.IsItemHovered())
            {
                Imgui.BeginTooltip();
                Imgui.PushTextWrapPosition(Imgui.GetFontSize() * 50.0f);
                for (var m = 0; m < policies.Length; m++)
                {
                    Imgui.Separator();
                    Imgui.Text($"{policies[m].Name}:");
                    Imgui.Separator();
                    Imgui.SetCursorPosX(Imgui.GetCursorPosX() + Imgui.GetStyle().IndentSpacing * 0.5f);
                    Imgui.TextUnformatted(policies[m].Tooltip);
                }
                Imgui.PopTextWrapPosition();
                Imgui.EndTooltip();
            }
        }

        private static void EditTableColumnsFlags(StateOption<TableColumnOptions> p_flags)
        {
            _ = Imgui.CheckboxFlags(".Disabled", p_flags, TableColumnOptions.Disabled);
            Imgui.SameLine();
            HelpMarker("Master disable flag (also hide from context menu)");
            _ = Imgui.CheckboxFlags(".DefaultHide", p_flags, TableColumnOptions.DefaultHide);
            _ = Imgui.CheckboxFlags(".DefaultSort", p_flags, TableColumnOptions.DefaultSort);
            if (Imgui.CheckboxFlags(".WidthStretch", p_flags, TableColumnOptions.WidthStretch))
            {
                p_flags.Value &= ~(TableColumnOptions.WidthMask ^ TableColumnOptions.WidthStretch);
            }

            if (Imgui.CheckboxFlags(".WidthFixed", p_flags, TableColumnOptions.WidthFixed))
            {
                p_flags.Value &= ~(TableColumnOptions.WidthMask ^ TableColumnOptions.WidthFixed);
            }

            _ = Imgui.CheckboxFlags(".NoResize", p_flags, TableColumnOptions.NoResize);
            _ = Imgui.CheckboxFlags(".NoReorder", p_flags, TableColumnOptions.NoReorder);
            _ = Imgui.CheckboxFlags(".NoHide", p_flags, TableColumnOptions.NoHide);
            _ = Imgui.CheckboxFlags(".NoClip", p_flags, TableColumnOptions.NoClip);
            _ = Imgui.CheckboxFlags(".NoSort", p_flags, TableColumnOptions.NoSort);
            _ = Imgui.CheckboxFlags(".NoSortAscending", p_flags, TableColumnOptions.NoSortAscending);
            _ = Imgui.CheckboxFlags(".NoSortDescending", p_flags, TableColumnOptions.NoSortDescending);
            _ = Imgui.CheckboxFlags(".NoHeaderLabel", p_flags, TableColumnOptions.NoHeaderLabel);
            _ = Imgui.CheckboxFlags(".NoHeaderWidth", p_flags, TableColumnOptions.NoHeaderWidth);
            _ = Imgui.CheckboxFlags(".PreferSortAscending", p_flags, TableColumnOptions.PreferSortAscending);
            _ = Imgui.CheckboxFlags(".PreferSortDescending", p_flags, TableColumnOptions.PreferSortDescending);
            _ = Imgui.CheckboxFlags(".IndentEnable", p_flags, TableColumnOptions.IndentEnable); Imgui.SameLine(); HelpMarker("Default for column 0");
            _ = Imgui.CheckboxFlags(".IndentDisable", p_flags, TableColumnOptions.IndentDisable); Imgui.SameLine(); HelpMarker("Default for column >0");
        }

        private static void ShowTableColumnsStatusFlags(StateOption<TableColumnOptions> flags)
        {
            _ = Imgui.CheckboxFlags(".IsEnabled", flags, TableColumnOptions.IsEnabled);
            _ = Imgui.CheckboxFlags(".IsVisible", flags, TableColumnOptions.IsVisible);
            _ = Imgui.CheckboxFlags(".IsSorted", flags, TableColumnOptions.IsSorted);
            _ = Imgui.CheckboxFlags(".IsHovered", flags, TableColumnOptions.IsHovered);
        }

        private static readonly State<bool> s_disableIndent = new(false);
        private enum ContentsType
        {
            CT_Text,
            CT_FillButton
        };
        private static readonly StateOption<TableOptions> s_flags4 = new(TableOptions.Borders | TableOptions.RowBg);
        private static readonly State<bool> s_displayHeaders = new(false);
        private static readonly State<int> s_contentsType = new((int)ContentsType.CT_Text);

        private static void ShowDemoWindowTables()
        {
            if (!Imgui.CollapsingHeader("Tables & Columns"))
            {
                return;
            }

            var TEXT_BASE_WIDTH = Imgui.CalcTextSize("A").Width;
            var TEXT_BASE_HEIGHT = Imgui.GetTextLineHeightWithSpacing();

            Imgui.PushId("Tables");

            var open_action = -1;
            if (Imgui.Button("Open all"))
            {
                open_action = 1;
            }

            Imgui.SameLine();
            if (Imgui.Button("Close all"))
            {
                open_action = 0;
            }

            Imgui.SameLine();

            _ = Imgui.Checkbox("Disable tree indentation", s_disableIndent);
            Imgui.SameLine();
            HelpMarker("Disable the indenting of tree nodes so demo tables can use the full window width.");
            Imgui.Separator();
            if (s_disableIndent)
            {
                Imgui.PushStyleVariable(StyleVariable.IndentSpacing, 0.0f);
            }

            if (open_action != -1)
            {
                Imgui.SetNextItemOpen(open_action != 0);
            }

            if (Imgui.TreeNode("Basic"))
            {
                HelpMarker("Using TableNextRow() + calling TableSetColumnIndex() _before_ each cell, in a loop.");
                if (Imgui.BeginTable("table1", 3))
                {
                    for (var row = 0; row < 4; row++)
                    {
                        Imgui.TableNextRow();
                        for (var column = 0; column < 3; column++)
                        {
                            _ = Imgui.TableSetColumnIndex(column);
                            Imgui.Text($"Row {row} Column {column}");
                        }
                    }
                    Imgui.EndTable();
                }

                HelpMarker("Using TableNextRow() + calling TableNextColumn() _before_ each cell, manually.");
                if (Imgui.BeginTable("table2", 3))
                {
                    for (var row = 0; row < 4; row++)
                    {
                        Imgui.TableNextRow();
                        _ = Imgui.TableNextColumn();
                        Imgui.Text($"Row {row}");
                        _ = Imgui.TableNextColumn();
                        Imgui.Text("Some contents");
                        _ = Imgui.TableNextColumn();
                        Imgui.Text("123.456");
                    }
                    Imgui.EndTable();
                }

                HelpMarker(
                    "Only using TableNextColumn(), which tends to be convenient for tables where every cell contains the same type of contents.\n" +
                    "This is also more similar to the old NextColumn() function of the Columns API, and provided to facilitate the Columns->Tables API transition.");
                if (Imgui.BeginTable("table3", 3))
                {
                    for (var item = 0; item < 14; item++)
                    {
                        _ = Imgui.TableNextColumn();
                        Imgui.Text($"Item {item}");
                    }
                    Imgui.EndTable();
                }

                Imgui.TreePop();
            }

            if (open_action != -1)
            {
                Imgui.SetNextItemOpen(open_action != 0);
            }

            if (Imgui.TreeNode("Borders, background"))
            {
                PushStyleCompact();
                _ = Imgui.CheckboxFlags("TableOptions.RowBg", s_flags4, TableOptions.RowBg);
                _ = Imgui.CheckboxFlags("TableOptions.Borders", s_flags4, TableOptions.Borders);
                Imgui.SameLine();
                HelpMarker("TableOptions.Borders\n = TableOptions.BordersInnerV\n | TableOptions.BordersOuterV\n | TableOptions.BordersInnerV\n | TableOptions.BordersOuterH");
                Imgui.Indent();

                _ = Imgui.CheckboxFlags("TableOptions.BordersH", s_flags4, TableOptions.BordersH);
                Imgui.Indent();
                _ = Imgui.CheckboxFlags("TableOptions.BordersOuterH", s_flags4, TableOptions.BordersOuterH);
                _ = Imgui.CheckboxFlags("TableOptions.BordersInnerH", s_flags4, TableOptions.BordersInnerH);
                Imgui.Unindent();

                _ = Imgui.CheckboxFlags("TableOptions.BordersV", s_flags4, TableOptions.BordersV);
                Imgui.Indent();
                _ = Imgui.CheckboxFlags("TableOptions.BordersOuterV", s_flags4, TableOptions.BordersOuterV);
                _ = Imgui.CheckboxFlags("TableOptions.BordersInnerV", s_flags4, TableOptions.BordersInnerV);
                Imgui.Unindent();

                _ = Imgui.CheckboxFlags("TableOptions.BordersOuter", s_flags4, TableOptions.BordersOuter);
                _ = Imgui.CheckboxFlags("TableOptions.BordersInner", s_flags4, TableOptions.BordersInner);
                Imgui.Unindent();

                Imgui.AlignTextToFramePadding(); Imgui.Text("Cell contents:");
                Imgui.SameLine();
                _ = Imgui.RadioButton("Text", s_contentsType, (int)ContentsType.CT_Text);
                Imgui.SameLine();
                _ = Imgui.RadioButton("FillButton", s_contentsType, (int)ContentsType.CT_FillButton);
                _ = Imgui.Checkbox("Display headers", s_displayHeaders);
                _ = Imgui.CheckboxFlags("TableOptions.NoBordersInBody", s_flags4, TableOptions.NoBordersInBody);
                Imgui.SameLine();
                HelpMarker("Disable vertical borders in columns Body (borders will always appear in Headers");
                PopStyleCompact();

                if (Imgui.BeginTable("table1", 3, s_flags4))
                {
                    if (s_displayHeaders)
                    {
                        Imgui.TableSetupColumn("One");
                        Imgui.TableSetupColumn("Two");
                        Imgui.TableSetupColumn("Three");
                        Imgui.TableHeadersRow();
                    }

                    for (var row = 0; row < 5; row++)
                    {
                        Imgui.TableNextRow();
                        for (var column = 0; column < 3; column++)
                        {
                            _ = Imgui.TableSetColumnIndex(column);
                            if (s_contentsType == (int)ContentsType.CT_Text)
                            {
                                Imgui.TextUnformatted($"Hello {column},{row}");
                            }
                            else if (s_contentsType == (int)ContentsType.CT_FillButton)
                            {
                                _ = Imgui.Button($"Hello {column}, {row}", new(-SizeF.MinNormalizedValue, 0.0f));
                            }
                        }
                    }
                    Imgui.EndTable();
                }
                Imgui.TreePop();
            }

            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Resizable, stretch");
            //    if (Imgui.TreeNode("Resizable, stretch"))
            //    {
            //        // By default, if we don't enable ScrollX the sizing policy for each column is "Stretch"
            //        // All columns maintain a sizing weight, and they will occupy all available width.
            //        static ImGuiTableFlags flags = TableOptions.SizingStretchSame | TableOptions.Resizable | TableOptions.BordersOuter | TableOptions.BordersV | TableOptions.ContextMenuInBody;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.Resizable", &flags, TableOptions.Resizable);
            //        Imgui.CheckboxFlags("TableOptions.BordersV", &flags, TableOptions.BordersV);
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
            //        // Here we use TableOptions.SizingFixedFit (even though _ScrollX is not set)
            //        // So columns will adopt the "Fixed" policy and will maintain a fixed width regardless of the whole available width (unless table is small)
            //        // If there is not enough available width to fit all columns, they will however be resized down.
            //        // FIXME-TABLE: Providing a stretch-on-init would make sense especially for tables which don't have saved settings
            //        HelpMarker(
            //            "Using _Resizable + _SizingFixedFit flags.\n" +
            //            "Fixed-width columns generally makes more sense if you want to use horizontal scrolling.\n\n" +
            //            "Double-click a column border to auto-fit the column to its contents.");
            //        PushStyleCompact();
            //        static ImGuiTableFlags flags = TableOptions.SizingFixedFit | TableOptions.Resizable | TableOptions.BordersOuter | TableOptions.BordersV | TableOptions.ContextMenuInBody;
            //        Imgui.CheckboxFlags("TableOptions.NoHostExtendX", &flags, TableOptions.NoHostExtendX);
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
            //            "Using TableSetupColumn() to alter resizing policy on a per-column basis.\n\n" +
            //            "When combining Fixed and Stretch columns, generally you only want one, maybe two trailing columns to use _WidthStretch.");
            //        static ImGuiTableFlags flags = TableOptions.SizingFixedFit | TableOptions.RowBg | TableOptions.Borders | TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable;
            //
            //        if (Imgui.BeginTable("table1", 3, flags))
            //        {
            //            Imgui.TableSetupColumn("AAA", TableColumnOptions.WidthFixed);
            //            Imgui.TableSetupColumn("BBB", TableColumnOptions.WidthFixed);
            //            Imgui.TableSetupColumn("CCC", TableColumnOptions.WidthStretch);
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
            //            Imgui.TableSetupColumn("AAA", TableColumnOptions.WidthFixed);
            //            Imgui.TableSetupColumn("BBB", TableColumnOptions.WidthFixed);
            //            Imgui.TableSetupColumn("CCC", TableColumnOptions.WidthFixed | TableColumnOptions.DefaultHide);
            //            Imgui.TableSetupColumn("DDD", TableColumnOptions.WidthStretch);
            //            Imgui.TableSetupColumn("EEE", TableColumnOptions.WidthStretch);
            //            Imgui.TableSetupColumn("FFF", TableColumnOptions.WidthStretch | TableColumnOptions.DefaultHide);
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
            //            "Click and drag column headers to reorder columns.\n\n" +
            //            "Right-click on a header to open a context menu.");
            //        static ImGuiTableFlags flags = TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable | TableOptions.BordersOuter | TableOptions.BordersV;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.Resizable", &flags, TableOptions.Resizable);
            //        Imgui.CheckboxFlags("TableOptions.Reorderable", &flags, TableOptions.Reorderable);
            //        Imgui.CheckboxFlags("TableOptions.Hideable", &flags, TableOptions.Hideable);
            //        Imgui.CheckboxFlags("TableOptions.NoBordersInBody", &flags, TableOptions.NoBordersInBody);
            //        Imgui.CheckboxFlags("TableOptions.NoBordersInBodyUntilResize", &flags, TableOptions.NoBordersInBodyUntilResize); Imgui.SameLine(); HelpMarker("Disable vertical borders in columns Body until hovered for resize (borders will always appear in Headers)");
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
            //        // Use outer_size.X == 0.0f instead of default to make the table as tight as possible (only valid when no scrolling and no stretch column)
            //        if (Imgui.BeginTable("table2", 3, flags | TableOptions.SizingFixedFit, ImVec2(0.0f, 0.0f)))
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
            //            "We often want outer padding activated when any using features which makes the edges of a column visible:\n" +
            //            "e.g.:\n" +
            //            "- BorderOuterV\n" +
            //            "- any form of row selection\n" +
            //            "Because of this, activating BorderOuterV sets the default to PadOuterX. Using PadOuterX or NoPadOuterX you can override the default.\n\n" +
            //            "Actual padding values are using style.CellPadding.\n\n" +
            //            "In this demo we don't show horizontal borders to emphasize how they don't affect default horizontal padding.");
            //
            //        static ImGuiTableFlags flags1 = TableOptions.BordersV;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.PadOuterX", &flags1, TableOptions.PadOuterX);
            //        Imgui.SameLine(); HelpMarker("Enable outer-most padding (default if TableOptions.BordersOuterV is set)");
            //        Imgui.CheckboxFlags("TableOptions.NoPadOuterX", &flags1, TableOptions.NoPadOuterX);
            //        Imgui.SameLine(); HelpMarker("Disable outer-most padding (default if TableOptions.BordersOuterV is not set)");
            //        Imgui.CheckboxFlags("TableOptions.NoPadInnerX", &flags1, TableOptions.NoPadInnerX);
            //        Imgui.SameLine(); HelpMarker("Disable inner padding between columns (double inner padding if BordersOuterV is on, single inner padding if BordersOuterV is off)");
            //        Imgui.CheckboxFlags("TableOptions.BordersOuterV", &flags1, TableOptions.BordersOuterV);
            //        Imgui.CheckboxFlags("TableOptions.BordersInnerV", &flags1, TableOptions.BordersInnerV);
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
            //                        Imgui.Text("Avail %.2f", Imgui.GetContentRegionAvailable().Width);
            //                    }
            //                    else
            //                    {
            //                        char buf[32];
            //                        sprintf(buf, "Hello %d,%d", column, row);
            //                        Imgui.Button(buf, ImVec2(-FLT_MIN, 0.0f));
            //                    }
            //                    //if (Imgui.TableGetColumnFlags() & TableColumnOptions.IsHovered)
            //                    //    Imgui.TableSetBgColor(ImGuiTableBgTarget_CellBg, IM_COL32(0, 100, 0, 255));
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        // Second example: set style.CellPadding to (0.0) or a custom value.
            //        // FIXME-TABLE: Vertical border effectively not displayed the same way as horizontal one...
            //        HelpMarker("Setting style.CellPadding to (0,0) or a custom value.");
            //        static ImGuiTableFlags flags2 = TableOptions.Borders | TableOptions.RowBg;
            //        static ImVec2 cell_padding(0.0f, 0.0f);
            //        static bool show_widget_frame_bg = true;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.Borders", &flags2, TableOptions.Borders);
            //        Imgui.CheckboxFlags("TableOptions.BordersH", &flags2, TableOptions.BordersH);
            //        Imgui.CheckboxFlags("TableOptions.BordersV", &flags2, TableOptions.BordersV);
            //        Imgui.CheckboxFlags("TableOptions.BordersInner", &flags2, TableOptions.BordersInner);
            //        Imgui.CheckboxFlags("TableOptions.BordersOuter", &flags2, TableOptions.BordersOuter);
            //        Imgui.CheckboxFlags("TableOptions.RowBg", &flags2, TableOptions.RowBg);
            //        Imgui.CheckboxFlags("TableOptions.Resizable", &flags2, TableOptions.Resizable);
            //        Imgui.Checkbox("show_widget_frame_bg", &show_widget_frame_bg);
            //        Imgui.Slider("CellPadding", &cell_padding.X, 0.0f, 10.0f, "%.0f");
            //        PopStyleCompact();
            //
            //        Imgui.PushStyleVariable(StyleVariable.CellPadding, cell_padding);
            //        if (Imgui.BeginTable("table_padding_2", 3, flags2))
            //        {
            //            static char text_bufs[3 * 5][16]; // Mini text storage for 3x5 cells
            //            static bool init = true;
            //            if (!show_widget_frame_bg)
            //                Imgui.PushStyleColor(StyleColor.FrameBg, 0);
            //            for (int cell = 0; cell < 3 * 5; cell++)
            //            {
            //                Imgui.TableNextColumn();
            //                if (init)
            //                    strcpy(text_bufs[cell], "edit me");
            //                Imgui.SetNextItemWidth(-FLT_MIN);
            //                Imgui.PushId(cell);
            //                Imgui.InputText("##cell", text_bufs[cell], IM_ARRAYSIZE(text_bufs[cell]));
            //                Imgui.PopId();
            //            }
            //            if (!show_widget_frame_bg)
            //                Imgui.PopStyleColor();
            //            init = false;
            //            Imgui.EndTable();
            //        }
            //        Imgui.PopStyleVariable();
            //
            //        Imgui.TreePop();
            //    }
            //
            //    if (open_action != -1)
            //        Imgui.SetNextItemOpen(open_action != 0);
            //    IMGUI_DEMO_MARKER("Tables/Explicit widths");
            //    if (Imgui.TreeNode("Sizing policies"))
            //    {
            //        static ImGuiTableFlags flags1 = TableOptions.BordersV | TableOptions.BordersOuterH | TableOptions.RowBg | TableOptions.ContextMenuInBody;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.Resizable", &flags1, TableOptions.Resizable);
            //        Imgui.CheckboxFlags("TableOptions.NoHostExtendX", &flags1, TableOptions.NoHostExtendX);
            //        PopStyleCompact();
            //
            //        static ImGuiTableFlags sizing_policy_flags[4] = { TableOptions.SizingFixedFit, TableOptions.SizingFixedSame, TableOptions.SizingStretchProp, TableOptions.SizingStretchSame };
            //        for (int table_n = 0; table_n < 4; table_n++)
            //        {
            //            Imgui.PushId(table_n);
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
            //            Imgui.PopId();
            //        }
            //
            //        Imgui.Spacing();
            //        Imgui.TextUnformatted("Advanced");
            //        Imgui.SameLine();
            //        HelpMarker("This section allows you to interact and see the effect of various sizing policies depending on whether Scroll is enabled and the contents of your columns.");
            //
            //        enum ContentsType { CT_ShowWidth, CT_ShortText, CT_LongText, CT_Button, CT_FillButton, CT_InputText };
            //        static ImGuiTableFlags flags = TableOptions.ScrollY | TableOptions.Borders | TableOptions.RowBg | TableOptions.Resizable;
            //        static int contents_type = CT_ShowWidth;
            //        static int column_count = 3;
            //
            //        PushStyleCompact();
            //        Imgui.PushId("Advanced");
            //        Imgui.PushItemWidth(TEXT_BASE_WIDTH * 30);
            //        EditTableSizingFlags(&flags);
            //        Imgui.Combo("Contents", &contents_type, "Show width\0Short Text\0Long Text\0Button\0Fill Button\0InputText\0");
            //        if (contents_type == CT_FillButton)
            //        {
            //            Imgui.SameLine();
            //            HelpMarker("Be mindful that using right-alignment (e.g. size.X = -FLT_MIN) creates a feedback loop where contents width can feed into auto-column width can feed into contents width.");
            //        }
            //        Imgui.Drag("Columns", &column_count, 0.1f, 1, 64, "%d", SliderOptions.AlwaysClamp);
            //        Imgui.CheckboxFlags("TableOptions.Resizable", &flags, TableOptions.Resizable);
            //        Imgui.CheckboxFlags("TableOptions.PreciseWidths", &flags, TableOptions.PreciseWidths);
            //        Imgui.SameLine(); HelpMarker("Disable distributing remainder width to stretched columns (width allocation on a 100-wide table with 3 columns: Without this flag: 33,33,34. With this flag: 33,33,33). With larger number of columns, resizing will appear to be less smooth.");
            //        Imgui.CheckboxFlags("TableOptions.ScrollX", &flags, TableOptions.ScrollX);
            //        Imgui.CheckboxFlags("TableOptions.ScrollY", &flags, TableOptions.ScrollY);
            //        Imgui.CheckboxFlags("TableOptions.NoClip", &flags, TableOptions.NoClip);
            //        Imgui.PopItemWidth();
            //        Imgui.PopId();
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
            //                Imgui.PushId(cell);
            //                char label[32];
            //                static char text_buf[32] = "";
            //                sprintf(label, "Hello %d,%d", column, row);
            //                switch (contents_type)
            //                {
            //                case CT_ShortText:  Imgui.TextUnformatted(label); break;
            //                case CT_LongText:   Imgui.Text("Some %s text %d,%d\nOver two lines..", column == 0 ? "long" : "longeeer", column, row); break;
            //                case CT_ShowWidth:  Imgui.Text("W: %.1f", Imgui.GetContentRegionAvailable().Width); break;
            //                case CT_Button:     Imgui.Button(label); break;
            //                case CT_FillButton: Imgui.Button(label, ImVec2(-FLT_MIN, 0.0f)); break;
            //                case CT_InputText:  Imgui.SetNextItemWidth(-FLT_MIN); Imgui.InputText("##", text_buf, text_buf.Length); break;
            //                }
            //                Imgui.PopId();
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
            //        static ImGuiTableFlags flags = TableOptions.ScrollY | TableOptions.RowBg | TableOptions.BordersOuter | TableOptions.BordersV | TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.ScrollY", &flags, TableOptions.ScrollY);
            //        PopStyleCompact();
            //
            //        // When using ScrollX or ScrollY we need to specify a size for our table container!
            //        // Otherwise by default the table will fit all available space, like a BeginChild() call.
            //        ImVec2 outer_size = ImVec2(0.0f, TEXT_BASE_HEIGHT * 8);
            //        if (Imgui.BeginTable("table_scrolly", 3, flags, outer_size))
            //        {
            //            Imgui.TableSetupScrollFreeze(0, 1); // Make top row always visible
            //            Imgui.TableSetupColumn("One", TableColumnOptions.None);
            //            Imgui.TableSetupColumn("Two", TableColumnOptions.None);
            //            Imgui.TableSetupColumn("Three", TableColumnOptions.None);
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
            //            "When ScrollX is enabled, the default sizing policy becomes TableOptions.SizingFixedFit, "
            //            "as automatically stretching columns doesn't make much sense with horizontal scrolling.\n\n" +
            //            "Also note that as of the current version, you will almost always want to enable ScrollY along with ScrollX,"
            //            "because the container window won't automatically extend vertically to fix contents (this may be improved in future versions).");
            //        static ImGuiTableFlags flags = TableOptions.ScrollX | TableOptions.ScrollY | TableOptions.RowBg | TableOptions.BordersOuter | TableOptions.BordersV | TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable;
            //        static int freeze_cols = 1;
            //        static int freeze_rows = 1;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.Resizable", &flags, TableOptions.Resizable);
            //        Imgui.CheckboxFlags("TableOptions.ScrollX", &flags, TableOptions.ScrollX);
            //        Imgui.CheckboxFlags("TableOptions.ScrollY", &flags, TableOptions.ScrollY);
            //        Imgui.SetNextItemWidth(Imgui.GetFrameHeight());
            //        Imgui.Drag("freeze_cols", &freeze_cols, 0.2f, 0, 9, NULL, SliderOptions.NoInput);
            //        Imgui.SetNextItemWidth(Imgui.GetFrameHeight());
            //        Imgui.Drag("freeze_rows", &freeze_rows, 0.2f, 0, 9, NULL, SliderOptions.NoInput);
            //        PopStyleCompact();
            //
            //        // When using ScrollX or ScrollY we need to specify a size for our table container!
            //        // Otherwise by default the table will fit all available space, like a BeginChild() call.
            //        ImVec2 outer_size = ImVec2(0.0f, TEXT_BASE_HEIGHT * 8);
            //        if (Imgui.BeginTable("table_scrollx", 7, flags, outer_size))
            //        {
            //            Imgui.TableSetupScrollFreeze(freeze_cols, freeze_rows);
            //            Imgui.TableSetupColumn("Line #", TableColumnOptions.NoHide); // Make the first column not hideable to match our use of TableSetupScrollFreeze()
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
            //            "this is rather unusual and only makes sense when specifying an 'inner_width' for the table!\n" +
            //            "Without an explicit value, inner_width is == outer_size.X and therefore using Stretch columns + ScrollX together doesn't make sense.");
            //        static ImGuiTableFlags flags2 = TableOptions.SizingStretchSame | TableOptions.ScrollX | TableOptions.ScrollY | TableOptions.BordersOuter | TableOptions.RowBg | TableOptions.ContextMenuInBody;
            //        static float inner_width = 1000.0f;
            //        PushStyleCompact();
            //        Imgui.PushId("flags3");
            //        Imgui.PushItemWidth(TEXT_BASE_WIDTH * 30);
            //        Imgui.CheckboxFlags("TableOptions.ScrollX", &flags2, TableOptions.ScrollX);
            //        Imgui.Drag("inner_width", &inner_width, 1.0f, 0.0f, FLT_MAX, "%.1f");
            //        Imgui.PopItemWidth();
            //        Imgui.PopId();
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
            //        static ImGuiTableColumnFlags column_flags[column_count] = { TableColumnOptions.DefaultSort, TableColumnOptions.None, TableColumnOptions.DefaultHide };
            //        static ImGuiTableColumnFlags column_flags_out[column_count] = { 0, 0, 0 }; // Output from TableGetColumnFlags()
            //
            //        if (Imgui.BeginTable("table_columns_flags_checkboxes", column_count, TableOptions.None))
            //        {
            //            PushStyleCompact();
            //            for (int column = 0; column < column_count; column++)
            //            {
            //                Imgui.TableNextColumn();
            //                Imgui.PushId(column);
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
            //                Imgui.PopId();
            //            }
            //            PopStyleCompact();
            //            Imgui.EndTable();
            //        }
            //
            //        // Create the real table we care about for the example!
            //        // We use a scrolling table to be able to showcase the difference between the _IsEnabled and _IsVisible flags above, otherwise in
            //        // a non-scrolling table columns are always visible (unless using TableOptions.NoKeepColumnsVisible + resizing the parent window down)
            //        const ImGuiTableFlags flags
            //            = TableOptions.SizingFixedFit | TableOptions.ScrollX | TableOptions.ScrollY
            //            | TableOptions.RowBg | TableOptions.BordersOuter | TableOptions.BordersV
            //            | TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable | TableOptions.Sortable;
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
            //        static ImGuiTableFlags flags1 = TableOptions.Borders | TableOptions.NoBordersInBodyUntilResize;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.Resizable", &flags1, TableOptions.Resizable);
            //        Imgui.CheckboxFlags("TableOptions.NoBordersInBodyUntilResize", &flags1, TableOptions.NoBordersInBodyUntilResize);
            //        PopStyleCompact();
            //        if (Imgui.BeginTable("table1", 3, flags1))
            //        {
            //            // We could also set TableOptions.SizingFixedFit on the table and all columns will default to TableColumnOptions.WidthFixed.
            //            Imgui.TableSetupColumn("one", TableColumnOptions.WidthFixed, 100.0f); // Default to 100.0f
            //            Imgui.TableSetupColumn("two", TableColumnOptions.WidthFixed, 200.0f); // Default to 200.0f
            //            Imgui.TableSetupColumn("three", TableColumnOptions.WidthFixed);       // Default to auto
            //            Imgui.TableHeadersRow();
            //            for (int row = 0; row < 4; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 3; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    if (row == 0)
            //                        Imgui.Text("(w: %5.1f)", Imgui.GetContentRegionAvailable().Width);
            //                    else
            //                        Imgui.Text("Hello %d,%d", column, row);
            //                }
            //            }
            //            Imgui.EndTable();
            //        }
            //
            //        HelpMarker("Using TableSetupColumn() to setup explicit width.\n\nUnless _NoKeepColumnsVisible is set, fixed columns with set width may still be shrunk down if there's not enough space in the host.");
            //
            //        static ImGuiTableFlags flags2 = TableOptions.None;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.NoKeepColumnsVisible", &flags2, TableOptions.NoKeepColumnsVisible);
            //        Imgui.CheckboxFlags("TableOptions.BordersInnerV", &flags2, TableOptions.BordersInnerV);
            //        Imgui.CheckboxFlags("TableOptions.BordersOuterV", &flags2, TableOptions.BordersOuterV);
            //        PopStyleCompact();
            //        if (Imgui.BeginTable("table2", 4, flags2))
            //        {
            //            // We could also set TableOptions.SizingFixedFit on the table and all columns will default to TableColumnOptions.WidthFixed.
            //            Imgui.TableSetupColumn("", TableColumnOptions.WidthFixed, 100.0f);
            //            Imgui.TableSetupColumn("", TableColumnOptions.WidthFixed, TEXT_BASE_WIDTH * 15.0f);
            //            Imgui.TableSetupColumn("", TableColumnOptions.WidthFixed, TEXT_BASE_WIDTH * 30.0f);
            //            Imgui.TableSetupColumn("", TableColumnOptions.WidthFixed, TEXT_BASE_WIDTH * 15.0f);
            //            for (int row = 0; row < 5; row++)
            //            {
            //                Imgui.TableNextRow();
            //                for (int column = 0; column < 4; column++)
            //                {
            //                    Imgui.TableSetColumnIndex(column);
            //                    if (row == 0)
            //                        Imgui.Text("(w: %5.1f)", Imgui.GetContentRegionAvailable().Width);
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
            //        if (Imgui.BeginTable("table_nested1", 2, TableOptions.Borders | TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable))
            //        {
            //            Imgui.TableSetupColumn("A0");
            //            Imgui.TableSetupColumn("A1");
            //            Imgui.TableHeadersRow();
            //
            //            Imgui.TableNextColumn();
            //            Imgui.Text("A0 Row 0");
            //            {
            //                float rows_height = TEXT_BASE_HEIGHT * 2;
            //                if (Imgui.BeginTable("table_nested2", 2, TableOptions.Borders | TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable))
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
            //        HelpMarker("You can pass a 'min_row_height' to TableNextRow().\n\nRows are padded with 'style.CellPadding.Y' on top and bottom, so effectively the minimum row height will always be >= 'style.CellPadding.Y * 2.0f'.\n\nWe cannot honor a _maximum_ row height as that would require a unique clipping rectangle per row.");
            //        if (Imgui.BeginTable("table_row_height", 1, TableOptions.BordersOuter | TableOptions.BordersInnerV))
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
            //        // Showcasing use of TableOptions.NoHostExtendX and TableOptions.NoHostExtendY
            //        // Important to that note how the two flags have slightly different behaviors!
            //        Imgui.Text("Using NoHostExtendX and NoHostExtendY:");
            //        PushStyleCompact();
            //        static ImGuiTableFlags flags = TableOptions.Borders | TableOptions.Resizable | TableOptions.ContextMenuInBody | TableOptions.RowBg | TableOptions.SizingFixedFit | TableOptions.NoHostExtendX;
            //        Imgui.CheckboxFlags("TableOptions.NoHostExtendX", &flags, TableOptions.NoHostExtendX);
            //        Imgui.SameLine(); HelpMarker("Make outer width auto-fit to columns, overriding outer_size.X value.\n\nOnly available when ScrollX/ScrollY are disabled and Stretch columns are not used.");
            //        Imgui.CheckboxFlags("TableOptions.NoHostExtendY", &flags, TableOptions.NoHostExtendY);
            //        Imgui.SameLine(); HelpMarker("Make outer height stop exactly at outer_size.Y (prevent auto-extending table past the limit).\n\nOnly available when ScrollX/ScrollY are disabled. Data below the limit will be clipped and not visible.");
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
            //        if (Imgui.BeginTable("table2", 3, TableOptions.Borders | TableOptions.RowBg, ImVec2(TEXT_BASE_WIDTH * 30, 0.0f)))
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
            //        if (Imgui.BeginTable("table3", 3, TableOptions.Borders | TableOptions.RowBg, ImVec2(TEXT_BASE_WIDTH * 30, 0.0f)))
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
            //        static ImGuiTableFlags flags = TableOptions.RowBg;
            //        static int row_bg_type = 1;
            //        static int row_bg_target = 1;
            //        static int cell_bg_type = 1;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.Borders", &flags, TableOptions.Borders);
            //        Imgui.CheckboxFlags("TableOptions.RowBg", &flags, TableOptions.RowBg);
            //        Imgui.SameLine(); HelpMarker("TableOptions.RowBg automatically sets RowBg0 to alternative colors pulled from the Style.");
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
            //                // We use a transparent color so we can see the one behind in case our target is RowBg1 and RowBg0 was already targeted by the TableOptions.RowBg flag.
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
            //        static ImGuiTableFlags flags = TableOptions.BordersV | TableOptions.BordersOuterH | TableOptions.Resizable | TableOptions.RowBg | TableOptions.NoBordersInBody;
            //
            //        if (Imgui.BeginTable("3ways", 3, flags))
            //        {
            //            // The first column will use the default _WidthStretch when ScrollX is Off and _WidthFixed when ScrollX is On
            //            Imgui.TableSetupColumn("Name", TableColumnOptions.NoHide);
            //            Imgui.TableSetupColumn("Size", TableColumnOptions.WidthFixed, TEXT_BASE_WIDTH * 12.0f);
            //            Imgui.TableSetupColumn("Type", TableColumnOptions.WidthFixed, TEXT_BASE_WIDTH * 18.0f);
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
            //            "Showcase using PushItemWidth() and how it is preserved on a per-column basis.\n\n" +
            //            "Note that on auto-resizing non-resizable fixed columns, querying the content width for e.g. right-alignment doesn't make sense.");
            //        if (Imgui.BeginTable("table_item_width", 3, TableOptions.Borders))
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
            //                    Imgui.PushItemWidth(-Imgui.GetContentRegionAvailable().Width * 0.5f);
            //                    Imgui.TableSetColumnIndex(2);
            //                    Imgui.PushItemWidth(-FLT_MIN); // Right-aligned
            //                }
            //
            //                // Draw our contents
            //                static float dummy_f = 0.0f;
            //                Imgui.PushId(row);
            //                Imgui.TableSetColumnIndex(0);
            //                Imgui.Slider("float0", &dummy_f, 0.0f, 1.0f);
            //                Imgui.TableSetColumnIndex(1);
            //                Imgui.Slider("float1", &dummy_f, 0.0f, 1.0f);
            //                Imgui.TableSetColumnIndex(2);
            //                Imgui.Slider("##float2", &dummy_f, 0.0f, 1.0f); // No visible label since right-aligned
            //                Imgui.PopId();
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
            //        if (Imgui.BeginTable("table_custom_headers", COLUMNS_COUNT, TableOptions.Borders | TableOptions.Reorderable | TableOptions.Hideable))
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
            //                Imgui.PushId(column);
            //                Imgui.PushStyleVariable(StyleVariable.FramePadding, ImVec2(0, 0));
            //                Imgui.Checkbox("##checkall", &column_selected[column]);
            //                Imgui.PopStyleVariable();
            //                Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.X);
            //                Imgui.TableHeader(column_name);
            //                Imgui.PopId();
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
            //        HelpMarker("By default, right-clicking over a TableHeadersRow()/TableHeader() line will open the default context-menu.\nUsing TableOptions.ContextMenuInBody we also allow right-clicking over columns body.");
            //        static ImGuiTableFlags flags1 = TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable | TableOptions.Borders | TableOptions.ContextMenuInBody;
            //
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.ContextMenuInBody", &flags1, TableOptions.ContextMenuInBody);
            //        PopStyleCompact();
            //
            //        // Context Menus: first example
            //        // [1.1] Right-click on the TableHeadersRow() line to open the default table context menu.
            //        // [1.2] Right-click in columns also open the default table context menu (if TableOptions.ContextMenuInBody is set)
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
            //        ImGuiTableFlags flags2 = TableOptions.Resizable | TableOptions.SizingFixedFit | TableOptions.Reorderable | TableOptions.Hideable | TableOptions.Borders;
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
            //                    Imgui.PushId(row * COLUMNS_COUNT + column);
            //                    Imgui.SmallButton("..");
            //                    if (Imgui.BeginPopupContextItem())
            //                    {
            //                        Imgui.Text("This is the popup for Button(\"..\") in Cell %d,%d", column, row);
            //                        if (Imgui.Button("Close"))
            //                            Imgui.CloseCurrentPopup();
            //                        Imgui.EndPopup();
            //                    }
            //                    Imgui.PopId();
            //                }
            //            }
            //
            //            // [2.3] Right-click anywhere in columns to open another custom popup
            //            // (instead of testing for !IsAnyItemHovered() we could also call OpenPopup() with PopupOptions.NoOpenOverExistingPopup
            //            // to manage popup priority as the popups triggers, here "are we hovering a column" are overlapping)
            //            int hovered_column = -1;
            //            for (int column = 0; column < COLUMNS_COUNT + 1; column++)
            //            {
            //                Imgui.PushId(column);
            //                if (Imgui.TableGetColumnFlags(column) & TableColumnOptions.IsHovered)
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
            //                Imgui.PopId();
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
            //        static ImGuiTableFlags flags = TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable | TableOptions.Borders | TableOptions.SizingFixedFit | TableOptions.NoSavedSettings;
            //        Imgui.CheckboxFlags("TableOptions.ScrollY", &flags, TableOptions.ScrollY);
            //        Imgui.CheckboxFlags("TableOptions.SizingFixedFit", &flags, TableOptions.SizingFixedFit);
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
            //                const int template_n = n % template_items_names.Length;
            //                MyItem& item = items[n];
            //                item.ID = n;
            //                item.Name = template_items_names[template_n];
            //                item.Quantity = (n * n - n) % 20; // Assign default quantities
            //            }
            //        }
            //
            //        // Options
            //        static ImGuiTableFlags flags =
            //            TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable | TableOptions.Sortable | TableOptions.SortMulti
            //            | TableOptions.RowBg | TableOptions.BordersOuter | TableOptions.BordersV | TableOptions.NoBordersInBody
            //            | TableOptions.ScrollY;
            //        PushStyleCompact();
            //        Imgui.CheckboxFlags("TableOptions.SortMulti", &flags, TableOptions.SortMulti);
            //        Imgui.SameLine(); HelpMarker("When sorting is enabled: hold shift when clicking headers to sort on multiple column. TableGetSortSpecs() may return specs where (SpecsCount > 1).");
            //        Imgui.CheckboxFlags("TableOptions.SortTristate", &flags, TableOptions.SortTristate);
            //        Imgui.SameLine(); HelpMarker("When sorting is enabled: allow no sorting, disable default sorting. TableGetSortSpecs() may return specs where (SpecsCount == 0).");
            //        PopStyleCompact();
            //
            //        if (Imgui.BeginTable("table_sorting", 4, flags, ImVec2(0.0f, TEXT_BASE_HEIGHT * 15), 0.0f))
            //        {
            //            // Declare columns
            //            // We use the "user_id" parameter of TableSetupColumn() to specify a user id that will be stored in the sort specifications.
            //            // This is so our sort function can identify a column given our own identifier. We could also identify them based on their index!
            //            // Demonstrate using a mixture of flags among available sort-related flags:
            //            // - TableColumnOptions.DefaultSort
            //            // - TableColumnOptions.NoSort / TableColumnOptions.NoSortAscending / TableColumnOptions.NoSortDescending
            //            // - TableColumnOptions.PreferSortAscending / TableColumnOptions.PreferSortDescending
            //            Imgui.TableSetupColumn("ID",       TableColumnOptions.DefaultSort          | TableColumnOptions.WidthFixed,   0.0f, MyItemColumnID_ID);
            //            Imgui.TableSetupColumn("Name",                                                  TableColumnOptions.WidthFixed,   0.0f, MyItemColumnID_Name);
            //            Imgui.TableSetupColumn("Action",   TableColumnOptions.NoSort               | TableColumnOptions.WidthFixed,   0.0f, MyItemColumnID_Action);
            //            Imgui.TableSetupColumn("Quantity", TableColumnOptions.PreferSortDescending | TableColumnOptions.WidthStretch, 0.0f, MyItemColumnID_Quantity);
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
            //                    Imgui.PushId(item->ID);
            //                    Imgui.TableNextRow();
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("%04d", item->ID);
            //                    Imgui.TableNextColumn();
            //                    Imgui.TextUnformatted(item->Name);
            //                    Imgui.TableNextColumn();
            //                    Imgui.SmallButton("None");
            //                    Imgui.TableNextColumn();
            //                    Imgui.Text("%d", item->Quantity);
            //                    Imgui.PopId();
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
            //            TableOptions.Resizable | TableOptions.Reorderable | TableOptions.Hideable
            //            | TableOptions.Sortable | TableOptions.SortMulti
            //            | TableOptions.RowBg | TableOptions.Borders | TableOptions.NoBordersInBody
            //            | TableOptions.ScrollX | TableOptions.ScrollY
            //            | TableOptions.SizingFixedFit;
            //
            //        enum ContentsType { CT_Text, CT_Button, CT_SmallButton, CT_FillButton, CT_Selectable, CT_SelectableSpanRow };
            //        static int contents_type = CT_SelectableSpanRow;
            //        const char* contents_type_names[] = { "Text", "Button", "SmallButton", "FillButton", "Selectable", "Selectable (span row)" };
            //        static int freeze_cols = 1;
            //        static int freeze_rows = 1;
            //        static int items_count = template_items_names.Length * 2;
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
            //                Imgui.CheckboxFlags("TableOptions.Resizable", &flags, TableOptions.Resizable);
            //                Imgui.CheckboxFlags("TableOptions.Reorderable", &flags, TableOptions.Reorderable);
            //                Imgui.CheckboxFlags("TableOptions.Hideable", &flags, TableOptions.Hideable);
            //                Imgui.CheckboxFlags("TableOptions.Sortable", &flags, TableOptions.Sortable);
            //                Imgui.CheckboxFlags("TableOptions.NoSavedSettings", &flags, TableOptions.NoSavedSettings);
            //                Imgui.CheckboxFlags("TableOptions.ContextMenuInBody", &flags, TableOptions.ContextMenuInBody);
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Decorations:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.CheckboxFlags("TableOptions.RowBg", &flags, TableOptions.RowBg);
            //                Imgui.CheckboxFlags("TableOptions.BordersV", &flags, TableOptions.BordersV);
            //                Imgui.CheckboxFlags("TableOptions.BordersOuterV", &flags, TableOptions.BordersOuterV);
            //                Imgui.CheckboxFlags("TableOptions.BordersInnerV", &flags, TableOptions.BordersInnerV);
            //                Imgui.CheckboxFlags("TableOptions.BordersH", &flags, TableOptions.BordersH);
            //                Imgui.CheckboxFlags("TableOptions.BordersOuterH", &flags, TableOptions.BordersOuterH);
            //                Imgui.CheckboxFlags("TableOptions.BordersInnerH", &flags, TableOptions.BordersInnerH);
            //                Imgui.CheckboxFlags("TableOptions.NoBordersInBody", &flags, TableOptions.NoBordersInBody); Imgui.SameLine(); HelpMarker("Disable vertical borders in columns Body (borders will always appear in Headers");
            //                Imgui.CheckboxFlags("TableOptions.NoBordersInBodyUntilResize", &flags, TableOptions.NoBordersInBodyUntilResize); Imgui.SameLine(); HelpMarker("Disable vertical borders in columns Body until hovered for resize (borders will always appear in Headers)");
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Sizing:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                EditTableSizingFlags(&flags);
            //                Imgui.SameLine(); HelpMarker("In the Advanced demo we override the policy of each column so those table-wide settings have less effect that typical.");
            //                Imgui.CheckboxFlags("TableOptions.NoHostExtendX", &flags, TableOptions.NoHostExtendX);
            //                Imgui.SameLine(); HelpMarker("Make outer width auto-fit to columns, overriding outer_size.X value.\n\nOnly available when ScrollX/ScrollY are disabled and Stretch columns are not used.");
            //                Imgui.CheckboxFlags("TableOptions.NoHostExtendY", &flags, TableOptions.NoHostExtendY);
            //                Imgui.SameLine(); HelpMarker("Make outer height stop exactly at outer_size.Y (prevent auto-extending table past the limit).\n\nOnly available when ScrollX/ScrollY are disabled. Data below the limit will be clipped and not visible.");
            //                Imgui.CheckboxFlags("TableOptions.NoKeepColumnsVisible", &flags, TableOptions.NoKeepColumnsVisible);
            //                Imgui.SameLine(); HelpMarker("Only available if ScrollX is disabled.");
            //                Imgui.CheckboxFlags("TableOptions.PreciseWidths", &flags, TableOptions.PreciseWidths);
            //                Imgui.SameLine(); HelpMarker("Disable distributing remainder width to stretched columns (width allocation on a 100-wide table with 3 columns: Without this flag: 33,33,34. With this flag: 33,33,33). With larger number of columns, resizing will appear to be less smooth.");
            //                Imgui.CheckboxFlags("TableOptions.NoClip", &flags, TableOptions.NoClip);
            //                Imgui.SameLine(); HelpMarker("Disable clipping rectangle for every individual columns (reduce draw command count, items will be able to overflow into other columns). Generally incompatible with ScrollFreeze options.");
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Padding:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.CheckboxFlags("TableOptions.PadOuterX", &flags, TableOptions.PadOuterX);
            //                Imgui.CheckboxFlags("TableOptions.NoPadOuterX", &flags, TableOptions.NoPadOuterX);
            //                Imgui.CheckboxFlags("TableOptions.NoPadInnerX", &flags, TableOptions.NoPadInnerX);
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Scrolling:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.CheckboxFlags("TableOptions.ScrollX", &flags, TableOptions.ScrollX);
            //                Imgui.SameLine();
            //                Imgui.SetNextItemWidth(Imgui.GetFrameHeight());
            //                Imgui.Drag("freeze_cols", &freeze_cols, 0.2f, 0, 9, NULL, SliderOptions.NoInput);
            //                Imgui.CheckboxFlags("TableOptions.ScrollY", &flags, TableOptions.ScrollY);
            //                Imgui.SameLine();
            //                Imgui.SetNextItemWidth(Imgui.GetFrameHeight());
            //                Imgui.Drag("freeze_rows", &freeze_rows, 0.2f, 0, 9, NULL, SliderOptions.NoInput);
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Sorting:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.CheckboxFlags("TableOptions.SortMulti", &flags, TableOptions.SortMulti);
            //                Imgui.SameLine(); HelpMarker("When sorting is enabled: hold shift when clicking headers to sort on multiple column. TableGetSortSpecs() may return specs where (SpecsCount > 1).");
            //                Imgui.CheckboxFlags("TableOptions.SortTristate", &flags, TableOptions.SortTristate);
            //                Imgui.SameLine(); HelpMarker("When sorting is enabled: allow no sorting, disable default sorting. TableGetSortSpecs() may return specs where (SpecsCount == 0).");
            //                Imgui.TreePop();
            //            }
            //
            //            if (Imgui.TreeNodeEx("Other:", ImGuiTreeNodeFlags_DefaultOpen))
            //            {
            //                Imgui.Checkbox("show_headers", &show_headers);
            //                Imgui.Checkbox("show_wrapped_text", &show_wrapped_text);
            //
            //                Imgui.DragFloat2("##OuterSize", &outer_size_value.X);
            //                Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.X);
            //                Imgui.Checkbox("outer_size", &outer_size_enabled);
            //                Imgui.SameLine();
            //                HelpMarker("If scrolling is disabled (ScrollX and ScrollY not set):\n" +
            //                    "- The table is output directly in the parent window.\n" +
            //                    "- OuterSize.X < 0.0f will right-align the table.\n" +
            //                    "- OuterSize.X = 0.0f will narrow fit the table unless there are any Stretch columns.\n" +
            //                    "- OuterSize.Y then becomes the minimum size for the table, which will extend vertically if there are more rows (unless NoHostExtendY is set).");
            //
            //                // From a user point of view we will tend to use 'inner_width' differently depending on whether our table is embedding scrolling.
            //                // To facilitate toying with this demo we will actually pass 0.0f to the BeginTable() when ScrollX is disabled.
            //                Imgui.Drag("inner_width (when ScrollX active)", &inner_width_with_scroll, 1.0f, 0.0f, FLT_MAX);
            //
            //                Imgui.Drag("row_min_height", &row_min_height, 1.0f, 0.0f, FLT_MAX);
            //                Imgui.SameLine(); HelpMarker("Specify height of the Selectable item.");
            //
            //                Imgui.Drag("items_count", &items_count, 0.1f, 0, 9999);
            //                Imgui.Combo("items_type (first column)", &contents_type, contents_type_names, contents_type_names.Length);
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
            //                const int template_n = n % template_items_names.Length;
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
            //        const float inner_width_to_use = (flags & TableOptions.ScrollX) ? inner_width_with_scroll : 0.0f;
            //        if (Imgui.BeginTable("table_advanced", 6, flags, outer_size_enabled ? outer_size_value : ImVec2(0, 0), inner_width_to_use))
            //        {
            //            // Declare columns
            //            // We use the "user_id" parameter of TableSetupColumn() to specify a user id that will be stored in the sort specifications.
            //            // This is so our sort function can identify a column given our own identifier. We could also identify them based on their index!
            //            Imgui.TableSetupColumn("ID",           TableColumnOptions.DefaultSort | TableColumnOptions.WidthFixed | TableColumnOptions.NoHide, 0.0f, MyItemColumnID_ID);
            //            Imgui.TableSetupColumn("Name",         TableColumnOptions.WidthFixed, 0.0f, MyItemColumnID_Name);
            //            Imgui.TableSetupColumn("Action",       TableColumnOptions.NoSort | TableColumnOptions.WidthFixed, 0.0f, MyItemColumnID_Action);
            //            Imgui.TableSetupColumn("Quantity",     TableColumnOptions.PreferSortDescending, 0.0f, MyItemColumnID_Quantity);
            //            Imgui.TableSetupColumn("Description",  (flags & TableOptions.NoHostExtendX) ? 0 : TableColumnOptions.WidthStretch, 0.0f, MyItemColumnID_Description);
            //            Imgui.TableSetupColumn("Hidden",       TableColumnOptions.DefaultHide | TableColumnOptions.NoSort);
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
            //            const bool sorts_specs_using_quantity = (Imgui.TableGetColumnFlags(3) & TableColumnOptions.IsSorted) != 0;
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
            //                    Imgui.PushId(item->ID);
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
            //                            if (Imgui.GetIo().KeyCtrl)
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
            //                    Imgui.PopId();
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
            //                    table_draw_list_draw_cmd_count - 1, table_scroll_cur.X, table_scroll_max.X, table_scroll_cur.Y, table_scroll_max.Y);
            //        }
            //        Imgui.TreePop();
            //    }

            Imgui.PopId();

            ShowDemoWindowColumns();

            if (s_disableIndent)
            {
                Imgui.PopStyleVariable();
            }
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
            //        Imgui.Drag("##columns_count", &columns_count, 0.1f, 2, 10, "%d columns");
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
            //            Imgui.Text("Avail %.2f", Imgui.GetContentRegionAvailable().Width);
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
            //        Imgui.BeginChild("##ScrollingRegion", child_size, false, WindowOptions.HorizontalScrollbar);
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
            //        ImGuiIO& io = Imgui.GetIo();
            //
            //        // Display inputs submitted to ImGuiIO
            //        IMGUI_DEMO_MARKER("Inputs & Focus/Inputs");
            //        Imgui.SetNextItemOpen(true, ImGuiCond_Once);
            //        if (Imgui.TreeNode("Inputs"))
            //        {
            //            HelpMarker(
            //                "This is a simplified view. See more detailed input state:\n" +
            //                "- in 'Tools->Metrics/Debugger->Inputs'.\n" +
            //                "- in 'Tools->Debug Log->IO'.");
            //            if (Imgui.IsMousePosValid())
            //                Imgui.Text("Mouse pos: (%g, %g)", io.MousePos.X, io.MousePos.Y);
            //            else
            //                Imgui.Text("Mouse pos: <INVALID>");
            //            Imgui.Text("Mouse delta: (%g, %g)", io.MouseDelta.X, io.MouseDelta.Y);
            //            Imgui.Text("Mouse down:");
            //            for (int i = 0; i < IM_ARRAYSIZE(io.MouseDown); i++) if (Imgui.IsMouseDown(i)) { Imgui.SameLine(); Imgui.Text("b%d (%.02f secs)", i, io.MouseDownDuration[i]); }
            //            Imgui.Text("Mouse wheel: %.1f", io.MouseWheel);
            //
            //            // We iterate both legacy native range and named ImGuiKey ranges, which is a little odd but this allows displaying the data for old/new backends.
            //            // User code should never have to go through such hoops: old code may use native keycodes, new code may use ImGuiKey codes.
            //#ifdef IMGUI_DISABLE_OBSOLETE_KEYIO
            //            struct funcs { static bool IsLegacyNativeDupe(ImGuiKey) { return false; } };
            //#else
            //            struct funcs { static bool IsLegacyNativeDupe(ImGuiKey key) { return key < 512 && Imgui.GetIo().KeyMap[key] != -1; } }; // Hide Native<>ImGuiKey duplicates when both exists in the array
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
            //                "Dear ImGui wants the corresponding inputs and we expect the underlying application to ignore them.\n\n" +
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
            //                    "Hovering the colored canvas will override io.WantCaptureXXX fields.\n" +
            //                    "Notice how normally (when set to none), the value of io.WantCaptureKeyboard would be false when hovering and true when clicking.");
            //                static int capture_override_mouse = -1;
            //                static int capture_override_keyboard = -1;
            //                const char* capture_override_desc[] = { "None", "Set to false", "Set to true" };
            //                Imgui.SetNextItemWidth(Imgui.GetFontSize() * 15);
            //                Imgui.Slider("SetNextFrameWantCaptureMouse() on hover", &capture_override_mouse, -1, +1, capture_override_desc[capture_override_mouse + 1], SliderOptions.AlwaysClamp);
            //                Imgui.SetNextItemWidth(Imgui.GetFontSize() * 15);
            //                Imgui.Slider("SetNextFrameWantCaptureKeyboard() on hover", &capture_override_keyboard, -1, +1, capture_override_desc[capture_override_keyboard + 1], SliderOptions.AlwaysClamp);
            //
            //                Imgui.ColorButton("##panel", ImVec4(0.7f, 0.1f, 0.7f, 1.0f), ColorEditOptions.NoTooltip | ColorEditOptions.NoDragDrop, ImVec2(128.0f, 96.0f)); // Dummy item
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
            //            IM_ASSERT(mouse_cursors_names.Length == ImGuiMouseCursor_COUNT);
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
            //            Imgui.InputText("1", buf, buf.Length);
            //            Imgui.InputText("2", buf, buf.Length);
            //            Imgui.InputText("3", buf, buf.Length);
            //            Imgui.PushAllowKeyboardFocus(false);
            //            Imgui.InputText("4 (tab skip)", buf, buf.Length);
            //            Imgui.SameLine(); HelpMarker("Item won't be cycled through when using TAB or Shift+Tab.");
            //            Imgui.PopAllowKeyboardFocus();
            //            Imgui.InputText("5", buf, buf.Length);
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
            //            Imgui.InputText("1", buf, buf.Length);
            //            if (Imgui.IsItemActive()) has_focus = 1;
            //
            //            if (focus_2) Imgui.SetKeyboardFocusHere();
            //            Imgui.InputText("2", buf, buf.Length);
            //            if (Imgui.IsItemActive()) has_focus = 2;
            //
            //            Imgui.PushAllowKeyboardFocus(false);
            //            if (focus_3) Imgui.SetKeyboardFocusHere();
            //            Imgui.InputText("3 (tab skip)", buf, buf.Length);
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
            //            Imgui.Slider("Float3", &f3[0], 0.0f, 1.0f);
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
            //                Imgui.GetForegroundDrawList()->AddLine(io.MouseClickedPos[0], io.MousePos, Imgui.GetColorU32(StyleColor.Button), 4.0f); // Draw a line between the button and the mouse cursor
            //
            //            // Drag operations gets "unlocked" when the mouse has moved past a certain threshold
            //            // (the default threshold is stored in io.MouseDragThreshold). You can request a lower or higher
            //            // threshold using the second parameter of IsMouseDragging() and GetMouseDragDelta().
            //            ImVec2 value_raw = Imgui.GetMouseDragDelta(0, 0.0f);
            //            ImVec2 value_with_lock_threshold = Imgui.GetMouseDragDelta(0);
            //            ImVec2 mouse_delta = io.MouseDelta;
            //            Imgui.Text("GetMouseDragDelta(0):");
            //            Imgui.Text("  w/ default threshold: (%.1f, %.1f)", value_with_lock_threshold.X, value_with_lock_threshold.Y);
            //            Imgui.Text("  w/ zero threshold: (%.1f, %.1f)", value_raw.X, value_raw.Y);
            //            Imgui.Text("io.MouseDelta: (%.1f, %.1f)", mouse_delta.X, mouse_delta.Y);
            //            Imgui.TreePop();
            //        }
            //    }
        }

        public static void ShowAboutWindow(State<bool> p_open)
        {
            //    if (!Imgui.Begin("About Dear ImGui", p_open, WindowOptions.AlwaysAutoResize))
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
            //        ImGuiIO& io = Imgui.GetIo();
            //        ImGuiStyle& style = Imgui.GetStyle();
            //
            //        bool copy_to_clipboard = Imgui.Button("Copy to clipboard");
            //        ImVec2 child_size = ImVec2(0, Imgui.GetTextLineHeightWithSpacing() * 18);
            //        Imgui.BeginChildFrame(Imgui.GetId("cfg_infos"), child_size, WindowOptions.NoMove);
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
            //        Imgui.Text("io.DisplaySize: %.2f,%.2f", io.DisplaySize.X, io.DisplaySize.Y);
            //        Imgui.Text("io.DisplayFramebufferScale: %.2f,%.2f", io.DisplayFramebufferScale.X, io.DisplayFramebufferScale.Y);
            //        Imgui.Separator();
            //        Imgui.Text("style.WindowPadding: %.2f,%.2f", style.WindowPadding.X, style.WindowPadding.Y);
            //        Imgui.Text("style.WindowBorderSize: %.2f", style.WindowBorderSize);
            //        Imgui.Text("style.FramePadding: %.2f,%.2f", style.FramePadding.X, style.FramePadding.Y);
            //        Imgui.Text("style.FrameRounding: %.2f", style.FrameRounding);
            //        Imgui.Text("style.FrameBorderSize: %.2f", style.FrameBorderSize);
            //        Imgui.Text("style.ItemSpacing: %.2f,%.2f", style.ItemSpacing.X, style.ItemSpacing.Y);
            //        Imgui.Text("style.ItemInnerSpacing: %.2f,%.2f", style.ItemInnerSpacing.X, style.ItemInnerSpacing.Y);
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
        //    ImGuiIO& io = Imgui.GetIo();
        //    ImFont* font_current = Imgui.GetFont();
        //    if (Imgui.BeginCombo(label, font_current->GetDebugName()))
        //    {
        //        for (int n = 0; n < io.Fonts->Fonts.Size; n++)
        //        {
        //            ImFont* font = io.Fonts->Fonts[n];
        //            Imgui.PushId((void*)font);
        //            if (Imgui.Selectable(font->GetDebugName(), font == font_current))
        //                io.FontDefault = font;
        //            Imgui.PopId();
        //        }
        //        Imgui.EndCombo();
        //    }
        //    Imgui.SameLine();
        //    HelpMarker(
        //        "- Load additional fonts with io.Fonts->AddFontFromFileTTF().\n" +
        //        "- The font atlas is built when calling io.Fonts->GetTexDataAsXXXX() or io.Fonts->Build().\n" +
        //        "- Read FAQ and docs/FONTS.md for more details.\n" +
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
            //    if (Imgui.Slider("FrameRounding", &style.FrameRounding, 0.0f, 12.0f, "%.0f"))
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
            //            Imgui.Slider("WindowPadding", (float*)&style.WindowPadding, 0.0f, 20.0f, "%.0f");
            //            Imgui.Slider("FramePadding", (float*)&style.FramePadding, 0.0f, 20.0f, "%.0f");
            //            Imgui.Slider("CellPadding", (float*)&style.CellPadding, 0.0f, 20.0f, "%.0f");
            //            Imgui.Slider("ItemSpacing", (float*)&style.ItemSpacing, 0.0f, 20.0f, "%.0f");
            //            Imgui.Slider("ItemInnerSpacing", (float*)&style.ItemInnerSpacing, 0.0f, 20.0f, "%.0f");
            //            Imgui.Slider("TouchExtraPadding", (float*)&style.TouchExtraPadding, 0.0f, 10.0f, "%.0f");
            //            Imgui.Slider("IndentSpacing", &style.IndentSpacing, 0.0f, 30.0f, "%.0f");
            //            Imgui.Slider("ScrollbarSize", &style.ScrollbarSize, 1.0f, 20.0f, "%.0f");
            //            Imgui.Slider("GrabMinSize", &style.GrabMinSize, 1.0f, 20.0f, "%.0f");
            //            Imgui.Text("Borders");
            //            Imgui.Slider("WindowBorderSize", &style.WindowBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.Slider("ChildBorderSize", &style.ChildBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.Slider("PopupBorderSize", &style.PopupBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.Slider("FrameBorderSize", &style.FrameBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.Slider("TabBorderSize", &style.TabBorderSize, 0.0f, 1.0f, "%.0f");
            //            Imgui.Text("Rounding");
            //            Imgui.Slider("WindowRounding", &style.WindowRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.Slider("ChildRounding", &style.ChildRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.Slider("FrameRounding", &style.FrameRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.Slider("PopupRounding", &style.PopupRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.Slider("ScrollbarRounding", &style.ScrollbarRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.Slider("GrabRounding", &style.GrabRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.Slider("LogSliderDeadzone", &style.LogSliderDeadzone, 0.0f, 12.0f, "%.0f");
            //            Imgui.Slider("TabRounding", &style.TabRounding, 0.0f, 12.0f, "%.0f");
            //            Imgui.Text("Alignment");
            //            Imgui.Slider("WindowTitleAlign", (float*)&style.WindowTitleAlign, 0.0f, 1.0f, "%.2f");
            //            int window_menu_button_position = style.WindowMenuButtonPosition + 1;
            //            if (Imgui.Combo("WindowMenuButtonPosition", (int*)&window_menu_button_position, "None\0Left\0Right\0"))
            //                style.WindowMenuButtonPosition = window_menu_button_position - 1;
            //            Imgui.Combo("ColorButtonPosition", (int*)&style.ColorButtonPosition, "Left\0Right\0");
            //            Imgui.Slider("ButtonTextAlign", (float*)&style.ButtonTextAlign, 0.0f, 1.0f, "%.2f");
            //            Imgui.SameLine(); HelpMarker("Alignment applies when a button is larger than its text content.");
            //            Imgui.Slider("SelectableTextAlign", (float*)&style.SelectableTextAlign, 0.0f, 1.0f, "%.2f");
            //            Imgui.SameLine(); HelpMarker("Alignment applies when a selectable is larger than its text content.");
            //            Imgui.Text("Safe Area Padding");
            //            Imgui.SameLine(); HelpMarker("Adjust if you cannot see the edges of your screen (e.g. on a TV where scaling has not been configured).");
            //            Imgui.Slider("DisplaySafeAreaPadding", (float*)&style.DisplaySafeAreaPadding, 0.0f, 30.0f, "%.0f");
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
            //                            name, 23 - (int)strlen(name), "", col.X, col.Y, col.z, col.w);
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
            //            if (Imgui.RadioButton("Opaque", alpha_flags == ColorEditOptions.None))             { alpha_flags = ColorEditOptions.None; } Imgui.SameLine();
            //            if (Imgui.RadioButton("Alpha",  alpha_flags == ColorEditOptions.AlphaPreview))     { alpha_flags = ColorEditOptions.AlphaPreview; } Imgui.SameLine();
            //            if (Imgui.RadioButton("Both",   alpha_flags == ColorEditOptions.AlphaPreviewHalf)) { alpha_flags = ColorEditOptions.AlphaPreviewHalf; } Imgui.SameLine();
            //            HelpMarker(
            //                "In the color list:\n" +
            //                "Left-click on color square to open color picker,\n" +
            //                "Right-click to open edit options menu.");
            //
            //            Imgui.BeginChild("##colors", ImVec2(0, 0), true, WindowOptions.AlwaysVerticalScrollbar | WindowOptions.AlwaysHorizontalScrollbar | WindowOptions.NavFlattened);
            //            Imgui.PushItemWidth(-160);
            //            for (int i = 0; i < ImGuiCol_COUNT; i++)
            //            {
            //                const char* name = Imgui.GetStyleColorName(i);
            //                if (!filter.PassFilter(name))
            //                    continue;
            //                Imgui.PushId(i);
            //                Imgui.ColorEdit4("##color", (float*)&style.Colors[i], ColorEditOptions.AlphaBar | alpha_flags);
            //                if (memcmp(&style.Colors[i], &ref->Colors[i], sizeof(ImVec4)) != 0)
            //                {
            //                    // Tips: in a real user application, you may want to merge and use an icon font into the main font,
            //                    // so instead of "Save"/"Revert" you'd use icons!
            //                    // Read the FAQ and docs/FONTS.md about using icon fonts. It's really easy and super convenient!
            //                    Imgui.SameLine(0.0f, style.ItemInnerSpacing.X); if (Imgui.Button("Save")) { ref->Colors[i] = style.Colors[i]; }
            //                    Imgui.SameLine(0.0f, style.ItemInnerSpacing.X); if (Imgui.Button("Revert")) { style.Colors[i] = ref->Colors[i]; }
            //                }
            //                Imgui.SameLine(0.0f, style.ItemInnerSpacing.X);
            //                Imgui.TextUnformatted(name);
            //                Imgui.PopId();
            //            }
            //            Imgui.PopItemWidth();
            //            Imgui.EndChild();
            //
            //            Imgui.EndTabItem();
            //        }
            //
            //        if (Imgui.BeginTabItem("Fonts"))
            //        {
            //            ImGuiIO& io = Imgui.GetIo();
            //            ImFontAtlas* atlas = io.Fonts;
            //            HelpMarker("Read FAQ and docs/FONTS.md for details on font loading.");
            //            Imgui.ShowFontAtlas(atlas);
            //
            //            // Post-baking font scaling. Note that this is NOT the nice way of scaling fonts, read below.
            //            // (we enforce hard clamping manually as by default Drag/Slider allows CTRL+Click text to get out of bounds).
            //            const float MIN_SCALE = 0.3f;
            //            const float MAX_SCALE = 2.0f;
            //            HelpMarker(
            //                "Those are old settings provided for convenience.\n" +
            //                "However, the _correct_ way of scaling your UI is currently to reload your font at the designed size, "
            //                "rebuild the font atlas, and call style.ScaleAllSizes() on a reference ImGuiStyle structure.\n" +
            //                "Using those settings here will give you poor quality results.");
            //            static float window_scale = 1.0f;
            //            Imgui.PushItemWidth(Imgui.GetFontSize() * 8);
            //            if (Imgui.Drag("window scale", &window_scale, 0.005f, MIN_SCALE, MAX_SCALE, "%.2f", SliderOptions.AlwaysClamp)) // Scale only this window
            //                Imgui.SetWindowFontScale(window_scale);
            //            Imgui.Drag("global scale", &io.FontGlobalScale, 0.005f, MIN_SCALE, MAX_SCALE, "%.2f", SliderOptions.AlwaysClamp); // Scale everything
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
            //            Imgui.Drag("Curve Tessellation Tolerance", &style.CurveTessellationTol, 0.02f, 0.10f, 10.0f, "%.2f");
            //            if (style.CurveTessellationTol < 0.10f) style.CurveTessellationTol = 0.10f;
            //
            //            // When editing the "Circle Segment Max Error" value, draw a preview of its effect on auto-tessellated circles.
            //            Imgui.Drag("Circle Tessellation Max Error", &style.CircleTessellationMaxError , 0.005f, 0.10f, 5.0f, "%.2f", SliderOptions.AlwaysClamp);
            //            if (Imgui.IsItemActive())
            //            {
            //                Imgui.SetNextWindowPos(Imgui.GetCursorScreenPos());
            //                Imgui.BeginTooltip();
            //                Imgui.TextUnformatted("(R = radius, N = number of segments)");
            //                Imgui.Spacing();
            //                ImDrawList* draw_list = Imgui.GetWindowDrawList();
            //                const float min_widget_width = Imgui.CalcTextSize("N: MMM\nR: MMM").X;
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
            //                    draw_list->AddCircle(ImVec2(p1.X + offset_x, p1.Y + offset_y), rad, Imgui.GetColorU32(StyleColor.Text));
            //                    Imgui.Dummy(ImVec2(canvas_width, RAD_MAX * 2));
            //
            //                    /*
            //                    const ImVec2 p2 = Imgui.GetCursorScreenPos();
            //                    draw_list->AddCircleFilled(ImVec2(p2.X + offset_x, p2.Y + offset_y), rad, Imgui.GetColorU32(StyleColor.Text));
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
            //            Imgui.Drag("Global Alpha", &style.Alpha, 0.005f, 0.20f, 1.0f, "%.2f"); // Not exposing zero here so user doesn't "lose" the UI (zero alpha clips all widgets). But application code could have a toggle to switch between zero and non-zero.
            //            Imgui.Drag("Disabled Alpha", &style.DisabledAlpha, 0.005f, 0.0f, 1.0f, "%.2f"); Imgui.SameLine(); HelpMarker("Additional alpha multiplier for disabled items (multiply over current value of Alpha).");
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
        private static readonly State<bool> s_b2 = new(true);

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
                    Imgui.GetWindowDrawList()?.AddRectangleFilled(new(p, new(p.X + sz, p.Y + sz)), Imgui.GetColor(color));
                    Imgui.Dummy(new(sz, sz));
                    Imgui.SameLine();
                    _ = Imgui.MenuItem(name);
                }

                Imgui.EndMenu();
            }

            if (Imgui.BeginMenu("Options"))
            {
                _ = Imgui.Checkbox("SomeOption", s_b2);
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
        //        vsnprintf(buf, buf.Length, fmt, args);
        //        buf[buf.Length-1] = 0;
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
        //        const float footer_height_to_reserve = Imgui.GetStyle().ItemSpacing.Y + Imgui.GetFrameHeightWithSpacing();
        //        if (Imgui.BeginChild("ScrollingRegion", ImVec2(0, -footer_height_to_reserve), false, WindowOptions.HorizontalScrollbar))
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
        //            Imgui.PushStyleVariable(StyleVariable.ItemSpacing, ImVec2(4, 1)); // Tighten spacing
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
        //                    Imgui.PushStyleColor(StyleColor.Text, color);
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
        //            Imgui.PopStyleVariable();
        //        }
        //        Imgui.EndChild();
        //        Imgui.Separator();
        //
        //        // Command-line
        //        bool reclaim_focus = false;
        //        ImGuiInputTextFlags input_text_flags = ImGuiInputTextFlags_EnterReturnsTrue | ImGuiInputTextFlags_EscapeClearsAll | ImGuiInputTextFlags_CallbackCompletion | ImGuiInputTextFlags_CallbackHistory;
        //        if (Imgui.InputText("Input", InputBuf, InputBuf.Length, input_text_flags, &TextEditCallbackStub, (void*)this))
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
        //        if (Imgui.BeginChild("scrolling", ImVec2(0, 0), false, WindowOptions.HorizontalScrollbar))
        //        {
        //            if (clear)
        //                Clear();
        //            if (copy)
        //                Imgui.LogToClipboard();
        //
        //            Imgui.PushStyleVariable(StyleVariable.ItemSpacing, ImVec2(0, 0));
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
        //            Imgui.PopStyleVariable();
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
            //            const char* category = categories[counter % categories.Length];
            //            const char* word = words[counter % words.Length];
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
            //    if (Imgui.Begin("Example: Simple layout", p_open, WindowOptions.MenuBar))
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
        //    Imgui.PushId(uid);
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
        //            Imgui.PushId(i); // Use field index as identifier.
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
        //                    Imgui.Drag("##value", &placeholder_members[i], 0.01f);
        //                Imgui.NextColumn();
        //            }
        //            Imgui.PopId();
        //        }
        //        Imgui.TreePop();
        //    }
        //    Imgui.PopId();
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
            //    Imgui.PushStyleVariable(StyleVariable.FramePadding, ImVec2(2, 2));
            //    if (Imgui.BeginTable("split", 2, TableOptions.BordersOuter | TableOptions.Resizable))
            //    {
            //        // Iterate placeholder objects (all the same data)
            //        for (int obj_i = 0; obj_i < 4; obj_i++)
            //        {
            //            ShowPlaceholderObject("Object", obj_i);
            //            //Imgui.Separator();
            //        }
            //        Imgui.EndTable();
            //    }
            //    Imgui.PopStyleVariable();
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
            //            Imgui.PushStyleVariable(StyleVariable.ItemSpacing, ImVec2(0, 0));
            //            ImGuiListClipper clipper;
            //            clipper.Begin(lines);
            //            while (clipper.Step())
            //                for (int i = clipper.DisplayStart; i < clipper.DisplayEnd; i++)
            //                    Imgui.Text("%i The quick brown fox jumps over the lazy dog", i);
            //            Imgui.PopStyleVariable();
            //            break;
            //        }
            //    case 2:
            //        // Multiple calls to Text(), not clipped (slow)
            //        Imgui.PushStyleVariable(StyleVariable.ItemSpacing, ImVec2(0, 0));
            //        for (int i = 0; i < lines; i++)
            //            Imgui.Text("%i The quick brown fox jumps over the lazy dog", i);
            //        Imgui.PopStyleVariable();
            //        break;
            //    }
            //    Imgui.EndChild();
            //    Imgui.End();
        }

        private static void ShowExampleAppAutoResize(State<bool> p_open)
        {
            //    if (!Imgui.Begin("Example: Auto-resizing window", p_open, WindowOptions.AlwaysAutoResize))
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
            //    Imgui.Slider("Number of lines", &lines, 1, 20);
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
            //        static void AspectRatio(ImGuiSizeCallbackData* data)    { float aspect_ratio = *(float*)data->UserData; data->DesiredSize.X = IM_MAX(data->CurrentSize.X, data->CurrentSize.Y); data->DesiredSize.Y = (float)(int)(data->DesiredSize.X / aspect_ratio); }
            //        static void Square(ImGuiSizeCallbackData* data)         { data->DesiredSize.X = data->DesiredSize.Y = IM_MAX(data->CurrentSize.X, data->CurrentSize.Y); }
            //        static void Step(ImGuiSizeCallbackData* data)           { float step = *(float*)data->UserData; data->DesiredSize = ImVec2((int)(data->CurrentSize.X / step + 0.5f) * step, (int)(data->CurrentSize.Y / step + 0.5f) * step); }
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
            //        Imgui.PushStyleVariable(StyleVariable.WindowPadding, ImVec2(0.0f, 0.0f));
            //    const ImGuiWindowFlags window_flags = auto_resize ? WindowOptions.AlwaysAutoResize : 0;
            //    const bool window_open = Imgui.Begin("Example: Constrained Resize", p_open, window_flags);
            //    if (!window_padding)
            //        Imgui.PopStyleVariable();
            //    if (window_open)
            //    {
            //        IMGUI_DEMO_MARKER("Examples/Constrained Resizing window");
            //        if (Imgui.GetIo().KeyShift)
            //        {
            //            // Display a dummy viewport (in your real app you would likely use ImageButton() to display a texture.
            //            ImVec2 avail_size = Imgui.GetContentRegionAvailable();
            //            ImVec2 pos = Imgui.GetCursorScreenPos();
            //            Imgui.ColorButton("viewport", ImVec4(0.5f, 0.2f, 0.5f, 1.0f), ColorEditOptions.NoTooltip | ColorEditOptions.NoDragDrop, avail_size);
            //            Imgui.SetCursorScreenPos(ImVec2(pos.X + 10, pos.Y + 10));
            //            Imgui.Text("%.2f x %.2f", avail_size.X, avail_size.Y);
            //        }
            //        else
            //        {
            //            Imgui.Text("(Hold SHIFT to display a dummy viewport)");
            //            if (Imgui.Button("Set 200x200")) { Imgui.SetWindowSize(ImVec2(200, 200)); } Imgui.SameLine();
            //            if (Imgui.Button("Set 500x500")) { Imgui.SetWindowSize(ImVec2(500, 500)); } Imgui.SameLine();
            //            if (Imgui.Button("Set 800x200")) { Imgui.SetWindowSize(ImVec2(800, 200)); }
            //            Imgui.SetNextItemWidth(Imgui.GetFontSize() * 20);
            //            Imgui.Combo("Constraint", &type, test_desc, test_desc.Length);
            //            Imgui.SetNextItemWidth(Imgui.GetFontSize() * 20);
            //            Imgui.Drag("Lines", &display_lines, 0.2f, 1, 100);
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
            //    ImGuiIO& io = Imgui.GetIo();
            //    ImGuiWindowFlags window_flags = WindowOptions.NoDecoration | WindowOptions.AlwaysAutoResize | WindowOptions.NoSavedSettings | WindowOptions.NoFocusOnAppearing | WindowOptions.NoNav;
            //    if (location >= 0)
            //    {
            //        const float PAD = 10.0f;
            //        const ImGuiViewport* viewport = Imgui.GetMainViewport();
            //        ImVec2 work_pos = viewport->WorkPos; // Use work area to avoid menu-bar/task-bar, if any!
            //        ImVec2 work_size = viewport->WorkSize;
            //        ImVec2 window_pos, window_pos_pivot;
            //        window_pos.X = (location & 1) ? (work_pos.X + work_size.X - PAD) : (work_pos.X + PAD);
            //        window_pos.Y = (location & 2) ? (work_pos.Y + work_size.Y - PAD) : (work_pos.Y + PAD);
            //        window_pos_pivot.X = (location & 1) ? 1.0f : 0.0f;
            //        window_pos_pivot.Y = (location & 2) ? 1.0f : 0.0f;
            //        Imgui.SetNextWindowPos(window_pos, ImGuiCond_Always, window_pos_pivot);
            //        window_flags |= WindowOptions.NoMove;
            //    }
            //    else if (location == -2)
            //    {
            //        // Center window
            //        Imgui.SetNextWindowPos(Imgui.GetMainViewport()->GetCenter(), ImGuiCond_Always, ImVec2(0.5f, 0.5f));
            //        window_flags |= WindowOptions.NoMove;
            //    }
            //    Imgui.SetNextWindowBgAlpha(0.35f); // Transparent background
            //    if (Imgui.Begin("Example: Simple overlay", p_open, window_flags))
            //    {
            //        IMGUI_DEMO_MARKER("Examples/Simple Overlay");
            //        Imgui.Text("Simple overlay\n" "(right-click to change position)");
            //        Imgui.Separator();
            //        if (Imgui.IsMousePosValid())
            //            Imgui.Text("Mouse Position: (%.1f,%.1f)", io.MousePos.X, io.MousePos.Y);
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
            //    static ImGuiWindowFlags flags = WindowOptions.NoDecoration | WindowOptions.NoMove | WindowOptions.NoSavedSettings;
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
            //        Imgui.CheckboxFlags("WindowOptions.NoBackground", &flags, WindowOptions.NoBackground);
            //        Imgui.CheckboxFlags("WindowOptions.NoDecoration", &flags, WindowOptions.NoDecoration);
            //        Imgui.Indent();
            //        Imgui.CheckboxFlags("WindowOptions.NoTitleBar", &flags, WindowOptions.NoTitleBar);
            //        Imgui.CheckboxFlags("WindowOptions.NoCollapse", &flags, WindowOptions.NoCollapse);
            //        Imgui.CheckboxFlags("WindowOptions.NoScrollbar", &flags, WindowOptions.NoScrollbar);
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
            //    Imgui.SetNextWindowPos(ImVec2(base_pos.X + 100, base_pos.Y + 100), ImGuiCond_FirstUseEver);
            //    Imgui.Begin("Same title as another window##1");
            //    IMGUI_DEMO_MARKER("Examples/Manipulating window titles");
            //    Imgui.Text("This is window 1.\nMy title is the same as window 2, but my identifier is unique.");
            //    Imgui.End();
            //
            //    Imgui.SetNextWindowPos(ImVec2(base_pos.X + 100, base_pos.Y + 200), ImGuiCond_FirstUseEver);
            //    Imgui.Begin("Same title as another window##2");
            //    Imgui.Text("This is window 2.\nMy title is the same as window 1, but my identifier is unique.");
            //    Imgui.End();
            //
            //    // Using "###" to display a changing title but keep a static identifier "AnimatedTitle"
            //    char buf[128];
            //    sprintf(buf, "Animated title %c %d###AnimatedTitle", "|/-\\"[(int)(Imgui.GetTime() / 0.25f) & 3], Imgui.GetFrameCount());
            //    Imgui.SetNextWindowPos(ImVec2(base_pos.X + 100, base_pos.Y + 300), ImGuiCond_FirstUseEver);
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
            //                ImVec2 p1 = ImVec2(p0.X + gradient_size.X, p0.Y + gradient_size.Y);
            //                ImU32 col_a = Imgui.GetColorU32(IM_COL32(0, 0, 0, 255));
            //                ImU32 col_b = Imgui.GetColorU32(IM_COL32(255, 255, 255, 255));
            //                draw_list->AddRectFilledMultiColor(p0, p1, col_a, col_b, col_b, col_a);
            //                Imgui.InvisibleButton("##gradient1", gradient_size);
            //            }
            //            {
            //                ImVec2 p0 = Imgui.GetCursorScreenPos();
            //                ImVec2 p1 = ImVec2(p0.X + gradient_size.X, p0.Y + gradient_size.Y);
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
            //            Imgui.Drag("Size", &sz, 0.2f, 2.0f, 100.0f, "%.0f");
            //            Imgui.Drag("Thickness", &thickness, 0.05f, 1.0f, 8.0f, "%.02f");
            //            Imgui.Slider("N-gon sides", &ngon_sides, 3, 12);
            //            Imgui.Checkbox("##circlesegmentoverride", &circle_segments_override);
            //            Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.X);
            //            circle_segments_override |= Imgui.Slider("Circle segments override", &circle_segments_override_v, 3, 40);
            //            Imgui.Checkbox("##curvessegmentoverride", &curve_segments_override);
            //            Imgui.SameLine(0.0f, Imgui.GetStyle().ItemInnerSpacing.X);
            //            curve_segments_override |= Imgui.Slider("Curves segments override", &curve_segments_override_v, 3, 40);
            //            Imgui.ColorEdit4("Color", &colf.X);
            //
            //            const ImVec2 p = Imgui.GetCursorScreenPos();
            //            const ImU32 col = ImColor(colf);
            //            const float spacing = 10.0f;
            //            const ImDrawFlags corners_tl_br = ImDrawFlags_RoundCornersTopLeft | ImDrawFlags_RoundCornersBottomRight;
            //            const float rounding = sz / 5.0f;
            //            const int circle_segments = circle_segments_override ? circle_segments_override_v : 0;
            //            const int curve_segments = curve_segments_override ? curve_segments_override_v : 0;
            //            float x = p.X + 4.0f;
            //            float y = p.Y + 4.0f;
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
            //                x = p.X + 4;
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
            //            //      Imgui.PushStyleVariable(StyleVariable.WindowPadding, ImVec2(0, 0));      // Disable padding
            //            //      Imgui.PushStyleColor(StyleColor.ChildBg, IM_COL32(50, 50, 50, 255));  // Set a background color
            //            //      Imgui.BeginChild("canvas", ImVec2(0.0f, 0.0f), true, WindowOptions.NoMove);
            //            //      Imgui.PopStyleColor();
            //            //      Imgui.PopStyleVariable();
            //            //      [...]
            //            //      Imgui.EndChild();
            //
            //            // Using InvisibleButton() as a convenience 1) it will advance the layout cursor and 2) allows us to use IsItemHovered()/IsItemActive()
            //            ImVec2 canvas_p0 = Imgui.GetCursorScreenPos();      // ImDrawList API uses screen coordinates!
            //            ImVec2 canvas_sz = Imgui.GetContentRegionAvailable();   // Resize canvas to what's available
            //            if (canvas_sz.X < 50.0f) canvas_sz.X = 50.0f;
            //            if (canvas_sz.Y < 50.0f) canvas_sz.Y = 50.0f;
            //            ImVec2 canvas_p1 = ImVec2(canvas_p0.X + canvas_sz.X, canvas_p0.Y + canvas_sz.Y);
            //
            //            // Draw border and background color
            //            ImGuiIO& io = Imgui.GetIo();
            //            ImDrawList* draw_list = Imgui.GetWindowDrawList();
            //            draw_list->AddRectFilled(canvas_p0, canvas_p1, IM_COL32(50, 50, 50, 255));
            //            draw_list->AddRect(canvas_p0, canvas_p1, IM_COL32(255, 255, 255, 255));
            //
            //            // This will catch our interactions
            //            Imgui.InvisibleButton("canvas", canvas_sz, ImGuiButtonFlags_MouseButtonLeft | ImGuiButtonFlags_MouseButtonRight);
            //            const bool is_hovered = Imgui.IsItemHovered(); // Hovered
            //            const bool is_active = Imgui.IsItemActive();   // Held
            //            const ImVec2 origin(canvas_p0.X + scrolling.X, canvas_p0.Y + scrolling.Y); // Lock scrolled origin
            //            const ImVec2 mouse_pos_in_canvas(io.MousePos.X - origin.X, io.MousePos.Y - origin.Y);
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
            //                scrolling.X += io.MouseDelta.X;
            //                scrolling.Y += io.MouseDelta.Y;
            //            }
            //
            //            // Context menu (under default mouse threshold)
            //            ImVec2 drag_delta = Imgui.GetMouseDragDelta(ImGuiMouseButton_Right);
            //            if (opt_enable_context_menu && drag_delta.X == 0.0f && drag_delta.Y == 0.0f)
            //                Imgui.OpenPopupOnItemClick("context", PopupOptions.MouseButtonRight);
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
            //                for (float x = fmodf(scrolling.X, GRID_STEP); x < canvas_sz.X; x += GRID_STEP)
            //                    draw_list->AddLine(ImVec2(canvas_p0.X + x, canvas_p0.Y), ImVec2(canvas_p0.X + x, canvas_p1.Y), IM_COL32(200, 200, 200, 40));
            //                for (float y = fmodf(scrolling.Y, GRID_STEP); y < canvas_sz.Y; y += GRID_STEP)
            //                    draw_list->AddLine(ImVec2(canvas_p0.X, canvas_p0.Y + y), ImVec2(canvas_p1.X, canvas_p0.Y + y), IM_COL32(200, 200, 200, 40));
            //            }
            //            for (int n = 0; n < points.Size; n += 2)
            //                draw_list->AddLine(ImVec2(origin.X + points[n].X, origin.Y + points[n].Y), ImVec2(origin.X + points[n + 1].X, origin.Y + points[n + 1].Y), IM_COL32(255, 255, 0, 255), 2.0f);
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
            //            ImVec2 window_pos = Imgui.GetWindowPosition();
            //            ImVec2 window_size = Imgui.GetWindowSize();
            //            ImVec2 window_center = ImVec2(window_pos.X + window_size.X * 0.5f, window_pos.Y + window_size.Y * 0.5f);
            //            if (draw_bg)
            //                Imgui.GetBackgroundDrawList()->AddCircle(window_center, window_size.X * 0.6f, IM_COL32(255, 0, 0, 200), 0, 10 + 4);
            //            if (draw_fg)
            //                Imgui.GetForegroundDrawList()->AddCircle(window_center, window_size.Y * 0.6f, IM_COL32(0, 255, 0, 200), 0, 10);
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
        //        Imgui.PushId(doc);
        //        Imgui.Text("Document \"%s\"", doc->Name);
        //        Imgui.PushStyleColor(StyleColor.Text, doc->Color);
        //        Imgui.TextWrapped("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
        //        Imgui.PopStyleColor();
        //        if (Imgui.Button("Modify", ImVec2(100, 0)))
        //            doc->Dirty = true;
        //        Imgui.SameLine();
        //        if (Imgui.Button("Save", ImVec2(100, 0)))
        //            doc->DoSave();
        //        Imgui.ColorEdit3("color", &doc->Color.X);  // Useful to test drag and drop and hold-dragged-to-open-tab behavior.
        //        Imgui.PopId();
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
            //    bool window_contents_visible = Imgui.Begin("Example: Documents", p_open, WindowOptions.MenuBar);
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
            //        Imgui.PushId(doc);
            //        if (Imgui.Checkbox(doc->Name, &doc->Open))
            //            if (!doc->Open)
            //                doc->DoForceClose();
            //        Imgui.PopId();
            //    }
            //
            //    Imgui.Separator();
            //
            //    // About the WindowOptions.UnsavedDocument / ImGuiTabItemFlags_UnsavedDocument flags.
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
            //            //if (Imgui.GetIo().KeyCtrl) Imgui.SetTabItemSelected(docs[1].Name);  // [DEBUG] Test SetTabItemSelected(), probably not very useful as-is anyway..
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
            //            if (Imgui.BeginPopupModal("Save?", NULL, WindowOptions.AlwaysAutoResize))
            //            {
            //                Imgui.Text("Save change to the following items?");
            //                float item_height = Imgui.GetTextLineHeightWithSpacing();
            //                if (Imgui.BeginChildFrame(Imgui.GetId("frame"), ImVec2(-FLT_MIN, 6.25f * item_height)))
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
