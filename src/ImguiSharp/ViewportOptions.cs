namespace ImguiSharp
{
    [Flags]
    public enum ViewportOptions
    {
        None = Native.ImGuiViewportFlags.ImGuiViewportFlags_None,
        IsPlatformWindow = Native.ImGuiViewportFlags.ImGuiViewportFlags_IsPlatformWindow,
        IsPlatformMonitor = Native.ImGuiViewportFlags.ImGuiViewportFlags_IsPlatformMonitor,
        OwnedByApp = Native.ImGuiViewportFlags.ImGuiViewportFlags_OwnedByApp,
    }
}
