namespace ImguiSharp
{
    public readonly unsafe struct FontGlyph : INativeReferenceWrapper<FontGlyph, Native.ImFontGlyph>
    {
        private readonly Native.ImFontGlyph* _glyph;

        public bool Colored
        {
            get => (_glyph->Codepoint & (1u << 32)) != 0;
            set
            {
                if (value)
                {
                    _glyph->Codepoint |= 1u << 32;
                }
                else
                {
                    _glyph->Codepoint &= ~(1u << 32);
                }
            }
        }

        public bool Visible
        {
            get => (_glyph->Codepoint & (1u << 31)) != 0;
            set
            {
                if (value)
                {
                    _glyph->Codepoint |= 1u << 31;
                }
                else
                {
                    _glyph->Codepoint &= ~(1u << 31);
                }
            }
        }

        public uint Codepoint
        {
            get => _glyph->Codepoint & 0x3FFFFFFF;
            set => _glyph->Codepoint = (value & ~0x3FFFFFFFu) | (_glyph->Codepoint & 0xC0000000u);
        }

        public float AdvanceX
        {
            get => _glyph->AdvanceX;
            set => _glyph->AdvanceX = value;
        }

        public Rectangle Corners
        {
            get => new(new(_glyph->X0, _glyph->Y0), new(_glyph->X1, _glyph->Y1));
            set
            {
                _glyph->X0 = value.Min.X;
                _glyph->Y0 = value.Min.Y;
                _glyph->X1 = value.Max.X;
                _glyph->Y1 = value.Max.Y;
            }
        }

        public TextureRectangle TextureCorners
        {
            get => new(new(_glyph->U0, _glyph->V0), new(_glyph->U1, _glyph->V1));
            set
            {
                _glyph->U0 = value.Min.U;
                _glyph->V0 = value.Min.V;
                _glyph->U1 = value.Max.U;
                _glyph->V1 = value.Max.V;
            }
        }

        private FontGlyph(Native.ImFontGlyph* glyph)
        {
            _glyph = glyph;
        }

        public static FontGlyph? Wrap(Native.ImFontGlyph* native) => new(native);

        public Native.ImFontGlyph* ToNative() => _glyph;
    }
}
