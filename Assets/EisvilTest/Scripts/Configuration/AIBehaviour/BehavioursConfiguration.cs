using System.Collections.Generic;
using UnityEngine;

public class BehavioursConfiguration : IBehavioursConfiguration
{
    private Dictionary<EAIBehaviour, IAIBehaviourConfiguration> _behaviourToConfiguration;

    public BehavioursConfiguration()
    {
        _behaviourToConfiguration = new()
        {
            {
                EAIBehaviour.Aggressive, new AIBehaviourBuilder()
                    .SetAgression(true)
                    .SetAggressionRadius(float.PositiveInfinity)
                    .GetConfiguration()
            },
            {
                EAIBehaviour.Patrolling, new AIBehaviourBuilder()
                    .SetPatrolling(true)
                    .SetPatrolZoneRadius(5)
                    .SetPatrolWaitTime(3)
                    .ToMain()
                    .SetAgression(true)
                    .SetAggressionRadius(3)
                    .GetConfiguration()
            },
            {
                EAIBehaviour.Tracker, new AIBehaviourBuilder()
                    .SetPatrolling(true)
                    .SetPatrolZoneRadius(3)
                    .SetPatrolPointFollowsNearestOpponent(true)
                    .SetPatrolWaitTime(1)
                    .ToMain()
                    .SetAgression(true)
                    .SetAggressionRadius(5)
                    .GetConfiguration()
            },
        };
    }

    public IAIBehaviourConfiguration GetConfiguration(EAIBehaviour behaviour)
    {
        if(!_behaviourToConfiguration.TryGetValue(behaviour, out var result))
        {
            Debug.LogError($"Attempt to get data on unregistered key: {behaviour.ToString()}");
        }

        return result;
    }
}