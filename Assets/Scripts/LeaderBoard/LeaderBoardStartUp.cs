using UnityEngine;

namespace LeaderBoard
{
    public class LeaderBoardStartUp : MonoBehaviour
    {
        [SerializeField] private YandexLeaderBoard _leaderboard;

        private void Start() => _leaderboard.Fill();
    }
}