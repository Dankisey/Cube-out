using UnityEngine;
using DG.Tweening;

public class BreatheAnimation : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _endScaleFactor;
    [SerializeField] private float _duration;

    private Tween _tween;

    private void OnEnable()
    {
        _tween = _target.DOScale(_endScaleFactor, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    private void OnDisable()
    {
        _tween?.Kill();
    }
}