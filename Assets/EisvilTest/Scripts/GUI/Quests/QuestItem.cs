using System;
using System.Collections.Generic;
using EisvilTest.Scripts.Configuration;
using EisvilTest.Scripts.Configuration.Quests;
using EisvilTest.Scripts.Configuration.Quests.ColorScheme;
using EisvilTest.Scripts.Quests.Goals;
using EisvilTest.Scripts.ResourcesManagement;
using EisvilTest.Scripts.ResourcesManagement.Enums;
using EisvilTest.Scripts.ResourcesManagement.Pools;
using TMPro;
using UnityEngine;

namespace EisvilTest.Scripts.GUI.Quests
{
    [RequireComponent(typeof(RectTransform))]
    public class QuestItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private RectTransform targetsParent;
        private ResourceManager _resourceManager;
        private Dictionary<IObservableValueReadOnly<string>, Action<string, string>> _observableToActionReference = new ();
        private IPool<TextMeshProUGUI> _textsPool;

        private RectTransform rectTransform;
        private bool _initialized;
        private IQuestColorScheme _questsColorScheme;
        public RectTransform RectTransform => rectTransform;

        public void Init()
        {
            if (_initialized) return;
            _initialized = true;

            _questsColorScheme = CompositionRoot.GetQuestsColorScheme();
            rectTransform = GetComponent<RectTransform>();
            _resourceManager = CompositionRoot.GetResourceManager();
            var poolsAggregator = CompositionRoot.GetPoolsAggregator();
            _textsPool = poolsAggregator.ContainPool<QuestItem>() 
                ? poolsAggregator.GetPool<QuestItem, TextMeshProUGUI>() 
                : poolsAggregator.CreatePool<QuestItem, TextMeshProUGUI>(MakeTextField, OnReturnFunction, 10);
        }

        public void Free()
        {
            foreach (var (observable, act) in _observableToActionReference)
            {
                observable.ValueChanged -= act;
            }
            _observableToActionReference.Clear();
        }

        public void SetName(string questName)
        {
            nameText.text = questName;
        }
        
        public void SetGoals(IEnumerable<GoalProperties> goals)
        {
            foreach (var goal in goals)
            {
                var textField = _textsPool.Get();
                textField.text = goal.Description.Value;
                Action<string, string> updateBoundedField = (_old, _new) => textField.text = _new;
                textField.transform.SetParent(targetsParent);
                textField.gameObject.SetActive(true);
                
                goal.Description.ValueChanged += updateBoundedField;
                _observableToActionReference.Add(goal.Description, updateBoundedField);
                textField.color = _questsColorScheme.QuestInProgressColor;
                goal.GoalAchieved.ValueChanged += (_old, _new) => { textField.color = _questsColorScheme.QuestCompleted; };
            }
        }
        
        private TextMeshProUGUI MakeTextField()
        {
            var item = _resourceManager.CreatePrefabInstance<TextMeshProUGUI, EGuiQuestsPrefabs>(EGuiQuestsPrefabs.TargetTextField);
            return item;
        }

        private void OnReturnFunction(TextMeshProUGUI item)
        {
            item.transform.parent = null;
            item.gameObject.SetActive(false);
        }
    }
}