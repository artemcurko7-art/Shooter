using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class RotationToTarget 
    {
        public void Rotate(Transform transform, Vector3 tracker, float smooth)
        {
            Vector3 direction = tracker - transform.position;
            direction.y = 0;
            
            Quaternion target = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, smooth);
        }
    }
}