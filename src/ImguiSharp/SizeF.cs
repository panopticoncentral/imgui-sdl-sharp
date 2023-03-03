namespace ImguiSharp
{
    public readonly unsafe record struct SizeF(float Width, float Height) : INativeValueWrapper<SizeF, Native.ImVec2>
    {
        public const float MinNormalizedValue = 1.175494351e-38F;

        public static SizeF Wrap(Native.ImVec2 native) => new(native.X, native.Y);

        public Native.ImVec2 ToNative() => new(Width, Height);
    }
}
