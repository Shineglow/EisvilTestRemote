namespace EisvilTest.Scripts.ResourcesManagement.Pools
{
    public interface IPool
    {
        void FreeAll();
    }

    public interface IPool<T>
    {
        void ReturnToPool(T instance);
        public T Get();
    }
}