using EisvilTest.Scripts.Characters;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.ResourcesManagement.Enums;

namespace EisvilTest.Scripts.Configuration.Characters.CharactersData
{
    public interface ICharacterConfigurationData
    {
        ECharacter Character { get; }
        ECharacterPrefabs Prefab { get; }
        ECharacterMaterials Material { get; }
        EWeapons InitialWeapon { get; }
        float MovementSpeed { get; }
        float MaxHealth { get; }
    }
}