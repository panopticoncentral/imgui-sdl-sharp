namespace SdlSharp.Imgui
{
    public readonly unsafe struct SizeCallbackData
    {
        private readonly Native.ImGuiSizeCallbackData* _data;

        public Position Position => new(_data->Pos);

        public Size CurrentSize => new(_data->CurrentSize);

        public Size DesiredSize
        {
            get => new(_data->DesiredSize);
            set => _data->DesiredSize = value.ToNative();
        }

        internal SizeCallbackData(Native.ImGuiSizeCallbackData* data)
        {
            _data = data;
        }
    }
}
