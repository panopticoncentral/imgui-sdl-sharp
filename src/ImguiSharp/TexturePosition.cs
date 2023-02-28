namespace ImguiSharp
{
    public readonly unsafe record struct TexturePosition(float U, float V) : INativeValueWrapper<TexturePosition, Native.ImVec2>
    {
        public static TexturePosition Wrap(Native.ImVec2 native) => new(native.X, native.Y);

        public Native.ImVec2 ToNative() => new(U, V);
    }
}
