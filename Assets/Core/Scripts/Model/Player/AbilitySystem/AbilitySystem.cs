using R3;
using System;
using System.Collections.Generic;
using Zenject;

public class AbilitySystem : IDisposable
{
    private CoroutineHolder _coroutineHolder;

    private AbilityData _abilityData;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private IAbility _currentAbility;
    private bool _isCurrentAbilityPerforming;

    public AbilitySystem(CoroutineHolder coroutines, AbilityData abilityData)
    {
        _coroutineHolder = coroutines;
        _abilityData = abilityData;
    }

    public void UseAbility(AbilityData.AbilityType abilityType)
    {
        if (_currentAbility != null && _isCurrentAbilityPerforming == true)
            return;

        IAbility ability = _abilityData.TryGetExistingAbility(abilityType);
        _currentAbility = ability;
        _isCurrentAbilityPerforming = true;

        ability.OnPerformEnd.Subscribe(_ => OnAbilityPerformed()).AddTo(_disposable);
        _coroutineHolder.StartCoroutine(ability.Perform());
    }

    private void OnAbilityPerformed()
    {
        _isCurrentAbilityPerforming = false;
        _currentAbility = null;
    }

    void IDisposable.Dispose() => _disposable?.Dispose();
}
