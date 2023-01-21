﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SdlSharp.Graphics;
using SdlSharp.Input;

namespace ImguiSharp.Platform.Sdl
{
    public static unsafe class ImplSdl2
    {
        private static Dictionary<nuint, Data>? s_dataDictionary;
        private static Dictionary<nuint, Data> DataDictionary => s_dataDictionary ??= new Dictionary<nuint, Data>();

        private sealed class Data
        {
            public Window _window;
            public Renderer _renderer;
            public ulong _time;
            public int _mouseButtonsDown;
            public Cursor[] _mouseCursors;
            public int _pendingMouseLeaveFrame;
            public byte* _clipboardTextData;

            public Data(Window window, Renderer renderer)
            {
                _window = window;
                _renderer = renderer;
                _mouseCursors = Array.Empty<Cursor>();
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

        //    static ImGuiKey ImGui_ImplSDL2_KeycodeToImGuiKey(int keycode)
        //    {
        //        switch (keycode)
        //        {
        //            case SDLK_TAB: return ImGuiKey_Tab;
        //            case SDLK_LEFT: return ImGuiKey_LeftArrow;
        //            case SDLK_RIGHT: return ImGuiKey_RightArrow;
        //            case SDLK_UP: return ImGuiKey_UpArrow;
        //            case SDLK_DOWN: return ImGuiKey_DownArrow;
        //            case SDLK_PAGEUP: return ImGuiKey_PageUp;
        //            case SDLK_PAGEDOWN: return ImGuiKey_PageDown;
        //            case SDLK_HOME: return ImGuiKey_Home;
        //            case SDLK_END: return ImGuiKey_End;
        //            case SDLK_INSERT: return ImGuiKey_Insert;
        //            case SDLK_DELETE: return ImGuiKey_Delete;
        //            case SDLK_BACKSPACE: return ImGuiKey_Backspace;
        //            case SDLK_SPACE: return ImGuiKey_Space;
        //            case SDLK_RETURN: return ImGuiKey_Enter;
        //            case SDLK_ESCAPE: return ImGuiKey_Escape;
        //            case SDLK_QUOTE: return ImGuiKey_Apostrophe;
        //            case SDLK_COMMA: return ImGuiKey_Comma;
        //            case SDLK_MINUS: return ImGuiKey_Minus;
        //            case SDLK_PERIOD: return ImGuiKey_Period;
        //            case SDLK_SLASH: return ImGuiKey_Slash;
        //            case SDLK_SEMICOLON: return ImGuiKey_Semicolon;
        //            case SDLK_EQUALS: return ImGuiKey_Equal;
        //            case SDLK_LEFTBRACKET: return ImGuiKey_LeftBracket;
        //            case SDLK_BACKSLASH: return ImGuiKey_Backslash;
        //            case SDLK_RIGHTBRACKET: return ImGuiKey_RightBracket;
        //            case SDLK_BACKQUOTE: return ImGuiKey_GraveAccent;
        //            case SDLK_CAPSLOCK: return ImGuiKey_CapsLock;
        //            case SDLK_SCROLLLOCK: return ImGuiKey_ScrollLock;
        //            case SDLK_NUMLOCKCLEAR: return ImGuiKey_NumLock;
        //            case SDLK_PRINTSCREEN: return ImGuiKey_PrintScreen;
        //            case SDLK_PAUSE: return ImGuiKey_Pause;
        //            case SDLK_KP_0: return ImGuiKey_Keypad0;
        //            case SDLK_KP_1: return ImGuiKey_Keypad1;
        //            case SDLK_KP_2: return ImGuiKey_Keypad2;
        //            case SDLK_KP_3: return ImGuiKey_Keypad3;
        //            case SDLK_KP_4: return ImGuiKey_Keypad4;
        //            case SDLK_KP_5: return ImGuiKey_Keypad5;
        //            case SDLK_KP_6: return ImGuiKey_Keypad6;
        //            case SDLK_KP_7: return ImGuiKey_Keypad7;
        //            case SDLK_KP_8: return ImGuiKey_Keypad8;
        //            case SDLK_KP_9: return ImGuiKey_Keypad9;
        //            case SDLK_KP_PERIOD: return ImGuiKey_KeypadDecimal;
        //            case SDLK_KP_DIVIDE: return ImGuiKey_KeypadDivide;
        //            case SDLK_KP_MULTIPLY: return ImGuiKey_KeypadMultiply;
        //            case SDLK_KP_MINUS: return ImGuiKey_KeypadSubtract;
        //            case SDLK_KP_PLUS: return ImGuiKey_KeypadAdd;
        //            case SDLK_KP_ENTER: return ImGuiKey_KeypadEnter;
        //            case SDLK_KP_EQUALS: return ImGuiKey_KeypadEqual;
        //            case SDLK_LCTRL: return ImGuiKey_LeftCtrl;
        //            case SDLK_LSHIFT: return ImGuiKey_LeftShift;
        //            case SDLK_LALT: return ImGuiKey_LeftAlt;
        //            case SDLK_LGUI: return ImGuiKey_LeftSuper;
        //            case SDLK_RCTRL: return ImGuiKey_RightCtrl;
        //            case SDLK_RSHIFT: return ImGuiKey_RightShift;
        //            case SDLK_RALT: return ImGuiKey_RightAlt;
        //            case SDLK_RGUI: return ImGuiKey_RightSuper;
        //            case SDLK_APPLICATION: return ImGuiKey_Menu;
        //            case SDLK_0: return ImGuiKey_0;
        //            case SDLK_1: return ImGuiKey_1;
        //            case SDLK_2: return ImGuiKey_2;
        //            case SDLK_3: return ImGuiKey_3;
        //            case SDLK_4: return ImGuiKey_4;
        //            case SDLK_5: return ImGuiKey_5;
        //            case SDLK_6: return ImGuiKey_6;
        //            case SDLK_7: return ImGuiKey_7;
        //            case SDLK_8: return ImGuiKey_8;
        //            case SDLK_9: return ImGuiKey_9;
        //            case SDLK_a: return ImGuiKey_A;
        //            case SDLK_b: return ImGuiKey_B;
        //            case SDLK_c: return ImGuiKey_C;
        //            case SDLK_d: return ImGuiKey_D;
        //            case SDLK_e: return ImGuiKey_E;
        //            case SDLK_f: return ImGuiKey_F;
        //            case SDLK_g: return ImGuiKey_G;
        //            case SDLK_h: return ImGuiKey_H;
        //            case SDLK_i: return ImGuiKey_I;
        //            case SDLK_j: return ImGuiKey_J;
        //            case SDLK_k: return ImGuiKey_K;
        //            case SDLK_l: return ImGuiKey_L;
        //            case SDLK_m: return ImGuiKey_M;
        //            case SDLK_n: return ImGuiKey_N;
        //            case SDLK_o: return ImGuiKey_O;
        //            case SDLK_p: return ImGuiKey_P;
        //            case SDLK_q: return ImGuiKey_Q;
        //            case SDLK_r: return ImGuiKey_R;
        //            case SDLK_s: return ImGuiKey_S;
        //            case SDLK_t: return ImGuiKey_T;
        //            case SDLK_u: return ImGuiKey_U;
        //            case SDLK_v: return ImGuiKey_V;
        //            case SDLK_w: return ImGuiKey_W;
        //            case SDLK_x: return ImGuiKey_X;
        //            case SDLK_y: return ImGuiKey_Y;
        //            case SDLK_z: return ImGuiKey_Z;
        //            case SDLK_F1: return ImGuiKey_F1;
        //            case SDLK_F2: return ImGuiKey_F2;
        //            case SDLK_F3: return ImGuiKey_F3;
        //            case SDLK_F4: return ImGuiKey_F4;
        //            case SDLK_F5: return ImGuiKey_F5;
        //            case SDLK_F6: return ImGuiKey_F6;
        //            case SDLK_F7: return ImGuiKey_F7;
        //            case SDLK_F8: return ImGuiKey_F8;
        //            case SDLK_F9: return ImGuiKey_F9;
        //            case SDLK_F10: return ImGuiKey_F10;
        //            case SDLK_F11: return ImGuiKey_F11;
        //            case SDLK_F12: return ImGuiKey_F12;
        //        }
        //        return ImGuiKey_None;
        //    }

        //    static void ImGui_ImplSDL2_UpdateKeyModifiers(SDL_Keymod sdl_key_mods)
        //    {
        //        ImGuiIO & io = ImGui::GetIO();
        //        io.AddKeyEvent(ImGuiMod_Ctrl, (sdl_key_mods & KMOD_CTRL) != 0);
        //        io.AddKeyEvent(ImGuiMod_Shift, (sdl_key_mods & KMOD_SHIFT) != 0);
        //        io.AddKeyEvent(ImGuiMod_Alt, (sdl_key_mods & KMOD_ALT) != 0);
        //        io.AddKeyEvent(ImGuiMod_Super, (sdl_key_mods & KMOD_GUI) != 0);
        //    }

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
        //                io.SetKeyEventNativeData(key, event->key.keysym.sym, event->key.keysym.scancode, event->key.keysym.scancode); // To support legacy indexing (<1.87 user code). Legacy backend uses SDLK_*** as indices to IsKeyXXX() functions.
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

        public static bool Init(Window window, Renderer renderer)
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

            //        // Load mouse cursors
            //        bd->MouseCursors[ImGuiMouseCursor_Arrow] = SDL_CreateSystemCursor(SDL_SYSTEM_CURSOR_ARROW);
            //        bd->MouseCursors[ImGuiMouseCursor_TextInput] = SDL_CreateSystemCursor(SDL_SYSTEM_CURSOR_IBEAM);
            //        bd->MouseCursors[ImGuiMouseCursor_ResizeAll] = SDL_CreateSystemCursor(SDL_SYSTEM_CURSOR_SIZEALL);
            //        bd->MouseCursors[ImGuiMouseCursor_ResizeNS] = SDL_CreateSystemCursor(SDL_SYSTEM_CURSOR_SIZENS);
            //        bd->MouseCursors[ImGuiMouseCursor_ResizeEW] = SDL_CreateSystemCursor(SDL_SYSTEM_CURSOR_SIZEWE);
            //        bd->MouseCursors[ImGuiMouseCursor_ResizeNESW] = SDL_CreateSystemCursor(SDL_SYSTEM_CURSOR_SIZENESW);
            //        bd->MouseCursors[ImGuiMouseCursor_ResizeNWSE] = SDL_CreateSystemCursor(SDL_SYSTEM_CURSOR_SIZENWSE);
            //        bd->MouseCursors[ImGuiMouseCursor_Hand] = SDL_CreateSystemCursor(SDL_SYSTEM_CURSOR_HAND);
            //        bd->MouseCursors[ImGuiMouseCursor_NotAllowed] = SDL_CreateSystemCursor(SDL_SYSTEM_CURSOR_NO);

            //        // Set platform dependent data in viewport
            //#if defined(SDL_VIDEO_DRIVER_WINDOWS)
            //SDL_SysWMinfo info;
            //SDL_VERSION(&info.version);
            //if (SDL_GetWindowWMInfo(window, &info))
            //    ImGui::GetMainViewport()->PlatformHandleRaw = (void*)info.info.win.window;
            //#else
            //        (void)window;
            //#endif

            //        // From 2.0.5: Set SDL hint to receive mouse click events on window focus, otherwise SDL doesn't emit the event.
            //        // Without this, when clicking to gain focus, our widgets wouldn't activate even though they showed as hovered.
            //        // (This is unfortunately a global SDL setting, so enabling it might have a side-effect on your application.
            //        // It is unlikely to make a difference, but if your app absolutely needs to ignore the initial on-focus click:
            //        // you can ignore SDL_MOUSEBUTTONDOWN events coming right after a SDL_WINDOWEVENT_FOCUS_GAINED)
            //# ifdef SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH
            //        SDL_SetHint(SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH, "1");
            //#endif

            //        // From 2.0.22: Disable auto-capture, this is preventing drag and drop across multiple windows (see #5710)
            //# ifdef SDL_HINT_MOUSE_AUTO_CAPTURE
            //        SDL_SetHint(SDL_HINT_MOUSE_AUTO_CAPTURE, "0");
            //#endif

            return true;
        }

