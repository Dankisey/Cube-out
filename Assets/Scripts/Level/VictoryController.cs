using System;
using UnityEngine;

namespace Game.Level
{
    public class VictoryController : MonoBehaviour
    {
        [SerializeField] private ProgressObserver _progressObserver;
        [SerializeField] private ProgressSaver _progressSaver;

        public event Action LevelCompleted;

        private void OnEnable()
        {
            _progressObserver.LevelCompleted += OnLevelCompleted;
        }

        private void OnDisable()
        {
            _progressObserver.LevelCompleted -= OnLevelCompleted;
        }

        private void OnLevelCompleted()
        {
            _progressSaver.SaveCurrentLevelCompletition(OnProgressSaved);
        }

        private void OnProgressSaved()
        {
            LevelCompleted?.Invoke();
        }
    }
}