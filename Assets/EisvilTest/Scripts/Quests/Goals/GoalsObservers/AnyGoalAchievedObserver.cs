using System.Collections.Generic;

namespace EisvilTest.Scripts.Quests.Goals.GoalsObservers
{
    public class AnyGoalAchievedObserver : GoalsObserver
    {
        public int GoalsAchieved { get; private set; }

        public override void Start()
        {
            base.Start();
            foreach (var goal in _goals)
            {
                goal.GoalAchieved += OnGoalAchievedLocal;
            }
        }

        private void OnGoalAchievedLocal(GoalCondition obj)
        {
            OnGoalAchieved();
        }
    }
}