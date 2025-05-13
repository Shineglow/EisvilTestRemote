using EisvilTest.Scripts.Configuration.Characters.CharactersData;

namespace EisvilTest.Scripts.Configuration.Characters
{
    public class CharactersConfiguration : ConfigurationBase<ECharacter, ICharacterConfigurationData>
    {
        public CharactersConfiguration()
        {
            _keyToData = new()
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
    }
}