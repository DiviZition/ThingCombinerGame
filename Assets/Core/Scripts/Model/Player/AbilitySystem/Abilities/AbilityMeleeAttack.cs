using R3;
using System.Collections;
using UnityEngine;
using Zenject;

public class AbilityMeleeAttack : AbilityBase
{
    private AbilityMeleeAttackConfig _abilityConfig;

    public AbilityMeleeAttack(AbilityMeleeAttackConfig abilityConfig) : base(abilityConfig)
    {
        _abilityConfig = abilityConfig;
    }

    [Inject]
    public void Construct()
    {
    }

    protected override IEnumerator AbilityLogic()
    {
        yield return new WaitForSeconds(_abilityConfig.CoolDown);
        Debug.Log("Melee attack logick performed");
    }
}

public class AbilityShieldBlock : AbilityBase
{
    private AbilityShieldBlockConfig _abilityConfig;

    public AbilityShieldBlock(AbilityShieldBlockConfig abilityConfig) : base(abilityConfig)
    {
        _abilityConfig = abilityConfig;
    }

    [Inject]
    public void Construct()
    {
    }

    protected override IEnumerator AbilityLogic()
    {
        yield return new WaitForSeconds(_abilityConfig.CoolDown);
        Debug.Log("Shield block logick performed");
    }
}