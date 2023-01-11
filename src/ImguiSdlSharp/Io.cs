namespace SdlSharp.Imgui
{
    public sealed unsafe class Io
    {
        private Native.ImGuiIO* _io;

        public static Io Instance => new(Native.ImGui_GetIO());

        internal Io(Native.ImGuiIO* io)
        { 
            _io = io;
        }

        internal Native.ImGuiIO* ToNative() => _io;
    }
}
