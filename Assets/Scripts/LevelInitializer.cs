using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private Transform _mapHolder;
    [SerializeField] private TrembleAnimator _trembleAnimator;
    [SerializeField] private ProgressObserver _progressObserver;

    private void Awake()
    {
        Cube[] cubes = GetComponentsInChildren<Cube>();

        foreach (Cube cube in cubes)  
            cube.Initialize(_trembleAnimator);
        
        _progressObserver.Initialize(cubes);
    }
}