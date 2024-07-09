using System.Collections.Generic;
using UnityEngine;

namespace LeaderBoard
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private LeaderboardElement _elementPrefab;

        private List<LeaderboardElement> _spawnedElements = new();

        public void ConstructLeaderboard(List<LeaderboardPlayer> leaderboardPlayers)
        {
            ClearLeadeboard();

            foreach (var player in leaderboardPlayers)
            {
                LeaderboardElement elementInstance = Instantiate(_elementPrefab, _container);
                elementInstance.Initialize(player.ImageURL, player.Name, player.Score, player.Rank);

                _spawnedElements.Add(elementInstance);
            }
        }

        private void ClearLeadeboard()
        {
            foreach (var element in _spawnedElements)
                Destroy(element.gameObject);

            _spawnedElements = new List<LeaderboardElement>();
        }
    }
}