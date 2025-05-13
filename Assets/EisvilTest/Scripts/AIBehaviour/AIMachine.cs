using EisvilTest.Scripts.Input;
using EisvilTest.Scripts.Triggers;
using UnityEngine;

namespace EisvilTest.Scripts.AIBehaviour
{
    public class AIMachine : MonoBehaviour
    {
        private readonly InputAbstraction _inputAbstraction;

        private AIContext _context;
        private readonly ActionDetails<Vector2> _move;
        private readonly ActionDetails<bool> _fire;
        private readonly ActionDetails<bool> _interaction;

        public AIMachine(IAIBehaviourConfiguration behaviourConfiguration, UniversalTrigger aggressionZone, Transform pawnTransform, Transform patrolPoint)
        {
            _context = new AIContext()
            {
                Fire = new ActionDetails<bool>(),
                Interaction = new ActionDetails<bool>(),
                Move = new ActionDetails<Vector2>(),
                AggressionZone = aggressionZone,
                BehaviourConfiguration = behaviourConfiguration,
                PatrolPoint = patrolPoint,
                PawnTransform = pawnTransform,
            };
            _inputAbstraction = new InputAbstraction(_context.Move, _context.Interaction, _context.Fire);
            
            aggressionZone.transform.SetParent(pawnTransform);
            aggressionZone.transform.localPosition = Vector3.zero;
        }

        public void Update()
        {
            
        }
    }
}