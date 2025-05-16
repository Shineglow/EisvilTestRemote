using System;

public interface IObservableValueReadOnly<T>
{
    T Value { get; }
    event Action<T,T> ValueChanged;
}