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

        public int OversampleHorizontal
        {
            get => _fontConfig->OversampleH;
            set => _fontConfig->OversampleH = value;
        }

        public int OversampleVertical
        {
            get => _fontConfig->OversampleV;
            set => _fontConfig->OversampleV = value;
        }

        public bool PixelSnapHeight
        {
            get => _fontConfig->PixelSnapH;
            set => _fontConfig->PixelSnapH = value;
        }

        public SizeF GlyphExtraSpacing
        {
            get => SizeF.Wrap(_fontConfig->GlyphExtraSpacing);
            set => _fontConfig->GlyphExtraSpacing = value.ToNative();
        }

        public PositionF GlyphOffset
        {
            get => PositionF.Wrap(_fontConfig->GlyphOffset);
            set => _fontConfig->GlyphOffset = value.ToNative();
        }

        public char* GlyphRanges
        {
            get => _fontConfig->GlyphRanges;
            set => _fontConfig->GlyphRanges = value;
        }

        public float GlyphMinimumAdvanceX
        {
            get => _fontConfig->GlyphMinAdvanceX;
            set => _fontConfig->GlyphMinAdvanceX = value;
        }

        public float GlyphMaximumAdvanceX
        {
            get => _fontConfig->GlyphMaxAdvanceX;
            set => _fontConfig->GlyphMaxAdvanceX = value;
        }

        public bool MergeMode
        {
            get => _fontConfig->MergeMode;
            set => _fontConfig->MergeMode = value;
        }

        public uint FontBuilderFlags
        {
            get => _fontConfig->FontBuilderFlags;
            set => _fontConfig->FontBuilderFlags = value;
        }

        public float RasterizerMultiply
        {
            get => _fontConfig->RasterizerMultiply;
            set => _fontConfig->RasterizerMultiply = value;
        }

        public char EllipsisChar
        {
            get => _fontConfig->EllipsisChar;
            set => _fontConfig->EllipsisChar = value;
        }

        private FontConfig(Native.ImFontConfig* fontConfig)
        {
            _fontConfig = fontConfig;
        }

        public static FontConfig? Wrap(Native.ImFontConfig* native) => native == null ? null : new(native);

        public Native.ImFontConfig* ToNative() => _fontConfig;
    }
}
