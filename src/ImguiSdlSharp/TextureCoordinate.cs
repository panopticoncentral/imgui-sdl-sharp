namespace SdlSharp.Imgui
{
    public readonly record struct TextureCoordinate(float U, float V)
    {
        internal TextureCoordinate(Native.ImVec2 vec) : this(vec.X, vec.Y)
        {
        }

        internal Native.ImVec2 ToNative() => new(U, V);
    }
}
