using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.MV.GameProgressContext
{
    public abstract class GameProgressView : MonoBehaviour
    {
        protected IGameProgress Model;
        
        [Inject]
        public void Construct(IGameProgress model)
        {
            Model = model;
            
            Model.ValueChanged += OnValueChanged;
            Model.Update();
        }

        private void OnDestroy()
        {
            Model.ValueChanged -= OnValueChanged;
        }
        
        protected abstract void OnValueChanged(int value);
    }
}