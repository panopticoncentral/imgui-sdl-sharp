namespace ImguiSharp
{
    public readonly unsafe struct FontAtlasCustomRectangle : INativeReferenceWrapper<FontAtlasCustomRectangle, Native.ImFontAtlasCustomRect>
    {
        private readonly Native.ImFontAtlasCustomRect* _rect;

        public ushort Width => _rect->Width;

        public ushort Height => _rect->Height;

        public ushort X => _rect->X;

        public ushort Y => _rect->Y;

        public uint GlyphId => _rect->GlyphID;

        public float GlyphAdvanceX => _rect->GlyphAdvanceX;

        public Position GlyphOffset => Position.Wrap(_rect->GlyphOffset);

        public Font? Font => ImguiSharp.Font.Wrap(_rect->Font);

        public bool Packed => Native.ImFontAtlasCustomRect_IsPacked(_rect);

        private FontAtlasCustomRectangle(Native.ImFontAtlasCustomRect* rect)
        {
            _rect = rect;
        }

        public static FontAtlasCustomRectangle? Wrap(Native.ImFontAtlasCustomRect* native) => native == null ? null : new(native);

        public Native.ImFontAtlasCustomRect* ToNative() => _rect;
    }
}
