using System;
using UnityEngine;
using Game.Cube;

namespace Game.Level
{
    public class ProgressObserver : MonoBehaviour
    {
        [SerializeField] private ProgressSaver _progressSaver;

        private float _currentProgress = 0f;
        private float _starPercentage;
        private int _destroyedCubes = 0;
        private int _currentStars = 0;
        private int _cubesOnStart;


        public event Action<float> ProgressChanged;
        public event Action<int> StarsAmountChanged;
        public event Action LevelCompleted;

        private void Awake()
        {
            _starPercentage = 1f / _progressSaver.GetMaxStars();
        }

        public void Initialize(Entity[] cubes)
        {
            _cubesOnStart = cubes.Length;

            foreach (Entity cube in cubes)
                cube.Destroying += OnCubeDestroying;
        }

        private void OnCubeDestroying(Entity cube)
        {
            cube.Destroying -= OnCubeDestroying;

            _destroyedCubes++;
            _currentProgress = (float)_destroyedCubes / _cubesOnStart;
            int stars = GetStarsAmount();

            ProgressChanged?.Invoke(_currentProgress);

            if (stars != _currentStars)
            {
                _currentStars = stars;
                StarsAmountChanged?.Invoke(_currentStars);
            }

            if (_destroyedCubes == _cubesOnStart)
                LevelCompleted?.Invoke();
        }

        private int GetStarsAmount()
        {
            int maxStars = _progressSaver.GetMaxStars();

            if (Mathf.Approximately(_currentProgress, 1f))
                return maxStars;

            int stars = (int)(_currentProgress / _starPercentage);

            if (stars == maxStars)
                stars--;

            return stars;
        }
    }
}