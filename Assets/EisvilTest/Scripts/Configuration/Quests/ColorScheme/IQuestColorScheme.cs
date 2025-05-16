using UnityEngine;

namespace EisvilTest.Scripts.Configuration.Quests.ColorScheme
{
    public interface IQuestColorScheme
    {
        Color QuestInProgressColor { get; }
        Color QuestCompleted { get; }
    }
}