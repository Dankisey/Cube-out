using System;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private Transform _mapHolder;
    [SerializeField] private TrembleAnimator _trembleAnimator;
    [SerializeField] private ProgressObserver _progressObserver;
    [SerializeField] private InitializeStrategies _initializeStrategie;
    [SerializeField] private MapGenerator _generator;

    private void Awake()
    {
        if (_initializeStrategie == InitializeStrategies.GenerateAndInitialize)
            _generator.Generate();

        Cube[] cubes = _mapHolder.GetComponentsInChildren<Cube>();

        foreach (Cube cube in cubes)
            cube.Initialize(_trembleAnimator);

        _progressObserver.Initialize(cubes);
    }
}

public enum InitializeStrategies
{
    InitializePrebuildedLevel,
    GenerateAndInitialize
}