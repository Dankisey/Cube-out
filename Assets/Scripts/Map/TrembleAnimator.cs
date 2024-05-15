using System;
using System.Collections;
using UnityEngine;

public class TrembleAnimator : MonoBehaviour
{
    [SerializeField] private CubeSettings _settings;

    public event Action<Cube> Bumped;

    public void DoBumpsAnimation(Cube[] cubes, Vector3 direction, float invokeDelay)
    {
        StartCoroutine(BumpsAnimation(cubes, direction, invokeDelay));
    }

    private IEnumerator BumpsAnimation(Cube[] cubes, Vector3 direction, float invokeDelay)
    {
        yield return new WaitForSeconds(invokeDelay);

        Vector3 startPosition;
        Vector3 endPosition;

        var wait = new WaitForSeconds(_settings.TrembleTime);

        for (int i = 0; i < cubes.Length; i++)
        {
            startPosition = cubes[i].transform.position;
            endPosition = startPosition + direction * _settings.TrembleDistance;

            cubes[i].TryMoveTo(endPosition, _settings.TrembleTime);

            yield return wait;

            Bumped?.Invoke(cubes[i]);

            cubes[i].TryMoveTo(startPosition, _settings.TrembleTime);

            if (i + 1 < cubes.Length)
            { 
                cubes[i + 1].Bump();

                if (cubes[i + 1].IsFrozen)
                    break;
            }
        }
    }
}