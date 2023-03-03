namespace ImguiSharp
{
    public readonly unsafe struct StateOption<T> : IDisposable
        where T : unmanaged, Enum
    {
        private readonly int* _value;

        public T Value
        {
            get => (T)(object)*_value;
            set => *_value = (int)(object)value;
        }

        public StateOption(T initialValue = default)
        {
            if (sizeof(T) != sizeof(int))
            {
                throw new InvalidOperationException();
            }

            _value = (int*)Native.ImGui_MemAlloc(sizeof(int));
            Value = initialValue;
        }

        public void Dispose() => Native.ImGui_MemFree(_value);

        public static implicit operator T(StateOption<T> v) => v.Value;

        internal int* ToNative() => _value;
    }

    public static unsafe class StateOptionExtensions
    {
        public static int* ToNative<T>(this StateOption<T>? v) where T : unmanaged, Enum => v == null ? null : v.Value.ToNative();
    }
}
