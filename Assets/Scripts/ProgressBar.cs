using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [Header("Bar")]
    [SerializeField] private ProgressObserver _progressObserver;
    [SerializeField] private Image _filler;
    [SerializeField][Range(0, 1)] private float _maxChangingDelta;
    [Header("Stars")]
    [SerializeField] private Star[] _stars;
    [SerializeField] private float[] _starPercentages = new float[] { 0.255f, 0.575f, 0.9f };

    private Coroutine _currentCoroutine = null;

    private void OnValidate()
    {
        if (_stars.Length < _starPercentages.Length)
            Debug.LogWarning($"Risk of {nameof(System.IndexOutOfRangeException)}. " +
                $"Length of {nameof(_starPercentages)} greater than {nameof(_stars)}");
    }

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
        ChangeActiveStars(normalizedValue);
    }

    private void ChangeActiveStars(float currentPercentage)
    {
        for (int i = 0; i < _starPercentages.Length; i++)
        {
            if (currentPercentage >= _starPercentages[i])
                _stars[i].Activate();
            else
                break;
        }
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