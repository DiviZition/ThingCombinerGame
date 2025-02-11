using UnityEngine;

public abstract class AbilityConfigBase : ScriptableObject, IAbilityDescription
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    public abstract AbilityData.AbilityType Type { get; }
}
