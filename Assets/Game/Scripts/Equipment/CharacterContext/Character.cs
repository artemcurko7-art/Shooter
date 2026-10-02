using System;
using Game.Scripts.PlayerContext;
using Game.Scripts.PlayerContext.GameInput;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Equipment.CharacterContext
{
    public class Character : MonoBehaviour
    {
        private Rotation _rotation;
        private TrackerUnits _trackerUnits;
        private IInput _input;
        
        [Inject]
        public void Construct(Rotation rotation, TrackerUnits trackerUnits, IInput input)
        {
            _rotation = rotation;
            _trackerUnits = trackerUnits;
            _input = input;
        }
        
        private void Update()
        {
            _rotation.Rotate(transform, _trackerUnits.Direction, _input.Horizontal, _input.Vertical, 240 * Time.deltaTime);
        }
    }
}