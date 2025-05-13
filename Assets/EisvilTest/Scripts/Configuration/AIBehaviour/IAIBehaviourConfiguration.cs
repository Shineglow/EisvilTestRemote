public interface IAIBehaviourConfiguration : IAIPatrolConfiguration, IAIAggressionConfiguration {}

public interface IAIPatrolConfiguration
{
    bool IsPatrolling { get; }
    bool IsPatrolPointStationary { get; }
    bool IsPatrolPointFollowsTheTarget { get; }
    float PatrolZoneRadius { get; }
    float PatrolWaitTime { get; }
}

public interface IAIAggressionConfiguration
{
    bool IsAggressive { get; }
    float AggressionRadius { get; }
}