using System.Threading;
using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class TrackerUnits
    {
        private readonly LayerMask _layerMask;
        private readonly Collider[] _results = new Collider[128];
        private readonly float _radius;
    
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
            float closestDistanceSqr = Mathf.Infinity;

            Direction = Vector3.zero;
            IsTracker = false;

            for (int i = 0; i < hitCount; i++)
            {
                Collider collider = _results[i];

                Vector3 targetPosition = collider.transform.position;
                targetPosition.y = position.y;

                Vector3 calculationDirection = targetPosition - position;
                float sqrDistance = calculationDirection.sqrMagnitude;

                if (sqrDistance < closestDistanceSqr)
                {
                    closestDistanceSqr = sqrDistance;
                    Direction = calculationDirection.normalized; 
                    IsTracker = true;
                }
            }
        }
    }
}