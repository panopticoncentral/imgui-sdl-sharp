namespace SdlSharp.Imgui
{
    public readonly unsafe struct FontAtlas
    {
        private readonly Native.ImFontAtlas* _fontAtlas;

        internal Native.ImFontAtlas* ToNative() => _fontAtlas;
    }
}
