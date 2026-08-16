namespace Game.Scripts.PlayerContext.Input
{
    public class DesktopInput : IInput
    {
        public float Horiontal { get; private set; }
        public float Vertical { get; private set; }
    
        public void Update()
        {
            Horiontal = UnityEngine.Input.GetAxis("Horizontal");
            Vertical = UnityEngine.Input.GetAxis("Vertical");
        }
    }
}