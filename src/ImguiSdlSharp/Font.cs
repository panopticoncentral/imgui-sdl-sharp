namespace SdlSharp.Imgui
{
    public readonly unsafe struct Font
    {
        private readonly Native.ImFont* _font;

        internal Font(Native.ImFont* font)
        {
            _font = font;
        }

        internal Native.ImFont* ToNative() => _font;
    }
}
