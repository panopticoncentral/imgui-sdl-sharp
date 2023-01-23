namespace ImguiSharp
{
    public readonly record struct TextureId(nuint Value)
    {
        internal TextureId(Native.ImTextureID textureId) : this(textureId.Value)
        {
        }

        internal Native.ImTextureID ToNative() => new(Value);
    }
}
