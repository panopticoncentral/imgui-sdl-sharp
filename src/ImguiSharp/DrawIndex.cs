namespace ImguiSharp
{
    public readonly unsafe struct DrawIndex : INativeWrapper<DrawIndex, Native.ImDrawIdx>
    {
        private readonly Native.ImDrawIdx* _idx;

        private DrawIndex(Native.ImDrawIdx* idx)
        {
            _idx = idx;
        }

        public static DrawIndex Wrap(Native.ImDrawIdx* native) => new(native);

        public Native.ImDrawIdx* ToNative() => _idx;
    }
}
