using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeMovement))]
public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSettings _settings;
    [SerializeField] private CubeMovement _movement;
    [SerializeField] private MeshRenderer _movableModel;
    [SerializeField] private MeshRenderer _frozenModel;
    [SerializeField] private float _maxCubesDistance = 1.1f;

    private TrembleAnimator _animator;
    private float _sqrMaxCubesDistance;

    public int BumpsCount { get; private set; } = 0;
    public bool IsFrozen { get; private set; } = false;
    public bool IsInteractable { get; private set; } = true;
    public bool IsMovingOut { get; private set; } = false;

    public Vector3 MovementDirection => _movement.Direction;

    public event Action<Cube> Destroying;

    private void Awake()
    {
        _movableModel.gameObject.SetActive(true);
        _frozenModel.gameObject.SetActive(false);
        _movement = GetComponent<CubeMovement>();
        _sqrMaxCubesDistance = _maxCubesDistance * _maxCubesDistance;
    }

    private void OnDestroy()
    {
        _movement.Stop();
        Destroying?.Invoke(this);
    }

    public void SetRotation(Direction direction)
    {
        transform.localEulerAngles = direction switch
        {
            Direction.Up => new Vector3(0, 0, 0),
            Direction.Down => new Vector3(180, 0, 0),
            Direction.Left => new Vector3(0, 0, 90),
            Direction.Right => new Vector3(0, 0, -90),
            Direction.Forward => new Vector3(90, 0, 0),
            Direction.Backward => new Vector3(-90, 0, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction)),
        };
    }

    public void Initialize(TrembleAnimator animator)
    {
        _animator = animator;
    }

    public void Explode()
    {
        Destroy(gameObject);
    }

    public void Bump()
    {
        BumpsCount++;
        TryFreeze();
    }

    public void Touch()
    {
        if (IsFrozen || IsInteractable == false)
            return;

        IsInteractable = false;
        Vector3 direction = transform.up;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction);

        if (CheckObstacles(hits, out List<Cube> obstacles, out RaycastHit first))
        {
            Vector3 target = first.collider.transform.localPosition + transform.parent.InverseTransformDirection(first.normal);


            float time = first.distance / _settings.MovementSpeed;
            _movement.MoveTo(target, time);

            AnimateBumping(obstacles, time);
            _animator.Bumped += OnBumped;
        }
        else
        {
            _movement.MoveOut();
            IsMovingOut = true;
            _movement.MovementEnded += OnDestroyingMovementEnded;
        }
    }

    public bool TryMoveTo(Vector3 target, float time)
    {
        if (IsFrozen)
            return false;

        _movement.MoveTo(target, time);

        return true;
    }

    public void AnimateBumping(List<Cube> obstacles, float delay = 0f)
    {
        Cube[] bumpingCubes = GetBumpingCubes(obstacles);
        _animator.DoBumpsAnimation(bumpingCubes, delay);
    }

    private Cube[] GetBumpingCubes(List<Cube> obstacles)
    {
        Cube[] cubes = new Cube[obstacles.Count + 1];
        cubes[0] = this;

        for (int i = 1; i < cubes.Length; i++)
            cubes[i] = obstacles[i - 1];

        return cubes;
    }

    private bool CheckObstacles(RaycastHit[] hits, out List<Cube> obstacles, out RaycastHit first)
    {
        obstacles = new List<Cube>();
        first = default;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.TryGetComponent(out Cube cube))
            {
                if (cube.IsMovingOut)
                    continue;

                if (obstacles.Count > 0)
                {
                    Vector3 lastObstaclePosition = obstacles[^1].transform.localPosition;
                    Vector3 currentObstaclePosition = cube.transform.localPosition;

                    if ((lastObstaclePosition - currentObstaclePosition).sqrMagnitude > _sqrMaxCubesDistance)
                        break;
                }

                if (obstacles.Count == 0)
                    first = hits[i];

                obstacles.Add(cube);

                if (cube.IsFrozen)
                    break;
            }
        }

        return obstacles.Count > 0;
    }

    private bool TryFreeze()
    {
        if (IsFrozen)
            return false;

        if (BumpsCount >= _settings.BumpsToFreeze)
        {
            IsFrozen = true;
            _movableModel.gameObject.SetActive(false);
            _frozenModel.gameObject.SetActive(true);
        }

        return IsFrozen;
    }

    private void OnBumped(Cube cube)
    {
        if (cube != this)
            return;

        _animator.Bumped -= OnBumped;
        StartCoroutine(ReturnInteractions());
    }

    private void OnDestroyingMovementEnded()
    {
        _movement.MovementEnded -= OnDestroyingMovementEnded;
        Destroying?.Invoke(this);
        Destroy(gameObject);
    }

    private IEnumerator ReturnInteractions()
    {
        yield return new WaitForSeconds(_settings.TrembleTime);

        IsInteractable = true;
    }
}