using System.Threading;
using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class TrackerUnits
    {
        private const int SecondInMilliseconds = 1000;
        private const float Cooldown = 0.3f;
        private readonly LayerMask _layerMask;
        private readonly Vector3 _position;
        private readonly Collider[] _results = new Collider[128];
        private readonly float _radius;
        private CancellationTokenSource _cancellationTokenSource;
    
        public TrackerUnits(LayerMask layerMask, float radius)
        {
            _layerMask = layerMask;
            _radius = radius;
        }
    
        public Vector3 Direction { get; private set; }
        public bool IsTracker { get; private set; }
        
        public void FindNearestPosition(Vector3 position)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(position, _radius, _results, _layerMask);
            float closestDistance = Mathf.Infinity;
            Direction = Vector3.zero;
            IsTracker = false;

            for (int i = 0; i < hitCount; i++)
            {
                Collider collider = _results[i];
                Vector3 calculationDirection = collider.bounds.center - _position;
                float distance = calculationDirection.sqrMagnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    Direction = calculationDirection;
                    IsTracker = true;
                }
            }
        }
    }
}