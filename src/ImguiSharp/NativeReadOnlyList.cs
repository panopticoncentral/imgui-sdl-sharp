using System.Collections;

namespace ImguiSharp
{
    internal sealed unsafe class NativeReadOnlyList<TNative, TManaged> : IReadOnlyList<TManaged>
        where TNative : unmanaged
    {
        public delegate TManaged ConstructorFunc(TNative* native);

        private readonly TNative* _list;
        private readonly ConstructorFunc _constructor;

        public TManaged this[int index] => index >= 0 && index < Count ? _constructor(_list + index) : throw new InvalidOperationException();

        public int Count { get; }

        internal NativeReadOnlyList(TNative* list, int count, ConstructorFunc constructor)
        {
            _list = list;
            Count = count;
            _constructor = constructor;
        }

        public IEnumerator<TManaged> GetEnumerator() => new NativeReadOnlyListEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private sealed class NativeReadOnlyListEnumerator : IEnumerator<TManaged>
        {
            private readonly NativeReadOnlyList<TNative, TManaged> _this;
            private int _index;

            public TManaged Current => _index >= 0 && _index < _this.Count ? _this._constructor(_this._list + _index) : throw new InvalidOperationException();

            object IEnumerator.Current => Current!;

            public NativeReadOnlyListEnumerator(NativeReadOnlyList<TNative, TManaged> @this)
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
