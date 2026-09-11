using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class RotationToTarget 
    {
        public void Rotate(Transform transform, Vector3 tracker, float smooth)
        {
            tracker.y = 0;
            
            Quaternion target = Quaternion.LookRotation(tracker);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, smooth);
        }
    }
}