using System.Collections.Generic;
using EisvilTest.Scripts.General;
using EisvilTest.Scripts.Quests.Goals;

namespace EisvilTest.Scripts.Configuration.Quests.Conditions
{
    public abstract class GoalConditionConfiguration
    {
        public abstract string Description { get; }
        public EBoolenOperation OperationBetweenMainGoalAndSubGoals;
        public List<GoalConditionConfiguration> SubGoalsConfiguration;
        public EBoolenOperation SubGoalsOperation;
        
        public abstract GoalCondition GetGoalConditionInstance();
    }
}