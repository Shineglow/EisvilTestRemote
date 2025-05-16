using EisvilTest.Scripts.Quests.Goals;
using EisvilTest.Scripts.Quests.Goals.GoalsClasses;

namespace EisvilTest.Scripts.Configuration.Quests.Conditions.Inheritors
{
    public class SpendTimeGoalConfiguration : GoalConditionConfiguration
    {
        public string DescriptionSetter;
        public float TimeToSpendInSeconds;

        public override string Description => DescriptionSetter;
        
        public override GoalCondition GetGoalConditionInstance()
        {
            return new SpendTimeGoalCondition(this);
        }
    }
}