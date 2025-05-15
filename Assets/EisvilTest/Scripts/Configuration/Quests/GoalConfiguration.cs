using System.Collections.Generic;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.General;
using EisvilTest.Scripts.Quests;
using EisvilTest.Scripts.Quests.Goals;

namespace EisvilTest.Scripts.Configuration.Quests
{
    public class GoalConfiguration
    {
        public string Description;
        public GoalConditionConfiguration GoalCondition;
        public EBoolenOperation OperationBetweenMainGoalAndSubGoals;
        public List<GoalConfiguration> SubGoalsConfiguration;
        public EBoolenOperation SubGoalsOperation;
    }

    public abstract class GoalConditionConfiguration
    {
        public abstract string Description { get; }
        public abstract GoalCondition GetGoalConditionInstance();
    }

    public class KillsGoalConfiguration : GoalConditionConfiguration
    {
        public string DescriptionSetter;
        public ECharacter EnemyType;
        public int KillsCount;

        public override string Description => DescriptionSetter;
        
        public override GoalCondition GetGoalConditionInstance()
        {
            throw new System.NotImplementedException();
        }
    }
}