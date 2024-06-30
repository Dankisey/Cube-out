using UnityEngine.SceneManagement;

public class MainMenuButton : CustomButton
{
    private const string MainMenu = nameof(MainMenu);

    protected override void OnButtonClick()
    {
        SceneManager.LoadScene(MainMenu);
    }
}