namespace ImguiSharp
{
    public sealed unsafe class FontGlyphRangesBuilder : IDisposable
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

        public char[] BuildRanges()
        {
            Native.ImVector_ImWchar vector;
            Native.ImFontGlyphRangesBuilder_BuildRanges(_builder, &vector);
            var array = vector.Size == 0 ? Array.Empty<char>() : new Span<char>(vector.Data, vector.Size).ToArray();
            if (vector.Data != null)
            {
                Native.ImGui_MemFree(vector.Data);
            }
            return array;
        }

        public void Dispose()
        {
            if (_builder != null)
            {
                if (_builder->UsedChars.Data != null)
                {
                    Native.ImGui_MemFree(_builder->UsedChars.Data);
                }
                Native.ImGui_MemFree(_builder);
                _builder = null;
            }
        }
    }
}
