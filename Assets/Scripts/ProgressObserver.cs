using System;
using UnityEngine;

public class ProgressObserver : MonoBehaviour
{
    private int _destroyedCubes = 0;
    private int _cubesOnStart;

    public event Action<float> ProgressChanged;
    public event Action LevelCompleted;

    public void Initialize(Cube[] cubes)
    {
        _cubesOnStart = cubes.Length;

        foreach (Cube cube in cubes)
            cube.Destroying += OnCubeDestroying;
    }

    private void OnCubeDestroying(Cube cube)
    {
        cube.Destroying -= OnCubeDestroying;

        _destroyedCubes++;
        ProgressChanged?.Invoke((float)_destroyedCubes /  _cubesOnStart);

        if (_destroyedCubes == _cubesOnStart)
            LevelCompleted?.Invoke();
    }
}