using R3;
using System;
using System.Collections;
using UnityEngine;

public abstract class AbilityBase : IAbility
{
    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public AbilityData.AbilityType Type { get; private set; }
    public Subject<Unit> OnPerformEnd { get; } = new Subject<Unit>();

    protected AbilityBase(IAbilityDescription description)
    {
        Name = description.Name;
        Icon = description.Icon;
        Type = description.Type;
    }

    public IEnumerator Perform()
    {
        yield return AbilityLogic();
        OnPerformEnd?.OnNext(Unit.Default);
    }

    protected abstract IEnumerator AbilityLogic();

    public void Dispose() => OnPerformEnd.Dispose();
}
