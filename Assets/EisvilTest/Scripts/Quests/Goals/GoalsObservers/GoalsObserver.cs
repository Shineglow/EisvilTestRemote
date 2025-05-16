using System;
using System.Collections.Generic;

namespace EisvilTest.Scripts.Quests.Goals.GoalsObservers
{
    public abstract class GoalsObserver
    {
        protected IReadOnlyCollection<GoalCondition> _goals { get; private set; }
        public event Action<IReadOnlyCollection<GoalCondition>> GoalAchieved;

        public virtual void Setup(IReadOnlyCollection<GoalCondition> goals)
        {
            _goals = goals;
        }

        public virtual void Start()
        {
            foreach (var goalCondition in _goals)
            {
                goalCondition.Start();
            }
        }

        protected void OnGoalAchieved()
        {
            GoalAchieved?.Invoke(_goals);
        }
    }
}