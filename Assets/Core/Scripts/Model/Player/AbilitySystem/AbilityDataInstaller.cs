using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbilityDataInstaller : MonoInstaller
{
    [field: SerializeField] private ScriptableAbilityBase[] _abilityConfigs;

    private Dictionary<AbilityType, IAbility> _abilityMap;

    public override void InstallBindings()
    {
        if (_abilityConfigs == null || _abilityConfigs.Length <= 0)
        {
            Debug.LogError($"{this.name}'s AbilityData does not have any abilities loaded");
            return;
        }

        Container.Bind<AbilityDataInstaller>().FromInstance(this).AsSingle().NonLazy();
        Container.Bind<AbilitySystem>().FromNew().AsSingle();

        _abilityMap = new Dictionary<AbilityType, IAbility>(_abilityConfigs.Length);
        for (int i = 0; i < _abilityConfigs.Length; i++)
        {
            _abilityMap.Add(_abilityConfigs[i].Type, _abilityConfigs[i]);
            Container.QueueForInject(_abilityConfigs[i]);
        }
    }

    public IAbility TryGetExistingAbility(AbilityType abilityType)
    {
        return _abilityMap[abilityType];
    }

    public enum AbilityType
    {
        MeleeAttack,
        ShieldBlock,
    }
}
