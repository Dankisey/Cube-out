using UnityEngine.SceneManagement;

public class RetryButton : CustomButton
{
    protected override void OnButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}