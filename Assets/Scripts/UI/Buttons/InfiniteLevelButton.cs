using UnityEngine;

public class InfiniteLevelButton : CustomButton
{
    [SerializeField] private LevelLoader _levelLoader;

    protected override void OnButtonClick()
    {
        _levelLoader.LoadInfiniteLevel();
    }
}