namespace EisvilTest.Scripts.Configuration.Characters.CharactersData
{
    public interface ICharacterConfigurationData
    {
        ECharacter Character { get; }
        float MovementSpeed { get; }
        float MaxHealth { get; }
    }
}