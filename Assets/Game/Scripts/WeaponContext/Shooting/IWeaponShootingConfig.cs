namespace Game.Scripts.WeaponContext.Shooting
{
    public interface IWeaponShootingConfig
    {
        public Projectile Projectile { get; }
        public int MaxCountShoot { get; }
        public float CooldownShoot { get; }
        public float CooldownReload { get; }
        public float Radius { get; }
        public float Speed { get; }
        public int Damage { get; }
    }
}