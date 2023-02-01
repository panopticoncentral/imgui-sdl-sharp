#pragma warning disable CA1000 // Do not declare static members on generic types

namespace ImguiSharp
{
    public unsafe interface INativeReferenceWrapper<TManaged, TNative>
        where TManaged : struct
        where TNative : unmanaged
    {
        static abstract TManaged Wrap(TNative* native);
        TNative* ToNative();
    }
}
