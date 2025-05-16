using UnityEngine;

namespace EisvilTest.Scripts.Configuration.Quests.ColorScheme
{
    public class QuestColorScheme : IQuestColorScheme
    {
        public Color QuestInProgressColor {get;} = Color.black;
        public Color QuestCompleted {get;} = Color.green;
    }
}