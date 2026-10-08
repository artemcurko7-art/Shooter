using Game.Scripts.Configs;

namespace Game.Scripts.WeaponContext.Provider
{
    public class WeaponProvider : IWeaponProvider
    {
        public WeaponConfig Config { get; private set; }
    
        public void Set(WeaponConfig config)
        {
            Config = config;
        }
    }
}