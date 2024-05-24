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

    private TrembleAnimator _animator;

    public int BumpsCount { get; private set; } = 0;
    public bool IsFrozen { get; private set; } = false;
    public bool IsInteractable { get; private set; } = true;
    public bool IsMovingOut { get; private set; } = false;

    public event Action<Cube> Destroying;

    private void Awake()
    {
        _movableModel.gameObject.SetActive(true);
        _frozenModel.gameObject.SetActive(false);
        _movement = GetComponent<CubeMovement>();
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
            Vector3 target = obstacles[0].transform.position + first.normal;

            float time = first.distance / _settings.MovementSpeed;
            _movement.MoveTo(target, time);

            AnimateBumping(obstacles, direction, time);
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

    public void AnimateBumping(List<Cube> obstacles, Vector3 direction, float delay = 0f)
    {
        Cube[] bumpingCubes = GetBumpingCubes(obstacles);
        _animator.DoBumpsAnimation(bumpingCubes, direction, delay);
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
        bool hasObstacles = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                if (cube.IsMovingOut)
                    continue;

                if (hasObstacles == false)
                {
                    first = hit;
                    hasObstacles = true;
                }

                obstacles.Add(cube);

                if (cube.IsFrozen)
                    break;
            }
        }

        return hasObstacles;
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