namespace ImguiSharp
{
    public readonly record struct Size(int Width, int Height)
    {
        public static explicit operator SizeF(Size s) => new(s.Width, s.Height);
    }
}
