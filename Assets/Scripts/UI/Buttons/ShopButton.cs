using UnityEngine.SceneManagement;

namespace Game.UI.Buttons
{
    public class ShopButton : CustomButton
    {
        private const string Shop = nameof(Shop);

        protected override void OnButtonClick()
        {
            SceneManager.LoadScene(Shop);
        }
    }
}