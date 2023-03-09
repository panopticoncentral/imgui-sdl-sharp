using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ImguiSharp
{
    public static unsafe class Imgui
    {
        private static Dictionary<nuint, Func<PositionF, SizeF, SizeF, SizeF>>? s_sizeCallbacks;
        private static Dictionary<nuint, Func<PositionF, SizeF, SizeF, SizeF>> SizeCallbacks => s_sizeCallbacks ??= new Dictionary<nuint, Func<PositionF, SizeF, SizeF, SizeF>>();

        private static Dictionary<nuint, Func<int, float>>? s_plotCallbacks;
        private static Dictionary<nuint, Func<int, float>> PlotCallbacks => s_plotCallbacks ??= new Dictionary<nuint, Func<int, float>>();

        private static Dictionary<nuint, (StateText Text, InputTextCallbacks Callbacks)>? s_inputTextCallbacks;
        private static Dictionary<nuint, (StateText Text, InputTextCallbacks Callbacks)> InputTextCallbacks => s_inputTextCallbacks ??= new Dictionary<nuint, (StateText Text, InputTextCallbacks Callbacks)>();

        private static Dictionary<nuint, Action<DrawList, DrawCommand>>? s_drawListCallbacks;
        internal static Dictionary<nuint, Action<DrawList, DrawCommand>> DrawListCallbacks => s_drawListCallbacks ??= new Dictionary<nuint, Action<DrawList, DrawCommand>>();

        #region Context creation and access

        public static Context CreateContext(FontAtlas? sharedFontAtlas = null) => Context.Wrap(Native.ImGui_CreateContext(sharedFontAtlas.ToNative()))!.Value;

        public static void DestroyContext(Context? context = null) => Native.ImGui_DestroyContext(context.ToNative());

        public static Context? GetCurrentContext() => Context.Wrap(Native.ImGui_GetCurrentContext());

        public static void SetCurrentContext(Context? context) => Native.ImGui_SetCurrentContext(context.ToNative());

        #endregion

        #region Main

        public static Io GetIo() => Io.Wrap(Native.ImGui_GetIO())!.Value;

        public static Style GetStyle() => Style.Wrap(Native.ImGui_GetStyle())!.Value;

        public static void NewFrame()
        {
            Native.ImGui_NewFrame();
            s_sizeCallbacks?.Clear();
            s_plotCallbacks?.Clear();
            s_inputTextCallbacks?.Clear();
            s_drawListCallbacks?.Clear();
        }

        public static void EndFrame() => Native.ImGui_EndFrame();

        public static void Render() => Native.ImGui_Render();

        public static DrawData? GetDrawData() => DrawData.Wrap(Native.ImGui_GetDrawData());

        #endregion

        #region Demo, Debug, Information

        public static void ShowDemoWindow(State<bool>? openState = null) => Native.ImGui_ShowDemoWindow(openState.ToNative());

        public static void ShowMetricsWindow(State<bool>? openState = null) => Native.ImGui_ShowMetricsWindow(openState.ToNative());

        public static void ShowDebugLogWindow(State<bool>? openState = null) => Native.ImGui_ShowDebugLogWindow(openState.ToNative());

        public static void ShowStackToolWindow(State<bool>? openState = null) => Native.ImGui_ShowStackToolWindow(openState.ToNative());

        public static void ShowAboutWindow(State<bool>? openState = null) => Native.ImGui_ShowAboutWindow(openState.ToNative());

        public static void ShowStyleEditor(Style? style = null) => Native.ImGui_ShowStyleEditor(style.ToNative());

        public static bool ShowStyleSelector(string label) => Native.StringToUtf8Func(label, Native.ImGui_ShowStyleSelector);

        public static void ShowFontSelector(string label) => Native.StringToUtf8Action(label, Native.ImGui_ShowFontSelector);

        public static void ShowUserGuide() => Native.ImGui_ShowUserGuide();

        public static string GetVersion() => Native.Utf8ToString(Native.ImGui_GetVersion())!;

        #endregion

        #region Styles

        public static void StyleColorsDark(Style? style = null) => Native.ImGui_StyleColorsDark(style.ToNative());

        public static void StyleColorsLight(Style? style = null) => Native.ImGui_StyleColorsLight(style.ToNative());

        public static void StyleColorsClassic(Style? style = null) => Native.ImGui_StyleColorsClassic(style.ToNative());

        #endregion

        #region Windows

        public static bool Begin(string name, State<bool>? openState = null, WindowOptions options = default) => Native.StringToUtf8Func(name, namePtr => Native.ImGui_Begin(namePtr, openState.ToNative(), (Native.ImGuiWindowFlags)options));

        public static void End() => Native.ImGui_End();

        #endregion

        #region Child Windows

        public static bool BeginChild(string name, SizeF size = default, bool border = false, WindowOptions options = default) => Native.StringToUtf8Func(name, namePtr => Native.ImGui_BeginChild(namePtr, size.ToNative(), border, (Native.ImGuiWindowFlags)options));

        public static void BeginChildId(Id id, SizeF size = default, bool border = false, WindowOptions options = default) => Native.ImGui_BeginChildID(id.ToNative(), size.ToNative(), border, (Native.ImGuiWindowFlags)options);

        public static void EndChild() => Native.ImGui_EndChild();

        #endregion

        #region Windows Utilities

        public static bool IsWindowAppearing() => Native.ImGui_IsWindowAppearing();

        public static bool IsWindowCollapsed() => Native.ImGui_IsWindowCollapsed();

        public static bool IsWindowFocused(FocusedOptions options = default) => Native.ImGui_IsWindowFocused((Native.ImGuiFocusedFlags)options);

        public static bool IsWindowHovered(HoveredOptions options = default) => Native.ImGui_IsWindowHovered((Native.ImGuiHoveredFlags)options);

        public static DrawList? GetWindowDrawList() => DrawList.Wrap(Native.ImGui_GetWindowDrawList());

        public static PositionF GetWindowPosition() => PositionF.Wrap(Native.ImGui_GetWindowPos());

        public static SizeF GetWindowSize() => SizeF.Wrap(Native.ImGui_GetWindowSize());

        public static float GetWindowWidth() => Native.ImGui_GetWindowWidth();

        public static float GetWindowHeight() => Native.ImGui_GetWindowHeight();

        #endregion

        #region Window Manipulation

        public static void SetNextWindowPosition(PositionF position, Condition condition = default) => Native.ImGui_SetNextWindowPos(position.ToNative(), (Native.ImGuiCond)condition);

        public static void SetNextWindowPosition(PositionF position, Condition condition = default, PositionF pivot = default) => Native.ImGui_SetNextWindowPosEx(position.ToNative(), (Native.ImGuiCond)condition, pivot.ToNative());

        public static void SetNextWindowSize(SizeF size, Condition condition = default) => Native.ImGui_SetNextWindowSize(size.ToNative(), (Native.ImGuiCond)condition);

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static void NativeSizeCallback(Native.ImGuiSizeCallbackData* data)
        {
            if (SizeCallbacks.TryGetValue((nuint)data->UserData, out var callback))
            {
                data->DesiredSize = callback(PositionF.Wrap(data->Pos), SizeF.Wrap(data->CurrentSize), SizeF.Wrap(data->DesiredSize)).ToNative();
            }
        }

        public static void SetNextWindowSizeConstraints(RectangleF rect) => Native.ImGui_SetNextWindowSizeConstraints(rect.Min.ToNative(), rect.Max.ToNative());

        public static void SetNextWindowSizeConstraints(RectangleF rect, Func<PositionF, SizeF, SizeF, SizeF> callback)
        {
            SizeCallbacks[(nuint)callback.GetHashCode()] = callback;
            Native.ImGui_SetNextWindowSizeConstraints(rect.Min.ToNative(), rect.Max.ToNative(), &NativeSizeCallback, (void*)callback.GetHashCode());
        }

        public static void SetNextWindowContentSize(SizeF size) => Native.ImGui_SetNextWindowContentSize(size.ToNative());

        public static void SetNextWindowCollapsed(bool collapsed, Condition condition = default) => Native.ImGui_SetNextWindowCollapsed(collapsed, (Native.ImGuiCond)condition);

        public static void SetNextWindowFocus() => Native.ImGui_SetNextWindowFocus();

        public static void SetNextWindowScroll(PositionF scroll) => Native.ImGui_SetNextWindowScroll(scroll.ToNative());

        public static void SetNextWindowBackgroundAlpha(float alpha) => Native.ImGui_SetNextWindowBgAlpha(alpha);

        public static void SetWindowPosition(PositionF position, Condition condition) => Native.ImGui_SetWindowPos(position.ToNative(), (Native.ImGuiCond)condition);

        public static void SetWindowSize(SizeF size, Condition condition) => Native.ImGui_SetWindowSize(size.ToNative(), (Native.ImGuiCond)condition);

        public static void SetWindowCollapsed(bool collapsed, Condition condition) => Native.ImGui_SetWindowCollapsed(collapsed, (Native.ImGuiCond)condition);

        public static void SetWindowFocus() => Native.ImGui_SetWindowFocus();

        public static void SetNamedWindowPosition(string name, PositionF position, Condition condition = default) => Native.StringToUtf8Action(name, namePtr => Native.ImGui_SetWindowPosStr(namePtr, position.ToNative(), (Native.ImGuiCond)condition));

        public static void SetNamedWindowSize(string name, SizeF size, Condition condition = default) => Native.StringToUtf8Action(name, namePtr => Native.ImGui_SetWindowSizeStr(namePtr, size.ToNative(), (Native.ImGuiCond)condition));

        public static void SetNamedWindowCollapsed(string name, bool collapsed, Condition condition = default) => Native.StringToUtf8Action(name, namePtr => Native.ImGui_SetWindowCollapsedStr(namePtr, collapsed, (Native.ImGuiCond)condition));

        public static void SetNamedWindowFocus(string name) => Native.StringToUtf8Action(name, Native.ImGui_SetWindowFocusStr);

        #endregion

        #region Content region

        public static SizeF GetContentRegionAvailable() => SizeF.Wrap(Native.ImGui_GetContentRegionAvail());

        public static PositionF GetContentRegionMax() => PositionF.Wrap(Native.ImGui_GetContentRegionMax());

        public static PositionF GetWindowContentRegionMin() => PositionF.Wrap(Native.ImGui_GetWindowContentRegionMin());

        public static PositionF GetWindowContentRegionMax() => PositionF.Wrap(Native.ImGui_GetWindowContentRegionMax());

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

        public static void PushStyleColor(StyleColor property, Color color) => Native.ImGui_PushStyleColor((Native.ImGuiCol)property, color.ToNative());

        public static void PushStyleColor(StyleColor property, ColorF color) => Native.ImGui_PushStyleColorImVec4((Native.ImGuiCol)property, color.ToNative());

        public static void PopStyleColor() => Native.ImGui_PopStyleColor();

        public static void PopStyleColor(int count) => Native.ImGui_PopStyleColorEx(count);

        public static void PushStyleVariable(StyleVariable variable, float value) => Native.ImGui_PushStyleVar((Native.ImGuiStyleVar)variable, value);

        public static void PushStyleVariable(StyleVariable variable, SizeF value) => Native.ImGui_PushStyleVarImVec2((Native.ImGuiStyleVar)variable, value.ToNative());

        public static void PushStyleVariable(StyleVariable variable, PositionF value) => Native.ImGui_PushStyleVarImVec2((Native.ImGuiStyleVar)variable, value.ToNative());

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

        public static Font? GetFont() => Font.Wrap(Native.ImGui_GetFont());

        public static float GetFontSize() => Native.ImGui_GetFontSize();

        public static TexturePosition GetFontWhitePixelTextureCoordinate() => TexturePosition.Wrap(Native.ImGui_GetFontTexUvWhitePixel());

        public static Color GetColor(StyleColor color) => Color.Wrap(Native.ImGui_GetColorU32((Native.ImGuiCol)color));

        public static Color GetColor(StyleColor color, float alphaMul) => Color.Wrap(Native.ImGui_GetColorU32Ex((Native.ImGuiCol)color, alphaMul));

        public static Color GetColor(ColorF color) => Color.Wrap(Native.ImGui_GetColorU32ImVec4(color.ToNative()));

        public static Color GetColor(Color color) => Color.Wrap(Native.ImGui_GetColorU32uint(color.ToNative()));

        public static ColorF GetStyleColor(StyleColor color) => ColorF.Wrap(*Native.ImGui_GetStyleColorVec4((Native.ImGuiCol)color));

        #endregion

        #region Cursor / Layout

        public static void Separator() => Native.ImGui_Separator();

        public static void SameLine() => Native.ImGui_SameLine();

        public static void SameLine(float offsetFromStartX, float spacing = -1.0f) => Native.ImGui_SameLineEx(offsetFromStartX, spacing);

        public static void NewLine() => Native.ImGui_NewLine();

        public static void Spacing() => Native.ImGui_Spacing();

        public static void Dummy(SizeF size) => Native.ImGui_Dummy(size.ToNative());

        public static void Indent() => Native.ImGui_Indent();

        public static void Indent(float indent) => Native.ImGui_IndentEx(indent);

        public static void Unindent() => Native.ImGui_Unindent();

        public static void Unindent(float indent) => Native.ImGui_UnindentEx(indent);

        public static void BeginGroup() => Native.ImGui_BeginGroup();

        public static void EndGroup() => Native.ImGui_EndGroup();

        public static PositionF GetCursorPosition() => PositionF.Wrap(Native.ImGui_GetCursorPos());

        public static float GetCursorPosX() => Native.ImGui_GetCursorPosX();

        public static float GetCursorPosY() => Native.ImGui_GetCursorPosY();

        public static void SetCursorPosition(PositionF localPosition) => Native.ImGui_SetCursorPos(localPosition.ToNative());

        public static void SetCursorPosX(float localX) => Native.ImGui_SetCursorPosX(localX);

        public static void SetCursorPosY(float localY) => Native.ImGui_SetCursorPosY(localY);

        public static PositionF GetCursorStartPosition() => PositionF.Wrap(Native.ImGui_GetCursorStartPos());

        public static PositionF GetCursorScreenPosition() => PositionF.Wrap(Native.ImGui_GetCursorScreenPos());

        public static void SetCursorScreenPosition(PositionF position) => Native.ImGui_SetCursorScreenPos(position.ToNative());

        public static void AlignTextToFramePadding() => Native.ImGui_AlignTextToFramePadding();

        public static float GetTextLineHeight() => Native.ImGui_GetTextLineHeight();

        public static float GetTextLineHeightWithSpacing() => Native.ImGui_GetTextLineHeightWithSpacing();

        public static float GetFrameHeight() => Native.ImGui_GetFrameHeight();

        public static float GetFrameHeightWithSpacing() => Native.ImGui_GetFrameHeightWithSpacing();

        #endregion

        #region ID stack/scopes

        public static void PushId(string id) => Native.StringToUtf8Action(id, Native.ImGui_PushID);

        public static void PushId(nuint id) => Native.ImGui_PushIDPtr((void*)id);

        public static void PushId(int id) => Native.ImGui_PushIDInt(id);

        public static void PopId() => Native.ImGui_PopID();

        public static Id GetId(string id) => Native.StringToUtf8Func(id, idPtr => Id.Wrap(Native.ImGui_GetID(idPtr)));

        public static Id GetId(nuint id) => Id.Wrap(Native.ImGui_GetIDPtr((void*)id));

        #endregion

        #region Widgets: Text

        public static void TextUnformatted(string text) => Native.StringToUtf8Action(text, Native.ImGui_TextUnformatted);

        public static void Text(string text) => Native.StringToUtf8Action(text, Native.ImGui_Text);

        public static void TextColored(ColorF color, string text) => Native.StringToUtf8Action(text, ptr => Native.ImGui_TextColored(color.ToNative(), ptr));

        public static void TextDisabled(string text) => Native.StringToUtf8Action(text, Native.ImGui_TextDisabled);

        public static void TextWrapped(string text) => Native.StringToUtf8Action(text, Native.ImGui_TextWrapped);

        public static void LabelText(string label, string text) => Native.StringToUtf8Action(label, text, Native.ImGui_LabelText);

        public static void BulletText(string text) => Native.StringToUtf8Action(text, Native.ImGui_BulletText);

        #endregion

        #region Widgets: Main

        public static bool Button(string label) => Native.StringToUtf8Func(label, Native.ImGui_Button);

        public static bool Button(string label, SizeF size) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_ButtonEx(labelPtr, size.ToNative()));

        public static bool SmallButton(string label) => Native.StringToUtf8Func(label, Native.ImGui_SmallButton);

        public static bool InvisibleButton(string id, SizeF size, ButtonOptions options = default) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_InvisibleButton(idPtr, size.ToNative(), (Native.ImGuiButtonFlags)options));

        public static bool ArrowButton(string id, Direction direction) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_ArrowButton(idPtr, (Native.ImGuiDir)direction));

        public static bool Checkbox(string label, State<bool> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_Checkbox(labelPtr, v.ToNative()));

        public static bool CheckboxFlags<T>(string label, StateOption<T> flags, T flagsValue) where T : unmanaged, Enum => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_CheckboxFlagsIntPtr(labelPtr, flags.ToNative(), (int)(object)flagsValue));

        public static bool CheckboxFlags(string label, State<int> flags, int flagsValue) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_CheckboxFlagsIntPtr(labelPtr, flags.ToNative(), flagsValue));

        public static bool CheckboxFlags(string label, State<uint> flags, uint flagsValue) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_CheckboxFlagsUintPtr(labelPtr, flags.ToNative(), flagsValue));

        public static bool RadioButton(string label, bool active) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_RadioButton(labelPtr, active));

        public static bool RadioButton(string label, State<int> v, int vButton) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_RadioButtonIntPtr(labelPtr, v.ToNative(), vButton));

        public static void ProgressBar(float fraction) => Native.ImGui_ProgressBar(fraction, new(float.MinValue, 0), null);

        public static void ProgressBar(float fraction, SizeF size, string? overlay = null) => Native.StringToUtf8Action(overlay, overlayPtr => Native.ImGui_ProgressBar(fraction, size.ToNative(), overlayPtr));

        public static void Bullet() => Native.ImGui_Bullet();

        #endregion

        #region Widgets: Images

        public static void Image(TextureId userTextureId, SizeF size) => Native.ImGui_Image(userTextureId.ToNative(), size.ToNative());

        public static void Image(TextureId userTextureId, SizeF size, TexturePosition uv0) => Image(userTextureId, size, new TextureRectangle(uv0, new(1, 1)));

        public static void Image(TextureId userTextureId, SizeF size, TextureRectangle rect) => Image(userTextureId, size, rect, new(1, 1, 1, 1));

        public static void Image(TextureId userTextureId, SizeF size, TextureRectangle rect, ColorF tintColor) => Image(userTextureId, size, rect, tintColor, default);

        public static void Image(TextureId userTextureId, SizeF size, TextureRectangle rect, ColorF tintColor, ColorF borderColor) => Native.ImGui_ImageEx(userTextureId.ToNative(), size.ToNative(), rect.Min.ToNative(), rect.Max.ToNative(), tintColor.ToNative(), borderColor.ToNative());

        public static bool ImageButton(string id, TextureId userTextureId, SizeF size) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_ImageButton(idPtr, userTextureId.ToNative(), size.ToNative()));

        public static bool ImageButton(string id, TextureId userTextureId, SizeF size, TexturePosition uv0) => ImageButton(id, userTextureId, size, new TextureRectangle(uv0, new(1, 1)));

        public static bool ImageButton(string id, TextureId userTextureId, SizeF size, TextureRectangle rect) => ImageButton(id, userTextureId, size, rect, default);

        public static bool ImageButton(string id, TextureId userTextureId, SizeF size, TextureRectangle rect, ColorF backgroundColor) => ImageButton(id, userTextureId, size, rect, backgroundColor, new(1, 1, 1, 1));

        public static bool ImageButton(string id, TextureId userTextureId, SizeF size, TextureRectangle rect, ColorF backgroundColor, ColorF tintColor) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_ImageButtonEx(idPtr, userTextureId.ToNative(), size.ToNative(), rect.Min.ToNative(), rect.Max.ToNative(), backgroundColor.ToNative(), tintColor.ToNative()));

        #endregion

        #region Widgets: Combo Box (Dropdown)

        public static bool BeginCombo(string label, string previewValue, ComboOptions options = default) => Native.StringToUtf8Func(label, previewValue, (labelPtr, previewValuePtr) => Native.ImGui_BeginCombo(labelPtr, previewValuePtr, (Native.ImGuiComboFlags)options));

        public static void EndCombo() => Native.ImGui_EndCombo();

        public static bool Combo(string label, State<int> currentItem, IReadOnlyList<string> items, int popupMaxHeightInItems = -1)
        {
            fixed (byte* labelPtr = Native.StringToUtf8(label))
            {
                var itemsPtr = stackalloc byte*[items.Count];
                for (var index = 0; index < items.Count; index++)
                {
                    var bytes = Native.StringToUtf8(items[index]);
                    var buffer = (byte*)Native.ImGui_MemAlloc((nuint)bytes.Length);
                    bytes.CopyTo(new(buffer, bytes.Length));
                    itemsPtr[index] = buffer;
                }

                var result = Native.ImGui_ComboCharEx(labelPtr, currentItem.ToNative(), itemsPtr, items.Count, popupMaxHeightInItems);

                for (var index = 0; index < items.Count; index++)
                {
                    Native.ImGui_MemFree(itemsPtr[index]);
                }

                return result;
            }
        }

        public static bool Combo(string label, State<int> currentItem, string itemsSeparatedByZeros, int popupMaxHeightInItems = -1) => Native.StringToUtf8Func(label, itemsSeparatedByZeros, (labelPtr, itemsSeparatedByZerosPtr) => Native.ImGui_ComboEx(labelPtr, currentItem.ToNative(), itemsSeparatedByZerosPtr, popupMaxHeightInItems));

        // Not doing callback versions due to string freeing issues.

        #endregion

        #region Widgets: Drag Sliders

        public static bool Drag(string label, State<float> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragFloat(labelPtr, v.ToNative()));

        public static bool Drag(string label, State<float> v, float speed = 1.0f, float min = default, float max = default, string? format = "%.3f", SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragFloatEx(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options));

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

        public static bool DragRange(string label, State<float> currentMin, State<float> currentMax) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragFloatRange2(labelPtr, currentMin.ToNative(), currentMax.ToNative()));

        public static bool DragRange(string label, State<float> currentMin, State<float> currentMax, float speed = 1.0f, float min = default, float max = default, string? format = "%.3f", string? formatMax = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, formatMax, (labelPtr, formatPtr, formatMaxPtr) => Native.ImGui_DragFloatRange2Ex(labelPtr, currentMin.ToNative(), currentMax.ToNative(), speed, min, max, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<int> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragInt(labelPtr, v.ToNative()));

        public static bool Drag(string label, State<int> v, float speed = 1.0f, int min = default, int max = default, string? format = "%d", SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragIntEx(labelPtr, v.ToNative(), speed, min, max, formatPtr, (Native.ImGuiSliderFlags)options));

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

        public static bool DragRange(string label, State<int> currentMin, State<int> currentMax) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragIntRange2(labelPtr, currentMin.ToNative(), currentMax.ToNative()));

        public static bool DragRange(string label, State<int> currentMin, State<int> currentMax, float speed = 1.0f, int min = default, int max = default, string? format = "%d", string? formatMax = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, formatMax, (labelPtr, formatPtr, formatMaxPtr) => Native.ImGui_DragIntRange2Ex(labelPtr, currentMin.ToNative(), currentMax.ToNative(), speed, min, max, formatPtr, formatMaxPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<byte> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative()));

        public static bool Drag(string label, State<byte> v, float speed = 1.0f, byte min = default, byte max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<sbyte> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative()));

        public static bool Drag(string label, State<sbyte> v, float speed = 1.0f, sbyte min = default, sbyte max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<ushort> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative()));

        public static bool Drag(string label, State<ushort> v, float speed = 1.0f, ushort min = default, ushort max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<short> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative()));

        public static bool Drag(string label, State<short> v, float speed = 1.0f, short min = default, short max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<uint> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative()));

        public static bool Drag(string label, State<uint> v, float speed = 1.0f, uint min = default, uint max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<ulong> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative()));

        public static bool Drag(string label, State<ulong> v, float speed = 1.0f, ulong min = default, ulong max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<long> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative()));

        public static bool Drag(string label, State<long> v, float speed = 1.0f, long min = default, long max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, State<double> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative()));

        public static bool Drag(string label, State<double> v, float speed = 1.0f, double min = default, double max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), speed, (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<byte> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<byte> v, float speed = 1.0f, byte min = default, byte max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<sbyte> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<sbyte> v, float speed = 1.0f, sbyte min = default, sbyte max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<ushort> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<ushort> v, float speed = 1.0f, ushort min = default, ushort max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<short> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<short> v, float speed = 1.0f, short min = default, short max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<uint> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<uint> v, float speed = 1.0f, uint min = default, uint max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<ulong> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<ulong> v, float speed = 1.0f, ulong min = default, ulong max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<long> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<long> v, float speed = 1.0f, long min = default, long max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), v.Length, speed, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Drag(string label, StateVector<double> v) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_DragScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), v.Length));

        public static bool Drag(string label, StateVector<double> v, float speed = 1.0f, double min = default, double max = default, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_DragScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), v.Length, speed, (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        #endregion

        #region Widgets: Regular Sliders

        public static bool Slider(string label, State<float> v, float min, float max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderFloat(labelPtr, v.ToNative(), min, max));

        public static bool Slider(string label, State<float> v, float min, float max, string? format = "%.3f", SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderFloatEx(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options));

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

        public static bool SliderAngle(string label, State<float> radian) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderAngle(labelPtr, radian.ToNative()));

        public static bool SliderAngle(string label, State<float> radian, float degreesMin = -360.0f, float degreesMax = 360.0f, string? format = "%.3f deg", SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderAngleEx(labelPtr, radian.ToNative(), degreesMin, degreesMax, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<int> v, int min, int max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderInt(labelPtr, v.ToNative(), min, max));

        public static bool Slider(string label, State<int> v, int min, int max, string? format = "%d", SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderIntEx(labelPtr, v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options));

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

        public static bool Slider(string label, State<byte> v, byte min, byte max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<byte> v, byte min, byte max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<sbyte> v, sbyte min, sbyte max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<sbyte> v, sbyte min, sbyte max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<ushort> v, ushort min, ushort max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<ushort> v, ushort min, ushort max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<short> v, short min, short max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<short> v, short min, short max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<uint> v, uint min, uint max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<uint> v, uint min, uint max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<ulong> v, ulong min, ulong max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<ulong> v, ulong min, ulong max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<long> v, long min, long max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), (void*)min, (void*)max));

        public static bool Slider(string label, State<long> v, long min, long max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, State<double> v, double min, double max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max)));

        public static bool Slider(string label, State<double> v, double min, double max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<byte> v, byte min, byte max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<byte> v, byte min, byte max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<sbyte> v, sbyte min, sbyte max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<sbyte> v, sbyte min, sbyte max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<ushort> v, ushort min, ushort max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<ushort> v, ushort min, ushort max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<short> v, short min, short max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<short> v, short min, short max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<uint> v, uint min, uint max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<uint> v, uint min, uint max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<ulong> v, ulong min, ulong max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<ulong> v, ulong min, ulong max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<long> v, long min, long max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), v.Length, (void*)min, (void*)max));

        public static bool Slider(string label, StateVector<long> v, long min, long max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), v.Length, (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool Slider(string label, StateVector<double> v, double min, double max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SliderScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), v.Length, (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max)));

        public static bool Slider(string label, StateVector<double> v, double min, double max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_SliderScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), v.Length, (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<float> v, float min, float max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderFloat(labelPtr, size.ToNative(), v.ToNative(), min, max));

        public static bool VerticalSlider(string label, SizeF size, State<float> v, float min, float max, string? format = "%.3f", SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderFloatEx(labelPtr, size.ToNative(), v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<int> v, int min, int max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderInt(labelPtr, size.ToNative(), v.ToNative(), min, max));

        public static bool VerticalSlider(string label, SizeF size, State<int> v, int min, int max, string? format = "%d", SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderIntEx(labelPtr, size.ToNative(), v.ToNative(), min, max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<byte> v, byte min, byte max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, SizeF size, State<byte> v, byte min, byte max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U8, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<sbyte> v, sbyte min, sbyte max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, SizeF size, State<sbyte> v, sbyte min, sbyte max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S8, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<ushort> v, ushort min, ushort max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, SizeF size, State<ushort> v, ushort min, ushort max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U16, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<short> v, short min, short max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, SizeF size, State<short> v, short min, short max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S16, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<uint> v, uint min, uint max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, SizeF size, State<uint> v, uint min, uint max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U32, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<ulong> v, ulong min, ulong max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, SizeF size, State<ulong> v, ulong min, ulong max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_U64, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<long> v, long min, long max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), (void*)min, (void*)max));

        public static bool VerticalSlider(string label, SizeF size, State<long> v, long min, long max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_S64, v.ToNative(), (void*)min, (void*)max, formatPtr, (Native.ImGuiSliderFlags)options));

        public static bool VerticalSlider(string label, SizeF size, State<double> v, double min, double max) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_VSliderScalar(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max)));

        public static bool VerticalSlider(string label, SizeF size, State<double> v, double min, double max, string? format = default, SliderOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_VSliderScalarEx(labelPtr, size.ToNative(), Native.ImGuiDataType.ImGuiDataType_Double, v.ToNative(), (void*)BitConverter.DoubleToInt64Bits(min), (void*)BitConverter.DoubleToInt64Bits(max), formatPtr, (Native.ImGuiSliderFlags)options));

        #endregion

        #region Widgets: Input with Keyboard

        private static Native.ImGuiInputTextFlags CallbackOptions(InputTextCallbacks callbacks) =>
            (callbacks.Filter == null ? 0 : Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackCharFilter)
            | (callbacks.History == null ? 0 : Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackHistory)
            | (callbacks.Completion == null ? 0 : Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackCompletion)
            | (callbacks.Always == null ? 0 : Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackAlways)
            | (callbacks.Resize == null ? 0 : Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackResize)
            | (callbacks.Edit == null ? 0 : Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackEdit);

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static int InputTextCallback(Native.ImGuiInputTextCallbackData* data)
        {
            var (buffer, callbacks) = InputTextCallbacks[(nuint)data->UserData]!;
            switch (data->EventFlag)
            {
                case Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackCompletion:
                    callbacks.Completion!((Key)data->EventKey, InputTextState.Wrap(data)!.Value);
                    break;

                case Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackHistory:
                    callbacks.History!((Key)data->EventKey, InputTextState.Wrap(data)!.Value);
                    break;

                case Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackAlways:
                    callbacks.Always!(InputTextState.Wrap(data)!.Value);
                    break;

                case Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackCharFilter:
                    var c = callbacks.Filter!(data->EventChar);
                    data->EventChar = c ?? '\0';
                    break;

                case Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackResize:
                    callbacks.Resize!(buffer, data->BufSize);
                    data->Buf = buffer.ToNative();
                    break;

                case Native.ImGuiInputTextFlags.ImGuiInputTextFlags_CallbackEdit:
                    callbacks.Edit!(InputTextState.Wrap(data)!.Value);
                    break;
            }

            return 0;
        }

        public static bool InputText(string label, StateText buffer, InputTextOptions options = default) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputText(labelPtr, buffer.ToNative(), (nuint)buffer.Capacity, (Native.ImGuiInputTextFlags)options));

        public static bool InputText(string label, StateText buffer, InputTextOptions options, InputTextCallbacks callbacks)
        {
            InputTextCallbacks[(nuint)callbacks.GetHashCode()] = (buffer, callbacks);
            return Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputTextEx(labelPtr, buffer.ToNative(), (nuint)buffer.Capacity, (Native.ImGuiInputTextFlags)options | CallbackOptions(callbacks), &InputTextCallback, (void*)callbacks.GetHashCode()));
        }

        public static bool InputTextMultiline(string label, StateText buffer) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputTextMultiline(labelPtr, buffer.ToNative(), (nuint)buffer.Capacity));

        public static bool InputTextMultiline(string label, StateText buffer, SizeF size, InputTextOptions options = default) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputTextMultilineEx(labelPtr, buffer.ToNative(), (nuint)buffer.Capacity, size.ToNative(), (Native.ImGuiInputTextFlags)options));

        public static bool InputTextMultiline(string label, StateText buffer, SizeF size, InputTextOptions options, InputTextCallbacks callbacks)
        {
            InputTextCallbacks[(nuint)callbacks.GetHashCode()] = (buffer, callbacks);
            return Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputTextMultilineEx(labelPtr, buffer.ToNative(), (nuint)buffer.Capacity, size.ToNative(), (Native.ImGuiInputTextFlags)options | CallbackOptions(callbacks), &InputTextCallback, (void*)callbacks.GetHashCode()));
        }

        public static bool InputText(string label, string hint, StateText buffer, InputTextOptions options = default) => Native.StringToUtf8Func(label, hint, (labelPtr, hintPtr) => Native.ImGui_InputTextWithHint(labelPtr, hintPtr, buffer.ToNative(), (nuint)buffer.Capacity, (Native.ImGuiInputTextFlags)options));

        public static bool InputText(string label, string hint, StateText buffer, InputTextOptions options, InputTextCallbacks callbacks)
        {
            InputTextCallbacks[(nuint)callbacks.GetHashCode()] = (buffer, callbacks);
            return Native.StringToUtf8Func(label, hint, (labelPtr, hintPtr) => Native.ImGui_InputTextWithHintEx(labelPtr, hintPtr, buffer.ToNative(), (nuint)buffer.Capacity, (Native.ImGuiInputTextFlags)options | CallbackOptions(callbacks), &InputTextCallback, (void*)callbacks.GetHashCode()));
        }

        public static bool Input(string label, State<sbyte> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, data.ToNative()));

        public static bool Input(string label, State<sbyte> data, sbyte step = default, sbyte stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, data.ToNative(), (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, StateVector<sbyte> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, data.ToNative(), data.Length));

        public static bool Input(string label, StateVector<sbyte> data, sbyte step = default, sbyte stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S8, data.ToNative(), data.Length, (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, State<byte> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, data.ToNative()));

        public static bool Input(string label, State<byte> data, byte step = default, byte stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, data.ToNative(), (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, StateVector<byte> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, data.ToNative(), data.Length));

        public static bool Input(string label, StateVector<byte> data, byte step = default, byte stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U8, data.ToNative(), data.Length, (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, State<short> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, data.ToNative()));

        public static bool Input(string label, State<short> data, short step = default, short stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, data.ToNative(), (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, StateVector<short> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, data.ToNative(), data.Length));

        public static bool Input(string label, StateVector<short> data, short step = default, short stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S16, data.ToNative(), data.Length, (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, State<ushort> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, data.ToNative()));

        public static bool Input(string label, State<ushort> data, ushort step = default, ushort stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, data.ToNative(), (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, StateVector<ushort> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, data.ToNative(), data.Length));

        public static bool Input(string label, StateVector<ushort> data, ushort step = default, ushort stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U16, data.ToNative(), data.Length, (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, State<int> v) => Native.StringToUtf8Func(label, ptr => Native.ImGui_InputInt(ptr, v.ToNative()));

        public static bool Input(string label, State<int> v, int step = 1, int stepFast = 100, InputTextOptions options = default) => Native.StringToUtf8Func(label, ptr => Native.ImGui_InputIntEx(ptr, v.ToNative(), step, stepFast, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, StateVector<int> v, InputTextOptions options = default) => Native.StringToUtf8Func(label, labelPtr => v.Length switch
        {
            2 => Native.ImGui_InputInt2(labelPtr, v.ToNative(), (Native.ImGuiInputTextFlags)options),
            3 => Native.ImGui_InputInt3(labelPtr, v.ToNative(), (Native.ImGuiInputTextFlags)options),
            4 => Native.ImGui_InputInt4(labelPtr, v.ToNative(), (Native.ImGuiInputTextFlags)options),
            _ => Native.ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S32, v.ToNative(), v.Length, null, null, null, (Native.ImGuiInputTextFlags)options),
        });

        public static bool Input(string label, State<uint> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, data.ToNative()));

        public static bool Input(string label, State<uint> data, uint step = default, uint stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, data.ToNative(), (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, StateVector<uint> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, data.ToNative(), data.Length));

        public static bool Input(string label, StateVector<uint> data, uint step = default, uint stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U32, data.ToNative(), data.Length, (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, State<long> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, data.ToNative()));

        public static bool Input(string label, State<long> data, long step = default, long stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, data.ToNative(), (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, StateVector<long> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, data.ToNative(), data.Length));

        public static bool Input(string label, StateVector<long> data, long step = default, long stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_S64, data.ToNative(), data.Length, (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, State<ulong> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalar(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, data.ToNative()));

        public static bool Input(string label, State<ulong> data, ulong step = default, ulong stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, data.ToNative(), (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, StateVector<ulong> data) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_InputScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, data.ToNative(), data.Length));

        public static bool Input(string label, StateVector<ulong> data, ulong step = default, ulong stepFast = default, string? format = default, InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_U64, data.ToNative(), data.Length, (void*)step, (void*)stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, State<float> v) => Native.StringToUtf8Func(label, ptr => Native.ImGui_InputFloat(ptr, v.ToNative()));

        public static bool Input(string label, State<float> v, float step = default, float stepFast = default, string format = "%.3f", InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputFloatEx(labelPtr, v.ToNative(), step, stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        public static bool Input(string label, StateVector<float> v) => Native.StringToUtf8Func(label, labelPtr => v.Length switch
        {
            2 => Native.ImGui_InputFloat2(labelPtr, v.ToNative()),
            3 => Native.ImGui_InputFloat3(labelPtr, v.ToNative()),
            4 => Native.ImGui_InputFloat4(labelPtr, v.ToNative()),
            _ => Native.ImGui_InputScalarN(labelPtr, Native.ImGuiDataType.ImGuiDataType_Float, v.ToNative(), v.Length),
        });

        public static bool Input(string label, StateVector<float> v, string format = "%.3f", InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => v.Length switch
        {
            2 => Native.ImGui_InputFloat2Ex(labelPtr, v.ToNative(), formatPtr, (Native.ImGuiInputTextFlags)options),
            3 => Native.ImGui_InputFloat3Ex(labelPtr, v.ToNative(), formatPtr, (Native.ImGuiInputTextFlags)options),
            4 => Native.ImGui_InputFloat4Ex(labelPtr, v.ToNative(), formatPtr, (Native.ImGuiInputTextFlags)options),
            _ => Native.ImGui_InputScalarNEx(labelPtr, Native.ImGuiDataType.ImGuiDataType_Float, v.ToNative(), v.Length, null, null, formatPtr, (Native.ImGuiInputTextFlags)options),
        });

        public static bool Input(string label, State<double> v) => Native.StringToUtf8Func(label, ptr => Native.ImGui_InputDouble(ptr, v.ToNative()));

        public static bool Input(string label, State<double> v, double step = 0.0, double stepFast = 0.0, string format = "%.6f", InputTextOptions options = default) => Native.StringToUtf8Func(label, format, (labelPtr, formatPtr) => Native.ImGui_InputDoubleEx(labelPtr, v.ToNative(), step, stepFast, formatPtr, (Native.ImGuiInputTextFlags)options));

        #endregion

        #region Widgets: Color Editor/Picker

        public static bool ColorEdit(string label, StateVector<float> color, ColorEditOptions options = default) =>
            color.Length switch
            {
                3 => Native.StringToUtf8Func(label, ptr => Native.ImGui_ColorEdit3(ptr, color.ToNative(), (Native.ImGuiColorEditFlags)options)),
                4 => Native.StringToUtf8Func(label, ptr => Native.ImGui_ColorEdit4(ptr, color.ToNative(), (Native.ImGuiColorEditFlags)options)),
                _ => throw new InvalidOperationException()
            };

        public static bool ColorPicker(string label, StateVector<float> color, ColorEditOptions options = default) => color.Length != 3 ? throw new InvalidOperationException() : Native.StringToUtf8Func(label, ptr => Native.ImGui_ColorPicker3(ptr, color.ToNative(), (Native.ImGuiColorEditFlags)options));

        public static bool ColorPicker(string label, StateVector<float> color, ColorEditOptions options = default, Span<float> referenceColor = default)
        {
            if (color.Length != 4)
            {
                throw new InvalidOperationException();
            }

            fixed (byte* labelPtr = Native.StringToUtf8(label))
            fixed (float* referencePtr = referenceColor)
            {
                return Native.ImGui_ColorPicker4(labelPtr, color.ToNative(), (Native.ImGuiColorEditFlags)options, referencePtr);
            }
        }

        public static bool ColorButton(string descriptionId, ColorF col, ColorEditOptions options = default) => Native.StringToUtf8Func(descriptionId, ptr => Native.ImGui_ColorButton(ptr, col.ToNative(), (Native.ImGuiColorEditFlags)options));

        public static bool ColorButton(string descriptionId, ColorF col, ColorEditOptions options = default, SizeF size = default) => Native.StringToUtf8Func(descriptionId, ptr => Native.ImGui_ColorButtonEx(ptr, col.ToNative(), (Native.ImGuiColorEditFlags)options, size.ToNative()));

        public static void SetColorEditOptions(ColorEditOptions options) => Native.ImGui_SetColorEditOptions((Native.ImGuiColorEditFlags)options);

        #endregion

        #region Widgets: Trees

        public static bool TreeNode(string label) => Native.StringToUtf8Func(label, Native.ImGui_TreeNode);

        public static bool TreeNode(string id, string format) => Native.StringToUtf8Func(id, format, Native.ImGui_TreeNodeStr);

        public static bool TreeNode(nuint id, string format) => Native.StringToUtf8Func(format, formatPtr => Native.ImGui_TreeNodePtr((void*)id, formatPtr));

        public static bool TreeNode(string label, TreeNodeOptions options = default) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_TreeNodeEx(labelPtr, (Native.ImGuiTreeNodeFlags)options));

        public static bool TreeNode(string id, TreeNodeOptions options, string format) => Native.StringToUtf8Func(id, format, (idPtr, formatPtr) => Native.ImGui_TreeNodeExStr(idPtr, (Native.ImGuiTreeNodeFlags)options, formatPtr));

        public static bool TreeNode(nuint id, TreeNodeOptions options, string format) => Native.StringToUtf8Func(format, formatPtr => Native.ImGui_TreeNodeExPtr((void*)id, (Native.ImGuiTreeNodeFlags)options, formatPtr));

        public static void TreePush(string id) => Native.StringToUtf8Action(id, Native.ImGui_TreePush);

        public static void TreePush(nuint id) => Native.ImGui_TreePushPtr((void*)id);

        public static void TreePop() => Native.ImGui_TreePop();

        public static float GetTreeNodeToLabelSpacing() => Native.ImGui_GetTreeNodeToLabelSpacing();

        public static bool CollapsingHeader(string label, TreeNodeOptions options = default) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_CollapsingHeader(labelPtr, (Native.ImGuiTreeNodeFlags)options));

        public static bool CollapsingHeader(string label, State<bool>? visible, TreeNodeOptions options = default) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_CollapsingHeaderBoolPtr(labelPtr, visible.ToNative(), (Native.ImGuiTreeNodeFlags)options));

        public static void SetNextItemOpen(bool isOpen, Condition cond = default) => Native.ImGui_SetNextItemOpen(isOpen, (Native.ImGuiCond)cond);

        #endregion

        #region Widgets: Selectables

        public static bool Selectable(string label) => Native.StringToUtf8Func(label, Native.ImGui_Selectable);

        public static bool Selectable(string label, bool selected = false, SelectableOptions options = default, SizeF size = default) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SelectableEx(labelPtr, selected, (Native.ImGuiSelectableFlags)options, size.ToNative()));

        public static bool Selectable(string label, State<bool> selected) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SelectableBoolPtr(labelPtr, selected.ToNative(), default));

        public static bool Selectable(string label, State<bool> selected, SelectableOptions options) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SelectableBoolPtr(labelPtr, selected.ToNative(), (Native.ImGuiSelectableFlags)options));

        public static bool Selectable(string label, State<bool> selected, SelectableOptions options, SizeF size) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_SelectableBoolPtrEx(labelPtr, selected.ToNative(), (Native.ImGuiSelectableFlags)options, size.ToNative()));

        #endregion

        #region Widgets: List Boxes

        public static bool BeginListBox(string label, SizeF size = default) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_BeginListBox(labelPtr, size.ToNative()));

        public static void EndListBox() => Native.ImGui_EndListBox();

        public static bool ListBox(string label, State<int> currentItem, IReadOnlyList<string> items, int heightInItems = -1)
        {
            fixed (byte* labelPtr = Native.StringToUtf8(label))
            {
                var itemsPtr = stackalloc byte*[items.Count];
                for (var index = 0; index < items.Count; index++)
                {
                    var bytes = Native.StringToUtf8(items[index]);
                    var buffer = (byte*)Native.ImGui_MemAlloc((nuint)bytes.Length);
                    bytes.CopyTo(new(buffer, bytes.Length));
                    itemsPtr[index] = buffer;
                }

                var result = Native.ImGui_ListBox(labelPtr, currentItem.ToNative(), itemsPtr, items.Count, heightInItems);

                for (var index = 0; index < items.Count; index++)
                {
                    Native.ImGui_MemFree(itemsPtr[index]);
                }

                return result;
            }
        }

        // Not doing callback versions due to string freeing issues.

        #endregion

        #region Widgets: Data Plotting

        public static void PlotLines(string label, Span<float> values)
        {
            fixed (byte* labelPtr = Native.StringToUtf8(label))
            fixed (float* valuesPtr = values)
            {
                Native.ImGui_PlotLines(labelPtr, valuesPtr, values.Length);
            }
        }

        public static void PlotLines(string label, Span<float> values, string? overlayText = default, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, SizeF graphSize = default)
        {
            fixed (byte* labelPtr = Native.StringToUtf8(label))
            fixed (float* valuesPtr = values)
            fixed (byte* overlayTextPtr = Native.StringToUtf8(overlayText))
            {
                Native.ImGui_PlotLinesEx(labelPtr, valuesPtr, values.Length, default, overlayTextPtr, scaleMin, scaleMax, graphSize.ToNative(), default);
            }
        }

        [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvCdecl) })]
        private static float NativePlotCallback(void* data, int index) => PlotCallbacks.TryGetValue((nuint)data, out var callback) ? callback(index) : 0;

        public static void PlotLines(string label, Func<int, float> getter, int valuesCount) => Native.StringToUtf8Action(label, labelPtr =>
        {
            PlotCallbacks[(nuint)getter.GetHashCode()] = getter;
            Native.ImGui_PlotLinesCallback(labelPtr, &NativePlotCallback, (void*)getter.GetHashCode(), valuesCount);
        });

        public static void PlotLines(string label, Func<int, float> getter, int valuesCount, string? overlayText = default, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, SizeF graphSize = default) => Native.StringToUtf8Action(label, overlayText, (labelPtr, overlayTextPtr) =>
        {
            PlotCallbacks[(nuint)getter.GetHashCode()] = getter;
            Native.ImGui_PlotLinesCallbackEx(labelPtr, &NativePlotCallback, (void*)getter.GetHashCode(), valuesCount, default, overlayTextPtr, scaleMin, scaleMax, graphSize.ToNative());
        });

        public static void PlotHistogram(string label, Span<float> values)
        {
            fixed (byte* labelPtr = Native.StringToUtf8(label))
            fixed (float* valuesPtr = values)
            {
                Native.ImGui_PlotHistogram(labelPtr, valuesPtr, values.Length);
            }
        }

        public static void PlotHistogram(string label, Span<float> values, string? overlayText = default, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, SizeF graphSize = default)
        {
            fixed (byte* labelPtr = Native.StringToUtf8(label))
            fixed (float* valuesPtr = values)
            fixed (byte* overlayTextPtr = Native.StringToUtf8(overlayText))
            {
                Native.ImGui_PlotHistogramEx(labelPtr, valuesPtr, values.Length, default, overlayTextPtr, scaleMin, scaleMax, graphSize.ToNative(), default);
            }
        }

        public static void PlotHistogram(string label, Func<int, float> getter, int valuesCount) => Native.StringToUtf8Action(label, labelPtr =>
        {
            PlotCallbacks[(nuint)getter.GetHashCode()] = getter;
            Native.ImGui_PlotHistogramCallback(labelPtr, &NativePlotCallback, (void*)getter.GetHashCode(), valuesCount);
        });

        public static void PlotHistogram(string label, Func<int, float> getter, int valuesCount, string? overlayText = default, float scaleMin = float.MaxValue, float scaleMax = float.MaxValue, SizeF graphSize = default) => Native.StringToUtf8Action(label, overlayText, (labelPtr, overlayTextPtr) =>
        {
            PlotCallbacks[(nuint)getter.GetHashCode()] = getter;
            Native.ImGui_PlotHistogramCallbackEx(labelPtr, &NativePlotCallback, (void*)getter.GetHashCode(), valuesCount, default, overlayTextPtr, scaleMin, scaleMax, graphSize.ToNative());
        });

        #endregion

        #region Widgets: Menus

        public static bool BeginMenuBar() => Native.ImGui_BeginMenuBar();

        public static void EndMenuBar() => Native.ImGui_EndMenuBar();

        public static bool BeginMainMenuBar() => Native.ImGui_BeginMainMenuBar();

        public static void EndMainMenuBar() => Native.ImGui_EndMainMenuBar();

        public static bool BeginMenu(string label) => Native.StringToUtf8Func(label, Native.ImGui_BeginMenu);

        public static bool BeginMenu(string label, bool enabled) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_BeginMenuEx(labelPtr, enabled));

        public static void EndMenu() => Native.ImGui_EndMenu();

        public static bool MenuItem(string label) => Native.StringToUtf8Func(label, Native.ImGui_MenuItem);

        public static bool MenuItem(string label, string? shortcut = default, bool selected = false, bool enabled = true) => Native.StringToUtf8Func(label, shortcut, (labelPtr, shortcutPtr) => Native.ImGui_MenuItemEx(labelPtr, shortcutPtr, selected, enabled));

        public static bool MenuItem(string label, string? shortcut, State<bool> selected, bool enabled = true) => Native.StringToUtf8Func(label, shortcut, (labelPtr, shortcutPtr) => Native.ImGui_MenuItemBoolPtr(labelPtr, shortcutPtr, selected.ToNative(), enabled));

        #endregion

        #region Tooltips

        public static void BeginTooltip() => Native.ImGui_BeginTooltip();

        public static void EndTooltip() => Native.ImGui_EndTooltip();

        public static void SetTooltip(string text) => Native.StringToUtf8Action(text, Native.ImGui_SetTooltip);

        #endregion

        #region Popups: begin/end functions

        public static bool BeginPopup(string id, WindowOptions options = default) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_BeginPopup(idPtr, (Native.ImGuiWindowFlags)options));

        public static bool BeginPopupModal(string name, State<bool>? open = default, WindowOptions options = default) => Native.StringToUtf8Func(name, namePtr => Native.ImGui_BeginPopupModal(namePtr, open.ToNative(), (Native.ImGuiWindowFlags)options));

        public static void EndPopup() => Native.ImGui_EndPopup();

        #endregion

        #region Popups: open/close functions

        public static void OpenPopup(string id, PopupOptions options = default) => Native.StringToUtf8Action(id, idPtr => Native.ImGui_OpenPopup(idPtr, (Native.ImGuiPopupFlags)options));

        public static void OpenPopup(Id id, PopupOptions options = default) => Native.ImGui_OpenPopupID(id.ToNative(), (Native.ImGuiPopupFlags)options);

        public static void OpenPopupOnItemClick(string? id = null, PopupOptions options = PopupOptions.MouseButtonRight) => Native.StringToUtf8Action(id, idPtr => Native.ImGui_OpenPopupOnItemClick(idPtr, (Native.ImGuiPopupFlags)options));

        public static void CloseCurrentPopup() => Native.ImGui_CloseCurrentPopup();

        #endregion

        #region Popups: open+begin combined functions helpers

        public static bool BeginPopupContextItem() => Native.ImGui_BeginPopupContextItem();

        public static bool BeginPopupContextItem(string? id = null, PopupOptions options = PopupOptions.MouseButtonRight) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_BeginPopupContextItemEx(idPtr, (Native.ImGuiPopupFlags)options));

        public static bool BeginPopupContextWindow() => Native.ImGui_BeginPopupContextWindow();

        public static bool BeginPopupContextWindow(string? id = null, PopupOptions options = PopupOptions.MouseButtonRight) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_BeginPopupContextWindowEx(idPtr, (Native.ImGuiPopupFlags)options));

        public static bool BeginPopupContextVoid() => Native.ImGui_BeginPopupContextVoid();

        public static bool BeginPopupContextVoid(string? id = null, PopupOptions options = PopupOptions.MouseButtonRight) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_BeginPopupContextVoidEx(idPtr, (Native.ImGuiPopupFlags)options));

        #endregion

        #region Popups: query functions

        public static bool IsPopupOpen(string id, PopupOptions options = default) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_IsPopupOpen(idPtr, (Native.ImGuiPopupFlags)options));

        #endregion

        #region Tables

        public static bool BeginTable(string id, int column) => BeginTable(id, column, default);

        public static bool BeginTable(string id, int column, TableOptions options) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_BeginTable(idPtr, column, (Native.ImGuiTableFlags)options));

        public static bool BeginTable(string id, int column, TableOptions options = default, SizeF outerSize = default, float innerWidth = default) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_BeginTableEx(idPtr, column, (Native.ImGuiTableFlags)options, outerSize.ToNative(), innerWidth));

        public static void EndTable() => Native.ImGui_EndTable();

        public static void TableNextRow() => Native.ImGui_TableNextRow();

        public static void TableNextRow(TableRowOptions rowOptions = default, float minRowHeight = default) => Native.ImGui_TableNextRowEx((Native.ImGuiTableRowFlags)rowOptions, minRowHeight);

        public static bool TableNextColumn() => Native.ImGui_TableNextColumn();

        public static bool TableSetColumnIndex(int columnN) => Native.ImGui_TableSetColumnIndex(columnN);

        #endregion

        #region Tables: Headers & Columns declaration

        public static void TableSetupColumn(string label, TableColumnOptions options = default) => Native.StringToUtf8Action(label, labelPtr => Native.ImGui_TableSetupColumn(labelPtr, (Native.ImGuiTableColumnFlags)options));

        public static void TableSetupColumn(string label, TableColumnOptions options = default, float initWidthOrWeight = default, Id userId = default) => Native.StringToUtf8Action(label, labelPtr => Native.ImGui_TableSetupColumnEx(labelPtr, (Native.ImGuiTableColumnFlags)options, initWidthOrWeight, userId.ToNative()));

        public static void TableSetupScrollFreeze(int cols, int rows) => Native.ImGui_TableSetupScrollFreeze(cols, rows);

        public static void TableHeadersRow() => Native.ImGui_TableHeadersRow();

        public static void TableHeader(string label) => Native.StringToUtf8Action(label, Native.ImGui_TableHeader);

        #endregion

        #region Tables: Sorting & Miscellaneous functions

        public static TableSortSpecifications? TableGetSortSpecifications() => TableSortSpecifications.Wrap(Native.ImGui_TableGetSortSpecs());

        public static int TableGetColumnCount() => Native.ImGui_TableGetColumnCount();

        public static int TableGetColumnIndex() => Native.ImGui_TableGetColumnIndex();

        public static int TableGetRowIndex() => Native.ImGui_TableGetRowIndex();

        public static string? TableGetColumnName(int columnN = -1) => Native.Utf8ToString(Native.ImGui_TableGetColumnName(columnN));

        public static TableColumnOptions TableGetColumnOptions(int columnN = -1) => (TableColumnOptions)Native.ImGui_TableGetColumnFlags(columnN);

        public static void TableSetColumnEnabled(int columnN, bool v) => Native.ImGui_TableSetColumnEnabled(columnN, v);

        public static void TableSetBackgroundColor(TableBackgroundTarget target, uint color, int columnN = -1) => Native.ImGui_TableSetBgColor((Native.ImGuiTableBgTarget)target, color, columnN);

        #endregion

        #region Legacy Columns API (prefer using Tables!)

        public static void Columns() => Native.ImGui_Columns();

        public static void Columns(int count = 1, string? id = null, bool order = true) => Native.StringToUtf8Action(id, idPtr => Native.ImGui_ColumnsEx(count, idPtr, order));

        public static void NextColumn() => Native.ImGui_NextColumn();

        public static int GetColumnIndex() => Native.ImGui_GetColumnIndex();

        public static float GetColumnWidth(int columnIndex = -1) => Native.ImGui_GetColumnWidth(columnIndex);

        public static void SetColumnWidth(int columnIndex, float width) => Native.ImGui_SetColumnWidth(columnIndex, width);

        public static float GetColumnOffset(int columnIndex = -1) => Native.ImGui_GetColumnOffset(columnIndex);

        public static void SetColumnOffset(int columnIndex, float offsetX) => Native.ImGui_SetColumnOffset(columnIndex, offsetX);

        public static int GetColumnsCount() => Native.ImGui_GetColumnsCount();

        #endregion

        #region Tab Bars, Tabs

        public static bool BeginTabBar(string id, TabBarOptions options = default) => Native.StringToUtf8Func(id, idPtr => Native.ImGui_BeginTabBar(idPtr, (Native.ImGuiTabBarFlags)options));

        public static void EndTabBar() => Native.ImGui_EndTabBar();

        public static bool BeginTabItem(string label, State<bool>? open = default, TabItemOptions options = default) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_BeginTabItem(labelPtr, open.ToNative(), (Native.ImGuiTabItemFlags)options));

        public static void EndTabItem() => Native.ImGui_EndTabItem();

        public static bool TabItemButton(string label, TabItemOptions options = default) => Native.StringToUtf8Func(label, labelPtr => Native.ImGui_TabItemButton(labelPtr, (Native.ImGuiTabItemFlags)options));

        public static void SetTabItemClosed(string tabOrDockedWindowLabel) => Native.StringToUtf8Action(tabOrDockedWindowLabel, Native.ImGui_SetTabItemClosed);

        #endregion

        #region Logging/Capture

        public static void LogToTty(int autoOpenDepth = -1) => Native.ImGui_LogToTTY(autoOpenDepth);

        public static void LogToFile(int autoOpenDepth = -1, string? filename = null) => Native.StringToUtf8Action(filename, filenamePtr => Native.ImGui_LogToFile(autoOpenDepth, filenamePtr));

        public static void LogToClipboard(int autoOpenDepth = -1) => Native.ImGui_LogToClipboard(autoOpenDepth);

        public static void LogFinish() => Native.ImGui_LogFinish();

        public static void LogButtons() => Native.ImGui_LogButtons();

        public static void LogText(string text) => Native.StringToUtf8Action(text, Native.ImGui_LogText);

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

        public static Payload? AcceptDragDropPayload(string type, DragDropOptions options = default) => Native.StringToUtf8Func(type, typePtr => Payload.Wrap(Native.ImGui_AcceptDragDropPayload(typePtr, (Native.ImGuiDragDropFlags)options)));

        public static void EndDragDropTarget() => Native.ImGui_EndDragDropTarget();

        public static Payload? GetDragDropPayload() => Payload.Wrap(Native.ImGui_GetDragDropPayload());

        #endregion

        #region Disabling [BETA API]

        public static void BeginDisabled(bool disabled = true) => Native.ImGui_BeginDisabled(disabled);

        public static void EndDisabled() => Native.ImGui_EndDisabled();

        #endregion

        #region Clipping

        public static void PushClipRect(RectangleF rect, bool intersectWithCurrentClipRect) => Native.ImGui_PushClipRect(rect.Min.ToNative(), rect.Max.ToNative(), intersectWithCurrentClipRect);

        public static void PopClipRect() => Native.ImGui_PopClipRect();

        #endregion

        #region Focus, Activation

        public static void SetItemDefaultFocus() => Native.ImGui_SetItemDefaultFocus();

        public static void SetKeyboardFocusHere() => Native.ImGui_SetKeyboardFocusHere();

        public static void SetKeyboardFocusHere(int offset) => Native.ImGui_SetKeyboardFocusHereEx(offset);

        #endregion

        #region Item/Widgets Utilities and Query Functions

        public static bool IsItemHovered(HoveredOptions options = default) => Native.ImGui_IsItemHovered((Native.ImGuiHoveredFlags)options);

        public static bool IsItemActive() => Native.ImGui_IsItemActive();

        public static bool IsItemFocused() => Native.ImGui_IsItemFocused();

        public static bool IsItemClicked() => Native.ImGui_IsItemClicked();

        public static bool IsItemClicked(MouseButton mouseButton) => Native.ImGui_IsItemClickedEx((Native.ImGuiMouseButton)mouseButton);

        public static bool IsItemVisible() => Native.ImGui_IsItemVisible();

        public static bool IsItemEdited() => Native.ImGui_IsItemEdited();

        public static bool IsItemActivated() => Native.ImGui_IsItemActivated();

        public static bool IsItemDeactivated() => Native.ImGui_IsItemDeactivated();

        public static bool IsItemDeactivatedAfterEdit() => Native.ImGui_IsItemDeactivatedAfterEdit();

        public static bool IsItemToggledOpen() => Native.ImGui_IsItemToggledOpen();

        public static bool IsAnyItemHovered() => Native.ImGui_IsAnyItemHovered();

        public static bool IsAnyItemActive() => Native.ImGui_IsAnyItemActive();

        public static bool IsAnyItemFocused() => Native.ImGui_IsAnyItemFocused();

        public static Id GetItemId() => Id.Wrap(Native.ImGui_GetItemID());

        public static RectangleF GetItemRectangle() => new(PositionF.Wrap(Native.ImGui_GetItemRectMin()), PositionF.Wrap(Native.ImGui_GetItemRectMax()));

        public static SizeF GetItemRectangleSize() => SizeF.Wrap(Native.ImGui_GetItemRectSize());

        public static void SetItemAllowOverlap() => Native.ImGui_SetItemAllowOverlap();

        #endregion

        #region Viewports

        public static Viewport GetMainViewport() => Viewport.Wrap(Native.ImGui_GetMainViewport())!.Value;

        #endregion

        #region Background/Foreground Draw Lists

        public static DrawList GetBackgroundDrawList => DrawList.Wrap(Native.ImGui_GetBackgroundDrawList())!.Value;

        public static DrawList GetForegroundDrawList => DrawList.Wrap(Native.ImGui_GetForegroundDrawList())!.Value;

        #endregion

        #region Miscellaneous Utilities

        public static bool IsRectVisibleBySize(SizeF size) => Native.ImGui_IsRectVisibleBySize(size.ToNative());

        public static bool IsRectVisible(RectangleF rect) => Native.ImGui_IsRectVisible(rect.Min.ToNative(), rect.Max.ToNative());

        public static double GetTime() => Native.ImGui_GetTime();

        public static int GetFrameCount() => Native.ImGui_GetFrameCount();

        public static string GetStyleColorName(StyleColor color) => Native.Utf8ToString(Native.ImGui_GetStyleColorName((Native.ImGuiCol)color))!;

        public static void SetStateStorage(Storage storage) => Native.ImGui_SetStateStorage(storage.ToNative());

        public static Storage? GetStateStorage() => Storage.Wrap(Native.ImGui_GetStateStorage());

        public static bool BeginChildFrame(Id id, SizeF size, WindowOptions flags = default) => Native.ImGui_BeginChildFrame(id.ToNative(), size.ToNative(), (Native.ImGuiWindowFlags)flags);

        public static void EndChildFrame() => Native.ImGui_EndChildFrame();

        #endregion

        #region Text Utilities

        public static SizeF CalcTextSize(string text) => SizeF.Wrap(Native.StringToUtf8Func(text, Native.ImGui_CalcTextSize));

        public static SizeF CalcTextSize(string text, bool hideTextAfterDoubleHash) => SizeF.Wrap(Native.StringToUtf8Func(text, textPtr => Native.ImGui_CalcTextSizeEx(textPtr, null, hideTextAfterDoubleHash)));

        public static SizeF CalcTextSize(string text, bool hideTextAfterDoubleHash, float wrapWidth) => SizeF.Wrap(Native.StringToUtf8Func(text, ptr => Native.ImGui_CalcTextSizeEx(ptr, null, hideTextAfterDoubleHash, wrapWidth)));

        #endregion

        #region Color Utilities

        public static ColorF ColorConvert(uint i) => ColorF.Wrap(Native.ImGui_ColorConvertU32ToFloat4(i));

        public static uint ColorConvert(ColorF c) => Native.ImGui_ColorConvertFloat4ToU32(c.ToNative());

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

        public static bool IsMouseHovering(RectangleF rect) => Native.ImGui_IsMouseHoveringRect(rect.Min.ToNative(), rect.Max.ToNative());

        public static bool IsMouseHovering(RectangleF rect, bool clip) => Native.ImGui_IsMouseHoveringRectEx(rect.Min.ToNative(), rect.Max.ToNative(), clip);

        public static bool IsMousePosValid(PositionF? mousePosition = default)
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

        public static PositionF GetMousePosition() => PositionF.Wrap(Native.ImGui_GetMousePos());

        public static PositionF GetMousePositionOnOpeningCurrentPopup() => PositionF.Wrap(Native.ImGui_GetMousePosOnOpeningCurrentPopup());

        public static bool IsMouseDragging(MouseButton button, float lockThreshold = -1.0f) => Native.ImGui_IsMouseDragging((Native.ImGuiMouseButton)button, lockThreshold);

        public static SizeF GetMouseDragDelta(MouseButton button = default, float lockThreshold = -1.0f) => SizeF.Wrap(Native.ImGui_GetMouseDragDelta((Native.ImGuiMouseButton)button, lockThreshold));

        public static void ResetMouseDragDelta() => Native.ImGui_ResetMouseDragDelta();

        public static void ResetMouseDragDelta(MouseButton button) => Native.ImGui_ResetMouseDragDeltaEx((Native.ImGuiMouseButton)button);

        public static MouseCursor GetMouseCursor() => (MouseCursor)Native.ImGui_GetMouseCursor();

        public static void SetMouseCursor(MouseCursor cursorType) => Native.ImGui_SetMouseCursor((Native.ImGuiMouseCursor)cursorType);

        public static void SetNextFrameWantCaptureMouse(bool wantCaptureMouse) => Native.ImGui_SetNextFrameWantCaptureMouse(wantCaptureMouse);

        #endregion

        #region Clipboard Utilities

        public static string? GetClipboardText => Native.Utf8ToString(Native.ImGui_GetClipboardText());

        public static void SetClipboardText(string? text) => Native.StringToUtf8Action(text, Native.ImGui_SetClipboardText);

        #endregion

        #region Settings/.Ini Utilities

        public static void LoadIniSettingsFromDisk(string iniFilename) => Native.StringToUtf8Action(iniFilename, Native.ImGui_LoadIniSettingsFromDisk);

        public static void LoadIniSettingsFromMemory(Span<byte> iniData)
        {
            fixed (byte* ptr = iniData)
            {
                Native.ImGui_LoadIniSettingsFromMemory(ptr, (nuint)iniData.Length);
            }
        }

        public static void SaveIniSettingsToDisk(string iniFilename) => Native.StringToUtf8Action(iniFilename, Native.ImGui_SaveIniSettingsToDisk);

        public static Span<byte> SaveIniSettingsToMemory()
        {
            nuint size = 0;
            var ptr = Native.ImGui_SaveIniSettingsToMemory(&size);
            return new(ptr, (int)size);
        }

        #endregion

        #region Debug Utilities

        public static void DebugTextEncoding(string text) => Native.StringToUtf8Action(text, Native.ImGui_DebugTextEncoding);

        // Not checking version since we elide structures

        #endregion

        #region Memory Allocators

        // No point in setting the allocators from managed space

        public static nuint MemAlloc(nuint size) => (nuint)Native.ImGui_MemAlloc(size);

        public static void MemFree(nuint block) => Native.ImGui_MemFree((void*)block);

        #endregion
    }
}
