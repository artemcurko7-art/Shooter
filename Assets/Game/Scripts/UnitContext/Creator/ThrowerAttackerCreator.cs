using System.Collections.Generic;
using Game.Scripts.Common.Interfaces;
using Game.Scripts.Configs;
using Game.Scripts.MV.StatContext;
using Game.Scripts.MV.StatContext.Type;
using Game.Scripts.PoolMono.Repository.Projectile;
using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.AttributeContext;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Creator
{
    [UnitAttackerType(UnitAttackerType.Thrower)]
    public class ThrowerAttackerCreator : IAttackerCreator
    {
        private readonly UnitConfig _unitConfig;
        private readonly IAttacker _attacker;
        private readonly IUnitProjectilePoolRepository _projectilePoolRepository;
        
        public ThrowerAttackerCreator(UnitConfig unitConfig, IAttacker attacker, IUnitProjectilePoolRepository projectilePoolRepository)
        {
            _unitConfig = unitConfig;
            _attacker = attacker;
            _projectilePoolRepository = projectilePoolRepository;
        }
        
        public IUnitAttacker Create(IReadOnlyDictionary<StatType, Stat> stats)
        {
            return new ThrowerAttacker(
                _unitConfig.LayerMask,
                _attacker,
                _projectilePoolRepository.GetUnitPools()[_unitConfig.ProjectileType],
                stats[StatType.Damage],
                stats[StatType.Speed],
                stats[StatType.Radius]);
        }
    }
}