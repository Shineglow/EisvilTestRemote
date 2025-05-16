using System.Collections.Generic;
using UnityEngine;

namespace EisvilTest.Scripts.Configuration
{
    public class ConfigurationBase<T1, T2>
    {
        protected Dictionary<T1, T2> _keyToData;

        public T2 GetData(T1 key)
        {
            if (!_keyToData.TryGetValue(key, out var result))
            {
                Debug.LogError("Invalid key. Data for this key not exist.");
            }

            return result;
        }
    }
}