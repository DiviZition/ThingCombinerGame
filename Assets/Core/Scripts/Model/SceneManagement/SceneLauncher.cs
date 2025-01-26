using UnityEngine;
using Zenject;

public class SceneLauncher : MonoBehaviour
{
    [SerializeField] private SceneTypes _targetScene;
    
    private SceneTransitionHandler _transitionHandler;

    [Inject]
    public void Construct(SceneTransitionHandler sceneTransitionHandler)
    {
        _transitionHandler = sceneTransitionHandler;
    }

    public void GoToTargetScene()
    {
        _transitionHandler.SwitchScene(_targetScene);
    }
}
