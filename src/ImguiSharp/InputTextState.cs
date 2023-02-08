namespace ImguiSharp
{
    public readonly unsafe struct InputTextState : INativeReferenceWrapper<InputTextState, Native.ImGuiInputTextCallbackData>
    {
        private readonly Native.ImGuiInputTextCallbackData* _data;

        private InputTextState(Native.ImGuiInputTextCallbackData* data)
        {
            _data = data;
        }

        public static InputTextState? Wrap(Native.ImGuiInputTextCallbackData* native) => new(native);

        public Native.ImGuiInputTextCallbackData* ToNative() => _data;
    }
}
