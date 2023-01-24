using System.Formats.Asn1;

namespace ImguiSharp
{
    public readonly unsafe struct DrawCommand
    {
        private readonly Native.ImDrawCmd* _cmd;

        public Rectangle ClipRectangle => new(_cmd->ClipRect);

        public TextureId TextureId => new(_cmd->TextureId);

        public uint VertexOffset => _cmd->VtxOffset;

        public uint IndexOffset => _cmd->IdxOffset;

        public uint ElementCount => _cmd->ElemCount;

        ImDrawCallback UserCallback;      // 4-8  // If != NULL, call the function instead of rendering the vertices. clip_rect and texture_id will be set normally.
        void* UserCallbackData;  // 4-8  // The draw callback code can access this.

        internal DrawCommand(Native.ImDrawCmd* cmd)
        {
            _cmd = cmd;
        }

        internal Native.ImDrawCmd* ToNative() => _cmd;
    }
}
