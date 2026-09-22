using Game.Scripts.GameWorldContext.Type;
using Game.Scripts.Wave;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/GameWorld", fileName = "GameWorld", order = 1)]
    public class GameWorldConfig : ScriptableObject
    {
        [field: SerializeField] public GameWorldType Type { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public WaveData[] Waves { get; private set; }
    }
}