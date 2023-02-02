namespace ImguiSharp
{
    public readonly unsafe record struct Position(float X, float Y) : INativeValueWrapper<Position, Native.ImVec2>
    {
        public static Position Wrap(Native.ImVec2 native) => new(native.X, native.Y);

        public Native.ImVec2 ToNative() => new(X, Y);

        public Position Scale(float scaleX, float scaleY) => new(X * scaleX, Y * scaleY);

        public static Position operator +(Position left, Position right) => new(left.X + right.X, left.Y + right.Y);

        public static Position operator -(Position left, Position right) => new(left.X - right.X, left.Y - right.Y);
    }
}
