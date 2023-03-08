namespace ImguiSharp
{
    public sealed unsafe class StateText : IDisposable
    {
        private byte* _value;

        public int Capacity { get; private set; }

        public StateText(int capacity, string? initializer = null)
        {
            if (capacity < 1)
            {
                throw new InvalidOperationException();
            }

            _value = (byte*)Native.ImGui_MemAlloc((nuint)capacity);
            Capacity = capacity;

            if (initializer != null)
            {
                Native.StringToUtf8(initializer).CopyTo(new Span<byte>(_value, Capacity));
            }
            else
            {
                _value[0] = 0;
            }
        }

        public void Dispose() => Native.ImGui_MemFree(_value);

        public void Resize(int newCapacity)
        {
            if (newCapacity < 1)
            {
                throw new InvalidOperationException();
            }
            var newValue = (byte*)Native.ImGui_MemAlloc((nuint)newCapacity);
            new Span<byte>(_value, Math.Min(Capacity, newCapacity)).CopyTo(new Span<byte>(newValue, newCapacity));
            Native.ImGui_MemFree(_value);
            _value = newValue;
            Capacity = newCapacity;
        }

        public override string ToString() => Native.Utf8ToString(_value)!;

        internal byte* ToNative() => _value;
    }
}
