using EisvilTest.Scripts.Configuration.Characters.CharactersData;

namespace EisvilTest.Scripts.Configuration.Characters
{
    public interface ICharacterConfigurations
    {
        ICharacterConfigurationData GetCharacterConfiguration(ECharacter characterID);
    }
}