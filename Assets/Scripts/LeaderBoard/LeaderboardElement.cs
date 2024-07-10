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

        public void Initialize(Localizator localization, string imageURL, string name, int score, int rank)
        {
            _image.Initialize(imageURL);
            _playerName.text = name;
            _playerScore.text = $"{localization.GetTranslation(Score)}: {score.ToString()}";
            _playerRank.text = $"{localization.GetTranslation(Rank)}: {rank.ToString()}";
        }
    }
}