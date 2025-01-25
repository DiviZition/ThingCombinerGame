using UnityEngine;
using Zenject;

public class GameStart : MonoBehaviour
{
    [Inject]
    public void Construct(SceneTransitionHandler sceneTransition)
    {
        sceneTransition.SwitchScene(SceneNames.MainMenu);
    }
}
