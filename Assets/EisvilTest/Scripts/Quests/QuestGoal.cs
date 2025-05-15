using System;
using System.Collections.Generic;
using EisvilTest.Scripts.Configuration.Quests;

namespace EisvilTest.Scripts.Quests
{
    public abstract class QuestGoal
    {
        public abstract event Action<QuestGoal> GoalAchieved;

        private List<QuestGoal> SubGoals = new();

        public QuestGoal(GoalConfiguration configuration)
        {
            
        }
    }
}