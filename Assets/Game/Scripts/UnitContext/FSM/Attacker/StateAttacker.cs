using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.FSM.Attacker
{
    public abstract class StateAttacker : StateMachine
    {
        public StateType Type => StateType.Attacker;
        
        public StateAttacker(State state) : base(state) { }
    }
}