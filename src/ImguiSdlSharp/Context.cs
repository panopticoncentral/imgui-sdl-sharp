namespace SdlSharp.Imgui
{
    /// <summary>
    /// A ImGui context.
    /// </summary>
    public sealed unsafe class Context : IDisposable
    {
        private readonly Native.ImGuiContext* _context;

        /// <summary>
        /// The current context.
        /// </summary>
        public static Context Current
        {
            get => new(Native.ImGui_GetCurrentContext());
            set => Native.ImGui_SetCurrentContext(value.ToNative());
        }

        internal Context(Native.ImGuiContext* context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new context.
        /// </summary>
        /// <returns>The new context.</returns>
        public static Context Create()
        {
            return new Context(Native.ImGui_CreateContext());
        }

        /// <summary>
        /// Creates a new context.
        /// </summary>
        /// <param name="sharedFontAtlas">The font atlas to share.</param>
        /// <returns>The new context.</returns>
        public static Context Create(FontAtlas sharedFontAtlas)
        {
            return new Context(Native.ImGui_CreateContext(sharedFontAtlas.ToNative()));
        }

        /// <summary>
        /// Disposes the context.
        /// </summary>
        public void Dispose()
        {
            Native.ImGui_DestroyContext(_context);
        }

        internal Native.ImGuiContext* ToNative() => _context;
    }
}
