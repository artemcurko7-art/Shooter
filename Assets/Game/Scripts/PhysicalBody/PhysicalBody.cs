using System;
using UnityEngine;

namespace Game.Scripts.PhysicalBody
{
    public abstract class PhysicalBody<T> : MonoBehaviour where T : PhysicalBody<T>
    {
        public virtual void Initialize(Vector3 position)
        {
           transform.position = position;
        }

        public virtual void ResetSettings()
        {
            transform.position = Vector3.zero;
        }
    }
}