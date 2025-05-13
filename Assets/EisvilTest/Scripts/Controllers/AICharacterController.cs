using EisvilTest.Scripts.CharacterSystem;
using EisvilTest.Scripts.Input;
using UnityEngine;

namespace EisvilTest.Scripts.Controllers
{
    public class AICharacterController : MonoBehaviour, ICharacterController
    {
        private IControllable _controllable;
        private InputAbstraction _input;

        private void Update()
        {
            var moveDirection = _input.Move.Value.Value;
            if (moveDirection != Vector2.zero)
            {
                _controllable.Move(moveDirection.XYtoXZ());
            }

            var fire = _input.Fire.WasPerformedThisFrame;
            if (fire)
            {
                // _controllable.Fire();
            }

            var interaction = _input.Interaction.WasPerformedThisFrame;
            if (interaction)
            {
                _controllable.Interact();
            }
        }

        public void SetInput(InputAbstraction inputAbstraction)
        {
            _input = inputAbstraction;
        }

        public void SetControllable(IControllable controllable)
        {
            _controllable = controllable;
        }
    }
}