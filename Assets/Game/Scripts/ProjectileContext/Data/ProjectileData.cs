using System;
using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.ProjectileContext.Type;
using UnityEngine;

namespace Game.Scripts.ProjectileContext.Data
{
    public class ProjectileData : IProjectileData
    {
        private readonly ProjectileConfig[] _configs;
        private readonly Dictionary<ProjectileType, ProjectileConfig> _projectiles = new();
    
        public ProjectileData()
        {
            _configs = Resources.LoadAll<ProjectileConfig>("Configs/Projectile");
            
            Fill();
        }

        public IReadOnlyDictionary<ProjectileType, ProjectileConfig> Projectiles => _projectiles;
    
        private void Fill()
        {
            foreach (var config in _configs)
            {
                if (config.Type == ProjectileType.None)
                    throw new InvalidOperationException($"Not type: {config.Type}");
            
                if (_projectiles.ContainsKey(config.Type))
                    throw new InvalidOperationException($"Duplicate type: {config.Type}");
                
                _projectiles.Add(config.Type, config);
            }
        }
    }
}