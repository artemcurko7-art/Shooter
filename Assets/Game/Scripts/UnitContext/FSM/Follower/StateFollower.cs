namespace Game.Scripts.UnitContext.FSM.Follower
{
    public abstract class StateFollower : StateMachine
    {
        public StateFollower(State state) : base(state) { }
    }
}