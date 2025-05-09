using EisvilTest.Scripts.Configuration.Characters;
using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public class Character : IControllable
    {
        private readonly IPawn _pawn;
        private readonly ICharacterConfigurationData _characterConfiguration;
        private readonly IWeapon _weapon;

        public Character(ICharacterConfigurationData characterConfiguration, IPawn pawn, IWeapon weapon)
        {
            _pawn = pawn;
            _characterConfiguration = characterConfiguration;
            _weapon = weapon;
        }

        public void Move(Vector2 moveDirection)
        {
            _pawn.Move(_characterConfiguration.MovementSpeed * moveDirection);
        }

        public void Fire()
        {
            _weapon.Fire();
        }

        public void Interact()
        {
            if (_pawn.IsInteractionAvailable)
            {
                _pawn.Interactable.Interact();
            }
        }
    }
}