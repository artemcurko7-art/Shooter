using UnityEngine;

namespace Game.Scripts.PlayerContext.GameInput
{
    public class DesktopInput : IInput
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
    
        public void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                Horizontal = 1;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                Horizontal = -1;
            }
            
            if (Input.GetKey(KeyCode.W))
            {
                Vertical = 1;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                Vertical = -1;
            }

            if (Input.GetKey(KeyCode.D) == false && Input.GetKey(KeyCode.A) == false)
            {
                Horizontal = 0;
            }
            
            if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.S) == false)
            {
                Vertical = 0;
            }
            
            //Horizontal = UnityEngine.Input.GetAxis("Horizontal");
            //Vertical = UnityEngine.Input.GetAxis("Vertical"); // может быть пересмотреть
        }
    }
}