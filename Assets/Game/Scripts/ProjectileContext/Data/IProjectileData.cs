using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.ProjectileContext.Type;

namespace Game.Scripts.ProjectileContext.Data
{
    public interface IProjectileData
    {
        public IReadOnlyDictionary<ProjectileType, ProjectileConfig> Projectiles { get; }
    }
}