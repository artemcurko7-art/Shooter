using System.Collections.Generic;
using Game.Scripts.Extensions;
using Game.Scripts.ProjectileContext.Data;
using Game.Scripts.ProjectileContext.Type;
using Zenject;

namespace Game.Scripts.PoolMono.Repository.Projectile
{
    public class ProjectilePoolRepository : IProjectilePoolRepository
    {
        private readonly Dictionary<ProjectileType, ProjectilePool> _pools = new();
        
        public ProjectilePoolRepository(IProjectileData data, DiContainer container)
        {
            foreach (var projectile in data.Projectiles)
            {
                var pool = container.InstantiateProjectilePool(projectile.Value.View);
                _pools.Add(projectile.Key, pool);
            }
        }
        
        public IReadOnlyDictionary<ProjectileType, ProjectilePool> Pools => _pools;
    }
}