using R3;
using System;
using System.Collections;

public interface IAbility : IAbilityDescription, IDisposable
{
    public Subject<Unit> OnPerformEnd { get; }
    public IEnumerator Perform();
}
