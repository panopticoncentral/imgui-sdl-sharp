using System.Collections;

namespace ImguiSharp
{
    public readonly unsafe struct ValueVector<TNative, TManaged> : IReadOnlyList<TManaged>
        where TNative : unmanaged
        where TManaged : struct, INativeValueWrapper<TManaged, TNative>
    {
        private readonly TNative* _vector;

        public TManaged this[int index] => index >= 0 && index < Count ? TManaged.Wrap(*(_vector + index)) : throw new InvalidOperationException();

        public int Count { get; }

        internal ValueVector(TNative* list, int count)
        {
            _vector = list;
            Count = count;
        }

        public TNative* ToNative() => _vector;

        public IEnumerator<TManaged> GetEnumerator() => new VectorEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private sealed class VectorEnumerator : IEnumerator<TManaged>
        {
            private readonly ValueVector<TNative, TManaged> _this;
            private int _index;

            public TManaged Current => _index >= 0 && _index < _this.Count ? TManaged.Wrap(*(_this._vector + _index)) : throw new InvalidOperationException();

            object IEnumerator.Current => Current!;

            public VectorEnumerator(ValueVector<TNative, TManaged> @this)
            {
                _this = @this;
                _index = -1;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_index < _this.Count)
                {
                    _index++;
                }

                return _index != _this.Count;
            }
            public void Reset() => _index = -1;
        }
    }
}
