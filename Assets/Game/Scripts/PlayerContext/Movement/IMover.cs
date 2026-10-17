using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public interface IMover
    {
        public Vector3 Direction { get; }
    }
}