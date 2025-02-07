using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationData[] _animations;
    [SerializeField] private AnimationOverride[] _animationOverride;

    private void Start()
    {
        InitializeAnimationHashes();
    }

    private void InitializeAnimationHashes()
    {
        for (int i = 0; i < _animations.Length; i++)
            _animations[i].Initialize();

        for (int i = 0; i < _animationOverride.Length; i++)
            for (int b = 0; b < _animationOverride[i].Animations.Length; b++)
                _animationOverride[i].Animations[b].Initialize();
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
        for (int i = 0; i < _animationOverride.Length; i++)
        {
            if (_animationOverride[i].OverrideKey == overrideKey)
                newAnimations = _animationOverride[i];
        }

        if (newAnimations == null)
            return;

        for (int i = 0; i < newAnimations.Animations.Length; i++)
        {
            AnimationData newAnimData = newAnimations.Animations[i];
            _animations[GetAnimationIndex(newAnimData.AnimationType)] = newAnimData;
        }
    }

    private int GetAnimationIndex(AnimationType animationType)
    {
        for (int i = 0; i < _animations.Length; i++)
        {
            if (_animations[i].AnimationType == animationType)
                return i;
        }

        return -1;
    }

    public AnimationData GetAnimationData(AnimationType animationType)
    {
        int index = GetAnimationIndex(animationType);

        return index < 0 ? null : _animations[index];
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
