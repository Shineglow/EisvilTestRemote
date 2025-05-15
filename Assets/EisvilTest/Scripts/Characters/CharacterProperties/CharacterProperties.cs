public class CharacterProperties : ICharacterProperties
{
    public ObservableValue<float> Health { get; } = new ObservableValue<float>();

    IObservableValueReadOnly<float> ICharacterProperties.Health => Health;
}