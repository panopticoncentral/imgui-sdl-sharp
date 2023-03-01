namespace ImguiSharp
{
    public readonly unsafe record struct Color(byte Red, byte Green, byte Blue, byte Alpha) : INativeValueWrapper<Color, uint>
    {
        private Color(uint color) : this(
            (byte)((color >> Native.IM_COL32_R_SHIFT) & 0xFF),
            (byte)((color >> Native.IM_COL32_G_SHIFT) & 0xFF),
            (byte)((color >> Native.IM_COL32_B_SHIFT) & 0xFF),
            (byte)((color >> Native.IM_COL32_A_SHIFT) & 0xFF))
        {
        }

        public static Color Wrap(uint native) => new(native);

        public uint ToNative() => Native.IM_COL32(Red, Green, Blue, Alpha);
    }
}
