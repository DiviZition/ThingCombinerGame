using Zenject;

public class PlayerInputsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        PlayerInputs inputs = new PlayerInputs();
        inputs.Enable();

        Container.Bind<PlayerInputs>().FromInstance(inputs).AsSingle();
    }
}
