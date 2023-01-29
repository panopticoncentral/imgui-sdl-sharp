namespace ImguiSharp
{
    public readonly unsafe struct Font : INativeWrapper<Font, Native.ImFont>
    {
        private readonly Native.ImFont* _font;

        private Font(Native.ImFont* font)
        {
            _font = font;
        }

        public static Font Wrap(Native.ImFont* native) => new(native);

        public Native.ImFont* ToNative() => _font;
    }
}
