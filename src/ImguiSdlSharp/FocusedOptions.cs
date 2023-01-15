namespace SdlSharp.Imgui
{
    public enum FocusedOptions
    {
        None = Native.ImGuiFocusedFlags.ImGuiFocusedFlags_None,
        ChildWindows = Native.ImGuiFocusedFlags.ImGuiFocusedFlags_ChildWindows,
        RootWindow = Native.ImGuiFocusedFlags.ImGuiFocusedFlags_RootWindow,
        AnyWindow = Native.ImGuiFocusedFlags.ImGuiFocusedFlags_AnyWindow,
        NoPopupHierarchy = Native.ImGuiFocusedFlags.ImGuiFocusedFlags_NoPopupHierarchy,

        RootAndChildWindows = Native.ImGuiFocusedFlags.ImGuiFocusedFlags_RootAndChildWindows,
    }
}
