using EisvilTest.Scripts.CharacterSystem.CharacterInterfaces;
using EisvilTest.Scripts.Weapon;
using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public interface IPawn : IMovable
    {
        Transform PawnTransform { get; }
        IInteractable Interactable { get; }
        bool IsInteractionAvailable { get; }
        void SetWeapon(WeaponMono weapon);
    }
}