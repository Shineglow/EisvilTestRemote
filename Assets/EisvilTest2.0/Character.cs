using System;
using System.Threading;
using System.Threading.Tasks;
using EisvilTest.Scripts.Configuration.Characters;
using EisvilTest.Scripts.Weapon;
using UnityEngine;

namespace EisvilTest2._0
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        public Transform CharacterTransform => transform;
        
        private readonly ICharacterConfigurationData _characterConfiguration;
        private readonly IWeapon _weapon;
        private readonly GameObject _coroutineObject;
        
        private bool _isAttacking;
        private Task _attackTask;
        private float _health;
        private Character _targetCharacter;
        private CancellationTokenSource _cts;
        private Vector3 _moveDirection;

        public void Move(Vector3 moveDirection)
        {
            _moveDirection = _characterConfiguration.MovementSpeed * moveDirection;
        }

        public void Fire()
        {
            if (_isAttacking || _targetCharacter == null)
                return;

            _cts = new CancellationTokenSource();
            _attackTask = AttackAsync(_targetCharacter, _cts.Token);
        }

        private Task AttackAsync(Character targetCharacter, CancellationToken ctsToken)
        {
            throw new NotImplementedException();
        }

        public void Interact()
        {
            throw new NotImplementedException();
        }

        private void FixedUpdate()
        {
            if (_moveDirection != Vector3.zero)
            {
                characterController.SimpleMove(_moveDirection);
                _moveDirection = Vector3.zero;
            }
        }

        private void Update()
        {
            
        }
    }
}