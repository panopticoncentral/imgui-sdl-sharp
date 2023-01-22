namespace ImguiSharp
{
    public readonly unsafe struct Storage
    {
        private readonly Native.ImGuiStorage* _storage;

        internal Storage(Native.ImGuiStorage* storage)
        {
            _storage = storage;
        }

        internal Native.ImGuiStorage* ToNative() => _storage;
    }
}
