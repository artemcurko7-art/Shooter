using Game.Scripts.Equipment.AttributeContext;
using Game.Scripts.UserUtils;

namespace Game.Scripts.Equipment.Type
{
    public enum RarityEquipmentType
    {
        None,
        [Weight(RarityEquipmentWeights.Usual)]Usual,
        [Weight(RarityEquipmentWeights.Unusual)]Unusual,
        [Weight(RarityEquipmentWeights.Rare)]Rare,
        [Weight(RarityEquipmentWeights.Epic)]Epic,
        [Weight(RarityEquipmentWeights.Legendary)]Legendary,
        [Weight(RarityEquipmentWeights.Mythical)]Mythical,
    }
}