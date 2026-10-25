using System;
using Game.Scripts.Equipment.CharacterContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.MV.StatContext.HealthViewContext
{
    public abstract class HealthView : MonoBehaviour
    {
        [Inject]
        public void Construct(IStat model)
        {
            Model = model;
            
            Model.ValueChanged += OnValueChanged;
            Model.Update();
        }
        
        protected IStat Model { get; private set; }

        private void OnDestroy()
        {
            Model.ValueChanged -= OnValueChanged;
        }

        protected abstract void OnValueChanged(int value);
    }
}