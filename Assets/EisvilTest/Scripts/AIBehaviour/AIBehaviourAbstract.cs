using EisvilTest.Scripts.Input;
using EisvilTest.Scripts.Triggers;
using UnityEngine;

namespace EisvilTest.Scripts.AIBehaviour
{
    public abstract class AIBehaviourAbstract : MonoBehaviour, IAIBehaviour
    {
        protected AIMachine _aiMachine;
        
        public AIBehaviourAbstract(AIMachine aiMachine)
        {
            _aiMachine = aiMachine;
        }
        
        public abstract void Enter(AIContext context);
        public abstract void Update();
        public abstract void Exit();
    }

    public struct AIContext
    {
        public IAIBehaviourConfiguration BehaviourConfiguration;
        public UniversalTrigger AggressionZone;
        public Transform PawnTransform;
        public Transform PatrolPoint;
        public ActionDetails<Vector2> Move;
        public ActionDetails<bool> Fire;
        public ActionDetails<bool> Interaction;
    }
}