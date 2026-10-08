using Game.Scripts.Configs;

namespace Game.Scripts.WeaponContext.Provider
{
    public interface IWeaponProvider
    {
        public WeaponConfig Config { get; }
    }
}