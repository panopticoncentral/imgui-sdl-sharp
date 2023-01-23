namespace ImguiSharp
{
    public enum FontAtlasOptions
    {
        None = Native.ImFontAtlasFlags.ImFontAtlasFlags_None,
        NoPowerOfTwoHeight = Native.ImFontAtlasFlags.ImFontAtlasFlags_NoPowerOfTwoHeight,
        NoMouseCursors = Native.ImFontAtlasFlags.ImFontAtlasFlags_NoMouseCursors,
        NoBakedLines = Native.ImFontAtlasFlags.ImFontAtlasFlags_NoBakedLines,
    }
}
