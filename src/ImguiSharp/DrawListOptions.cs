namespace ImguiSharp
{
    [Flags]
    public enum DrawListOptions
    {
        None = Native.ImDrawListFlags.ImDrawListFlags_None,
        AntiAliasedLines = Native.ImDrawListFlags.ImDrawListFlags_AntiAliasedLines,
        AntiAliasedLinesUseTexture = Native.ImDrawListFlags.ImDrawListFlags_AntiAliasedLinesUseTex,
        AntiAliasedFill = Native.ImDrawListFlags.ImDrawListFlags_AntiAliasedFill,
        AllowVtxOffset = Native.ImDrawListFlags.ImDrawListFlags_AllowVtxOffset,
    }
}
