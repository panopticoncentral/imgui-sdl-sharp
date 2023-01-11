using SdlSharp.Graphics;

namespace SdlSharp.Imgui
{
    public static unsafe class ImGui
    {
        public static void NewFrame() => Native.ImGui_NewFrame();

        public static void EndFrame() => Native.ImGui_EndFrame();

        public static void Render() => Native.ImGui_Render();
    }
}
