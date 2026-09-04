namespace Game.Scripts.PlayerContext.GameInput
{
    public class MobileInput : IInput
    {
        private readonly FixedJoystick _joystick;
    
        public MobileInput(FixedJoystick joystick)
        {
            _joystick = joystick;
        }
    
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
    
        public void Update()
        {
            Horizontal = _joystick.Horizontal;
            Vertical = _joystick.Vertical;
        }
    }
}