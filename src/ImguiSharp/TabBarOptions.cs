namespace ImguiSharp
{
    public enum TabBarOptions
    {
        None = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_None,
        Reorderable = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_Reorderable,
        AutoSelectNewTabs = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_AutoSelectNewTabs,
        TabListPopupButton = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_TabListPopupButton,
        NoCloseWithMiddleMouseButton = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_NoCloseWithMiddleMouseButton,
        NoTabListScrollingButtons = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_NoTabListScrollingButtons,
        NoTooltip = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_NoTooltip,
        FittingPolicyResizeDown = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_FittingPolicyResizeDown,
        FittingPolicyScroll = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_FittingPolicyScroll,
        FittingPolicyMask = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_FittingPolicyMask_,
        FittingPolicyDefault = Native.ImGuiTabBarFlags.ImGuiTabBarFlags_FittingPolicyDefault_,
    }
}
