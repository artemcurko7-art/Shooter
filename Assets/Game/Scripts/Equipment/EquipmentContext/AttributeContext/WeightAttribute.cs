using System;

namespace Game.Scripts.Equipment.EquipmentContext.AttributeContext
{
    [AttributeUsage(AttributeTargets.Field)]
    public class WeightAttribute : Attribute
    {
        public int Weight { get; }

        public WeightAttribute(int weight)
        {
            Weight = weight;
        }
    }
}