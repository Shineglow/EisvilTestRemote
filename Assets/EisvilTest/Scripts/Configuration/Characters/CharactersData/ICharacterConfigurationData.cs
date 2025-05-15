using EisvilTest.Scripts.Characters;
using EisvilTest.Scripts.Configuration.Weapon;

namespace EisvilTest.Scripts.Configuration.Characters.CharactersData
{
    public interface ICharacterConfigurationData
    {
        ECharacter Character { get; }
        ECharacterPrefabs Prefab { get; }
        EWeapons InitialWeapon { get; }
        float MovementSpeed { get; }
        float MaxHealth { get; }
    }
}