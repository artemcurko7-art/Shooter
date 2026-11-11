using Game.Scripts.UnitContext.Attacker;
using Game.Scripts.UnitContext.Type;

namespace Game.Scripts.UnitContext.Repository
{
    public interface IUnitAttackerService
    {
        public IUnitAttacker Get(Unit unit, StateType stateType);
    }
}