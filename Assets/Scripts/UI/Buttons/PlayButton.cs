using UnityEngine;

public class PlayButton : CustomButton
{
    [SerializeField] private LevelLoader _levelLoader;

    protected override void OnButtonClick()
    {
        _levelLoader.LoadLastAvailableLevel();
    }
}