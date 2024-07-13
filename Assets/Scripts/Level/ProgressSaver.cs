using System;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_WEBGL && !UNITY_EDITOR
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;
#endif

namespace Game
{
    public class ProgressSaver : MonoBehaviour
    {
        private const string Level = nameof(Level);

        [SerializeField] private LevelSettings _levelSettings;

        private Action<int> _checkLevelsCallback = null;

        public void SaveCurrentLevelCompletition(Action callback)
        {
            string levelName = SceneManager.GetActiveScene().name;

            PlayerPrefs.SetInt(levelName, _levelSettings.MaxStars);

#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerPrefs.Save(onSuccessCallback : callback);
#else
            PlayerPrefs.Save();
            callback.Invoke();
#endif
        }

        public void DeleteLevelProgress(int levelIndex)
        {
            PlayerPrefs.DeleteKey($"{Level} {levelIndex}");
            PlayerPrefs.Save();
        }

        public void CheckLastCompletedLevelIndex(Action<int> callback)
        {
            if (_checkLevelsCallback != null)
                _checkLevelsCallback += callback;
            else
                _checkLevelsCallback = callback;

#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerPrefs.Load(onSuccessCallback: CheckLevels);
#else
            CheckLevels();
#endif
        }

        public int GetLevelsAmount()
        {
            return _levelSettings.AvailableLevels;
        }

        public int GetMaxStars()
        {
            return _levelSettings.MaxStars;
        }

        private void CheckLevels()
        {
            int lastCompletedLevelIndex = 0;

            for (int i = 1; i <= _levelSettings.AvailableLevels; i++)
            {
                if (PlayerPrefs.HasKey($"{Level} {i}"))
                    lastCompletedLevelIndex = i;
                else
                    break;
            }

            _checkLevelsCallback.Invoke(lastCompletedLevelIndex);
            _checkLevelsCallback = null;
        }
    }
}