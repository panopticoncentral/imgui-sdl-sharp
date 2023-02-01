namespace ImguiSharp
{
    public readonly unsafe struct Style : INativeReferenceWrapper<Style, Native.ImGuiStyle>
    {
        private readonly Native.ImGuiStyle* _style;

        private Style(Native.ImGuiStyle* style)
        {
            _style = style;
        }

        public static Style Wrap(Native.ImGuiStyle* native) => new(native);

        public Native.ImGuiStyle* ToNative() => _style;
    }
}
