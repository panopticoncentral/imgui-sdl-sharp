using System.Collections;

namespace ImguiSharp
{
    public readonly unsafe struct StateVector<T> : IDisposable, IEnumerable<T>
        where T : unmanaged
    {
        private readonly T* _valuePointer;

        public int Length { get; }

        public T this[int index]
        {
            get => index >= 0 && index < Length ? _valuePointer[index] : throw new InvalidOperationException();
            set
            {
                if (index < 0 || index >= Length)
                {
                    throw new InvalidOperationException();
                }

                _valuePointer[index] = value;
            }
        }

        internal StateVector(T* valuePointer, int length)
        {
            _valuePointer = valuePointer;
            Length = length;
        }

        public StateVector(int length, IReadOnlyList<T>? values = default)
        {
            if (values?.Count > length)
            {
                throw new InvalidOperationException();
            }

            _valuePointer = (T*)Native.ImGui_MemAlloc((nuint)(sizeof(T) * length));
            Length = length;

            for (var index = 0; index < length; index++)
            {
                _valuePointer[index] = values != null && index < values.Count ? values[index] : default;
            }
        }

        public void Dispose() => Native.ImGui_MemFree(_valuePointer);

        internal T* ToNative() => _valuePointer;

        public State<T> GetStateOfElement(int index) => index >= 0 && index < Length ? new(&_valuePointer[index]) : throw new InvalidOperationException();

        public IEnumerator<T> GetEnumerator() => new StateVectorEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private struct StateVectorEnumerator : IEnumerator<T>
        {
            private readonly StateVector<T> _vector;
            private int _current;

            public T Current => _current >= 0 && _current < _vector.Length ? _vector[_current] : throw new InvalidOperationException();

            object IEnumerator.Current => Current;

            public StateVectorEnumerator(StateVector<T> vector)
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
