namespace ImguiSharp
{
    public readonly unsafe struct TableSortSpecifications : INativeReferenceWrapper<TableSortSpecifications, Native.ImGuiTableSortSpecs>
    {
        private readonly Native.ImGuiTableSortSpecs* _native;

        private TableSortSpecifications(Native.ImGuiTableSortSpecs* native)
        {
            _native = native;
        }

        public static TableSortSpecifications? Wrap(Native.ImGuiTableSortSpecs* native) => native == null ? null : new(native);

        public Native.ImGuiTableSortSpecs* ToNative() => _native;
    }
}