        //    void ImGui_ImplSDL2_Shutdown()
        //    {
        //        ImGui_ImplSDL2_Data* bd = ImGui_ImplSDL2_GetBackendData();
        //        IM_ASSERT(bd != nullptr && "No platform backend to shutdown, or already shutdown?");
        //        ImGuiIO & io = ImGui::GetIO();

        //        if (bd->ClipboardTextData)
        //            SDL_free(bd->ClipboardTextData);
        //        for (ImGuiMouseCursor cursor_n = 0; cursor_n < ImGuiMouseCursor_COUNT; cursor_n++)
        //            SDL_FreeCursor(bd->MouseCursors[cursor_n]);

        //        io.BackendPlatformName = nullptr;
        //        io.BackendPlatformUserData = nullptr;
        //        IM_DELETE(bd);
        //    }

        //    static void ImGui_ImplSDL2_UpdateMouseData()
        //    {
        //        ImGui_ImplSDL2_Data* bd = ImGui_ImplSDL2_GetBackendData();
        //        ImGuiIO & io = ImGui::GetIO();

        //        // We forward mouse input when hovered or captured (via SDL_MOUSEMOTION) or when focused (below)
        //#if SDL_HAS_CAPTURE_AND_GLOBAL_MOUSE
        //// SDL_CaptureMouse() let the OS know e.g. that our imgui drag outside the SDL window boundaries shouldn't e.g. trigger other operations outside
        //SDL_CaptureMouse((bd->MouseButtonsDown != 0 && ImGui::GetDragDropPayload() == nullptr) ? SDL_TRUE : SDL_FALSE);
        //SDL_Window* focused_window = SDL_GetKeyboardFocus();
        //const bool is_app_focused = (bd->Window == focused_window);
        //#else
        //        const bool is_app_focused = (SDL_GetWindowFlags(bd->Window) & SDL_WINDOW_INPUT_FOCUS) != 0; // SDL 2.0.3 and non-windowed systems: single-viewport only
        //#endif
        //        if (is_app_focused)
        //        {
        //            // (Optional) Set OS mouse position from Dear ImGui if requested (rarely used, only when ImGuiConfigFlags_NavEnableSetMousePos is enabled by user)
        //            if (io.WantSetMousePos)
        //                SDL_WarpMouseInWindow(bd->Window, (int)io.MousePos.x, (int)io.MousePos.y);

