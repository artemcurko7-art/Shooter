using System;
using UnityEngine;

namespace Game.Scripts.HitImpacted
{
    public interface IImpactReceiver
    {
        public event Action<RaycastHit> HitImpacted;
    }
}