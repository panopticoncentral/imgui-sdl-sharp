namespace ImguiSharp
{
    public readonly unsafe struct State<T> : IDisposable
        where T : unmanaged
    {
        private readonly T* _value;

        public T Value
        {
            get => *_value;
            set => *_value = value;
        }

        public State(T initialValue = default)
        {
            _value = (T*)Native.ImGui_MemAlloc((nuint)sizeof(T));
            Value = initialValue;
        }

        internal State(T* value)
        {
            _value = value;
        }

        public void Dispose() => Native.ImGui_MemFree(_value);

        public static implicit operator T(State<T> v) => v.Value;

        internal T* ToNative() => _value;
    }

    public static unsafe class StateExtensions
    {
        public static T* ToNative<T>(this State<T>? v) where T : unmanaged => v == null ? null : v.Value.ToNative();
    }
}
