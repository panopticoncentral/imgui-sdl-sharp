namespace ImguiSharp
{
    public readonly unsafe record struct Position(float X, float Y) : INativeValueWrapper<Position, Native.ImVec2>
    {
        public static Position Wrap(Native.ImVec2 native) => new(native.X, native.Y);

        public Native.ImVec2 ToNative() => new(X, Y);
    }
}
