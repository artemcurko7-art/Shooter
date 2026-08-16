namespace Game.Scripts.PlayerContext.Input
{
    public class MobileInput : IInput
    {
        private readonly FixedJoystick _joystick;
    
        public MobileInput(FixedJoystick joystick)
        {
            _joystick = joystick;
        }
    
        public float Horiontal { get; private set; }
        public float Vertical { get; private set; }
    
        public void Update()
        {
            Horiontal = _joystick.Horizontal;
            Vertical = _joystick.Vertical;
        }
    }
}