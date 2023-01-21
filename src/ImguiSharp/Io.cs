namespace ImguiSharp
{
    public readonly unsafe struct Io
    {
        private readonly Native.ImGuiIO* _io;

        public ConfigOptions ConfigOptions
        {
            get => (ConfigOptions)_io->ConfigFlags;
            set => _io->ConfigFlags = (Native.ImGuiConfigFlags)value;
        }

        public BackendOptions BackendOptions
        {
            get => (BackendOptions)_io->BackendFlags;
            set => _io->BackendFlags = (Native.ImGuiBackendFlags)value;
        }

        public Size DisplaySize
        {
            get => new(_io->DisplaySize);
            set => _io->DisplaySize = value.ToNative();
        }

        public float DeltaTime => _io->DeltaTime;

        public float IniSavingRate
        {
            get => _io->IniSavingRate;
            set => _io->IniSavingRate = value;
        }

        public string? IniFilename
        {
            get => Native.Utf8ToString(_io->IniFilename);
            set
            {
                // CONSIDER: In this case, if the string we're replacing isn't static, we may have a (small) leak
                var source = Native.StringToUtf8(value);
                var ptr = Native.ImGui_MemAlloc((nuint)source.Length);
                source.CopyTo(new Span<byte>(ptr, source.Length));
                _io->IniFilename = (byte*)ptr;
            }
        }

        public string? LogFilename
        {
            get => Native.Utf8ToString(_io->LogFilename);
            set
            {
                // CONSIDER: In this case, if the string we're replacing isn't static, we may have a (small) leak
                var source = Native.StringToUtf8(value);
                var ptr = Native.ImGui_MemAlloc((nuint)source.Length);
                source.CopyTo(new Span<byte>(ptr, source.Length));
                _io->LogFilename = (byte*)ptr;
            }
        }

        public float MouseDoubleClickTime
        {
            get => _io->MouseDoubleClickTime;
            set => _io->MouseDoubleClickTime = value;
        }
        public float MouseDoubleClickMaxDist
        {
            get => _io->MouseDoubleClickMaxDist;
            set => _io->MouseDoubleClickMaxDist = value;
        }
        public float MouseDragThreshold
        {
            get => _io->MouseDragThreshold;
            set => _io->MouseDragThreshold = value;
        }
        public float KeyRepeatDelay
        {
            get => _io->KeyRepeatDelay;
            set => _io->KeyRepeatDelay = value;
        }
        public float KeyRepeatRate
        {
            get => _io->KeyRepeatRate;
            set => _io->KeyRepeatRate = value;
        }
        public float HoverDelayNormal
        {
            get => _io->HoverDelayNormal;
            set => _io->HoverDelayNormal = value;
        }
        public float HoverDelayShort
        {
            get => _io->HoverDelayShort;
            set => _io->HoverDelayShort = value;
        }

        public nuint UserData
        {
            get => (nuint)_io->UserData;
            set => _io->UserData = (void*)value;
        }

        public FontAtlas FontAtlas => new(_io->Fonts);

        public float FontGlobalScale
        {
            get => _io->FontGlobalScale;
            set => _io->FontGlobalScale = value;
        }

        public bool FontAllowUserScaling
        {
            get => _io->FontAllowUserScaling;
            set => _io->FontAllowUserScaling = value;
        }

        public Font? FontDefault
        {
            get => _io->FontDefault == null ? null : new(_io->FontDefault);
            set => _io->FontDefault = value == null ? null : value.Value.ToNative();
        }

        public Size DisplayFramebufferScale
        {
            get => new(_io->DisplayFramebufferScale);
            set => _io->DisplayFramebufferScale = value.ToNative();
        }

        public bool MouseDrawCursor
        {
            get => _io->MouseDrawCursor;
            set => _io->MouseDrawCursor = value;
        }
        public bool ConfigMacOSXBehaviors
        {
            get => _io->ConfigMacOSXBehaviors;
            set => _io->ConfigMacOSXBehaviors = value;
        }

        public bool ConfigInputTrickleEventQueue
        {
            get => _io->ConfigInputTrickleEventQueue;
            set => _io->ConfigInputTrickleEventQueue = value;
        }

        public bool ConfigInputTextCursorBlink
        {
            get => _io->ConfigInputTextCursorBlink;
            set => _io->ConfigInputTextCursorBlink = value;
        }

        public bool ConfigInputTextEnterKeepActive
        {
            get => _io->ConfigInputTextEnterKeepActive;
            set => _io->ConfigInputTextEnterKeepActive = value;
        }

        public bool ConfigDragClickToInputText
        {
            get => _io->ConfigDragClickToInputText;
            set => _io->ConfigDragClickToInputText = value;
        }

        public bool ConfigWindowsResizeFromEdges
        {
            get => _io->ConfigWindowsResizeFromEdges;
            set => _io->ConfigWindowsResizeFromEdges = value;
        }
        public bool ConfigWindowsMoveFromTitleBarOnly
        {
            get => _io->ConfigWindowsMoveFromTitleBarOnly;
            set => _io->ConfigWindowsMoveFromTitleBarOnly = value;
        }

        public float ConfigMemoryCompactTimer
        {
            get => _io->ConfigMemoryCompactTimer;
            set => _io->ConfigMemoryCompactTimer = value;
        }

        public string? BackendPlatformName
        {
            get => Native.Utf8ToString(_io->BackendPlatformName);
            set
            {
                if (_io->BackendPlatformName != null)
                {
                    throw new InvalidOperationException();
                }

                var source = Native.StringToUtf8(value);
                var ptr = Native.ImGui_MemAlloc((nuint)source.Length);
                source.CopyTo(new Span<byte>(ptr, source.Length));
                _io->BackendPlatformName = (byte*)ptr;
            }
        }

        public string? BackendRendererName
        {
            get => Native.Utf8ToString(_io->BackendRendererName);
            set
            {
                if (_io->BackendRendererName != null)
                {
                    throw new InvalidOperationException();
                }

                var source = Native.StringToUtf8(value);
                var ptr = Native.ImGui_MemAlloc((nuint)source.Length);
                source.CopyTo(new Span<byte>(ptr, source.Length));
                _io->BackendRendererName = (byte*)ptr;
            }
        }

        public nuint BackendPlatformUserData
        {
            get => (nuint)_io->BackendPlatformUserData;
            set => _io->BackendPlatformUserData = (void*)value;
        }

        public nuint BackendRendererUserData
        {
            get => (nuint)_io->BackendRendererUserData;
            set => _io->BackendRendererUserData = (void*)value;
        }

        public nuint BackendLanguageUserData
        {
            get => (nuint)_io->BackendLanguageUserData;
            set => _io->BackendLanguageUserData = (void*)value;
        }

        public delegate* unmanaged[Cdecl]<void*, byte*> GetClipboardText
        {
            set => _io->GetClipboardTextFn = value;
        }

        public delegate* unmanaged[Cdecl]<void*, byte*, void> SetClipboardText
        {
            set => _io->SetClipboardTextFn = value;
        }

        public nuint ClipboardUserData
        {
            get => (nuint)_io->ClipboardUserData;
            set => _io->ClipboardUserData = (void*)value;
        }

#if false
    //------------------------------------------------------------------
    // Platform Functions
    // (the imgui_impl_xxxx backend files are setting those up for you)
    //------------------------------------------------------------------

    // Optional: Access OS clipboard
    // (default to use native Win32 clipboard on Windows, otherwise uses a private clipboard. Override to access OS clipboard on other architectures)
    const char* (*GetClipboardTextFn)(void* user_data);
    void        (*SetClipboardTextFn)(void* user_data, const char* text);
    void*       ClipboardUserData;

    // Optional: Notify OS Input Method Editor of the screen position of your cursor for text input position (e.g. when using Japanese/Chinese IME on Windows)
    // (default to use native imm32 api on Windows)
    void        (*SetPlatformImeDataFn)(ImGuiViewport* viewport, ImGuiPlatformImeData* data);
    void* _UnusedPadding;                                     // Unused field to keep data structure the same size.

        //------------------------------------------------------------------
        // Input - Call before calling NewFrame()
        //------------------------------------------------------------------

        // Input Functions
        IMGUI_API void AddKeyEvent(ImGuiKey key, bool down);                   // Queue a new key down/up event. Key should be "translated" (as in, generally ImGuiKey_A matches the key end-user would use to emit an 'A' character)
        IMGUI_API void AddKeyAnalogEvent(ImGuiKey key, bool down, float v);    // Queue a new key down/up event for analog values (e.g. ImGuiKey_Gamepad_ values). Dead-zones should be handled by the backend.
        IMGUI_API void AddMousePosEvent(float x, float y);                     // Queue a mouse position update. Use -FLT_MAX,-FLT_MAX to signify no mouse (e.g. app not focused and not hovered)
        IMGUI_API void AddMouseButtonEvent(int button, bool down);             // Queue a mouse button change
        IMGUI_API void AddMouseWheelEvent(float wh_x, float wh_y);             // Queue a mouse wheel update
        IMGUI_API void AddFocusEvent(bool focused);                            // Queue a gain/loss of focus for the application (generally based on OS/platform focus of your window)
        IMGUI_API void AddInputCharacter(unsigned int c);                      // Queue a new character input
        IMGUI_API void AddInputCharacterUTF16(ImWchar16 c);                    // Queue a new character input from a UTF-16 character, it can be a surrogate
        IMGUI_API void AddInputCharactersUTF8(const char* str);                // Queue a new characters input from a UTF-8 string

        IMGUI_API void SetKeyEventNativeData(ImGuiKey key, int native_keycode, int native_scancode, int native_legacy_index = -1); // [Optional] Specify index for legacy <1.87 IsKeyXXX() functions with native indices + specify native keycode, scancode.
        IMGUI_API void SetAppAcceptingEvents(bool accepting_events);           // Set master flag for accepting key/mouse/text events (default to true). Useful if you have native dialog boxes that are interrupting your application loop/refresh, and you want to disable events being queued while your app is frozen.
        IMGUI_API void ClearInputCharacters();                                 // [Internal] Clear the text input buffer manually
        IMGUI_API void ClearInputKeys();                                       // [Internal] Release all keys

        //------------------------------------------------------------------
        // Output - Updated by NewFrame() or EndFrame()/Render()
        // (when reading from the io.WantCaptureMouse, io.WantCaptureKeyboard flags to dispatch your inputs, it is
        //  generally easier and more correct to use their state BEFORE calling NewFrame(). See FAQ for details!)
        //------------------------------------------------------------------

        bool WantCaptureMouse;                   // Set when Dear ImGui will use mouse inputs, in this case do not dispatch them to your main game/application (either way, always pass on mouse inputs to imgui). (e.g. unclicked mouse is hovering over an imgui window, widget is active, mouse was clicked over an imgui window, etc.).
        bool WantCaptureKeyboard;                // Set when Dear ImGui will use keyboard inputs, in this case do not dispatch them to your main game/application (either way, always pass keyboard inputs to imgui). (e.g. InputText active, or an imgui window is focused and navigation is enabled, etc.).
        bool WantTextInput;                      // Mobile/console: when set, you may display an on-screen keyboard. This is set by Dear ImGui when it wants textual keyboard input to happen (e.g. when a InputText widget is active).
        bool WantSetMousePos;                    // MousePos has been altered, backend should reposition mouse on next frame. Rarely used! Set only when ImGuiConfigFlags_NavEnableSetMousePos flag is enabled.
        bool WantSaveIniSettings;                // When manual .ini load/save is active (io.IniFilename == NULL), this will be set to notify your application that you can call SaveIniSettingsToMemory() and save yourself. Important: clear io.WantSaveIniSettings yourself after saving!
        bool NavActive;                          // Keyboard/Gamepad navigation is currently allowed (will handle ImGuiKey_NavXXX events) = a window is focused and it doesn't use the ImGuiWindowFlags_NoNavInputs flag.
        bool NavVisible;                         // Keyboard/Gamepad navigation is visible and allowed (will handle ImGuiKey_NavXXX events).
        float Framerate;                          // Estimate of application framerate (rolling average over 60 frames, based on io.DeltaTime), in frame per second. Solely for convenience. Slow applications may not want to use a moving average or may want to reset underlying buffers occasionally.
        int MetricsRenderVertices;              // Vertices output during last call to Render()
        int MetricsRenderIndices;               // Indices output during last call to Render() = number of triangles * 3
        int MetricsRenderWindows;               // Number of visible windows
        int MetricsActiveWindows;               // Number of active windows
        int MetricsActiveAllocations;           // Number of active allocations, updated by MemAlloc/MemFree based on current context. May be off if you have multiple imgui contexts.
        ImVec2 MouseDelta;                         // Mouse delta. Note that this is zero if either current or previous position are invalid (-FLT_MAX,-FLT_MAX), so a disappearing/reappearing mouse won't have a huge delta.
#endif

        internal Io(Native.ImGuiIO* io)
        {
            _io = io;
        }

        internal Native.ImGuiIO* ToNative() => _io;
    }
}