        //            // (Optional) Fallback to provide mouse position when focused (SDL_MOUSEMOTION already provides this when hovered or captured)
        //            if (bd->MouseButtonsDown == 0)
        //            {
        //                int window_x, window_y, mouse_x_global, mouse_y_global;
        //                SDL_GetGlobalMouseState(&mouse_x_global, &mouse_y_global);
        //                SDL_GetWindowPosition(bd->Window, &window_x, &window_y);
        //                io.AddMousePosEvent((float)(mouse_x_global - window_x), (float)(mouse_y_global - window_y));
        //            }
        //        }
        //    }

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
        //        MAP_BUTTON(ImGuiKey_GamepadStart, SDL_CONTROLLER_BUTTON_START);
        //        MAP_BUTTON(ImGuiKey_GamepadBack, SDL_CONTROLLER_BUTTON_BACK);
        //        MAP_BUTTON(ImGuiKey_GamepadFaceLeft, SDL_CONTROLLER_BUTTON_X);              // Xbox X, PS Square
        //        MAP_BUTTON(ImGuiKey_GamepadFaceRight, SDL_CONTROLLER_BUTTON_B);              // Xbox B, PS Circle
        //        MAP_BUTTON(ImGuiKey_GamepadFaceUp, SDL_CONTROLLER_BUTTON_Y);              // Xbox Y, PS Triangle
        //        MAP_BUTTON(ImGuiKey_GamepadFaceDown, SDL_CONTROLLER_BUTTON_A);              // Xbox A, PS Cross
        //        MAP_BUTTON(ImGuiKey_GamepadDpadLeft, SDL_CONTROLLER_BUTTON_DPAD_LEFT);
        //        MAP_BUTTON(ImGuiKey_GamepadDpadRight, SDL_CONTROLLER_BUTTON_DPAD_RIGHT);
        //        MAP_BUTTON(ImGuiKey_GamepadDpadUp, SDL_CONTROLLER_BUTTON_DPAD_UP);
        //        MAP_BUTTON(ImGuiKey_GamepadDpadDown, SDL_CONTROLLER_BUTTON_DPAD_DOWN);
        //        MAP_BUTTON(ImGuiKey_GamepadL1, SDL_CONTROLLER_BUTTON_LEFTSHOULDER);
        //        MAP_BUTTON(ImGuiKey_GamepadR1, SDL_CONTROLLER_BUTTON_RIGHTSHOULDER);
        //        MAP_ANALOG(ImGuiKey_GamepadL2, SDL_CONTROLLER_AXIS_TRIGGERLEFT, 0.0f, 32767);
        //        MAP_ANALOG(ImGuiKey_GamepadR2, SDL_CONTROLLER_AXIS_TRIGGERRIGHT, 0.0f, 32767);
        //        MAP_BUTTON(ImGuiKey_GamepadL3, SDL_CONTROLLER_BUTTON_LEFTSTICK);
        //        MAP_BUTTON(ImGuiKey_GamepadR3, SDL_CONTROLLER_BUTTON_RIGHTSTICK);
        //        MAP_ANALOG(ImGuiKey_GamepadLStickLeft, SDL_CONTROLLER_AXIS_LEFTX, -thumb_dead_zone, -32768);
        //        MAP_ANALOG(ImGuiKey_GamepadLStickRight, SDL_CONTROLLER_AXIS_LEFTX, +thumb_dead_zone, +32767);
        //        MAP_ANALOG(ImGuiKey_GamepadLStickUp, SDL_CONTROLLER_AXIS_LEFTY, -thumb_dead_zone, -32768);
        //        MAP_ANALOG(ImGuiKey_GamepadLStickDown, SDL_CONTROLLER_AXIS_LEFTY, +thumb_dead_zone, +32767);
        //        MAP_ANALOG(ImGuiKey_GamepadRStickLeft, SDL_CONTROLLER_AXIS_RIGHTX, -thumb_dead_zone, -32768);
        //        MAP_ANALOG(ImGuiKey_GamepadRStickRight, SDL_CONTROLLER_AXIS_RIGHTX, +thumb_dead_zone, +32767);
        //        MAP_ANALOG(ImGuiKey_GamepadRStickUp, SDL_CONTROLLER_AXIS_RIGHTY, -thumb_dead_zone, -32768);
        //        MAP_ANALOG(ImGuiKey_GamepadRStickDown, SDL_CONTROLLER_AXIS_RIGHTY, +thumb_dead_zone, +32767);
        //#undef MAP_BUTTON
        //#undef MAP_ANALOG
        //    }

