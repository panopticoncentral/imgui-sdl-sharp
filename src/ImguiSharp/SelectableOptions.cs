namespace ImguiSharp
{
    public enum SelectableOptions
    {
        None = Native.ImGuiSelectableFlags.ImGuiSelectableFlags_None,
        DontClosePopups = Native.ImGuiSelectableFlags.ImGuiSelectableFlags_DontClosePopups,
        SpanAllColumns = Native.ImGuiSelectableFlags.ImGuiSelectableFlags_SpanAllColumns,
        AllowDoubleClick = Native.ImGuiSelectableFlags.ImGuiSelectableFlags_AllowDoubleClick,
        Disabled = Native.ImGuiSelectableFlags.ImGuiSelectableFlags_Disabled,
        AllowItemOverlap = Native.ImGuiSelectableFlags.ImGuiSelectableFlags_AllowItemOverlap,
    }
}
