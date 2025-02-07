using R3;
using System;
using UnityEngine;
using Zenject;

public class PlayerAnimationController : ITickable, IDisposable
{
    private StatusData _statusData;
    private PlayerMovementStrategyHandler _movement;
    private AnimationSwitcher _animations;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public PlayerAnimationController(AnimationSwitcher animations, StatusData playerStatuses, PlayerMovementStrategyHandler movement)
    {
        _statusData = playerStatuses;
        _movement = movement;
    }

    public void Tick()
    {
        HandleMoveAnimations();
    }

    private void HandleMoveAnimations()
    {
        if(_movement.CurrentStrategy.IsMoveing == true)
        {
        }
    }

    public void Dispose() => _disposable.Dispose();
}
