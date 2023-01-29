namespace ImguiSharp
{
    public readonly unsafe struct DrawVertex : INativeWrapper<DrawVertex, Native.ImDrawVert>
    {
        private readonly Native.ImDrawVert* _vert;

        private DrawVertex(Native.ImDrawVert* vert)
        {
            _vert = vert;
        }

        public static DrawVertex Wrap(Native.ImDrawVert* native) => new(native);

        public Native.ImDrawVert* ToNative() => _vert;
    }
}
