using System.Collections.Generic;
using Game.Scripts.Configs;
using Game.Scripts.GameWorldContext.Type;

namespace Game.Scripts.GameWorldContext.Data
{
    public interface IGameWorldData
    {
        public IReadOnlyDictionary<GameWorldType, GameWorldConfig> Worlds { get; }
    }
}