using Game.Scripts.Configs;

namespace Game.Scripts.Provider
{
    public class WeaponProvider
    {
        public WeaponConfig Config { get; private set; }
    
        public void Set(WeaponConfig config)
        {
            Config = config;
        }
    }
}