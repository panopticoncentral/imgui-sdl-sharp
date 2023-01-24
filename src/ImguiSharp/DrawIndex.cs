namespace ImguiSharp
{
    public readonly unsafe struct DrawIndex
    {
        private readonly Native.ImDrawIdx* _idx;

        internal DrawIndex(Native.ImDrawIdx* idx)
        {
            _idx = idx;
        }

        internal Native.ImDrawIdx* ToNative() => _idx;
    }
}
