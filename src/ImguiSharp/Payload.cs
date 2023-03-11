namespace ImguiSharp
{
    public readonly unsafe struct Payload : INativeReferenceWrapper<Payload, Native.ImGuiPayload>
    {
        private readonly Native.ImGuiPayload* _payload;

        public const string ColorType = Native.IMGUI_PAYLOAD_TYPE_COLOR_3F;
        public const string ColorAlphaType = Native.IMGUI_PAYLOAD_TYPE_COLOR_4F;

        private Payload(Native.ImGuiPayload* payload)
        {
            _payload = payload;
        }

        public Span<T> GetData<T>() where T : unmanaged => new((T*)_payload->Data, _payload->DataSize / sizeof(T));

        public static Payload? Wrap(Native.ImGuiPayload* native) => native == null ? null : new(native);

        public Native.ImGuiPayload* ToNative() => _payload;
    }
}
