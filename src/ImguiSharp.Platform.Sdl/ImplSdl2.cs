using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SdlSharp;
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
                _mouseCursors = new Cursor[(int)MouseCursor.Count];
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

            var utf8Clipboard = Native.StringToUtf8(Clipboard.Text);
            var buffer = (byte*)SdlSharp.Native.SDL_malloc((nuint)utf8Clipboard.Length);
            utf8Clipboard.CopyTo(new Span<byte>(buffer, utf8Clipboard.Length));
            bd._clipboardTextData = buffer;

            return bd._clipboardTextData;
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static void SetClipboardText(void* unused, byte* text) => _ = SdlSharp.Native.SDL_SetClipboardText(text);

        private static Key KeycodeToImGuiKey(Keycode keycode)
        {
            return keycode switch
            {
                Keycode.Tab => Key.Tab,
                Keycode.Left => Key.LeftArrow,
                Keycode.Right => Key.RightArrow,
                Keycode.Up => Key.UpArrow,
                Keycode.Down => Key.DownArrow,
                Keycode.PageUp => Key.PageUp,
                Keycode.PageDown => Key.PageDown,
                Keycode.Home => Key.Home,
                Keycode.End => Key.End,
                Keycode.Insert => Key.Insert,
                Keycode.Delete => Key.Delete,
                Keycode.Backspace => Key.Backspace,
                Keycode.Space => Key.Space,
                Keycode.Return => Key.Enter,
                Keycode.Escape => Key.Escape,
                Keycode.Quote => Key.Apostrophe,
                Keycode.Comma => Key.Comma,
                Keycode.Minus => Key.Minus,
                Keycode.Period => Key.Period,
                Keycode.Slash => Key.Slash,
                Keycode.Semicolon => Key.Semicolon,
                Keycode.Equals => Key.Equal,
                Keycode.LeftBracket => Key.LeftBracket,
                Keycode.Backslash => Key.Backslash,
                Keycode.RightBracket => Key.RightBracket,
                Keycode.Backquote => Key.GraveAccent,
                Keycode.Capslock => Key.CapsLock,
                Keycode.ScrollLock => Key.ScrollLock,
                Keycode.NumLockClear => Key.NumLock,
                Keycode.PrintScreen => Key.PrintScreen,
                Keycode.Pause => Key.Pause,
                Keycode.NumPad0 => Key.Keypad0,
                Keycode.NumPad1 => Key.Keypad1,
                Keycode.NumPad2 => Key.Keypad2,
                Keycode.NumPad3 => Key.Keypad3,
                Keycode.NumPad4 => Key.Keypad4,
                Keycode.NumPad5 => Key.Keypad5,
                Keycode.NumPad6 => Key.Keypad6,
                Keycode.NumPad7 => Key.Keypad7,
                Keycode.NumPad8 => Key.Keypad8,
                Keycode.NumPad9 => Key.Keypad9,
                Keycode.NumPadPeriod => Key.KeypadDecimal,
                Keycode.NumPadDivide => Key.KeypadDivide,
                Keycode.NumPadMultiply => Key.KeypadMultiply,
                Keycode.NumPadMinus => Key.KeypadSubtract,
                Keycode.NumPadPlus => Key.KeypadAdd,
                Keycode.NumPadEnter => Key.KeypadEnter,
                Keycode.NumPadEquals => Key.KeypadEqual,
                Keycode.LeftCtrl => Key.LeftCtrl,
                Keycode.LeftShift => Key.LeftShift,
                Keycode.LeftAlt => Key.LeftAlt,
                Keycode.LeftGui => Key.LeftSuper,
                Keycode.RightCtrl => Key.RightCtrl,
                Keycode.RightShift => Key.RightShift,
                Keycode.RightAlt => Key.RightAlt,
                Keycode.RightGui => Key.RightSuper,
                Keycode.Application => Key.Menu,
                Keycode.Number0 => Key.Number0,
                Keycode.Number1 => Key.Number1,
                Keycode.Number2 => Key.Number2,
                Keycode.Number3 => Key.Number3,
                Keycode.Number4 => Key.Number4,
                Keycode.Number5 => Key.Number5,
                Keycode.Number6 => Key.Number6,
                Keycode.Number7 => Key.Number7,
                Keycode.Number8 => Key.Number8,
                Keycode.Number9 => Key.Number9,
                Keycode.a => Key.A,
                Keycode.b => Key.B,
                Keycode.c => Key.C,
                Keycode.d => Key.D,
                Keycode.e => Key.E,
                Keycode.f => Key.F,
                Keycode.g => Key.G,
                Keycode.h => Key.H,
                Keycode.i => Key.I,
                Keycode.j => Key.J,
                Keycode.k => Key.K,
                Keycode.l => Key.L,
                Keycode.m => Key.M,
                Keycode.n => Key.N,
                Keycode.o => Key.O,
                Keycode.p => Key.P,
                Keycode.q => Key.Q,
                Keycode.r => Key.R,
                Keycode.s => Key.S,
                Keycode.t => Key.T,
                Keycode.u => Key.U,
                Keycode.v => Key.V,
                Keycode.w => Key.W,
                Keycode.x => Key.X,
                Keycode.y => Key.Y,
                Keycode.z => Key.Z,
                Keycode.F1 => Key.F1,
                Keycode.F2 => Key.F2,
                Keycode.F3 => Key.F3,
                Keycode.F4 => Key.F4,
                Keycode.F5 => Key.F5,
                Keycode.F6 => Key.F6,
                Keycode.F7 => Key.F7,
                Keycode.F8 => Key.F8,
                Keycode.F9 => Key.F9,
                Keycode.F10 => Key.F10,
                Keycode.F11 => Key.F11,
                Keycode.F12 => Key.F12,
                _ => Key.None,
            };
        }

        private static void UpdateKeyModifiers(KeyModifier keyModifiers)
        {
            var io = Imgui.GetIo();
            io.AddKeyEvent(Key.ModCtrl, (keyModifiers & KeyModifier.Ctrl) != 0);
            io.AddKeyEvent(Key.ModShift, (keyModifiers & KeyModifier.Shift) != 0);
            io.AddKeyEvent(Key.ModAlt, (keyModifiers & KeyModifier.Alt) != 0);
            io.AddKeyEvent(Key.ModSuper, (keyModifiers & KeyModifier.Gui) != 0);
        }

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

            bd._mouseCursors[(int)MouseCursor.Arrow] = Cursor.Create(SystemCursor.Arrow);
            bd._mouseCursors[(int)MouseCursor.TextInput] = Cursor.Create(SystemCursor.IBeam);
            bd._mouseCursors[(int)MouseCursor.ResizeAll] = Cursor.Create(SystemCursor.SizeAll);
            bd._mouseCursors[(int)MouseCursor.ResizeNS] = Cursor.Create(SystemCursor.SizeNS);
            bd._mouseCursors[(int)MouseCursor.ResizeEW] = Cursor.Create(SystemCursor.SizeWE);
            bd._mouseCursors[(int)MouseCursor.ResizeNESW] = Cursor.Create(SystemCursor.SizeNESW);
            bd._mouseCursors[(int)MouseCursor.ResizeNWSE] = Cursor.Create(SystemCursor.SizeNWSE);
            bd._mouseCursors[(int)MouseCursor.Hand] = Cursor.Create(SystemCursor.Hand);
            bd._mouseCursors[(int)MouseCursor.NotAllowed] = Cursor.Create(SystemCursor.No);

            var wmInfo = window.WindowManagerInfo;
            if (wmInfo != null)
            {
                Imgui.GetMainViewport()->PlatformHandleRaw = (void*)wmInfo.Value.Window;
            }

            _ = Hint.MouseFocusClickthrough.Set("1");
            _ = Hint.MouseAutoCapture.Set("0");

            Mouse.Motion += static (s, e) =>
            {
                var io = Imgui.GetIo();
                io.AddMousePosEvent(new(e.Location.X, e.Location.Y));
            };

            Mouse.Wheel += static (s, e) =>
            {
                var io = Imgui.GetIo();
                var wheelX = (e.Location.X > 0) ? 1.0f : (e.Location.X < 0) ? -1.0f : 0.0f;
                var wheelY = (e.Location.Y > 0) ? 1.0f : (e.Location.Y < 0) ? -1.0f : 0.0f;
                io.AddMouseWheelEvent(new(wheelX, wheelY));
            };

            static int TranslateMouseButton(MouseButton button)
            {
                return button switch
                {
                    MouseButton.Left => 0,
                    MouseButton.Right => 1,
                    MouseButton.Middle => 2,
                    MouseButton.X1 => 3,
                    MouseButton.X2 => 4,
                    _ => -1
                };
            }

            Mouse.ButtonDown += static (s, e) =>
            {
                var bd = GetBackendData();
                if (bd == null)
                {
                    throw new InvalidOperationException();
                }

                var io = Imgui.GetIo();
                var mouseButton = TranslateMouseButton(e.Button);
                if (mouseButton == -1)
                {
                    return;
                }

                io.AddMouseButtonEvent(mouseButton, true);
                bd._mouseButtonsDown |= 1 << mouseButton;
            };

            Mouse.ButtonUp += static (s, e) =>
            {
                var bd = GetBackendData();
                if (bd == null)
                {
                    throw new InvalidOperationException();
                }

                var io = Imgui.GetIo();
                var mouseButton = TranslateMouseButton(e.Button);
                if (mouseButton == -1)
                {
                    return;
                }

                io.AddMouseButtonEvent(mouseButton, false);
                bd._mouseButtonsDown &= ~(1 << mouseButton);
            };

            Keyboard.TextInput += static (s, e) =>
            {
                var io = Imgui.GetIo();
                io.AddInputCharactersUTF8(e.Text);
            };


            Keyboard.KeyDown += static (s, e) =>
            {
                var io = Imgui.GetIo();
                UpdateKeyModifiers(e.Modifiers);
                io.AddKeyEvent(KeycodeToImGuiKey(e.Keycode), true);
            };

            Keyboard.KeyUp += static (s, e) =>
            {
                var io = Imgui.GetIo();
                UpdateKeyModifiers(e.Modifiers);
                io.AddKeyEvent(KeycodeToImGuiKey(e.Keycode), false);
            };

            Window.Entered += static (s, e) =>
            {
                var bd = GetBackendData();
                if (bd == null)
                {
                    throw new InvalidOperationException();
                }

                bd._pendingMouseLeaveFrame = 0;
            };

            Window.Left += static (s, e) =>
            {
                var bd = GetBackendData();
                if (bd == null)
                {
                    throw new InvalidOperationException();
                }

                bd._pendingMouseLeaveFrame = Imgui.GetFrameCount() + 1;
            };

            Window.FocusGained += static (s, e) =>
            {
                var io = Imgui.GetIo();
                io.AddFocusEvent(true);
            };

            Window.FocusLost += static (s, e) =>
            {
                var io = Imgui.GetIo();
                io.AddFocusEvent(false);
            };

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
                bd._mouseCursors[(int)cursor].Dispose();
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

            Mouse.Capture(bd._mouseButtonsDown != 0 && Imgui.GetDragDropPayload() == null);
            var focusedWindow = Keyboard.Focus;

            if (bd._window == focusedWindow)
            {
                if (io.WantSetMousePos)
                {
                    Mouse.Warp(bd._window, new((int)io.MousePosition.X, (int)io.MousePosition.Y));
                }

                if (bd._mouseButtonsDown == 0)
                {
                    var mouseGlobal = Mouse.GlobalState.Position;
                    var window = bd._window.Position;
                    io.AddMousePosEvent(new(mouseGlobal.X - window.X, mouseGlobal.Y - window.Y));
                }
            }
        }

        private static void UpdateMouseCursor()
        {
            var io = Imgui.GetIo();

            if ((io.ConfigOptions & ConfigOptions.NoMouseCursorChange) != 0)
            {
                return;
            }

            var bd = GetBackendData();
            if (bd == null)
            {
                throw new InvalidOperationException();
            }

            MouseCursor imguiCursor = Imgui.GetMouseCursor();
            if (io.MouseDrawCursor || imguiCursor == MouseCursor.None)
            {
                _ = Cursor.Show(State.Disable);
            }
            else
            {
                Cursor.Current = bd._mouseCursors[(int)imguiCursor] ?? bd._mouseCursors[(int)MouseCursor.Arrow];
                _ = Cursor.Show(State.Enable);
            }
        }

        private static void UpdateGamepads()
        {
            var io = Imgui.GetIo();

            if ((io.ConfigOptions & ConfigOptions.NavEnableGamepad) == 0)
            {
                return;
            }

            io.BackendOptions &= ~BackendOptions.HasGamepad;
            var gameController = Joystick.Infos[0].OpenGameController();
            if (gameController == null)
            {
                return;
            }
            io.BackendOptions |= BackendOptions.HasGamepad;

            static float Saturate(float v) => v < 0.0f ? 0.0f : v > 1.0f ? 1.0f : v;

            void MapButton(Key key, GameControllerButton button) => io.AddKeyEvent(key, gameController.GetButton(button));

            void MapAnalog(Key key, GameControllerAxis axis, short v0, short v1)
            {
                var vn = (float)(gameController.GetAxis(axis) - v0) / (v1 - v0);
                vn = Saturate(vn);
                io.AddKeyAnalogEvent(key, vn > 0.1f, vn);
            }

            const int ThumbDeadZone = 8000;

            MapButton(Key.GamepadStart, GameControllerButton.Start);
            MapButton(Key.GamepadBack, GameControllerButton.Back);
            MapButton(Key.GamepadFaceLeft, GameControllerButton.X);
            MapButton(Key.GamepadFaceRight, GameControllerButton.B);
            MapButton(Key.GamepadFaceUp, GameControllerButton.Y);
            MapButton(Key.GamepadFaceDown, GameControllerButton.A);
            MapButton(Key.GamepadDpadLeft, GameControllerButton.DpadLeft);
            MapButton(Key.GamepadDpadRight, GameControllerButton.DpadRight);
            MapButton(Key.GamepadDpadUp, GameControllerButton.DpadUp);
            MapButton(Key.GamepadDpadDown, GameControllerButton.DpadDown);
            MapButton(Key.GamepadL1, GameControllerButton.LeftShoulder);
            MapButton(Key.GamepadR1, GameControllerButton.RightShoulder);
            MapAnalog(Key.GamepadL2, GameControllerAxis.TriggerLeft, 0, 32767);
            MapAnalog(Key.GamepadR2, GameControllerAxis.TriggerRight, 0, 32767);
            MapButton(Key.GamepadL3, GameControllerButton.LeftStick);
            MapButton(Key.GamepadR3, GameControllerButton.RightStick);
            MapAnalog(Key.GamepadLStickLeft, GameControllerAxis.LeftX, -ThumbDeadZone, -32768);
            MapAnalog(Key.GamepadLStickRight, GameControllerAxis.LeftX, +ThumbDeadZone, +32767);
            MapAnalog(Key.GamepadLStickUp, GameControllerAxis.LeftY, -ThumbDeadZone, -32768);
            MapAnalog(Key.GamepadLStickDown, GameControllerAxis.LeftY, +ThumbDeadZone, +32767);
            MapAnalog(Key.GamepadRStickLeft, GameControllerAxis.RightX, -ThumbDeadZone, -32768);
            MapAnalog(Key.GamepadRStickRight, GameControllerAxis.RightX, +ThumbDeadZone, +32767);
            MapAnalog(Key.GamepadRStickUp, GameControllerAxis.RightY, -ThumbDeadZone, -32768);
            MapAnalog(Key.GamepadRStickDown, GameControllerAxis.RightY, +ThumbDeadZone, +32767);
        }

        public static void NewFrame()
        {
            var bd = GetBackendData();
            if (bd == null)
            {
                throw new InvalidOperationException();
            }

            var io = Imgui.GetIo();

            var size = bd._window.Size;
            if ((bd._window.Flags & SdlSharp.Graphics.WindowOptions.Minimized) != 0)
            {
                size = default;
            }

            var displaySize = bd._renderer.OutputSize;

            io.DisplaySize = new(size.Width, size.Height);
            if (size.Width > 0 && size.Height > 0)
            {
                io.DisplayFramebufferScale = new((float)displaySize.Width / size.Width, (float)displaySize.Height / size.Height);
            }

            var frequency = SdlSharp.Timer.PerformanceFrequency;
            var current_time = SdlSharp.Timer.PerformanceCounter;
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
