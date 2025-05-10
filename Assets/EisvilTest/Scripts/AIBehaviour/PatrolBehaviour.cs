using EisvilTest.Scripts.CharacterSystem;
using EisvilTest.Scripts.Triggers;
using System.Threading.Tasks;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour, IAIBehaviour, ICharacterController
{
    private IAIBehaviourConfiguration _behaviourConfiguration;
    private UniversalTrigger _agressionZone;
    private Transform _pawnTransform;
    private Transform _patrolPoint;
    
    private bool _isPurseTarget;
    private bool _isPatrol;
    private bool _isStay;
    private IControllable _controllable;
    private Vector3 _patrolTarget;

    public PatrolBehaviour(IAIBehaviourConfiguration behaviourConfiguration, UniversalTrigger agressionZone, Transform pawnTransform, Transform patrolPoint)
    {
        _behaviourConfiguration = behaviourConfiguration;
        _agressionZone = agressionZone;
        agressionZone.transform.SetParent(pawnTransform);
        agressionZone.transform.localPosition = Vector3.zero;
        agressionZone.AddActionToBoth(OnAgressionZoneEnter, OnAgressionZoneExit, LayerMask.GetMask("Player"));
        _pawnTransform = pawnTransform;
        _patrolPoint = patrolPoint;
    }

    private void OnAgressionZoneEnter(GameObject interactedObject)
    {
        _isPurseTarget = true;
    }

    private void OnAgressionZoneExit(GameObject interactedObject)
    {
        _isPatrol = true;
    }

    public void Update()
    {
        if(_isPatrol && !_isPurseTarget)
        {
            _controllable.MoveToPosition(_patrolTarget);
        }
    }

    private async Task Patrol() // async
    {
        while(_isPatrol && !_isPurseTarget)
        {
            _patrolTarget = GetRandomPatrolPoint();
            
        }
        // whyle(_isPatrol){
        //     Choose new point
        //     whyle(not reach the target) Move to target
        //     Stay on point some time
        // }

        await Task.CompletedTask;
    }

    private Vector3 GetRandomPatrolPoint()
    {
        var nextDirectionAngle = Vector3.SignedAngle((_patrolPoint.position - _pawnTransform.position).normalized, Vector3.right, Vector3.up);
        var nextDirection = Quaternion.AngleAxis(nextDirectionAngle + (float)Random.Range(-45f, 45f), Vector3.up).eulerAngles 
            * Random.Range(_behaviourConfiguration.PatrolZoneRadius*0.5f, _behaviourConfiguration.PatrolZoneRadius + _behaviourConfiguration.PatrolZoneRadius);
        
        var points = LineIntersactions.LineCircleIntersections(_pawnTransform.position, nextDirection, _patrolPoint.position, _behaviourConfiguration.PatrolZoneRadius);
        var point = points[0];
        const float SqrMagnitudeEpsilon = 0.02f;
        if ((point - _pawnTransform.position).sqrMagnitude < SqrMagnitudeEpsilon // if target point same as start pos
            || (point.normalized + nextDirection.normalized).sqrMagnitude < SqrMagnitudeEpsilon) // or target point in opposit direction
        {
            point = points[0];
        }

        return point;
    }

    public async Task StayOnPoint()
    {
        float timer = _behaviourConfiguration.PatrolWaitTime;
        while(timer > 0 || !_isPurseTarget)
        {
            await Task.Yield();
            timer -= Time.deltaTime;
        }
    }

    public void SetControllable(IControllable controllable)
    {
        _controllable = controllable;
    }
}