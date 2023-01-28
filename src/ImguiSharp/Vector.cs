using System.Collections;

namespace ImguiSharp
{
    public readonly unsafe struct Vector<TNative, TManaged> : IReadOnlyList<TManaged>
        where TNative : unmanaged
    {
        internal delegate TManaged ConstructorFunc(TNative* native);

        private readonly TNative* _vector;
        private readonly ConstructorFunc _constructor;

        public TManaged this[int index] => index >= 0 && index < Count ? _constructor(_vector + index) : throw new InvalidOperationException();

        public int Count { get; }

        internal Vector(TNative* list, int count, ConstructorFunc constructor)
        {
            _vector = list;
            Count = count;
            _constructor = constructor;
        }

        internal TNative* ToNative() => _vector;

        public IEnumerator<TManaged> GetEnumerator() => new VectorEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private sealed class VectorEnumerator : IEnumerator<TManaged>
        {
            private readonly Vector<TNative, TManaged> _this;
            private int _index;

            public TManaged Current => _index >= 0 && _index < _this.Count ? _this._constructor(_this._vector + _index) : throw new InvalidOperationException();

            object IEnumerator.Current => Current!;

            public VectorEnumerator(Vector<TNative, TManaged> @this)
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

                return _index == _this.Count;
            }
            public void Reset() => _index = -1;
        }
    }
}
