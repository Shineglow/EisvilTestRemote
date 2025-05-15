using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EisvilTest.Scripts.Configuration.Characters;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.Input;
using EisvilTest.Scripts.Weapon;
using UnityEngine;
using UnityEngine.Serialization;

namespace EisvilTest.Scripts.CharacterSystem
{
    public class Character : MonoBehaviour
    {
        private readonly CharacterProperties _characterProperties = new();
        public ICharacterProperties CharacterProperties => _characterProperties;

        private ICharacterConfigurationData _characterConfiguration;
        private IInputAbstraction _input;
        [SerializeField] private MovableComponent _movableComponent;
        [SerializeField] private WeaponKeeperComponent _weaponKeeperComponent;
        [SerializeField] private DamageableComponent _damageableComponent;
        private WeaponLogic _weapon;
        private WeaponConfiguration _weaponConfiguration;
        private Camera _camera;

        public ECharacter CharacterType => _characterConfiguration.Character;
        public event Action<Character> CharacterDied;

        private void Awake()
        {
            _damageableComponent.DamageInflicted += OnDamageInflicted;
        }

        private void OnDamageInflicted(float damage)
        {
            _characterProperties.Health.Value -= damage;
            if (_characterProperties.Health.Value <= 0)
            {
                CharacterDied?.Invoke(this);
            }
        }

        public void Init(ICharacterConfigurationData characterConfiguration)
        {
            _characterConfiguration = characterConfiguration;
            _camera = Camera.main;
        }

        public void SetInput(IInputAbstraction input)
        {
            _input = input;
        }

        public void SetWeapon(WeaponLogic weapon, WeaponConfiguration configuration, Func<Transform, Transform, Transform, CancellationToken, UniTask> animationFunction)
        {
            _weapon = weapon;
            _weaponConfiguration = configuration;
            _weaponKeeperComponent.PutWeapon(weapon.GetTransform(), configuration.Offset, configuration.Orientation, animationFunction);
            weapon.Equip(gameObject, LayerMask.GetMask("Player", "Enemy", "Destructable"));
        }

        private void Update()
        {
            if (_input.Move.IsPressed)
            {
                Vector3 speed = 6f * _input.Move.Value.XYtoXZ();
                _movableComponent.Move(speed);
            }

            if (_input.Fire.Value && !_weaponKeeperComponent.IsAttacking)
            {
                _weapon.SetWeaponAttackMode();
                _weaponKeeperComponent.Attack(_weaponConfiguration.AttacksDellay);
            }
            if (!_weaponKeeperComponent.IsAttacking)
            {
                _weapon.SetWeaponNormalMode();
            }

            if (_input.MousePos.WasChanged)
            {
                Ray ray = _camera.ScreenPointToRay(_input.MousePos.Value);
    
                if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
                {
                    Vector3 targetPoint = hitInfo.point;
                    RotateToMouse(targetPoint);
                }
            }
        }

        private void RotateToMouse(Vector3 targetPoint)
        {
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
    }
}