using System;
using UnityEngine;

public class LevelPauseController : MonoBehaviour
{
    public event Action IsResumed;
    public event Action IsPaused;

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