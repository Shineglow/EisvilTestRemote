using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem.CharacterInterfaces
{
    public interface IMovable
    {
        void Move(Vector3 direction);
        void MoveToPosition(Vector3 position);
    }
}