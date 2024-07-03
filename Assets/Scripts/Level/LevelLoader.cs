using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelLoader : MonoBehaviour
{
    private const string InfiniteLevel = nameof(InfiniteLevel);
    private const string Level = nameof(Level);

    [SerializeField] private ProgressSaver _progressSaver;

    public void LoadLastAvailableLevel()
    {
        int lastAvailableLevelIndex = _progressSaver.GetLastCompletedLevelIndex() + 1;
        int levelsAmount = _progressSaver.GetLevelsAmount();

        if (levelsAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(levelsAmount));

        if (lastAvailableLevelIndex <= levelsAmount)
            TryLoadLevel(lastAvailableLevelIndex);
        else
            LoadInfiniteLevel();
    }

    public void LoadInfiniteLevel()
    {
        SceneManager.LoadScene(InfiniteLevel);
    }

    public bool TryLoadLevel(int levelIndex)
    {
        if (levelIndex > _progressSaver.GetLevelsAmount() || levelIndex <= 0)
            return false;

        SceneManager.LoadScene($"{Level} {levelIndex}");

        return true;
    }

    public bool CanLoadLevel(int levelIndex)
    {
        int lastAvailableLevelIndex = _progressSaver.GetLastCompletedLevelIndex() + 1;

        return levelIndex <= lastAvailableLevelIndex;
    }
}