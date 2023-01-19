namespace ImguiSharp
{
    public readonly unsafe record struct Color(float Red, float Green, float Blue, float Alpha)
    {
        internal Color(Native.ImVec4* color) : this(color->X, color->Y, color->Z, color->W)
        {
        }

        internal Native.ImVec4 ToNative() => new(Red, Green, Blue, Alpha);
    }
}
