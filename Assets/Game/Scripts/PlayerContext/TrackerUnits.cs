using Game.Scripts.Service.PhysicalBody;
using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class TrackerUnits
    {
        private readonly IUnitService _service;
        private readonly Collider[] _results = new Collider[128];
        private Vector3 _newPosition;
    
        public TrackerUnits(IUnitService service)
        {
            _service = service;
        }
    
        public bool IsTracker { get; private set; }

        public Vector3 GetNearestPosition(Vector3 position, float radius, LayerMask layerMask)
        {
            int hitCount = Physics.OverlapSphereNonAlloc(position, radius, _results, layerMask);
            float closestDistance = Mathf.Infinity;
            _newPosition = Vector3.zero;
            IsTracker = false;
        
            for (int i = 0; i < hitCount; i++)
            {
                Collider collider = _results[i];
                float distance = (collider.transform.position - position).sqrMagnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _newPosition = collider.transform.position;
                    IsTracker = true;
                }
            }
        
            return _newPosition;
        }
    }
}