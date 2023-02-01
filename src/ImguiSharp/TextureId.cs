namespace ImguiSharp
{
    public readonly unsafe record struct TextureId(nuint Value) : INativeValueWrapper<TextureId, Native.ImTextureID>
    {
        public static TextureId Wrap(Native.ImTextureID native) => new(native.Value);

        public Native.ImTextureID ToNative() => new(Value);
    }
}
