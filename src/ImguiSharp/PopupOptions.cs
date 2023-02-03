namespace ImguiSharp
{
    public enum PopupOptions
    {
        None = Native.ImGuiPopupFlags.ImGuiPopupFlags_None,
        MouseButtonLeft = Native.ImGuiPopupFlags.ImGuiPopupFlags_MouseButtonLeft,
        MouseButtonRight = Native.ImGuiPopupFlags.ImGuiPopupFlags_MouseButtonRight,
        MouseButtonMiddle = Native.ImGuiPopupFlags.ImGuiPopupFlags_MouseButtonMiddle,
        MouseButtonMask = Native.ImGuiPopupFlags.ImGuiPopupFlags_MouseButtonMask_,
        MouseButtonDefault = Native.ImGuiPopupFlags.ImGuiPopupFlags_MouseButtonDefault_,
        NoOpenOverExistingPopup = Native.ImGuiPopupFlags.ImGuiPopupFlags_NoOpenOverExistingPopup,
        NoOpenOverItems = Native.ImGuiPopupFlags.ImGuiPopupFlags_NoOpenOverItems,
        AnyPopupId = Native.ImGuiPopupFlags.ImGuiPopupFlags_AnyPopupId,
        AnyPopupLevel = Native.ImGuiPopupFlags.ImGuiPopupFlags_AnyPopupLevel,
        AnyPopup = Native.ImGuiPopupFlags.ImGuiPopupFlags_AnyPopup,
    }
}
