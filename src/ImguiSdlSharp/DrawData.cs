namespace SdlSharp.Imgui
{
    public readonly unsafe struct DrawData
    {
        private readonly Native.ImDrawData* _data;

        internal DrawData(Native.ImDrawData* data)
        {
            _data = data;
        }

        internal Native.ImDrawData* ToNative() => _data;
    }
}
