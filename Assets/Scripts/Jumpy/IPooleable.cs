namespace Jumpy
{
    public interface IPooleable
    {
        void Release();
        void Capture();
    }
}