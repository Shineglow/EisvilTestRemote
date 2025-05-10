public class AIBehaviourBuilder : IAIBehaviourBuilder, IPatrollingBuilder, IAgressionBuilder
{
    private BehaviourConfigurationGeneral configuration;
    public AIBehaviourBuilder Self { get; private set; }

    public AIBehaviourBuilder()
    {
        configuration = new BehaviourConfigurationGeneral();
        Self = this;
    }

    public IPatrollingBuilder SetPatrolling(bool isPatrolling)
    {
        configuration.IsPatrolling = isPatrolling;
        return this;
    }

    public IPatrollingBuilder SetPatrolPointStationary(bool value)
    {
        configuration.IsPatrolPointStationary = value;
        return this;
    }

    public IPatrollingBuilder SetPatrolPointFollowsNearestOpponent(bool value)
    {
        configuration.IsPatrolPointFollowsTheTarget = value;
        return this;
    }

    public IPatrollingBuilder SetPatrolZoneRadius(float value)
    {
        configuration.PatrolZoneRadius = value;
        return this;
    }

    public IPatrollingBuilder SetPatrolWaitTime(float value)
    {
        configuration.PatrolWaitTime = value;
        return this;
    }

    public IAgressionBuilder SetAgression(bool isAgression)
    {
        configuration.IsAggressive = isAgression;
        return this;
    }

    public IAgressionBuilder SetAggressionRadius(float radius)
    {
        configuration.AggressionRadius = radius;
        return this;
    }

    public IAIBehaviourBuilder ToMain()
    {
        return this;
    }

    public IAIBehaviourConfiguration GetConfiguration()
    {
        return configuration;
    }
}