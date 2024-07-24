using UnityEngine;
using Game.Cube;
using Game.Level.Map;

namespace Game.Level
{
    public class Initializer : MonoBehaviour
    {
        [SerializeField] private Transform _mapHolder;
        [SerializeField] private TrembleAnimator _trembleAnimator;
        [SerializeField] private ProgressObserver _progressObserver;
        [SerializeField] private MapObserver _mapObserver;
        [SerializeField] private InitializeStrategies _initializeStrategie;
        [SerializeField] private MapGenerator _generator;

        private void Awake()
        {
            if (_initializeStrategie == InitializeStrategies.GenerateAndInitialize)
                _generator.Generate();

            Entity[] cubes = _mapHolder.GetComponentsInChildren<Entity>();

            foreach (Entity cube in cubes)
                cube.Initialize(_trembleAnimator);

            _progressObserver.Initialize(cubes);
            _mapObserver.Initialize(cubes);
        }
    }

    public enum InitializeStrategies
    {
        InitializePrebuildedLevel,
        GenerateAndInitialize
    }
}