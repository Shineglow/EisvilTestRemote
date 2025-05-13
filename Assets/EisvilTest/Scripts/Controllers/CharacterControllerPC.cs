using EisvilTest.Scripts.CharacterSystem;
using UnityEngine;

namespace EisvilTest.Scripts.Controllers
{
    public class CharacterControllerPC : MonoBehaviour, ICharacterController
    {
        private IControllable _controllable;
        private PlayerControls.PlayerActions _playerControls;

        public void SetPlayerControls(PlayerControls playerControls)
        {
            _playerControls = playerControls.Player;
            _playerControls.Enable();
        }

        public void SetControllable(IControllable controllable)
        {
            _controllable = controllable;
        }

        private void Update()
        {
            var moveDirection = _playerControls.Move.ReadValue<Vector2>();
            if (moveDirection != Vector2.zero)
            {
                _controllable.Move(moveDirection.XYtoXZ());
            }

            var fire = _playerControls.Fire.WasPerformedThisFrame();
            if (fire)
            {
                _controllable.Fire();
            }

            var interaction = _playerControls.Interaction.WasPerformedThisFrame();
            if (interaction)
            {
                _controllable.Interact();
            }
        }
    }
}