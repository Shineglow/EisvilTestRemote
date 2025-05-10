public class BehaviourConfigurationGeneral : IAIBehaviourConfiguration
{
    // Patrol
    public bool IsPatrolling { get; set; }
    public bool IsPatrolPointStationary { get; set;}
    public bool IsPatrolPointFollowsTheTarget { get; set;}
    public float PatrolZoneRadius { get; set;}
    public float PatrolWaitTime { get; set; }

    // Agression
    public bool IsAggressive { get; set;}
    public float AggressionRadius { get; set;}
}