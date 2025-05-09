using EisvilTest.Scripts.CharacterSystem.CharacterInterfaces;
using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public interface IControllable : IWeaponUser, IMovable, IInteractor
    {
        void Move(Vector2 moveDirection);
        void Fire();
        void Interact();
    }
}