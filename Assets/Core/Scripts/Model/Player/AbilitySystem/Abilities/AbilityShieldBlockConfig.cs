using UnityEngine;

[CreateAssetMenu(fileName = "ShieldBlockConfig", menuName = "ScriptableObjects/Ability/ShieldBlock")]
public class AbilityShieldBlockConfig : AbilityConfigBase
{
    [field: SerializeField] public float CoolDown { get; private set; }
    [field: SerializeField] public int AttackRange { get; private set; }

    public override AbilityData.AbilityType Type => AbilityData.AbilityType.ShieldBlock;
}