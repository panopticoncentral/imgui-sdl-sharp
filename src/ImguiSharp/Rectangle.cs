namespace ImguiSharp
{
    public readonly record struct Rectangle(float X1, float Y1, float X2, float Y2)
    {
        internal Rectangle(Native.ImVec4 vec) : this(vec.X, vec.Y, vec.Z, vec.W)
        {
        }

        internal Native.ImVec4 ToNative() => new(X1, Y1, X2, Y2);
    }
}
