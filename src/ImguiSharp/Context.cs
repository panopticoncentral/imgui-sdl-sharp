namespace ImguiSharp
{
    public readonly unsafe struct Context : INativeReferenceWrapper<Context, Native.ImGuiContext>
    {
        private readonly Native.ImGuiContext* _context;

        private Context(Native.ImGuiContext* context)
        {
            _context = context;
        }

        public static Context Wrap(Native.ImGuiContext* native) => new(native);

        public Native.ImGuiContext* ToNative() => _context;
    }
}
