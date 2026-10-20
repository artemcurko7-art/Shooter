using UnityEngine;

namespace Game.Scripts.UI.Genetic
{
    public class GeneticElementSpawner : MonoBehaviour
    {
        public T Spawn<T>(T prefab, Transform parent) where T : Component, IGeneticElement
        {
            if (!prefab || !parent)
            {
                Debug.LogWarning("[GeneticElementSpawner] Не назначен prefab или parent!", this);
                return null;
            }

            return Instantiate(prefab, parent);
        }
    }
}