using System;
using System.Collections.Generic;

namespace EisvilTest.Scripts.ResourcesManagement.Pools
{
    public class PoolsAggregator
    {
        private Dictionary<Type, IPool> _typeToPool = new();

        public bool ContainPool<T>()
        {
            return _typeToPool.ContainsKey(typeof(T));
        }

        public IPool<T2> CreatePool<T1, T2>(Func<T2> factory, Action<T2> onReturnAction, int initialCount)
        {
            Pool<T2> result = new Pool<T2>(factory, onReturnAction, initialCount);
            _typeToPool.Add(typeof(T1), result);
            return result;
        }

        public void RemovePool<T>()
        {
            if (_typeToPool.TryGetValue(typeof(T), out var val))
            {
                val.FreeAll();
                _typeToPool.Remove(typeof(T));
            }
        }

        public IPool<T2> GetPool<T1, T2>()
        {
            return (IPool<T2>)_typeToPool[typeof(T1)];
        }

        public bool TryCreate<T1, T2>(Func<T2> factory, Action<T2> onReturnAction, int initialCount)
        {
            bool result;
            if (ContainPool<T1>())
            {
                result = false;
            }
            else
            {
                CreatePool<T1,T2>(factory, onReturnAction, initialCount);
                result = true;
            }

            return result;
        }

        public bool TryGetPool<T1, T2>(out IPool<T2>  pool)
        {
            bool result;
            
            if (ContainPool<T1>())
            {
                pool = GetPool<T1, T2>();
                result = true;
            }
            else
            {
                pool = default;
                result = false;
            }

            return result;
        }
    }
}