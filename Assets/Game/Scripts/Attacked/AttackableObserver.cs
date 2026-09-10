using Game.Scripts.Configs;
using Game.Scripts.Service.Subscriber;
using Game.Scripts.WeaponContext.Shooting;

namespace Game.Scripts.Attacked
{
    public abstract class AttackableObserver : ISubscriber
    {
        private readonly WeaponConfig _config;
        private readonly IWeaponShooting[] _shootings;
        
        public AttackableObserver(WeaponConfig config, IWeaponShooting[] shootings)
        {
            _config = config;
            _shootings = shootings;
        }
        
        public void Subscribe()
        {
            foreach (var shooting in _shootings)
                if (shooting.Type == _config.ShootingType)
                    shooting.Attacked += OnAttacked;
        }

        public void Unsubscribe()
        {
            foreach (var shooting in _shootings)
                if (shooting.Type == _config.ShootingType)
                    shooting.Attacked -= OnAttacked;
        }

        protected abstract void OnAttacked();
    }
}