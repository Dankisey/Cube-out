using UnityEngine.SceneManagement;
using Agava.YandexGames;

namespace Game.UI.Buttons
{
    public class LeaderBoardButton : CustomButton
    {
        private const string Leaderboard = nameof(Leaderboard);

        protected override void OnButtonClick()
        {
            PlayerAccount.Authorize();

            if (PlayerAccount.IsAuthorized)
                PlayerAccount.RequestPersonalProfileDataPermission();

            if (PlayerAccount.IsAuthorized == false)
                return;

            SceneManager.LoadScene(Leaderboard);
        }
    }
}