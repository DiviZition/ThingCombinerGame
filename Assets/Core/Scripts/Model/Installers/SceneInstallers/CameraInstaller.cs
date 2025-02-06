using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private Camera _playersCamera;

    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(_playersCamera).AsSingle();
    }
}
