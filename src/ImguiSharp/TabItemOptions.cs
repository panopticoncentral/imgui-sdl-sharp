namespace ImguiSharp
{
    public enum TabItemOptions
    {
        None = Native.ImGuiTabItemFlags.ImGuiTabItemFlags_None,
        UnsavedDocument = Native.ImGuiTabItemFlags.ImGuiTabItemFlags_UnsavedDocument,
        SetSelected = Native.ImGuiTabItemFlags.ImGuiTabItemFlags_SetSelected,
        NoCloseWithMiddleMouseButton = Native.ImGuiTabItemFlags.ImGuiTabItemFlags_NoCloseWithMiddleMouseButton,
        NoPushId = Native.ImGuiTabItemFlags.ImGuiTabItemFlags_NoPushId,
        NoTooltip = Native.ImGuiTabItemFlags.ImGuiTabItemFlags_NoTooltip,
        NoReorder = Native.ImGuiTabItemFlags.ImGuiTabItemFlags_NoReorder,
        Leading = Native.ImGuiTabItemFlags.ImGuiTabItemFlags_Leading,
        Trailing = Native.ImGuiTabItemFlags.ImGuiTabItemFlags_Trailing,
    }
}
