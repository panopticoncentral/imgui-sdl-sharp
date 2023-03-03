namespace ImguiSharp
{
    [Flags]
    public enum ConfigOptions
    {
        None = Native.ImGuiConfigFlags.ImGuiConfigFlags_None,
        NavEnableKeyboard = Native.ImGuiConfigFlags.ImGuiConfigFlags_NavEnableKeyboard,
        NavEnableGamepad = Native.ImGuiConfigFlags.ImGuiConfigFlags_NavEnableGamepad,
        NavEnableSetMousePosition = Native.ImGuiConfigFlags.ImGuiConfigFlags_NavEnableSetMousePos,
        NavNoCaptureKeyboard = Native.ImGuiConfigFlags.ImGuiConfigFlags_NavNoCaptureKeyboard,
        NoMouse = Native.ImGuiConfigFlags.ImGuiConfigFlags_NoMouse,
        NoMouseCursorChange = Native.ImGuiConfigFlags.ImGuiConfigFlags_NoMouseCursorChange,

        IsSRGB = Native.ImGuiConfigFlags.ImGuiConfigFlags_IsSRGB,
        IsTouchScreen = Native.ImGuiConfigFlags.ImGuiConfigFlags_IsTouchScreen
    }
}
