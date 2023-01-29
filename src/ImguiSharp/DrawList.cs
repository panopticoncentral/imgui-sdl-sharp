namespace ImguiSharp
{
    public readonly unsafe struct DrawList : INativeWrapper<DrawList, Native.ImDrawList>
    {
        private readonly Native.ImDrawList* _list;

        public IReadOnlyList<DrawCommand> Commands => new Vector<Native.ImDrawCmd, DrawCommand>(_list->CmdBuffer.Data, _list->CmdBuffer.Size);

        public IReadOnlyList<DrawIndex> Indexes => new Vector<Native.ImDrawIdx, DrawIndex>(_list->IdxBuffer.Data, _list->IdxBuffer.Size);

        public IReadOnlyList<DrawVertex> Vertices => new Vector<Native.ImDrawVert, DrawVertex>(_list->VtxBuffer.Data, _list->VtxBuffer.Size);

        public DrawListOptions Options => (DrawListOptions)_list->Flags;

        private DrawList(Native.ImDrawList* list)
        {
            _list = list;
        }

        public static DrawList Wrap(Native.ImDrawList* native) => new(native);

        public Native.ImDrawList* ToNative() => _list;
    }
}
