namespace ImguiSharp
{
    public readonly unsafe struct DrawVertex
    {
        private readonly Native.ImDrawVert* _vert;

        internal DrawVertex(Native.ImDrawVert* vert)
        {
            _vert = vert;
        }

        internal Native.ImDrawVert* ToNative() => _vert;
    }
}
