using System.Collections.Generic;
using Game.Scripts.ProjectileContext.Type;

namespace Game.Scripts.PoolMono.Repository.Projectile
{
    public interface IUnitProjectilePoolRepository
    {
        public IReadOnlyDictionary<ProjectileType, ProjectilePool> GetUnitPools();
    }
}