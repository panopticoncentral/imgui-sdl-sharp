namespace ImguiSharp
{
    public readonly unsafe struct Style : INativeReferenceWrapper<Style, Native.ImGuiStyle>
    {
        private readonly Native.ImGuiStyle* _style;

        private Style(Native.ImGuiStyle* style)
        {
            _style = style;
        }

        public static Style? Wrap(Native.ImGuiStyle* native) => native == null ? null : new(native);

        public Native.ImGuiStyle* ToNative() => _style;
    }

    public static unsafe class StyleExtensions
    {
        public static Native.ImGuiStyle* ToNative(this Style? v) => v == null ? null : v.Value.ToNative();
    }
}
