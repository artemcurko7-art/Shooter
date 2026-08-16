using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class RotationToTarget 
    {
        public void Rotate(Transform transform, Vector3 tracker, float smooth)
        {
            Quaternion target = Quaternion.LookRotation(tracker - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, smooth);
        }
    }
}