namespace SdlSharp.Imgui
{
    public sealed unsafe class State<T> : IDisposable
        where T : unmanaged
    {
        private readonly T* _value;

        public T Value => *_value;

        public State()
        {
            _value = (T*)Native.ImGui_MemAlloc((nuint)sizeof(T));
        }

        public void Dispose() => Native.ImGui_MemFree(_value);

        internal T* ToNative() => _value;
    }
}
