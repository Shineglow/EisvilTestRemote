using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public interface IMovable
    {
        void Move(Vector2 scaledDirection);
    }
}