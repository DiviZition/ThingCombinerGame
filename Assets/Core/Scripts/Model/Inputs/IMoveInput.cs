using UnityEngine;

public interface IMoveInput
{
    public Vector3 MoveDirectionXZ { get; }
    public sbyte MoveDirectionY { get; }
    public bool IsSprinting { get; }
}
