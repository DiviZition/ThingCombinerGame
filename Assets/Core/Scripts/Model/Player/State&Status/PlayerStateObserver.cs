using UnityEngine;
using Zenject;

public class PlayerStateObserver : MonoBehaviour
{
    private PlayerStateData _stateData;

    [Inject]
    public void Construct(PlayerStateData stateData)
    {
        _stateData = stateData;
        Debug.Log(_stateData);
    }
}