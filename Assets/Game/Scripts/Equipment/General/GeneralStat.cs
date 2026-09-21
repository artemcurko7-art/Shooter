using System;
using Game.Scripts.MV.StatContext.Type;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Equipment.General
{
    public class GeneralStat : MonoBehaviour
    {
        [SerializeField] private TMP_Text _valueText;
        [field: SerializeField] public StatType Type { get; private set; }

        public void SetValue(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException($"value is below zero: {value}");
            
            _valueText.text = value.ToString();
        }
    }
}