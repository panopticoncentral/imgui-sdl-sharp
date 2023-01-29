using System.Collections;

namespace ImguiSharp
{
    public readonly unsafe struct DrawData : IReadOnlyList<DrawList>
    {
        private readonly Native.ImDrawData* _data;

        public bool Valid => _data->Valid;

        public int TotalIdxCount => _data->TotalIdxCount;

        public int TotalVtxCount => _data->TotalVtxCount;

        public int Count => _data->CmdListsCount;

        public DrawList this[int index] => index >= 0 && index < _data->CmdListsCount ? DrawList.Wrap(_data->CmdLists[index]) : throw new InvalidOperationException();

        public Position DisplayPosition => new(_data->DisplayPos);

        public Size DisplaySize => new(_data->DisplaySize);

        public (float X, float Y) FramebufferScale => (_data->FramebufferScale.X, _data->FramebufferScale.Y);

        internal DrawData(Native.ImDrawData* data)
        {
            _data = data;
        }

        internal Native.ImDrawData* ToNative() => _data;

        public IEnumerator<DrawList> GetEnumerator() => new DrawListEnumerator(_data);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private sealed class DrawListEnumerator : IEnumerator<DrawList>
        {
            private readonly Native.ImDrawData* _data;
            private int _index;

            public DrawList Current => _index >= 0 && _index < _data->CmdListsCount ? DrawList.Wrap(_data->CmdLists[_index]) : throw new InvalidOperationException();

            object IEnumerator.Current => Current;

            public DrawListEnumerator(Native.ImDrawData* data)
            {
                _data = data;
                _index = -1;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (_index < _data->CmdListsCount)
                {
                    _index++;
                }

                return _index == _data->CmdListsCount;
            }
            public void Reset() => _index = -1;
        }
    }
}
