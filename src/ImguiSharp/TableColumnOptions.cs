namespace ImguiSharp
{
    public enum TableColumnOptions
    {
        None = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_None,
        Disabled = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_Disabled,
        DefaultHide = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_DefaultHide,
        DefaultSort = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_DefaultSort,
        WidthStretch = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_WidthStretch,
        WidthFixed = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_WidthFixed,
        NoResize = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoResize,
        NoReorder = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoReorder,
        NoHide = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoHide,
        NoClip = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoClip,
        NoSort = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoSort,
        NoSortAscending = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoSortAscending,
        NoSortDescending = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoSortDescending,
        NoHeaderLabel = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoHeaderLabel,
        NoHeaderWidth = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoHeaderWidth,
        PreferSortAscending = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_PreferSortAscending,
        PreferSortDescending = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_PreferSortDescending,
        IndentEnable = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_IndentEnable,
        IndentDisable = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_IndentDisable,

        IsEnabled = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_IsEnabled,
        IsVisible = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_IsVisible,
        IsSorted = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_IsSorted,
        IsHovered = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_IsHovered,

        WidthMask = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_WidthMask_,
        IndentMask = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_IndentMask_,
        StatusMask = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_StatusMask_,
        NoDirectResize = Native.ImGuiTableColumnFlags.ImGuiTableColumnFlags_NoDirectResize_,
    }
}
