public interface IAIBehaviourBuilder : IGetConfiguration
{
    IPatrollingBuilder SetPatrolling(bool isPatrolling);
    IAgressionBuilder SetAgression(bool isAgression);
}