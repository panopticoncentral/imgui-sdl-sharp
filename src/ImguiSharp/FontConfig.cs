namespace ImguiSharp
{
    public readonly unsafe struct FontConfig : INativeWrapper<FontConfig, Native.ImFontConfig>
    {
        private readonly Native.ImFontConfig* _fontConfig;

        private FontConfig(Native.ImFontConfig* fontConfig)
        {
            _fontConfig = fontConfig;
        }

        public static FontConfig Wrap(Native.ImFontConfig* native) => new(native);

        public Native.ImFontConfig* ToNative() => _fontConfig;
    }
}
