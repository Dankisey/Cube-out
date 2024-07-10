using System;
using UnityEngine;
using Agava.WebUtility;

namespace Game
{
    public class BackgroundChangingObserver : MonoBehaviour
    {
        public event Action IsRunningInBackground;
        public event Action IsReturnedToApp;

        private void OnEnable()
        {
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
        }

        private void OnDisable()
        {
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        }

        private void OnInBackgroundChangeApp(bool isInApp)
        {
            if (isInApp)
                IsReturnedToApp?.Invoke();
            else
                IsRunningInBackground?.Invoke();
        }

        private void OnInBackgroundChangeWeb(bool isOnBackground)
        {
            if (isOnBackground)
                IsRunningInBackground?.Invoke();
            else
                IsReturnedToApp?.Invoke();
        }
    }
}