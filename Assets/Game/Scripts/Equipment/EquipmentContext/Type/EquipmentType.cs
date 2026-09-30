using Game.Scripts.Equipment.EquipmentContext.AttributeContext;

namespace Game.Scripts.Equipment.EquipmentContext.Type
{
    public enum EquipmentType
    {
        None,
        [Weight(EquipmentWeights.Weapon)]Weapon,
        [Weight(EquipmentWeights.Amulet)]Amulet,
        [Weight(EquipmentWeights.Gloves)]Gloves,
        [Weight(EquipmentWeights.Helmet)]Helmet,
        [Weight(EquipmentWeights.Suit)]Suit,
        [Weight(EquipmentWeights.Boots)]Boots,
    }
}