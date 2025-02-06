using UnityEngine;
using Zenject;

public class StartSceneLauncher : MonoBehaviour
{
    [SerializeField] private SceneTypes _targetScene;

    [Inject]
    public void Construct(SceneTransitionHandler sceneTransitions)
    {
        sceneTransitions.SwitchScene(_targetScene);
    }
}
