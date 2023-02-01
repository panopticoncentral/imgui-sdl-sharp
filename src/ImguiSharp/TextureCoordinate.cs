namespace ImguiSharp
{
    public readonly unsafe record struct TextureCoordinate(float U, float V) : INativeValueWrapper<TextureCoordinate, Native.ImVec2>
    {
        public static TextureCoordinate Wrap(Native.ImVec2 native) => new(native.X, native.Y);

        public Native.ImVec2 ToNative() => new(U, V);
    }
}
