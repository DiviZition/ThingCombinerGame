using System.Collections;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ShieldBlockConfig", menuName = "ScriptableObjects/Ability/ShieldBlock")]
public class AbilityShieldBlock : ScriptableAbilityBase
{
    [field: SerializeField] public float CoolDown { get; private set; }
    [field: SerializeField] public int AttackRange { get; private set; }

    public override AbilityDataInstaller.AbilityType Type => AbilityDataInstaller.AbilityType.ShieldBlock;

    [Inject]
    public void Construct()
    {

    }

    protected override IEnumerator AbilityLogic()
    {
        yield return new WaitForSeconds(CoolDown);
        Debug.Log("Shield block logick performed");
    }
}