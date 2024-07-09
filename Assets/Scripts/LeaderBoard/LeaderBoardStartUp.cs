using UnityEngine;

namespace LeaderBoard
{
    public class LeaderBoardStartUp : MonoBehaviour
    {
        [SerializeField] private YandexLeaderBoard _leaderboard;

        private void Awake() => _leaderboard.Fill();
    }
}