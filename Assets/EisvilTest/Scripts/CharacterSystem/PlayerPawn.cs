using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public class PlayerPawn : MonoBehaviour, IPawn
    {
        public IInteractable Interactable { get; set; }
        public bool IsInteractionAvailable { get; set; }
        [SerializeField] private CharacterController characterController;

        private Vector3 _moveDirection;
        
        public void Move(Vector3 moveDirection)
        {
            _moveDirection = moveDirection;
        }

        private void FixedUpdate()
        {
            if (_moveDirection != Vector3.zero)
            {
                characterController.SimpleMove(_moveDirection);
                _moveDirection = Vector3.zero;
            }
        }
    }
}