namespace ImguiSharp
{
    public readonly unsafe record struct Size(float Width, float Height) : INativeValueWrapper<Size, Native.ImVec2>
    {
        public static Size Wrap(Native.ImVec2 native) => new(native.X, native.Y);

        public Native.ImVec2 ToNative() => new(Width, Height);
    }
}
