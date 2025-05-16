using EisvilTest.Scripts.Characters;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.ResourcesManagement.Enums;

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
                        Character = ECharacter.Player,
                        Prefab = ECharacterPrefabs.Player,
                        Material = ECharacterMaterials.Player,
                        InitialWeapon = EWeapons.Stick,
                        MaxHealth = 20,
                        MovementSpeed = 2,
                    }
                },
                {ECharacter.EnemyRed, new CharacterConfigurationData()
                    {
                        Character = ECharacter.EnemyRed,
                        Prefab = ECharacterPrefabs.Enemy,
                        Material = ECharacterMaterials.Red,
                        MaxHealth = 1,
                        MovementSpeed = 2.1f,
                    }
                },
                {ECharacter.EnemyGreen, new CharacterConfigurationData()
                    {
                        Character = ECharacter.EnemyGreen,
                        Prefab = ECharacterPrefabs.Enemy,
                        Material = ECharacterMaterials.Green,
                        MaxHealth = 5,
                        MovementSpeed = 1.5f,
                    }
                },
            };
        }
    }
}