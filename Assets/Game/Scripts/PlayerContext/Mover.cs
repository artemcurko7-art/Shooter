using UnityEngine;

namespace Game.Scripts.PlayerContext
{
    public class Mover
    {
        public void Move(Rigidbody rigidbody, float horizontal, float vertical, float speed)
        {
            rigidbody.velocity = new Vector3(horizontal, rigidbody.velocity.y, vertical) * speed;
        }
    }
}