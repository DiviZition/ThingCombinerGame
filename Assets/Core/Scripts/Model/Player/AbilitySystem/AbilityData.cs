using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbilityData : MonoInstaller
{
    [field: SerializeField] private AbilityConfigBase[] _abilityConfigs;
    private Dictionary<AbilityType, IAbility> _abilityMap;

    public override void InstallBindings()
    {
        if (_abilityConfigs == null || _abilityConfigs.Length <= 0)
        {
            Debug.LogError($"{this.name}'s AbilityData does not have any abilities loaded");
            return;
        }

        Container.Bind<AbilityData>().FromInstance(this).AsSingle().NonLazy();
        Container.Bind<AbilitySystem>().FromNew().AsSingle();

        _abilityMap = new Dictionary<AbilityType, IAbility>(_abilityConfigs.Length);

        for (int i = 0; i < _abilityConfigs.Length; i++)
        {
            _abilityMap.Add(_abilityConfigs[i].Type, CreateAndInjectAbility(Container, _abilityConfigs[i]));
        }
    }

    public IAbility TryGetExistingAbility(AbilityType abilityType)
    {
        return _abilityMap[abilityType];
    }

    private IAbility CreateAndInjectAbility(DiContainer container, AbilityConfigBase abilityConfig)
    {
        IAbility ability = null;

        if(abilityConfig is AbilityMeleeAttackConfig meleeConfig)
        {
            ability = new AbilityMeleeAttack(meleeConfig);
        }
        else if (abilityConfig is AbilityShieldBlockConfig shieldBlockConfig)
        {
            ability = new AbilityShieldBlock(shieldBlockConfig);
        }

        if (ability != null)
            container.QueueForInject(ability);

        return ability;
    }

    public enum AbilityType
    {
        MeleeAttack,
        ShieldBlock,
    }
}
