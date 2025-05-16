using UnityEngine;

namespace EisvilTest.Scripts.Characters
{
    public class MovableComponent : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        private Vector3 _speed;

        public void SetCharacterController(CharacterController characterController)
        {
            _characterController = characterController;
        }

        public void Move(Vector3 speed)
        {
            _speed = speed;
        }

        private void FixedUpdate()
        {
            _characterController.SimpleMove(_speed);
            _speed = Vector3.zero;
        }

        public void SetPosition(Vector3 newPosition)
        {
            _characterController.enabled = false;
            transform.position = newPosition;
            _characterController.enabled = true;
        }
    }
}