        //    void ImGui_ImplSDL2_NewFrame()
        //    {
        //        ImGui_ImplSDL2_Data* bd = ImGui_ImplSDL2_GetBackendData();
        //        IM_ASSERT(bd != nullptr && "Did you call ImGui_ImplSDL2_Init()?");
        //        ImGuiIO & io = ImGui::GetIO();

        //        // Setup display size (every frame to accommodate for window resizing)
        //        int w, h;
        //        int display_w, display_h;
        //        SDL_GetWindowSize(bd->Window, &w, &h);
        //        if (SDL_GetWindowFlags(bd->Window) & SDL_WINDOW_MINIMIZED)
        //            w = h = 0;
        //        if (bd->Renderer != nullptr)
        //            SDL_GetRendererOutputSize(bd->Renderer, &display_w, &display_h);
        //        else
        //            SDL_GL_GetDrawableSize(bd->Window, &display_w, &display_h);
        //        io.DisplaySize = ImVec2((float)w, (float)h);
        //        if (w > 0 && h > 0)
        //            io.DisplayFramebufferScale = ImVec2((float)display_w / w, (float)display_h / h);

        //        // Setup time step (we don't use SDL_GetTicks() because it is using millisecond resolution)
        //        static Uint64 frequency = SDL_GetPerformanceFrequency();
        //        Uint64 current_time = SDL_GetPerformanceCounter();
        //        io.DeltaTime = bd->Time > 0 ? (float)((double)(current_time - bd->Time) / frequency) : (float)(1.0f / 60.0f);
        //        bd->Time = current_time;

        //        if (bd->PendingMouseLeaveFrame && bd->PendingMouseLeaveFrame >= ImGui::GetFrameCount() && bd->MouseButtonsDown == 0)
        //        {
        //            io.AddMousePosEvent(-FLT_MAX, -FLT_MAX);
        //            bd->PendingMouseLeaveFrame = 0;
        //        }

        //        ImGui_ImplSDL2_UpdateMouseData();
        //        ImGui_ImplSDL2_UpdateMouseCursor();

        //        // Update game controllers (if enabled and available)
        //        ImGui_ImplSDL2_UpdateGamepads();
        //    }
    }
}
