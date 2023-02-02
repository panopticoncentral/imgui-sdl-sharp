// Can't use auto properties in this type because we need to reflect on the underlying field
#pragma warning disable IDE0032 // Use auto property

namespace ImguiSharp
{
    public readonly unsafe record struct DrawVertex : INativeValueWrapper<DrawVertex, Native.ImDrawVert>
    {
        private readonly Position _xy;
        private readonly TextureCoordinate _uv;
        private readonly uint _color;

        public Position Xy => _xy;
        public TextureCoordinate Uv => _uv;
        public uint Color => _color;

        public DrawVertex(Position xy, TextureCoordinate uv, uint color)
        {
            _xy = xy;
            _uv = uv;
            _color = color;
        }

        public static DrawVertex Wrap(Native.ImDrawVert native) => new(Position.Wrap(native.pos), TextureCoordinate.Wrap(native.uv), native.col);

        public Native.ImDrawVert ToNative() => new() { pos = Xy.ToNative(), uv = Uv.ToNative(), col = Color };
    }
}
