using System.Collections.Generic;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using UnityEngine;

namespace EisvilTest.Scripts.Configuration.Characters
{
    public class CharactersConfiguration : ICharacterConfigurations
    {
        private Dictionary<ECharacter, ICharacterConfigurationData> _characterToConfigurationData;

        public CharactersConfiguration()
        {
            _characterToConfigurationData = new()
            {
                { ECharacter.Player, new CharacterConfigurationData()
                    {
                        MaxHealth = 20,
                        MovementSpeed = 2,
                    }
                },
                {ECharacter.EnemyFast, new CharacterConfigurationData()
                    {
                        MaxHealth = 1,
                        MovementSpeed = 2.1f,
                    }
                },
                {ECharacter.EnemyBig, new CharacterConfigurationData()
                    {
                        MaxHealth = 5,
                        MovementSpeed = 1.5f,
                    }
                },
            };
        }

        public ICharacterConfigurationData GetCharacterConfiguration(ECharacter characterID)
        {
            if (!_characterToConfigurationData.TryGetValue(characterID, out var result))
            {
                Debug.LogError($"Trying to access data with wrong key! No data for the {characterID.ToString()} key");
            }

            return result;
        }
    }
}