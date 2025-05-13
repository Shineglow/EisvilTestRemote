using EisvilTest.Scripts.CharacterSystem.CharacterInterfaces;
using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public interface IControllable : IWeaponUser, IMovable, IInteractor
    {
        Transform CharacterTransform { get; }
        void SetTargetCharacter(Character targetCharacter);
    }
}