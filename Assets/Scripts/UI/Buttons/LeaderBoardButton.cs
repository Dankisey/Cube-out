using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI.Buttons
{
    public class LeaderBoardButton : CustomButton
    {
        private const string Leaderboard = nameof(Leaderboard);

        [SerializeField] private Authorizer _authorizer;
        [SerializeField] private UIGroup _selfGroup;
        [SerializeField] private UIGroup _authorizingGroup;

        protected override void OnButtonClick()
        {
            _selfGroup.TurnOff();

            if (_authorizer.IsPlayerAuthorized == false)
            {
                _authorizingGroup.TurnOn();
                return;
            }

            if (_authorizer.IsPlayerAuthorized)
            {
                _authorizer.DoDataRequest(() => SceneManager.LoadScene(Leaderboard));
            }
        }
    }
}