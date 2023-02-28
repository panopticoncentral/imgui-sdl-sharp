namespace ImguiSharp
{
    public readonly unsafe record struct RectangleF(PositionF Min, PositionF Max) : INativeValueWrapper<RectangleF, Native.ImVec4>
    {
        public static RectangleF Wrap(Native.ImVec4 native) => new(new(native.X, native.Y), new(native.Z, native.W));

        public static RectangleF Wrap(Native.ImVec2 min, Native.ImVec2 max) => new(PositionF.Wrap(min), PositionF.Wrap(max));

        public Native.ImVec4 ToNative() => new(Min.X, Min.Y, Max.X, Max.Y);
    }
}
