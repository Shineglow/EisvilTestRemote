using EisvilTest.Scripts.Characters;
using EisvilTest.Scripts.Configuration;
using UnityEngine;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.Controllers;
using EisvilTest.Scripts.General;
using EisvilTest.Scripts.Input;

namespace EisvilTest.Scripts
{
    public class BootstrapMain : MonoBehaviour
    {
        private ConfigurationBase<EWeapons, WeaponConfiguration> _weaponsConfiguration;
        private ConfigurationBase<ECharacter, ICharacterConfigurationData> _charactersConfiguration;
        private ICharacterController _characterController;
        [SerializeField] private Vector3 _playerSpawnPosition;

        private Character character;
        private CameraController _mainCamera;
        private CharactersSystem _charactersSystem;

        private void Start()
        {
            _charactersConfiguration = CompositionRoot.GetCharactersConfiguration();
            _weaponsConfiguration = CompositionRoot.GetWeaponsConfiguration();
            _charactersSystem = CompositionRoot.GetCharactersSystem();
            _mainCamera = CompositionRoot.GetMainCamera();
            
            CreateAndSetupPlayer();
            SpawnEnemies();
        }

        private void CreateAndSetupPlayer()
        {
            InputCreator.CreateBoundedInstances(out var setter, out var getter);

#if UNITY_STANDALONE || UNITY_EDITOR
            var characterControllerPC = new GameObject("CharacterController").AddComponent<CharacterControllerPC>();
            characterControllerPC.Init(new PlayerControls(), setter);
#endif

            character = _charactersSystem.CreateCharacter(ECharacter.Player);
            character.SetInput(getter);
            character.SetPosition(_playerSpawnPosition);
            character.SetOrientationCamera(_mainCamera.CameraMain);
            _mainCamera.SetTarget(character.transform);
        }

        private void SpawnEnemies()
        {
            for (var i = -7; i < 8; i++)
            {
                var enemy = _charactersSystem.CreateCharacter(ECharacter.EnemyRed);
                enemy.SetPosition(_playerSpawnPosition + new Vector3(4f, 0, i * 2f));
                var anotherEnemy = _charactersSystem.CreateCharacter(ECharacter.EnemyGreen);
                anotherEnemy.SetPosition(_playerSpawnPosition + new Vector3(-4f, 0, i * 2f));
            }
        }
    }
}
