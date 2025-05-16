using System.Text;
using EisvilTest.Scripts.Characters;
using EisvilTest.Scripts.Configuration.Quests.Conditions;
using EisvilTest.Scripts.Configuration.Quests.Conditions.Inheritors;

namespace EisvilTest.Scripts.Quests.Goals.GoalsClasses
{
    public class KillsGoalCondition : GoalCondition
    {
        private readonly CharactersSystem _characterSystem;
        private readonly KillsGoalConfiguration _killsConfiguration;
        private int _count;
        private StringBuilder _stringBuilder;

        public KillsGoalCondition(KillsGoalConfiguration configuration) : base(configuration)
        {
            _killsConfiguration = configuration;
            _stringBuilder = new StringBuilder();
            _characterSystem = CompositionRoot.GetCharactersSystem();
        }

        public override void Start()
        {
            base.Start();
            _characterSystem.AnyCharacterDied += OnCharacterDied;
            RebuildDescription();
        }

        private void OnCharacterDied(Character obj)
        {
            bool anyValuesChanged = false;
            
            if (obj.CharacterType == _killsConfiguration.EnemyType)
            {
                _count++;
                anyValuesChanged = true;
            }

            if (_count == _killsConfiguration.KillsCount)
            {
                SetGoalAchieved();
                anyValuesChanged = true;
            }

            if (anyValuesChanged)
            {
                RebuildDescription();
            }
        }

        private void RebuildDescription()
        {
            _stringBuilder.Clear();
            var killsDescription = string.Format(GoalsStringsTemplates.KillsGoal_KillsCount, _count, _killsConfiguration.KillsCount);
            if (!string.IsNullOrWhiteSpace(_killsConfiguration.Description))
            {
                _stringBuilder.Append($"{_killsConfiguration.Description} ");
            }
            _stringBuilder.Append(killsDescription);
            UpdateDescription(_stringBuilder.ToString());
        }

        public override void DisableMainGoalTracking()
        {
            _characterSystem.AnyCharacterDied -= OnCharacterDied;
        }
    }
}