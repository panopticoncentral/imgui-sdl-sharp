namespace ImguiSharp
{
    [Flags]
    public enum DragDropOptions
    {
        None = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_None,

        SourceNoPreviewTooltip = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_SourceNoPreviewTooltip,
        SourceNoDisableHover = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_SourceNoDisableHover,
        SourceNoHoldToOpenOthers = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_SourceNoHoldToOpenOthers,
        SourceAllowNullID = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_SourceAllowNullID,
        SourceExtern = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_SourceExtern,
        SourceAutoExpirePayload = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_SourceAutoExpirePayload,

        AcceptBeforeDelivery = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_AcceptBeforeDelivery,
        AcceptNoDrawDefaultRect = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_AcceptNoDrawDefaultRect,
        AcceptNoPreviewTooltip = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_AcceptNoPreviewTooltip,
        AcceptPeekOnly = Native.ImGuiDragDropFlags.ImGuiDragDropFlags_AcceptPeekOnly,
    }
}
