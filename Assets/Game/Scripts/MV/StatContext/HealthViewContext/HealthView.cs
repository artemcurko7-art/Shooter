using System;
using Game.Scripts.Equipment.CharacterContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.MV.StatContext.HealthViewContext
{
    public abstract class HealthView : MonoBehaviour
    {
        [Inject]
        public void Construct(ICharacterHealthResolver model)
        {
            Model = model;
            
            Model.Health.ValueChanged += OnValueChanged;
            Model.Health.Update();
        }
        
        protected ICharacterHealthResolver Model { get; private set; }

        private void OnDestroy()
        {
            Model.Health.ValueChanged -= OnValueChanged;
        }

        protected abstract void OnValueChanged(int value);
    }
}