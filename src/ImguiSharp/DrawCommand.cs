namespace ImguiSharp
{
    public readonly unsafe struct DrawCommand
    {
        private readonly Native.ImDrawCmd* _cmd;

        internal DrawCommand(Native.ImDrawCmd* cmd)
        {
            _cmd = cmd;
        }

        internal Native.ImDrawCmd* ToNative() => _cmd;
    }
}
