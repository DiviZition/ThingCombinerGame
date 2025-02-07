using System;
using UnityEngine;

[Serializable]
public class AnimationOverride
{
    [field: SerializeField] public KeyType OverrideKey {  get; private set; }
    [field: SerializeField] public AnimationData[] Animations { get; private set; }

    public enum KeyType
    {
        MoveSetDefault,
        MoveSetSwim,

        AttackMelee,
        AttackBow,
        AttackSpell,
        AttackThrow,
    }
}
