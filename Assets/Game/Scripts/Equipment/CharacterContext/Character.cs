using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Equipment.CharacterContext.Type;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PlayerContext;
using Game.Scripts.PlayerContext.GameInput;
using Game.Scripts.SquadContext.Type;
using Game.Scripts.WeaponContext;
using Game.Scripts.WeaponContext.Type;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Equipment.CharacterContext
{
    public class Character : MonoBehaviour, IAttacker
    {
        [field: SerializeField] public Transform Weapon { get; private set; }
        [field: SerializeField] public Transform ShootPosition { get; private set; }

        private WeaponView _weaponView;
        private Rotator _rotator;
        private TrackerUnits _trackerUnits;
        private IInput _input;
        private Vector3 _direction;
        
        public IReadOnlyDictionary<StatType, Stat> Stats { get; private set; }
        public Transform Transform => ShootPosition;
        
        [Inject]
        public void Construct(Rotator rotator, TrackerUnits trackerUnits, IInput input)
        {
            _rotator = rotator;
            _trackerUnits = trackerUnits;
            _input = input;
        }
        
        public void Initialize(IReadOnlyDictionary<StatType, Stat> stats)
        {
            Stats = stats;
            StartCoroutine(StartTrackerUnits());
        }
        
        private void Update()
        {
            _rotator.Rotate(transform, _direction, _input.Horizontal, _input.Vertical, 240 * Time.deltaTime);
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