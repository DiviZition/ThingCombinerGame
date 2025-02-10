using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveInput : IMoveInput, IDisposable
{
    public Vector3 MoveDirectionXZ => GetMoveDirectionXZ();
    public sbyte MoveDirectionY => GetVerticalMoveDirection();
    public bool IsSprinting {  get; private set; }

    private DefaultInputs _inputs;

    public PlayerMoveInput(DefaultInputs inputs)
    {
        _inputs = inputs;

        _inputs.GamePlay.Sprint.started += OnStartSprinting;
        _inputs.GamePlay.Sprint.canceled += OnSprintingStopped;
    }

    private Vector3 GetMoveDirectionXZ()
    {
        Vector2 inputDirection = _inputs.GamePlay.MoveDirection.ReadValue<Vector2>();

        return new Vector3(inputDirection.x, 0, inputDirection.y);
    }

    private sbyte GetVerticalMoveDirection()
    {
        sbyte direction = 0;
        if (_inputs.GamePlay.MoveUp.ReadValue<bool>() == true)
            direction = 1;
        else if (_inputs.GamePlay.MoveDown.ReadValue<bool>() == true)
            direction = -1;

        return direction;
    }

    private void OnStartSprinting(InputAction.CallbackContext context) => IsSprinting = true;
    private void OnSprintingStopped(InputAction.CallbackContext context) => IsSprinting = false;

    public void Dispose()
    {
        _inputs.GamePlay.Sprint.started -= OnStartSprinting;
        _inputs.GamePlay.Sprint.started -= OnSprintingStopped;
    }
}