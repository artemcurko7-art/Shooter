using System;
using Game.Scripts.Equipment.CharacterContext;
using UnityEngine;
using Zenject;

namespace Game.Scripts.MV.StatContext.HealthViewContext
{
    public abstract class HealthView : MonoBehaviour
    {
        private IStat _stat;
        
        [Inject]
        public void Construct(IStat stat, IHealth health)
        {
            _stat = stat;
            Model = health;
            
            _stat.ValueChanged += OnValueChanged;
            _stat.Update();
        }
        
        protected IHealth Model { get; private set; }

        private void OnDestroy()
        {
            _stat.ValueChanged -= OnValueChanged;
        }

        protected abstract void OnValueChanged(int value);
    }
}