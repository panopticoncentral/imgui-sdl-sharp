namespace ImguiSharp
{
    public readonly record struct Position(float X, float Y)
    {
        internal Position(Native.ImVec2 vec) : this(vec.X, vec.Y)
        {
        }

        internal Native.ImVec2 ToNative() => new(X, Y);
    }
}
