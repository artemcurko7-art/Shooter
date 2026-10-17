using Game.Scripts.GameWorldContext.Type;

namespace Game.Scripts.GameWorldContext.Provider
{
    public interface IGameWorldProvider
    {
        public GameWorldType Type { get; }
    }
}