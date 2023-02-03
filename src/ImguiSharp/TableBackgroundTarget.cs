namespace ImguiSharp
{
    public enum TableBackgroundTarget
    {
        None = Native.ImGuiTableBgTarget.ImGuiTableBgTarget_None,
        RowBackground0 = Native.ImGuiTableBgTarget.ImGuiTableBgTarget_RowBg0,
        RowBackground1 = Native.ImGuiTableBgTarget.ImGuiTableBgTarget_RowBg1,
        CellBackground = Native.ImGuiTableBgTarget.ImGuiTableBgTarget_CellBg,
    }
}
