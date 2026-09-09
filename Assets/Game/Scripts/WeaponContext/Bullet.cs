using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _direction;

        private void Start()
        {
            StartCoroutine(StartDestroyed());
        }

        private void Update()
        {
            transform.Translate(_direction * 10 * Time.deltaTime);
        }
        
        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        private IEnumerator StartDestroyed()
        {
            yield return new WaitForSeconds(3);
            
            Destroy(gameObject);
        }
    }
}