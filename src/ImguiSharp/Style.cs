namespace ImguiSharp
{
    public readonly unsafe struct Style : INativeReferenceWrapper<Style, Native.ImGuiStyle>
    {
        private readonly Native.ImGuiStyle* _style;

        public float Alpha
        {
            get => _style->Alpha;
            set => _style->Alpha = value;
        }

        public float DisabledAlpha
        {
            get => _style->DisabledAlpha;
            set => _style->DisabledAlpha = value;
        }

        public SizeF WindowPadding
        {
            get => SizeF.Wrap(_style->WindowPadding);
            set => _style->WindowPadding = value.ToNative();
        }

        public float WindowRounding
        {
            get => _style->WindowRounding;
            set => _style->WindowRounding = value;
        }

        public float WindowBorderSize
        {
            get => _style->WindowBorderSize;
            set => _style->WindowBorderSize = value;
        }

        public SizeF WindowMinSize
        {
            get => SizeF.Wrap(_style->WindowMinSize);
            set => _style->WindowMinSize = value.ToNative();
        }

        public PositionF WindowTitleAlign
        {
            get => PositionF.Wrap(_style->WindowTitleAlign);
            set => _style->WindowTitleAlign = value.ToNative();
        }

        public Direction WindowMenuButtonPosition
        {
            get => (Direction)_style->WindowMenuButtonPosition;
            set => _style->WindowMenuButtonPosition = (Native.ImGuiDir)value;
        }

        public float ChildRounding
        {
            get => _style->ChildRounding;
            set => _style->ChildRounding = value;
        }

        public float ChildBorderSize
        {
            get => _style->ChildBorderSize;
            set => _style->ChildBorderSize = value;
        }

        public float PopupRounding
        {
            get => _style->PopupRounding;
            set => _style->PopupRounding = value;
        }

        public float PopupBorderSize
        {
            get => _style->PopupBorderSize;
            set => _style->PopupBorderSize = value;
        }

        public SizeF FramePadding
        {
            get => SizeF.Wrap(_style->FramePadding);
            set => _style->FramePadding = value.ToNative();
        }

        public float FrameRounding
        {
            get => _style->FrameRounding;
            set => _style->FrameRounding = value;
        }

        public float FrameBorderSize
        {
            get => _style->FrameBorderSize;
            set => _style->FrameBorderSize = value;
        }

        public SizeF ItemSpacing
        {
            get => SizeF.Wrap(_style->ItemSpacing);
            set => _style->ItemSpacing = value.ToNative();
        }

        public SizeF ItemInnerSpacing
        {
            get => SizeF.Wrap(_style->ItemInnerSpacing);
            set => _style->ItemInnerSpacing = value.ToNative();
        }

        public SizeF CellPadding
        {
            get => SizeF.Wrap(_style->CellPadding);
            set => _style->CellPadding = value.ToNative();
        }

        public SizeF TouchExtraPadding
        {
            get => SizeF.Wrap(_style->TouchExtraPadding);
            set => _style->TouchExtraPadding = value.ToNative();
        }

        public float IndentSpacing
        {
            get => _style->IndentSpacing;
            set => _style->IndentSpacing = value;
        }

        public float ColumnsMinSpacing
        {
            get => _style->ColumnsMinSpacing;
            set => _style->ColumnsMinSpacing = value;
        }

        public float ScrollbarSize
        {
            get => _style->ScrollbarSize;
            set => _style->ScrollbarSize = value;
        }

        public float ScrollbarRounding
        {
            get => _style->ScrollbarRounding;
            set => _style->ScrollbarRounding = value;
        }

        public float GrabMinSize
        {
            get => _style->GrabMinSize;
            set => _style->GrabMinSize = value;
        }

        public float GrabRounding
        {
            get => _style->GrabRounding;
            set => _style->GrabRounding = value;
        }

        public float LogSliderDeadzone
        {
            get => _style->LogSliderDeadzone;
            set => _style->LogSliderDeadzone = value;
        }

        public float TabRounding
        {
            get => _style->TabRounding;
            set => _style->TabRounding = value;
        }

        public float TabBorderSize
        {
            get => _style->TabBorderSize;
            set => _style->TabBorderSize = value;
        }

        public float TabMinWidthForCloseButton
        {
            get => _style->TabMinWidthForCloseButton;
            set => _style->TabMinWidthForCloseButton = value;
        }

        public Direction ColorButtonPosition
        {
            get => (Direction)_style->ColorButtonPosition;
            set => _style->ColorButtonPosition = (Native.ImGuiDir)value;
        }

        public PositionF ButtonTextAlign
        {
            get => PositionF.Wrap(_style->ButtonTextAlign);
            set => _style->ButtonTextAlign = value.ToNative();
        }

        public PositionF SelectableTextAlign
        {
            get => PositionF.Wrap(_style->SelectableTextAlign);
            set => _style->SelectableTextAlign = value.ToNative();
        }

        public SizeF DisplayWindowPadding
        {
            get => SizeF.Wrap(_style->DisplayWindowPadding);
            set => _style->DisplayWindowPadding = value.ToNative();
        }

        public SizeF DisplaySafeAreaPadding
        {
            get => SizeF.Wrap(_style->DisplaySafeAreaPadding);
            set => _style->DisplaySafeAreaPadding = value.ToNative();
        }

        public float MouseCursorScale
        {
            get => _style->MouseCursorScale;
            set => _style->MouseCursorScale = value;
        }

        public bool AntiAliasedLines
        {
            get => _style->AntiAliasedLines;
            set => _style->AntiAliasedLines = value;
        }

        public bool AntiAliasedLinesUseTex
        {
            get => _style->AntiAliasedLinesUseTex;
            set => _style->AntiAliasedLinesUseTex = value;
        }

        public bool AntiAliasedFill
        {
            get => _style->AntiAliasedFill;
            set => _style->AntiAliasedFill = value;
        }

        public float CurveTessellationTol
        {
            get => _style->CurveTessellationTol;
            set => _style->CurveTessellationTol = value;
        }

        public float CircleTessellationMaxError
        {
            get => _style->CircleTessellationMaxError;
            set => _style->CircleTessellationMaxError = value;
        }

        private Style(Native.ImGuiStyle* style)
        {
            _style = style;
        }

        public ColorF GetColor(StyleColor color) =>
            ColorF.Wrap(new(_style->Colors[(int)color * 4], _style->Colors[((int)color * 4) + 1], _style->Colors[((int)color * 4) + 2], _style->Colors[((int)color * 4) + 3]));

        public void SetColor(StyleColor styleColor, ColorF color)
        {
            _style->Colors[(int)styleColor * 4] = color.Red;
            _style->Colors[((int)styleColor * 4) + 1] = color.Green;
            _style->Colors[((int)styleColor * 4) + 2] = color.Blue;
            _style->Colors[((int)styleColor * 4) + 3] = color.Alpha;
        }

        public void ScaleAllSizes(float scale) => Native.ImGuiStyle_ScaleAllSizes(_style, scale);

        public static Style? Wrap(Native.ImGuiStyle* native) => native == null ? null : new(native);

        public Native.ImGuiStyle* ToNative() => _style;
    }

    public static unsafe class StyleExtensions
    {
        public static Native.ImGuiStyle* ToNative(this Style? v) => v == null ? null : v.Value.ToNative();
    }
}
