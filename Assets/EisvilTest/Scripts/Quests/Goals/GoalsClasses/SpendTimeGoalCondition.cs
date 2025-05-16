using System;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using EisvilTest.Scripts.Configuration.Quests.Conditions;
using EisvilTest.Scripts.Configuration.Quests.Conditions.Inheritors;
using UnityEngine;

namespace EisvilTest.Scripts.Quests.Goals.GoalsClasses
{
    public class SpendTimeGoalCondition : GoalCondition
    {
        private readonly StringBuilder _stringBuilder;
        private CancellationTokenSource _cts;
        private readonly SpendTimeGoalConfiguration _timeSpendConfiguration;
        private int secondsWithoutMilliseconds;
        private float secondsSpend;
        private int stepsCount;
        private const int StringRebuildStep = 5;

        public SpendTimeGoalCondition(SpendTimeGoalConfiguration configuration) : base(configuration)
        {
            _timeSpendConfiguration = configuration;
            _stringBuilder = new StringBuilder();
        }
        
        public override void Start()
        {
            base.Start();
            secondsWithoutMilliseconds = 0;
            secondsSpend = 0;
            stepsCount = 0;
            _cts = new CancellationTokenSource();
            _ = WaitSeconds(_cts.Token);
        }

        private async UniTask WaitSeconds(CancellationToken token)
        {
            try
            {
                await UniTask.WhenAny(UniTask.Delay(
                    Convert.ToInt32(_timeSpendConfiguration.TimeToSpendInSeconds * 1000),
                    cancellationToken: token), UpdateTimeVariable());
            }
            catch (OperationCanceledException oce)
            {
                Debug.Log("Operation was canceled from source.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            SetGoalAchieved();
            
            async UniTask UpdateTimeVariable()
            {
                while (!token.IsCancellationRequested)
                {
                    secondsSpend += Time.deltaTime;
                    if (secondsWithoutMilliseconds < (int)secondsSpend)
                    {
                        secondsWithoutMilliseconds = (int)secondsSpend;
                        if (secondsWithoutMilliseconds / StringRebuildStep > stepsCount)
                        {
                            stepsCount = secondsWithoutMilliseconds / StringRebuildStep;
                            RebuildDescription();
                        }
                    }
                    await UniTask.Yield();
                }
            }
        }
        
        private void RebuildDescription()
        {
            _stringBuilder.Clear();
            if (!string.IsNullOrWhiteSpace(_timeSpendConfiguration.Description))
            {
                _stringBuilder.Append($"{_timeSpendConfiguration.Description} ");
            }

            _stringBuilder.Append("(");
            if (secondsWithoutMilliseconds / 60 > 0)
            {
                var minutesSpendDescription = string.Format(GoalsStringsTemplates.TimeSpend_TimesSpendedMinutes, secondsWithoutMilliseconds/60);
                _stringBuilder.Append(minutesSpendDescription);
            }
            if (secondsWithoutMilliseconds % 60 > 0)
            {
                var secondsSpendDescription = string.Format(GoalsStringsTemplates.TimeSpend_TimesSpendedSeconds, secondsWithoutMilliseconds%60);
                _stringBuilder.Append(secondsSpendDescription);
            }
            _stringBuilder.Append(")");
            
            UpdateDescription(_stringBuilder.ToString());
        }

        public override void DisableMainGoalTracking()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}