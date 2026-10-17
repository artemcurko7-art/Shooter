using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class Rotator 
    {
        private readonly TrackerUnits _trackerUnits;
        private readonly RotatorToTarget _rotatorToTarget;
        private readonly CalculationRotatorAngle _calculationRotatorAngle;
    
        public Rotator(TrackerUnits trackerUnits, RotatorToTarget rotatorToTarget, CalculationRotatorAngle calculationRotatorAngle)
        {
            _trackerUnits = trackerUnits;
            _rotatorToTarget = rotatorToTarget;
            _calculationRotatorAngle = calculationRotatorAngle;
        }

        public void Rotate(Transform transform, Vector3 tracker, float horizontal, float vertical, float smooth)
        {
            if (_trackerUnits.IsTracker)
                _rotatorToTarget.Rotate(transform, tracker, smooth);
            else if (horizontal != 0 || vertical != 0)
                _calculationRotatorAngle.Rotate(transform, horizontal, vertical, smooth);
        }
    }
}