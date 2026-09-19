using System;
using System.Collections;
using Game.Scripts.PhysicalBody;
using UnityEngine;

namespace Game.Scripts.VFX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Effect : PhysicalBody<Effect>
    {
        private ParticleSystem _particleSystem;
        
        public event Action<Effect> Released;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public override void Initialize(Vector3 position)
        {
            base.Initialize(position);
            
            StartCoroutine(StartReleased());
        }
        
        private IEnumerator StartReleased()
        {
            yield return new WaitForSeconds(_particleSystem.main.duration);
            
            Released?.Invoke(this);
        }
    }
}