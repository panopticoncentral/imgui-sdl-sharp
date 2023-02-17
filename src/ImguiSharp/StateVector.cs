using System.Collections;

namespace ImguiSharp
{
    public sealed unsafe class StateVector<T> : IDisposable, IEnumerable<T>
        where T : unmanaged
    {
        private readonly T* _value;

        public int Length { get; }

        public T this[int index]
        {
            get => index >= 0 && index < Length ? _value[index] : throw new InvalidOperationException();
            set
            {
                if (index < 0 || index >= Length)
                {
                    throw new InvalidOperationException();
                }

                _value[index] = value;
            }
        }

        public StateVector(int length)
        {
            _value = (T*)Native.ImGui_MemAlloc((nuint)(sizeof(T) * length));
            Length = length;
        }

        public void Dispose() => Native.ImGui_MemFree(_value);

        internal T* ToNative() => _value;

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
