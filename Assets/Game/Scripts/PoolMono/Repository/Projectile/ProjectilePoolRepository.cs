using System;
using System.Collections.Generic;
using Game.Scripts.Extensions;
using Game.Scripts.PoolMono.Repository.Projectile.Type;
using Game.Scripts.ProjectileContext.Data;
using Game.Scripts.ProjectileContext.Type;
using Zenject;

namespace Game.Scripts.PoolMono.Repository.Projectile
{
    public class ProjectilePoolRepository : ICharacterProjectilePoolRepository, IUnitProjectilePoolRepository
    {
        private readonly IProjectileData _data;
        private readonly DiContainer _container;
        private readonly Dictionary<ProjectilePoolType, Dictionary<ProjectileType, ProjectilePool>> _pools = new();
        
        public ProjectilePoolRepository(IProjectileData data, DiContainer container)
        {
            _data = data;
            _container = container;
            
            Fill();
        }

        private void Fill()
        {
            foreach (var type in Enum.GetValues(typeof(ProjectilePoolType)))
            {
                var projectilePoolType = (ProjectilePoolType)type;
                
                _pools.Add(projectilePoolType, new Dictionary<ProjectileType, ProjectilePool>());
                
                foreach (var projectile in _data.Projectiles)
                {
                    var pool = _container.InstantiateProjectilePool(projectile.Value.View);
                    _pools[projectilePoolType].Add(projectile.Key, pool);
                }
            }
        }

        public IReadOnlyDictionary<ProjectileType, ProjectilePool> GetCharacterPools()
        {
            return _pools[ProjectilePoolType.Character];
        }
        
        public IReadOnlyDictionary<ProjectileType, ProjectilePool> GetUnitPools()
        {
            return _pools[ProjectilePoolType.Unit];
        }
    }
}