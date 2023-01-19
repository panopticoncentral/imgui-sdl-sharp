namespace ImguiSharp
{
    public readonly unsafe struct FontAtlas
    {
        private readonly Native.ImFontAtlas* _fontAtlas;

        internal FontAtlas(Native.ImFontAtlas* fontAtlas)
        {
            _fontAtlas = fontAtlas;
        }

        internal Native.ImFontAtlas* ToNative() => _fontAtlas;
    }
}
