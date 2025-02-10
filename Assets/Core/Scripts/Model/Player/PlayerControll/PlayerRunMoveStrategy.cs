using UnityEngine;

public class PlayerRunMoveStrategy : IControllable
{
    private CharacterController _controller;
    private Transform _transform;
    private MovementSettingsConfig _moveConfig;
    private IMoveInput _inputs;
    private Camera _camera;
    private Transform _cameraTransform;

    private float _fallVelocity;

    public bool IsMoveing => _inputs.MoveDirectionXZ != Vector3.zero;
    public bool IsSprinting => _inputs.IsSprinting;

    public PlayerRunMoveStrategy(CharacterController controller, MovementSettingsConfig moveConfig, PlayerMoveInput inputs, Camera camera)
    {
        _controller = controller;
        _moveConfig = moveConfig;
        _inputs = inputs;
        _camera = camera;

        _transform = _controller.transform;
        _cameraTransform = _camera.transform;
    }

    public void Perform()
    {
        Vector3 moveDirection = CalculateMoveDirection();
        moveDirection *= IsSprinting ? _moveConfig.SprintSpeed : _moveConfig.MoveSpeed;

        MakeCharacterLookForward(moveDirection);
        moveDirection.y = DefineGravity();

        _controller.Move(moveDirection * Time.fixedDeltaTime);
    }

    private Vector3 CalculateMoveDirection()
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection += _cameraTransform.right * _inputs.MoveDirectionXZ.x;
        moveDirection += _cameraTransform.forward * _inputs.MoveDirectionXZ.z;
        moveDirection.y = 0;
        moveDirection.Normalize();

        return moveDirection;
    }

    private void MakeCharacterLookForward(Vector3 moveDirection)
    {
        if (IsMoveing == false)
            return;

        Quaternion lookDirection = Quaternion.LookRotation(moveDirection);
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookDirection, _moveConfig.LookRotationSpeed);
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

    public void OnEnter() { }
    public void OnExit() { }
}