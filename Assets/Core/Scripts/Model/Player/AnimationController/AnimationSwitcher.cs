using UnityEngine;

public class AnimationSwitcher
{
    private Animator _animator;
    private AnimationsConfig _config;

    public AnimationSwitcher(Animator animator, AnimationsConfig config)
    {
        _animator = animator;
        _config = config;

        InitializeAnimationHashes();
    }

    private void InitializeAnimationHashes()
    {
        for (int i = 0; i < _config.Animations.Length; i++)
            _config.Animations[i].Initialize();

        for (int i = 0; i < _config.Overrides.Length; i++)
            for (int b = 0; b < _config.Overrides[i].Animations.Length; b++)
                _config.Overrides[i].Animations[b].Initialize();
    }

    public void StartAnimation(AnimationType animationType)
    {
        AnimationData animationData;

        animationData = GetAnimationData(animationType);

        _animator.CrossFade(animationData.Hash, animationData.TransitionDuration);
    }

    public void OverrideAnimations(AnimationOverride.KeyType overrideKey)
    {
        AnimationOverride newAnimations = null;
        for (int i = 0; i < _config.Overrides.Length; i++)
        {
            if (_config.Overrides[i].OverrideKey == overrideKey)
                newAnimations = _config.Overrides[i];
        }

        if (newAnimations == null)
            return;

        for (int i = 0; i < newAnimations.Animations.Length; i++)
        {
            AnimationData newAnimData = newAnimations.Animations[i];
            _config.Animations[GetAnimationIndex(newAnimData.AnimationType)] = newAnimData;
        }
    }

    private int GetAnimationIndex(AnimationType animationType)
    {
        for (int i = 0; i < _config.Animations.Length; i++)
        {
            if (_config.Animations[i].AnimationType == animationType)
                return i;
        }

        return -1;
    }

    public AnimationData GetAnimationData(AnimationType animationType)
    {
        int index = GetAnimationIndex(animationType);

        return index < 0 ? null : _config.Animations[index];
    }

    public enum AnimationType : sbyte
    {
        None = -1,

        Idle,
        MoveSlow,
        MoveDefault,
        MoveFast,

        Attack,

        BoostSpell,
        ShieldBlock,

        Starving,
        Eating,
    }
}
