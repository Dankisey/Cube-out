using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Game.Level;

namespace Game.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private ProgressObserver _progressObserver;
        [SerializeField] private Image _filler;
        [SerializeField][Range(0, 1)] private float _maxChangingDelta;

        private Coroutine _currentCoroutine = null;

        private void Awake()
        {
            _filler.fillAmount = 0;
        }

        private void OnEnable()
        {
            _progressObserver.ProgressChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _progressObserver.ProgressChanged -= OnValueChanged;
        }

        private void OnValueChanged(float normalizedValue)
        {
            ChangeSliderValue(normalizedValue);
        }

        private void ChangeSliderValue(float normalizedValue)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(ChangeValueSmooth(normalizedValue));
        }

        private IEnumerator ChangeValueSmooth(float targetValue)
        {
            while (Mathf.Approximately(_filler.fillAmount, targetValue) == false)
            {
                _filler.fillAmount = Mathf.MoveTowards(_filler.fillAmount, targetValue, _maxChangingDelta * Time.deltaTime);

                yield return null;
            }

            _currentCoroutine = null;
        }
    }
}