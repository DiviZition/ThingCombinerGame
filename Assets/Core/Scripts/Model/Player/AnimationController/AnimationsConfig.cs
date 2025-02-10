using UnityEngine;

[CreateAssetMenu(fileName = "AnimationsConfig", menuName = "ScriptableObjects/Configs/AnimationSetup")]
public class AnimationsConfig : ScriptableObject
{
    [field: SerializeField] public AnimationData[] Animations { get; private set; }
    [field: SerializeField] public AnimationOverride[] Overrides { get; private set; }
}
