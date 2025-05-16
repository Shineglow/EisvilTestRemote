using EisvilTest.Scripts.Quests.Goals;
using EisvilTest.Scripts.Quests.Goals.GoalsClasses;

namespace EisvilTest.Scripts.Configuration.Quests.Conditions
{
    public class AggregatorConditionConfiguration : GoalConditionConfiguration
    {
        public string DescriptionSetter;
        public override string Description => DescriptionSetter;
        public override GoalCondition GetGoalConditionInstance()
        {
            return new GoalAggregatorCondition(this);
        }
    }
}