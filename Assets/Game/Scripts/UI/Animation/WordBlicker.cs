using System.Collections;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Animation
{
    public class WordBlicker : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _charDuration = 0.15f;
        [SerializeField] private float _delayBetweenChars = 0.05f;
        [SerializeField] private float _cycleDelay = 0.3f;

        [Header("Colors")]
        [SerializeField] private Color _originalColor = Color.white;
        [SerializeField] private Color _glowColor = Color.yellow;

        private Coroutine _coroutine;
        private bool _isRunning;

        public void OnEnable()
        {
            if (_isRunning) return;
            _isRunning = true;

            _text.ForceMeshUpdate();

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(RunSequence());
        }

        public void OnDisable()
        {
            _isRunning = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            ResetToBaseColor();
        }

        private void ResetToBaseColor()
        {
            if (_text == null) return;
            SetAllCharsColor(_originalColor);
        }

        private IEnumerator RunSequence()
        {
            while (_isRunning && _text)
            {
                _text.ForceMeshUpdate();
                var info = _text.textInfo;
                var charCount = info.characterCount;

                for (var i = 0; i < charCount; i++)
                {
                    if (!_isRunning) break;
                    if (!info.characterInfo[i].isVisible) continue;

                    StartCoroutine(AnimateChar(i, true));

                    yield return new WaitForSeconds(_delayBetweenChars);
                }

                yield return new WaitForSeconds(_cycleDelay);
            }
        }

        private IEnumerator AnimateChar(int charIndex, bool toGlow)
        {
            if (!_text) yield break;

            _text.ForceMeshUpdate();
            var info = _text.textInfo;

            if (charIndex >= info.characterCount) yield break;

            var charInfo = info.characterInfo[charIndex];
            if (!charInfo.isVisible) yield break;

            var startColor = GetCharColor(charIndex);
            var endColor = toGlow ? _glowColor : _originalColor;

            var elapsed = 0f;

            while (elapsed < _charDuration && _isRunning)
            {
                var t = elapsed / _charDuration;
                var color = Color.Lerp(startColor, endColor, t);
                SetCharColor(charIndex, color);

                elapsed += Time.deltaTime;
                yield return null;
            }

            if (_isRunning)
                SetCharColor(charIndex, endColor);

            if (toGlow && _isRunning)
                StartCoroutine(AnimateChar(charIndex, false));
        }

        private Color GetCharColor(int charIndex)
        {
            var info = _text.textInfo;
            if (charIndex >= info.characterCount) return _originalColor;

            var charInfo = info.characterInfo[charIndex];
            var meshIndex = charInfo.materialReferenceIndex;
            var vertexIndex = charInfo.vertexIndex;

            var meshInfo = info.meshInfo[meshIndex];
            return meshInfo.colors32[vertexIndex];
        }

        private void SetCharColor(int charIndex, Color color)
        {
            if (!_text) return;

            var info = _text.textInfo;
            if (charIndex >= info.characterCount) return;

            var charInfo = info.characterInfo[charIndex];
            if (!charInfo.isVisible) return;

            var meshIndex = charInfo.materialReferenceIndex;
            var vertexIndex = charInfo.vertexIndex;

            var meshInfo = info.meshInfo[meshIndex];

            for (var v = 0; v < 4; v++)
            {
                meshInfo.colors32[vertexIndex + v] = color;
            }

            meshInfo.mesh.MarkModified();
            _text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        private void SetAllCharsColor(Color color)
        {
            if (_text == null) return;

            _text.ForceMeshUpdate();
            var info = _text.textInfo;

            for (var i = 0; i < info.characterCount; i++)
            {
                SetCharColor(i, color);
            }
        }
    }
}