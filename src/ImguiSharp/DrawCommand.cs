namespace ImguiSharp
{
    public readonly unsafe struct DrawCommand
    {
        private readonly Native.ImDrawCmd* _cmd;

        public DrawCommandKind Kind =>
            _cmd->UserCallback == null
                ? DrawCommandKind.Vertex
                : (nint)_cmd->UserCallback == Native.ImDrawCallback_ResetRenderState
                    ? DrawCommandKind.ResetRenderState
                    : DrawCommandKind.Callback;

        public Rectangle ClipRectangle => new(_cmd->ClipRect);

        public TextureId TextureId => new(_cmd->TextureId);

        public uint VertexOffset => _cmd->VtxOffset;

        public uint IndexOffset => _cmd->IdxOffset;

        public uint ElementCount => _cmd->ElemCount;

        internal DrawCommand(Native.ImDrawCmd* cmd)
        {
            _cmd = cmd;
        }

        public void DoCallback(DrawList drawList) => _cmd->UserCallback(drawList.ToNative(), _cmd);

        internal Native.ImDrawCmd* ToNative() => _cmd;
    }
}
