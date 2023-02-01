namespace ImguiSharp
{
    public readonly unsafe record struct DrawIndex(ushort Index) : INativeValueWrapper<DrawIndex, Native.ImDrawIdx>
    {
        public static DrawIndex Wrap(Native.ImDrawIdx native) => new(native.Value);

        public Native.ImDrawIdx ToNative() => new(Index);
    }
}
