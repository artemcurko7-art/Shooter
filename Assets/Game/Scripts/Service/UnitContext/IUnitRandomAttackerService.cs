using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Repository
{
    public interface IUnitRandomAttackerService
    {
        public IUnitAttacker GetRandom(Unit unit, StateType stateType);
    }
}