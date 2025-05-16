using System;
using System.Collections.Generic;

namespace EisvilTest.Scripts.ResourcesManagement.Pools
{
    public class Pool<T> : IPool, IPool<T>
    {
        private HashSet<T> actives = new();
        private Stack<T> free = new();
        
        private readonly Action<T> _onReturnAction;
        private readonly Func<T> _factory;

        public Pool(Func<T> factory, Action<T> onReturnAction, int initialCount = 3)
        {
            _onReturnAction = onReturnAction;
            _factory = factory;
            for (int i = 0; i < initialCount; i++)
            {
                actives.Add(factory());
            }
        }

        public void FreeAll()
        {
            foreach (var active in actives)
            {
                ReturnToPool(active);
            }
            actives.Clear();
        }

        public void ReturnToPool(T instance)
        {
            actives.Remove(instance);
            _onReturnAction(instance);
            free.Push(instance);
        }

        public T Get()
        {
            if (!free.TryPop(out T result))
            {
                result = _factory();
            }
            actives.Add(result);
            return result;
        }
    }
}