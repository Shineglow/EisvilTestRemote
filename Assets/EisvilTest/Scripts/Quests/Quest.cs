using System;
using System.Collections.Generic;
using EisvilTest.Scripts.Configuration.Quests;
using EisvilTest.Scripts.Quests.Goals;

namespace EisvilTest.Scripts.Quests
{
    public class Quest
    {
        public object Id { get; private set; }
        public string Name { get; }
        public EQuests QuestId { get; }
        
        public string QuestDescription { get; private set; }
        private readonly GoalCondition _conditionInstance;
        public IReadOnlyList<GoalProperties> GoalsProperties { get; }
        public event Action<Quest> QuestCompleted; 
        
        public bool IsCompleted { get; private set; }

        public Quest(QuestConfiguration configuration)
        {
            QuestId = configuration.QuestId;
            Name = configuration.Name;
            QuestDescription = configuration.Description;
            _conditionInstance = configuration.GoalConfiguration.GetGoalConditionInstance();
            GoalsProperties = _conditionInstance.UnwrapObservableValuesFromAttachedGoals();
        }

        public void Start(object id)
        {
            Id = id;
            _conditionInstance.GoalAchieved += OnGoalAchieved;
            _conditionInstance.Start();
        }

        private void OnGoalAchieved(GoalCondition goalCondition)
        {
            IsCompleted = true;
            QuestCompleted?.Invoke(this);
        }
    }
}