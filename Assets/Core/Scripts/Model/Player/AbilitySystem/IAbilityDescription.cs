using System;
using UnityEngine;

public interface IAbilityDescription
{
    public string Name { get; }
    public AbilityData.AbilityType Type { get; }
    public Sprite Icon { get; }
}
