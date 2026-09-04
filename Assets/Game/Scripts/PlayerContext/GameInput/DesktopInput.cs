namespace Game.Scripts.PlayerContext.GameInput
{
    public class DesktopInput : IInput
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
    
        public void Update()
        {
            Horizontal = UnityEngine.Input.GetAxis("Horizontal");
            Vertical = UnityEngine.Input.GetAxis("Vertical");
        }
    }
}