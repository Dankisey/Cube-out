using UnityEngine.SceneManagement;

namespace Game.UI.Buttons
{
    public class RetryButton : CustomButton
    {
        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}