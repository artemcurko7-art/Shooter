using Game.Scripts.MV.StatContext.Type;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/DisplayStat", fileName = "DisplayStat", order = 8)]
    public class DisplayStatConfig : ScriptableObject
    {
        [field: SerializeField] public StatType Type { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }
}