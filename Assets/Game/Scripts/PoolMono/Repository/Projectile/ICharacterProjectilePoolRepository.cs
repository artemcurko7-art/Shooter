using System.Collections.Generic;
using Game.Scripts.ProjectileContext.Type;

namespace Game.Scripts.PoolMono.Repository.Projectile
{
    public interface ICharacterProjectilePoolRepository
    {
        public IReadOnlyDictionary<ProjectileType, ProjectilePool> GetCharacterPools();
    }
}