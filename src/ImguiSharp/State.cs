namespace ImguiSharp
{
    public readonly unsafe struct State<T> : IDisposable
        where T : unmanaged
    {
        internal readonly T* ValuePointer;

        public T Value
        {
            get => *ValuePointer;
            set => *ValuePointer = value;
        }

        public State(T initialValue = default)
        {
            ValuePointer = (T*)Native.ImGui_MemAlloc((nuint)sizeof(T));
            Value = initialValue;
        }

        internal State(T* value)
        {
            ValuePointer = value;
        }

        public void Dispose() => Native.ImGui_MemFree(ValuePointer);

        public static implicit operator T(State<T> v) => v.Value;

        internal T* ToNative() => ValuePointer;
    }

    public static unsafe class StateExtensions
    {
        public static T* ToNative<T>(this State<T>? v) where T : unmanaged => v == null ? null : v.Value.ToNative();

        public static StateVector<float> ToVector(this State<ColorF> v) => new((float*)v.ValuePointer, 3);

        public static StateVector<float> ToVectorAlpha(this State<ColorF> v) => new((float*)v.ValuePointer, 4);
    }
}
