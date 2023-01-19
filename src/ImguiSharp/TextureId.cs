namespace ImguiSharp
{
    public readonly record struct TextureId
    {
        private readonly Native.ImTextureID _textureId;

        internal TextureId(Native.ImTextureID textureId)
        {
            _textureId = textureId;
        }

        internal Native.ImTextureID ToNative() => _textureId;
    }
}
