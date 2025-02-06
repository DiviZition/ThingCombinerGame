using Nenn.InspectorEnhancements.Runtime.Attributes;
using System;
using UnityEngine;

public class CombineComposition : MonoBehaviour
{
    [SerializeField] private CombinationObjectsSetup _defaultFirstState;
    [SerializeField] private CompositionStateSetup[] _combinationsOfComposition;
    [field: SerializeField] public bool Completed { get; private set; }

    [MethodButton]
    public void ReceiveObjectForCombination(CombinationTriggerTypes combinationObject)
    {
        for (int i = 0; i < _combinationsOfComposition.Length; i++)
        {
            if (_combinationsOfComposition[i].TriggerObject == combinationObject &&
                _combinationsOfComposition[i].IsCombined == false)
            {
                _combinationsOfComposition[i].ApplyCombination();

                if (CheckIsCompleted() == true)
                {
                    Completed = true;
                    Debug.Log("Completed");
                }
            }
        }
    }

    [MethodButton]
    public void RevertToFirstState()
    {
        Completed = false;

        _defaultFirstState.ApplyCombination();
        for (int i = 0; i < _combinationsOfComposition.Length; i++)
        {
            _combinationsOfComposition[i].SetNotCombined();
        }
    }

    private bool CheckIsCompleted()
    {
        for (int i = 0; i < _combinationsOfComposition.Length; i++)
        {
            if (_combinationsOfComposition[i].IsCombined == false)
                return false;
        }

        return true;
    }
}

public enum CombinationTriggerTypes
{
    None = -1,

    Cube,
    Sphere,
    Cilinder,
    Pillar,
}

[Serializable]
public class CompositionStateSetup : CombinationObjectsSetup
{
    [field: SerializeField] public CombinationTriggerTypes TriggerObject { get; private set; }
}

[Serializable]
public class CombinationObjectsSetup
{
    [SerializeField] private GameObject[] _objectsToEnable;
    [SerializeField] private GameObject[] _objectsToDisable;

    public bool IsCombined { get; private set; }

    public void ApplyCombination(bool isReverting = false)
    {
        foreach (var element in _objectsToEnable)
            element.SetActive(true);

        foreach (var element in _objectsToDisable)
            element.SetActive(false);

        IsCombined = true;
    }

    public void SetNotCombined() => IsCombined = false;
}