namespace ImguiSharp
{
    public sealed unsafe class StateVector<T> : IDisposable
        where T : unmanaged
    {
        private readonly T* _value;

        public int Length { get; }

        public T this[int index] => index >= 0 && index < Length ? _value[index] : throw new InvalidOperationException();

        public StateVector(int length)
        {
            _value = (T*)Native.ImGui_MemAlloc((nuint)(sizeof(T) * length));
            Length = length;
        }

        public void Dispose() => Native.ImGui_MemFree(_value);

        internal T* ToNative() => _value;
    }
}
