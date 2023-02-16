namespace ImguiSharp
{
    public unsafe struct FontGlyphRangesBuilder : IDisposable
    {
        private Native.ImFontGlyphRangesBuilder* _builder;

        public FontGlyphRangesBuilder()
        {
            _builder = (Native.ImFontGlyphRangesBuilder*)Native.ImGui_MemAlloc((nuint)sizeof(Native.ImFontGlyphRangesBuilder));
        }

        public void Clear() => Native.ImFontGlyphRangesBuilder_Clear(_builder);

        public bool GetBit(nuint n) => Native.ImFontGlyphRangesBuilder_GetBit(_builder, n);

        public void SetBit(nuint n) => Native.ImFontGlyphRangesBuilder_SetBit(_builder, n);

        public void AddChar(char c) => Native.ImFontGlyphRangesBuilder_AddChar(_builder, c);

        public void AddText(string s)
        {
            fixed (byte* sPtr = Native.StringToUtf8(s))
            {
                Native.ImFontGlyphRangesBuilder_AddText(_builder, sPtr);
            }
        }

        public void AddRanges(Span<char> ranges)
        {
            fixed (char* rangesPtr = ranges)
            {
                Native.ImFontGlyphRangesBuilder_AddRanges(_builder, rangesPtr);
            }
        }

        //public void BuildRanges(ImVector_ImWchar* out_ranges) => Native.ImFontGlyphRangesBuilder_BuildRanges(_builder);

        public void Dispose()
        {
            if (_builder != null)
            {
                Native.ImGui_MemFree(_builder);
                _builder = null;
            }
        }
    }
}
