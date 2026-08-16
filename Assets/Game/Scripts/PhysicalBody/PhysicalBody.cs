using UnityEngine;

namespace Game.Scripts.PhysicalBody
{
    public class PhysicalBody<T> : MonoBehaviour where T : PhysicalBody<T>
    {
        public void Initialize(Vector3 position)
        {
            transform.position = position;
        }

        public void ResetSettings()
        {
            transform.position = Vector3.zero;
        }
    }
}