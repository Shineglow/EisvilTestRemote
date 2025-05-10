public interface IAIBehaviourConfiguration
{
    bool IsPatrolling { get; }
    bool IsPatrolPointStationary { get; }
    bool IsPatrolPointFollowsTheTarget { get; }
    float PatrolZoneRadius { get; }
    float PatrolWaitTime { get; }
    bool IsAggressive { get; }
    float AggressionRadius { get; }
}