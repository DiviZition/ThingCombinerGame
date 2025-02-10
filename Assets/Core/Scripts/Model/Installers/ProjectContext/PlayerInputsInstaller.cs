using Zenject;

public class PlayerInputsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        DefaultInputs inputs = new DefaultInputs();
        inputs.Enable();

        Container.Bind<DefaultInputs>().FromInstance(inputs).AsSingle();
    }
}
