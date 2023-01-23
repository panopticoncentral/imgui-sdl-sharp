namespace ImguiSharp
{
    public sealed unsafe class FontConfig
    {
        private readonly Native.ImFontConfig* _fontConfig;

        internal FontConfig(Native.ImFontConfig* fontConfig)
        {
            _fontConfig = fontConfig;
        }

        internal Native.ImFontConfig* ToNative() => _fontConfig;
    }
}
