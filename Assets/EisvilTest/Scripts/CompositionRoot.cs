using EisvilTest.Scripts.Characters;
using EisvilTest.Scripts.Configuration;
using EisvilTest.Scripts.Configuration.Characters;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.General;
using EisvilTest.Scripts.ResourcesManagement;
using EisvilTest.Scripts.ResourcesManagement.Enums;
using EisvilTest.Scripts.Weapon;
using UnityEngine;

namespace EisvilTest.Scripts
{
    public class CompositionRoot : MonoBehaviour
    {
        private static ResourceManager ResourceManager;
        private static CharactersSystem CharactersSystem;
        private static WeaponSystem WeaponSystem;
        private static CameraController MainCamera;
        
        private static ConfigurationBase<EWeapons, WeaponConfiguration> WeaponsConfiguration;
        private static ConfigurationBase<ECharacter, ICharacterConfigurationData> CharactersConfiguration;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public static ResourceManager GetResourceManager()
        {
            return ResourceManager ??= new ResourceManager();
        }

        public static CharactersSystem GetCharactersSystem()
        {
            return CharactersSystem ??= new CharactersSystem();
        }

        public static WeaponSystem GetWeaponSystem()
        {
            return WeaponSystem ??= new WeaponSystem();
        }

        public static CameraController GetMainCamera()
        {
            return MainCamera ??= GetResourceManager()
                .CreatePrefabInstance<CameraController, EGeneralPrefabs>(EGeneralPrefabs.MainCamera);
        }

        public static ConfigurationBase<EWeapons, WeaponConfiguration> GetWeaponsConfiguration()
        {
            return WeaponsConfiguration ??= new WeaponsConfiguration();
        }
        
        public static ConfigurationBase<ECharacter, ICharacterConfigurationData> GetCharactersConfiguration()
        {
            return CharactersConfiguration ??= new CharactersConfiguration();
        }
    }
}