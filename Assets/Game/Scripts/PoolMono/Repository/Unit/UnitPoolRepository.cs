using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.Extensions;
using Game.Scripts.UnitContext.Data;
using Game.Scripts.UnitContext.Type;
using Zenject;

namespace Game.Scripts.PoolMono.Repository.Unit
{
    public class UnitPoolRepository : IUnitPoolRepository
    {
        private readonly IUnitData _data;
        private readonly DiContainer _container;
        private readonly Dictionary<UnitType, List<UnitPool>> _pools = new();
        
        public UnitPoolRepository(IUnitData data, DiContainer container)
        {
            _data = data;
            _container = container;
            
            Fill();
        }

        private void Fill()
        {
            foreach (var unit in _data.Units)
            {
                foreach (var config in unit.Value)
                {
                    if (_pools.ContainsKey(config.Type) == false)
                        _pools.Add(config.Type, new List<UnitPool>());
                    
                    _pools[config.Type].Add(_container.InstantiateUnitPool(config));
                }
            }
        }

        public IReadOnlyDictionary<UnitType, List<UnitPool>> Pools => _pools;
    }
}