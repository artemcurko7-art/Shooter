using Game.Scripts.Equipment.CharacterContext;

namespace Game.Scripts.WeaponContext
{
    public class ShotContext
    {
        public ShotContext(ICharacter character, int damage, bool isCritical)
        {
            Character = character;
            Damage = damage;
            IsCritical = isCritical;
        }
        
        public ICharacter Character { get; }
        public int Damage { get; }
        public bool IsCritical { get; }
    }
}