using System;

namespace EisvilTest.Scripts.General
{
    public class ObservableValue<T> : IReadOnlyObservableValue<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                var valueOld = _value;
                _value = value;
                ValueChanged?.Invoke(valueOld, value);
            }
        }

        public event Action<T, T> ValueChanged;

        public ObservableValue(T initial) => Value = initial;
        public ObservableValue() : this(default) {}

        public static implicit operator ObservableValue<T>(T value)
        {
            return new ObservableValue<T>(value);
        }

        public static implicit operator T(ObservableValue<T> observable)
        {
            return observable._value;
        }
    }
}