using UnityEngine;
using Zenject;

public class UiCompositionInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        UiComposition ui = Container.InstantiatePrefabResourceForComponent<UiComposition>("UIHUD");
        Object.DontDestroyOnLoad(ui.gameObject);
        Container.Bind<UiComposition>().FromInstance(ui).AsSingle().NonLazy();
    }
}