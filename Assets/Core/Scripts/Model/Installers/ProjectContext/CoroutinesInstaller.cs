using UnityEngine;
using Zenject;

public class CoroutinesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        CoroutineHolder coroutineHolder = new GameObject("[COROUTINES]").AddComponent<CoroutineHolder>();
        Object.DontDestroyOnLoad(coroutineHolder);

        Container.Bind<CoroutineHolder>().FromInstance(coroutineHolder).AsSingle().NonLazy();
    }
}

