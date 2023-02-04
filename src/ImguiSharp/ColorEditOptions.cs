namespace ImguiSharp
{
    public enum ColorEditOptions
    {
        None = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_None,
        NoAlpha = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoAlpha,
        NoPicker = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoPicker,
        NoOptions = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoOptions,
        NoSmallPreview = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoSmallPreview,
        NoInputs = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoInputs,
        NoTooltip = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoTooltip,
        NoLabel = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoLabel,
        NoSidePreview = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoSidePreview,
        NoDragDrop = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoDragDrop,
        NoBorder = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_NoBorder,

        AlphaBar = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_AlphaBar,
        AlphaPreview = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_AlphaPreview,
        AlphaPreviewHalf = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_AlphaPreviewHalf,
        HDR = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_HDR,
        DisplayRGB = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_DisplayRGB,
        DisplayHSV = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_DisplayHSV,
        DisplayHex = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_DisplayHex,
        Uint8 = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_Uint8,
        Float = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_Float,
        PickerHueBar = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_PickerHueBar,
        PickerHueWheel = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_PickerHueWheel,
        InputRGB = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_InputRGB,
        InputHSV = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_InputHSV,

        DefaultOptions = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_DefaultOptions_,

        DisplayMask  = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_DisplayMask_,
        DataTypeMask = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_DataTypeMask_,
        PickerMask = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_PickerMask_,
        InputMask = Native.ImGuiColorEditFlags.ImGuiColorEditFlags_InputMask_,
    }
}
