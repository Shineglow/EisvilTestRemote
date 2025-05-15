using EisvilTest.Scripts.Characters;
using EisvilTest.Scripts.Configuration.Weapon;

namespace EisvilTest.Scripts.Configuration.Characters.CharactersData
{
    public class CharacterConfigurationData : ICharacterConfigurationData
    {
        public ECharacter Character { get; set; }
        public ECharacterPrefabs Prefab { get; set; }
        public EWeapons InitialWeapon { get; set; }
        public float MaxHealth { get; set; }
        public float MovementSpeed { get; set; }
    }
}