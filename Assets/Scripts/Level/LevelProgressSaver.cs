using UnityEngine;
using System;

public class LevelProgressSaver
{
    private const string Level = nameof(Level);

    [SerializeField] private LevelSettings _levelSettings;

    public void SetLevelProgress(int levelIndex, int stars)
    {
        if (levelIndex <= 0 || levelIndex > _levelSettings.AvailableLevels)
            throw new ArgumentOutOfRangeException(nameof(levelIndex));

        if (stars <= 0 || stars > _levelSettings.MaxStars)
            throw new ArgumentOutOfRangeException(nameof(stars));

        PlayerPrefs.SetInt($"{Level} {levelIndex}", stars);
        PlayerPrefs.Save();
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

        for (int i = 1; i < _levelSettings.AvailableLevels; i++)
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
}