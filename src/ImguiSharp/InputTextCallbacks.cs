namespace ImguiSharp
{
    public sealed class InputTextCallbacks
    {
        public Func<char, char?>? Filter { get; init; }

        public Action<StateText, int>? Resize { get; init; }

        public Action<Key, InputTextState>? Completion { get; init; }

        public Action<Key, InputTextState>? History { get; init; }

        public Action<InputTextState>? Edit { get; init; }

        public Action<InputTextState>? Always { get; init; }
    }
}
