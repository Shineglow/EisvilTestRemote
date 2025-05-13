using EisvilTest.Scripts.CharacterSystem;
using EisvilTest.Scripts.Input;
using UnityEngine;

namespace EisvilTest.Scripts.Controllers
{
    public class CharacterControllerPC : MonoBehaviour, ICharacterController
    {
        private PlayerControls.PlayerActions _playerControls;
        public InputAbstractionSetter InputAbstraction { get; set; }

        public void Init(PlayerControls playerControls, InputAbstractionSetter inputAbstraction)
        {
            InputAbstraction = inputAbstraction;
            _playerControls = playerControls.Player;
            _playerControls.Enable();
        }

        private void Update()
        {
            var moveOld = InputAbstraction.Move.Value;
            InputAbstraction.Move.Value = _playerControls.Move.ReadValue<Vector2>();
            InputAbstraction.Move.IsPressed = _playerControls.Move.IsPressed();
            InputAbstraction.Move.WasChanged = moveOld != InputAbstraction.Move.Value;

            var fireOld = InputAbstraction.Fire.Value;
            InputAbstraction.Fire.Value = _playerControls.Fire.WasPressedThisFrame();
            InputAbstraction.Fire.IsPressed = _playerControls.Fire.IsPressed();
            InputAbstraction.Fire.WasChanged = fireOld != InputAbstraction.Fire.Value;
            
            var interactionOld = InputAbstraction.Interaction.Value;
            InputAbstraction.Interaction.Value = _playerControls.Interaction.WasPressedThisFrame();
            InputAbstraction.Interaction.IsPressed = _playerControls.Interaction.IsPressed();
            InputAbstraction.Interaction.WasChanged = interactionOld != InputAbstraction.Interaction.Value;
            
            var mousePosOld = InputAbstraction.MousePos.Value;
            InputAbstraction.MousePos.Value = _playerControls.MousePos.ReadValue<Vector2>();
            InputAbstraction.MousePos.WasChanged = mousePosOld != InputAbstraction.MousePos.Value;
        }
    }
}