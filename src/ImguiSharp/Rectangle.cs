namespace ImguiSharp
{
    public readonly unsafe record struct Rectangle(Position Min, Position Max) : INativeValueWrapper<Rectangle, Native.ImVec4>
    {
        public static Rectangle Wrap(Native.ImVec4 native) => new(new(native.X, native.Y), new(native.Z, native.W));

        public static Rectangle Wrap(Native.ImVec2 min, Native.ImVec2 max) => new(Position.Wrap(min), Position.Wrap(max));

        public Native.ImVec4 ToNative() => new(Min.X, Min.Y, Max.X, Max.Y);
    }
}
