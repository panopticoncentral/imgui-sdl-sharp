namespace ImguiSharp
{
    [Flags]
    public enum Condition
    {
        None = Native.ImGuiCond.ImGuiCond_None,
        Always = Native.ImGuiCond.ImGuiCond_Always,
        Once = Native.ImGuiCond.ImGuiCond_Once,
        FirstUseEver = Native.ImGuiCond.ImGuiCond_FirstUseEver,
        Appearing = Native.ImGuiCond.ImGuiCond_Appearing,
    }
}
