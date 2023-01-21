namespace ImguiSharp
{
    [Flags]
    public enum BackendOptions
    {
        None = Native.ImGuiBackendFlags.ImGuiBackendFlags_None,
        HasGamepad = Native.ImGuiBackendFlags.ImGuiBackendFlags_HasGamepad,
        HasMouseCursors = Native.ImGuiBackendFlags.ImGuiBackendFlags_HasMouseCursors,
        HasSetMousePos = Native.ImGuiBackendFlags.ImGuiBackendFlags_HasSetMousePos,
        RendererHasVtxOffset = Native.ImGuiBackendFlags.ImGuiBackendFlags_RendererHasVtxOffset,
    }
}
