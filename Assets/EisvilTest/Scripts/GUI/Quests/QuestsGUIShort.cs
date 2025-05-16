using System.Collections.Generic;
using System.Linq;
using EisvilTest.Scripts.Quests.Goals;
using EisvilTest.Scripts.ResourcesManagement;
using EisvilTest.Scripts.ResourcesManagement.Enums;
using EisvilTest.Scripts.ResourcesManagement.Pools;
using TMPro;
using UnityEngine;

namespace EisvilTest.Scripts.GUI.Quests
{
    [RequireComponent(typeof(RectTransform))]
    public class QuestsGUIShort : MonoBehaviour
    {
        private RectTransform _rectTransform;
        [SerializeField] private RectTransform content;
        private ResourceManager _resourceManager;

        private Dictionary<string, QuestItem> _questNameToItem = new();
        private IPool<QuestItem> _itemsPool;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _resourceManager = CompositionRoot.GetResourceManager();
            var poolsAggregator = CompositionRoot.GetPoolsAggregator();
            if (!poolsAggregator.ContainPool<QuestItem>())
            {
                _itemsPool = poolsAggregator.CreatePool<QuestsGUIShort, QuestItem>(MakeItem, OnReturnFunction, 10);
            }
        }

        public void AddQuest(string questName, IReadOnlyList<GoalProperties> goals)
        {
            var newItem = _itemsPool.Get();
            newItem.Init();
            newItem.SetName(questName);
            newItem.SetGoals(goals);
            _questNameToItem.Add(questName, newItem);
            newItem.RectTransform.SetParent(content);
            newItem.gameObject.SetActive(true);
        }

        public void RemoveQuest(string questName)
        {
            if (_questNameToItem.TryGetValue(questName, out var value))
            {
                _itemsPool.ReturnToPool(value);
                _questNameToItem.Remove(questName);
            }
        }

        private QuestItem MakeItem()
        {
            var item = _resourceManager.CreatePrefabInstance<QuestItem, EGuiQuestsPrefabs>(EGuiQuestsPrefabs.QuestItemDefault);
            return item;
        }
        
        private void OnReturnFunction(QuestItem obj)
        {
            obj.Free();
            obj.gameObject.SetActive(false);
        }

        public void SetParent(RectTransform parent)
        {
            _rectTransform.SetParent(parent);
            _rectTransform.offsetMin = new Vector2(0, 280);
            _rectTransform.offsetMax = new Vector2(0, -290);
        }
    }
}