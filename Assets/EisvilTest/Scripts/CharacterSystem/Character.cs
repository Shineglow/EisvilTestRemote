using EisvilTest.Scripts.Configuration.Characters;
using System.Collections;
using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public class Character : IControllable
    {
        private readonly IPawn _pawn;
        private readonly ICharacterConfigurationData _characterConfiguration;
        private readonly IWeapon _weapon;
        private readonly GameObject _coroutineObject;

        public Character(ICharacterConfigurationData characterConfiguration, IPawn pawn, IWeapon weapon)
        {
            _pawn = pawn;
            _characterConfiguration = characterConfiguration;
            _weapon = weapon;
            _coroutineObject = new GameObject();
        }

        public void Move(Vector3 moveDirection)
        {
            _pawn.Move(_characterConfiguration.MovementSpeed * moveDirection);
        }

        public void Fire()
        {
            _weapon?.Fire();
        }

        public void Interact()
        {
            if (_pawn.IsInteractionAvailable)
            {
                _pawn.Interactable.Interact();
            }
        }
        
        public void MoveToPosition(Vector3 position)
        {
            var destination = position.normalized * _characterConfiguration.MovementSpeed;
            if(destination.sqrMagnitude > position.sqrMagnitude)
            {
                destination = position;
            }
            _pawn.Move(destination);
        }
    }
}