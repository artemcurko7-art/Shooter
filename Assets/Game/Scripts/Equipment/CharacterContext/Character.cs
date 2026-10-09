using System;
using System.Collections;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.MV.StatContext;
using Game.Scripts.PlayerContext;
using Game.Scripts.PlayerContext.GameInput;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Equipment.CharacterContext
{
    public class Character : MonoBehaviour, IAttacker
    {
        [field: SerializeField] public Transform Weapon { get; private set; }
        [field: SerializeField] public Transform ShootPosition { get; private set; }
        
        private Rotation _rotation;
        private TrackerUnits _trackerUnits;
        private IInput _input;
        private Vector3 _direction;

        public CharacterType CharacterType { get; private set; }
        public WeaponType WeaponType { get; private set; }
        public Stat[] Stats { get; private set; }
        public Transform Transform => ShootPosition;
        
        [Inject]
        public void Construct(Rotation rotation, TrackerUnits trackerUnits, IInput input)
        {
            _rotation = rotation;
            _trackerUnits = trackerUnits;
            _input = input;
        }
        
        public void Initialize(CharacterType type, WeaponType weaponType, Stat[] stats)
        {
            CharacterType = type;
            WeaponType = weaponType;
            Stats = stats;
            StartCoroutine(StartTrackerUnits());
        }
        
        private void Update()
        {
            _rotation.Rotate(transform, _direction, _input.Horizontal, _input.Vertical, 240 * Time.deltaTime);
        }
        
        private IEnumerator StartTrackerUnits()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(0.1f);
            
                _direction = _trackerUnits.GetNearestPosition(transform.position);
            }
        }
    }
}