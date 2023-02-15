namespace ImguiSharp
{
    public readonly unsafe struct FontConfig : INativeReferenceWrapper<FontConfig, Native.ImFontConfig>
    {
        private readonly Native.ImFontConfig* _fontConfig;

        public Span<byte> Data
        {
            get => new(_fontConfig->FontData, _fontConfig->FontDataSize);
            set
            {
                var buffer = Native.ImGui_MemAlloc((nuint)value.Length);
                value.CopyTo(new Span<byte>(buffer, value.Length));
                _fontConfig->FontData = buffer;
                _fontConfig->FontDataSize = value.Length;
                // We're going to have the font atlas free this for us.
                _fontConfig->FontDataOwnedByAtlas = true;
            }
        }

        public int Number
        {
            get => _fontConfig->FontNo;
            set => _fontConfig->FontNo = value;
        }

        public float SizePixels
        {
            get => _fontConfig->SizePixels;
            set => _fontConfig->SizePixels = value;
        }

#if false
            public int OversampleH;
            public int OversampleV;
            public bool PixelSnapH;
            public ImVec2 GlyphExtraSpacing;
            public ImVec2 GlyphOffset;
            public char* GlyphRanges;
            public float GlyphMinAdvanceX;
            public float GlyphMaxAdvanceX;
            public bool MergeMode;
            public uint FontBuilderFlags;
            public float RasterizerMultiply;
            public char EllipsisChar;

#endif

        private FontConfig(Native.ImFontConfig* fontConfig)
        {
            _fontConfig = fontConfig;
        }

        public static FontConfig? Wrap(Native.ImFontConfig* native) => native == null ? null : new(native);

        public Native.ImFontConfig* ToNative() => _fontConfig;
    }
}
