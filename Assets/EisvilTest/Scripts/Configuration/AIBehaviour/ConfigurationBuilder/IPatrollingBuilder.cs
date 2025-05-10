public interface IPatrollingBuilder : IToMainBuilder, IGetConfiguration
{
    IPatrollingBuilder SetPatrolPointStationary(bool value);
    IPatrollingBuilder SetPatrolPointFollowsNearestOpponent(bool value);
    IPatrollingBuilder SetPatrolZoneRadius(float value);
    IPatrollingBuilder SetPatrolWaitTime(float value);
}