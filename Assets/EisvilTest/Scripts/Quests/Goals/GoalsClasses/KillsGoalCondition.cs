using EisvilTest.Scripts.Characters;
using EisvilTest.Scripts.CharacterSystem;
using EisvilTest.Scripts.Configuration.Quests;

namespace EisvilTest.Scripts.Quests.Goals.GoalsClasses
{
    public class KillsGoalCondition : GoalCondition
    {
        private readonly CharactersSystem _characterSystem;
        private readonly KillsGoalConfiguration _killsConfiguration;
        private int _count;

        public KillsGoalCondition(KillsGoalConfiguration configuration)
        {
            _killsConfiguration = configuration;
            _characterSystem = CompositionRoot.GetCharactersSystem();
            _characterSystem.AnyCharacterDied += OnCharacterDied;
        }

        private void OnCharacterDied(Character obj)
        {
            if (obj.CharacterType == _killsConfiguration.EnemyType)
            {
                _count++;
            }

            if (_count == _killsConfiguration.KillsCount)
            {
                SetGoalAchieved();
            }
        }

        protected override void OnGoalAchieved()
        {
            _characterSystem.AnyCharacterDied -= OnCharacterDied;
        }
    }
}