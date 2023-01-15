namespace SdlSharp.Imgui
{
    public readonly unsafe struct DrawList
    {
        private readonly Native.ImDrawList* _list;

        internal DrawList(Native.ImDrawList* list)
        {
            _list = list;
        }

        internal Native.ImDrawList* ToNative() => _list;
    }
}
