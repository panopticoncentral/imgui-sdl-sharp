namespace SdlSharp.Imgui
{
    public sealed unsafe class StateText
    {
        private byte* _buffer;
        private int _size;

        public StateText(int size)
        {
            _buffer = (byte*)Native.ImGui_MemAlloc((nuint)size);
            _size = size;
        }

        public void Dispose() => Native.ImGui_MemFree(_buffer);

        internal byte* ToNative() => _buffer;

        internal void Resize(int newSize)
        {
            var newBuffer = (byte*)Native.ImGui_MemAlloc((nuint)newSize);
            new Span<byte>(_buffer, _size).CopyTo(new Span<byte>(newBuffer, newSize));
            Native.ImGui_MemFree(_buffer);
            _buffer = newBuffer;
            _size = newSize;
        }

        public override string ToString() => SdlSharp.Native.Utf8ToString(_buffer)!;
    }
}
