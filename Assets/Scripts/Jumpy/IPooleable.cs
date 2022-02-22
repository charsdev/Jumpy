namespace Chars.Tools
{
    public interface IPooleable
    {
        void Release();
        void Capture();
    }
}