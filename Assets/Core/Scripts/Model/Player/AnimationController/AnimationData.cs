using System;
using UnityEngine;

[Serializable]
public class AnimationData
{
    [Header("The exact same name, with corresponding animator state")]
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public AnimationSwitcher.AnimationType AnimationType { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float TransitionDuration { get; private set; }

    public int Hash { get; private set; }

    public void Initialize()
    {
        if (AnimationName == null || AnimationName == ""/* || AnimationType == AnimationType.None*/)
            throw new Exception("Empty AnimationData detected");

        Hash = Animator.StringToHash(AnimationName);
    }
}