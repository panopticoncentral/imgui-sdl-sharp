namespace SdlSharp.Imgui
{
    public enum WindowOptions
    {
        None = Native.ImGuiWindowFlags.ImGuiWindowFlags_None,
        NoTitleBar = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoTitleBar,
        NoResize = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoResize,
        NoMove = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoMove,
        NoScrollbar = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoScrollbar,
        NoScrollWithMouse = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoScrollWithMouse,
        NoCollapse = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoCollapse,
        AlwaysAutoResize = Native.ImGuiWindowFlags.ImGuiWindowFlags_AlwaysAutoResize,
        NoBackground = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoBackground,
        NoSavedSettings = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoSavedSettings,
        NoMouseInputs = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoMouseInputs,
        MenuBar = Native.ImGuiWindowFlags.ImGuiWindowFlags_MenuBar,
        HorizontalScrollbar = Native.ImGuiWindowFlags.ImGuiWindowFlags_HorizontalScrollbar,
        NoFocusOnAppearing = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoFocusOnAppearing,
        NoBringToFrontOnFocus = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoBringToFrontOnFocus,
        AlwaysVerticalScrollbar = Native.ImGuiWindowFlags.ImGuiWindowFlags_AlwaysVerticalScrollbar,
        AlwaysHorizontalScrollbar = Native.ImGuiWindowFlags.ImGuiWindowFlags_AlwaysHorizontalScrollbar,
        AlwaysUseWindowPadding = Native.ImGuiWindowFlags.ImGuiWindowFlags_AlwaysUseWindowPadding,
        NoNavInputs = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoNavInputs,
        NoNavFocus = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoNavFocus,
        UnsavedDocument = Native.ImGuiWindowFlags.ImGuiWindowFlags_UnsavedDocument,
        NoNav = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoNav,
        NoDecoration = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoDecoration,
        NoInputs = Native.ImGuiWindowFlags.ImGuiWindowFlags_NoInputs,

        NavFlattened = Native.ImGuiWindowFlags.ImGuiWindowFlags_NavFlattened,
        ChildWindow = Native.ImGuiWindowFlags.ImGuiWindowFlags_ChildWindow,
        Tooltip = Native.ImGuiWindowFlags.ImGuiWindowFlags_Tooltip,
        Popup = Native.ImGuiWindowFlags.ImGuiWindowFlags_Popup,
        Modal = Native.ImGuiWindowFlags.ImGuiWindowFlags_Modal,
        ChildMenu = Native.ImGuiWindowFlags.ImGuiWindowFlags_ChildMenu
    }
}
