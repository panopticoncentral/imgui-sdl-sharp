namespace ImguiSharp
{
    public readonly unsafe struct Payload : INativeWrapper<Payload, Native.ImGuiPayload>
    {
        private readonly Native.ImGuiPayload* _payload;

        private Payload(Native.ImGuiPayload* payload)
        {
            _payload = payload;
        }

        public static Payload Wrap(Native.ImGuiPayload* native) => new(native);

        public Native.ImGuiPayload* ToNative() => _payload;
    }
}
