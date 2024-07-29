using System;
using UnityEngine;

namespace Game.Level
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private BackgroundChangingObserver _backgroundChangingObserver;
        [SerializeField] private AdShower _adShower;

        public event Action IsResumed;
        public event Action IsPaused;

        private void OnEnable()
        {
            _backgroundChangingObserver.IsRunningInBackground += Pause;
            _adShower.SuggestingAdd += Pause;
            _adShower.AdStarted += Pause;
            _adShower.AdEnded += Resume;
        }

        private void OnDisable()
        {
            _backgroundChangingObserver.IsRunningInBackground -= Pause;
            _adShower.SuggestingAdd -= Pause;
            _adShower.AdStarted -= Pause;
            _adShower.AdEnded -= Resume;
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