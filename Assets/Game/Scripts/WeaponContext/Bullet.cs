using UnityEngine;

namespace Game.Scripts.WeaponContext
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _direction;
    
        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }
    
        private void Update()
        {
            transform.Translate(_direction * 3 * Time.deltaTime);
        }
    }
}