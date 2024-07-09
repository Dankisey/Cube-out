using System;
using System.Collections;
using UnityEngine;

namespace Game.Cube
{
    public class TrembleAnimator : MonoBehaviour
    {
        [SerializeField] private CubeSettings _settings;
        [SerializeField] private Transform _mapRoot;
        [SerializeField] private float _maxCubesDistance = 1.1f;

        private float _sqrMaxCubesDistance;

        public event Action<Entity> Bumped;

        private void Awake()
        {
            _sqrMaxCubesDistance = _maxCubesDistance * _maxCubesDistance;
        }

        public void DoBumpsAnimation(Entity[] cubes, float invokeDelay)
        {
            StartCoroutine(BumpsAnimation(cubes, invokeDelay));
        }

        private IEnumerator BumpsAnimation(Entity[] cubes, float invokeDelay)
        {
            yield return new WaitForSeconds(invokeDelay);

            Vector3 startPosition;
            Vector3 endPosition;

            var wait = new WaitForSeconds(_settings.TrembleTime);
            Vector3 localDirection = _mapRoot.InverseTransformDirection(cubes[0].MovementDirection);

            for (int i = 0; i < cubes.Length; i++)
            {
                startPosition = cubes[i].transform.localPosition;
                endPosition = startPosition + localDirection * _settings.TrembleDistance;

                cubes[i].TryMoveTo(endPosition, _settings.TrembleTime);

                yield return wait;

                Bumped?.Invoke(cubes[i]);

                cubes[i].TryMoveTo(startPosition, _settings.TrembleTime);

                if (i + 1 < cubes.Length)
                {
                    if ((cubes[i + 1].transform.localPosition - startPosition).sqrMagnitude > _sqrMaxCubesDistance)
                        break;

                    cubes[i + 1].Bump();

                    if (cubes[i + 1].IsFrozen)
                        break;
                }
            }
        }
    }
}