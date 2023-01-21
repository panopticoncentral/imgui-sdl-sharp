using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ImguiSharp.Platform.Sdl
{
    public static unsafe class ImplSdl2
    {
        private static Dictionary<nuint, Data>? s_dataDictionary;
        private static Dictionary<nuint, Data> DataDictionary => s_dataDictionary ??= new Dictionary<nuint, Data>();

        private sealed class Data
        {
            public SdlSharp.Native.SDL_Window* _window;
            public SdlSharp.Native.SDL_Renderer* _renderer;
            public ulong _time;
            public int _mouseButtonsDown;
            public SdlSharp.Native.SDL_Cursor*[] _mouseCursors;
            public int _pendingMouseLeaveFrame;
            public byte* _clipboardTextData;

            public Data(SdlSharp.Native.SDL_Window* window, SdlSharp.Native.SDL_Renderer* renderer)
            {
                _window = window;
                _renderer = renderer;
                _mouseCursors = new SdlSharp.Native.SDL_Cursor*[(int)MouseCursor.Count];
            }
        };

        private static Data? GetBackendData() => Imgui.GetCurrentContext() != null ? DataDictionary[Imgui.GetIo().BackendPlatformUserData] : null;

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static byte* GetClipboardText(void* unused)
        {
            var bd = GetBackendData();
            if (bd == null)
            {
                return null;
            }

            if (bd._clipboardTextData != null)
            {
                SdlSharp.Native.SDL_free(bd._clipboardTextData);
            }

            bd._clipboardTextData = SdlSharp.Native.SDL_GetClipboardText();
            return bd._clipboardTextData;
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static void SetClipboardText(void* unused, byte* text) => _ = SdlSharp.Native.SDL_SetClipboardText(text);

        private static Key KeycodeToImGuiKey(int keycode)
        {
            return (SdlSharp.Native.SDL_KeyCode)keycode switch
            {
                SdlSharp.Native.SDL_KeyCode.SDLK_TAB => Key.Tab,
                SdlSharp.Native.SDL_KeyCode.SDLK_LEFT => Key.LeftArrow,
                SdlSharp.Native.SDL_KeyCode.SDLK_RIGHT => Key.RightArrow,
                SdlSharp.Native.SDL_KeyCode.SDLK_UP => Key.UpArrow,
                SdlSharp.Native.SDL_KeyCode.SDLK_DOWN => Key.DownArrow,
                SdlSharp.Native.SDL_KeyCode.SDLK_PAGEUP => Key.PageUp,
                SdlSharp.Native.SDL_KeyCode.SDLK_PAGEDOWN => Key.PageDown,
                SdlSharp.Native.SDL_KeyCode.SDLK_HOME => Key.Home,
                SdlSharp.Native.SDL_KeyCode.SDLK_END => Key.End,
                SdlSharp.Native.SDL_KeyCode.SDLK_INSERT => Key.Insert,
                SdlSharp.Native.SDL_KeyCode.SDLK_DELETE => Key.Delete,
                SdlSharp.Native.SDL_KeyCode.SDLK_BACKSPACE => Key.Backspace,
                SdlSharp.Native.SDL_KeyCode.SDLK_SPACE => Key.Space,
                SdlSharp.Native.SDL_KeyCode.SDLK_RETURN => Key.Enter,
                SdlSharp.Native.SDL_KeyCode.SDLK_ESCAPE => Key.Escape,
                SdlSharp.Native.SDL_KeyCode.SDLK_QUOTE => Key.Apostrophe,
                SdlSharp.Native.SDL_KeyCode.SDLK_COMMA => Key.Comma,
                SdlSharp.Native.SDL_KeyCode.SDLK_MINUS => Key.Minus,
                SdlSharp.Native.SDL_KeyCode.SDLK_PERIOD => Key.Period,
                SdlSharp.Native.SDL_KeyCode.SDLK_SLASH => Key.Slash,
                SdlSharp.Native.SDL_KeyCode.SDLK_SEMICOLON => Key.Semicolon,
                SdlSharp.Native.SDL_KeyCode.SDLK_EQUALS => Key.Equal,
                SdlSharp.Native.SDL_KeyCode.SDLK_LEFTBRACKET => Key.LeftBracket,
                SdlSharp.Native.SDL_KeyCode.SDLK_BACKSLASH => Key.Backslash,
                SdlSharp.Native.SDL_KeyCode.SDLK_RIGHTBRACKET => Key.RightBracket,
                SdlSharp.Native.SDL_KeyCode.SDLK_BACKQUOTE => Key.GraveAccent,
                SdlSharp.Native.SDL_KeyCode.SDLK_CAPSLOCK => Key.CapsLock,
                SdlSharp.Native.SDL_KeyCode.SDLK_SCROLLLOCK => Key.ScrollLock,
                SdlSharp.Native.SDL_KeyCode.SDLK_NUMLOCKCLEAR => Key.NumLock,
                SdlSharp.Native.SDL_KeyCode.SDLK_PRINTSCREEN => Key.PrintScreen,
                SdlSharp.Native.SDL_KeyCode.SDLK_PAUSE => Key.Pause,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_0 => Key.Keypad0,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_1 => Key.Keypad1,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_2 => Key.Keypad2,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_3 => Key.Keypad3,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_4 => Key.Keypad4,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_5 => Key.Keypad5,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_6 => Key.Keypad6,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_7 => Key.Keypad7,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_8 => Key.Keypad8,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_9 => Key.Keypad9,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_PERIOD => Key.KeypadDecimal,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_DIVIDE => Key.KeypadDivide,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_MULTIPLY => Key.KeypadMultiply,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_MINUS => Key.KeypadSubtract,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_PLUS => Key.KeypadAdd,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_ENTER => Key.KeypadEnter,
                SdlSharp.Native.SDL_KeyCode.SDLK_KP_EQUALS => Key.KeypadEqual,
                SdlSharp.Native.SDL_KeyCode.SDLK_LCTRL => Key.LeftCtrl,
                SdlSharp.Native.SDL_KeyCode.SDLK_LSHIFT => Key.LeftShift,
                SdlSharp.Native.SDL_KeyCode.SDLK_LALT => Key.LeftAlt,
                SdlSharp.Native.SDL_KeyCode.SDLK_LGUI => Key.LeftSuper,
                SdlSharp.Native.SDL_KeyCode.SDLK_RCTRL => Key.RightCtrl,
                SdlSharp.Native.SDL_KeyCode.SDLK_RSHIFT => Key.RightShift,
                SdlSharp.Native.SDL_KeyCode.SDLK_RALT => Key.RightAlt,
                SdlSharp.Native.SDL_KeyCode.SDLK_RGUI => Key.RightSuper,
                SdlSharp.Native.SDL_KeyCode.SDLK_APPLICATION => Key.Menu,
                SdlSharp.Native.SDL_KeyCode.SDLK_0 => Key.Num0,
                SdlSharp.Native.SDL_KeyCode.SDLK_1 => Key.Num1,
                SdlSharp.Native.SDL_KeyCode.SDLK_2 => Key.Num2,
                SdlSharp.Native.SDL_KeyCode.SDLK_3 => Key.Num3,
                SdlSharp.Native.SDL_KeyCode.SDLK_4 => Key.Num4,
                SdlSharp.Native.SDL_KeyCode.SDLK_5 => Key.Num5,
                SdlSharp.Native.SDL_KeyCode.SDLK_6 => Key.Num6,
                SdlSharp.Native.SDL_KeyCode.SDLK_7 => Key.Num7,
                SdlSharp.Native.SDL_KeyCode.SDLK_8 => Key.Num8,
                SdlSharp.Native.SDL_KeyCode.SDLK_9 => Key.Num9,
                SdlSharp.Native.SDL_KeyCode.SDLK_a => Key.A,
                SdlSharp.Native.SDL_KeyCode.SDLK_b => Key.B,
                SdlSharp.Native.SDL_KeyCode.SDLK_c => Key.C,
                SdlSharp.Native.SDL_KeyCode.SDLK_d => Key.D,
                SdlSharp.Native.SDL_KeyCode.SDLK_e => Key.E,
                SdlSharp.Native.SDL_KeyCode.SDLK_f => Key.F,
                SdlSharp.Native.SDL_KeyCode.SDLK_g => Key.G,
                SdlSharp.Native.SDL_KeyCode.SDLK_h => Key.H,
                SdlSharp.Native.SDL_KeyCode.SDLK_i => Key.I,
                SdlSharp.Native.SDL_KeyCode.SDLK_j => Key.J,
                SdlSharp.Native.SDL_KeyCode.SDLK_k => Key.K,
                SdlSharp.Native.SDL_KeyCode.SDLK_l => Key.L,
                SdlSharp.Native.SDL_KeyCode.SDLK_m => Key.M,
                SdlSharp.Native.SDL_KeyCode.SDLK_n => Key.N,
                SdlSharp.Native.SDL_KeyCode.SDLK_o => Key.O,
                SdlSharp.Native.SDL_KeyCode.SDLK_p => Key.P,
                SdlSharp.Native.SDL_KeyCode.SDLK_q => Key.Q,
                SdlSharp.Native.SDL_KeyCode.SDLK_r => Key.R,
                SdlSharp.Native.SDL_KeyCode.SDLK_s => Key.S,
                SdlSharp.Native.SDL_KeyCode.SDLK_t => Key.T,
                SdlSharp.Native.SDL_KeyCode.SDLK_u => Key.U,
                SdlSharp.Native.SDL_KeyCode.SDLK_v => Key.V,
                SdlSharp.Native.SDL_KeyCode.SDLK_w => Key.W,
                SdlSharp.Native.SDL_KeyCode.SDLK_x => Key.X,
                SdlSharp.Native.SDL_KeyCode.SDLK_y => Key.Y,
                SdlSharp.Native.SDL_KeyCode.SDLK_z => Key.Z,
                SdlSharp.Native.SDL_KeyCode.SDLK_F1 => Key.F1,
                SdlSharp.Native.SDL_KeyCode.SDLK_F2 => Key.F2,
                SdlSharp.Native.SDL_KeyCode.SDLK_F3 => Key.F3,
                SdlSharp.Native.SDL_KeyCode.SDLK_F4 => Key.F4,
                SdlSharp.Native.SDL_KeyCode.SDLK_F5 => Key.F5,
                SdlSharp.Native.SDL_KeyCode.SDLK_F6 => Key.F6,
                SdlSharp.Native.SDL_KeyCode.SDLK_F7 => Key.F7,
                SdlSharp.Native.SDL_KeyCode.SDLK_F8 => Key.F8,
                SdlSharp.Native.SDL_KeyCode.SDLK_F9 => Key.F9,
                SdlSharp.Native.SDL_KeyCode.SDLK_F10 => Key.F10,
                SdlSharp.Native.SDL_KeyCode.SDLK_F11 => Key.F11,
                SdlSharp.Native.SDL_KeyCode.SDLK_F12 => Key.F12,
                _ => Key.None,
            };
        }

        private static void UpdateKeyModifiers(SdlSharp.Native.SDL_Keymod sdl_key_mods)
        {
            var io = Imgui.GetIo();
            io.AddKeyEvent(Key.ModCtrl, (sdl_key_mods & SdlSharp.Native.SDL_Keymod.KMOD_CTRL) != 0);
            io.AddKeyEvent(Key.ModShift, (sdl_key_mods & SdlSharp.Native.SDL_Keymod.KMOD_SHIFT) != 0);
            io.AddKeyEvent(Key.ModAlt, (sdl_key_mods & SdlSharp.Native.SDL_Keymod.KMOD_ALT) != 0);
            io.AddKeyEvent(Key.ModSuper, (sdl_key_mods & SdlSharp.Native.SDL_Keymod.KMOD_GUI) != 0);
        }

        //    // You can read the io.WantCaptureMouse, io.WantCaptureKeyboard flags to tell if dear imgui wants to use your inputs.
        //    // - When io.WantCaptureMouse is true, do not dispatch mouse input data to your main application, or clear/overwrite your copy of the mouse data.
        //    // - When io.WantCaptureKeyboard is true, do not dispatch keyboard input data to your main application, or clear/overwrite your copy of the keyboard data.
        //    // Generally you may always pass all inputs to dear imgui, and hide them from your application based on those two flags.
        //    // If you have multiple SDL events and some of them are not meant to be used by dear imgui, you may need to filter events based on their windowID field.
        //    bool ImGui_ImplSDL2_ProcessEvent(const SDL_Event* event)
        //    {
        //        ImGuiIO & io = ImGui::GetIO();
        //        ImGui_ImplSDL2_Data* bd = ImGui_ImplSDL2_GetBackendData();

        //        switch (event->type)
        //{
        //    case SDL_MOUSEMOTION:
        //            {
        //                io.AddMousePosEvent((float)event->motion.x, (float)event->motion.y);
        //                return true;
        //            }
        //    case SDL_MOUSEWHEEL:
        //            {
        //                float wheel_x = (event->wheel.x > 0) ? 1.0f : (event->wheel.x < 0) ? -1.0f : 0.0f;
        //                float wheel_y = (event->wheel.y > 0) ? 1.0f : (event->wheel.y < 0) ? -1.0f : 0.0f;
        //                io.AddMouseWheelEvent(wheel_x, wheel_y);
        //                return true;
        //            }
        //    case SDL_MOUSEBUTTONDOWN:
        //    case SDL_MOUSEBUTTONUP:
        //            {
        //                int mouse_button = -1;
        //                if (event->button.button == SDL_BUTTON_LEFT) { mouse_button = 0; }
        //                if (event->button.button == SDL_BUTTON_RIGHT) { mouse_button = 1; }
        //                if (event->button.button == SDL_BUTTON_MIDDLE) { mouse_button = 2; }
        //                if (event->button.button == SDL_BUTTON_X1) { mouse_button = 3; }
        //                if (event->button.button == SDL_BUTTON_X2) { mouse_button = 4; }
        //                if (mouse_button == -1)
        //                    break;
        //                io.AddMouseButtonEvent(mouse_button, (event->type == SDL_MOUSEBUTTONDOWN));
        //                bd->MouseButtonsDown = (event->type == SDL_MOUSEBUTTONDOWN) ? (bd->MouseButtonsDown | (1 << mouse_button)) : (bd->MouseButtonsDown & ~(1 << mouse_button));
        //                return true;
        //            }
        //    case SDL_TEXTINPUT:
        //            {
        //                io.AddInputCharactersUTF8(event->text.text);
        //                return true;
        //            }
        //    case SDL_KEYDOWN:
        //    case SDL_KEYUP:
        //            {
        //                ImGui_ImplSDL2_UpdateKeyModifiers((SDL_Keymod)event->key.keysym.mod);
        //                ImGuiKey key = ImGui_ImplSDL2_KeycodeToImGuiKey(event->key.keysym.sym);
        //                io.AddKeyEvent(key, (event->type == SDL_KEYDOWN));
        //                io.SetKeyEventNativeData(key, event->key.keysym.sym, event->key.keysym.scancode, event->key.keysym.scancode); // To support legacy indexing (<1.87 user code). Legacy backend uses SdlSharp.Native.SDL_KeyCode.SDLK_*** as indices to IsKeyXXX() functions.
        //                return true;
        //            }
        //    case SDL_WINDOWEVENT:
        //            {
        //                // - When capturing mouse, SDL will send a bunch of conflicting LEAVE/ENTER event on every mouse move, but the final ENTER tends to be right.
        //                // - However we won't get a correct LEAVE event for a captured window.
        //                // - In some cases, when detaching a window from main viewport SDL may send SDL_WINDOWEVENT_ENTER one frame too late,
        //                //   causing SDL_WINDOWEVENT_LEAVE on previous frame to interrupt drag operation by clear mouse position. This is why
        //                //   we delay process the SDL_WINDOWEVENT_LEAVE events by one frame. See issue #5012 for details.
        //                Uint8 window_event = event->window.event;
        //                if (window_event == SDL_WINDOWEVENT_ENTER)
        //                    bd->PendingMouseLeaveFrame = 0;
        //                if (window_event == SDL_WINDOWEVENT_LEAVE)
        //                    bd->PendingMouseLeaveFrame = ImGui::GetFrameCount() + 1;
        //                if (window_event == SDL_WINDOWEVENT_FOCUS_GAINED)
        //                    io.AddFocusEvent(true);
        //                else if (event->window.event == SDL_WINDOWEVENT_FOCUS_LOST)
        //            io.AddFocusEvent(false);
        //                return true;
        //            }
        //        }
        //        return false;
        //    }

        public static bool Init(SdlSharp.Native.SDL_Window* window, SdlSharp.Native.SDL_Renderer* renderer)
        {
            var io = Imgui.GetIo();

            if (io.BackendPlatformUserData != 0)
            {
                throw new InvalidOperationException("Platform backend is already initialized.");
            }

            var bd = new Data(window, renderer);
            DataDictionary[(nuint)bd.GetHashCode()] = bd;

            io.BackendPlatformUserData = (nuint)bd.GetHashCode();
            io.BackendPlatformName = "imgui_impl_sdl";
            io.BackendOptions |= BackendOptions.HasMouseCursors;
            io.BackendOptions |= BackendOptions.HasSetMousePos;

            io.SetClipboardText = &SetClipboardText;
            io.GetClipboardText = &GetClipboardText;
            io.ClipboardUserData = 0;

            bd._mouseCursors[(int)MouseCursor.Arrow] = SdlSharp.Native.SDL_CreateSystemCursor(SdlSharp.Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_ARROW);
            bd._mouseCursors[(int)MouseCursor.TextInput] = SdlSharp.Native.SDL_CreateSystemCursor(SdlSharp.Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_IBEAM);
            bd._mouseCursors[(int)MouseCursor.ResizeAll] = SdlSharp.Native.SDL_CreateSystemCursor(SdlSharp.Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZEALL);
            bd._mouseCursors[(int)MouseCursor.ResizeNS] = SdlSharp.Native.SDL_CreateSystemCursor(SdlSharp.Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZENS);
            bd._mouseCursors[(int)MouseCursor.ResizeEW] = SdlSharp.Native.SDL_CreateSystemCursor(SdlSharp.Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZEWE);
            bd._mouseCursors[(int)MouseCursor.ResizeNESW] = SdlSharp.Native.SDL_CreateSystemCursor(SdlSharp.Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZENESW);
            bd._mouseCursors[(int)MouseCursor.ResizeNWSE] = SdlSharp.Native.SDL_CreateSystemCursor(SdlSharp.Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_SIZENWSE);
            bd._mouseCursors[(int)MouseCursor.Hand] = SdlSharp.Native.SDL_CreateSystemCursor(SdlSharp.Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_HAND);
            bd._mouseCursors[(int)MouseCursor.NotAllowed] = SdlSharp.Native.SDL_CreateSystemCursor(SdlSharp.Native.SDL_SystemCursor.SDL_SYSTEM_CURSOR_NO);

            SDL_SysWMinfo info;
            SDL_VERSION(&info.version);
            if (SDL_GetWindowWMInfo(window, &info))
            {
                Imgui.GetMainViewport()->PlatformHandleRaw = (void*)info.info.win.window;
            }

            _ = SdlSharp.Native.StringToUtf8Func(SdlSharp.Native.SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH, "1", SdlSharp.Native.SDL_SetHint);
            _ = SdlSharp.Native.StringToUtf8Func(SdlSharp.Native.SDL_HINT_MOUSE_AUTO_CAPTURE, "0", SdlSharp.Native.SDL_SetHint);

            return true;
        }

        public static void Shutdown()
        {
            var bd = GetBackendData();
            if (bd == null)
            {
                throw new InvalidOperationException();
            }

            var io = Imgui.GetIo();

            if (bd._clipboardTextData != null)
            {
                SdlSharp.Native.SDL_free(bd._clipboardTextData);
            }
            for (MouseCursor cursor = 0; cursor < MouseCursor.Count; cursor++)
            {
                SdlSharp.Native.SDL_FreeCursor(bd._mouseCursors[(int)cursor]);
            }

            io.BackendPlatformName = null;
            io.BackendPlatformUserData = 0;
            _ = DataDictionary.Remove((nuint)bd.GetHashCode());
        }

        private static void UpdateMouseData()
        {
            var bd = GetBackendData();
            if (bd == null)
            {
                throw new InvalidOperationException();
            }

            var io = Imgui.GetIo();

            _ = SdlSharp.Native.SDL_CaptureMouse(bd._mouseButtonsDown != 0 && Imgui.GetDragDropPayload() == null);
            var focusedWindow = SdlSharp.Native.SDL_GetKeyboardFocus();

            if (bd._window == focusedWindow)
            {
                if (io.WantSetMousePos)
                {
                    SdlSharp.Native.SDL_WarpMouseInWindow(bd._window, (int)io.MousePosition.X, (int)io.MousePosition.Y);
                }

                if (bd._mouseButtonsDown == 0)
                {
                    int windowX, windowY, mouseXGlobal, mouseYGlobal;
                    _ = SdlSharp.Native.SDL_GetGlobalMouseState(&mouseXGlobal, &mouseYGlobal);
                    SdlSharp.Native.SDL_GetWindowPosition(bd._window, &windowX, &windowY);
                    io.AddMousePosEvent(new(mouseXGlobal - windowX, mouseYGlobal - windowY));
                }
            }
        }

        //    static void ImGui_ImplSDL2_UpdateMouseCursor()
        //    {
        //        ImGuiIO & io = ImGui::GetIO();
        //        if (io.ConfigFlags & ImGuiConfigFlags_NoMouseCursorChange)
        //            return;
        //        ImGui_ImplSDL2_Data* bd = ImGui_ImplSDL2_GetBackendData();

        //        ImGuiMouseCursor imgui_cursor = ImGui::GetMouseCursor();
        //        if (io.MouseDrawCursor || imgui_cursor == ImGuiMouseCursor_None)
        //        {
        //            // Hide OS mouse cursor if imgui is drawing it or if it wants no cursor
        //            SDL_ShowCursor(SDL_FALSE);
        //        }
        //        else
        //        {
        //            // Show OS mouse cursor
        //            SDL_SetCursor(bd->MouseCursors[imgui_cursor] ? bd->MouseCursors[imgui_cursor] : bd->MouseCursors[ImGuiMouseCursor_Arrow]);
        //            SDL_ShowCursor(SDL_TRUE);
        //        }
        //    }

        //    static void ImGui_ImplSDL2_UpdateGamepads()
        //    {
        //        ImGuiIO & io = ImGui::GetIO();
        //        if ((io.ConfigFlags & ImGuiConfigFlags_NavEnableGamepad) == 0) // FIXME: Technically feeding gamepad shouldn't depend on this now that they are regular inputs.
        //            return;

        //        // Get gamepad
        //        io.BackendFlags &= ~ImGuiBackendFlags_HasGamepad;
        //        SDL_GameController* game_controller = SDL_GameControllerOpen(0);
        //        if (!game_controller)
        //            return;
        //        io.BackendFlags |= ImGuiBackendFlags_HasGamepad;

        //        // Update gamepad inputs
        //#define IM_SATURATE(V)                      (V < 0.0f ? 0.0f : V > 1.0f ? 1.0f : V)
        //#define MAP_BUTTON(KEY_NO, BUTTON_NO)       { io.AddKeyEvent(KEY_NO, SDL_GameControllerGetButton(game_controller, BUTTON_NO) != 0); }
        //#define MAP_ANALOG(KEY_NO, AXIS_NO, V0, V1) { float vn = (float)(SDL_GameControllerGetAxis(game_controller, AXIS_NO) - V0) / (float)(V1 - V0); vn = IM_SATURATE(vn); io.AddKeyAnalogEvent(KEY_NO, vn > 0.1f, vn); }
        //        const int thumb_dead_zone = 8000;           // SDL_gamecontroller.h suggests using this value.
        //        MAP_BUTTON(Key.GamepadStart, SDL_CONTROLLER_BUTTON_START);
        //        MAP_BUTTON(Key.GamepadBack, SDL_CONTROLLER_BUTTON_BACK);
        //        MAP_BUTTON(Key.GamepadFaceLeft, SDL_CONTROLLER_BUTTON_X);              // Xbox X, PS Square
        //        MAP_BUTTON(Key.GamepadFaceRight, SDL_CONTROLLER_BUTTON_B);              // Xbox B, PS Circle
        //        MAP_BUTTON(Key.GamepadFaceUp, SDL_CONTROLLER_BUTTON_Y);              // Xbox Y, PS Triangle
        //        MAP_BUTTON(Key.GamepadFaceDown, SDL_CONTROLLER_BUTTON_A);              // Xbox A, PS Cross
        //        MAP_BUTTON(Key.GamepadDpadLeft, SDL_CONTROLLER_BUTTON_DPAD_LEFT);
        //        MAP_BUTTON(Key.GamepadDpadRight, SDL_CONTROLLER_BUTTON_DPAD_RIGHT);
        //        MAP_BUTTON(Key.GamepadDpadUp, SDL_CONTROLLER_BUTTON_DPAD_UP);
        //        MAP_BUTTON(Key.GamepadDpadDown, SDL_CONTROLLER_BUTTON_DPAD_DOWN);
        //        MAP_BUTTON(Key.GamepadL1, SDL_CONTROLLER_BUTTON_LEFTSHOULDER);
        //        MAP_BUTTON(Key.GamepadR1, SDL_CONTROLLER_BUTTON_RIGHTSHOULDER);
        //        MAP_ANALOG(Key.GamepadL2, SDL_CONTROLLER_AXIS_TRIGGERLEFT, 0.0f, 32767);
        //        MAP_ANALOG(Key.GamepadR2, SDL_CONTROLLER_AXIS_TRIGGERRIGHT, 0.0f, 32767);
        //        MAP_BUTTON(Key.GamepadL3, SDL_CONTROLLER_BUTTON_LEFTSTICK);
        //        MAP_BUTTON(Key.GamepadR3, SDL_CONTROLLER_BUTTON_RIGHTSTICK);
        //        MAP_ANALOG(Key.GamepadLStickLeft, SDL_CONTROLLER_AXIS_LEFTX, -thumb_dead_zone, -32768);
        //        MAP_ANALOG(Key.GamepadLStickRight, SDL_CONTROLLER_AXIS_LEFTX, +thumb_dead_zone, +32767);
        //        MAP_ANALOG(Key.GamepadLStickUp, SDL_CONTROLLER_AXIS_LEFTY, -thumb_dead_zone, -32768);
        //        MAP_ANALOG(Key.GamepadLStickDown, SDL_CONTROLLER_AXIS_LEFTY, +thumb_dead_zone, +32767);
        //        MAP_ANALOG(Key.GamepadRStickLeft, SDL_CONTROLLER_AXIS_RIGHTX, -thumb_dead_zone, -32768);
        //        MAP_ANALOG(Key.GamepadRStickRight, SDL_CONTROLLER_AXIS_RIGHTX, +thumb_dead_zone, +32767);
        //        MAP_ANALOG(Key.GamepadRStickUp, SDL_CONTROLLER_AXIS_RIGHTY, -thumb_dead_zone, -32768);
        //        MAP_ANALOG(Key.GamepadRStickDown, SDL_CONTROLLER_AXIS_RIGHTY, +thumb_dead_zone, +32767);
        //#undef MAP_BUTTON
        //#undef MAP_ANALOG
        //    }

        public static void NewFrame()
        {
            var bd = GetBackendData();
            if (bd == null)
            {
                throw new InvalidOperationException();
            }

            var io = Imgui.GetIo();

            int width, height;
            int displayWidth, displayHeight;
            SdlSharp.Native.SDL_GetWindowSize(bd._window, &width, &height);
            if ((SdlSharp.Native.SDL_GetWindowFlags(bd._window) & (uint)SdlSharp.Native.SDL_WindowFlags.SDL_WINDOW_MINIMIZED) != 0)
            {
                width = height = 0;
            }

            _ = SdlSharp.Native.SDL_GetRendererOutputSize(bd._renderer, &displayWidth, &displayHeight);

            io.DisplaySize = new(width, height);
            if (width > 0 && height > 0)
            {
                io.DisplayFramebufferScale = new((float)displayWidth / width, (float)displayHeight / height);
            }

            ulong frequency = SdlSharp.Native.SDL_GetPerformanceFrequency();
            ulong current_time = SdlSharp.Native.SDL_GetPerformanceCounter();
            io.DeltaTime = bd._time > 0 ? (float)((double)(current_time - bd._time) / frequency) : (float)(1.0f / 60.0f);
            bd._time = current_time;

            if (bd._pendingMouseLeaveFrame && bd._pendingMouseLeaveFrame >= Imgui.GetFrameCount() && bd._mouseButtonsDown == 0)
            {
                io.AddMousePosEvent(new(-float.MaxValue, -float.MaxValue));
                bd._pendingMouseLeaveFrame = 0;
            }

            UpdateMouseData();
            UpdateMouseCursor();

            UpdateGamepads();
        }
    }
}
