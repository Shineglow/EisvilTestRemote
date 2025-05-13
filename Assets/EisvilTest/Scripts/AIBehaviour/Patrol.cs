using System.Collections;
using System.Threading;
using EisvilTest.Scripts.CharacterSystem;
using EisvilTest.Scripts.Triggers;
using UnityEngine;

namespace EisvilTest.Scripts.AIBehaviour
{
    public class Patrol : MonoBehaviour
    {
        private Character _character;
        private UniversalTrigger _aggressionZone;

        private float _attackRange = 0.3f;
        private float _idleTime = 2f;

        private Vector3? _currentDestination;
        private bool _isChasingPlayer;
        private Coroutine _idleCoroutine;
        private Transform _patrolPoint;
        private IAIPatrolConfiguration _configuration;
        private bool _isNeedToHitTarget;
        private float patrolZoneRadius;
        private Character _targetOpponent;

        public void Init(Character character, IAIPatrolConfiguration configuration)
        {
            _character = character;
            _aggressionZone = new GameObject("AggressionZone").AddComponent<UniversalTrigger>();
            _aggressionZone.transform.SetParent(_character.CharacterTransform);
            _aggressionZone.AddActionToBoth(OnAggressionZoneEnter, OnAggressionZoneExit, LayerMask.GetMask("Player"));
            _patrolPoint = new GameObject("PatrolPoint").transform;
            _patrolPoint.SetParent(_character.CharacterTransform);
            _configuration = configuration;
        }
        
        private void OnAggressionZoneEnter(GameObject interactedObject)
        {
            _targetOpponent = interactedObject.GetComponent<Character>();
            _isChasingPlayer = true;
        }

        private void OnAggressionZoneExit(GameObject interactedObject)
        {
            _targetOpponent = null;
            _isChasingPlayer = false;
        }

        private void Update()
        {
            if (_isChasingPlayer)
            {
                ChasePlayer();
            }
            else
            {
                Wander();
            }
        }
        
        private void ChasePlayer()
        {
            float distance = Vector3.Distance(_character.CharacterTransform.position, _targetOpponent.CharacterTransform.position);
            if (distance <= _attackRange)
            {
                _character.Fire();
            }

            MoveTowards(_character.CharacterTransform.position);
        }

        private void Wander()
        {
            if (_currentDestination == null)
            {
                _currentDestination = PickRandomDestination(_patrolPoint.position, _character.CharacterTransform.position, _configuration.PatrolZoneRadius);
            }

            float distance = Vector3.Distance(transform.position, _currentDestination.Value);
            if (distance < 0.5f)
            {
                if (_idleCoroutine == null)
                    _idleCoroutine = StartCoroutine(IdleAndPickNewPoint());
            }
            else
            {
                MoveTowards(_currentDestination.Value);
            }
        }
        
        private void MoveTowards(Vector3 target)
        {
            _character.Move(target);
        }

        private Vector3 PickRandomDestination(Vector3 patrolPoint, Vector3 selfPosition, float patrolZoneRadius)
        {
            var nextDirectionAngle = Vector3.SignedAngle((patrolPoint - selfPosition).normalized, Vector3.right, Vector3.up);
            var nextRandomDirection = Quaternion.AngleAxis(nextDirectionAngle + Random.Range(-45f, 45f), Vector3.up).eulerAngles;
            var targetPoint = nextRandomDirection * Random.Range(patrolZoneRadius / 2, patrolZoneRadius * 2);
        
            var points = LineIntersactions.LineCircleIntersections(selfPosition, targetPoint, patrolPoint, patrolZoneRadius);
            var point = points[0];
            const float SqrMagnitudeEpsilon = 0.02f;
        
            if ((point - selfPosition).sqrMagnitude < SqrMagnitudeEpsilon // if target point same as start pos
                || (point.normalized + nextRandomDirection.normalized).sqrMagnitude < SqrMagnitudeEpsilon) // or target point in opposit direction
            {
                point = points[0];
            }

            return point;
        }

        private IEnumerator IdleAndPickNewPoint()
        {
            yield return new WaitForSeconds(_idleTime);
            _currentDestination = null;
            _idleCoroutine = null;
        }
    }
}