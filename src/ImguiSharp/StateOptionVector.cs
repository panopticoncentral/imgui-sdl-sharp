using System.Collections;

namespace ImguiSharp
{
    public readonly unsafe struct StateOptionVector<T> : IDisposable, IEnumerable<T>
        where T : unmanaged, Enum
    {
        private readonly int* _valuePointer;

        public int Length { get; }

        public T this[int index]
        {
            get => index >= 0 && index < Length ? (T)(object)_valuePointer[index] : throw new InvalidOperationException();
            set
            {
                if (index < 0 || index >= Length)
                {
                    throw new InvalidOperationException();
                }

                _valuePointer[index] = (int)(object)value;
            }
        }

        public StateOptionVector(int length, IReadOnlyList<T>? values = default)
        {
            if ((sizeof(T) != sizeof(int)) || (values?.Count > length))
            {
                throw new InvalidOperationException();
            }

            _valuePointer = (int*)Native.ImGui_MemAlloc((nuint)(sizeof(int) * length));
            Length = length;

            for (var index = 0; index < length; index++)
            {
                _valuePointer[index] = values != null && index < values.Count ? (int)(object)values[index] : default;
            }
        }

        public void Dispose() => Native.ImGui_MemFree(_valuePointer);

        public StateOption<T> GetStateOptionOfElement(int index) => index >= 0 && index < Length ? new(&_valuePointer[index]) : throw new InvalidOperationException();

        public IEnumerator<T> GetEnumerator() => new StateOptionVectorEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private struct StateOptionVectorEnumerator : IEnumerator<T>
        {
            private readonly StateOptionVector<T> _vector;
            private int _current;

            public T Current => _current >= 0 && _current < _vector.Length ? _vector[_current] : throw new InvalidOperationException();

            object IEnumerator.Current => Current;

            public StateOptionVectorEnumerator(StateOptionVector<T> vector)
            {
                _vector = vector;
                _current = -1;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_current != _vector.Length)
                {
                    _current++;
                }

                return _current >= 0 && _current < _vector.Length;
            }

            public void Reset() => _current = -1;
        }
    }
}
