using System;
using UnityEngine;

[Serializable]
public class ObservableValue<T> : IObservableValueReadOnly<T>
{
    [SerializeField] private T _value;
    public T Value
    {
        get { return _value; }
        set 
        { 
            if(Equals(value, _value)) return;
            var old = _value;
            _value = value;
            ValueChanged?.Invoke(old, value);
        }
    }

    public event Action<T, T> ValueChanged;

    public ObservableValue() : this(default) { }
    public ObservableValue(T value) { Value = value; }

    public static implicit operator ObservableValue<T>(T value)
    {
        return new ObservableValue<T> { Value = value };
    }

    public static implicit operator T(ObservableValue<T> value)
    {
        return value.Value;
    }
}