using UnityEngine;
using Zenject;

public class AbilityUser : MonoBehaviour
{
    private AbilitySystem _abilitySystem;

    [Inject]
    public void Construct(AbilitySystem abilitySystem)
    {
        _abilitySystem = abilitySystem; 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _abilitySystem.UseAbility(AbilityDataInstaller.AbilityType.MeleeAttack);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _abilitySystem.UseAbility(AbilityDataInstaller.AbilityType.ShieldBlock);
        }
    }
}
