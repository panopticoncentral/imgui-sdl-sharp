namespace ImguiSharp
{
    public readonly record struct Id
    {
        private readonly Native.ImGuiID _id;

        internal Id(Native.ImGuiID id)
        {
            _id = id;
        }

        internal Native.ImGuiID ToNative() => _id;
    }
}
