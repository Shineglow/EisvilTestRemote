using System;
using UnityEngine;

namespace EisvilTest.Scripts.Characters.CharacterProperties
{
    [Serializable]
    public class CharacterProperties : ICharacterProperties
    {
        [field: SerializeField] public ObservableValue<float> Health { get; private set; } = new ObservableValue<float>();

        IObservableValueReadOnly<float> ICharacterProperties.Health => Health;
    }
}