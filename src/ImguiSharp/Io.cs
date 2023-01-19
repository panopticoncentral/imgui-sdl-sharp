namespace ImguiSharp
{
    public readonly unsafe struct Io
    {
        private readonly Native.ImGuiIO* _io;

        internal Io(Native.ImGuiIO* io)
        {
            _io = io;
        }

        internal Native.ImGuiIO* ToNative() => _io;
    }
}
