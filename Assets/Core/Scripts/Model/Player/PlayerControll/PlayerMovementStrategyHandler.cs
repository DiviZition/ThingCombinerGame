using R3;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementStrategyHandler : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private MovementSettingsConfig _moveSettingsConfig;

    private Dictionary<PlayerStateData.StateType, IControllable> _strategies = new(2);
    private CompositeDisposable _disposable = new CompositeDisposable();

    private IControllable _currentControllStrategy;

    [Inject]
    public void Construct(PlayerStateData stateData, PlayerInputs inputs, Camera camera)
    {
        _strategies.Add(PlayerStateData.StateType.Default, 
            new PlayerRunMoveStrategy(_characterController, _moveSettingsConfig, inputs, camera));

        SwitchStrategy(PlayerStateData.StateType.Default);

        stateData.OnStateChange.Subscribe(stateType => SwitchStrategy(stateType)).AddTo(_disposable);
    }

    private void FixedUpdate()
    {
        _currentControllStrategy.Perform();
    }

    private void SwitchStrategy(PlayerStateData.StateType correspondingState)
    {
        if ( _currentControllStrategy != _strategies[correspondingState])
        {
            if (_currentControllStrategy != null)
                _currentControllStrategy.OnExit();

            _currentControllStrategy = _strategies[correspondingState];
            _currentControllStrategy.OnEnter();
        }
    }
}
