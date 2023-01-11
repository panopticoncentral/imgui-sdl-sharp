namespace SdlSharp.Imgui
{
    public sealed unsafe class DrawData
    {
        private Native.ImDrawData* _data;

        public static DrawData Instance => new(Native.ImGui_GetDrawData());

        internal DrawData(Native.ImDrawData* data) 
        { 
            _data = data; 
        }

        internal Native.ImDrawData* ToNative() => _data;
    }
}
