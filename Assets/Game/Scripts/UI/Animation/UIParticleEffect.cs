using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Animation
{
    public class UIParticleEffect : MonoBehaviour
    {
        [Header("Частицы")]
        [SerializeField] private Sprite _particleSprite;
        [SerializeField] private int _particleCount = 25;

        [Header("Размер")]
        [SerializeField] private float _minSize = 4f;
        [SerializeField] private float _maxSize = 12f;

        [Header("Движение")]
        [SerializeField] private float _minSpeed = 8f;
        [SerializeField] private float _maxSpeed = 25f;
        [SerializeField] private float _waveAmount = 15f;
        [SerializeField] private float _waveSpeed = 1.5f;

        [Header("Жизнь")]
        [SerializeField] private float _minLifetime = 2f;
        [SerializeField] private float _maxLifetime = 4f;

        [Header("Цвет")]
        [SerializeField] private Color _startColor = new Color(1f, 0.8f, 0.25f, 0f);
        [SerializeField] private Color _endColor = new Color(1f, 0.55f, 0.1f, 0f);

        private readonly List<Particle> _particles = new();

        private RectTransform _rectTransform;
        private RectTransform _particleContainer;

        private void Awake()
        {
            _rectTransform = transform as RectTransform;

            CreateParticleContainer();
            CreateParticles();
        }

        private void Update()
        {
            UpdateParticles();
        }

        private void OnDestroy()
        {
            _particles.Clear();
        }

        private void CreateParticleContainer()
        {
            var container = new GameObject("Particles", typeof(RectTransform));
            _particleContainer = container.GetComponent<RectTransform>();

            _particleContainer.SetParent(transform, false);
            _particleContainer.anchorMin = Vector2.zero;
            _particleContainer.anchorMax = Vector2.one;
            _particleContainer.offsetMin = Vector2.zero;
            _particleContainer.offsetMax = Vector2.zero;
        }

        private void CreateParticles()
        {
            for (var i = 0; i < _particleCount; i++)
            {
                CreateParticle(i);
            }
        }

        private void CreateParticle(int index)
        {
            var gameObject = new GameObject(
                $"Particle_{index}",
                typeof(RectTransform),
                typeof(Image)
            );

            var rect = gameObject.GetComponent<RectTransform>();
            var image = gameObject.GetComponent<Image>();

            rect.SetParent(_particleContainer, false);
            rect.sizeDelta = Vector2.one;

            image.sprite = _particleSprite;
            image.raycastTarget = false;

            var particle = new Particle
            {
                RectTransform = rect,
                Image = image
            };

            _particles.Add(particle);

            ResetParticle(particle, true);
        }

        private void ResetParticle(Particle particle, bool randomPosition)
        {
            var rect = _rectTransform.rect;

            var x = Random.Range(rect.xMin, rect.xMax);
            var y = randomPosition
                ? Random.Range(rect.yMin, rect.yMax)
                : rect.yMin - 20f;

            particle.RectTransform.anchoredPosition = new Vector2(x, y);

            particle.Size = Random.Range(_minSize, _maxSize);
            particle.Speed = Random.Range(_minSpeed, _maxSpeed);
            particle.Lifetime = Random.Range(_minLifetime, _maxLifetime);
            particle.Time = randomPosition
                ? Random.Range(0f, particle.Lifetime)
                : 0f;

            particle.WaveOffset = Random.Range(0f, Mathf.PI * 2f);
            particle.WaveDirection = Random.value > 0.5f ? 1f : -1f;

            particle.RectTransform.sizeDelta = new Vector2(
                particle.Size,
                particle.Size
            );

            particle.Image.color = Color.clear;
        }

        private void UpdateParticles()
        {
            foreach (var particle in _particles)
            {
                particle.Time += Time.deltaTime;

                if (particle.Time >= particle.Lifetime)
                {
                    ResetParticle(particle, false);
                    continue;
                }

                var normalizedTime = particle.Time / particle.Lifetime;

                UpdatePosition(particle, normalizedTime);
                UpdateVisual(particle, normalizedTime);
            }
        }

        private void UpdatePosition(Particle particle, float normalizedTime)
        {
            var position = particle.RectTransform.anchoredPosition;

            position.y += particle.Speed * Time.deltaTime;

            var wave = Mathf.Sin(
                particle.Time * _waveSpeed + particle.WaveOffset
            );

            position.x += wave * _waveAmount * Time.deltaTime * particle.WaveDirection;

            particle.RectTransform.anchoredPosition = position;
        }

        private void UpdateVisual(Particle particle, float normalizedTime)
        {
            var alpha = CalculateAlpha(normalizedTime);

            var color = Color.Lerp(
                _startColor,
                _endColor,
                normalizedTime
            );

            color.a *= alpha;

            particle.Image.color = color;

            var scale = CalculateScale(normalizedTime);

            particle.RectTransform.localScale = Vector3.one * scale;
        }

        private float CalculateAlpha(float normalizedTime)
        {
            if (normalizedTime < 0.2f)
            {
                return Mathf.InverseLerp(
                    0f,
                    0.2f,
                    normalizedTime
                );
            }

            if (normalizedTime > 0.75f)
            {
                return Mathf.InverseLerp(
                    1f,
                    0.75f,
                    normalizedTime
                );
            }

            return 1f;
        }

        private float CalculateScale(float normalizedTime)
        {
            var pulse = Mathf.Sin(
                normalizedTime * Mathf.PI * 3f
            );

            return 0.8f + pulse * 0.2f;
        }

        private class Particle
        {
            public RectTransform RectTransform;
            public Image Image;

            public float Size;
            public float Speed;
            public float Lifetime;
            public float Time;

            public float WaveOffset;
            public float WaveDirection;
        }
    }
}