using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttackConfig", menuName = "ScriptableObjects/Ability/MeleeAttack")]
public class AbilityMeleeAttackConfig : AbilityConfigBase
{
    [field: SerializeField] public float CoolDown { get; private set; }
    [field: SerializeField] public int AttackRange { get; private set; }

    public override AbilityData.AbilityType Type => AbilityData.AbilityType.MeleeAttack;
}
