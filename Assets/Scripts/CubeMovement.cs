using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class CubeMovement : MonoBehaviour 
{
    [SerializeField] private CubeSettings _settings;
    [SerializeField] private Transform _transform;

    private Tween _currentTween;

    public event Action<Vector3> MovementStarted;
    public event Action MovementEnded;

    public void MoveTo(Vector3 target, float time)
    {
        if (Utils.Approximately(_transform.position, target))
            return;

        target = transform.parent.InverseTransformPoint(target);

        _currentTween = _transform.DOLocalMove(target, time).SetEase(Ease.Linear);
        MovementStarted?.Invoke(target);

        StartCoroutine(InvokeCallback(time));
    }

    public void MoveOut(Vector3 direction)
    {
        Vector3 target = direction * _settings.DistanceBeforeDestroy;
        float time = _settings.DistanceBeforeDestroy / _settings.MovementSpeed;

        MoveTo(target, time);
    }

    public void Stop()
    {
        _currentTween?.Kill();
    }

    private IEnumerator InvokeCallback(float delay) 
    {
        yield return new WaitForSeconds(delay);

        MovementEnded?.Invoke();
    }
}