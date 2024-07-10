using Game.Level;
using UnityEngine;

namespace Game.UI
{
    public class VicoryUIGroup : UIGroup
    {
        [SerializeField] private VictoryController _victoryController;

        private void OnEnable()
        {
            _victoryController.LevelCompleted += TurnOn;
        }

        private void OnDisable()
        {
            _victoryController.LevelCompleted -= TurnOn;
        }
    }
}