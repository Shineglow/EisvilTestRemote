using System;
using System.Collections.Generic;
using EisvilTest.Scripts.CharacterSystem;
using EisvilTest.Scripts.Configuration;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.ResourcesManagement;

namespace EisvilTest.Scripts.Characters
{
    public class CharactersSystem
    {
        private Dictionary<ECharacter, (HashSet<Character> active, Stack<Character> inactive)> _poolsByType = new();
        
        private readonly ConfigurationBase<ECharacter, ICharacterConfigurationData> _characterConfiguration;
        private readonly ResourceManager _resourceManager;

        public event Action<Character> AnyCharacterDied;

        public CharactersSystem()
        {
            _characterConfiguration = CompositionRoot.GetCharactersConfiguration();
            _resourceManager = CompositionRoot.GetResourceManager();
        }

        public Character CreateCharacter(ECharacter character)
        {
            HashSet<Character> active;
            Stack<Character> inactive;
            Character characterInstance;
            
            if (_poolsByType.TryGetValue(character, out var pools))
            {
                (active, inactive) = pools;
                if (inactive.Count > 0)
                {
                    characterInstance = inactive.Pop();
                }
                else
                {
                    characterInstance = _resourceManager.CreatePrefabInstance<Character, ECharacter>(character);
                    active.Add(characterInstance);
                }
            }
            else
            {
                _poolsByType.Add(character, (active = new(), inactive = new()));
                characterInstance = _resourceManager.CreatePrefabInstance<Character, ECharacter>(character);
                active.Add(characterInstance);
            }
            
            characterInstance.Init(_characterConfiguration.GetData(character));
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