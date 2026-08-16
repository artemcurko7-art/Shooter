using System;
using System.Collections.Generic;

namespace Game.Scripts.FSM
{
    public class State
    {
        private StateMachine _currentState;

        private readonly Dictionary<Type, StateMachine> _states = new();

        public void AddState(StateMachine stateMachine)
        {
            _states.Add(stateMachine.GetType(), stateMachine);
        }

        public void SetState<T>() where T : StateMachine
        {
            var type = typeof(T);

            if (_currentState != null && _currentState.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var newState))
            {
                _currentState?.Exit();

                _currentState = newState;

                _currentState?.Enter();
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}
