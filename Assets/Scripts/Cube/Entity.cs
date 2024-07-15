using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace Game.Cube
{
    [RequireComponent(typeof(Movement))]
    public class Entity : MonoBehaviour
    {
        [SerializeField] private CubeSettings _settings;
        [SerializeField] private Movement _movement;
        [SerializeField] private MeshRenderer _movableModel;
        [SerializeField] private MeshRenderer _frozenModel;

        private TrembleAnimator _animator;

        public int BumpsCount { get; private set; } = 0;
        public bool IsFrozen { get; private set; } = false;
        public bool IsInteractable { get; private set; } = true;
        public bool IsMovingOut { get; private set; } = false;

        public Vector3 MovementDirection => _movement.Direction;

        public event Action<Entity> Destroying;
        public event Action Frozen;

        private void Awake()
        {
            _movableModel.gameObject.SetActive(true);
            _frozenModel.gameObject.SetActive(false);
            _movement = GetComponent<Movement>();
        }

        private void OnDestroy()
        {
            _movement.Stop();
            Destroying?.Invoke(this);
        }

        public void SetRotation(Directions direction)
        {
            transform.localEulerAngles = direction switch
            {
                Directions.Up => new Vector3(0, 0, 0),
                Directions.Down => new Vector3(180, 0, 0),
                Directions.Left => new Vector3(0, 0, 90),
                Directions.Right => new Vector3(0, 0, -90),
                Directions.Forward => new Vector3(90, 0, 0),
                Directions.Backward => new Vector3(-90, 0, 0),
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
            RaycastHit[] hits = Physics.RaycastAll(transform.position, MovementDirection);

            if (CheckObstacles(hits, out List<Entity> obstacles, out RaycastHit first))
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

        public void AnimateBumping(List<Entity> obstacles, float delay = 0f)
        {
            Entity[] bumpingCubes = GetBumpingCubes(obstacles);
            _animator.DoBumpsAnimation(bumpingCubes, delay);
        }

        private Entity[] GetBumpingCubes(List<Entity> obstacles)
        {
            Entity[] cubes = new Entity[obstacles.Count + 1];
            cubes[0] = this;

            for (int i = 1; i < cubes.Length; i++)
                cubes[i] = obstacles[i - 1];

            return cubes;
        }

        private bool CheckObstacles(RaycastHit[] hits, out List<Entity> obstacles, out RaycastHit first)
        {    
            obstacles = new List<Entity>();
            Vector3 selfPosition = transform.position;
            hits = hits.OrderBy(hit => GetSqrMagnitude(selfPosition, hit.point)).ToArray();
            first = default;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent(out Entity cube))
                {
                    if (cube.IsMovingOut)
                        continue;

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
                Frozen?.Invoke();
            }

            return IsFrozen;
        }

        private void OnBumped(Entity cube)
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

        private float GetSqrMagnitude(Vector3 firstPosition, Vector3 secondPosition) => 
            (secondPosition - firstPosition).sqrMagnitude;
    }

    public enum Directions
    {
        None = 0,
        Up = 1,
        Down = -1,
        Right = 2,
        Left = -2,
        Forward = 3,
        Backward = -3
    }
}