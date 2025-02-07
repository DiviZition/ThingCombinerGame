using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementStrategyHandler : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private MovementSettingsConfig _moveSettingsConfig;

    private IControllable _defaultMove;

    private bool _isFreezed;

    public IControllable CurrentStrategy { get; private set; }

    [Inject]
    public void Construct(PlayerInputs inputs, Camera camera)
    {
        _defaultMove = new PlayerRunMoveStrategy(_characterController, _moveSettingsConfig, inputs, camera);

        SwitchStrategy(_defaultMove);
    }

    private void FixedUpdate()
    {
        if (_isFreezed == false)
            CurrentStrategy.Perform();
    }

    public void Freeze() => _isFreezed = true;
    public void UnFreeze() => _isFreezed = false;

    private void SwitchStrategy(IControllable newState)
    {
        if (CurrentStrategy != newState)
        {
            if (CurrentStrategy != null)
                CurrentStrategy.OnExit();

            CurrentStrategy = newState;
            CurrentStrategy.OnEnter();
        }
    }
}
