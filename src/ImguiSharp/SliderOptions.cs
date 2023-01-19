namespace ImguiSharp
{
    public enum SliderOptions
    {
        None = Native.ImGuiSliderFlags.ImGuiSliderFlags_None,
        AlwaysClamp = Native.ImGuiSliderFlags.ImGuiSliderFlags_AlwaysClamp,
        Logarithmic = Native.ImGuiSliderFlags.ImGuiSliderFlags_Logarithmic,
        NoRoundToFormat = Native.ImGuiSliderFlags.ImGuiSliderFlags_NoRoundToFormat,
        NoInput = Native.ImGuiSliderFlags.ImGuiSliderFlags_NoInput,
        InvalidMask = Native.ImGuiSliderFlags.ImGuiSliderFlags_InvalidMask_,
    }
}
