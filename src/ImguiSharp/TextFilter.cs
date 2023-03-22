namespace ImguiSharp
{
    public sealed unsafe class TextFilter : IDisposable
    {
        private Native.ImGuiTextFilter* _textFilter;

        public bool IsActive => Native.ImGuiTextFilter_IsActive(_textFilter);

        public TextFilter()
        {
            _textFilter = (Native.ImGuiTextFilter*)Native.ImGui_MemAlloc((nuint)sizeof(Native.ImGuiTextFilter));
            new Span<byte>(_textFilter, sizeof(Native.ImGuiTextFilter)).Clear();
        }

        public void Dispose()
        {
            Native.ImGui_MemFree(_textFilter);
            _textFilter = null;
        }

        public bool Draw(string label = "Filter (inc,-exc)", float width = 0.0f) => Native.StringToUtf8Func(label, labelPtr => Native.ImGuiTextFilter_Draw(_textFilter, labelPtr, width));

        public bool PassFilter(string text) => Native.StringToUtf8Func(text, textPtr => Native.ImGuiTextFilter_PassFilter(_textFilter, textPtr));

        public void Build() => Native.ImGuiTextFilter_Build(_textFilter);

        public void Clear() => Native.ImGuiTextFilter_Clear(_textFilter);
    }
}
