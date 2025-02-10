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

    private AnimationSwitcher.AnimationType _currentAnimation;

    public PlayerAnimationController(AnimationSwitcher animations, StatusData playerStatuses, PlayerMovementStrategyHandler movement)
    {
        _statusData = playerStatuses;
        _movement = movement;
        _animations = animations;
    }

    public void Tick()
    {
        HandleMoveAnimations();
    }

    private void HandleMoveAnimations()
    {
        AnimationSwitcher.AnimationType newAnim;

        if(_movement.CurrentStrategy.IsMoveing == true)
        {
            if (_movement.CurrentStrategy.IsSprinting == true)
                newAnim = AnimationSwitcher.AnimationType.MoveFast;
            else
                newAnim = AnimationSwitcher.AnimationType.MoveDefault;
        }
        else
        {
            newAnim = AnimationSwitcher.AnimationType.Idle;
        }

        if(newAnim != _currentAnimation)
        {
            _animations.StartAnimation(newAnim);
            _currentAnimation = newAnim;
        }
    }

    public void Dispose() => _disposable.Dispose();

}
