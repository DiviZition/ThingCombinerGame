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

    private string _currentScene;
    private bool _isSceneLoadingInProcess;
    private const float ProgressMaxValue = 0.9f;

    public SceneTransitionHandler(CoroutineHolder coroutineHolder, UiComposition uiComposition, DiContainer container)
    {
        _coroutineHolder = coroutineHolder;

        _loadingProgress = uiComposition.LoadingScreen;
        _screenVail = uiComposition.ScreenVail;
        _screenVail.DeactivateVeil();
        _loadingProgress.UpdateProgressBar(0);

        _loadingComposition = container.InstantiatePrefabResource("LevelLoadingComposition");
        _loadingComposition.SetActive(false);
        _loadingComposition.transform.position = Vector3.down * 10;
        UnityEngine.Object.DontDestroyOnLoad(_loadingComposition);

        SceneManager.LoadScene(SceneNames.LoadingScene, LoadSceneMode.Single);
        SceneManager.LoadScene(SceneNames.MainMenu, LoadSceneMode.Additive);
        _currentScene = SceneNames.MainMenu;
    }

    public void SwitchScene(SceneTypes sceneType) => SwitchScene(SceneNames.GetSceneName(sceneType));

    public void SwitchScene(string sceneName)
    {
        if (_isSceneLoadingInProcess == true)
            return;

        _isSceneLoadingInProcess = true;
        _coroutineHolder.StartCoroutine(LoadNewScene(sceneName));
    }

    private IEnumerator LoadNewScene(string sceneName)
    {
        yield return WaitUntilVailActivated();
        yield return SceneManager.UnloadSceneAsync(_currentScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

        _screenVail.DeactivateVeil();
        _loadingComposition.gameObject.SetActive(true);

        AsyncOperation sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        _currentScene = sceneName;
        sceneLoadingOperation.allowSceneActivation = false;
        
        while (sceneLoadingOperation.progress < ProgressMaxValue)
        {
            Debug.Log(sceneLoadingOperation.progress);

            float progress = sceneLoadingOperation.progress * 100;
            _loadingProgress.UpdateProgressBar(progress);
            yield return null;
        }

        yield return WaitUntilVailActivated();

        sceneLoadingOperation.allowSceneActivation = true;
        _loadingComposition.gameObject.SetActive(false);
        _loadingProgress.UpdateProgressBar(0);
        _screenVail.DeactivateVeil();

        _isSceneLoadingInProcess = false;
    }

    private IEnumerator WaitUntilVailActivated()
    {
        bool isVailActivated = false;
        _screenVail.OnActivateVeilFinished.Subscribe(_ => { isVailActivated = true; }).AddTo(_disposable);

        _screenVail.ActivateVeil();
        yield return new WaitUntil(() => isVailActivated == true);
    }

    public void Dispose() => _disposable.Dispose();
}
