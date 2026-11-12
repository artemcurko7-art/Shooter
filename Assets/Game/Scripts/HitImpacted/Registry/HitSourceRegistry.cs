using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.HitImpacted.HitSources;
using Game.Scripts.PoolMono.Repository.Projectile;
using Game.Scripts.Service.UnitContext;

namespace Game.Scripts.HitImpacted.Registry
{
    public class HitSourceRegistry : IHitSourceRegistry
    {
        private readonly ICharacterProjectilePoolRepository _projectilePoolRepository;
        private readonly IUnitAttackerService _attackerService;
        private readonly List<IHitSource> _sources = new();
        
        public HitSourceRegistry(ICharacterProjectilePoolRepository projectilePoolRepository, IUnitAttackerService attackerService)
        {
            _projectilePoolRepository = projectilePoolRepository;
            _attackerService = attackerService;
            
            Fill();
        }
        
        public IEnumerable<IHitSource> GetSources(Func<IHitSource, bool> filter = null)
        {
            return filter == null ? _sources : _sources.Where(filter);
        }
        
        private void Fill()
        {
            foreach (var pool in _projectilePoolRepository.GetCharacterPools().Values)
            {
                if (pool is IHitSource source)
                    _sources.Add(source);
            }
            
            if (_attackerService is IHitSource unitSource)
                _sources.Add(unitSource);
        }
    }
}