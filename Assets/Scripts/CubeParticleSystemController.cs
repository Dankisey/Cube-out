using UnityEngine;

public class CubeParticleSystemController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private CubeMovement _movement;
    [SerializeField] private float _distanceThreshold;

    private void OnEnable()
    {
        _movement.MovementStarted += OnMovementStarted;
        _movement.MovementEnded += OnMovementEnded;
    }

    private void OnDisable()
    {
        _movement.MovementStarted -= OnMovementStarted;
        _movement.MovementEnded -= OnMovementEnded;
    }

    private void OnMovementStarted(Vector3 target)
    {
        float distance = (target - transform.localPosition).magnitude;

        if (distance > _distanceThreshold)
            _particleSystem.Play();
    }

    private void OnMovementEnded()
    {
        _particleSystem.Stop();
    }
}