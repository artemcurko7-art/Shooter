using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Game.Scripts.Equipment.AttributeContext;

namespace Game.Scripts.UserUtils
{
    public static class WeightedRandomSampling
    {
        private static readonly Random s_random = new();

        public static TEnum GetRandomWeighted<TEnum>() where TEnum : Enum
        {
            return GetWeightedShuffle<TEnum>().First();
        }
        
        private static List<TEnum> GetWeightedShuffle<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Skip(1)
                .Select(value => new
                {
                    Value = value,
                    Key = Math.Pow(s_random.NextDouble(), 1.0 / GetWeight(value))
                })
                .OrderByDescending(x => x.Key)
                .Select(x => x.Value)
                .ToList();
        }
        
        private static float GetWeight<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            WeightAttribute attribute = field?.GetCustomAttribute<WeightAttribute>();
            
            return attribute?.Weight ?? 1.0f;
        }
    }
}