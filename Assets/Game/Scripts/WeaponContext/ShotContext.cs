using Game.Scripts.Equipment.CharacterContext;

namespace Game.Scripts.WeaponContext
{
    public class ShotContext
    {
        public ShotContext(IAttacker attacker, int damage, bool isCritical)
        {
            Attacker = attacker;
            Damage = damage;
            IsCritical = isCritical;
        }
        
        public IAttacker Attacker { get; }
        public int Damage { get; }
        public bool IsCritical { get; }
    }
}