using System;
using EisvilTest.Scripts.Configuration.Characters;
using System.Threading;
using System.Threading.Tasks;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.Weapon;
using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public class Character : IControllable
    {
        public Transform CharacterTransform => _pawn.PawnTransform;
        
        private readonly IPawn _pawn;
        private readonly ICharacterConfigurationData _characterConfiguration;
        private readonly IWeapon _weapon;
        private readonly GameObject _coroutineObject;
        
        private bool _isAttacking;
        private Task _attackTask;
        private float _health;
        private Character _targetCharacter;
        private CancellationTokenSource _cts;

        public float Health => _health;

        public event Action<Character> CharacterDied;

        public Character(ICharacterConfigurationData characterConfiguration, IPawn pawn, IWeapon weapon)
        {
            _pawn = pawn;
            _characterConfiguration = characterConfiguration;
            _weapon = weapon;
            _coroutineObject = new GameObject();
        }

        public void SetTargetCharacter(Character targetCharacter)
        {
            _targetCharacter = targetCharacter;
        }

        public void Move(Vector3 moveDirection)
        {
            _pawn.Move(_characterConfiguration.MovementSpeed * moveDirection);
        }

        public void Fire()
        {
            if (_isAttacking || _targetCharacter == null)
                return;

            _cts = new CancellationTokenSource();
            _attackTask = AttackAsync(_targetCharacter, _cts.Token);
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

        private async Task AttackAsync(Character target, CancellationToken token)
        {
            if (_isAttacking)
                return;

            _isAttacking = true;
            float damage = _weapon.WeaponProperties.BaseDamage;

            try
            {
                Debug.Log($"[EnemyAI] Начало атаки. Урон: {damage}");
                await Task.WhenAll(_weapon.Fire(), target.GetDamage(damage));
                await Task.Delay(Convert.ToInt32(_weapon.WeaponProperties.AttackTime * 1000), token);
                Debug.Log($"[EnemyAI] Атака завершена. Урон {damage} нанесён.");
            }
            catch (OperationCanceledException)
            {
                Debug.Log("[EnemyAI] Атака прервана через исключение.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[EnemyAI] Ошибка во время атаки: {ex.Message}");
            }
            finally
            {
                _isAttacking = false;
            }
        }

        private Task GetDamage(float damage)
        {
            _health -= damage;
            if (_health < 0)
            {
                CharacterDied?.Invoke(this);
            }
            return Task.CompletedTask;
        }
    }
}