using Game.Scripts.Equipment.EquipmentContext.AttributeContext;

namespace Game.Scripts.Equipment.EquipmentContext.Type
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