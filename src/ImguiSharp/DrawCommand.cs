namespace ImguiSharp
{
    public readonly unsafe struct DrawCommand : INativeReferenceWrapper<DrawCommand, Native.ImDrawCmd>
    {
        private readonly Native.ImDrawCmd* _cmd;

        public DrawCommandKind Kind =>
            _cmd->UserCallback == null
                ? DrawCommandKind.Vertex
                : (nuint)_cmd->UserCallback == Native.ImDrawCallback_ResetRenderState
                    ? DrawCommandKind.ResetRenderState
                    : DrawCommandKind.Callback;

        public RectangleF ClipRectangle => RectangleF.Wrap(_cmd->ClipRect);

        public TextureId TextureId => TextureId.Wrap(Native.ImDrawCmd_GetTexID(_cmd));

        public uint VertexOffset => _cmd->VtxOffset;

        public uint IndexOffset => _cmd->IdxOffset;

        public uint ElementCount => _cmd->ElemCount;

        private DrawCommand(Native.ImDrawCmd* cmd)
        {
            _cmd = cmd;
        }

        public void DoCallback(DrawList drawList)
        {
            if ((nuint)_cmd->UserCallback == unchecked((nuint)(-2)))
            {
                if (Imgui.DrawListCallbacks.TryGetValue((nuint)_cmd->UserCallbackData, out var action))
                {
                    action(drawList, this);
                }
            }
            else
            {
                _cmd->UserCallback(drawList.ToNative(), _cmd);
            }
        }

        public static DrawCommand? Wrap(Native.ImDrawCmd* native) => native == null ? null : new(native);

        public Native.ImDrawCmd* ToNative() => _cmd;
    }
}
