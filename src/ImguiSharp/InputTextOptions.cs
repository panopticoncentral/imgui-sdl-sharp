namespace ImguiSharp
{
    [Flags]
    public enum InputTextOptions
    {
        None = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_None,
        CharsDecimal = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CharsDecimal,
        CharsHexadecimal = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CharsHexadecimal,
        CharsUppercase = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CharsUppercase,
        CharsNoBlank = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CharsNoBlank,
        AutoSelectAll = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_AutoSelectAll,
        EnterReturnsTrue = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_EnterReturnsTrue,
        AllowTabInput = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_AllowTabInput,
        CtrlEnterForNewLine = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CtrlEnterForNewLine,
        NoHorizontalScroll = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_NoHorizontalScroll,
        AlwaysOverwrite = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_AlwaysOverwrite,
        ReadOnly = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_ReadOnly,
        Password = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_Password,
        NoUndoRedo = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_NoUndoRedo,
        CharsScientific = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CharsScientific,
        EscapeClearsAll = Native.ImGuiInputTextFlags.ImGuiInputTextFlags_EscapeClearsAll
    }
}
