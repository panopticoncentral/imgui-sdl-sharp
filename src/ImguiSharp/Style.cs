namespace ImguiSharp
{
    public readonly unsafe struct Style
    {
        private readonly Native.ImGuiStyle* _style;

        internal Style(Native.ImGuiStyle* style)
        {
            _style = style;
        }

        internal Native.ImGuiStyle* ToNative() => _style;
    }
}
