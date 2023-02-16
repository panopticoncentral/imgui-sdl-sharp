namespace ImguiSharp
{
    public readonly unsafe struct PlatformImeData : INativeReferenceWrapper<PlatformImeData, Native.ImGuiPlatformImeData>
    {
        private readonly Native.ImGuiPlatformImeData* _native;

        public bool WantVisible
        {
            get => _native->WantVisible;
            set => _native->WantVisible = value;
        }

        public Position InputPos
        {
            get => Position.Wrap(_native->InputPos);
            set => _native->InputPos = value.ToNative();
        }

        public float InputLineHeight
        {
            get => _native->InputLineHeight;
            set => _native->InputLineHeight = value;
        }

        private PlatformImeData(Native.ImGuiPlatformImeData* native)
        {
            _native = native;
        }

        public static PlatformImeData? Wrap(Native.ImGuiPlatformImeData* native) => native == null ? null : new(native);

        public unsafe Native.ImGuiPlatformImeData* ToNative() => _native;
    }
}
