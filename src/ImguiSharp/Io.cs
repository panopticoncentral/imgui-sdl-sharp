namespace ImguiSharp
{
    public readonly unsafe struct Io : INativeWrapper<Io, Native.ImGuiIO>
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

        public float DeltaTime
        {
            get => _io->DeltaTime;
            set => _io->DeltaTime = value;
        }

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

        public FontAtlas Fonts => FontAtlas.Wrap(_io->Fonts);

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
            get => _io->FontDefault == null ? null : Font.Wrap(_io->FontDefault);
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

        public delegate* unmanaged[Cdecl]<Native.ImGuiViewport*, Native.ImGuiPlatformImeData*, void> SetPlatformImeData
        {
            set => _io->SetPlatformImeDataFn = value;
        }

        public bool WantCaptureMouse
        {
            get => _io->WantCaptureMouse;
            set => _io->WantCaptureMouse = value;
        }

        public bool WantCaptureKeyboard
        {
            get => _io->WantCaptureKeyboard;
            set => _io->WantCaptureKeyboard = value;
        }

        public bool WantTextInput
        {
            get => _io->WantTextInput;
            set => _io->WantTextInput = value;
        }

        public bool WantSetMousePos
        {
            get => _io->WantSetMousePos;
            set => _io->WantSetMousePos = value;
        }

        public bool WantSaveIniSettings
        {
            get => _io->WantSaveIniSettings;
            set => _io->WantSaveIniSettings = value;
        }

        public bool NavActive
        {
            get => _io->NavActive;
            set => _io->NavActive = value;
        }

        public bool NavVisible
        {
            get => _io->NavVisible;
            set => _io->NavVisible = value;
        }

        public float Framerate
        {
            get => _io->Framerate;
            set => _io->Framerate = value;
        }

        public int MetricsRenderVertices
        {
            get => _io->MetricsRenderVertices;
            set => _io->MetricsRenderVertices = value;
        }

        public int MetricsRenderIndices
        {
            get => _io->MetricsRenderIndices;
            set => _io->MetricsRenderIndices = value;
        }

        public int MetricsRenderWindows
        {
            get => _io->MetricsRenderWindows;
            set => _io->MetricsRenderWindows = value;
        }

        public int MetricsActiveWindows
        {
            get => _io->MetricsActiveWindows;
            set => _io->MetricsActiveWindows = value;
        }

        public int MetricsActiveAllocations
        {
            get => _io->MetricsActiveAllocations;
            set => _io->MetricsActiveAllocations = value;
        }

        public Size MouseDelta
        {
            get => new(_io->MouseDelta);
            set => _io->MouseDelta = value.ToNative();
        }

        public Position MousePosition => new(_io->MousePos);

        private Io(Native.ImGuiIO* io)
        {
            _io = io;
        }

        public static Io Wrap(Native.ImGuiIO* native) => new(native);

        public void AddKeyEvent(Key key, bool down) => Native.ImGuiIO_AddKeyEvent(_io, (Native.ImGuiKey)key, down);

        public void AddKeyAnalogEvent(Key key, bool down, float v) => Native.ImGuiIO_AddKeyAnalogEvent(_io, (Native.ImGuiKey)key, down, v);

        public void AddMousePosEvent(Position position) => Native.ImGuiIO_AddMousePosEvent(_io, position.X, position.Y);

        public void AddMouseButtonEvent(int button, bool down) => Native.ImGuiIO_AddMouseButtonEvent(_io, button, down);

        public void AddMouseWheelEvent(Position location) => Native.ImGuiIO_AddMouseWheelEvent(_io, location.X, location.Y);

        public void AddFocusEvent(bool focused) => Native.ImGuiIO_AddFocusEvent(_io, focused);

        public void AddInputCharacter(char c) => Native.ImGuiIO_AddInputCharacter(_io, c);

        public void AddInputCharacterUTF16(char c) => Native.ImGuiIO_AddInputCharacterUTF16(_io, c);

        public void AddInputCharactersUTF8(string str)
        {
            var io = _io;
            Native.StringToUtf8Action(str, ptr => Native.ImGuiIO_AddInputCharactersUTF8(io, ptr));
        }

        public void SetKeyEventNativeData(Key key, int nativeKeycode, int nativeScancode) => Native.ImGuiIO_SetKeyEventNativeData(_io, (Native.ImGuiKey)key, nativeKeycode, nativeScancode);

        public void SetKeyEventNativeData(Key key, int nativeKeycode, int nativeScancode, int nativeLegacyIndex) => Native.ImGuiIO_SetKeyEventNativeDataEx(_io, (Native.ImGuiKey)key, nativeKeycode, nativeScancode, nativeLegacyIndex);

        public void SetAppAcceptingEvents(bool acceptingEvents) => Native.ImGuiIO_SetAppAcceptingEvents(_io, acceptingEvents);

        public void ClearInputCharacters() => Native.ImGuiIO_ClearInputCharacters(_io);

        public void ClearInputKeys() => Native.ImGuiIO_ClearInputKeys(_io);

        public Native.ImGuiIO* ToNative() => _io;
    }
}
