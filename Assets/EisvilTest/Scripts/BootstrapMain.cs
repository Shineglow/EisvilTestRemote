using EisvilTest.Scripts.CharacterSystem;
using EisvilTest.Scripts.Configuration.Characters;
using EisvilTest.Scripts.Triggers;
using UnityEngine;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.PlayerSystem;

namespace EisvilTest.Scripts
{
    public class BootstrapMain : MonoBehaviour
    {
        [SerializeField] private UniversalTrigger trigger;
        [SerializeField] private PlayerPawn pawn;
        private CharactersConfiguration _charactersConfiguration;
        private ICharacterController _characterController;

        private void Awake()
        {
            // initialization here
            int mask = LayerMask.GetMask("Player");
            trigger.AddActionToBoth(OnUniversalTriggerEnter, OnUniversalTriggerExit, mask);
        }

        private void Start()
        {
            _charactersConfiguration = new CharactersConfiguration();
            var characterControllerPC = new GameObject("CharacterController").AddComponent<CharacterControllerPC>();
            characterControllerPC.SetPlayerControls(new PlayerControls());
            _characterController = characterControllerPC;
            Character character = new Character(_charactersConfiguration.GetCharacterConfiguration(ECharacter.Player), pawn, null);
            _characterController.SetControllable(character);
        }

        private void OnUniversalTriggerEnter(GameObject obj)
        {
            Debug.Log("Enter");
        }
        
        private void OnUniversalTriggerExit(GameObject obj)
        {
            Debug.Log("Exit");
        }
    }
}
