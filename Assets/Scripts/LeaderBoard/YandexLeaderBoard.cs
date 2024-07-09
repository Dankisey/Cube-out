using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;

namespace LeaderBoard
{
    public class YandexLeaderBoard : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";
        private const string AnonymousName = "Anonymous";

        [SerializeField] private LeaderboardView _leaderboardView;

        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

        public void IncreasePlayerScore()
        {
            Leaderboard.GetPlayerEntry(LeaderboardName, (entryResponse) =>
            {
                if (entryResponse == null)
                    SetPlayerScore(1);
                else
                    SetPlayerScore(entryResponse.score + 1);
            });
        }

        public void Fill()
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            _leaderboardPlayers.Clear();

            Leaderboard.GetEntries(LeaderboardName, (entryResponse) =>
            {
                foreach (var entry in entryResponse.entries)
                {
                    string imageURL = entry.player.profilePicture;
                    string name = entry.player.publicName;
                    int score = entry.score;
                    int rank = entry.rank;

                    if (string.IsNullOrEmpty(name))
                        name = AnonymousName;

                    _leaderboardPlayers.Add(new LeaderboardPlayer(imageURL, name, score, rank));
                }

                _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
            });
        }

        private void SetPlayerScore(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetPlayerEntry(LeaderboardName, (entryResponse) =>
            {
                if (entryResponse == null || entryResponse.score < score)
                    Leaderboard.SetScore(LeaderboardName, score);
            });
        }
    }
}