namespace ImguiSharp
{
    public readonly record struct InputTextCallbacks(
        Func<char, char?>? Filter = null,
        Action<StateText, int>? Resize = null,
        Action<Key, InputTextState>? Completion = null,
        Action<Key, InputTextState>? History = null,
        Action<InputTextState>? Edit = null,
        Action<InputTextState>? Always = null)
    {
    }
}
