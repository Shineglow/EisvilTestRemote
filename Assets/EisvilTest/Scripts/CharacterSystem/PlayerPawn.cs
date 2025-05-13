using EisvilTest.Scripts.Weapon;
using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public class PlayerPawn : MonoBehaviour, IPawn
    {
        public Transform PawnTransform => transform;
        public IInteractable Interactable { get; set; }
        public bool IsInteractionAvailable { get; set; }
        [SerializeField] private CharacterController characterController;

        private Vector3 _moveDirection;
        private Vector3 _targetPosition;
        
        public void Move(Vector3 moveDirection)
        {
            _moveDirection = moveDirection;
        }

        public void MoveToPosition(Vector3 position)
        {
            _targetPosition = position;
        }

        private void FixedUpdate()
        {
            if (_moveDirection != Vector3.zero)
            {
                characterController.SimpleMove(_moveDirection);
                _moveDirection = Vector3.zero;
            }
        }

        public void SetWeapon(WeaponMono weapon)
        {
            weapon.EquipWeapon(transform);
        }
    }
}