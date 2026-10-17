using Game.Scripts.GameWorldContext.Type;

namespace Game.Scripts.GameWorldContext.Provider
{
    public class GameWorldProvider : IGameWorldProvider
    {
        public GameWorldProvider()
        {
            Type = GameWorldType.Forest; // test
        }
        
        public GameWorldType Type { get; private set; }
        
        public void Set(GameWorldType type)
        {
            Type = type;
        }
    }
}