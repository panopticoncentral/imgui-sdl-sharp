namespace SdlSharp.Imgui
{
    public sealed unsafe class FontAtlas
    {
        private readonly Native.ImFontAtlas* _fontAtlas;

        internal Native.ImFontAtlas* ToNative() => _fontAtlas;
    }
}
