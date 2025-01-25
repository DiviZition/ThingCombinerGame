using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneTransitionHandler
{
    private CoroutineHolder _coroutineHolder;
    private LoadingScreenComposition _loadingComposition;

    public SceneTransitionHandler(CoroutineHolder coroutineHolder, DiContainer container)
    {
        _coroutineHolder = coroutineHolder;

        _loadingComposition = container.InstantiatePrefabResourceForComponent<LoadingScreenComposition>("LoadingSceneComposition");
        _loadingComposition.GameObject.SetActive(false);
        Object.DontDestroyOnLoad(_loadingComposition.GameObject);
    }

    public void SwitchScene(SceneTypes sceneType) => SwitchScene(SceneNames.GetSceneName(sceneType));

    public void SwitchScene(string sceneName)
    {
        _coroutineHolder.StartCoroutine(LoadNewScene(sceneName));
    }

    private IEnumerator LoadNewScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(SceneNames.LoadingScene, LoadSceneMode.Single);
        _loadingComposition.GameObject.SetActive(true);

        AsyncOperation sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (sceneLoadingOperation.isDone == false)
        {
            float progress = sceneLoadingOperation.progress / 0.9f;
            _loadingComposition.UpdateProgressBar(progress);
            //_text.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }

        yield return new WaitForSeconds(1);
        _loadingComposition.GameObject.SetActive(false);

        yield return null;
    }
}
