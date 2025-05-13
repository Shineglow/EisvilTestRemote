using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace EisvilTest.Scripts.Configuration
{
    public class BaseConfiguration<T1, T2>
    {
        private readonly Dictionary<T1, T2> _idToData = new();

        protected void AddSingle(T1 key, T2 value)
        {
            _idToData.Add(key, value);
        }

        protected void AddMany(IEnumerable<KeyValuePair<T1,T2>> kvps)
        {
            _idToData.AddRange(kvps);
        }

        public T2 GetConfiguration(T1 key)
        {
            if(!_idToData.TryGetValue(key, out var result))
            {
                Debug.LogError("Invalid key. Collection doesn't contain data for this key.");
            }

            return result;
        }
    }
}