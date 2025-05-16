using System;
using System.Collections.Generic;
using EisvilTest.Scripts.Configuration.Quests.Conditions;
using EisvilTest.Scripts.General;
using EisvilTest.Scripts.Quests.Goals.GoalsObservers;

namespace EisvilTest.Scripts.Quests.Goals
{
    public abstract class GoalCondition
    {
        private readonly ObservableValue<string> _description = new();
        public IObservableValueReadOnly<string> Description => _description;
        public event Action<GoalCondition> GoalAchieved;
        
        protected List<GoalCondition> AttachedGoalConditions;
        private GoalConditionConfiguration _configuration;
        private GoalsObserver _goalsObserver;

        private GoalPropertiesSetter _propertiesSetter { get; }
        public GoalProperties GoalProperties { get; }
        
        public bool MainGoalAchieved { get; private set; }
        public bool SecondaryGoalsAchieved { get; private set; }

        public GoalCondition(GoalConditionConfiguration configuration)
        {
            _propertiesSetter = new();
            GoalProperties = new(_propertiesSetter);
            _propertiesSetter.Description.Value = configuration.Description;
            _propertiesSetter.GoalAchieved.Value = false;
            _configuration = configuration;
        }

        public virtual void Start()
        {
            if (_configuration.SubGoalsConfiguration != null)
            {
                AttachedGoalConditions = new(_configuration.SubGoalsConfiguration.Count);
                foreach (var goalConf in _configuration.SubGoalsConfiguration )
                {
                    AttachedGoalConditions.Add(goalConf.GetGoalConditionInstance());
                }

                _goalsObserver = _configuration.SubGoalsOperation switch
                {
                    EBoolenOperation.And => new AllGoalsAchievedObserver(),
                    EBoolenOperation.Or => new AnyGoalAchievedObserver(),
                    _ => throw new ArgumentOutOfRangeException()
                };
                _goalsObserver.Setup(AttachedGoalConditions);
                _goalsObserver.Start();
                _goalsObserver.GoalAchieved += OnSecondaryGoalsAchieved;
            }
            else
            {
                SecondaryGoalsAchieved = true;
            }
        }

        private void OnSecondaryGoalsAchieved(IReadOnlyCollection<GoalCondition> goals)
        {
            SecondaryGoalsAchieved = true;
            SetGoalAchieved();
        }

        protected void SetGoalAchieved()
        {
            if (_propertiesSetter.GoalAchieved.Value) return;
            MainGoalAchieved = true;
            switch (_configuration.OperationBetweenMainGoalAndSubGoals)
            {
                case EBoolenOperation.And:
                    if (!MainGoalAchieved || !SecondaryGoalsAchieved)
                        return;
                    break;
                case EBoolenOperation.Or:
                    if (!MainGoalAchieved && !SecondaryGoalsAchieved)
                        return;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _propertiesSetter.GoalAchieved.Value = true;

            DisableProgressTracking();
            
            GoalAchieved?.Invoke(this);
        }

        private void DisableProgressTracking()
        {
            DisableMainGoalTracking();
            if(AttachedGoalConditions != null)
            {
                foreach (var attachedGoalCondition in AttachedGoalConditions)
                {
                    attachedGoalCondition.DisableMainGoalTracking();
                }
            }
        }

        public abstract void DisableMainGoalTracking();

        public IReadOnlyList<GoalProperties> UnwrapObservableValuesFromAttachedGoals()
        {
            List<GoalProperties> descriptions = new();
            AddDescription(descriptions);
            return descriptions;
        }

        private void AddDescription(ICollection<GoalProperties> descriptions)
        {
            descriptions.Add(GoalProperties);
            if (AttachedGoalConditions != null)
            {
                foreach (var attachedGoal in AttachedGoalConditions)
                {
                    attachedGoal.AddDescription(descriptions);
                }
            }
        }

        protected void UpdateDescription(string newDescription)
        {
            _propertiesSetter.Description.Value = newDescription;
        }
    }
}