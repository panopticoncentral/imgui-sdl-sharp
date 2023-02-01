namespace ImguiSharp
{
    public readonly unsafe record struct Rectangle(float X1, float Y1, float X2, float Y2) : INativeValueWrapper<Rectangle, Native.ImVec4>
    {
        public static Rectangle Wrap(Native.ImVec4 native) => new(native.X, native.Y, native.Z, native.W);

        public Native.ImVec4 ToNative() => new(X1, Y1, X2, Y2);
    }
}
