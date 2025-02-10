using System;
using UnityEngine;


public class MeleeAttackSkill : ScriptableObject, IDisposable
{
    public Action OnEnd;

    public void Dispose()
    {
        throw new NotImplementedException();
    }

}
