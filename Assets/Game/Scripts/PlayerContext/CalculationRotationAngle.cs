using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class CalculationRotationAngle 
    {
        public void Rotate(Transform transform, float horizontal, float vertical, float smooth)
        {
            float angleRad = Mathf.Atan2(horizontal, vertical);
            float angleDeg = angleRad * Mathf.Rad2Deg;
        
            float targetAngle = angleDeg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, smooth);
        }
    }
}
