using UnityEngine.SceneManagement;

namespace Game.UI.Buttons
{
    public class MainMenuButton : CustomButton
    {
        private const string MainMenu = nameof(MainMenu);

        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(MainMenu);
        }
    }
}