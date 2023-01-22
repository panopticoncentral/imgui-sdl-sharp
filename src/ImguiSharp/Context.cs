namespace ImguiSharp
{
    public readonly unsafe struct Context
    {
        private readonly Native.ImGuiContext* _context;

        internal Context(Native.ImGuiContext* context)
        {
            _context = context;
        }

        internal Native.ImGuiContext* ToNative() => _context;
    }
}
