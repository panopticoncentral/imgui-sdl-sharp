namespace ImguiSharp
{
    public readonly unsafe record struct Color(float Red, float Green, float Blue, float Alpha) : INativeValueWrapper<Color, Native.ImVec4>
    {
        private Color(Native.ImVec4 color) : this(color.X, color.Y, color.Z, color.W)
        {
        }

        public static Color Wrap(Native.ImVec4 native) => new(native);

        public static Color FromHsv(float h, float s, float v, float a = 1.0f)
        {
            var (r, g, b) = Imgui.ColorConvertHsvToRgb(h, s, v);
            return new(r, g, b, a);
        }

        public Native.ImVec4 ToNative() => new(Red, Green, Blue, Alpha);
    }
}
