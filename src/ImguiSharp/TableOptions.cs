namespace ImguiSharp
{
    public enum TableOptions
    {
        None = Native.ImGuiTableFlags.ImGuiTableFlags_None,
        Resizable = Native.ImGuiTableFlags.ImGuiTableFlags_Resizable,
        Reorderable = Native.ImGuiTableFlags.ImGuiTableFlags_Reorderable,
        Hideable = Native.ImGuiTableFlags.ImGuiTableFlags_Hideable,
        Sortable = Native.ImGuiTableFlags.ImGuiTableFlags_Sortable,
        NoSavedSettings = Native.ImGuiTableFlags.ImGuiTableFlags_NoSavedSettings,
        ContextMenuInBody = Native.ImGuiTableFlags.ImGuiTableFlags_ContextMenuInBody,

        RowBg = Native.ImGuiTableFlags.ImGuiTableFlags_RowBg,
        BordersInnerH = Native.ImGuiTableFlags.ImGuiTableFlags_BordersInnerH,
        BordersOuterH = Native.ImGuiTableFlags.ImGuiTableFlags_BordersOuterH,
        BordersInnerV = Native.ImGuiTableFlags.ImGuiTableFlags_BordersInnerV,
        BordersOuterV = Native.ImGuiTableFlags.ImGuiTableFlags_BordersOuterV,
        BordersH = Native.ImGuiTableFlags.ImGuiTableFlags_BordersH,
        BordersV = Native.ImGuiTableFlags.ImGuiTableFlags_BordersV,
        BordersInner = Native.ImGuiTableFlags.ImGuiTableFlags_BordersInner,
        BordersOuter = Native.ImGuiTableFlags.ImGuiTableFlags_BordersOuter,
        Borders = Native.ImGuiTableFlags.ImGuiTableFlags_Borders,
        NoBordersInBody = Native.ImGuiTableFlags.ImGuiTableFlags_NoBordersInBody,
        NoBordersInBodyUntilResize = Native.ImGuiTableFlags.ImGuiTableFlags_NoBordersInBodyUntilResize,

        SizingFixedFit = Native.ImGuiTableFlags.ImGuiTableFlags_SizingFixedFit,
        SizingFixedSame = Native.ImGuiTableFlags.ImGuiTableFlags_SizingFixedSame,
        SizingStretchProp = Native.ImGuiTableFlags.ImGuiTableFlags_SizingStretchProp,
        SizingStretchSame = Native.ImGuiTableFlags.ImGuiTableFlags_SizingStretchSame,

        NoHostExtendX = Native.ImGuiTableFlags.ImGuiTableFlags_NoHostExtendX,
        NoHostExtendY = Native.ImGuiTableFlags.ImGuiTableFlags_NoHostExtendY,
        NoKeepColumnsVisible = Native.ImGuiTableFlags.ImGuiTableFlags_NoKeepColumnsVisible,
        PreciseWidths = Native.ImGuiTableFlags.ImGuiTableFlags_PreciseWidths,

        NoClip = Native.ImGuiTableFlags.ImGuiTableFlags_NoClip,

        PadOuterX = Native.ImGuiTableFlags.ImGuiTableFlags_PadOuterX,
        NoPadOuterX = Native.ImGuiTableFlags.ImGuiTableFlags_NoPadOuterX,
        NoPadInnerX = Native.ImGuiTableFlags.ImGuiTableFlags_NoPadInnerX,

        ScrollX = Native.ImGuiTableFlags.ImGuiTableFlags_ScrollX,
        ScrollY = Native.ImGuiTableFlags.ImGuiTableFlags_ScrollY,

        SortMulti = Native.ImGuiTableFlags.ImGuiTableFlags_SortMulti,
        SortTristate = Native.ImGuiTableFlags.ImGuiTableFlags_SortTristate,

        SizingMask = Native.ImGuiTableFlags.ImGuiTableFlags_SizingMask_,
    }
}
