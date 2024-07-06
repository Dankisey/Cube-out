using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;


public class ProgressSaver : MonoBehaviour
{
    private const string Level = nameof(Level);

    [SerializeField] private LevelSettings _levelSettings;

    public void SaveCurrentLevelCompletition()
    {
        string levelName = SceneManager.GetActiveScene().name;

        PlayerPrefs.SetInt(levelName, _levelSettings.MaxStars);
        PlayerPrefs.Save();
    }

    public void DeleteLevelProgress(int levelIndex)
    {
        PlayerPrefs.DeleteKey($"{Level} {levelIndex}");
        PlayerPrefs.Save();
    }

    public bool TrySetLevelProgress(string levelName, int stars)
    {
        if (stars <= 0 || stars > _levelSettings.MaxStars)
            throw new ArgumentOutOfRangeException(nameof(stars));

        if (PlayerPrefs.HasKey(levelName))
        {
            if (PlayerPrefs.GetInt(levelName) > stars)
                return false;
        }
        
        PlayerPrefs.SetInt(levelName, stars);
        PlayerPrefs.Save();

        return true;
    }

    public int GetLevelProgress(int levelIndex)
    {
        if (levelIndex > _levelSettings.AvailableLevels)
            throw new ArgumentOutOfRangeException(nameof(levelIndex));

        string levelKey = $"{Level} {levelIndex}";

        if (PlayerPrefs.HasKey(levelKey))
            return PlayerPrefs.GetInt(levelKey);
        else
            return 0;
    }

    public int GetLastCompletedLevelIndex()
    {
        int lastCompletedLevelIndex = 0;

        for (int i = 1; i <= _levelSettings.AvailableLevels; i++)
        {
            if (PlayerPrefs.HasKey($"{Level} {i}"))    
                lastCompletedLevelIndex = i;
            else   
                break;          
        }

        return lastCompletedLevelIndex;
    }

    public int GetLevelsAmount()
    {
        return _levelSettings.AvailableLevels;
    }

    public int GetMaxStars()
    {
        return _levelSettings.MaxStars;
    }
}