namespace ImguiSharp
{
    public readonly unsafe struct InputTextState : INativeReferenceWrapper<InputTextState, Native.ImGuiInputTextCallbackData>
    {
        private readonly Native.ImGuiInputTextCallbackData* _data;

        public string Text => Native.Utf8ToString(_data->Buf)!;

        public int Length => _data->BufTextLen;

        public bool HasSelection => Native.ImGuiInputTextCallbackData_HasSelection(_data);

        public int CursorPosition
        {
            get => _data->CursorPos;
            set => _data->CursorPos = value;
        }

        public int SelectionStart
        {
            get => _data->SelectionStart;
            set => _data->SelectionStart = value;
        }

        public int SelectionEnd
        {
            get => _data->SelectionEnd;
            set => _data->SelectionEnd = value;
        }

        private InputTextState(Native.ImGuiInputTextCallbackData* data)
        {
            _data = data;
        }

        internal void ReplaceBuffer(byte* buffer) => _data->Buf = buffer;

        public void DeleteChars(int pos, int count) => Native.ImGuiInputTextCallbackData_DeleteChars(_data, pos, count);

        public void InsertChars(int pos, string text)
        {
            fixed (byte* textPtr = Native.StringToUtf8(text))
            {
                Native.ImGuiInputTextCallbackData_InsertChars(_data, pos, textPtr);
            }
        }

        public void SelectAll() => Native.ImGuiInputTextCallbackData_SelectAll(_data);

        public void ClearSelection() => Native.ImGuiInputTextCallbackData_ClearSelection(_data);

        public static InputTextState? Wrap(Native.ImGuiInputTextCallbackData* native) => native == null ? null : new(native);

        public Native.ImGuiInputTextCallbackData* ToNative() => _data;
    }
}
