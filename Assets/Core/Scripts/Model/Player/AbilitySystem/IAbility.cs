using R3;
using System;
using System.Collections;
using UnityEngine;

public interface IAbility : IDisposable
{
    public string Name { get; }
    public AbilityDataInstaller.AbilityType Type { get; }
    public Sprite Icon { get; }
    public Subject<Unit> OnPerformEnd { get; }
    public IEnumerator Perform();
}
