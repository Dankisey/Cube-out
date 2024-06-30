using UnityEngine.SceneManagement;

public class LevelsMenuButton : CustomButton
{
    private const string LevelsMenu = nameof(LevelsMenu);

    protected override void OnButtonClick()
    {
        SceneManager.LoadScene(LevelsMenu);
    }
}