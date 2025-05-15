using System;

namespace EisvilTest.Scripts.Quests.Goals
{
    public abstract class GoalCondition
    {
        public event Action<GoalCondition> GoalAchieved;
        public bool IsGoalAchieved { get; private set; }

        protected void SetGoalAchieved()
        {
            if (IsGoalAchieved) return;
            IsGoalAchieved = true;
            GoalAchieved?.Invoke(this);

            OnGoalAchieved();
        }

        protected abstract void OnGoalAchieved();
    }
}