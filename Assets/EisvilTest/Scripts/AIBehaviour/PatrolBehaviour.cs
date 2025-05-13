using System.Threading.Tasks;
using EisvilTest.Scripts.AIBehaviour;
using UnityEngine;

public class PatrolBehaviour : AIBehaviourAbstract
{
    private bool _isPurseTarget; // преследует
    private bool _isPatrol; // патрулирует
    private bool _isStay; // стоит

    private bool _isPatrolTargetWasInitialized;
    private Vector3 _patrolTarget;
    private Vector3 _moveDirection;
    
    private AIContext _context;

    public override void Enter(AIContext context)
    {
        _context = context;
        _context.AggressionZone.AddActionToBoth(OnAggressionZoneEnter, OnAggressionZoneExit, LayerMask.GetMask("Player"));
    }

    public override void Update()
    {
        if (IsReachTarget())
        {
            // _aiMachine.
        }

        if (!_isPatrolTargetWasInitialized)
        {
            _patrolTarget = GetRandomPatrolPoint();
            _isPatrolTargetWasInitialized = true;
        }
        
        _moveDirection = _patrolTarget.normalized;
    }

    private bool IsReachTarget()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
        _isPatrolTargetWasInitialized = false;
        _context = new();
    }

    private async Task Patrol() // async
    {
        // whyle(_isPatrol){
        //     Choose new point
        //     whyle(not reach the target) Move to target
        //     Stay on point some time
        // }

        await Task.CompletedTask;
    }

    private Vector3 GetRandomPatrolPoint()
    {
        var nextDirectionAngle = Vector3.SignedAngle((_context.PatrolPoint.position - _context.PawnTransform.position).normalized, Vector3.right, Vector3.up);
        var nextDirection = Quaternion.AngleAxis(nextDirectionAngle + Random.Range(-45f, 45f), Vector3.up).eulerAngles;
        var targetPoint = nextDirection * Random.Range(_context.BehaviourConfiguration.PatrolZoneRadius / 2, _context.BehaviourConfiguration.PatrolZoneRadius * 2);
        
        var points = LineIntersactions.LineCircleIntersections(_context.PawnTransform.position, targetPoint, _context.PatrolPoint.position, _context.BehaviourConfiguration.PatrolZoneRadius);
        var point = points[0];
        const float SqrMagnitudeEpsilon = 0.02f;
        
        if ((point - _context.PawnTransform.position).sqrMagnitude < SqrMagnitudeEpsilon // if target point same as start pos
            || (point.normalized + nextDirection.normalized).sqrMagnitude < SqrMagnitudeEpsilon) // or target point in opposit direction
        {
            point = points[0];
        }

        return point;
    }

    private void OnAggressionZoneEnter(GameObject interactedObject)
    {
        _isPurseTarget = true;
    }

    private void OnAggressionZoneExit(GameObject interactedObject)
    {
        _isPatrol = true;
    }

    public PatrolBehaviour(AIMachine aiMachine) : base(aiMachine){}
}