using UnityEngine;
using Zenject;
using GeneralSolutions;

public class PlayerComponentsInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovementStrategyHandler _playerMovement;

    private void OnValidate()
    {
        if (_playerMovement == null)
            _playerMovement = GeneralMethods.TryGetComponentValidating<PlayerMovementStrategyHandler>(this);
    }

    public override void InstallBindings()
    {
        Container.Bind<PlayerMovementStrategyHandler>().FromInstance(_playerMovement).AsSingle();
        Container.Bind<PlayerInputs>().FromInstance(CreatePlayerInputs()).AsSingle();
        Container.Bind<PlayerStateData>().FromNew().AsSingle();
        Container.Bind<StatusData>().FromNew().AsSingle().NonLazy();
    }

    private PlayerInputs CreatePlayerInputs()
    {
        PlayerInputs inputs = new PlayerInputs();
        inputs.Enable();

        return inputs;
    }
}
