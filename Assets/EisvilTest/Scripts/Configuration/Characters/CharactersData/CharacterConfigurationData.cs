using EisvilTest.Scripts.Configuration.Characters.CharactersData;

namespace EisvilTest.Scripts.Configuration.Characters
{
    public class CharacterConfigurationData : ICharacterConfigurationData
    {
        public ECharacter Character { get; set; }
        public float MaxHealth { get; set; }
        public float MovementSpeed { get; set; }
    }
}