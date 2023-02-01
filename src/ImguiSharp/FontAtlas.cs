namespace ImguiSharp
{
    public readonly unsafe struct FontAtlas : INativeReferenceWrapper<FontAtlas, Native.ImFontAtlas>
    {
        private readonly Native.ImFontAtlas* _fontAtlas;

        public FontAtlasOptions Options
        {
            get => (FontAtlasOptions)_fontAtlas->Flags;
            set => _fontAtlas->Flags = (Native.ImFontAtlasFlags)value;
        }

        public TextureId TextureId => TextureId.Wrap(_fontAtlas->TexID);

        public int TextureDesiredWidth
        {
            get => _fontAtlas->TexDesiredWidth;
            set => _fontAtlas->TexDesiredWidth = value;
        }

        public int TextureGlyphPadding
        {
            get => _fontAtlas->TexGlyphPadding;
            set => _fontAtlas->TexGlyphPadding = value;
        }

        public bool Locked => _fontAtlas->Locked;

        public bool Built => Native.ImFontAtlas_IsBuilt(_fontAtlas);

        private FontAtlas(Native.ImFontAtlas* fontAtlas)
        {
            _fontAtlas = fontAtlas;
        }

        public static FontAtlas Wrap(Native.ImFontAtlas* native) => new(native);

        public Font AddFont(FontConfig fontConfig) => Font.Wrap(Native.ImFontAtlas_AddFont(_fontAtlas, fontConfig.ToNative()));

        public Font AddFontDefault(FontConfig? fontConfig = default) => Font.Wrap(Native.ImFontAtlas_AddFontDefault(_fontAtlas, fontConfig == null ? null : fontConfig.Value.ToNative()));

        public Font AddFontFromFileTtf(string filename, float sizePixels, FontConfig? fontConfig = default, Span<char> glyphRanges = default)
        {
            fixed (byte* filenamePtr = Native.StringToUtf8(filename))
            fixed (char* glyphRangesPtr = glyphRanges)
            {
                return Font.Wrap(Native.ImFontAtlas_AddFontFromFileTTF(_fontAtlas, filenamePtr, sizePixels, fontConfig == null ? null : fontConfig.Value.ToNative(), glyphRangesPtr));
            }
        }

        public Font AddFontFromMemoryTtf(Span<byte> fontData, float sizePixels, FontConfig? fontConfig = default, Span<char> glyphRanges = default)
        {
            fixed (byte* fontDataPtr = fontData)
            fixed (char* glyphRangesPtr = glyphRanges)
            {
                return Font.Wrap(Native.ImFontAtlas_AddFontFromMemoryTTF(_fontAtlas, fontDataPtr, fontData.Length, sizePixels, fontConfig == null ? null : fontConfig.Value.ToNative(), glyphRangesPtr));
            }
        }

        public Font AddFontFromMemoryCompressedTtf(Span<byte> compressedFontData, float sizePixels, FontConfig? fontConfig = default, Span<char> glyphRanges = default)
        {
            fixed (byte* compressedFontDataPtr = compressedFontData)
            fixed (char* glyphRangesPtr = glyphRanges)
            {
                return Font.Wrap(Native.ImFontAtlas_AddFontFromMemoryCompressedTTF(_fontAtlas, compressedFontDataPtr, compressedFontData.Length, sizePixels, fontConfig == null ? null : fontConfig.Value.ToNative(), glyphRangesPtr));
            }
        }

        public Font AddFontFromMemoryCompressedBase85Ttf(Span<byte> compressedFontDataBase85, float sizePixels, FontConfig? fontConfig = default, Span<char> glyphRanges = default)
        {
            fixed (byte* compressedFontDataBase85Ptr = compressedFontDataBase85)
            fixed (char* glyphRangesPtr = glyphRanges)
            {
                return Font.Wrap(Native.ImFontAtlas_AddFontFromMemoryCompressedBase85TTF(_fontAtlas, compressedFontDataBase85Ptr, sizePixels, fontConfig == null ? null : fontConfig.Value.ToNative(), glyphRangesPtr));
            }
        }

        public void ClearInputData() => Native.ImFontAtlas_ClearInputData(_fontAtlas);

        public void ClearTextureData() => Native.ImFontAtlas_ClearTexData(_fontAtlas);

        public void ClearFonts() => Native.ImFontAtlas_ClearFonts(_fontAtlas);

        public void Clear() => Native.ImFontAtlas_Clear(_fontAtlas);

        public bool Build() => Native.ImFontAtlas_Build(_fontAtlas);

        public void GetTextureDataAsAlpha8(out Span<byte> pixels, out int width, out int height, out int bytesPerPixel)
        {
            byte* pixelsLocal;
            int widthLocal, heightLocal, bytesPerPixelLocal;

            Native.ImFontAtlas_GetTexDataAsAlpha8(_fontAtlas, &pixelsLocal, &widthLocal, &heightLocal, &bytesPerPixelLocal);
            pixels = new(pixelsLocal, widthLocal * heightLocal * bytesPerPixelLocal);
            width = widthLocal;
            height = heightLocal;
            bytesPerPixel = bytesPerPixelLocal;
        }

        public void GetTextureDataAsRgba32(out Span<byte> pixels, out int width, out int height, out int bytesPerPixel)
        {
            byte* pixelsLocal;
            int widthLocal, heightLocal, bytesPerPixelLocal;

            Native.ImFontAtlas_GetTexDataAsRGBA32(_fontAtlas, &pixelsLocal, &widthLocal, &heightLocal, &bytesPerPixelLocal);
            pixels = new(pixelsLocal, widthLocal * heightLocal * bytesPerPixelLocal);
            width = widthLocal;
            height = heightLocal;
            bytesPerPixel = bytesPerPixelLocal;
        }

        public void SetTextureId(TextureId id) => Native.ImFontAtlas_SetTexID(_fontAtlas, id.ToNative());

        private Span<char> ConvertGlyphRange(char* chars)
        {
            static int RangeLength(char* v)
            {
                var current = v;
                while (*current != '\0')
                {
                    current++;
                }
                return (int)(current - v);
            }

            return new(chars, RangeLength(chars));
        }

        public Span<char> GetGlyphRangesDefault() => ConvertGlyphRange(Native.ImFontAtlas_GetGlyphRangesDefault(_fontAtlas));

        public Span<char> GetGlyphRangesGreek() => ConvertGlyphRange(Native.ImFontAtlas_GetGlyphRangesGreek(_fontAtlas));

        public Span<char> GetGlyphRangesKorean() => ConvertGlyphRange(Native.ImFontAtlas_GetGlyphRangesKorean(_fontAtlas));

        public Span<char> GetGlyphRangesJapanese() => ConvertGlyphRange(Native.ImFontAtlas_GetGlyphRangesJapanese(_fontAtlas));

        public Span<char> GetGlyphRangesChineseFull() => ConvertGlyphRange(Native.ImFontAtlas_GetGlyphRangesChineseFull(_fontAtlas));

        public Span<char> GetGlyphRangesChineseSimplifiedCommon() => ConvertGlyphRange(Native.ImFontAtlas_GetGlyphRangesChineseSimplifiedCommon(_fontAtlas));

        public Span<char> GetGlyphRangesCyrillic() => ConvertGlyphRange(Native.ImFontAtlas_GetGlyphRangesCyrillic(_fontAtlas));

        public Span<char> GetGlyphRangesThai() => ConvertGlyphRange(Native.ImFontAtlas_GetGlyphRangesThai(_fontAtlas));

        public Span<char> GetGlyphRangesVietnamese() => ConvertGlyphRange(Native.ImFontAtlas_GetGlyphRangesVietnamese(_fontAtlas));

        public int AddCustomRectRegular(int width, int height) => Native.ImFontAtlas_AddCustomRectRegular(_fontAtlas, width, height);

        public int AddCustomRectFontGlyph(Font font, char id, int width, int height, float advanceX, Position offset = default) =>
            Native.ImFontAtlas_AddCustomRectFontGlyph(_fontAtlas, font.ToNative(), id, width, height, advanceX, offset.ToNative());

        public CustomRect GetCustomRectByIndex(int index) => CustomRect.Wrap(Native.ImFontAtlas_GetCustomRectByIndex(_fontAtlas, index));

        public Native.ImFontAtlas* ToNative() => _fontAtlas;

        public readonly unsafe struct CustomRect : INativeReferenceWrapper<CustomRect, Native.ImFontAtlasCustomRect>
        {
            private readonly Native.ImFontAtlasCustomRect* _customRect;

            private CustomRect(Native.ImFontAtlasCustomRect* customRect)
            {
                _customRect = customRect;
            }

            public static CustomRect Wrap(Native.ImFontAtlasCustomRect* native) => new(native);

            public Native.ImFontAtlasCustomRect* ToNative() => _customRect;
        }
    }
}
