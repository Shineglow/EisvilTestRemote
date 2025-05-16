using EisvilTest.Scripts.Configuration.Quests;
using EisvilTest.Scripts.Configuration.Quests.Conditions;

namespace EisvilTest.Scripts.Quests.Goals.GoalsClasses
{
    public class GoalAggregatorCondition : GoalCondition
    {
        public GoalAggregatorCondition(GoalConditionConfiguration configuration) : base(configuration){}

        public override void DisableMainGoalTracking()
        {
            foreach (GoalCondition attachedGoalCondition in AttachedGoalConditions)
            {
                attachedGoalCondition.DisableMainGoalTracking();
            }
        }
    }
}