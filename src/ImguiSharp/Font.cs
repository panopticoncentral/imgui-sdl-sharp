namespace ImguiSharp
{
    public readonly unsafe struct Font : INativeReferenceWrapper<Font, Native.ImFont>
    {
        private readonly Native.ImFont* _font;

        private Font(Native.ImFont* font)
        {
            _font = font;
        }

        public static Font? Wrap(Native.ImFont* native) => native == null ? null : new(native);

        public Native.ImFont* ToNative() => _font;
    }
}
