namespace ImguiSharp
{
    [Flags]
    public enum ComboOptions
    {
        None = Native.ImGuiComboFlags.ImGuiComboFlags_None,
        PopupAlignLeft = Native.ImGuiComboFlags.ImGuiComboFlags_PopupAlignLeft,
        HeightSmall = Native.ImGuiComboFlags.ImGuiComboFlags_HeightSmall,
        HeightRegular = Native.ImGuiComboFlags.ImGuiComboFlags_HeightRegular,
        HeightLarge = Native.ImGuiComboFlags.ImGuiComboFlags_HeightLarge,
        HeightLargest = Native.ImGuiComboFlags.ImGuiComboFlags_HeightLargest,
        NoArrowButton = Native.ImGuiComboFlags.ImGuiComboFlags_NoArrowButton,
        NoPreview = Native.ImGuiComboFlags.ImGuiComboFlags_NoPreview,
        HeightMask = Native.ImGuiComboFlags.ImGuiComboFlags_HeightMask_,
    }
}
