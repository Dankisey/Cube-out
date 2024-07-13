using UnityEngine;

namespace Game.Level
{
    public class MusicController : MonoBehaviour
    {
        private const string IsMutedInSettings = nameof(IsMutedInSettings);
        private const int True = 1;
        private const int False = 0;

        [SerializeField] private BackgroundChangingObserver _backgroundChangingObserver;
        [SerializeField] private AudioSource _audioSource;

        public bool IsMuted => PlayerPrefs.GetInt(IsMutedInSettings) == True;

        private void Awake()
        {
            if (PlayerPrefs.HasKey(IsMutedInSettings))
            {
                if (IsMuted)
                    _audioSource.mute = true;
                else
                    _audioSource.mute = false;
            }
            else
            {
                PlayerPrefs.SetInt(IsMutedInSettings, False);
            }
        }

        private void OnEnable()
        {
            _backgroundChangingObserver.IsReturnedToApp += OnReturnedToApp;
            _backgroundChangingObserver.IsRunningInBackground += OnRunningInBackground;
        }

        private void OnDisable()
        {
            _backgroundChangingObserver.IsReturnedToApp -= OnReturnedToApp;
            _backgroundChangingObserver.IsRunningInBackground -= OnRunningInBackground;
        }

        public void MuteAll()
        {
            PlayerPrefs.SetInt(IsMutedInSettings, True);
            Mute();
        }

        public void UnmuteAll()
        {
            PlayerPrefs.SetInt(IsMutedInSettings, False);
            Unmute();
        }

        private void Mute()
        {
            _audioSource.mute = true;
        }

        private void Unmute()
        {
            _audioSource.mute = false;
        }

        private void OnReturnedToApp()
        {
            if (IsMuted == false)
                Unmute();
        }

        private void OnRunningInBackground()
        {
            if (IsMuted)
                return;

            Mute();
        }
    }
}