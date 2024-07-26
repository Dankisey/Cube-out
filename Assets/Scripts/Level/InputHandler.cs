using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.Bomb;
using Game.Cube;
using Game.Level.Map;

namespace Game.Level
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private PauseController _levelPauseController;
        [SerializeField] private BombThrower _bombThrower;
        [SerializeField] private Rotator _rotator;

        private IInputRaycastProcessor _currentRaycastProcessor;
        private BombNavigatingBehaviour _bombNavigating;
        private CubeTouchingBehaviour _cubeTouching;
        private PlayerInputActions _input;
        private Camera _camera;
        private bool _isHolding;

        public event Action BombThrowingFailed;

        private void Awake()
        {
            _camera = Camera.main;
            Time.timeScale = 1;

            _bombNavigating = new BombNavigatingBehaviour(_bombThrower);
            _cubeTouching = new CubeTouchingBehaviour();
            _currentRaycastProcessor = _cubeTouching;
        }

        private void OnEnable()
        {
            _input ??= new PlayerInputActions();
            _input.Enable();
            _input.Player.Tap.performed += OnTapped;
            _input.Player.Hold.performed += OnHoldStart;
            _input.Player.Hold.canceled += OnHoldEnd;
            _levelPauseController.IsPaused += OnGamePaused;
            _levelPauseController.IsResumed += OnGameResumed;
            _bombNavigating.ThrowingFailed += OnThrowingFailed;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Player.Tap.performed -= OnTapped;
            _input.Player.Hold.performed -= OnHoldStart;
            _input.Player.Hold.canceled -= OnHoldEnd;
            _levelPauseController.IsPaused -= OnGamePaused;
            _levelPauseController.IsResumed -= OnGameResumed;
            _bombNavigating.ThrowingFailed -= OnThrowingFailed;
        }

        private void Update()
        {
            if (_isHolding)
                _rotator.Rotate(_input.Player.TapDelta.ReadValue<Vector2>());
        }

        public void ActivateBombNavigating()
        {
            _currentRaycastProcessor = _bombNavigating;
        }

        public void ActivateCubeTouching()
        {
            _currentRaycastProcessor = _cubeTouching;
        }

        private void OnGameResumed()
        {
            StartCoroutine(EnableInput());
        }

        private void OnGamePaused()
        {
            _input.Disable();
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

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask: 1, QueryTriggerInteraction.Ignore))
            {
                _currentRaycastProcessor.ProcessRaycast(hit);
            }
        }

        private void OnThrowingFailed()
        {
            BombThrowingFailed?.Invoke();
        }

        private IEnumerator EnableInput(float delay = 0.2f)
        {
            yield return new WaitForSeconds(delay);
            
            _input.Enable();
        }
    }

    public class BombNavigatingBehaviour : IInputRaycastProcessor
    {
        private readonly BombThrower _bombThrower;

        public event Action ThrowingFailed;

        public BombNavigatingBehaviour(BombThrower thrower) => _bombThrower = thrower;

        public void ProcessRaycast(RaycastHit hit)
        {
            if (_bombThrower.TryThrow(hit.point) == false)
                ThrowingFailed?.Invoke();
        }
    }

    public class CubeTouchingBehaviour : IInputRaycastProcessor
    {
        public void ProcessRaycast(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out Entity cube))
                cube.Touch();
        }
    }

    public interface IInputRaycastProcessor
    {
        public void ProcessRaycast(RaycastHit hit);
    }
}