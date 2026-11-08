using Game.Scripts.PassiveAbilityContext.Data;
using Game.Scripts.PoolMono;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Extensions
{
    public static class DiContainerExtensions
    {
        public static PassiveAbilityViewPool InstantiatePassiveAbilityViewPool(
            this DiContainer container,
            PassiveAbilityData data,
            Transform transform)
        {
            return container.Instantiate<PassiveAbilityViewPool>(new object[] { data, transform });
        }
    }
}