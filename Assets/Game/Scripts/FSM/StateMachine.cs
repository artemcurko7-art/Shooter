namespace Game.Scripts.FSM
{
    public class StateMachine 
    {
        protected readonly State State;

        public StateMachine(State state)
        {
            State = state;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}
