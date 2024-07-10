using System;
using UnityEngine;

namespace Game.Level
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private BackgroundChangingObserver _backgroundChangingObserver;

        public event Action IsResumed;
        public event Action IsPaused;

        private void OnEnable()
        {
            _backgroundChangingObserver.IsRunningInBackground += Pause;
        }

        private void OnDisable()
        {
            _backgroundChangingObserver.IsRunningInBackground -= Pause;
        }

        public void Resume()
        {
            Time.timeScale = 1;
            IsResumed?.Invoke();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            IsPaused?.Invoke();
        }
    }
}