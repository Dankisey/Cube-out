using UnityEngine.SceneManagement;

namespace Game.UI.Buttons
{
    public class LevelsMenuButton : CustomButton
    {
        private const string LevelsMenu = nameof(LevelsMenu);

        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(LevelsMenu);
        }
    }
}