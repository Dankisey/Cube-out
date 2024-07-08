using UnityEngine;

public class VictoryController : MonoBehaviour
{
    [SerializeField] private ProgressObserver _progressObserver;
    [SerializeField] private ProgressSaver _progressSaver;
    [SerializeField] private UIGroup _victoryPanelGroup;

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
        _victoryPanelGroup.TurnOn();
    }
}