namespace ImguiSharp
{
    public readonly unsafe struct Payload
    {
        private readonly Native.ImGuiPayload* _payload;

        internal Payload(Native.ImGuiPayload* payload)
        {
            _payload = payload;
        }

        internal Native.ImGuiPayload* ToNative() => _payload;
    }
}
