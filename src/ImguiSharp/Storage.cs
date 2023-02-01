namespace ImguiSharp
{
    public readonly unsafe struct Storage : INativeReferenceWrapper<Storage, Native.ImGuiStorage>
    {
        private readonly Native.ImGuiStorage* _storage;

        private Storage(Native.ImGuiStorage* storage)
        {
            _storage = storage;
        }

        public static Storage Wrap(Native.ImGuiStorage* native) => new(native);

        public Native.ImGuiStorage* ToNative() => _storage;
    }
}
