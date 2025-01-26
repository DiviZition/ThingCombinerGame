using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneTransitionHandler
{
    private CoroutineHolder _coroutineHolder;
    private CompositeDisposable _disposable = new CompositeDisposable();

    private GameObject _loadingComposition;
    private UiLoadingScreen _loadingProgress;
    private UiScreenVail _screenVail;

    public SceneTransitionHandler(CoroutineHolder coroutineHolder, UiComposition uiComposition, DiContainer container)
    {
        _coroutineHolder = coroutineHolder;

        _loadingProgress = uiComposition.LoadingScreen;
        _screenVail = uiComposition.ScreenVail;

        _loadingComposition = container.InstantiatePrefabResource("LevelLoadingComposition");
        _loadingComposition.SetActive(false);
        UnityEngine.Object.DontDestroyOnLoad(_loadingComposition);
    }

    public void SwitchScene(SceneTypes sceneType) => SwitchScene(SceneNames.GetSceneName(sceneType));

    public void SwitchScene(string sceneName) => _coroutineHolder.StartCoroutine(LoadNewScene(sceneName));

    private IEnumerator LoadNewScene(string sceneName)
    {
        yield return ActivateVeil();
        _screenVail.DeactivateVeil();
        _loadingComposition.gameObject.SetActive(true);

        AsyncOperation sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (sceneLoadingOperation.isDone == false)
        {
            float progress = sceneLoadingOperation.progress / 0.9f;
            _loadingProgress.UpdateProgressBar(progress);
            //_text.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }

        yield return new WaitForSeconds(2);
        yield return ActivateVeil();

        _loadingComposition.gameObject.SetActive(false);
        _screenVail.DeactivateVeil();

        yield return null;
    }

    private IEnumerator ActivateVeil()
    {
        bool isVailActivated = false;
        _screenVail.OnActivateVeilFinished.Subscribe(_ => { isVailActivated = true; }).AddTo(_disposable);

        _screenVail.ActivateVeil();
        yield return new WaitUntil(() => isVailActivated == true);
    }

    public void Dispose() => _disposable.Dispose();
}
