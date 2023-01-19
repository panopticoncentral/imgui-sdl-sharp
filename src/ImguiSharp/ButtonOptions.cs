namespace ImguiSharp
{
    [Flags]
    public enum ButtonOptions
    {
        None = Native.ImGuiButtonFlags.ImGuiButtonFlags_None,
        MouseButtonLeft = Native.ImGuiButtonFlags.ImGuiButtonFlags_MouseButtonLeft,
        MouseButtonRight = Native.ImGuiButtonFlags.ImGuiButtonFlags_MouseButtonRight,
        MouseButtonMiddle = Native.ImGuiButtonFlags.ImGuiButtonFlags_MouseButtonMiddle,

        Mask = Native.ImGuiButtonFlags.ImGuiButtonFlags_MouseButtonMask_,
        Default = MouseButtonLeft,
    }
}
