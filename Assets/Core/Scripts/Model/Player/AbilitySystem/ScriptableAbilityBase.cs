using R3;
using UnityEngine;
using System.Collections;

public abstract class ScriptableAbilityBase : ScriptableObject, IAbility
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    public abstract AbilityDataInstaller.AbilityType Type { get; }

    public Subject<Unit> OnPerformEnd { get; } = new Subject<Unit>();

    public IEnumerator Perform()
    {
        yield return AbilityLogic();
        OnPerformEnd?.OnNext(Unit.Default);
    }

    protected abstract IEnumerator AbilityLogic();

    public void Dispose() => OnPerformEnd.Dispose();
}
