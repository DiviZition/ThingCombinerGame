using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private CinemachineBrain _cameraBrainPrefab;

    public override void InstallBindings()
    {
        CinemachineBrain camera = Container.InstantiatePrefab(_cameraBrainPrefab).GetComponent<CinemachineBrain>();
        Object.DontDestroyOnLoad(camera.gameObject);

        Container.Bind<CinemachineBrain>().FromInstance(camera);
    }
}