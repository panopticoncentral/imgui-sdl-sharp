#pragma warning disable CA1000

namespace ImguiSharp
{
    public unsafe interface INativeWrapper<TManaged, TNative>
        where TManaged : struct
        where TNative : unmanaged
    {
        static abstract TManaged Wrap(TNative* native);
        TNative* ToNative();
    }
}
