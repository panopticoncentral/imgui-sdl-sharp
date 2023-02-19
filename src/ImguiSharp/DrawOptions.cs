namespace ImguiSharp
{
    public enum DrawOptions
    {
        None = Native.ImDrawFlags.ImDrawFlags_None,
        Closed = Native.ImDrawFlags.ImDrawFlags_Closed,
        RoundCornersTopLeft = Native.ImDrawFlags.ImDrawFlags_RoundCornersTopLeft,
        RoundCornersTopRight = Native.ImDrawFlags.ImDrawFlags_RoundCornersTopRight,
        RoundCornersBottomLeft = Native.ImDrawFlags.ImDrawFlags_RoundCornersBottomLeft,
        RoundCornersBottomRight = Native.ImDrawFlags.ImDrawFlags_RoundCornersBottomRight,
        RoundCornersNone = Native.ImDrawFlags.ImDrawFlags_RoundCornersNone,
        RoundCornersTop = Native.ImDrawFlags.ImDrawFlags_RoundCornersTop,
        RoundCornersBottom = Native.ImDrawFlags.ImDrawFlags_RoundCornersBottom,
        RoundCornersLeft = Native.ImDrawFlags.ImDrawFlags_RoundCornersLeft,
        RoundCornersRight = Native.ImDrawFlags.ImDrawFlags_RoundCornersRight,
        RoundCornersAll = Native.ImDrawFlags.ImDrawFlags_RoundCornersAll,
        RoundCornersDefault = Native.ImDrawFlags.ImDrawFlags_RoundCornersDefault_,
        RoundCornersMask = Native.ImDrawFlags.ImDrawFlags_RoundCornersMask_,
    }
}
