using Game.Scripts.Equipment.EquipmentContext;

namespace Game.Scripts.Service.Equipment.EquipmentContext
{
    public interface IReplacementService
    {
        public EquipmentSlot EquipmentSlot { get; }
    }
}