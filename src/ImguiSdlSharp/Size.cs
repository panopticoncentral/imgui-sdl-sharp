namespace SdlSharp.Imgui
{
    public readonly record struct Size(float Width, float Height)
    {
        internal Size(Native.ImVec2 vec) : this(vec.X, vec.Y)
        {
        }

        internal Native.ImVec2 ToNative() => new(Width, Height);
    }
}
