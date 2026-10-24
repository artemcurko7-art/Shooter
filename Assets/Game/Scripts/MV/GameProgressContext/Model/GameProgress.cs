using System;
using Game.Scripts.PhysicalBody.UnitContext;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.Service.UnitContext;

namespace Game.Scripts.MV.GameProgressContext
{
    public class GameProgress : IGameProgress, ISubscriber
    {
        private readonly IUnitService _unitService;
        
        private int _value;
        
        public event Action<int> ValueChanged;

        public GameProgress(IUnitService unitService)
        {
            _unitService = unitService;
            MaxValue = unitService.MaxCount;
        }
        
        public int MaxValue { get; }

        public int Value
        {
            get => _value;

            private set
            {
                _value = Math.Clamp(value, 0, MaxValue);
                ValueChanged?.Invoke(_value);
            }
        }

        public void Subscribe()
        {
            _unitService.Added += OnAdded;
        }

        public void Unsubscribe()
        {
            _unitService.Added -= OnAdded;
        }
        
        public void Update()
        {
            ValueChanged?.Invoke(Value);
        }

        private void OnAdded(Unit unit)
        {
            unit.Dead += OnDead;
        }

        private void OnDead(Unit unit)
        {
            Value++;
            unit.Dead -= OnDead;
        }
    }
}