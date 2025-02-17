﻿using System.Collections;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MeleeAttackConfig", menuName = "ScriptableObjects/Ability/MeleeAttack")]
public class AbilityMeleeAttack : ScriptableAbilityBase
{
    [field: SerializeField] public float CoolDown { get; private set; }
    [field: SerializeField] public int AttackRange { get; private set; }

    public override AbilityDataInstaller.AbilityType Type => AbilityDataInstaller.AbilityType.MeleeAttack;

    [Inject]
    public void Construct()
    {

    }

    protected override IEnumerator AbilityLogic()
    {
        yield return new WaitForSeconds(CoolDown);
        Debug.Log("Melee attack logick performed");
    }
}
