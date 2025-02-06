using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunMoveStrategy : IControllable, IDisposable
{
    private CharacterController _controller;
    private MovementSettingsConfig _moveConfig;
    private PlayerInputs _inputs;
    private Camera _camera;

    private float _fallVelocity;
    private bool _isSprinting;

    public PlayerRunMoveStrategy(CharacterController controller, MovementSettingsConfig moveConfig, PlayerInputs inputs, Camera camera)
    {
        _controller = controller;
        _moveConfig = moveConfig;
        _inputs = inputs;
        _camera = camera;

        _inputs.GamePlay.Sprint.started += OnStartSprint;
        _inputs.GamePlay.Sprint.canceled += OnStopSprinting;
        _inputs.GamePlay.MoveDirection.performed += GetMoveInput;
    }
    public void OnEnter() {  }  

    private void GetMoveInput(InputAction.CallbackContext obj) => obj.ReadValue<Vector2>();
    private void OnStartSprint(InputAction.CallbackContext context) => _isSprinting = true;
    private void OnStopSprinting(InputAction.CallbackContext context) => _isSprinting = false;

    public void Perform()
    {
        Vector3 moveDirection = CalculateMoveDirection();

        moveDirection *= _isSprinting ? _moveConfig.SprintSpeed : _moveConfig.MoveSpeed;

        moveDirection.y = DefineGravity();

        _controller.Move(moveDirection * Time.fixedDeltaTime);
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector2 inputs = _inputs.GamePlay.MoveDirection.ReadValue<Vector2>();

        Vector3 moveDirection = _camera.transform.forward * inputs.y + _camera.transform.right * inputs.x;
        moveDirection.y = 0;
        moveDirection.Normalize();

        return moveDirection;
    }

    public float DefineGravity()
    {
        if (_controller.isGrounded == true)
        {
            _fallVelocity = Mathf.Abs(_moveConfig.GroundedGravity) * -1;
        }
        else if (_fallVelocity <= Mathf.Abs(_moveConfig.MaxFallSpeed) * -1)
        {
            _fallVelocity = Mathf.Abs(_moveConfig.MaxFallSpeed) * -1;
        }
        else
        {
            _fallVelocity += Mathf.Abs(_moveConfig.FreeFallGravityAcceleration) * -1;
        }

        return _fallVelocity;
    }

    public void OnExit()
    {
    }

    public void Dispose()
    {
        _inputs.GamePlay.Sprint.started -= OnStartSprint;
        _inputs.GamePlay.Sprint.canceled -= OnStopSprinting;
        _inputs.GamePlay.MoveDirection.performed -= GetMoveInput;
    }
}