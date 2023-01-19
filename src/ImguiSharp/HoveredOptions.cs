namespace ImguiSharp
{
    public enum HoveredOptions
    {
        None = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_None,
        ChildWindows = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_ChildWindows,
        RootWindow = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_RootWindow,
        AnyWindow = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_AnyWindow,
        NoPopupHierarchy = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_NoPopupHierarchy,

        AllowWhenBlockedByPopup = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_AllowWhenBlockedByPopup,

        AllowWhenBlockedByActiveItem = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_AllowWhenBlockedByActiveItem,
        AllowWhenOverlapped = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_AllowWhenOverlapped,
        AllowWhenDisabled = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_AllowWhenDisabled,
        NoNavOverride = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_NoNavOverride,
        RectOnly = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_RectOnly,
        RootAndChildWindows = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_RootAndChildWindows,

        DelayNormal = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_DelayNormal,
        DelayShort = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_DelayShort,
        NoSharedDelay = Native.ImGuiHoveredFlags.ImGuiHoveredFlags_NoSharedDelay,
    }
}
