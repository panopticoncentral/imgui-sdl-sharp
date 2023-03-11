namespace ImguiSharp
{
    public readonly unsafe record struct ColorF(float Red, float Green, float Blue, float Alpha = 1.0f) : INativeValueWrapper<ColorF, Native.ImVec4>
    {
        private ColorF(Native.ImVec4 color) : this(color.X, color.Y, color.Z, color.W)
        {
        }

        public static ColorF Wrap(Native.ImVec4 native) => new(native);

        public static ColorF FromHsv(float h, float s, float v, float a = 1.0f)
        {
            var c = Imgui.ColorConvertHsvToRgb(h, s, v);
            return new(c.Red, c.Green, c.Blue, a);
        }

        public Native.ImVec4 ToNative() => new(Red, Green, Blue, Alpha);
    }
}
