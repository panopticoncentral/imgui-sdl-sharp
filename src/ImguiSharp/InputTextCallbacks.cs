namespace ImguiSharp
{
    public sealed class InputTextCallbacks
    {
        public Func<char, char?>? Filter { get; init; }

        public Func<StateVector<char>, int, StateVector<char>>? Resize { get; init; }

        public Func<Key, InputTextState, bool>? Completion { get; init; }

        public Func<Key, InputTextState, bool>? History { get; init; }

        public Func<InputTextState, bool>? Edit { get; init; }

        public Func<InputTextState, bool>? Always { get; init; }
    }
}
