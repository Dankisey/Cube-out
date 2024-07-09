using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Game.Cube
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private CubeSettings _settings;
        [SerializeField] private Transform _transform;

        private Tween _currentTween;
        private float _remainingLifetime;

        public bool IsMovingOut { get; private set; } = false;
        public Vector3 Direction => _transform.up;

        public event Action<Vector3> MovementStarted;
        public event Action MovementEnded;

        private void Awake()
        {
            _remainingLifetime = _settings.TimeBeforeDestroy;
        }

        private void Update()
        {
            if (IsMovingOut == false)
                return;

            _transform.position += _settings.MovementSpeed * Time.deltaTime * Direction;
            _remainingLifetime -= Time.deltaTime;

            if (_remainingLifetime <= 0)
                StartCoroutine(InvokeMovementEndedCallback());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_transform.position, _transform.position + Direction);
        }

        public void MoveTo(Vector3 localTarget, float time)
        {
            if (Utils.Approximately(_transform.localPosition, localTarget))
                return;

            _currentTween = _transform.DOLocalMove(localTarget, time).SetEase(Ease.Linear);
            MovementStarted?.Invoke(localTarget);

            StartCoroutine(InvokeMovementEndedCallback(time));
        }

        public void MoveOut()
        {
            IsMovingOut = true;
            Vector3 target = _transform.localPosition + _settings.MovementSpeed * _settings.TimeBeforeDestroy * Direction;
            MovementStarted?.Invoke(target);
        }

        public void Stop()
        {
            _currentTween?.Kill();
        }

        private IEnumerator InvokeMovementEndedCallback(float delay = 0)
        {
            yield return new WaitForSeconds(delay);

            MovementEnded?.Invoke();
        }
    }
}