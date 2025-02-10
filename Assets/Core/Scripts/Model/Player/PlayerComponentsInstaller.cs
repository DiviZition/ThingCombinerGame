using UnityEngine;
using Zenject;
using GeneralSolutions;

public class PlayerComponentsInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovementStrategyHandler _playerMovement;
    [SerializeField] private Animator _animator;

    [SerializeField] private MovementSettingsConfig _moveConfig;
    [SerializeField] private AnimationsConfig _animatiosConfig;

    private void OnValidate()
    {
        if (_playerMovement == null)
            _playerMovement = GeneralMethods.TryGetComponentValidating<PlayerMovementStrategyHandler>(this);
    }

    public override void InstallBindings()
    {
        Container.Bind<PlayerMovementStrategyHandler>().FromInstance(_playerMovement).AsSingle();
        Container.Bind<Animator>().FromInstance(_animator).AsSingle();

        Container.Bind<MovementSettingsConfig>().FromInstance(_moveConfig).AsSingle();
        Container.Bind<AnimationsConfig>().FromInstance(_animatiosConfig).AsSingle();

        BindInputs(Container);

        Container.Bind<StatusData>().FromNew().AsSingle().NonLazy();
        Container.Bind<AnimationSwitcher>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerAnimationController>().FromNew().AsSingle().NonLazy();
    }

    private void BindInputs(DiContainer container)
    {
        DefaultInputs inputs = new DefaultInputs();
        inputs.Enable();

        Container.Bind<DefaultInputs>().FromInstance(inputs).AsSingle();
        //Need to define what input should we use
        container.BindInterfacesAndSelfTo<PlayerMoveInput>().FromNew().AsSingle().NonLazy();
    }
}
