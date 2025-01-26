using UnityEngine;
using Zenject;

public class SceneTransitionHolderInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SceneTransitionHandler>().FromNew().AsSingle().NonLazy();
    }
}