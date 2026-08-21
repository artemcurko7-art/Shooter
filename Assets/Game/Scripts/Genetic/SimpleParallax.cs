using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Genetic
{
    public sealed class SimpleParallax : MonoBehaviour
    {
        public float scrollSpeed = 2.0f;
        [SerializeField] private Image renderer;


        void Update()
        {
            float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
            Vector2 offset = new Vector2(x, 0);
            renderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}