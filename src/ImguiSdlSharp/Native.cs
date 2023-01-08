using System;
using System.Runtime.InteropServices;
using static SdlSharp.Imgui.Native;

namespace SdlSharp.Imgui
{
    public static unsafe class Native
    {
        public const string ImguiLibrary = "ImguiSdlSharp.Native.dll";

        public const string IMGUI_VERSION = "1.89.2";
        public const int IMGUI_VERSION_NUM = 18920;

        #region FORWARDS TODO
        public struct ImGuiContext { }
        public struct ImFontAtlas { }
        public struct ImDrawData { }
        public struct ImDrawList { }
        public struct ImGuiSizeCallback { }

        public struct ImFont { }

        public struct ImGuiTableSortSpecs { }

        public struct ImGuiInputTextCallback { }

        public struct ImGuiPayload { }

        public struct ImGuiViewport { }

        public struct ImDrawListSharedData { }

        public struct ImGuiStorage { }

        public struct ImGuiMemAllocFunc { }

        public struct ImGuiMemFreeFunc { }
        #endregion

        public readonly record struct ImTextureID(nuint Value);

        public readonly record struct ImGuiID(uint Value);

        public readonly record struct ImVec2(float x, float y) { }

        public readonly record struct ImVec4(float x, float y, float z, float w) { }

