using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CubeMovement : MonoBehaviour 
{
    [SerializeField] private CubeSettings _settings;
    [SerializeField] private Transform _transform;

    private Tween _currentTween;
    private Vector3 _direction;
    private float _remainingLifetime;
    private bool _isMovingOut = false;

    public event Action<Vector3> MovementStarted;
    public event Action MovementEnded;

    private void Awake()
    {
        _direction = _transform.up;
        _remainingLifetime = _settings.TimeBeforeDestroy;
    }

    private void Update()
    {
        if (_isMovingOut == false)
            return;

        _transform.localPosition += _settings.MovementSpeed * Time.deltaTime * _direction;
        _remainingLifetime -= Time.deltaTime;

        if (_remainingLifetime <= 0)
            StartCoroutine(InvokeCallback());
    }

    public void MoveTo(Vector3 target, float time)
    {
        if (Utils.Approximately(_transform.position, target))
            return;

        target = transform.parent.InverseTransformPoint(target);

        _currentTween = _transform.DOLocalMove(target, time).SetEase(Ease.Linear);
        MovementStarted?.Invoke(target);

        StartCoroutine(InvokeCallback(time));
    }

    public void MoveOut()
    {
        _isMovingOut = true;
        MovementStarted?.Invoke(_transform.position + _settings.MovementSpeed * _settings.TimeBeforeDestroy * _direction);
    }

    public void Stop()
    {
        _currentTween?.Kill();
    }

    private IEnumerator InvokeCallback(float delay = 0) 
    {
        yield return new WaitForSeconds(delay);

        MovementEnded?.Invoke();
    }
}