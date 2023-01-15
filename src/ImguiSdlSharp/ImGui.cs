using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SdlSharp.Imgui
{
    public static unsafe class Imgui
    {
        private static Dictionary<nuint, SizeCallback>? s_sizeCallbacks;
        private static Dictionary<nuint, SizeCallback> SizeCallbacks => s_sizeCallbacks ??= new Dictionary<nuint, SizeCallback>();

        public static Context CreateContext(FontAtlas? sharedFontAtlas = null) => new(Native.ImGui_CreateContext(sharedFontAtlas == null ? null : sharedFontAtlas.Value.ToNative()));

        public static void DestroyContext(Context? context = null) => Native.ImGui_DestroyContext(context == null ? null : context.Value.ToNative());

        public static Context GetCurrentContext() => new(Native.ImGui_GetCurrentContext());

        public static void SetCurrentContext(Context? context) => Native.ImGui_SetCurrentContext(context == null ? null : context.Value.ToNative());

        public static Io GetIo() => new(Native.ImGui_GetIO());

        public static Style GetStyle() => new(Native.ImGui_GetStyle());

        public static void NewFrame()
        {
            Native.ImGui_NewFrame();
            s_sizeCallbacks?.Clear();
        }

        public static void EndFrame() => Native.ImGui_EndFrame();

        public static void Render() => Native.ImGui_Render();

        public static DrawData GetDrawData() => new(Native.ImGui_GetDrawData());

        public static void ShowDemoWindow(State<bool>? openState = null) => Native.ImGui_ShowDemoWindow(openState == null ? null : openState.ToNative());

        public static void ShowMetricsWindow(State<bool>? openState = null) => Native.ImGui_ShowMetricsWindow(openState == null ? null : openState.ToNative());

        public static void ShowDebugLogWindow(State<bool>? openState = null) => Native.ImGui_ShowDebugLogWindow(openState == null ? null : openState.ToNative());

        public static void ShowStackToolWindow(State<bool>? openState = null) => Native.ImGui_ShowStackToolWindow(openState == null ? null : openState.ToNative());

        public static void ShowAboutWindow(State<bool>? openState = null) => Native.ImGui_ShowAboutWindow(openState == null ? null : openState.ToNative());

        public static void ShowStyleEditor(Style? style = null) => Native.ImGui_ShowStyleEditor(style == null ? null : style.ToNative());

        public static bool ShowStyleSelector(string label) => SdlSharp.Native.StringToUtf8Func(label, Native.ImGui_ShowStyleSelector);

        public static void ShowFontSelector(string label) => SdlSharp.Native.StringToUtf8Action(label, Native.ImGui_ShowFontSelector);

        public static void ShowUserGuide() => Native.ImGui_ShowUserGuide();

        public static void StyleColorsDark(Style? style) => Native.ImGui_StyleColorsDark(style == null ? null : style.ToNative());

        public static void StyleColorsLight(Style? style) => Native.ImGui_StyleColorsLight(style == null ? null : style.ToNative());

        public static void StyleColorsClassic(Style? style) => Native.ImGui_StyleColorsClassic(style == null ? null : style.ToNative());

        public static bool Begin(string name, State<bool>? openState = null, WindowOptions options = default) => SdlSharp.Native.StringToUtf8Func(name, ptr => Native.ImGui_Begin(ptr, openState == null ? null : openState.ToNative(), (Native.ImGuiWindowFlags)options));

        public static void End() => Native.ImGui_End();

        public static bool BeginChild(string name, Size? size = null, bool border = false, WindowOptions options = default) => SdlSharp.Native.StringToUtf8Func(name, ptr => Native.ImGui_BeginChild(ptr, size == null ? default : size.Value.ToNative(), border, (Native.ImGuiWindowFlags)options));

        public static void BeginChildId(Id id, Size? size = null, bool border = false, WindowOptions options = default) =>
            Native.ImGui_BeginChildID(id.ToNative(), size == null ? default : size.Value.ToNative(), border, (Native.ImGuiWindowFlags)options);

        public static void EndChild() => Native.ImGui_EndChild();

        public static bool IsWindowAppearing() => Native.ImGui_IsWindowAppearing();

        public static bool IsWindowCollapsed() => Native.ImGui_IsWindowCollapsed();

        public static bool IsWindowFocused(FocusedOptions options = default) => Native.ImGui_IsWindowFocused((Native.ImGuiFocusedFlags)options);

        public static bool IsWindowHovered(HoveredOptions options = default) => Native.ImGui_IsWindowHovered((Native.ImGuiHoveredFlags)options);

        public static DrawList GetWindowDrawList() => new(Native.ImGui_GetWindowDrawList());

        public static Position GetWindowPosition() => new(Native.ImGui_GetWindowPos());

        public static Size GetWindowSize() => new(Native.ImGui_GetWindowSize());

        public static float GetWindowWidth() => Native.ImGui_GetWindowWidth();

        public static float GetWindowHeight() => Native.ImGui_GetWindowHeight();

        public static void SetNextWindowPosition(Position position, Condition condition = default) => Native.ImGui_SetNextWindowPos(position.ToNative(), (Native.ImGuiCond)condition);

        public static void SetNextWindowPosition(Position position, Condition condition = default, Position pivot = default) => Native.ImGui_SetNextWindowPosEx(position.ToNative(), (Native.ImGuiCond)condition, pivot.ToNative());

        public static void SetNextWindowSize(Size size, Condition condition = default) => Native.ImGui_SetNextWindowSize(size.ToNative(), (Native.ImGuiCond)condition);

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static void NativeSizeCallback(Native.ImGuiSizeCallbackData* data)
        {
            if (SizeCallbacks.TryGetValue((nuint)data->UserData, out var callback))
            {
                SizeCallbackData wrapper = new(data);
                callback(ref wrapper);
            }
        }

        public static void SetNextWindowSizeConstraints(Size minimum, Size maximum, SizeCallback? callback = null)
        {
            if (callback != null)
            {
                SizeCallbacks[(nuint)callback.GetHashCode()] = callback;
                Native.ImGui_SetNextWindowSizeConstraints(minimum.ToNative(), maximum.ToNative(), &NativeSizeCallback, (void*)callback.GetHashCode());
            }
            else
            {
                Native.ImGui_SetNextWindowSizeConstraints(minimum.ToNative(), maximum.ToNative());
            }
        }

        public static void SetNextWindowContentSize(Size size) => Native.ImGui_SetNextWindowContentSize(size.ToNative());

        public static void SetNextWindowCollapsed(bool collapsed, Condition condition = default) => Native.ImGui_SetNextWindowCollapsed(collapsed, (Native.ImGuiCond)condition);

        public static void SetNextWindowFocus() => Native.ImGui_SetNextWindowFocus();

        public static void SetNextWindowScroll(Position scroll) => Native.ImGui_SetNextWindowScroll(scroll.ToNative());

        public static void SetNextWindowBackgroundAlpha(float alpha) => Native.ImGui_SetNextWindowBgAlpha(alpha);

        public static void SetWindowPosition(Position position, Condition condition) => Native.ImGui_SetWindowPos(position.ToNative(), (Native.ImGuiCond)condition);

        public static void SetWindowSize(Size size, Condition condition) => Native.ImGui_SetWindowSize(size.ToNative(), (Native.ImGuiCond)condition);

        public static void SetWindowCollapsed(bool collapsed, Condition condition) => Native.ImGui_SetWindowCollapsed(collapsed, (Native.ImGuiCond)condition);

        public static void SetWindowFocus() => Native.ImGui_SetWindowFocus();

        public static void SetNamedWindowPosition(string name, Position position, Condition condition = default) => SdlSharp.Native.StringToUtf8Action(name, ptr => Native.ImGui_SetWindowPosStr(ptr, position.ToNative(), (Native.ImGuiCond)condition));

        public static void SetNamedWindowSize(string name, Size size, Condition condition = default) => SdlSharp.Native.StringToUtf8Action(name, ptr => Native.ImGui_SetWindowSizeStr(ptr, size.ToNative(), (Native.ImGuiCond)condition));

        public static void SetNamedWindowCollapsed(string name, bool collapsed, Condition condition = default) => SdlSharp.Native.StringToUtf8Action(name, ptr => Native.ImGui_SetWindowCollapsedStr(ptr, collapsed, (Native.ImGuiCond)condition));

        public static void SetNamedWindowFocus(string name) => SdlSharp.Native.StringToUtf8Action(name, Native.ImGui_SetWindowFocusStr);

        public static Size GetContentRegionAvailable() => new(Native.ImGui_GetContentRegionAvail());

        public static Position GetContentRegionMax() => new(Native.ImGui_GetContentRegionMax());

        public static Position GetWindowContentRegionMin() => new(Native.ImGui_GetWindowContentRegionMin());

        public static Position GetWindowContentRegionMax() => new(Native.ImGui_GetWindowContentRegionMax());

        public static float GetScrollX() => Native.ImGui_GetScrollX();

        public static float GetScrollY() => Native.ImGui_GetScrollY();

        public static void SetScrollX(float value) => Native.ImGui_SetScrollX(value);

        public static void SetScrollY(float value) => Native.ImGui_SetScrollY(value);

        public static float GetScrollMaxX() => Native.ImGui_GetScrollMaxX();

        public static float GetScrollMaxY() => Native.ImGui_GetScrollMaxY();

        public static void SetScrollHereX(float centerRatio = 0.5f) => Native.ImGui_SetScrollHereX(centerRatio);

        public static void SetScrollHereY(float centerRatio = 0.5f) => Native.ImGui_SetScrollHereY(centerRatio);

        public static void SetScrollFromPositionX(float localX, float centerRatio = 0.5f) => Native.ImGui_SetScrollFromPosX(localX, centerRatio);

        public static void SetScrollFromPositionY(float localY, float centerRatio = 0.5f) => Native.ImGui_SetScrollFromPosY(localY, centerRatio);

        public static void PushFont(Font font) => Native.ImGui_PushFont(font.ToNative());

        public static void PopFont() => Native.ImGui_PopFont();

        public static void PushStyleColor(StyleColor property, uint color) => Native.ImGui_PushStyleColor((Native.ImGuiCol)property, color);

        public static void PushStyleColor(StyleColor property, Color color) => Native.ImGui_PushStyleColorImVec4((Native.ImGuiCol)property, color.ToNative());

        public static void PopStyleColor() => Native.ImGui_PopStyleColor();

        public static void PopStyleColor(int count) => Native.ImGui_PopStyleColorEx(count);

        public static void PushStyleVariable(StyleVariable variable, float value) => Native.ImGui_PushStyleVar((Native.ImGuiStyleVar)variable, value);

        public static void PushStyleVariable(StyleVariable variable, (float, float) value) => Native.ImGui_PushStyleVarImVec2((Native.ImGuiStyleVar)variable, new(value.Item1, value.Item2));

        public static void PopStyleVar() => Native.ImGui_PopStyleVar();

        public static void PopStyleVar(int count) => Native.ImGui_PopStyleVarEx(count);

        public static void PushAllowKeyboardFocus(bool allowFocus) => Native.ImGui_PushAllowKeyboardFocus(allowFocus);

        public static void PopAllowKeyboardFocus() => Native.ImGui_PopAllowKeyboardFocus();

        public static void PushButtonRepeat(bool repeat) => Native.ImGui_PushButtonRepeat(repeat);

        public static void PopButtonRepeat() => Native.ImGui_PopButtonRepeat();

        public static void PushItemWidth(float itemWidth) => Native.ImGui_PushItemWidth(itemWidth);

        public static void PopItemWidth() => Native.ImGui_PopItemWidth();

        public static void SetNextItemWidth(float itemWidth) => Native.ImGui_SetNextItemWidth(itemWidth);

        public static float CalcItemWidth() => Native.ImGui_CalcItemWidth();

        public static void PushTextWrapPosition(float wrapLocalPositionX = 0.0f) => Native.ImGui_PushTextWrapPos(wrapLocalPositionX);

        public static void PopTextWrapPosition() => Native.ImGui_PopTextWrapPos();

        public static Font GetFont() => new(Native.ImGui_GetFont());

        public static float GetFontSize() => Native.ImGui_GetFontSize();

        public static TextureCoordinate GetFontWhitePixelTextureCoordinate() => new(Native.ImGui_GetFontTexUvWhitePixel());

        public static uint GetColor(StyleColor color) => Native.ImGui_GetColorU32((Native.ImGuiCol)color);

        public static uint GetColor(StyleColor color, float alphaMul) => Native.ImGui_GetColorU32Ex((Native.ImGuiCol)color, alphaMul);

        public static uint GetColor(Color color) => Native.ImGui_GetColorU32ImVec4(color.ToNative());

        public static uint GetColor(uint color) => Native.ImGui_GetColorU32uint(color);

        public static Color GetStyleColor(StyleColor color) => new(Native.ImGui_GetStyleColorVec4((Native.ImGuiCol)color));

        public static void Separator() => Native.ImGui_Separator();

        public static void SameLine() => Native.ImGui_SameLine();

        public static void SameLine(float offsetFromStartX, float spacing) => Native.ImGui_SameLineEx(offsetFromStartX, spacing);

        public static void NewLine() => Native.ImGui_NewLine();

        public static void Spacing() => Native.ImGui_Spacing();

        public static void Dummy(Size size) => Native.ImGui_Dummy(size.ToNative());

        public static void Indent() => Native.ImGui_Indent();

        public static void Indent(float indent) => Native.ImGui_IndentEx(indent);

        public static void Unindent() => Native.ImGui_Unindent();

        public static void Unindent(float indent) => Native.ImGui_UnindentEx(indent);

        public static void BeginGroup() => Native.ImGui_BeginGroup();

        public static void EndGroup() => Native.ImGui_EndGroup();

        public static Position GetCursorPosition() => new(Native.ImGui_GetCursorPos());

        public static float GetCursorPosX() => Native.ImGui_GetCursorPosX();

        public static float GetCursorPosY() => Native.ImGui_GetCursorPosY();

        public static void SetCursorPosition(Position localPosition) => Native.ImGui_SetCursorPos(localPosition.ToNative());

        public static void SetCursorPosX(float localX) => Native.ImGui_SetCursorPosX(localX);

        public static void SetCursorPosY(float localY) => Native.ImGui_SetCursorPosY(localY);

        public static Position GetCursorStartPosition() => new(Native.ImGui_GetCursorStartPos());

        public static Position GetCursorScreenPosition() => new(Native.ImGui_GetCursorScreenPos());

        public static void SetCursorScreenPosition(Position position) => Native.ImGui_SetCursorScreenPos(position.ToNative());

        public static void AlignTextToFramePadding() => Native.ImGui_AlignTextToFramePadding();

        public static float GetTextLineHeight() => Native.ImGui_GetTextLineHeight();

        public static float GetTextLineHeightWithSpacing() => Native.ImGui_GetTextLineHeightWithSpacing();

        public static float GetFrameHeight() => Native.ImGui_GetFrameHeight();

        public static float GetFrameHeightWithSpacing() => Native.ImGui_GetFrameHeightWithSpacing();

        public static void PushID(string id) => SdlSharp.Native.StringToUtf8Action(id, Native.ImGui_PushID);

        public static void PushID(nuint id) => Native.ImGui_PushIDPtr((void*)id);

        public static void PushID(int id) => Native.ImGui_PushIDInt(id);

        public static void PopID() => Native.ImGui_PopID();

        public static Id GetID(string id) => SdlSharp.Native.StringToUtf8Func(id, ptr => new Id(Native.ImGui_GetID(ptr)));

        public static Id GetIDPtr(nuint id) => new(Native.ImGui_GetIDPtr((void*)id));

        public static void TextUnformatted(string text) => SdlSharp.Native.StringToUtf8Action(text, Native.ImGui_TextUnformatted);

        public static void Text(string text) => SdlSharp.Native.StringToUtf8Action(text, ptr => Native.ImGui_Text(ptr, __arglist()));

        public static void TextColored(Color color, string text) => SdlSharp.Native.StringToUtf8Action(text, ptr => Native.ImGui_TextColored(color.ToNative(), ptr, __arglist()));

        public static void TextDisabled(string text) => SdlSharp.Native.StringToUtf8Action(text, ptr => Native.ImGui_TextDisabled(ptr, __arglist()));

        public static void TextWrapped(string text) => SdlSharp.Native.StringToUtf8Action(text, ptr => Native.ImGui_TextWrapped(ptr, __arglist()));

        public static void LabelText(string label, string text) => SdlSharp.Native.StringToUtf8Action(label, text, (labelPtr, textPtr) => Native.ImGui_LabelText(labelPtr, textPtr, __arglist()));

        public static void BulletText(string text) => SdlSharp.Native.StringToUtf8Action(text, ptr => Native.ImGui_BulletText(ptr, __arglist()));

        public static bool Button(string label) => SdlSharp.Native.StringToUtf8Func(label, Native.ImGui_Button);

        public static bool Button(string label, Size size) => SdlSharp.Native.StringToUtf8Func(label, ptr => Native.ImGui_ButtonEx(ptr, size.ToNative()));

        public static bool SmallButton(string label) => SdlSharp.Native.StringToUtf8Func(label, Native.ImGui_SmallButton);

        public static bool InvisibleButton(string id, Size size, ButtonOptions options = default) => SdlSharp.Native.StringToUtf8Func(id, ptr => Native.ImGui_InvisibleButton(ptr, size.ToNative(), (Native.ImGuiButtonFlags)options));

        public static bool ArrowButton(string id, BindingDirection direction) => SdlSharp.Native.StringToUtf8Func(id, ptr => Native.ImGui_ArrowButton(ptr, (Native.ImGuiDir)direction));

        public static bool Checkbox(string label, State<bool> v) => SdlSharp.Native.StringToUtf8Func(label, ptr => Native.ImGui_Checkbox(ptr, v.ToNative()));

        public static bool CheckboxFlags(string label, State<int> flags, int flagsValue) => SdlSharp.Native.StringToUtf8Func(label, ptr => Native.ImGui_CheckboxFlagsIntPtr(ptr, flags.ToNative(), flagsValue));

        public static bool CheckboxFlags(string label, State<uint> flags, uint flagsValue) => SdlSharp.Native.StringToUtf8Func(label, ptr => Native.ImGui_CheckboxFlagsUintPtr(ptr, flags.ToNative(), flagsValue));

        public static bool RadioButton(string label, bool active) => SdlSharp.Native.StringToUtf8Func(label, ptr => Native.ImGui_RadioButton(ptr, active));

        public static bool RadioButton(string label, State<int> v, int vButton) => SdlSharp.Native.StringToUtf8Func(label, ptr => Native.ImGui_RadioButtonIntPtr(ptr, v.ToNative(), vButton));

        public static void ProgressBar(float fraction) => Native.ImGui_ProgressBar(fraction, new(float.MinValue, 0), null);

        public static void ProgressBar(float fraction, Size size) => Native.ImGui_ProgressBar(fraction, size.ToNative(), null);

        public static void ProgressBar(float fraction, Size size, string overlay) => SdlSharp.Native.StringToUtf8Action(overlay, ptr => Native.ImGui_ProgressBar(fraction, size.ToNative(), ptr));

        public static void Bullet() => Native.ImGui_Bullet();

        public static void Image(TextureId userTextureId, Size size) => Native.ImGui_Image(userTextureId.ToNative(), size.ToNative());

        public static void Image(TextureId userTextureId, Size size, TextureCoordinate uv0) => Image(userTextureId, size, uv0, new(1, 1));

        public static void Image(TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1) => Image(userTextureId, size, uv0, uv1, new(1, 1, 1, 1));

        public static void Image(TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1, Color tintColor) => Image(userTextureId, size, uv0, uv1, tintColor, default);

        public static void Image(TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1, Color tintColor, Color borderColor) => Native.ImGui_ImageEx(userTextureId.ToNative(), size.ToNative(), uv0.ToNative(), uv1.ToNative(), tintColor.ToNative(), borderColor.ToNative());

        public static bool ImageButton(string id, TextureId userTextureId, Size size) => SdlSharp.Native.StringToUtf8Func(id, ptr => Native.ImGui_ImageButton(ptr, userTextureId.ToNative(), size.ToNative()));

        public static bool ImageButton(string id, TextureId userTextureId, Size size, TextureCoordinate uv0) => ImageButton(id, userTextureId, size, uv0, new(1, 1));

        public static bool ImageButton(string id, TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1) => ImageButton(id, userTextureId, size, uv0, uv1, default);

        public static bool ImageButton(string id, TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1, Color backgroundColor) => ImageButton(id, userTextureId, size, uv0, uv1, backgroundColor, new(1, 1, 1, 1));

        public static bool ImageButton(string id, TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1, Color backgroundColor, Color tintColor) => SdlSharp.Native.StringToUtf8Func(id, ptr => Native.ImGui_ImageButtonEx(ptr, userTextureId.ToNative(), size.ToNative(), uv0.ToNative(), uv1.ToNative(), backgroundColor.ToNative(), tintColor.ToNative()));

        public static bool BeginCombo(string label, string previewValue, ComboOptions options = default) => SdlSharp.Native.StringToUtf8Func(label, previewValue, (labelPtr, previewValuePtr) => Native.ImGui_BeginCombo(labelPtr, previewValuePtr, (Native.ImGuiComboFlags)options));

        public static void EndCombo() => Native.ImGui_EndCombo();

        public static bool Combo(string label, State<int> currentItem, Span<string> items) => Native.ImGui_ComboChar();

        public static bool Combo(string label, State<int> currentItem, Span<string> items, int popupMaxHeightInItems = -1) => Native.ImGui_ComboCharEx();

        public static bool Combo(string label, State<int> currentItem, string itemsSeparatedByZeros) => Native.ImGui_Combo();

        public static bool Combo(string label, State<int> currentItem, string itemsSeparatedByZeros, int popupMaxHeightInItems = -1) => Native.ImGui_ComboEx();

        public static bool Combo(string label, State<int> currentItem, delegate* unmanaged[Cdecl]<void*, int, string*, bool> items_getter, void* data, int items_count) => Native.ImGui_ComboCallback();

        public static bool Combo(string label, State<int> currentItem, delegate* unmanaged[Cdecl]<void*, int, string*, bool> items_getter, void* data, int items_count, int popupMaxHeightInItems = -1) => Native.ImGui_ComboCallbackEx();

        public static bool DragFloat(byte* label, float* v) => Native.ImGui_DragFloat();

        public static bool DragFloatEx(byte* label, float* v, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default) => Native.ImGui_DragFloatEx();

        public static bool DragFloat2(byte* label, float* v) => Native.ImGui_DragFloat2();

        public static bool DragFloat2Ex(byte* label, float* v, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default) => Native.ImGui_DragFloat2Ex();

        public static bool DragFloat3(byte* label, float* v) => Native.ImGui_DragFloat3();

        public static bool DragFloat3Ex(byte* label, float* v, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default) => Native.ImGui_DragFloat3Ex();

        public static bool DragFloat4(byte* label, float* v) => Native.ImGui_DragFloat4();

        public static bool DragFloat4Ex(byte* label, float* v, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default) => Native.ImGui_DragFloat4Ex();

        public static bool DragFloatRange2(byte* label, float* v_current_min, float* v_current_max) => Native.ImGui_DragFloatRange2();

        public static bool DragFloatRange2Ex(byte* label, float* v_current_min, float* v_current_max, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, byte* format_max = default, ImGuiSliderFlags flags = default) => Native.ImGui_DragFloatRange2Ex();

        public static bool DragInt(byte* label, int* v) => Native.ImGui_DragInt();

        public static bool DragIntEx(byte* label, int* v, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default) => Native.ImGui_DragIntEx();

        public static bool DragInt2(byte* label, int* v) => Native.ImGui_DragInt2();

        public static bool DragInt2Ex(byte* label, int* v, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default) => Native.ImGui_DragInt2Ex();

        public static bool DragInt3(byte* label, int* v) => Native.ImGui_DragInt3();

        public static bool DragInt3Ex(byte* label, int* v, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default) => Native.ImGui_DragInt3Ex();

        public static bool DragInt4(byte* label, int* v) => Native.ImGui_DragInt4();

        public static bool DragInt4Ex(byte* label, int* v, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default) => Native.ImGui_DragInt4Ex();

        public static bool DragIntRange2(byte* label, int* v_current_min, int* v_current_max) => Native.ImGui_DragIntRange2();

        public static bool DragIntRange2Ex(byte* label, int* v_current_min, int* v_current_max, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, byte* format_max = default, ImGuiSliderFlags flags = default) => Native.ImGui_DragIntRange2Ex();

        public static bool DragScalar(byte* label, ImGuiDataType data_type, void* p_data) => Native.ImGui_DragScalar();

        public static bool DragScalarEx(byte* label, ImGuiDataType data_type, void* p_data, float v_speed = 1.0f, void* p_min = default, void* p_max = default, byte* format = default, ImGuiSliderFlags flags = default) => Native.ImGui_DragScalarEx();

        public static bool DragScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components) => Native.ImGui_DragScalarN();

        public static bool DragScalarNEx(byte* label, ImGuiDataType data_type, void* p_data, int components, float v_speed = 1.0f, void* p_min = default, void* p_max = default, byte* format = default, ImGuiSliderFlags flags = default) => Native.ImGui_DragScalarNEx();

        public static bool SliderFloat(byte* label, float* v, float v_min, float v_max) => Native.ImGui_SliderFloat();

        public static bool SliderFloatEx(byte* label, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default) => Native.ImGui_SliderFloatEx();

        public static bool SliderFloat2(byte* label, float* v, float v_min, float v_max) => Native.ImGui_SliderFloat2();

        public static bool SliderFloat2Ex(byte* label, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default) => Native.ImGui_SliderFloat2Ex();

        public static bool SliderFloat3(byte* label, float* v, float v_min, float v_max) => Native.ImGui_SliderFloat3();

        public static bool SliderFloat3Ex(byte* label, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default) => Native.ImGui_SliderFloat3Ex();

        public static bool SliderFloat4(byte* label, float* v, float v_min, float v_max) => Native.ImGui_SliderFloat4();

        public static bool SliderFloat4Ex(byte* label, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default) => Native.ImGui_SliderFloat4Ex();

        public static bool SliderAngle(byte* label, float* v_rad) => Native.ImGui_SliderAngle();

        public static bool SliderAngleEx(byte* label, float* v_rad, float v_degrees_min /* = -360.0f */, float v_degrees_max /* = +360.0f */, byte* format /* = "%.0f deg" */, ImGuiSliderFlags flags = default) => Native.ImGui_SliderAngleEx();

        public static bool SliderInt(byte* label, int* v, int v_min, int v_max) => Native.ImGui_SliderInt();

        public static bool SliderIntEx(byte* label, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default) => Native.ImGui_SliderIntEx();

        public static bool SliderInt2(byte* label, int* v, int v_min, int v_max) => Native.ImGui_SliderInt2();

        public static bool SliderInt2Ex(byte* label, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default) => Native.ImGui_SliderInt2Ex();

        public static bool SliderInt3(byte* label, int* v, int v_min, int v_max) => Native.ImGui_SliderInt3();

        public static bool SliderInt3Ex(byte* label, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default) => Native.ImGui_SliderInt3Ex();

        public static bool SliderInt4(byte* label, int* v, int v_min, int v_max) => Native.ImGui_SliderInt4();

        public static bool SliderInt4Ex(byte* label, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default) => Native.ImGui_SliderInt4Ex();

        public static bool SliderScalar(byte* label, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max) => Native.ImGui_SliderScalar();

        public static bool SliderScalarEx(byte* label, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max, byte* format = default, ImGuiSliderFlags flags = default) => Native.ImGui_SliderScalarEx();

        public static bool SliderScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components, void* p_min, void* p_max) => Native.ImGui_SliderScalarN();

        public static bool SliderScalarNEx(byte* label, ImGuiDataType data_type, void* p_data, int components, void* p_min, void* p_max, byte* format = default, ImGuiSliderFlags flags = default) => Native.ImGui_SliderScalarNEx();

        public static bool VSliderFloat(byte* label, ImVec2 size, float* v, float v_min, float v_max) => Native.ImGui_VSliderFloat();

        public static bool VSliderFloatEx(byte* label, ImVec2 size, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default) => Native.ImGui_VSliderFloatEx();

        public static bool VSliderInt(byte* label, ImVec2 size, int* v, int v_min, int v_max) => Native.ImGui_VSliderInt();

        public static bool VSliderIntEx(byte* label, ImVec2 size, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default) => Native.ImGui_VSliderIntEx();

        public static bool VSliderScalar(byte* label, ImVec2 size, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max) => Native.ImGui_VSliderScalar();

        public static bool VSliderScalarEx(byte* label, ImVec2 size, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max, byte* format = default, ImGuiSliderFlags flags = default) => Native.ImGui_VSliderScalarEx();

        public static bool InputText(byte* label, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default) => Native.ImGui_InputText();

        public static bool InputTextEx(byte* label, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default, delegate* unmanaged[Cdecl]<ImGuiInputTextCallbackData, int> callback = default, void* user_data = default) => Native.ImGui_InputTextEx();

        public static bool InputTextMultiline(byte* label, char* buf, nuint buf_size) => Native.ImGui_InputTextMultiline();

        public static bool InputTextMultilineEx(byte* label, char* buf, nuint buf_size, ImVec2 size = default, ImGuiInputTextFlags flags = default, delegate* unmanaged[Cdecl]<ImGuiInputTextCallbackData, int> callback = default, void* user_data = default) => Native.ImGui_InputTextMultilineEx();

        public static bool InputTextWithHint(byte* label, byte* hint, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default) => Native.ImGui_InputTextWithHint();

        public static bool InputTextWithHintEx(byte* label, byte* hint, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default, delegate* unmanaged[Cdecl]<ImGuiInputTextCallbackData, int> callback = default, void* user_data = default) => Native.ImGui_InputTextWithHintEx();

        public static bool InputFloat(byte* label, float* v) => Native.ImGui_InputFloat();

        public static bool InputFloatEx(byte* label, float* v, float step = default, float step_fast = default, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputFloatEx();

        public static bool InputFloat2(byte* label, float* v) => Native.ImGui_InputFloat2();

        public static bool InputFloat2Ex(byte* label, float* v, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputFloat2Ex();

        public static bool InputFloat3(byte* label, float* v) => Native.ImGui_InputFloat3();

        public static bool InputFloat3Ex(byte* label, float* v, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputFloat3Ex();

        public static bool InputFloat4(byte* label, float* v) => Native.ImGui_InputFloat4();

        public static bool InputFloat4Ex(byte* label, float* v, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputFloat4Ex();

        public static bool InputInt(byte* label, int* v) => Native.ImGui_InputInt();

        public static bool InputIntEx(byte* label, int* v, int step = 1, int step_fast = 100, ImGuiInputTextFlags flags = default) => Native.ImGui_InputIntEx();

        public static bool InputInt2(byte* label, int* v, ImGuiInputTextFlags flags = default) => Native.ImGui_InputInt2();

        public static bool InputInt3(byte* label, int* v, ImGuiInputTextFlags flags = default) => Native.ImGui_InputInt3();

        public static bool InputInt4(byte* label, int* v, ImGuiInputTextFlags flags = default) => Native.ImGui_InputInt4();

        public static bool InputDouble(byte* label, double* v) => Native.ImGui_InputDouble();

        public static bool InputDoubleEx(byte* label, double* v, double step /* = 0.0 */, double step_fast /* = 0.0 */, byte* format /* = "%.6f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputDoubleEx();

        public static bool InputScalar(byte* label, ImGuiDataType data_type, void* p_data) => Native.ImGui_InputScalar();

        public static bool InputScalarEx(byte* label, ImGuiDataType data_type, void* p_data, void* p_step = default, void* p_step_fast = default, byte* format = default, ImGuiInputTextFlags flags = default) => Native.ImGui_InputScalarEx();

        public static bool InputScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components) => Native.ImGui_InputScalarN();

        public static bool InputScalarNEx(byte* label, ImGuiDataType data_type, void* p_data, int components, void* p_step = default, void* p_step_fast = default, byte* format = default, ImGuiInputTextFlags flags = default) => Native.ImGui_InputScalarNEx();

        public static bool ColorEdit3(byte* label, float* col, ImGuiColorEditFlags flags = default) => Native.ImGui_ColorEdit3();

        public static bool ColorEdit4(byte* label, float* col, ImGuiColorEditFlags flags = default) => Native.ImGui_ColorEdit4();

        public static bool ColorPicker3(byte* label, float* col, ImGuiColorEditFlags flags = default) => Native.ImGui_ColorPicker3();

        public static bool ColorPicker4(byte* label, float* col, ImGuiColorEditFlags flags = default, float* ref_col = default) => Native.ImGui_ColorPicker4();

        public static bool ColorButton(byte* desc_id, ImVec4 col, ImGuiColorEditFlags flags = default) => Native.ImGui_ColorButton();

        public static bool ColorButtonEx(byte* desc_id, ImVec4 col, ImGuiColorEditFlags flags = default, ImVec2 size = default) => Native.ImGui_ColorButtonEx();

        public static void SetColorEditOptions(ImGuiColorEditFlags flags) => Native.ImGui_SetColorEditOptions();

        public static bool TreeNode(byte* label) => Native.ImGui_TreeNode();

        public static bool TreeNodeStr(byte* str_id, byte* fmt, __arglist) => Native.ImGui_TreeNodeStr();

        public static bool TreeNodePtr(void* ptr_id, byte* fmt, __arglist) => Native.ImGui_TreeNodePtr();

        public static bool TreeNodeV(byte* str_id, byte* fmt, nuint /* va_list */ args) => Native.ImGui_TreeNodeV();

        public static bool TreeNodeVPtr(void* ptr_id, byte* fmt, nuint /* va_list */ args) => Native.ImGui_TreeNodeVPtr();

        public static bool TreeNodeEx(byte* label, ImGuiTreeNodeFlags flags = default) => Native.ImGui_TreeNodeEx();

        public static bool TreeNodeExStr(byte* str_id, ImGuiTreeNodeFlags flags, byte* fmt, __arglist) => Native.ImGui_TreeNodeExStr();

        public static bool TreeNodeExPtr(void* ptr_id, ImGuiTreeNodeFlags flags, byte* fmt, __arglist) => Native.ImGui_TreeNodeExPtr();

        public static bool TreeNodeExV(byte* str_id, ImGuiTreeNodeFlags flags, byte* fmt, nuint /* va_list */ args) => Native.ImGui_TreeNodeExV();

        public static bool TreeNodeExVPtr(void* ptr_id, ImGuiTreeNodeFlags flags, byte* fmt, nuint /* va_list */ args) => Native.ImGui_TreeNodeExVPtr();

        public static void TreePush(byte* str_id) => Native.ImGui_TreePush();

        public static void TreePushPtr(void* ptr_id) => Native.ImGui_TreePushPtr();

        public static void TreePop() => Native.ImGui_TreePop();

        public static float GetTreeNodeToLabelSpacing() => Native.ImGui_GetTreeNodeToLabelSpacing();

        public static bool CollapsingHeader(byte* label, ImGuiTreeNodeFlags flags = default) => Native.ImGui_CollapsingHeader();

        public static bool CollapsingHeaderBoolPtr(byte* label, bool* p_visible, ImGuiTreeNodeFlags flags = default) => Native.ImGui_CollapsingHeaderBoolPtr();

        public static void SetNextItemOpen(bool is_open, ImGuiCond cond = default) => Native.ImGui_SetNextItemOpen();

        public static bool Selectable(byte* label) => Native.ImGui_Selectable();

        public static bool SelectableEx(byte* label, bool selected = false, ImGuiSelectableFlags flags = default, ImVec2 size = default) => Native.ImGui_SelectableEx();

        public static bool SelectableBoolPtr(byte* label, bool* p_selected, ImGuiSelectableFlags flags = default) => Native.ImGui_SelectableBoolPtr();

        public static bool SelectableBoolPtrEx(byte* label, bool* p_selected, ImGuiSelectableFlags flags = default, ImVec2 size = default) => Native.ImGui_SelectableBoolPtrEx();

        public static bool BeginListBox(byte* label, ImVec2 size = default) => Native.ImGui_BeginListBox();

        public static void EndListBox() => Native.ImGui_EndListBox();

        public static bool ListBox(byte* label, int* current_item, byte** items, int items_count, int height_in_items = -1) => Native.ImGui_ListBox();

        public static bool ListBoxCallback(byte* label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte**, bool> items_getter, void* data, int items_count) => Native.ImGui_ListBoxCallback();

        public static bool ListBoxCallbackEx(byte* label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte**, bool> items_getter, void* data, int items_count, int height_in_items = -1) => Native.ImGui_ListBoxCallbackEx();

        public static void PlotLines(byte* label, float* values, int values_count) => Native.ImGui_PlotLines();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotLinesEx(byte* label, float* values, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default, int stride = sizeof(float));

        public static void PlotLinesCallback(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count) => Native.ImGui_PlotLinesCallback();

        public static void PlotLinesCallbackEx(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default) => Native.ImGui_PlotLinesCallbackEx();

        public static void PlotHistogram(byte* label, float* values, int values_count) => Native.ImGui_PlotHistogram();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotHistogramEx(byte* label, float* values, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default, int stride = sizeof(float));

        public static void PlotHistogramCallback(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count) => Native.ImGui_PlotHistogramCallback();

        public static void PlotHistogramCallbackEx(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default) => Native.ImGui_PlotHistogramCallbackEx();

        public static bool BeginMenuBar() => Native.ImGui_BeginMenuBar();

        public static void EndMenuBar() => Native.ImGui_EndMenuBar();

        public static bool BeginMainMenuBar() => Native.ImGui_BeginMainMenuBar();

        public static void EndMainMenuBar() => Native.ImGui_EndMainMenuBar();

        public static bool BeginMenu(byte* label) => Native.ImGui_BeginMenu();

        public static bool BeginMenuEx(byte* label, bool enabled = true) => Native.ImGui_BeginMenuEx();

        public static void EndMenu() => Native.ImGui_EndMenu();

        public static bool MenuItem(byte* label) => Native.ImGui_MenuItem();

        public static bool MenuItemEx(byte* label, byte* shortcut = default, bool selected = false, bool enabled = true) => Native.ImGui_MenuItemEx();

        public static bool MenuItemBoolPtr(byte* label, byte* shortcut, bool* p_selected, bool enabled = true) => Native.ImGui_MenuItemBoolPtr();

        public static void BeginTooltip() => Native.ImGui_BeginTooltip();

        public static void EndTooltip() => Native.ImGui_EndTooltip();

        public static void SetTooltip(byte* fmt, __arglist) => Native.ImGui_SetTooltip();

        public static void SetTooltipV(byte* fmt, nuint /* va_list */ args) => Native.ImGui_SetTooltipV();
    }
}
