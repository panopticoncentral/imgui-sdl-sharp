namespace ImguiSharp
{
    public readonly unsafe record struct PositionF(float X, float Y) : INativeValueWrapper<PositionF, Native.ImVec2>
    {
        public static PositionF Wrap(Native.ImVec2 native) => new(native.X, native.Y);

        public Native.ImVec2 ToNative() => new(X, Y);

        public PositionF Scale(float scaleX, float scaleY) => new(X * scaleX, Y * scaleY);

        public static PositionF operator +(PositionF left, PositionF right) => new(left.X + right.X, left.Y + right.Y);

        public static PositionF operator -(PositionF left, PositionF right) => new(left.X - right.X, left.Y - right.Y);
    }
}
