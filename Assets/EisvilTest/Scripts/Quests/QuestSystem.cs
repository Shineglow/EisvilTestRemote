using System.Collections.Generic;
using EisvilTest.Scripts.Configuration;
using EisvilTest.Scripts.Configuration.Quests;
using EisvilTest.Scripts.GUI.Quests;
using UnityEngine;

namespace EisvilTest.Scripts.Quests
{
    public class QuestSystem
    {
        private readonly ConfigurationBase<EQuests, QuestConfiguration> _questsConfiguration;
        private readonly Dictionary<object, PlayerQuestsRecord> _playerToQuestsData;
        private readonly QuestsGUIShort _questsGUIShort;
        private readonly GuiMain _guiMain;

        public QuestSystem()
        {
            _playerToQuestsData = new();
            _questsConfiguration = CompositionRoot.GetQuestsConfiguration();
            _guiMain = CompositionRoot.GetGuiMain();
            _questsGUIShort = CompositionRoot.GetQuestsGUIShort();
            _questsGUIShort.SetParent(_guiMain.MainRect);
        }

        public Quest StartQuest(object id, EQuests questId)
        {
            var questConf = _questsConfiguration.GetData(questId);
            var quest = new Quest(questConf);
            _questsGUIShort.AddQuest(quest.Name, quest.GoalsProperties);

            var playerQuestsRecord = GetPlayerQuestsRecord(id);
            playerQuestsRecord.ActiveQuests.Add(questId, quest);

            quest.QuestCompleted += OnQuestCompleted;
            Debug.Log($"Quest {questConf.Name} begin!");
            quest.Start(id);
            return quest;
        }

        private void OnQuestCompleted(Quest completedQuest)
        {
            var playerQuestsRecord = GetPlayerQuestsRecord(completedQuest.Id);
            playerQuestsRecord.ActiveQuests.Remove(completedQuest.QuestId);
            playerQuestsRecord.CompleatedQuests.Add(completedQuest.QuestId, completedQuest);
            Debug.Log($"Quest {completedQuest.Name} completed!!!");
        }

        private PlayerQuestsRecord GetPlayerQuestsRecord(object id)
        {
            if (!_playerToQuestsData.TryGetValue(id, out var result))
            {
                result = new PlayerQuestsRecord();
            }

            return result;
        }
    }

    public class PlayerQuestsRecord
    {
        public Dictionary<EQuests, Quest> ActiveQuests = new();
        public Dictionary<EQuests, Quest> CompleatedQuests = new();
    }
}