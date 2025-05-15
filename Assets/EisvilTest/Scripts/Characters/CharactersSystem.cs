using System;
using System.Collections.Generic;
using EisvilTest.Scripts.Configuration;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.ResourcesManagement;
using EisvilTest.Scripts.Weapon;

namespace EisvilTest.Scripts.Characters
{
    public class CharactersSystem
    {
        private Dictionary<ECharacter, (HashSet<Character> active, Stack<Character> inactive)> _poolsByType = new();
        
        private readonly ConfigurationBase<ECharacter, ICharacterConfigurationData> _characterConfiguration;
        private readonly ConfigurationBase<EWeapons, WeaponConfiguration> _weaponsConfiguration;
        private readonly ResourceManager _resourceManager;
        private readonly WeaponSystem _weaponSystem;

        public event Action<Character> AnyCharacterDied;

        public CharactersSystem()
        {
            _characterConfiguration = CompositionRoot.GetCharactersConfiguration();
            _weaponsConfiguration = CompositionRoot.GetWeaponsConfiguration();
            _weaponSystem = CompositionRoot.GetWeaponSystem();
            _resourceManager = CompositionRoot.GetResourceManager();
        }

        public Character CreateCharacter(ECharacter character)
        {
            HashSet<Character> active;
            Stack<Character> inactive;
            Character characterInstance;
            var characterConfiguration = _characterConfiguration.GetData(character);

            #region GetCharacterInstance

            if (_poolsByType.TryGetValue(character, out var pools))
            {
                (active, inactive) = pools;
                if (inactive.Count > 0)
                {
                    characterInstance = inactive.Pop();
                }
                else
                {
                    characterInstance = _resourceManager.CreatePrefabInstance<Character, ECharacterPrefabs>(characterConfiguration.Prefab);
                    active.Add(characterInstance);
                }
            }
            else
            {
                _poolsByType.Add(character, (active = new(), inactive = new()));
                characterInstance = _resourceManager.CreatePrefabInstance<Character, ECharacterPrefabs>(characterConfiguration.Prefab);
                active.Add(characterInstance);
            }
            characterInstance.Init(characterConfiguration);
            
            #endregion

            #region SetInitialWeapon

            if (characterConfiguration.InitialWeapon != EWeapons.None)
            {
                var weaponLogic = _weaponSystem.GetWeapon(characterConfiguration.InitialWeapon);
                var configuration = _weaponsConfiguration.GetData(characterConfiguration.InitialWeapon);
                characterInstance.SetWeapon(weaponLogic, configuration, WeaponAnimations.SwipeAnimationPattern(90f, configuration.AttackTime));
            }

            #endregion
            
            characterInstance.CharacterDied += OnCharacterDied;

            return characterInstance;
        }

        private void OnCharacterDied(Character character)
        {
            character.Disable();
            var (active, inactive) = _poolsByType[character.CharacterType];
            active.Remove(character);
            inactive.Push(character);

            AnyCharacterDied?.Invoke(character);
        }
    }
}