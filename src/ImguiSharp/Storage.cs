namespace ImguiSharp
{
    public readonly unsafe struct Storage : INativeReferenceWrapper<Storage, Native.ImGuiStorage>
    {
        private readonly Native.ImGuiStorage* _storage;

        private Storage(Native.ImGuiStorage* storage)
        {
            _storage = storage;
        }

        public void Clear() => Native.ImGuiStorage_Clear(_storage);

        public int Get(Id key, int defaultVal = 0) => Native.ImGuiStorage_GetInt(_storage, key.ToNative(), defaultVal);

        public void Set(Id key, int val) => Native.ImGuiStorage_SetInt(_storage, key.ToNative(), val);

        public bool Get(Id key, bool defaultVal = false) => Native.ImGuiStorage_GetBool(_storage, key.ToNative(), defaultVal);

        public void Set(Id key, bool val) => Native.ImGuiStorage_SetBool(_storage, key.ToNative(), val);

        public float Get(Id key, float defaultVal = default) => Native.ImGuiStorage_GetFloat(_storage, key.ToNative(), defaultVal);

        public void Set(Id key, float val) => Native.ImGuiStorage_SetFloat(_storage, key.ToNative(), val);

        public nuint Get(Id key) => (nuint)Native.ImGuiStorage_GetVoidPtr(_storage, key.ToNative());

        public void Set(Id key, nuint val) => Native.ImGuiStorage_SetVoidPtr(_storage, key.ToNative(), (void*)val);

        public Span<int> GetRef(Id key, int defaultVal = 0) => new(Native.ImGuiStorage_GetIntRef(_storage, key.ToNative(), defaultVal), 1);

        public Span<bool> GetRef(Id key, bool defaultVal = false) => new(Native.ImGuiStorage_GetBoolRef(_storage, key.ToNative(), defaultVal), 1);

        public Span<float> GetRef(Id key, float defaultVal = default) => new(Native.ImGuiStorage_GetFloatRef(_storage, key.ToNative(), defaultVal), 1);

        public Span<nuint> GetRef(Id key, nuint defaultVal = default) => new(Native.ImGuiStorage_GetVoidPtrRef(_storage, key.ToNative(), (void*)defaultVal), 1);

        public void SetAll(int val) => Native.ImGuiStorage_SetAllInt(_storage, val);

        public void BuildSortByKey() => Native.ImGuiStorage_BuildSortByKey(_storage);

        public static Storage? Wrap(Native.ImGuiStorage* native) => native == null ? null : new(native);

        public Native.ImGuiStorage* ToNative() => _storage;
    }
}
