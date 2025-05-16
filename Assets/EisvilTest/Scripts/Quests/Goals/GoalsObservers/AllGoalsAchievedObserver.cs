using System.Collections.Generic;

namespace EisvilTest.Scripts.Quests.Goals.GoalsObservers
{
    public class AllGoalsAchievedObserver : GoalsObserver
    {
        public int GoalsAchieved { get; private set; }
        
        public override void Setup(IReadOnlyCollection<GoalCondition> goals)
        {
            base.Setup(goals);
            foreach (var goal in goals)
            {
                goal.GoalAchieved += OnGoalAchievedLocal;
            }
        }

        private void OnGoalAchievedLocal(GoalCondition obj)
        {
            GoalsAchieved++;
            if (GoalsAchieved >= _goals.Count)
            {
                OnGoalAchieved();
            }
        }
    }
}