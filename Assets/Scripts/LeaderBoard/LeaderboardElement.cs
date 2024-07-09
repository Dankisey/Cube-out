using UnityEngine;
using TMPro;

namespace LeaderBoard
{
    public class LeaderboardElement : MonoBehaviour
    {
        private const string Score = nameof(Score);
        private const string Rank = nameof(Rank);

        [SerializeField] private Localization.Localization _localization;
        [SerializeField] private URLImage _image;
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerScore;
        [SerializeField] private TMP_Text _playerRank;

        public void Initialize(string imageURL, string name, int score, int rank)
        {
            _image.Initialize(imageURL);
            _playerName.text = name;
            _playerScore.text = $"{_localization.GetTranslation(Score)}: {score.ToString()}";
            _playerRank.text = $"{_localization.GetTranslation(Rank)}: {rank.ToString()}";
        }
    }
}