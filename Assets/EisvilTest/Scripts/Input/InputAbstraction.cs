using EisvilTest.Scripts.General;
using UnityEngine;

namespace EisvilTest.Scripts.Input
{
    public class InputAbstraction
    {
        public InputAbstraction(IActionDetails<Vector2> move, IActionDetails<bool> interaction, IActionDetails<bool> fire)
        {
            Move = move;
            Interaction = interaction;
            Fire = fire;
        }
        
        public IActionDetails<Vector2> Move {get;}
        public IActionDetails<bool> Interaction {get;}
        public IActionDetails<bool> Fire {get;}
    }

    public interface IActionDetails<T>
    {
        IReadOnlyObservableValue<T> Value { get; }
        bool WasPerformedThisFrame { get; }
        bool IsActive { get; }
    }

    public class ActionDetails<T> : IActionDetails<T>
    {
        public IReadOnlyObservableValue<T> Value { get; set; }
        public bool WasPerformedThisFrame { get; set; }
        public bool IsActive { get; set; }
    }
}