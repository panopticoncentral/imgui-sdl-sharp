namespace SdlSharp.Imgui
{
    public sealed unsafe class State<T> : IDisposable
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

        public void Dispose() => Native.ImGui_MemFree(_value);

        public static implicit operator T(State<T> v) => v.Value;

        internal T* ToNative() => _value;
    }
}
