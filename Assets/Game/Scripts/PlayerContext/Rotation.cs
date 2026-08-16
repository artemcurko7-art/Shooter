using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class Rotation 
    {
        private readonly TrackerUnits _trackerUnits;
        private readonly RotationToTarget _rotationToTarget;
        private readonly CalculationRotationAngle _calculationRotationAngle;
    
        public Rotation(TrackerUnits trackerUnits, RotationToTarget rotationToTarget, CalculationRotationAngle calculationRotationAngle)
        {
            _trackerUnits = trackerUnits;
            _rotationToTarget = rotationToTarget;
            _calculationRotationAngle = calculationRotationAngle;
        }

        public void Rotate(Transform transform, Vector3 tracker, float horizontal, float vertical, float smooth)
        {
            if (_trackerUnits.IsTracker)
                _rotationToTarget.Rotate(transform, tracker, smooth);
            else if (horizontal != 0 || vertical != 0)
                _calculationRotationAngle.Rotate(transform, horizontal, vertical, smooth);
        }
    }
}