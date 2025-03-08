namespace Assets.Codebase.ObjectPool.Interfaces
{
    public interface IFactory<T>
    {
        public T Create();
    }
}
