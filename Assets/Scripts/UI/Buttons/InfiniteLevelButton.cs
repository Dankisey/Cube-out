using UnityEngine;
using Game.Level;

namespace Game.UI.Buttons
{
    public class InfiniteLevelButton : CustomButton
    {
        [SerializeField] private Loader _loader;

        protected override void OnButtonClick()
        {
            _loader.LoadInfiniteLevel();
        }
    }
}