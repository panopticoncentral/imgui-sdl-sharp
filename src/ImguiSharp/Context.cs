namespace ImguiSharp
{
    public readonly unsafe struct Context : INativeWrapper<Context, Native.ImGuiContext>
    {
        private readonly Native.ImGuiContext* _context;

        private Context(Native.ImGuiContext* context)
        {
            _context = context;
        }

        public static Context Wrap(Native.ImGuiContext* context) => new(context);

        public Native.ImGuiContext* ToNative() => _context;
    }
}
