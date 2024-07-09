using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIGroup : MonoBehaviour
    {
        [SerializeField] private TurningOffStrategie _turningOffStrategie;
        [SerializeField] private bool _turnOffOnAwake;

        private DisableStrategie _disableStrategie;
        private CanvasGroup _canvasGroup;

        private enum TurningOffStrategie
        {
            DisableInteractions,
            FullyDisable
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            switch (_turningOffStrategie)
            {
                case TurningOffStrategie.DisableInteractions:
                    _disableStrategie = new DisableInteractionStrategie(_canvasGroup);
                    break;
                case TurningOffStrategie.FullyDisable:
                    _disableStrategie = new FullyDisableStrategie(_canvasGroup);
                    break;
            }

            if (_turnOffOnAwake)
                TurnOff();
        }

        public void TurnOn()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        public void TurnOff()
        {
            _disableStrategie.TurnOff();
        }

        private abstract class DisableStrategie
        {
            protected CanvasGroup CanvasGroup;

            public DisableStrategie(CanvasGroup canvasGroup)
            {
                CanvasGroup = canvasGroup;
            }

            public abstract void TurnOff();
        }

        private class DisableInteractionStrategie : DisableStrategie
        {
            public DisableInteractionStrategie(CanvasGroup canvasGroup) : base(canvasGroup) { }

            public override void TurnOff()
            {
                CanvasGroup.blocksRaycasts = false;
                CanvasGroup.interactable = false;
            }
        }

        private class FullyDisableStrategie : DisableStrategie
        {
            public FullyDisableStrategie(CanvasGroup canvasGroup) : base(canvasGroup) { }

            public override void TurnOff()
            {
                CanvasGroup.alpha = 0;
                CanvasGroup.blocksRaycasts = false;
                CanvasGroup.interactable = false;
            }
        }
    }
}