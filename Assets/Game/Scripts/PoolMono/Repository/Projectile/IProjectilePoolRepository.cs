using System.Collections.Generic;
using Game.Scripts.ProjectileContext.Type;

namespace Game.Scripts.PoolMono.Repository.Projectile
{
    public interface IProjectilePoolRepository
    {
        public IReadOnlyDictionary<ProjectileType, ProjectilePool> Pools { get; }
    }
}