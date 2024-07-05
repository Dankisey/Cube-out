using UnityEngine.SceneManagement;

public class ShopButton : CustomButton
{
    private const string Shop = nameof(Shop);

    protected override void OnButtonClick()
    {
        SceneManager.LoadScene(Shop);
    }
}