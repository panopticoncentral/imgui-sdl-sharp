namespace ImguiSharp
{
    public readonly unsafe record struct DrawVertex(Position Xy, TextureCoordinate Uv, uint Color) : INativeValueWrapper<DrawVertex, Native.ImDrawVert>
    {
        public static DrawVertex Wrap(Native.ImDrawVert native) => new(Position.Wrap(native.pos), TextureCoordinate.Wrap(native.uv), native.col);

        public Native.ImDrawVert ToNative() => new() { pos = Xy.ToNative(), uv = Uv.ToNative(), col = Color };
    }
}
