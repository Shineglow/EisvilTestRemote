using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.Quests.Goals;
using EisvilTest.Scripts.Quests.Goals.GoalsClasses;

namespace EisvilTest.Scripts.Configuration.Quests.Conditions.Inheritors
{
    public class KillsGoalConfiguration : GoalConditionConfiguration
    {
        public string DescriptionSetter;
        public ECharacter EnemyType;
        public int KillsCount;

        public override string Description => DescriptionSetter;
        
        public override GoalCondition GetGoalConditionInstance()
        {
            return new KillsGoalCondition(this);
        }
    }
}