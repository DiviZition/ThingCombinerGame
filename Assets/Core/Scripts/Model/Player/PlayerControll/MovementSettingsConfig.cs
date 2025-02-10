using UnityEngine;

[CreateAssetMenu(fileName = "MoveSettingsConfig", menuName = "ScriptableObjects/Configs/MoveSettings")]
public class MovementSettingsConfig : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float LookRotationSpeed { get; private set; }
    [field: SerializeField] public float SprintSpeed {  get; private set; }

    [field: SerializeField] public float MaxFallSpeed { get; private set; }
    [field: SerializeField] public float GroundedGravity { get; private set; }
    [field: SerializeField] public float FreeFallGravityAcceleration { get; private set; }
}