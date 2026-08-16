using System.Collections.Generic;
using Game.Scripts.PlayerContext;
using Game.Scripts.PhysicalBody.UnitContext;
using Zenject;

namespace Game.Scripts.PoolMono
{
    public class UnitPool : PoolMono<Unit>
    {
        private readonly List<ITransformable> _units = new();
    
        public UnitPool(DiContainer container) : base(container) { }
    
        public IReadOnlyList<ITransformable> Units => _units;
    
        protected override void ActionOnGet(Unit unit)
        {
            base.ActionOnGet(unit);
            unit.Disabled += OnRelease;
            _units.Add(unit);
        }

        protected override void ActionOnRelease(Unit unit)
        {
            base.ActionOnRelease(unit);
            unit.ResetSettings();
        }

        protected override void OnRelease(Unit unit)
        {
            base.OnRelease(unit);
            unit.Disabled -= OnRelease;
            _units.Remove(unit);
        }
    }
}