using System;
using System.Collections.Generic;

namespace EisvilTest.Scripts.FSM
{
    public class FSM
    {
        private FSMState StateCurrent { get; set; }
        
        private Dictionary<Type, FSMState> _fsmStates = new Dictionary<Type, FSMState>();

        public void AddState(FSMState state)
        {
            _fsmStates.Add(state.GetType(), state);
        }

        public void SetState<T>()
        {
            var type = typeof(T);

            if (StateCurrent != null && StateCurrent.GetType() == type)
            {
                return;
            }

            if (_fsmStates.TryGetValue(type, out var value))
            {
                StateCurrent?.Exit();
                StateCurrent = value;
                StateCurrent.Enter();
            }
        }

        public void Update()
        {
            StateCurrent?.Update();
        }
    }
}