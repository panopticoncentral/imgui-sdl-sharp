namespace ImguiSharp
{
    public readonly unsafe struct DrawList : INativeReferenceWrapper<DrawList, Native.ImDrawList>
    {
        private readonly Native.ImDrawList* _list;

        public ReferenceVector<Native.ImDrawCmd, DrawCommand> Commands => new(_list->CmdBuffer.Data, _list->CmdBuffer.Size);

        public ValueVector<Native.ImDrawIdx, DrawIndex> Indexes => new(_list->IdxBuffer.Data, _list->IdxBuffer.Size);

        public ValueVector<Native.ImDrawVert, DrawVertex> Vertices => new(_list->VtxBuffer.Data, _list->VtxBuffer.Size);

        public DrawListOptions Options => (DrawListOptions)_list->Flags;

        private DrawList(Native.ImDrawList* list)
        {
            _list = list;
        }

        public static DrawList? Wrap(Native.ImDrawList* native) => native == null ? null : new(native);

        public Native.ImDrawList* ToNative() => _list;
    }
}
