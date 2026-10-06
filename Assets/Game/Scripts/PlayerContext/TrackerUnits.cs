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
        
        public bool IsTracker { get; private set; }
        
        public Vector3 GetNearestPosition(Vector3 position)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(position, _radius, _results, _layerMask);
            float closestDistanceSqr = Mathf.Infinity;

            Vector3 direction = Vector3.zero;
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
                    direction = calculationDirection.normalized;
                    IsTracker = true;
                }
            }
            
            return direction;
        }
    }
}