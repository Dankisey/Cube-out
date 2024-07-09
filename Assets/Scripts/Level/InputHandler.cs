using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Game.Bomb;
using Game.Cube;
using Game.Level.Map;

namespace Game.Level
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PauseController _levelPauseController;
        [SerializeField] private BombThrower _bombThrower;
        [SerializeField] private Button _bombButton;
        [SerializeField] private Rotator _rotator;

        private PlayerInputActions _input;
        private Camera _camera;
        private bool _isHolding = false;
        private bool _isAimingBomb = false;

        private void Awake()
        {
            _camera = Camera.main;
            _input ??= new PlayerInputActions();
            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.Tap.performed += OnTapped;
            _input.Player.Hold.performed += OnHoldStart;
            _input.Player.Hold.canceled += OnHoldEnd;
            _bombButton.onClick.AddListener(OnBombButtonClicked);
            _levelPauseController.IsPaused += OnGamePaused;
            _levelPauseController.IsResumed += OnGameResumed;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Tap.performed -= OnTapped;
            _input.Player.Hold.performed -= OnHoldStart;
            _input.Player.Hold.canceled -= OnHoldEnd;
            _bombButton.onClick.RemoveListener(OnBombButtonClicked);
            _levelPauseController.IsPaused -= OnGamePaused;
            _levelPauseController.IsResumed -= OnGameResumed;
        }

        private void Update()
        {
            if (_isHolding)
                _rotator.Rotate(_input.Player.TapDelta.ReadValue<Vector2>());
        }

        private void OnGameResumed()
        {
            _input.Enable();
        }

        private void OnGamePaused()
        {
            _input.Disable();
        }

        private void OnBombButtonClicked()
        {
            _isAimingBomb = true;
        }

        private void OnHoldStart(InputAction.CallbackContext context)
        {
            _isHolding = true;
        }

        private void OnHoldEnd(InputAction.CallbackContext context)
        {
            _isHolding = false;
        }

        private void OnTapped(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(_input.Player.TapPosition.ReadValue<Vector2>());
            int layerMask = 1;

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (_isAimingBomb)
                {
                    _bombThrower.TryThrow(hit.point);
                    _isAimingBomb = false;

                    return;
                }

                if (hit.collider.TryGetComponent(out Entity cube))
                    cube.Touch();
            }
        }
    }
}