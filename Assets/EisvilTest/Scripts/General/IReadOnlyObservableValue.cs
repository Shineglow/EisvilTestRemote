using System;

namespace EisvilTest.Scripts.General
{
    public interface IReadOnlyObservableValue<T>
    {
        T Value { get; }
        event Action<T, T> ValueChanged;
    }
}