namespace ImguiSharp
{
    public sealed unsafe class StateText : IDisposable
    {
        private byte* _value;

        public int Length { get; private set; }

        public StateText(int length, string? initializer = null)
        {
            _value = (byte*)Native.ImGui_MemAlloc((nuint)length);
            Length = length;

            if (initializer != null)
            {
                Native.StringToUtf8(initializer).CopyTo(new Span<byte>(_value, Length));
            }
            else
            {
                _value[0] = 0;
            }
        }

        public void Dispose() => Native.ImGui_MemFree(_value);

        public void Resize(int newLength)
        {
            var newValue = (byte*)Native.ImGui_MemAlloc((nuint)newLength);
            new Span<byte>(_value, Math.Min(Length, newLength)).CopyTo(new Span<byte>(newValue, newLength));
            Native.ImGui_MemFree(_value);
            _value = newValue;
            Length = newLength;
        }

        public override string ToString() => Native.Utf8ToString(_value)!;

        internal byte* ToNative() => _value;
    }
}
