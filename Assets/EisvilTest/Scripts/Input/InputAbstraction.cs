using UnityEngine;

namespace EisvilTest.Scripts.Input
{
    public class InputCreator
    {
        private static IInputAbstraction _voidInput;
        public static IInputAbstraction VoidInput
        {
            get
            {
                if (_voidInput == null)
                {
                    CreateBoundedInstances(out _, out _voidInput);
                }

                return _voidInput;
            }
        }
        
        public static void CreateBoundedInstances(out InputAbstractionSetter setter, out IInputAbstraction getter)
        {
            setter = new InputAbstractionSetter();
            var getterInstance = new InputAbstraction()
            {
                MousePos = setter.MousePos,
                Move = setter.Move,
                Fire = setter.Fire,
                Interaction = setter.Interaction,
            };
            getter = getterInstance;
        }
    }
    
    public class InputAbstraction : IInputAbstraction
    {
        public IInputActionDetails<Vector2> MousePos { get; set; }
        public IInputActionDetails<Vector2> Move { get; set; }
        public IInputActionDetails<bool> Fire { get; set; }
        public IInputActionDetails<bool> Interaction { get; set; }
    }

    public class InputAbstractionSetter
    {
        public InputActionDetails<Vector2> MousePos { get; } = new();
        public InputActionDetails<Vector2> Move { get; } = new();
        public InputActionDetails<bool> Fire { get; } = new();
        public InputActionDetails<bool> Interaction { get; } = new();
    }

    public interface IInputAbstraction
    {
        IInputActionDetails<Vector2> MousePos {get;}
        IInputActionDetails<Vector2> Move {get;}
        IInputActionDetails<bool> Fire {get;}
        IInputActionDetails<bool> Interaction {get;}
    }

    public class InputActionDetails<T> : IInputActionDetails<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value)) return;
                _value = value;
                _isPressed = !Equals(default, value);
            }
        }

        private bool _isPressed;
        public bool IsPressed { get => _isPressed; set => _isPressed = value; }
        public bool WasChanged { get; set; }
    }
    
    public interface IInputActionDetails<T>
    {
        public T Value { get; }
        public bool IsPressed { get; }
        public bool WasChanged { get; }
    }
}