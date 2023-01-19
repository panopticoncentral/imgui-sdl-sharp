namespace ImguiSharp
{
    public readonly unsafe struct Context
    {
        private readonly Native.ImGuiContext* _context;

        public static string Version => Native.Utf8ToString(Native.ImGui_GetVersion())!;

        internal Context(Native.ImGuiContext* context)
        {
            _context = context;
        }

        internal Native.ImGuiContext* ToNative() => _context;
    }
}
