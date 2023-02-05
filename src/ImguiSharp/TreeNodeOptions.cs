namespace ImguiSharp
{
    public enum TreeNodeOptions
    {
        None = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_None,
        Selected = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_Selected,
        Framed = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_Framed,
        AllowItemOverlap = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_AllowItemOverlap,
        NoTreePushOnOpen = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_NoTreePushOnOpen,
        NoAutoOpenOnLog = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_NoAutoOpenOnLog,
        DefaultOpen = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_DefaultOpen,
        OpenOnDoubleClick = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_OpenOnDoubleClick,
        OpenOnArrow = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_OpenOnArrow,
        Leaf = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_Leaf,
        Bullet = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_Bullet,
        FramePadding = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_FramePadding,
        SpanAvailWidth = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_SpanAvailWidth,
        SpanFullWidth = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_SpanFullWidth,
        NavLeftJumpsBackHere = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_NavLeftJumpsBackHere,

        CollapsingHeader = Native.ImGuiTreeNodeFlags.ImGuiTreeNodeFlags_CollapsingHeader,
    }
}
