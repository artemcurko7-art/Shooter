using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.FSM.Death
{
    public abstract class StateDeath : StateMachine
    {
        public StateType Type => StateType.Death;

        public StateDeath(State state) : base(state) { }
    }
}