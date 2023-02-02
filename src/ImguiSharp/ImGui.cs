using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ImguiSharp
{
    public static unsafe class Imgui
    {
        private static Dictionary<nuint, Func<Position, Size, Size, Size>>? s_sizeCallbacks;
        private static Dictionary<nuint, Func<Position, Size, Size, Size>> SizeCallbacks => s_sizeCallbacks ??= new Dictionary<nuint, Func<Position, Size, Size, Size>>();

        #region Context creation and access

        public static Context CreateContext(FontAtlas? sharedFontAtlas = null) => Context.Wrap(Native.ImGui_CreateContext(sharedFontAtlas == null ? null : sharedFontAtlas.Value.ToNative()));

        public static void DestroyContext(Context? context = null) => Native.ImGui_DestroyContext(context == null ? null : context.Value.ToNative());

        public static Context? GetCurrentContext()
        {
            var context = Native.ImGui_GetCurrentContext();
            return context == null ? null : Context.Wrap(context);
        }

        public static void SetCurrentContext(Context? context) => Native.ImGui_SetCurrentContext(context == null ? null : context.Value.ToNative());

        #endregion

        #region Main

        public static Io GetIo() => Io.Wrap(Native.ImGui_GetIO());

        public static Style GetStyle() => Style.Wrap(Native.ImGui_GetStyle());

        public static void NewFrame()
        {
            Native.ImGui_NewFrame();
            s_sizeCallbacks?.Clear();
        }

        public static void EndFrame() => Native.ImGui_EndFrame();

        public static void Render() => Native.ImGui_Render();

        public static DrawData GetDrawData() => DrawData.Wrap(Native.ImGui_GetDrawData());

        #endregion

        #region Demo, Debug, Information

        public static void ShowDemoWindow(State<bool>? openState = null) => Native.ImGui_ShowDemoWindow(openState == null ? null : openState.ToNative());

        public static void ShowMetricsWindow(State<bool>? openState = null) => Native.ImGui_ShowMetricsWindow(openState == null ? null : openState.ToNative());

        public static void ShowDebugLogWindow(State<bool>? openState = null) => Native.ImGui_ShowDebugLogWindow(openState == null ? null : openState.ToNative());

        public static void ShowStackToolWindow(State<bool>? openState = null) => Native.ImGui_ShowStackToolWindow(openState == null ? null : openState.ToNative());

        public static void ShowAboutWindow(State<bool>? openState = null) => Native.ImGui_ShowAboutWindow(openState == null ? null : openState.ToNative());

        public static void ShowStyleEditor(Style? style = null) => Native.ImGui_ShowStyleEditor(style == null ? null : style.Value.ToNative());

        public static bool ShowStyleSelector(string label) => Native.StringToUtf8Func(label, Native.ImGui_ShowStyleSelector);

        public static void ShowFontSelector(string label) => Native.StringToUtf8Action(label, Native.ImGui_ShowFontSelector);

        public static void ShowUserGuide() => Native.ImGui_ShowUserGuide();

        public static string GetVersion() => Native.Utf8ToString(Native.ImGui_GetVersion())!;

        #endregion

        #region Styles

        public static void StyleColorsDark(Style? style = null) => Native.ImGui_StyleColorsDark(style == null ? null : style.Value.ToNative());

        public static void StyleColorsLight(Style? style = null) => Native.ImGui_StyleColorsLight(style == null ? null : style.Value.ToNative());

        public static void StyleColorsClassic(Style? style = null) => Native.ImGui_StyleColorsClassic(style == null ? null : style.Value.ToNative());

        #endregion

        #region Windows

        public static bool Begin(string name, State<bool>? openState = null, WindowOptions options = default) => Native.StringToUtf8Func(name, ptr => Native.ImGui_Begin(ptr, openState == null ? null : openState.ToNative(), (Native.ImGuiWindowFlags)options));

        public static void End() => Native.ImGui_End();

        #endregion

        #region Child Windows

        public static bool BeginChild(string name, Size? size = null, bool border = false, WindowOptions options = default) => Native.StringToUtf8Func(name, ptr => Native.ImGui_BeginChild(ptr, size == null ? default : size.Value.ToNative(), border, (Native.ImGuiWindowFlags)options));

        public static void BeginChildId(Id id, Size? size = null, bool border = false, WindowOptions options = default) =>
            Native.ImGui_BeginChildID(id.ToNative(), size == null ? default : size.Value.ToNative(), border, (Native.ImGuiWindowFlags)options);

        public static void EndChild() => Native.ImGui_EndChild();

        #endregion

        #region Windows Utilities

        public static bool IsWindowAppearing() => Native.ImGui_IsWindowAppearing();

        public static bool IsWindowCollapsed() => Native.ImGui_IsWindowCollapsed();

        public static bool IsWindowFocused(FocusedOptions options = default) => Native.ImGui_IsWindowFocused((Native.ImGuiFocusedFlags)options);

        public static bool IsWindowHovered(HoveredOptions options = default) => Native.ImGui_IsWindowHovered((Native.ImGuiHoveredFlags)options);

        public static DrawList GetWindowDrawList() => DrawList.Wrap(Native.ImGui_GetWindowDrawList());

        public static Position GetWindowPosition() => Position.Wrap(Native.ImGui_GetWindowPos());

        public static Size GetWindowSize() => Size.Wrap(Native.ImGui_GetWindowSize());

        public static float GetWindowWidth() => Native.ImGui_GetWindowWidth();

        public static float GetWindowHeight() => Native.ImGui_GetWindowHeight();

        #endregion

        #region Window Manipulation

        public static void SetNextWindowPosition(Position position, Condition condition = default) => Native.ImGui_SetNextWindowPos(position.ToNative(), (Native.ImGuiCond)condition);

        public static void SetNextWindowPosition(Position position, Condition condition = default, Position pivot = default) => Native.ImGui_SetNextWindowPosEx(position.ToNative(), (Native.ImGuiCond)condition, pivot.ToNative());

        public static void SetNextWindowSize(Size size, Condition condition = default) => Native.ImGui_SetNextWindowSize(size.ToNative(), (Native.ImGuiCond)condition);

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static void NativeSizeCallback(Native.ImGuiSizeCallbackData* data)
        {
            if (SizeCallbacks.TryGetValue((nuint)data->UserData, out var callback))
            {
                data->DesiredSize = callback(Position.Wrap(data->Pos), Size.Wrap(data->CurrentSize), Size.Wrap(data->DesiredSize)).ToNative();
            }
        }

        public static void SetNextWindowSizeConstraints(Size minimum, Size maximum, Func<Position, Size, Size, Size>? callback = null)
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

        public static void SetNamedWindowPosition(string name, Position position, Condition condition = default) => Native.StringToUtf8Action(name, ptr => Native.ImGui_SetWindowPosStr(ptr, position.ToNative(), (Native.ImGuiCond)condition));

        public static void SetNamedWindowSize(string name, Size size, Condition condition = default) => Native.StringToUtf8Action(name, ptr => Native.ImGui_SetWindowSizeStr(ptr, size.ToNative(), (Native.ImGuiCond)condition));

        public static void SetNamedWindowCollapsed(string name, bool collapsed, Condition condition = default) => Native.StringToUtf8Action(name, ptr => Native.ImGui_SetWindowCollapsedStr(ptr, collapsed, (Native.ImGuiCond)condition));

        public static void SetNamedWindowFocus(string name) => Native.StringToUtf8Action(name, Native.ImGui_SetWindowFocusStr);

        #endregion

        #region Content region

        public static Size GetContentRegionAvailable() => Size.Wrap(Native.ImGui_GetContentRegionAvail());

        public static Position GetContentRegionMax() => Position.Wrap(Native.ImGui_GetContentRegionMax());

        public static Position GetWindowContentRegionMin() => Position.Wrap(Native.ImGui_GetWindowContentRegionMin());

        public static Position GetWindowContentRegionMax() => Position.Wrap(Native.ImGui_GetWindowContentRegionMax());

        #endregion

        #region Windows Scrolling

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

        #endregion

        #region Parameters stacks (shared)

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

        #endregion

        #region Parameters stacks (current window)

        public static void PushItemWidth(float itemWidth) => Native.ImGui_PushItemWidth(itemWidth);

        public static void PopItemWidth() => Native.ImGui_PopItemWidth();

        public static void SetNextItemWidth(float itemWidth) => Native.ImGui_SetNextItemWidth(itemWidth);

        public static float CalcItemWidth() => Native.ImGui_CalcItemWidth();

        public static void PushTextWrapPosition(float wrapLocalPositionX = 0.0f) => Native.ImGui_PushTextWrapPos(wrapLocalPositionX);

        public static void PopTextWrapPosition() => Native.ImGui_PopTextWrapPos();

        #endregion

        #region Style read access

        public static Font GetFont() => Font.Wrap(Native.ImGui_GetFont());

        public static float GetFontSize() => Native.ImGui_GetFontSize();

        public static TextureCoordinate GetFontWhitePixelTextureCoordinate() => TextureCoordinate.Wrap(Native.ImGui_GetFontTexUvWhitePixel());

        public static uint GetColor(StyleColor color) => Native.ImGui_GetColorU32((Native.ImGuiCol)color);

        public static uint GetColor(StyleColor color, float alphaMul) => Native.ImGui_GetColorU32Ex((Native.ImGuiCol)color, alphaMul);

        public static uint GetColor(Color color) => Native.ImGui_GetColorU32ImVec4(color.ToNative());

        public static uint GetColor(uint color) => Native.ImGui_GetColorU32uint(color);

        public static Color GetStyleColor(StyleColor color) => Color.Wrap(*Native.ImGui_GetStyleColorVec4((Native.ImGuiCol)color));

        #endregion

        #region Cursor / Layout

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

        public static Position GetCursorPosition() => Position.Wrap(Native.ImGui_GetCursorPos());

        public static float GetCursorPosX() => Native.ImGui_GetCursorPosX();

        public static float GetCursorPosY() => Native.ImGui_GetCursorPosY();

        public static void SetCursorPosition(Position localPosition) => Native.ImGui_SetCursorPos(localPosition.ToNative());

        public static void SetCursorPosX(float localX) => Native.ImGui_SetCursorPosX(localX);

        public static void SetCursorPosY(float localY) => Native.ImGui_SetCursorPosY(localY);

        public static Position GetCursorStartPosition() => Position.Wrap(Native.ImGui_GetCursorStartPos());

        public static Position GetCursorScreenPosition() => Position.Wrap(Native.ImGui_GetCursorScreenPos());

        public static void SetCursorScreenPosition(Position position) => Native.ImGui_SetCursorScreenPos(position.ToNative());

        public static void AlignTextToFramePadding() => Native.ImGui_AlignTextToFramePadding();

        public static float GetTextLineHeight() => Native.ImGui_GetTextLineHeight();

        public static float GetTextLineHeightWithSpacing() => Native.ImGui_GetTextLineHeightWithSpacing();

        public static float GetFrameHeight() => Native.ImGui_GetFrameHeight();

        public static float GetFrameHeightWithSpacing() => Native.ImGui_GetFrameHeightWithSpacing();

        #endregion

        #region ID stack/scopes

        public static void PushID(string id) => Native.StringToUtf8Action(id, Native.ImGui_PushID);

        public static void PushID(nuint id) => Native.ImGui_PushIDPtr((void*)id);

        public static void PushID(int id) => Native.ImGui_PushIDInt(id);

        public static void PopID() => Native.ImGui_PopID();

        public static Id GetID(string id) => Native.StringToUtf8Func(id, ptr => Id.Wrap(Native.ImGui_GetID(ptr)));

        public static Id GetIDPtr(nuint id) => Id.Wrap(Native.ImGui_GetIDPtr((void*)id));

        #endregion

        #region Widgets: Text

        public static void TextUnformatted(string text) => Native.StringToUtf8Action(text, Native.ImGui_TextUnformatted);

        public static void Text(string text) => Native.StringToUtf8Action(text, ptr => Native.ImGui_Text(ptr, __arglist()));

        public static void TextColored(Color color, string text) => Native.StringToUtf8Action(text, ptr => Native.ImGui_TextColored(color.ToNative(), ptr, __arglist()));

        public static void TextDisabled(string text) => Native.StringToUtf8Action(text, ptr => Native.ImGui_TextDisabled(ptr, __arglist()));

        public static void TextWrapped(string text) => Native.StringToUtf8Action(text, ptr => Native.ImGui_TextWrapped(ptr, __arglist()));

        public static void LabelText(string label, string text) => Native.StringToUtf8Action(label, text, (labelPtr, textPtr) => Native.ImGui_LabelText(labelPtr, textPtr, __arglist()));

        public static void BulletText(string text) => Native.StringToUtf8Action(text, ptr => Native.ImGui_BulletText(ptr, __arglist()));

        #endregion

        #region Widgets: Main

        public static bool Button(string label) => Native.StringToUtf8Func(label, Native.ImGui_Button);

        public static bool Button(string label, Size size) => Native.StringToUtf8Func(label, ptr => Native.ImGui_ButtonEx(ptr, size.ToNative()));

        public static bool SmallButton(string label) => Native.StringToUtf8Func(label, Native.ImGui_SmallButton);

        public static bool InvisibleButton(string id, Size size, ButtonOptions options = default) => Native.StringToUtf8Func(id, ptr => Native.ImGui_InvisibleButton(ptr, size.ToNative(), (Native.ImGuiButtonFlags)options));

        public static bool ArrowButton(string id, BindingDirection direction) => Native.StringToUtf8Func(id, ptr => Native.ImGui_ArrowButton(ptr, (Native.ImGuiDir)direction));

        public static bool Checkbox(string label, State<bool> v) => Native.StringToUtf8Func(label, ptr => Native.ImGui_Checkbox(ptr, v.ToNative()));

        public static bool CheckboxFlags(string label, State<int> flags, int flagsValue) => Native.StringToUtf8Func(label, ptr => Native.ImGui_CheckboxFlagsIntPtr(ptr, flags.ToNative(), flagsValue));

        public static bool CheckboxFlags(string label, State<uint> flags, uint flagsValue) => Native.StringToUtf8Func(label, ptr => Native.ImGui_CheckboxFlagsUintPtr(ptr, flags.ToNative(), flagsValue));

        public static bool RadioButton(string label, bool active) => Native.StringToUtf8Func(label, ptr => Native.ImGui_RadioButton(ptr, active));

        public static bool RadioButton(string label, State<int> v, int vButton) => Native.StringToUtf8Func(label, ptr => Native.ImGui_RadioButtonIntPtr(ptr, v.ToNative(), vButton));

        public static void ProgressBar(float fraction) => Native.ImGui_ProgressBar(fraction, new(float.MinValue, 0), null);

        public static void ProgressBar(float fraction, Size size) => Native.ImGui_ProgressBar(fraction, size.ToNative(), null);

        public static void ProgressBar(float fraction, Size size, string overlay) => Native.StringToUtf8Action(overlay, ptr => Native.ImGui_ProgressBar(fraction, size.ToNative(), ptr));

        public static void Bullet() => Native.ImGui_Bullet();

        #endregion

        #region Widgets: Images

        public static void Image(TextureId userTextureId, Size size) => Native.ImGui_Image(userTextureId.ToNative(), size.ToNative());

        public static void Image(TextureId userTextureId, Size size, TextureCoordinate uv0) => Image(userTextureId, size, uv0, new(1, 1));

        public static void Image(TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1) => Image(userTextureId, size, uv0, uv1, new(1, 1, 1, 1));

        public static void Image(TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1, Color tintColor) => Image(userTextureId, size, uv0, uv1, tintColor, default);

        public static void Image(TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1, Color tintColor, Color borderColor) => Native.ImGui_ImageEx(userTextureId.ToNative(), size.ToNative(), uv0.ToNative(), uv1.ToNative(), tintColor.ToNative(), borderColor.ToNative());

        public static bool ImageButton(string id, TextureId userTextureId, Size size) => Native.StringToUtf8Func(id, ptr => Native.ImGui_ImageButton(ptr, userTextureId.ToNative(), size.ToNative()));

        public static bool ImageButton(string id, TextureId userTextureId, Size size, TextureCoordinate uv0) => ImageButton(id, userTextureId, size, uv0, new(1, 1));

        public static bool ImageButton(string id, TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1) => ImageButton(id, userTextureId, size, uv0, uv1, default);

        public static bool ImageButton(string id, TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1, Color backgroundColor) => ImageButton(id, userTextureId, size, uv0, uv1, backgroundColor, new(1, 1, 1, 1));

        public static bool ImageButton(string id, TextureId userTextureId, Size size, TextureCoordinate uv0, TextureCoordinate uv1, Color backgroundColor, Color tintColor) => Native.StringToUtf8Func(id, ptr => Native.ImGui_ImageButtonEx(ptr, userTextureId.ToNative(), size.ToNative(), uv0.ToNative(), uv1.ToNative(), backgroundColor.ToNative(), tintColor.ToNative()));

        #endregion

        #region Widgets: Combo Box (Dropdown)

        public static bool BeginCombo(string label, string previewValue, ComboOptions options = default) => Native.StringToUtf8Func(label, previewValue, (labelPtr, previewValuePtr) => Native.ImGui_BeginCombo(labelPtr, previewValuePtr, (Native.ImGuiComboFlags)options));

        public static void EndCombo() => Native.ImGui_EndCombo();

        #endregion

        #region Widgets: Drag Sliders

        public static bool Drag(string label, State<float> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragFloat(labelPtr, v.ToNative()));

        public static bool Drag(string label, State<float> v, float speed = 1.0f, float min = default, float max = default, string? format = "%.3f", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragFloatEx(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<float> v) => Native.StringToUtf8Func(label, labelPtr => v.Length switch
            {
                2 => Native.ImGui_DragFloat2(labelPtr, v.ToNative()),
                3 => Native.ImGui_DragFloat3(labelPtr, v.ToNative()),
                4 => Native.ImGui_DragFloat4(labelPtr, v.ToNative()),
                _ => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_Float, v.ToNative(), v.Length),
            });

        public static bool Drag(string label, StateVector<float> v, float speed = 1.0f, float min = default, float max = default, string? format = "%.3f", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => v.Length switch
            {
                2 => Native.ImGui_DragFloat2Ex(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                3 => Native.ImGui_DragFloat3Ex(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                4 => Native.ImGui_DragFloat4Ex(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                _ => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Float, v.ToNative(), v.Length, speed, (void*)BitConverter.SingleToUInt32Bits(min), (void*)BitConverter.SingleToUInt32Bits(max), formatPtr, (Native.ImGuiSliderFlags)options),
            });

        public static bool DragRange(string label, State<float> currentMin, State<float> currentMax) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragFloatRange2(labelPtr, currentMin.ToNative(), currentMax.ToNative()));

        public static bool DragRange(string label, State<float> currentMin, State<float> currentMax, float speed = 1.0f, float min = default, float max = default, string? format = "%.3f", string? formatMax = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, formatMax, (labelPtr, formatPtr, formatMaxPtr) => Native.ImGui_DragFloatRange2Ex(labelPtr, currentMin.ToNative(), currentMax.ToNative(), speed, min, max, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<int> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragInt(labelPtr, v.ToNative()));

        public static bool Drag(string label, State<int> v, float speed = 1.0f, int min = default, int max = default, string? format = "%d", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragIntEx(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<int> v) => Native.StringToUtf8Func(label, labelPtr => v.Length switch
        {
            2 => Native.ImGui_DragInt2(labelPtr, v.ToNative()),
            3 => Native.ImGui_DragInt3(labelPtr, v.ToNative()),
            4 => Native.ImGui_DragInt4(labelPtr, v.ToNative()),
            _ => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S32, v.ToNative(), v.Length),
        });

        public static bool Drag(string label, StateVector<int> v, float speed = 1.0f, int min = default, int max = default, string? format = "%d", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => v.Length switch
            {
                2 => Native.ImGui_DragInt2Ex(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                3 => Native.ImGui_DragInt3Ex(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                4 => Native.ImGui_DragInt4Ex(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                _ => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S32, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options),
            });

        public static bool DragRange(string label, State<int> currentMin, State<int> currentMax) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragIntRange2(labelPtr, currentMin.ToNative(), currentMax.ToNative()));

        public static bool DragRange(string label, State<int> currentMin, State<int> currentMax, float speed = 1.0f, int min = default, int max = default, string? format = "%d", string? formatMax = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, formatMax, (labelPtr, formatPtr, formatMaxPtr) => Native.ImGui_DragIntRange2Ex(labelPtr, currentMin.ToNative(), currentMax.ToNative(), speed, min, max, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<byte> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative()));

        public static bool Drag(string label, State<byte> v, float speed = 1.0f, byte min = default, byte max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<sbyte> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative()));

        public static bool Drag(string label, State<sbyte> v, float speed = 1.0f, sbyte min = default, sbyte max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<ushort> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative()));

        public static bool Drag(string label, State<ushort> v, float speed = 1.0f, ushort min = default, ushort max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<short> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative()));

        public static bool Drag(string label, State<short> v, float speed = 1.0f, short min = default, short max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<uint> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative()));

        public static bool Drag(string label, State<uint> v, float speed = 1.0f, uint min = default, uint max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<ulong> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative()));

        public static bool Drag(string label, State<ulong> v, float speed = 1.0f, ulong min = default, ulong max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<long> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative()));

        public static bool Drag(string label, State<long> v, float speed = 1.0f, long min = default, long max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<double> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative()));

        public static bool Drag(string label, State<double> v, float speed = 1.0f, double min = default, double max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), speed, (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<byte> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<byte> v, float speed = 1.0f, byte min = default, byte max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<sbyte> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<sbyte> v, float speed = 1.0f, sbyte min = default, sbyte max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<ushort> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<ushort> v, float speed = 1.0f, ushort min = default, ushort max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<short> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<short> v, float speed = 1.0f, short min = default, short max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<uint> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<uint> v, float speed = 1.0f, uint min = default, uint max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<ulong> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<ulong> v, float speed = 1.0f, ulong min = default, ulong max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<long> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<long> v, float speed = 1.0f, long min = default, long max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<double> v) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<double> v, float speed = 1.0f, double min = default, double max = default, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), v.Length, speed, (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        #endregion

        #region Widgets: Regular Sliders

        public static bool Slider(string label, State<float> v, float min, float max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderFloat(labelPtr, v.ToNative(), min, max));

        public static bool Slider(string label, State<float> v, float min, float max, string? format = "%.3f", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderFloatEx(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<float> v, float min, float max) => Native.StringToUtf8Func(label, labelPtr => v.Length switch
        {
            2 => Native.ImGui_SliderFloat2(labelPtr, v.ToNative(), min, max),
            3 => Native.ImGui_SliderFloat3(labelPtr, v.ToNative(), min, max),
            4 => Native.ImGui_SliderFloat4(labelPtr, v.ToNative(), min, max),
            _ => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_Float, v.ToNative(), v.Length, (void*)BitConverter.SingleToUInt32Bits(min), (void*)BitConverter.SingleToUInt32Bits(max)),
        });

        public static bool Slider(string label, StateVector<float> v, float min, float max, string? format = "%.3f", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => v.Length switch
            {
                2 => Native.ImGui_SliderFloat2Ex(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                3 => Native.ImGui_SliderFloat3Ex(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                4 => Native.ImGui_SliderFloat4Ex(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                _ => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Float, v.ToNative(), v.Length, (void*)BitConverter.SingleToUInt32Bits(min), (void*)BitConverter.SingleToUInt32Bits(max), formatPtr, (Native.ImGuiSliderFlags)options),
            });

        public static bool SliderAngle(string label, State<float> radian) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderAngle(labelPtr, radian.ToNative()));

        public static bool SliderAngle(string label, State<float> radian, float degreesMin = -360.0f, float degreesMax = 360.0f, string? format = "%.3f deg", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderAngleEx(labelPtr, radian.ToNative(), degreesMin, degreesMax, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<int> v, int min, int max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderInt(labelPtr, v.ToNative(), min, max));

        public static bool Slider(string label, State<int> v, int min, int max, string? format = "%d", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderIntEx(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<int> v, int min, int max) => Native.StringToUtf8Func(label, labelPtr => v.Length switch
        {
            2 => Native.ImGui_SliderInt2(labelPtr, v.ToNative(), min, max),
            3 => Native.ImGui_SliderInt3(labelPtr, v.ToNative(), min, max),
            4 => Native.ImGui_SliderInt4(labelPtr, v.ToNative(), min, max),
            _ => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S32, v.ToNative(), v.Length, (void*)min, (void*)max),
        });

        public static bool Slider(string label, StateVector<int> v, int min, int max, string? format = "%d", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => v.Length switch
            {
                2 => Native.ImGui_SliderInt2Ex(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                3 => Native.ImGui_SliderInt3Ex(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                4 => Native.ImGui_SliderInt4Ex(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options),
                _ => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S32, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options),
            });

        public static bool Slider(string label, State<byte> v, byte min, byte max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<byte> v, byte min, byte max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<sbyte> v, sbyte min, sbyte max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<sbyte> v, sbyte min, sbyte max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<ushort> v, ushort min, ushort max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<ushort> v, ushort min, ushort max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<short> v, short min, short max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<short> v, short min, short max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<uint> v, uint min, uint max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<uint> v, uint min, uint max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<ulong> v, ulong min, ulong max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<ulong> v, ulong min, ulong max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<long> v, long min, long max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<long> v, long min, long max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<double> v, double min, double max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max)));

        public static bool Slider(string label, State<double> v, double min, double max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<byte> v, byte min, byte max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<byte> v, byte min, byte max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<sbyte> v, sbyte min, sbyte max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<sbyte> v, sbyte min, sbyte max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<ushort> v, ushort min, ushort max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<ushort> v, ushort min, ushort max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<short> v, short min, short max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<short> v, short min, short max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<uint> v, uint min, uint max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<uint> v, uint min, uint max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<ulong> v, ulong min, ulong max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<ulong> v, ulong min, ulong max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<long> v, long min, long max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<long> v, long min, long max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<double> v, double min, double max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), v.Length, (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max)));

        public static bool Slider(string label, StateVector<double> v, double min, double max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), v.Length, (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<float> v, float min, float max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderFloat(labelPtr, size.ToNative(), v.ToNative(), min, max));

        public static bool VerticalSlider(string label, Size size, State<float> v, float min, float max, string? format = "%.3f", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderFloatEx(labelPtr, size.ToNative(), v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<int> v, int min, int max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderInt(labelPtr, size.ToNative(), v.ToNative(), min, max));

        public static bool VerticalSlider(string label, Size size, State<int> v, int min, int max, string? format = "%d", SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderIntEx(labelPtr, size.ToNative(), v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<byte> v, byte min, byte max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, Size size, State<byte> v, byte min, byte max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<sbyte> v, sbyte min, sbyte max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, Size size, State<sbyte> v, sbyte min, sbyte max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<ushort> v, ushort min, ushort max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, Size size, State<ushort> v, ushort min, ushort max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<short> v, short min, short max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, Size size, State<short> v, short min, short max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<uint> v, uint min, uint max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, Size size, State<uint> v, uint min, uint max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<ulong> v, ulong min, ulong max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, Size size, State<ulong> v, ulong min, ulong max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<long> v, long min, long max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, Size size, State<long> v, long min, long max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, Size size, State<double> v, double min, double max) =>
            Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max)));

        public static bool VerticalSlider(string label, Size size, State<double> v, double min, double max, string? format = default, SliderOptions options = default) =>
            Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        #endregion

        #region * Widgets: Input with Keyboard

        //public static bool InputText(byte* label, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default) => Native.ImGui_InputText();

        //public static bool InputTextEx(byte* label, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default, delegate* unmanaged[Cdecl]<ImGuiInputTextCallbackData, int> callback = default, void* user_data = default) => Native.ImGui_InputTextEx();

        //public static bool InputTextMultiline(byte* label, char* buf, nuint buf_size) => Native.ImGui_InputTextMultiline();

        //public static bool InputTextMultilineEx(byte* label, char* buf, nuint buf_size, ImVec2 size = default, ImGuiInputTextFlags flags = default, delegate* unmanaged[Cdecl]<ImGuiInputTextCallbackData, int> callback = default, void* user_data = default) => Native.ImGui_InputTextMultilineEx();

        //public static bool InputTextWithHint(byte* label, byte* hint, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default) => Native.ImGui_InputTextWithHint();

        //public static bool InputTextWithHintEx(byte* label, byte* hint, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default, delegate* unmanaged[Cdecl]<ImGuiInputTextCallbackData, int> callback = default, void* user_data = default) => Native.ImGui_InputTextWithHintEx();

        //public static bool InputFloat(byte* label, float* v) => Native.ImGui_InputFloat();

        //public static bool InputFloatEx(byte* label, float* v, float step = default, float step_fast = default, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputFloatEx();

        //public static bool InputFloat2(byte* label, float* v) => Native.ImGui_InputFloat2();

        //public static bool InputFloat2Ex(byte* label, float* v, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputFloat2Ex();

        //public static bool InputFloat3(byte* label, float* v) => Native.ImGui_InputFloat3();

        //public static bool InputFloat3Ex(byte* label, float* v, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputFloat3Ex();

        //public static bool InputFloat4(byte* label, float* v) => Native.ImGui_InputFloat4();

        //public static bool InputFloat4Ex(byte* label, float* v, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputFloat4Ex();

        //public static bool InputInt(byte* label, int* v) => Native.ImGui_InputInt();

        //public static bool InputIntEx(byte* label, int* v, int step = 1, int step_fast = 100, ImGuiInputTextFlags flags = default) => Native.ImGui_InputIntEx();

        //public static bool InputInt2(byte* label, int* v, ImGuiInputTextFlags flags = default) => Native.ImGui_InputInt2();

        //public static bool InputInt3(byte* label, int* v, ImGuiInputTextFlags flags = default) => Native.ImGui_InputInt3();

        //public static bool InputInt4(byte* label, int* v, ImGuiInputTextFlags flags = default) => Native.ImGui_InputInt4();

        //public static bool InputDouble(byte* label, double* v) => Native.ImGui_InputDouble();

        //public static bool InputDoubleEx(byte* label, double* v, double step /* = 0.0 */, double step_fast /* = 0.0 */, byte* format /* = "%.6f" */, ImGuiInputTextFlags flags = default) => Native.ImGui_InputDoubleEx();

        //public static bool InputScalar(byte* label, ImGuiDataType data_type, void* p_data) => Native.ImGui_InputScalar();

        //public static bool InputScalarEx(byte* label, ImGuiDataType data_type, void* p_data, void* p_step = default, void* p_step_fast = default, byte* format = default, ImGuiInputTextFlags flags = default) => Native.ImGui_InputScalarEx();

        //public static bool InputScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components) => Native.ImGui_InputScalarN();

        //public static bool InputScalarNEx(byte* label, ImGuiDataType data_type, void* p_data, int components, void* p_step = default, void* p_step_fast = default, byte* format = default, ImGuiInputTextFlags flags = default) => Native.ImGui_InputScalarNEx();

        #endregion

        #region * Widgets: Color Editor/Picker

        //public static bool ColorEdit3(byte* label, float* col, ImGuiColorEditFlags flags = default) => Native.ImGui_ColorEdit3();

        //public static bool ColorEdit4(byte* label, float* col, ImGuiColorEditFlags flags = default) => Native.ImGui_ColorEdit4();

        //public static bool ColorPicker3(byte* label, float* col, ImGuiColorEditFlags flags = default) => Native.ImGui_ColorPicker3();

        //public static bool ColorPicker4(byte* label, float* col, ImGuiColorEditFlags flags = default, float* ref_col = default) => Native.ImGui_ColorPicker4();

        //public static bool ColorButton(byte* desc_id, ImVec4 col, ImGuiColorEditFlags flags = default) => Native.ImGui_ColorButton();

        //public static bool ColorButtonEx(byte* desc_id, ImVec4 col, ImGuiColorEditFlags flags = default, ImVec2 size = default) => Native.ImGui_ColorButtonEx();

        //public static void SetColorEditOptions(ImGuiColorEditFlags flags) => Native.ImGui_SetColorEditOptions();

        #endregion

        #region * Widgets: Trees

        //public static bool TreeNode(byte* label) => Native.ImGui_TreeNode();

        //public static bool TreeNodeStr(byte* str_id, byte* fmt, __arglist) => Native.ImGui_TreeNodeStr();

        //public static bool TreeNodePtr(void* ptr_id, byte* fmt, __arglist) => Native.ImGui_TreeNodePtr();

        //public static bool TreeNodeV(byte* str_id, byte* fmt, nuint /* va_list */ args) => Native.ImGui_TreeNodeV();

        //public static bool TreeNodeVPtr(void* ptr_id, byte* fmt, nuint /* va_list */ args) => Native.ImGui_TreeNodeVPtr();

        //public static bool TreeNodeEx(byte* label, ImGuiTreeNodeFlags flags = default) => Native.ImGui_TreeNodeEx();

        //public static bool TreeNodeExStr(byte* str_id, ImGuiTreeNodeFlags flags, byte* fmt, __arglist) => Native.ImGui_TreeNodeExStr();

        //public static bool TreeNodeExPtr(void* ptr_id, ImGuiTreeNodeFlags flags, byte* fmt, __arglist) => Native.ImGui_TreeNodeExPtr();

        //public static bool TreeNodeExV(byte* str_id, ImGuiTreeNodeFlags flags, byte* fmt, nuint /* va_list */ args) => Native.ImGui_TreeNodeExV();

        //public static bool TreeNodeExVPtr(void* ptr_id, ImGuiTreeNodeFlags flags, byte* fmt, nuint /* va_list */ args) => Native.ImGui_TreeNodeExVPtr();

        //public static void TreePush(byte* str_id) => Native.ImGui_TreePush();

        //public static void TreePushPtr(void* ptr_id) => Native.ImGui_TreePushPtr();

        //public static void TreePop() => Native.ImGui_TreePop();

        //public static float GetTreeNodeToLabelSpacing() => Native.ImGui_GetTreeNodeToLabelSpacing();

        //public static bool CollapsingHeader(byte* label, ImGuiTreeNodeFlags flags = default) => Native.ImGui_CollapsingHeader();

        //public static bool CollapsingHeaderBoolPtr(byte* label, bool* p_visible, ImGuiTreeNodeFlags flags = default) => Native.ImGui_CollapsingHeaderBoolPtr();

        //public static void SetNextItemOpen(bool is_open, ImGuiCond cond = default) => Native.ImGui_SetNextItemOpen();

        #endregion

        #region * Widgets: Selectables

        //public static bool Selectable(byte* label) => Native.ImGui_Selectable();

        //public static bool SelectableEx(byte* label, bool selected = false, ImGuiSelectableFlags flags = default, ImVec2 size = default) => Native.ImGui_SelectableEx();

        //public static bool SelectableBoolPtr(byte* label, bool* p_selected, ImGuiSelectableFlags flags = default) => Native.ImGui_SelectableBoolPtr();

        //public static bool SelectableBoolPtrEx(byte* label, bool* p_selected, ImGuiSelectableFlags flags = default, ImVec2 size = default) => Native.ImGui_SelectableBoolPtrEx();

        #endregion

        #region * Widgets: List Boxes

        //public static bool BeginListBox(byte* label, ImVec2 size = default) => Native.ImGui_BeginListBox();

        //public static void EndListBox() => Native.ImGui_EndListBox();

        //public static bool ListBox(byte* label, int* current_item, byte** items, int items_count, int height_in_items = -1) => Native.ImGui_ListBox();

        //public static bool ListBoxCallback(byte* label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte**, bool> items_getter, void* data, int items_count) => Native.ImGui_ListBoxCallback();

        //public static bool ListBoxCallbackEx(byte* label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte**, bool> items_getter, void* data, int items_count, int height_in_items = -1) => Native.ImGui_ListBoxCallbackEx();

        #endregion

        #region * Widgets: Data Plotting

        //public static void PlotLines(byte* label, float* values, int values_count) => Native.ImGui_PlotLines();

        //[DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        //public static void ImGui_PlotLinesEx(byte* label, float* values, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default, int stride = sizeof(float));

        //public static void PlotLinesCallback(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count) => Native.ImGui_PlotLinesCallback();

        //public static void PlotLinesCallbackEx(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default) => Native.ImGui_PlotLinesCallbackEx();

        //public static void PlotHistogram(byte* label, float* values, int values_count) => Native.ImGui_PlotHistogram();

        //[DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        //public static void ImGui_PlotHistogramEx(byte* label, float* values, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default, int stride = sizeof(float));

        //public static void PlotHistogramCallback(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count) => Native.ImGui_PlotHistogramCallback();

        //public static void PlotHistogramCallbackEx(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default) => Native.ImGui_PlotHistogramCallbackEx();

        #endregion

        #region * Widgets: Menus

        //public static bool BeginMenuBar() => Native.ImGui_BeginMenuBar();

        //public static void EndMenuBar() => Native.ImGui_EndMenuBar();

        //public static bool BeginMainMenuBar() => Native.ImGui_BeginMainMenuBar();

        //public static void EndMainMenuBar() => Native.ImGui_EndMainMenuBar();

        //public static bool BeginMenu(byte* label) => Native.ImGui_BeginMenu();

        //public static bool BeginMenuEx(byte* label, bool enabled = true) => Native.ImGui_BeginMenuEx();

        //public static void EndMenu() => Native.ImGui_EndMenu();

        //public static bool MenuItem(byte* label) => Native.ImGui_MenuItem();

        //public static bool MenuItemEx(byte* label, byte* shortcut = default, bool selected = false, bool enabled = true) => Native.ImGui_MenuItemEx();

        //public static bool MenuItemBoolPtr(byte* label, byte* shortcut, bool* p_selected, bool enabled = true) => Native.ImGui_MenuItemBoolPtr();

        #endregion

        #region * Tooltips

        //public static void BeginTooltip() => Native.ImGui_BeginTooltip();

        //public static void EndTooltip() => Native.ImGui_EndTooltip();

        //public static void SetTooltip(byte* fmt, __arglist) => Native.ImGui_SetTooltip();

        //public static void SetTooltipV(byte* fmt, nuint /* va_list */ args) => Native.ImGui_SetTooltipV();

        #endregion

        #region * Popups: begin/end functions

        //public static bool ImGui_BeginPopup(byte* str_id, ImGuiWindowFlags flags = default);

        //public static bool ImGui_BeginPopupModal(byte* name, bool* p_open = default, ImGuiWindowFlags flags = default);

        //public static void ImGui_EndPopup();

        #endregion

        #region * Popups: open/close functions

        //public static void ImGui_OpenPopup(byte* str_id, ImGuiPopupFlags popup_flags = default);

        //public static void ImGui_OpenPopupID(ImGuiID id, ImGuiPopupFlags popup_flags = default);

        //public static void ImGui_OpenPopupOnItemClick(byte* str_id = default, ImGuiPopupFlags popup_flags = (ImGuiPopupFlags)1);

        //public static void ImGui_CloseCurrentPopup();

        #endregion

        #region * Popups: open+begin combined functions helpers

        //public static bool ImGui_BeginPopupContextItem();

        //public static bool ImGui_BeginPopupContextItemEx(byte* str_id = default, ImGuiPopupFlags popup_flags = (ImGuiPopupFlags)1);

        //public static bool ImGui_BeginPopupContextWindow();

        //public static bool ImGui_BeginPopupContextWindowEx(byte* str_id = default, ImGuiPopupFlags popup_flags = (ImGuiPopupFlags)1);

        //public static bool ImGui_BeginPopupContextVoid();

        //public static bool ImGui_BeginPopupContextVoidEx(byte* str_id = default, ImGuiPopupFlags popup_flags = (ImGuiPopupFlags)1);

        #endregion

        #region * Popups: query functions

        //public static bool ImGui_IsPopupOpen(byte* str_id, ImGuiPopupFlags flags = default);

        #endregion

        #region * Tables

        //public static bool ImGui_BeginTable(byte* str_id, int column, ImGuiTableFlags flags = default);

        //public static bool ImGui_BeginTableEx(byte* str_id, int column, ImGuiTableFlags flags = default, ImVec2 outer_size = default, float inner_width = default);

        //public static void ImGui_EndTable();

        //public static void ImGui_TableNextRow();

        //public static void ImGui_TableNextRowEx(ImGuiTableRowFlags row_flags = default, float min_row_height = default);

        //public static bool ImGui_TableNextColumn();

        //public static bool ImGui_TableSetColumnIndex(int column_n);

        #endregion

        #region * Tables: Headers & Columns declaration

        //public static void ImGui_TableSetupColumn(byte* label, ImGuiTableColumnFlags flags = default);

        //public static void ImGui_TableSetupColumnEx(byte* label, ImGuiTableColumnFlags flags = default, float init_width_or_weight = default, ImGuiID user_id = default);

        //public static void ImGui_TableSetupScrollFreeze(int cols, int rows);

        //public static void ImGui_TableHeadersRow();

        //public static void ImGui_TableHeader(byte* label);

        #endregion

        #region * Tables: Sorting & Miscellaneous functions

        //public static ImGuiTableSortSpecs* ImGui_TableGetSortSpecs();

        //public static int ImGui_TableGetColumnCount();

        //public static int ImGui_TableGetColumnIndex();

        //public static int ImGui_TableGetRowIndex();

        //public static byte* ImGui_TableGetColumnName(int column_n = -1);

        //public static ImGuiTableColumnFlags ImGui_TableGetColumnFlags(int column_n = -1);

        //public static void ImGui_TableSetColumnEnabled(int column_n, bool v);

        //public static void ImGui_TableSetBgColor(ImGuiTableBgTarget target, uint color, int column_n = -1);

        #endregion

        #region * Legacy Columns API (prefer using Tables!)

        //public static void ImGui_Columns();

        //public static void ImGui_ColumnsEx(int count = 1, byte* id = default, bool border = true);

        //public static void ImGui_NextColumn();

        //public static int ImGui_GetColumnIndex();

        //public static float ImGui_GetColumnWidth(int column_index = -1);

        //public static void ImGui_SetColumnWidth(int column_index, float width);

        //public static float ImGui_GetColumnOffset(int column_index = -1);

        //public static void ImGui_SetColumnOffset(int column_index, float offset_x);

        //public static int ImGui_GetColumnsCount();

        #endregion

        #region * Tab Bars, Tabs

        //public static bool ImGui_BeginTabBar(byte* str_id, ImGuiTabBarFlags flags = default);

        //public static void ImGui_EndTabBar();

        //public static bool ImGui_BeginTabItem(byte* label, bool* p_open = default, ImGuiTabItemFlags flags = default);

        //public static void ImGui_EndTabItem();

        //public static bool ImGui_TabItemButton(byte* label, ImGuiTabItemFlags flags = default);

        //public static void ImGui_SetTabItemClosed(byte* tab_or_docked_window_label);

        #endregion

        #region * Logging/Capture

        //public static void ImGui_LogToTTY(int auto_open_depth = -1);

        //public static void ImGui_LogToFile(int auto_open_depth = -1, byte* filename = default);

        //public static void ImGui_LogToClipboard(int auto_open_depth = -1);

        //public static void ImGui_LogFinish();

        //public static void ImGui_LogButtons();

        //public static void ImGui_LogText(byte* fmt, __arglist);

        //public static void ImGui_LogTextV(byte* fmt, nuint /* va_list */ args);

        #endregion

        #region Drag and Drop

        public static bool BeginDragDropSource(DragDropOptions options = default) => Native.ImGui_BeginDragDropSource((Native.ImGuiDragDropFlags)options);

        public static bool SetDragDropPayload(string type, Span<byte> data, Condition cond = default)
        {
            fixed (byte* typePtr = Native.StringToUtf8(type))
            fixed (byte* dataPtr = data)
            {
                return Native.ImGui_SetDragDropPayload(typePtr, dataPtr, (nuint)data.Length, (Native.ImGuiCond)cond);
            }
        }

        public static void EndDragDropSource() => Native.ImGui_EndDragDropSource();

        public static bool BeginDragDropTarget() => Native.ImGui_BeginDragDropTarget();

        public static Payload? AcceptDragDropPayload(string type, DragDropOptions options = default) =>
            Native.StringToUtf8Func<Payload?>(type, typePtr =>
            {
                var payload = Native.ImGui_AcceptDragDropPayload(typePtr, (Native.ImGuiDragDropFlags)options);
                return payload == null ? null : Payload.Wrap(payload);
            });

        public static void EndDragDropTarget() => Native.ImGui_EndDragDropTarget();

        public static Payload? GetDragDropPayload()
        {
            var payload = Native.ImGui_GetDragDropPayload();
            return payload == null ? null : Payload.Wrap(payload);
        }

        #endregion

        #region * Disabling [BETA API]

        //public static void ImGui_BeginDisabled(bool disabled = true);

        //public static void ImGui_EndDisabled();

        #endregion

        #region * Clipping

        //public static void ImGui_PushClipRect(ImVec2 clip_rect_min, ImVec2 clip_rect_max, bool intersect_with_current_clip_rect);

        //public static void ImGui_PopClipRect();

        #endregion

        #region * Focus, Activation

        //public static void ImGui_SetItemDefaultFocus();

        //public static void ImGui_SetKeyboardFocusHere();

        //public static void ImGui_SetKeyboardFocusHereEx(int offset = default);

        #endregion

        #region * Item/Widgets Utilities and Query Functions

        //public static bool ImGui_IsItemHovered(ImGuiHoveredFlags flags = default);

        //public static bool ImGui_IsItemActive();

        //public static bool ImGui_IsItemFocused();

        //public static bool ImGui_IsItemClicked();

        //public static bool ImGui_IsItemClickedEx(ImGuiMouseButton mouse_button = default);

        //public static bool ImGui_IsItemVisible();

        //public static bool ImGui_IsItemEdited();

        //public static bool ImGui_IsItemActivated();

        //public static bool ImGui_IsItemDeactivated();

        //public static bool ImGui_IsItemDeactivatedAfterEdit();

        //public static bool ImGui_IsItemToggledOpen();

        //public static bool ImGui_IsAnyItemHovered();

        //public static bool ImGui_IsAnyItemActive();

        //public static bool ImGui_IsAnyItemFocused();

        //public static ImGuiID ImGui_GetItemID();

        //public static ImVec2 ImGui_GetItemRectMin();

        //public static ImVec2 ImGui_GetItemRectMax();

        //public static ImVec2 ImGui_GetItemRectSize();

        //public static void ImGui_SetItemAllowOverlap();

        #endregion

        #region Viewports

        public static Viewport GetMainViewport() => Viewport.Wrap(Native.ImGui_GetMainViewport());

        #endregion

        #region * Background/Foreground Draw Lists

        //public static ImDrawList* ImGui_GetBackgroundDrawList();

        //public static ImDrawList* ImGui_GetForegroundDrawList();

        #endregion

        #region Miscellaneous Utilities

        public static bool IsRectVisibleBySize(Size size) => Native.ImGui_IsRectVisibleBySize(size.ToNative());

        public static bool IsRectVisible(Position rectMinimum, Position rectMaximum) => Native.ImGui_IsRectVisible(rectMinimum.ToNative(), rectMaximum.ToNative());

        public static double GetTime() => Native.ImGui_GetTime();

        public static int GetFrameCount() => Native.ImGui_GetFrameCount();

        public static string GetStyleColorName(StyleColor color) => Native.Utf8ToString(Native.ImGui_GetStyleColorName((Native.ImGuiCol)color))!;

        public static void SetStateStorage(Storage storage) => Native.ImGui_SetStateStorage(storage.ToNative());

        public static Storage GetStateStorage() => Storage.Wrap(Native.ImGui_GetStateStorage());

        public static bool BeginChildFrame(Id id, Size size, WindowOptions flags = default) => Native.ImGui_BeginChildFrame(id.ToNative(), size.ToNative(), (Native.ImGuiWindowFlags)flags);

        public static void EndChildFrame() => Native.ImGui_EndChildFrame();

        #endregion

        #region * Text Utilities

        //public static ImVec2 ImGui_CalcTextSize(byte* text);

        //public static ImVec2 ImGui_CalcTextSizeEx(byte* text, byte* text_end = default, bool hide_text_after_double_hash = false, float wrap_width = -1.0f);

        #endregion

        #region Color Utilities

        public static Color ColorConvert(uint i) => Color.Wrap(Native.ImGui_ColorConvertU32ToFloat4(i));

        public static uint ColorConvert(Color c) => Native.ImGui_ColorConvertFloat4ToU32(c.ToNative());

        public static (float H, float S, float V) ColorConvertRgbToHsv(float r, float g, float b)
        {
            float h, s, v;
            Native.ImGui_ColorConvertRGBtoHSV(r, g, b, &h, &s, &v);
            return (h, s, v);
        }

        public static (float R, float G, float B) ColorConvertHsvToRgb(float h, float s, float v)
        {
            float r, g, b;
            Native.ImGui_ColorConvertHSVtoRGB(h, s, v, &r, &g, &b);
            return (r, g, b);
        }

        #endregion

        #region Inputs Utilities: Keyboard/Mouse/Gamepad

        public static bool IsKeyDown(Key key) => Native.ImGui_IsKeyDown((Native.ImGuiKey)key);

        public static bool IsKeyPressed(Key key) => Native.ImGui_IsKeyPressed((Native.ImGuiKey)key);

        public static bool IsKeyPressed(Key key, bool repeat) => Native.ImGui_IsKeyPressedEx((Native.ImGuiKey)key, repeat);

        public static bool IsKeyReleased(Key key) => Native.ImGui_IsKeyReleased((Native.ImGuiKey)key);

        public static int GetKeyPressedAmount(Key key, float repeatDelay, float rate) => Native.ImGui_GetKeyPressedAmount((Native.ImGuiKey)key, repeatDelay, rate);

        public static string? GetKeyName(Key key) => Native.Utf8ToString(Native.ImGui_GetKeyName((Native.ImGuiKey)key));

        public static void SetNextFrameWantCaptureKeyboard(bool wantCaptureKeyboard) => Native.ImGui_SetNextFrameWantCaptureKeyboard(wantCaptureKeyboard);

        #endregion

        #region Inputs Utilities: Mouse specific

        public static bool IsMouseDown(MouseButton button) => Native.ImGui_IsMouseDown((Native.ImGuiMouseButton)button);

        public static bool IsMouseClicked(MouseButton button) => Native.ImGui_IsMouseClicked((Native.ImGuiMouseButton)button);

        public static bool IsMouseClicked(MouseButton button, bool repeat) => Native.ImGui_IsMouseClickedEx((Native.ImGuiMouseButton)button, repeat);

        public static bool IsMouseReleased(MouseButton button) => Native.ImGui_IsMouseReleased((Native.ImGuiMouseButton)button);

        public static bool IsMouseDoubleClicked(MouseButton button) => Native.ImGui_IsMouseDoubleClicked((Native.ImGuiMouseButton)button);

        public static int GetMouseClickedCount(MouseButton button) => Native.ImGui_GetMouseClickedCount((Native.ImGuiMouseButton)button);

        public static bool IsMouseHovering(Position min, Position max) => Native.ImGui_IsMouseHoveringRect(min.ToNative(), max.ToNative());

        public static bool IsMouseHovering(Position min, Position max, bool clip) => Native.ImGui_IsMouseHoveringRectEx(min.ToNative(), max.ToNative(), clip);

        public static bool IsMousePosValid(Position? mousePosition = default)
        {
            if (mousePosition == null)
            {
                return Native.ImGui_IsMousePosValid();
            }
            else
            {
                var local = mousePosition.Value.ToNative();
                return Native.ImGui_IsMousePosValid(&local);
            }
        }

        public static bool IsAnyMouseDown() => Native.ImGui_IsAnyMouseDown();

        public static Position GetMousePosition() => Position.Wrap(Native.ImGui_GetMousePos());

        public static Position GetMousePositionOnOpeningCurrentPopup() => Position.Wrap(Native.ImGui_GetMousePosOnOpeningCurrentPopup());

        public static bool IsMouseDragging(MouseButton button, float lockThreshold = -1.0f) => Native.ImGui_IsMouseDragging((Native.ImGuiMouseButton)button, lockThreshold);

        public static Size GetMouseDragDelta(MouseButton button = default, float lockThreshold = -1.0f) => Size.Wrap(Native.ImGui_GetMouseDragDelta((Native.ImGuiMouseButton)button, lockThreshold));

        public static void ResetMouseDragDelta() => Native.ImGui_ResetMouseDragDelta();

        public static void ResetMouseDragDelta(MouseButton button) => Native.ImGui_ResetMouseDragDeltaEx((Native.ImGuiMouseButton)button);

        public static MouseCursor GetMouseCursor() => (MouseCursor)Native.ImGui_GetMouseCursor();

        public static void SetMouseCursor(MouseCursor cursorType) => Native.ImGui_SetMouseCursor((Native.ImGuiMouseCursor)cursorType);

        public static void SetNextFrameWantCaptureMouse(bool wantCaptureMouse) => Native.ImGui_SetNextFrameWantCaptureMouse(wantCaptureMouse);

        #endregion

        #region * Clipboard Utilities

        //public static byte* ImGui_GetClipboardText();

        //public static void ImGui_SetClipboardText(byte* text);

        #endregion

        #region * Settings/.Ini Utilities

        //public static void ImGui_LoadIniSettingsFromDisk(byte* ini_filename);

        //public static void ImGui_LoadIniSettingsFromMemory(byte* ini_data, nuint ini_size = default);

        //public static void ImGui_SaveIniSettingsToDisk(byte* ini_filename);

        //public static byte* ImGui_SaveIniSettingsToMemory(nuint* out_ini_size = default);

        #endregion

        #region * Debug Utilities

        //public static void ImGui_DebugTextEncoding(byte* text);

        //public static bool ImGui_DebugCheckVersionAndDataLayout(byte* version_str, nuint sz_io, nuint sz_style, nuint sz_vec2, nuint sz_vec4, nuint sz_drawvert, nuint sz_drawidx);

        #endregion

        #region * Memory Allocators

        //public static void ImGui_SetAllocatorFunctions(delegate* unmanaged[Cdecl]<nuint, void*, void*> alloc_func, delegate* unmanaged[Cdecl]<void*, void*, void> free_func, void* user_data = default);

        //public static void ImGui_GetAllocatorFunctions(delegate* unmanaged[Cdecl]<nuint, void*, void*>* p_alloc_func, delegate* unmanaged[Cdecl]<void*, void*, void>* p_free_func, void** p_user_data);

        //public static void* ImGui_MemAlloc(nuint size);

        //public static void ImGui_MemFree(void* ptr);

        #endregion

    }
}