        #region Context creation and access

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiContext* ImGui_CreateContext(ImFontAtlas* shared_font_atlas = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_DestroyContext(ImGuiContext* ctx = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiContext* ImGui_GetCurrentContext();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetCurrentContext(ImGuiContext* ctx);

        #endregion

        #region Main

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiIO* ImGui_GetIO();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiStyle* ImGui_GetStyle();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_NewFrame();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndFrame();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Render();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImDrawData* ImGui_GetDrawData();

        #endregion

        #region Demo, Debug, Information

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ShowDemoWindow(bool* p_open = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ShowMetricsWindow(bool* p_open = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ShowDebugLogWindow(bool* p_open = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ShowStackToolWindow(bool* p_open = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ShowAboutWindow(bool* p_open = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ShowStyleEditor(ImGuiStyle* @ref = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ShowStyleSelector(byte* label);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ShowFontSelector(byte* label);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ShowUserGuide();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* ImGui_GetVersion();

        #endregion

        #region Styles

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_StyleColorsDark(ImGuiStyle* dst = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_StyleColorsLight(ImGuiStyle* dst = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_StyleColorsClassic(ImGuiStyle* dst = default);

        #endregion

        #region Windows

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_Begin(byte* name, bool* p_open = default, ImGuiWindowFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_End();

        #endregion

        #region Child Windows

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginChild(byte* str_id, ImVec2 size = default, bool border = false, ImGuiWindowFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginChildID(ImGuiID id, ImVec2 size = default, bool border = false, ImGuiWindowFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndChild();

        #endregion

        #region Windows Utilities

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsWindowAppearing();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsWindowCollapsed();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsWindowFocused(ImGuiFocusedFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsWindowHovered(ImGuiHoveredFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImDrawList* ImGui_GetWindowDrawList();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetWindowPos();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetWindowSize();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetWindowWidth();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetWindowHeight();

        #endregion

        #region Window Manipulation

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextWindowPos(ImVec2 pos, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextWindowPosEx(ImVec2 pos, ImGuiCond cond = default, ImVec2 pivot = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextWindowSize(ImVec2 size, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextWindowSizeConstraints(ImVec2 size_min, ImVec2 size_max, ImGuiSizeCallback custom_callback = default, void* custom_callback_data = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextWindowContentSize(ImVec2 size);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextWindowCollapsed(bool collapsed, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextWindowFocus();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextWindowScroll(ImVec2 scroll);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextWindowBgAlpha(float alpha);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetWindowPos(ImVec2 pos, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetWindowSize(ImVec2 size, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetWindowCollapsed(bool collapsed, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetWindowFocus();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetWindowFontScale(float scale);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetWindowPosStr(byte* name, ImVec2 pos, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetWindowSizeStr(byte* name, ImVec2 size, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetWindowCollapsedStr(byte* name, bool collapsed, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetWindowFocusStr(byte* name);

        #endregion

        #region Content region

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetContentRegionAvail();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetContentRegionMax();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetWindowContentRegionMin();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetWindowContentRegionMax();

        #endregion

        #region Windows Scrolling

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetScrollX();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetScrollY();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetScrollX(float scroll_x);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetScrollY(float scroll_y);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetScrollMaxX();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetScrollMaxY();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetScrollHereX(float center_x_ratio = 0.5f);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetScrollHereY(float center_y_ratio = 0.5f);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetScrollFromPosX(float local_x, float center_x_ratio = 0.5f);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetScrollFromPosY(float local_y, float center_y_ratio = 0.5f);

        #endregion

        #region Parameters stacks (shared)

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushFont(ImFont* font);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopFont();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushStyleColor(ImGuiCol idx, uint col);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushStyleColorImVec4(ImGuiCol idx, ImVec4 col);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopStyleColor();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopStyleColorEx(int count = 1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushStyleVar(ImGuiStyleVar idx, float val);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushStyleVarImVec2(ImGuiStyleVar idx, ImVec2 val);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopStyleVar();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopStyleVarEx(int count = 1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushAllowKeyboardFocus(bool allow_keyboard_focus);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopAllowKeyboardFocus();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushButtonRepeat(bool repeat);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopButtonRepeat();

        #endregion

        #region Parameters stacks (current window)

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushItemWidth(float item_width);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopItemWidth();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextItemWidth(float item_width);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_CalcItemWidth();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushTextWrapPos(float wrap_local_pos_x = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopTextWrapPos();

        #endregion

        #region Style read access

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImFont* ImGui_GetFont();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetFontSize();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetFontTexUvWhitePixel();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ImGui_GetColorU32(ImGuiCol idx);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ImGui_GetColorU32Ex(ImGuiCol idx, float alpha_mul = 1.0f);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ImGui_GetColorU32ImVec4(ImVec4 col);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ImGui_GetColorU32uint(uint col);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec4* ImGui_GetStyleColorVec4(ImGuiCol idx);

        #endregion

        #region Cursor / Layout

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Separator();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SameLine();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SameLineEx(float offset_from_start_x = default, float spacing = -1.0f);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_NewLine();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Spacing();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Dummy(ImVec2 size);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Indent();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_IndentEx(float indent_w = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Unindent();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_UnindentEx(float indent_w = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_BeginGroup();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndGroup();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetCursorPos();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetCursorPosX();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetCursorPosY();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetCursorPos(ImVec2 local_pos);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetCursorPosX(float local_x);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetCursorPosY(float local_y);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetCursorStartPos();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetCursorScreenPos();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetCursorScreenPos(ImVec2 pos);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_AlignTextToFramePadding();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetTextLineHeight();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetTextLineHeightWithSpacing();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetFrameHeight();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetFrameHeightWithSpacing();

        #endregion

        #region ID stack/scopes

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushID(byte* str_id);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushIDStr(byte* str_id_begin, byte* str_id_end);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushIDPtr(void* ptr_id);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushIDInt(int int_id);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopID();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiID ImGui_GetID(byte* str_id);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiID ImGui_GetIDStr(byte* str_id_begin, byte* str_id_end);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiID ImGui_GetIDPtr(void* ptr_id);

        #endregion

        #region Widgets: Text

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TextUnformatted(byte* text);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TextUnformattedEx(byte* text, byte* text_end = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Text(byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TextV(byte* fmt, nuint /* va_list */ args);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TextColored(ImVec4 col, byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TextColoredV(ImVec4 col, byte* fmt, nuint /* va_list */ args);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TextDisabled(byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TextDisabledV(byte* fmt, nuint /* va_list */ args);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TextWrapped(byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TextWrappedV(byte* fmt, nuint /* va_list */ args);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LabelText(byte* label, byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LabelTextV(byte* label, byte* fmt, nuint /* va_list */ args);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_BulletText(byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_BulletTextV(byte* fmt, nuint /* va_list */ args);

        #endregion

        #region Widgets: Main

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_Button(byte* label);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ButtonEx(byte* label, ImVec2 size = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SmallButton(byte* label);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InvisibleButton(byte* str_id, ImVec2 size, ImGuiButtonFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ArrowButton(byte* str_id, ImGuiDir dir);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_Checkbox(byte* label, bool* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_CheckboxFlagsIntPtr(byte* label, int* flags, int flags_value);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_CheckboxFlagsUintPtr(byte* label, uint* flags, uint flags_value);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_RadioButton(byte* label, bool active);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_RadioButtonIntPtr(byte* label, int* v, int v_button);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ProgressBar(float fraction, ImVec2 size_arg /* = ImVec2(-FLT_MIN, 0) */, byte* overlay = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Bullet();


        #endregion

        #region Widgets: Images

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Image(ImTextureID user_texture_id, ImVec2 size);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ImageEx(ImTextureID user_texture_id, ImVec2 size, ImVec2 uv0 = default, ImVec2 uv1 = default /* = ImVec2(1, 1) */, ImVec4 tint_col = default /* = ImVec4(1, 1, 1, 1) */, ImVec4 border_col = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ImageButton(byte* str_id, ImTextureID user_texture_id, ImVec2 size);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ImageButtonEx(byte* str_id, ImTextureID user_texture_id, ImVec2 size, ImVec2 uv0 = default, ImVec2 uv1 = default /* = ImVec2(1, 1) */, ImVec4 bg_col = default, ImVec4 tint_col = default /* = ImVec4(1, 1, 1, 1) */);

        #endregion

        #region Widgets: Combo Box (Dropdown)

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginCombo(byte* label, byte* preview_value, ImGuiComboFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndCombo();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ComboChar(byte* label, int* current_item, byte** items, int items_count);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ComboCharEx(byte* label, int* current_item, byte** items, int items_count, int popup_max_height_in_items = -1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_Combo(byte* label, int* current_item, byte* items_separated_by_zeros);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ComboEx(byte* label, int* current_item, byte* items_separated_by_zeros, int popup_max_height_in_items = -1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ComboCallback(byte* label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte**, bool> items_getter, void* data, int items_count);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ComboCallbackEx(byte* label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte**, bool> items_getter, void* data, int items_count, int popup_max_height_in_items = -1);

        #endregion

        #region Widgets: Drag Sliders

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloat(byte* label, float* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloatEx(byte* label, float* v, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloat2(byte* label, float* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloat2Ex(byte* label, float* v, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloat3(byte* label, float* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloat3Ex(byte* label, float* v, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloat4(byte* label, float* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloat4Ex(byte* label, float* v, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloatRange2(byte* label, float* v_current_min, float* v_current_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragFloatRange2Ex(byte* label, float* v_current_min, float* v_current_max, float v_speed = 1.0f, float v_min = default, float v_max = default, byte* format = default /* = "%.3f" */, byte* format_max = default, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragInt(byte* label, int* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragIntEx(byte* label, int* v, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragInt2(byte* label, int* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragInt2Ex(byte* label, int* v, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragInt3(byte* label, int* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragInt3Ex(byte* label, int* v, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragInt4(byte* label, int* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragInt4Ex(byte* label, int* v, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragIntRange2(byte* label, int* v_current_min, int* v_current_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragIntRange2Ex(byte* label, int* v_current_min, int* v_current_max, float v_speed = 1.0f, int v_min = default, int v_max = default, byte* format = default /* = "%d" */, byte* format_max = default, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragScalar(byte* label, ImGuiDataType data_type, void* p_data);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragScalarEx(byte* label, ImGuiDataType data_type, void* p_data, float v_speed = 1.0f, void* p_min = default, void* p_max = default, byte* format = default, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DragScalarNEx(byte* label, ImGuiDataType data_type, void* p_data, int components, float v_speed = 1.0f, void* p_min = default, void* p_max = default, byte* format = default, ImGuiSliderFlags flags = default);

        #endregion

        #region Widgets: Regular Sliders

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderFloat(byte* label, float* v, float v_min, float v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderFloatEx(byte* label, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderFloat2(byte* label, float* v, float v_min, float v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderFloat2Ex(byte* label, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderFloat3(byte* label, float* v, float v_min, float v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderFloat3Ex(byte* label, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderFloat4(byte* label, float* v, float v_min, float v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderFloat4Ex(byte* label, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderAngle(byte* label, float* v_rad);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderAngleEx(byte* label, float* v_rad, float v_degrees_min /* = -360.0f */, float v_degrees_max /* = +360.0f */, byte* format /* = "%.0f deg" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderInt(byte* label, int* v, int v_min, int v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderIntEx(byte* label, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderInt2(byte* label, int* v, int v_min, int v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderInt2Ex(byte* label, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderInt3(byte* label, int* v, int v_min, int v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderInt3Ex(byte* label, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderInt4(byte* label, int* v, int v_min, int v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderInt4Ex(byte* label, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderScalar(byte* label, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderScalarEx(byte* label, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max, byte* format = default, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components, void* p_min, void* p_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SliderScalarNEx(byte* label, ImGuiDataType data_type, void* p_data, int components, void* p_min, void* p_max, byte* format = default, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_VSliderFloat(byte* label, ImVec2 size, float* v, float v_min, float v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_VSliderFloatEx(byte* label, ImVec2 size, float* v, float v_min, float v_max, byte* format = default /* = "%.3f" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_VSliderInt(byte* label, ImVec2 size, int* v, int v_min, int v_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_VSliderIntEx(byte* label, ImVec2 size, int* v, int v_min, int v_max, byte* format = default /* = "%d" */, ImGuiSliderFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_VSliderScalar(byte* label, ImVec2 size, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_VSliderScalarEx(byte* label, ImVec2 size, ImGuiDataType data_type, void* p_data, void* p_min, void* p_max, byte* format = default, ImGuiSliderFlags flags = default);

        #endregion

        #region Widgets: Input with Keyboard

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputText(byte* label, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputTextEx(byte* label, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default, ImGuiInputTextCallback callback = default, void* user_data = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputTextMultiline(byte* label, char* buf, nuint buf_size);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputTextMultilineEx(byte* label, char* buf, nuint buf_size, ImVec2 size = default, ImGuiInputTextFlags flags = default, ImGuiInputTextCallback callback = default, void* user_data = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputTextWithHint(byte* label, byte* hint, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputTextWithHintEx(byte* label, byte* hint, char* buf, nuint buf_size, ImGuiInputTextFlags flags = default, ImGuiInputTextCallback callback = default, void* user_data = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputFloat(byte* label, float* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputFloatEx(byte* label, float* v, float step = default, float step_fast = default, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputFloat2(byte* label, float* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputFloat2Ex(byte* label, float* v, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputFloat3(byte* label, float* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputFloat3Ex(byte* label, float* v, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputFloat4(byte* label, float* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputFloat4Ex(byte* label, float* v, byte* format = default /* = "%.3f" */, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputInt(byte* label, int* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputIntEx(byte* label, int* v, int step = 1, int step_fast = 100, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputInt2(byte* label, int* v, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputInt3(byte* label, int* v, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputInt4(byte* label, int* v, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputDouble(byte* label, double* v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputDoubleEx(byte* label, double* v, double step /* = 0.0 */, double step_fast /* = 0.0 */, byte* format /* = "%.6f" */, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputScalar(byte* label, ImGuiDataType data_type, void* p_data);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputScalarEx(byte* label, ImGuiDataType data_type, void* p_data, void* p_step = default, void* p_step_fast = default, byte* format = default, ImGuiInputTextFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputScalarN(byte* label, ImGuiDataType data_type, void* p_data, int components);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_InputScalarNEx(byte* label, ImGuiDataType data_type, void* p_data, int components, void* p_step = default, void* p_step_fast = default, byte* format = default, ImGuiInputTextFlags flags = default);

        #endregion

        #region Widgets: Color Editor/Picker

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ColorEdit3(byte* label, float* col, ImGuiColorEditFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ColorEdit4(byte* label, float* col, ImGuiColorEditFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ColorPicker3(byte* label, float* col, ImGuiColorEditFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ColorPicker4(byte* label, float* col, ImGuiColorEditFlags flags = default, float* ref_col = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ColorButton(byte* desc_id, ImVec4 col, ImGuiColorEditFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ColorButtonEx(byte* desc_id, ImVec4 col, ImGuiColorEditFlags flags = default, ImVec2 size = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetColorEditOptions(ImGuiColorEditFlags flags);

        #endregion

        #region Widgets: Trees

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNode(byte* label);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNodeStr(byte* str_id, byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNodePtr(void* ptr_id, byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNodeV(byte* str_id, byte* fmt, nuint /* va_list */ args);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNodeVPtr(void* ptr_id, byte* fmt, nuint /* va_list */ args);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNodeEx(byte* label, ImGuiTreeNodeFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNodeExStr(byte* str_id, ImGuiTreeNodeFlags flags, byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNodeExPtr(void* ptr_id, ImGuiTreeNodeFlags flags, byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNodeExV(byte* str_id, ImGuiTreeNodeFlags flags, byte* fmt, nuint /* va_list */ args);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TreeNodeExVPtr(void* ptr_id, ImGuiTreeNodeFlags flags, byte* fmt, nuint /* va_list */ args);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TreePush(byte* str_id);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TreePushPtr(void* ptr_id);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TreePop();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetTreeNodeToLabelSpacing();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_CollapsingHeader(byte* label, ImGuiTreeNodeFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_CollapsingHeaderBoolPtr(byte* label, bool* p_visible, ImGuiTreeNodeFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextItemOpen(bool is_open, ImGuiCond cond = default);

        #endregion

        #region Widgets: Selectables

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_Selectable(byte* label);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SelectableEx(byte* label, bool selected = false, ImGuiSelectableFlags flags = default, ImVec2 size = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SelectableBoolPtr(byte* label, bool* p_selected, ImGuiSelectableFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SelectableBoolPtrEx(byte* label, bool* p_selected, ImGuiSelectableFlags flags = default, ImVec2 size = default);

        #endregion

        #region Widgets: List Boxes

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginListBox(byte* label, ImVec2 size = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndListBox();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ListBox(byte* label, int* current_item, byte** items, int items_count, int height_in_items = -1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ListBoxCallback(byte* label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte**, bool> items_getter, void* data, int items_count);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_ListBoxCallbackEx(byte* label, int* current_item, delegate* unmanaged[Cdecl]<void*, int, byte**, bool> items_getter, void* data, int items_count, int height_in_items = -1);

        #endregion

        #region Widgets: Data Plotting

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotLines(byte* label, float* values, int values_count);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotLinesEx(byte* label, float* values, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default, int stride = sizeof(float));

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotLinesCallback(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotLinesCallbackEx(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotHistogram(byte* label, float* values, int values_count);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotHistogramEx(byte* label, float* values, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default, int stride = sizeof(float));

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotHistogramCallback(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PlotHistogramCallbackEx(byte* label, delegate* unmanaged[Cdecl]<void*, int, float> values_getter, void* data, int values_count, int values_offset = default, byte* overlay_text = default, float scale_min = float.MaxValue, float scale_max = float.MaxValue, ImVec2 graph_size = default);

        #endregion

        #region Widgets: Menus

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginMenuBar();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndMenuBar();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginMainMenuBar();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndMainMenuBar();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginMenu(byte* label);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginMenuEx(byte* label, bool enabled = true);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndMenu();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_MenuItem(byte* label);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_MenuItemEx(byte* label, byte* shortcut = default, bool selected = false, bool enabled = true);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_MenuItemBoolPtr(byte* label, byte* shortcut, bool* p_selected, bool enabled = true);

        #endregion

        #region Tooltips

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_BeginTooltip();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndTooltip();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetTooltip(byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetTooltipV(byte* fmt, nuint /* va_list */ args);

        #endregion

        #region Popups: begin/end functions

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginPopup(byte* str_id, ImGuiWindowFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginPopupModal(byte* name, bool* p_open = default, ImGuiWindowFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndPopup();

        #endregion

        #region Popups: open/close functions

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_OpenPopup(byte* str_id, ImGuiPopupFlags popup_flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_OpenPopupID(ImGuiID id, ImGuiPopupFlags popup_flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_OpenPopupOnItemClick(byte* str_id = default, ImGuiPopupFlags popup_flags = (ImGuiPopupFlags)1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_CloseCurrentPopup();

        #endregion

        #region Popups: open+begin combined functions helpers

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginPopupContextItem();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginPopupContextItemEx(byte* str_id = default, ImGuiPopupFlags popup_flags = (ImGuiPopupFlags)1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginPopupContextWindow();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginPopupContextWindowEx(byte* str_id = default, ImGuiPopupFlags popup_flags = (ImGuiPopupFlags)1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginPopupContextVoid();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginPopupContextVoidEx(byte* str_id = default, ImGuiPopupFlags popup_flags = (ImGuiPopupFlags)1);

        #endregion

        #region Popups: query functions

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsPopupOpen(byte* str_id, ImGuiPopupFlags flags = default);

        #endregion

        #region Tables

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginTable(byte* str_id, int column, ImGuiTableFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginTableEx(byte* str_id, int column, ImGuiTableFlags flags = default, ImVec2 outer_size = default, float inner_width = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndTable();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TableNextRow();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TableNextRowEx(ImGuiTableRowFlags row_flags = default, float min_row_height = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TableNextColumn();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TableSetColumnIndex(int column_n);

        #endregion

        #region Tables: Headers & Columns declaration

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TableSetupColumn(byte* label, ImGuiTableColumnFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TableSetupColumnEx(byte* label, ImGuiTableColumnFlags flags = default, float init_width_or_weight = default, ImGuiID user_id = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TableSetupScrollFreeze(int cols, int rows);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TableHeadersRow();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TableHeader(byte* label);

        #endregion

        #region Tables: Sorting & Miscellaneous functions

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiTableSortSpecs* ImGui_TableGetSortSpecs();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGui_TableGetColumnCount();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGui_TableGetColumnIndex();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGui_TableGetRowIndex();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* ImGui_TableGetColumnName(int column_n = -1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiTableColumnFlags ImGui_TableGetColumnFlags(int column_n = -1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TableSetColumnEnabled(int column_n, bool v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_TableSetBgColor(ImGuiTableBgTarget target, uint color, int column_n = -1);

        #endregion

        #region Legacy Columns API (prefer using Tables!)

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_Columns();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ColumnsEx(int count = 1, byte* id = default, bool border = true);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_NextColumn();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGui_GetColumnIndex();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetColumnWidth(int column_index = -1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetColumnWidth(int column_index, float width);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern float ImGui_GetColumnOffset(int column_index = -1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetColumnOffset(int column_index, float offset_x);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGui_GetColumnsCount();

        #endregion

        #region Tab Bars, Tabs

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginTabBar(byte* str_id, ImGuiTabBarFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndTabBar();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginTabItem(byte* label, bool* p_open = default, ImGuiTabItemFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndTabItem();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_TabItemButton(byte* label, ImGuiTabItemFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetTabItemClosed(byte* tab_or_docked_window_label);

        #endregion

        #region Logging/Capture

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LogToTTY(int auto_open_depth = -1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LogToFile(int auto_open_depth = -1, byte* filename = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LogToClipboard(int auto_open_depth = -1);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LogFinish();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LogButtons();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LogText(byte* fmt, __arglist);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LogTextV(byte* fmt, nuint /* va_list */ args);

        #endregion

        #region Drag and Drop

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginDragDropSource(ImGuiDragDropFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_SetDragDropPayload(byte* type, void* data, nuint sz, ImGuiCond cond = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndDragDropSource();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginDragDropTarget();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiPayload* ImGui_AcceptDragDropPayload(byte* type, ImGuiDragDropFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndDragDropTarget();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiPayload* ImGui_GetDragDropPayload();

        #endregion

        #region Disabling [BETA API]

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_BeginDisabled(bool disabled = true);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndDisabled();

        #endregion

        #region Clipping

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PushClipRect(ImVec2 clip_rect_min, ImVec2 clip_rect_max, bool intersect_with_current_clip_rect);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_PopClipRect();

        #endregion

        #region Focus, Activation

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetItemDefaultFocus();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetKeyboardFocusHere();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetKeyboardFocusHereEx(int offset = default);

        #endregion

        #region Item/Widgets Utilities and Query Functions

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemHovered(ImGuiHoveredFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemActive();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemFocused();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemClicked();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemClickedEx(ImGuiMouseButton mouse_button = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemVisible();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemEdited();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemActivated();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemDeactivated();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemDeactivatedAfterEdit();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsItemToggledOpen();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsAnyItemHovered();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsAnyItemActive();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsAnyItemFocused();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiID ImGui_GetItemID();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetItemRectMin();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetItemRectMax();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetItemRectSize();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetItemAllowOverlap();

        #endregion

        #region Viewports

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiViewport* ImGui_GetMainViewport();

        #endregion

        #region Background/Foreground Draw Lists

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImDrawList* ImGui_GetBackgroundDrawList();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImDrawList* ImGui_GetForegroundDrawList();

        #endregion

        #region Miscellaneous Utilities

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsRectVisibleBySize(ImVec2 size);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsRectVisible(ImVec2 rect_min, ImVec2 rect_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern double ImGui_GetTime();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGui_GetFrameCount();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImDrawListSharedData* ImGui_GetDrawListSharedData();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* ImGui_GetStyleColorName(ImGuiCol idx);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetStateStorage(ImGuiStorage* storage);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiStorage* ImGui_GetStateStorage();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_BeginChildFrame(ImGuiID id, ImVec2 size, ImGuiWindowFlags flags = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_EndChildFrame();

        #endregion

        #region Text Utilities

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_CalcTextSize(byte* text);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_CalcTextSizeEx(byte* text, byte* text_end = default, bool hide_text_after_double_hash = false, float wrap_width = -1.0f);

        #endregion

        #region Color Utilities

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec4 ImGui_ColorConvertU32ToFloat4(uint @in);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint ImGui_ColorConvertFloat4ToU32(ImVec4 @in);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ColorConvertRGBtoHSV(float r, float g, float b, float* out_h, float* out_s, float* out_v);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ColorConvertHSVtoRGB(float h, float s, float v, float* out_r, float* out_g, float* out_b);

        #endregion

        #region Inputs Utilities: Keyboard/Mouse/Gamepad

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsKeyDown(ImGuiKey key);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsKeyPressed(ImGuiKey key);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsKeyPressedEx(ImGuiKey key, bool repeat = true);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsKeyReleased(ImGuiKey key);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGui_GetKeyPressedAmount(ImGuiKey key, float repeat_delay, float rate);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* ImGui_GetKeyName(ImGuiKey key);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextFrameWantCaptureKeyboard(bool want_capture_keyboard);

        #endregion

        #region Inputs Utilities: Mouse specific

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsMouseDown(ImGuiMouseButton button);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsMouseClicked(ImGuiMouseButton button);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsMouseClickedEx(ImGuiMouseButton button, bool repeat = false);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsMouseReleased(ImGuiMouseButton button);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsMouseDoubleClicked(ImGuiMouseButton button);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ImGui_GetMouseClickedCount(ImGuiMouseButton button);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsMouseHoveringRect(ImVec2 r_min, ImVec2 r_max);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsMouseHoveringRectEx(ImVec2 r_min, ImVec2 r_max, bool clip = true);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsMousePosValid(ImVec2* mouse_pos = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsAnyMouseDown();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetMousePos();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetMousePosOnOpeningCurrentPopup();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_IsMouseDragging(ImGuiMouseButton button, float lock_threshold = -1.0f);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImVec2 ImGui_GetMouseDragDelta(ImGuiMouseButton button = default, float lock_threshold = -1.0f);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ResetMouseDragDelta();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_ResetMouseDragDeltaEx(ImGuiMouseButton button = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern ImGuiMouseCursor ImGui_GetMouseCursor();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetMouseCursor(ImGuiMouseCursor cursor_type);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetNextFrameWantCaptureMouse(bool want_capture_mouse);

        #endregion

        #region Clipboard Utilities

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* ImGui_GetClipboardText();

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetClipboardText(byte* text);
        #endregion

        #region Settings/.Ini Utilities

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LoadIniSettingsFromDisk(byte* ini_filename);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_LoadIniSettingsFromMemory(byte* ini_data, nuint ini_size = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SaveIniSettingsToDisk(byte* ini_filename);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern byte* ImGui_SaveIniSettingsToMemory(nuint* out_ini_size = default);

        #endregion

        #region Debug Utilities

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_DebugTextEncoding(byte* text);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ImGui_DebugCheckVersionAndDataLayout(byte* version_str, nuint sz_io, nuint sz_style, nuint sz_vec2, nuint sz_vec4, nuint sz_drawvert, nuint sz_drawidx);

        #endregion

        #region Memory Allocators

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_SetAllocatorFunctions(ImGuiMemAllocFunc alloc_func, ImGuiMemFreeFunc free_func, void* user_data = default);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_GetAllocatorFunctions(ImGuiMemAllocFunc* p_alloc_func, ImGuiMemFreeFunc* p_free_func, void** p_user_data);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void* ImGui_MemAlloc(nuint size);

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGui_MemFree(void* ptr);

        #endregion

        #region Enumerated types

        public enum ImGuiWindowFlags
        {
            ImGuiWindowFlags_None = 0,
            ImGuiWindowFlags_NoTitleBar = 1 << 0,
            ImGuiWindowFlags_NoResize = 1 << 1,
            ImGuiWindowFlags_NoMove = 1 << 2,
            ImGuiWindowFlags_NoScrollbar = 1 << 3,
            ImGuiWindowFlags_NoScrollWithMouse = 1 << 4,
            ImGuiWindowFlags_NoCollapse = 1 << 5,
            ImGuiWindowFlags_AlwaysAutoResize = 1 << 6,
            ImGuiWindowFlags_NoBackground = 1 << 7,
            ImGuiWindowFlags_NoSavedSettings = 1 << 8,
            ImGuiWindowFlags_NoMouseInputs = 1 << 9,
            ImGuiWindowFlags_MenuBar = 1 << 10,
            ImGuiWindowFlags_HorizontalScrollbar = 1 << 11,
            ImGuiWindowFlags_NoFocusOnAppearing = 1 << 12,
            ImGuiWindowFlags_NoBringToFrontOnFocus = 1 << 13,
            ImGuiWindowFlags_AlwaysVerticalScrollbar = 1 << 14,
            ImGuiWindowFlags_AlwaysHorizontalScrollbar = 1 << 15,
            ImGuiWindowFlags_AlwaysUseWindowPadding = 1 << 16,
            ImGuiWindowFlags_NoNavInputs = 1 << 18,
            ImGuiWindowFlags_NoNavFocus = 1 << 19,
            ImGuiWindowFlags_UnsavedDocument = 1 << 20,
            ImGuiWindowFlags_NoNav = ImGuiWindowFlags_NoNavInputs | ImGuiWindowFlags_NoNavFocus,
            ImGuiWindowFlags_NoDecoration = ImGuiWindowFlags_NoTitleBar | ImGuiWindowFlags_NoResize | ImGuiWindowFlags_NoScrollbar | ImGuiWindowFlags_NoCollapse,
            ImGuiWindowFlags_NoInputs = ImGuiWindowFlags_NoMouseInputs | ImGuiWindowFlags_NoNavInputs | ImGuiWindowFlags_NoNavFocus,

            ImGuiWindowFlags_NavFlattened = 1 << 23,
            ImGuiWindowFlags_ChildWindow = 1 << 24,
            ImGuiWindowFlags_Tooltip = 1 << 25,
            ImGuiWindowFlags_Popup = 1 << 26,
            ImGuiWindowFlags_Modal = 1 << 27,
            ImGuiWindowFlags_ChildMenu = 1 << 28,
        }

        public enum ImGuiInputTextFlags
        {
            ImGuiInputTextFlags_None = 0,
            ImGuiInputTextFlags_CharsDecimal = 1 << 0,
            ImGuiInputTextFlags_CharsHexadecimal = 1 << 1,
            ImGuiInputTextFlags_CharsUppercase = 1 << 2,
            ImGuiInputTextFlags_CharsNoBlank = 1 << 3,
            ImGuiInputTextFlags_AutoSelectAll = 1 << 4,
            ImGuiInputTextFlags_EnterReturnsTrue = 1 << 5,
            ImGuiInputTextFlags_CallbackCompletion = 1 << 6,
            ImGuiInputTextFlags_CallbackHistory = 1 << 7,
            ImGuiInputTextFlags_CallbackAlways = 1 << 8,
            ImGuiInputTextFlags_CallbackCharFilter = 1 << 9,
            ImGuiInputTextFlags_AllowTabInput = 1 << 10,
            ImGuiInputTextFlags_CtrlEnterForNewLine = 1 << 11,
            ImGuiInputTextFlags_NoHorizontalScroll = 1 << 12,
            ImGuiInputTextFlags_AlwaysOverwrite = 1 << 13,
            ImGuiInputTextFlags_ReadOnly = 1 << 14,
            ImGuiInputTextFlags_Password = 1 << 15,
            ImGuiInputTextFlags_NoUndoRedo = 1 << 16,
            ImGuiInputTextFlags_CharsScientific = 1 << 17,
            ImGuiInputTextFlags_CallbackResize = 1 << 18,
            ImGuiInputTextFlags_CallbackEdit = 1 << 19,
            ImGuiInputTextFlags_EscapeClearsAll = 1 << 20,

        }

        public enum ImGuiTreeNodeFlags
        {
            ImGuiTreeNodeFlags_None = 0,
            ImGuiTreeNodeFlags_Selected = 1 << 0,
            ImGuiTreeNodeFlags_Framed = 1 << 1,
            ImGuiTreeNodeFlags_AllowItemOverlap = 1 << 2,
            ImGuiTreeNodeFlags_NoTreePushOnOpen = 1 << 3,
            ImGuiTreeNodeFlags_NoAutoOpenOnLog = 1 << 4,
            ImGuiTreeNodeFlags_DefaultOpen = 1 << 5,
            ImGuiTreeNodeFlags_OpenOnDoubleClick = 1 << 6,
            ImGuiTreeNodeFlags_OpenOnArrow = 1 << 7,
            ImGuiTreeNodeFlags_Leaf = 1 << 8,
            ImGuiTreeNodeFlags_Bullet = 1 << 9,
            ImGuiTreeNodeFlags_FramePadding = 1 << 10,
            ImGuiTreeNodeFlags_SpanAvailWidth = 1 << 11,
            ImGuiTreeNodeFlags_SpanFullWidth = 1 << 12,
            ImGuiTreeNodeFlags_NavLeftJumpsBackHere = 1 << 13,

            ImGuiTreeNodeFlags_CollapsingHeader = ImGuiTreeNodeFlags_Framed | ImGuiTreeNodeFlags_NoTreePushOnOpen | ImGuiTreeNodeFlags_NoAutoOpenOnLog,
        }

        public enum ImGuiPopupFlags
        {
            ImGuiPopupFlags_None = 0,
            ImGuiPopupFlags_MouseButtonLeft = 0,
            ImGuiPopupFlags_MouseButtonRight = 1,
            ImGuiPopupFlags_MouseButtonMiddle = 2,
            ImGuiPopupFlags_MouseButtonMask_ = 0x1F,
            ImGuiPopupFlags_MouseButtonDefault_ = 1,
            ImGuiPopupFlags_NoOpenOverExistingPopup = 1 << 5,
            ImGuiPopupFlags_NoOpenOverItems = 1 << 6,
            ImGuiPopupFlags_AnyPopupId = 1 << 7,
            ImGuiPopupFlags_AnyPopupLevel = 1 << 8,
            ImGuiPopupFlags_AnyPopup = ImGuiPopupFlags_AnyPopupId | ImGuiPopupFlags_AnyPopupLevel,
        }

        public enum ImGuiSelectableFlags
        {
            ImGuiSelectableFlags_None = 0,
            ImGuiSelectableFlags_DontClosePopups = 1 << 0,
            ImGuiSelectableFlags_SpanAllColumns = 1 << 1,
            ImGuiSelectableFlags_AllowDoubleClick = 1 << 2,
            ImGuiSelectableFlags_Disabled = 1 << 3,
            ImGuiSelectableFlags_AllowItemOverlap = 1 << 4,
        }

        public enum ImGuiComboFlags
        {
            ImGuiComboFlags_None = 0,
            ImGuiComboFlags_PopupAlignLeft = 1 << 0,
            ImGuiComboFlags_HeightSmall = 1 << 1,
            ImGuiComboFlags_HeightRegular = 1 << 2,
            ImGuiComboFlags_HeightLarge = 1 << 3,
            ImGuiComboFlags_HeightLargest = 1 << 4,
            ImGuiComboFlags_NoArrowButton = 1 << 5,
            ImGuiComboFlags_NoPreview = 1 << 6,
            ImGuiComboFlags_HeightMask_ = ImGuiComboFlags_HeightSmall | ImGuiComboFlags_HeightRegular | ImGuiComboFlags_HeightLarge | ImGuiComboFlags_HeightLargest,
        }

        public enum ImGuiTabBarFlags
        {
            ImGuiTabBarFlags_None = 0,
            ImGuiTabBarFlags_Reorderable = 1 << 0,
            ImGuiTabBarFlags_AutoSelectNewTabs = 1 << 1,
            ImGuiTabBarFlags_TabListPopupButton = 1 << 2,
            ImGuiTabBarFlags_NoCloseWithMiddleMouseButton = 1 << 3,
            ImGuiTabBarFlags_NoTabListScrollingButtons = 1 << 4,
            ImGuiTabBarFlags_NoTooltip = 1 << 5,
            ImGuiTabBarFlags_FittingPolicyResizeDown = 1 << 6,
            ImGuiTabBarFlags_FittingPolicyScroll = 1 << 7,
            ImGuiTabBarFlags_FittingPolicyMask_ = ImGuiTabBarFlags_FittingPolicyResizeDown | ImGuiTabBarFlags_FittingPolicyScroll,
            ImGuiTabBarFlags_FittingPolicyDefault_ = ImGuiTabBarFlags_FittingPolicyResizeDown,
        }

        public enum ImGuiTabItemFlags
        {
            ImGuiTabItemFlags_None = 0,
            ImGuiTabItemFlags_UnsavedDocument = 1 << 0,
            ImGuiTabItemFlags_SetSelected = 1 << 1,
            ImGuiTabItemFlags_NoCloseWithMiddleMouseButton = 1 << 2,
            ImGuiTabItemFlags_NoPushId = 1 << 3,
            ImGuiTabItemFlags_NoTooltip = 1 << 4,
            ImGuiTabItemFlags_NoReorder = 1 << 5,
            ImGuiTabItemFlags_Leading = 1 << 6,
            ImGuiTabItemFlags_Trailing = 1 << 7,
        }

        public enum ImGuiTableFlags
        {

            ImGuiTableFlags_None = 0,
            ImGuiTableFlags_Resizable = 1 << 0,
            ImGuiTableFlags_Reorderable = 1 << 1,
            ImGuiTableFlags_Hideable = 1 << 2,
            ImGuiTableFlags_Sortable = 1 << 3,
            ImGuiTableFlags_NoSavedSettings = 1 << 4,
            ImGuiTableFlags_ContextMenuInBody = 1 << 5,

            ImGuiTableFlags_RowBg = 1 << 6,
            ImGuiTableFlags_BordersInnerH = 1 << 7,
            ImGuiTableFlags_BordersOuterH = 1 << 8,
            ImGuiTableFlags_BordersInnerV = 1 << 9,
            ImGuiTableFlags_BordersOuterV = 1 << 10,
            ImGuiTableFlags_BordersH = ImGuiTableFlags_BordersInnerH | ImGuiTableFlags_BordersOuterH,
            ImGuiTableFlags_BordersV = ImGuiTableFlags_BordersInnerV | ImGuiTableFlags_BordersOuterV,
            ImGuiTableFlags_BordersInner = ImGuiTableFlags_BordersInnerV | ImGuiTableFlags_BordersInnerH,
            ImGuiTableFlags_BordersOuter = ImGuiTableFlags_BordersOuterV | ImGuiTableFlags_BordersOuterH,
            ImGuiTableFlags_Borders = ImGuiTableFlags_BordersInner | ImGuiTableFlags_BordersOuter,
            ImGuiTableFlags_NoBordersInBody = 1 << 11,
            ImGuiTableFlags_NoBordersInBodyUntilResize = 1 << 12,

            ImGuiTableFlags_SizingFixedFit = 1 << 13,
            ImGuiTableFlags_SizingFixedSame = 2 << 13,
            ImGuiTableFlags_SizingStretchProp = 3 << 13,
            ImGuiTableFlags_SizingStretchSame = 4 << 13,

            ImGuiTableFlags_NoHostExtendX = 1 << 16,
            ImGuiTableFlags_NoHostExtendY = 1 << 17,
            ImGuiTableFlags_NoKeepColumnsVisible = 1 << 18,
            ImGuiTableFlags_PreciseWidths = 1 << 19,

            ImGuiTableFlags_NoClip = 1 << 20,

            ImGuiTableFlags_PadOuterX = 1 << 21,
            ImGuiTableFlags_NoPadOuterX = 1 << 22,
            ImGuiTableFlags_NoPadInnerX = 1 << 23,

            ImGuiTableFlags_ScrollX = 1 << 24,
            ImGuiTableFlags_ScrollY = 1 << 25,

            ImGuiTableFlags_SortMulti = 1 << 26,
            ImGuiTableFlags_SortTristate = 1 << 27,


            ImGuiTableFlags_SizingMask_ = ImGuiTableFlags_SizingFixedFit | ImGuiTableFlags_SizingFixedSame | ImGuiTableFlags_SizingStretchProp | ImGuiTableFlags_SizingStretchSame,
        }

        public enum ImGuiTableColumnFlags
        {
            ImGuiTableColumnFlags_None = 0,
            ImGuiTableColumnFlags_Disabled = 1 << 0,
            ImGuiTableColumnFlags_DefaultHide = 1 << 1,
            ImGuiTableColumnFlags_DefaultSort = 1 << 2,
            ImGuiTableColumnFlags_WidthStretch = 1 << 3,
            ImGuiTableColumnFlags_WidthFixed = 1 << 4,
            ImGuiTableColumnFlags_NoResize = 1 << 5,
            ImGuiTableColumnFlags_NoReorder = 1 << 6,
            ImGuiTableColumnFlags_NoHide = 1 << 7,
            ImGuiTableColumnFlags_NoClip = 1 << 8,
            ImGuiTableColumnFlags_NoSort = 1 << 9,
            ImGuiTableColumnFlags_NoSortAscending = 1 << 10,
            ImGuiTableColumnFlags_NoSortDescending = 1 << 11,
            ImGuiTableColumnFlags_NoHeaderLabel = 1 << 12,
            ImGuiTableColumnFlags_NoHeaderWidth = 1 << 13,
            ImGuiTableColumnFlags_PreferSortAscending = 1 << 14,
            ImGuiTableColumnFlags_PreferSortDescending = 1 << 15,
            ImGuiTableColumnFlags_IndentEnable = 1 << 16,
            ImGuiTableColumnFlags_IndentDisable = 1 << 17,


            ImGuiTableColumnFlags_IsEnabled = 1 << 24,
            ImGuiTableColumnFlags_IsVisible = 1 << 25,
            ImGuiTableColumnFlags_IsSorted = 1 << 26,
            ImGuiTableColumnFlags_IsHovered = 1 << 27,


            ImGuiTableColumnFlags_WidthMask_ = ImGuiTableColumnFlags_WidthStretch | ImGuiTableColumnFlags_WidthFixed,
            ImGuiTableColumnFlags_IndentMask_ = ImGuiTableColumnFlags_IndentEnable | ImGuiTableColumnFlags_IndentDisable,
            ImGuiTableColumnFlags_StatusMask_ = ImGuiTableColumnFlags_IsEnabled | ImGuiTableColumnFlags_IsVisible | ImGuiTableColumnFlags_IsSorted | ImGuiTableColumnFlags_IsHovered,
            ImGuiTableColumnFlags_NoDirectResize_ = 1 << 30,
        }

        public enum ImGuiTableRowFlags
        {
            ImGuiTableRowFlags_None = 0,
            ImGuiTableRowFlags_Headers = 1 << 0,
        }

        public enum ImGuiTableBgTarget
        {
            ImGuiTableBgTarget_None = 0,
            ImGuiTableBgTarget_RowBg0 = 1,
            ImGuiTableBgTarget_RowBg1 = 2,
            ImGuiTableBgTarget_CellBg = 3,
        }

        public enum ImGuiFocusedFlags
        {
            ImGuiFocusedFlags_None = 0,
            ImGuiFocusedFlags_ChildWindows = 1 << 0,
            ImGuiFocusedFlags_RootWindow = 1 << 1,
            ImGuiFocusedFlags_AnyWindow = 1 << 2,
            ImGuiFocusedFlags_NoPopupHierarchy = 1 << 3,

            ImGuiFocusedFlags_RootAndChildWindows = ImGuiFocusedFlags_RootWindow | ImGuiFocusedFlags_ChildWindows,
        }

        public enum ImGuiHoveredFlags
        {
            ImGuiHoveredFlags_None = 0,
            ImGuiHoveredFlags_ChildWindows = 1 << 0,
            ImGuiHoveredFlags_RootWindow = 1 << 1,
            ImGuiHoveredFlags_AnyWindow = 1 << 2,
            ImGuiHoveredFlags_NoPopupHierarchy = 1 << 3,

            ImGuiHoveredFlags_AllowWhenBlockedByPopup = 1 << 5,

            ImGuiHoveredFlags_AllowWhenBlockedByActiveItem = 1 << 7,
            ImGuiHoveredFlags_AllowWhenOverlapped = 1 << 8,
            ImGuiHoveredFlags_AllowWhenDisabled = 1 << 9,
            ImGuiHoveredFlags_NoNavOverride = 1 << 10,
            ImGuiHoveredFlags_RectOnly = ImGuiHoveredFlags_AllowWhenBlockedByPopup | ImGuiHoveredFlags_AllowWhenBlockedByActiveItem | ImGuiHoveredFlags_AllowWhenOverlapped,
            ImGuiHoveredFlags_RootAndChildWindows = ImGuiHoveredFlags_RootWindow | ImGuiHoveredFlags_ChildWindows,


            ImGuiHoveredFlags_DelayNormal = 1 << 11,
            ImGuiHoveredFlags_DelayShort = 1 << 12,
            ImGuiHoveredFlags_NoSharedDelay = 1 << 13,
        }

        public enum ImGuiDragDropFlags
        {
            ImGuiDragDropFlags_None = 0,

            ImGuiDragDropFlags_SourceNoPreviewTooltip = 1 << 0,
            ImGuiDragDropFlags_SourceNoDisableHover = 1 << 1,
            ImGuiDragDropFlags_SourceNoHoldToOpenOthers = 1 << 2,
            ImGuiDragDropFlags_SourceAllowNullID = 1 << 3,
            ImGuiDragDropFlags_SourceExtern = 1 << 4,
            ImGuiDragDropFlags_SourceAutoExpirePayload = 1 << 5,

            ImGuiDragDropFlags_AcceptBeforeDelivery = 1 << 10,
            ImGuiDragDropFlags_AcceptNoDrawDefaultRect = 1 << 11,
            ImGuiDragDropFlags_AcceptNoPreviewTooltip = 1 << 12,
            ImGuiDragDropFlags_AcceptPeekOnly = ImGuiDragDropFlags_AcceptBeforeDelivery | ImGuiDragDropFlags_AcceptNoDrawDefaultRect,
        }

        public const string IMGUI_PAYLOAD_TYPE_COLOR_3F = "_COL3F";
        public const string IMGUI_PAYLOAD_TYPE_COLOR_4F = "_COL4F";

        public enum ImGuiDataType
        {
            ImGuiDataType_S8,
            ImGuiDataType_U8,
            ImGuiDataType_S16,
            ImGuiDataType_U16,
            ImGuiDataType_S32,
            ImGuiDataType_U32,
            ImGuiDataType_S64,
            ImGuiDataType_U64,
            ImGuiDataType_Float,
            ImGuiDataType_Double,
            ImGuiDataType_COUNT,
        }

        public enum ImGuiDir
        {
            ImGuiDir_None = -1,
            ImGuiDir_Left = 0,
            ImGuiDir_Right = 1,
            ImGuiDir_Up = 2,
            ImGuiDir_Down = 3,
            ImGuiDir_COUNT,
        }

        public enum ImGuiSortDirection
        {
            ImGuiSortDirection_None = 0,
            ImGuiSortDirection_Ascending = 1,
            ImGuiSortDirection_Descending = 2,
        }

        public enum ImGuiKey
        {
            ImGuiKey_None = 0,
            ImGuiKey_Tab = 512,
            ImGuiKey_LeftArrow,
            ImGuiKey_RightArrow,
            ImGuiKey_UpArrow,
            ImGuiKey_DownArrow,
            ImGuiKey_PageUp,
            ImGuiKey_PageDown,
            ImGuiKey_Home,
            ImGuiKey_End,
            ImGuiKey_Insert,
            ImGuiKey_Delete,
            ImGuiKey_Backspace,
            ImGuiKey_Space,
            ImGuiKey_Enter,
            ImGuiKey_Escape,
            ImGuiKey_LeftCtrl,
            ImGuiKey_LeftShift,
            ImGuiKey_LeftAlt,
            ImGuiKey_LeftSuper,
            ImGuiKey_RightCtrl,
            ImGuiKey_RightShift,
            ImGuiKey_RightAlt,
            ImGuiKey_RightSuper,
            ImGuiKey_Menu,
            ImGuiKey_0,
            ImGuiKey_1,
            ImGuiKey_2,
            ImGuiKey_3,
            ImGuiKey_4,
            ImGuiKey_5,
            ImGuiKey_6,
            ImGuiKey_7,
            ImGuiKey_8,
            ImGuiKey_9,
            ImGuiKey_A,
            ImGuiKey_B,
            ImGuiKey_C,
            ImGuiKey_D,
            ImGuiKey_E,
            ImGuiKey_F,
            ImGuiKey_G,
            ImGuiKey_H,
            ImGuiKey_I,
            ImGuiKey_J,
            ImGuiKey_K,
            ImGuiKey_L,
            ImGuiKey_M,
            ImGuiKey_N,
            ImGuiKey_O,
            ImGuiKey_P,
            ImGuiKey_Q,
            ImGuiKey_R,
            ImGuiKey_S,
            ImGuiKey_T,
            ImGuiKey_U,
            ImGuiKey_V,
            ImGuiKey_W,
            ImGuiKey_X,
            ImGuiKey_Y,
            ImGuiKey_Z,
            ImGuiKey_F1,
            ImGuiKey_F2,
            ImGuiKey_F3,
            ImGuiKey_F4,
            ImGuiKey_F5,
            ImGuiKey_F6,
            ImGuiKey_F7,
            ImGuiKey_F8,
            ImGuiKey_F9,
            ImGuiKey_F10,
            ImGuiKey_F11,
            ImGuiKey_F12,
            ImGuiKey_Apostrophe,
            ImGuiKey_Comma,
            ImGuiKey_Minus,
            ImGuiKey_Period,
            ImGuiKey_Slash,
            ImGuiKey_Semicolon,
            ImGuiKey_Equal,
            ImGuiKey_LeftBracket,
            ImGuiKey_Backslash,
            ImGuiKey_RightBracket,
            ImGuiKey_GraveAccent,
            ImGuiKey_CapsLock,
            ImGuiKey_ScrollLock,
            ImGuiKey_NumLock,
            ImGuiKey_PrintScreen,
            ImGuiKey_Pause,
            ImGuiKey_Keypad0,
            ImGuiKey_Keypad1,
            ImGuiKey_Keypad2,
            ImGuiKey_Keypad3,
            ImGuiKey_Keypad4,
            ImGuiKey_Keypad5,
            ImGuiKey_Keypad6,
            ImGuiKey_Keypad7,
            ImGuiKey_Keypad8,
            ImGuiKey_Keypad9,
            ImGuiKey_KeypadDecimal,
            ImGuiKey_KeypadDivide,
            ImGuiKey_KeypadMultiply,
            ImGuiKey_KeypadSubtract,
            ImGuiKey_KeypadAdd,
            ImGuiKey_KeypadEnter,
            ImGuiKey_KeypadEqual,

            ImGuiKey_GamepadStart,
            ImGuiKey_GamepadBack,
            ImGuiKey_GamepadFaceLeft,
            ImGuiKey_GamepadFaceRight,
            ImGuiKey_GamepadFaceUp,
            ImGuiKey_GamepadFaceDown,
            ImGuiKey_GamepadDpadLeft,
            ImGuiKey_GamepadDpadRight,
            ImGuiKey_GamepadDpadUp,
            ImGuiKey_GamepadDpadDown,
            ImGuiKey_GamepadL1,
            ImGuiKey_GamepadR1,
            ImGuiKey_GamepadL2,
            ImGuiKey_GamepadR2,
            ImGuiKey_GamepadL3,
            ImGuiKey_GamepadR3,
            ImGuiKey_GamepadLStickLeft,
            ImGuiKey_GamepadLStickRight,
            ImGuiKey_GamepadLStickUp,
            ImGuiKey_GamepadLStickDown,
            ImGuiKey_GamepadRStickLeft,
            ImGuiKey_GamepadRStickRight,
            ImGuiKey_GamepadRStickUp,
            ImGuiKey_GamepadRStickDown,

            ImGuiKey_MouseLeft,
            ImGuiKey_MouseRight,
            ImGuiKey_MouseMiddle,
            ImGuiKey_MouseX1,
            ImGuiKey_MouseX2,
            ImGuiKey_MouseWheelX,
            ImGuiKey_MouseWheelY,

            ImGuiKey_ReservedForModCtrl,
            ImGuiKey_ReservedForModShift,
            ImGuiKey_ReservedForModAlt,
            ImGuiKey_ReservedForModSuper,
            ImGuiKey_COUNT,

            ImGuiMod_None = 0,
            ImGuiMod_Ctrl = 1 << 12,
            ImGuiMod_Shift = 1 << 13,
            ImGuiMod_Alt = 1 << 14,
            ImGuiMod_Super = 1 << 15,
            ImGuiMod_Shortcut = 1 << 11,
            ImGuiMod_Mask_ = 0xF800,

            ImGuiKey_NamedKey_BEGIN = 512,
            ImGuiKey_NamedKey_END = ImGuiKey_COUNT,
            ImGuiKey_NamedKey_COUNT = ImGuiKey_NamedKey_END - ImGuiKey_NamedKey_BEGIN,
            ImGuiKey_KeysData_SIZE = ImGuiKey_NamedKey_COUNT,
            ImGuiKey_KeysData_OFFSET = ImGuiKey_NamedKey_BEGIN,
        };

        public enum ImGuiConfigFlags
        {
            ImGuiConfigFlags_None = 0,
            ImGuiConfigFlags_NavEnableKeyboard = 1 << 0,
            ImGuiConfigFlags_NavEnableGamepad = 1 << 1,
            ImGuiConfigFlags_NavEnableSetMousePos = 1 << 2,
            ImGuiConfigFlags_NavNoCaptureKeyboard = 1 << 3,
            ImGuiConfigFlags_NoMouse = 1 << 4,
            ImGuiConfigFlags_NoMouseCursorChange = 1 << 5,


            ImGuiConfigFlags_IsSRGB = 1 << 20,
            ImGuiConfigFlags_IsTouchScreen = 1 << 21,
        }

        public enum ImGuiBackendFlags
        {
            ImGuiBackendFlags_None = 0,
            ImGuiBackendFlags_HasGamepad = 1 << 0,
            ImGuiBackendFlags_HasMouseCursors = 1 << 1,
            ImGuiBackendFlags_HasSetMousePos = 1 << 2,
            ImGuiBackendFlags_RendererHasVtxOffset = 1 << 3,
        }

        public enum ImGuiCol
        {
            ImGuiCol_Text,
            ImGuiCol_TextDisabled,
            ImGuiCol_WindowBg,
            ImGuiCol_ChildBg,
            ImGuiCol_PopupBg,
            ImGuiCol_Border,
            ImGuiCol_BorderShadow,
            ImGuiCol_FrameBg,
            ImGuiCol_FrameBgHovered,
            ImGuiCol_FrameBgActive,
            ImGuiCol_TitleBg,
            ImGuiCol_TitleBgActive,
            ImGuiCol_TitleBgCollapsed,
            ImGuiCol_MenuBarBg,
            ImGuiCol_ScrollbarBg,
            ImGuiCol_ScrollbarGrab,
            ImGuiCol_ScrollbarGrabHovered,
            ImGuiCol_ScrollbarGrabActive,
            ImGuiCol_CheckMark,
            ImGuiCol_SliderGrab,
            ImGuiCol_SliderGrabActive,
            ImGuiCol_Button,
            ImGuiCol_ButtonHovered,
            ImGuiCol_ButtonActive,
            ImGuiCol_Header,
            ImGuiCol_HeaderHovered,
            ImGuiCol_HeaderActive,
            ImGuiCol_Separator,
            ImGuiCol_SeparatorHovered,
            ImGuiCol_SeparatorActive,
            ImGuiCol_ResizeGrip,
            ImGuiCol_ResizeGripHovered,
            ImGuiCol_ResizeGripActive,
            ImGuiCol_Tab,
            ImGuiCol_TabHovered,
            ImGuiCol_TabActive,
            ImGuiCol_TabUnfocused,
            ImGuiCol_TabUnfocusedActive,
            ImGuiCol_PlotLines,
            ImGuiCol_PlotLinesHovered,
            ImGuiCol_PlotHistogram,
            ImGuiCol_PlotHistogramHovered,
            ImGuiCol_TableHeaderBg,
            ImGuiCol_TableBorderStrong,
            ImGuiCol_TableBorderLight,
            ImGuiCol_TableRowBg,
            ImGuiCol_TableRowBgAlt,
            ImGuiCol_TextSelectedBg,
            ImGuiCol_DragDropTarget,
            ImGuiCol_NavHighlight,
            ImGuiCol_NavWindowingHighlight,
            ImGuiCol_NavWindowingDimBg,
            ImGuiCol_ModalWindowDimBg,
            ImGuiCol_COUNT,
        }

        public enum ImGuiStyleVar
        {
            ImGuiStyleVar_Alpha,
            ImGuiStyleVar_DisabledAlpha,
            ImGuiStyleVar_WindowPadding,
            ImGuiStyleVar_WindowRounding,
            ImGuiStyleVar_WindowBorderSize,
            ImGuiStyleVar_WindowMinSize,
            ImGuiStyleVar_WindowTitleAlign,
            ImGuiStyleVar_ChildRounding,
            ImGuiStyleVar_ChildBorderSize,
            ImGuiStyleVar_PopupRounding,
            ImGuiStyleVar_PopupBorderSize,
            ImGuiStyleVar_FramePadding,
            ImGuiStyleVar_FrameRounding,
            ImGuiStyleVar_FrameBorderSize,
            ImGuiStyleVar_ItemSpacing,
            ImGuiStyleVar_ItemInnerSpacing,
            ImGuiStyleVar_IndentSpacing,
            ImGuiStyleVar_CellPadding,
            ImGuiStyleVar_ScrollbarSize,
            ImGuiStyleVar_ScrollbarRounding,
            ImGuiStyleVar_GrabMinSize,
            ImGuiStyleVar_GrabRounding,
            ImGuiStyleVar_TabRounding,
            ImGuiStyleVar_ButtonTextAlign,
            ImGuiStyleVar_SelectableTextAlign,
            ImGuiStyleVar_COUNT,
        }

        public enum ImGuiButtonFlags
        {
            ImGuiButtonFlags_None = 0,
            ImGuiButtonFlags_MouseButtonLeft = 1 << 0,
            ImGuiButtonFlags_MouseButtonRight = 1 << 1,
            ImGuiButtonFlags_MouseButtonMiddle = 1 << 2,


            ImGuiButtonFlags_MouseButtonMask_ = ImGuiButtonFlags_MouseButtonLeft | ImGuiButtonFlags_MouseButtonRight | ImGuiButtonFlags_MouseButtonMiddle,
            ImGuiButtonFlags_MouseButtonDefault_ = ImGuiButtonFlags_MouseButtonLeft,
        }

        public enum ImGuiColorEditFlags
        {
            ImGuiColorEditFlags_None = 0,
            ImGuiColorEditFlags_NoAlpha = 1 << 1,
            ImGuiColorEditFlags_NoPicker = 1 << 2,
            ImGuiColorEditFlags_NoOptions = 1 << 3,
            ImGuiColorEditFlags_NoSmallPreview = 1 << 4,
            ImGuiColorEditFlags_NoInputs = 1 << 5,
            ImGuiColorEditFlags_NoTooltip = 1 << 6,
            ImGuiColorEditFlags_NoLabel = 1 << 7,
            ImGuiColorEditFlags_NoSidePreview = 1 << 8,
            ImGuiColorEditFlags_NoDragDrop = 1 << 9,
            ImGuiColorEditFlags_NoBorder = 1 << 10,

            ImGuiColorEditFlags_AlphaBar = 1 << 16,
            ImGuiColorEditFlags_AlphaPreview = 1 << 17,
            ImGuiColorEditFlags_AlphaPreviewHalf = 1 << 18,
            ImGuiColorEditFlags_HDR = 1 << 19,
            ImGuiColorEditFlags_DisplayRGB = 1 << 20,
            ImGuiColorEditFlags_DisplayHSV = 1 << 21,
            ImGuiColorEditFlags_DisplayHex = 1 << 22,
            ImGuiColorEditFlags_Uint8 = 1 << 23,
            ImGuiColorEditFlags_Float = 1 << 24,
            ImGuiColorEditFlags_PickerHueBar = 1 << 25,
            ImGuiColorEditFlags_PickerHueWheel = 1 << 26,
            ImGuiColorEditFlags_InputRGB = 1 << 27,
            ImGuiColorEditFlags_InputHSV = 1 << 28,

            ImGuiColorEditFlags_DefaultOptions_ = ImGuiColorEditFlags_Uint8 | ImGuiColorEditFlags_DisplayRGB | ImGuiColorEditFlags_InputRGB | ImGuiColorEditFlags_PickerHueBar,

            ImGuiColorEditFlags_DisplayMask_ = ImGuiColorEditFlags_DisplayRGB | ImGuiColorEditFlags_DisplayHSV | ImGuiColorEditFlags_DisplayHex,
            ImGuiColorEditFlags_DataTypeMask_ = ImGuiColorEditFlags_Uint8 | ImGuiColorEditFlags_Float,
            ImGuiColorEditFlags_PickerMask_ = ImGuiColorEditFlags_PickerHueWheel | ImGuiColorEditFlags_PickerHueBar,
            ImGuiColorEditFlags_InputMask_ = ImGuiColorEditFlags_InputRGB | ImGuiColorEditFlags_InputHSV,
        }

        public enum ImGuiSliderFlags
        {
            ImGuiSliderFlags_None = 0,
            ImGuiSliderFlags_AlwaysClamp = 1 << 4,
            ImGuiSliderFlags_Logarithmic = 1 << 5,
            ImGuiSliderFlags_NoRoundToFormat = 1 << 6,
            ImGuiSliderFlags_NoInput = 1 << 7,
            ImGuiSliderFlags_InvalidMask_ = 0x7000000F,
        }

        public enum ImGuiMouseButton
        {
            ImGuiMouseButton_Left = 0,
            ImGuiMouseButton_Right = 1,
            ImGuiMouseButton_Middle = 2,
            ImGuiMouseButton_COUNT = 5,
        }

        public enum ImGuiMouseCursor
        {
            ImGuiMouseCursor_None = -1,
            ImGuiMouseCursor_Arrow = 0,
            ImGuiMouseCursor_TextInput,
            ImGuiMouseCursor_ResizeAll,
            ImGuiMouseCursor_ResizeNS,
            ImGuiMouseCursor_ResizeEW,
            ImGuiMouseCursor_ResizeNESW,
            ImGuiMouseCursor_ResizeNWSE,
            ImGuiMouseCursor_Hand,
            ImGuiMouseCursor_NotAllowed,
            ImGuiMouseCursor_COUNT,
        }

        public enum ImGuiCond
        {
            ImGuiCond_None = 0,
            ImGuiCond_Always = 1 << 0,
            ImGuiCond_Once = 1 << 1,
            ImGuiCond_FirstUseEver = 1 << 2,
            ImGuiCond_Appearing = 1 << 3,
        }

        #endregion

        #region ImGuiStyle

        public struct ImGuiStyle
        {
            public float Alpha;
            public float DisabledAlpha;
            public ImVec2 WindowPadding;
            public float WindowRounding;
            public float WindowBorderSize;
            public ImVec2 WindowMinSize;
            public ImVec2 WindowTitleAlign;
            public ImGuiDir WindowMenuButtonPosition;
            public float ChildRounding;
            public float ChildBorderSize;
            public float PopupRounding;
            public float PopupBorderSize;
            public ImVec2 FramePadding;
            public float FrameRounding;
            public float FrameBorderSize;
            public ImVec2 ItemSpacing;
            public ImVec2 ItemInnerSpacing;
            public ImVec2 CellPadding;
            public ImVec2 TouchExtraPadding;
            public float IndentSpacing;
            public float ColumnsMinSpacing;
            public float ScrollbarSize;
            public float ScrollbarRounding;
            public float GrabMinSize;
            public float GrabRounding;
            public float LogSliderDeadzone;
            public float TabRounding;
            public float TabBorderSize;
            public float TabMinWidthForCloseButton;
            public ImGuiDir ColorButtonPosition;
            public ImVec2 ButtonTextAlign;
            public ImVec2 SelectableTextAlign;
            public ImVec2 DisplayWindowPadding;
            public ImVec2 DisplaySafeAreaPadding;
            public float MouseCursorScale;
            public bool AntiAliasedLines;
            public bool AntiAliasedLinesUseTex;
            public bool AntiAliasedFill;
            public float CurveTessellationTol;
            public float CircleTessellationMaxError;
            public fixed float /* ImVec4 */ Colors[(int)ImGuiCol.ImGuiCol_COUNT * 4];
        }

        [DllImport(ImguiLibrary, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImGuiStyle_ScaleAllSizes(ImGuiStyle* self, float scale_factor);

        #endregion

        #region ImGuiIO

        public struct ImGuiKeyData
        {
            bool Down;
            float DownDuration;
            float DownDurationPrev;
            float AnalogValue;
        }

        public struct ImGuiIO
        {
            ImGuiConfigFlags ConfigFlags;
            ImGuiBackendFlags BackendFlags;
            ImVec2 DisplaySize;
            float DeltaTime;
            float IniSavingRate;
            byte* IniFilename;
            byte* LogFilename;
            float MouseDoubleClickTime;
            float MouseDoubleClickMaxDist;
            float MouseDragThreshold;
            float KeyRepeatDelay;
            float KeyRepeatRate;
            float HoverDelayNormal;
            float HoverDelayShort;
            void* UserData;

            ImFontAtlas* Fonts;
            float FontGlobalScale;
            bool FontAllowUserScaling;
            ImFont* FontDefault;
            ImVec2 DisplayFramebufferScale;

            bool MouseDrawCursor;
            bool ConfigMacOSXBehaviors;
            bool ConfigInputTrickleEventQueue;
            bool ConfigInputTextCursorBlink;
            bool ConfigInputTextEnterKeepActive;
            bool ConfigDragClickToInputText;
            bool ConfigWindowsResizeFromEdges;
            bool ConfigWindowsMoveFromTitleBarOnly;
            float ConfigMemoryCompactTimer;

            byte* BackendPlatformName;
            byte* BackendRendererName;
            void* BackendPlatformUserData;
            void* BackendRendererUserData;
            void* BackendLanguageUserData;

            byte* (*GetClipboardTextFn)(void* user_data);
            void (* SetClipboardTextFn) (void* user_data, byte* text);
            void* ClipboardUserData;

            void (* SetPlatformImeDataFn) (ImGuiViewport* viewport, ImGuiPlatformImeData* data);
            void* _UnusedPadding;

            bool WantCaptureMouse;
            bool WantCaptureKeyboard;
            bool WantTextInput;
            bool WantSetMousePos;
            bool WantSaveIniSettings;
            bool NavActive;
            bool NavVisible;
            float Framerate;
            int MetricsRenderVertices;
            int MetricsRenderIndices;
            int MetricsRenderWindows;
            int MetricsActiveWindows;
            int MetricsActiveAllocations;
            ImVec2 MouseDelta;

            ImVec2 MousePos;
            fixed bool MouseDown[5];
            float MouseWheel;
            float MouseWheelH;
            bool KeyCtrl;
            bool KeyShift;
            bool KeyAlt;
            bool KeySuper;

            ImGuiKeyChord KeyMods;
            ImGuiKeyData KeysData[ImGuiKey_KeysData_SIZE];
            bool WantCaptureMouseUnlessPopupClose;
            ImVec2 MousePosPrev;
            fixed ImVec2 MouseClickedPos[5];
            fixed double MouseClickedTime[5];
            fixed bool MouseClicked[5];
            fixed bool MouseDoubleClicked[5];
            fixed ushort MouseClickedCount[5];
            fixed ushort MouseClickedLastCount[5];
            fixed bool MouseReleased[5];
            fixed bool MouseDownOwned[5];
            fixed bool MouseDownOwnedUnlessPopupClose[5];
            fixed float MouseDownDuration[5];
            fixed float MouseDownDurationPrev[5];
            fixed float MouseDragMaxDistanceSqr[5];
            fixed float PenPressure;
            bool AppFocusLost;
            bool AppAcceptingEvents;
            sbyte BackendUsingLegacyKeyArrays;
            bool BackendUsingLegacyNavInputArray;
            char InputQueueSurrogate;
            ImVector_ImWchar InputQueueCharacters;
        }

        CIMGUI_API void ImGuiIO_AddKeyEvent(ImGuiIO* self, ImGuiKey key, bool down);
        CIMGUI_API void ImGuiIO_AddKeyAnalogEvent(ImGuiIO* self, ImGuiKey key, bool down, float v);
        CIMGUI_API void ImGuiIO_AddMousePosEvent(ImGuiIO* self, float x, float y);
        CIMGUI_API void ImGuiIO_AddMouseButtonEvent(ImGuiIO* self, int button, bool down);
        CIMGUI_API void ImGuiIO_AddMouseWheelEvent(ImGuiIO* self, float wh_x, float wh_y);
        CIMGUI_API void ImGuiIO_AddFocusEvent(ImGuiIO* self, bool focused);
        CIMGUI_API void ImGuiIO_AddInputCharacter(ImGuiIO* self, uint c);
        CIMGUI_API void ImGuiIO_AddInputCharacterUTF16(ImGuiIO* self, char c);
        CIMGUI_API void ImGuiIO_AddInputCharactersUTF8(ImGuiIO* self, byte* str);
        CIMGUI_API void ImGuiIO_SetKeyEventNativeData(ImGuiIO* self, ImGuiKey key, int native_keycode, int native_scancode);
        CIMGUI_API void ImGuiIO_SetKeyEventNativeDataEx(ImGuiIO* self, ImGuiKey key, int native_keycode, int native_scancode, int native_legacy_index = -1);
        CIMGUI_API void ImGuiIO_SetAppAcceptingEvents(ImGuiIO* self, bool accepting_events);
        CIMGUI_API void ImGuiIO_ClearInputCharacters(ImGuiIO* self);
        CIMGUI_API void ImGuiIO_ClearInputKeys(ImGuiIO* self);


        #endregion
    }
}