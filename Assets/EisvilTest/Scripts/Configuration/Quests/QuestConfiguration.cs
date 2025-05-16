using EisvilTest.Scripts.Configuration.Quests.Conditions;

namespace EisvilTest.Scripts.Configuration.Quests
{
    public class QuestConfiguration
    {
        public EQuests QuestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GoalConditionConfiguration GoalConfiguration { get; set; }
    }
}