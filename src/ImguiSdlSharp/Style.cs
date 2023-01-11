namespace SdlSharp.Imgui
{
    public sealed unsafe class Style
    {
        private Native.ImGuiStyle* _style;

        public static Style Instance => new(Native.ImGui_GetStyle());

        internal Style(Native.ImGuiStyle* style)
        { 
            _style = style;
        }

        public static void Dark(Style? style = null) => Native.ImGui_StyleColorsDark(style == null ? null : style.ToNative());

        public static void Light(Style? style = null) => Native.ImGui_StyleColorsLight(style == null ? null : style.ToNative());

        public static void Classic(Style? style = null) => Native.ImGui_StyleColorsClassic(style == null ? null : style.ToNative());

        internal Native.ImGuiStyle* ToNative() => _style;
    }
}
