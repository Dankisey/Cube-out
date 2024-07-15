using UnityEngine;
using Localization;
using TMPro;

namespace LeaderBoard
{
    public class LeaderboardElement : MonoBehaviour
    {
        private const string Score = nameof(Score);
        private const string Rank = nameof(Rank);

        [SerializeField] private URLImage _image;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerScore;
        [SerializeField] private TMP_Text _playerRank;

        private int _score;
        private int _rank;

        public void Initialize(Localizator localization, string imageURL, string name, int score, int rank)
        {
            _image.Initialize(imageURL);
            _playerName.text = name;
            _score = score;
            _rank = rank;
            localization.GetTranslation(Score, SetScoreText);
            localization.GetTranslation(Rank, SetRankText);
        }

        private void SetScoreText(string scoreTranslation) => _playerScore.text = $"{scoreTranslation}: {_score}";

        private void SetRankText(string rankTranslation) => _playerRank.text = $"{rankTranslation}: {_rank}";
    }
}