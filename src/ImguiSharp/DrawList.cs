namespace ImguiSharp
{
    public readonly unsafe struct DrawList
    {
        private readonly Native.ImDrawList* _list;

        public IReadOnlyList<DrawCommand> Commands => new NativeReadOnlyList<Native.ImDrawCmd, DrawCommand>(_list->CmdBuffer.Data, _list->CmdBuffer.Size, n => new(n));

        public IReadOnlyList<DrawIndex> Indexes => new NativeReadOnlyList<Native.ImDrawIdx, DrawIndex>(_list->IdxBuffer.Data, _list->IdxBuffer.Size, n => new(n));

        public IReadOnlyList<DrawVertex> Vertices => new NativeReadOnlyList<Native.ImDrawVert, DrawVertex>(_list->VtxBuffer.Data, _list->VtxBuffer.Size, n => new(n));

        public DrawListOptions Options => (DrawListOptions)_list->Flags;

        internal DrawList(Native.ImDrawList* list)
        {
            _list = list;
        }

        internal Native.ImDrawList* ToNative() => _list;
    }
}
