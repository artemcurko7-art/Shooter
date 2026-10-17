using Game.Scripts.Configs;

namespace Game.Scripts.GameWorldContext
{
    public class GameWorld
    {
        public GameWorld(GameWorldConfig config)
        {
            Config = config;
        }
        
        public GameWorldConfig Config { get; }
    }
}