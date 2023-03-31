namespace ImguiSharp
{
    public sealed unsafe class ListClipper : IDisposable
    {
        private Native.ImGuiListClipper* _clipper;

        public int DisplayStart => _clipper->DisplayStart;

        public int DisplayEnd => _clipper->DisplayEnd;

        public ListClipper()
        {
            _clipper = (Native.ImGuiListClipper*)Native.ImGui_MemAlloc((nuint)sizeof(Native.ImGuiListClipper));
        }

        public void Begin(int count, float height = -1.0f) => Native.ImGuiListClipper_Begin(_clipper, count, height);

        public void End() => Native.ImGuiListClipper_End(_clipper);

        public bool Step() => Native.ImGuiListClipper_Step(_clipper);

        public void ForceDisplayRangeByIndicies(int min, int max) => Native.ImGuiListClipper_ForceDisplayRangeByIndices(_clipper, min, max);

        public void Dispose()
        {
            if (_clipper != null)
            {
                Native.ImGui_MemFree(_clipper);
                _clipper = null;
            }
        }
    }
}
