namespace ImguiSharp
{
    public readonly unsafe record struct Id(uint Value) : INativeValueWrapper<Id, Native.ImGuiID>
    {
        public static Id Wrap(Native.ImGuiID native) => new(native.Value);

        public Native.ImGuiID ToNative() => new(Value);
    }
}
