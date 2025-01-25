using Nenn.InspectorEnhancements.Runtime.Attributes;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingScreenComposition : MonoBehaviour
{
    [field: SerializeField] public Camera LoadingScreenCamera { get; private set; }

    [SerializeField] private Image _veil;
    [SerializeField] private float _veilStateChangeSpeed;

    [SerializeField] private Image _progressBar;

    public GameObject GameObject { get; private set; }

    private CoroutineHolder _coroutineHolder;

    [Inject]
    public void Construct(CoroutineHolder coroutineHolder)
    {
        _coroutineHolder = coroutineHolder;
        GameObject = this.gameObject;
    }

    public void UpdateProgressBar(float value) => _progressBar.fillAmount = value;

    [MethodButton]
    public void ShowTheVeil() => _coroutineHolder.StartCoroutine(ShowTheVeilProcess());

    [MethodButton]
    public void RemoveTheVeil() => _coroutineHolder.StartCoroutine(RemoveTheVeilProcess());


    private IEnumerator ShowTheVeilProcess()
    {
        Color veilColor = _veil.color;
        float startTime = Time.time;

        while (veilColor.a < 1)
        {
            veilColor.a = (Time.time - startTime) / (startTime + _veilStateChangeSpeed - startTime);
            _veil.color = veilColor;

            yield return null;
        }
    }

    private IEnumerator RemoveTheVeilProcess()
    {
        Color veilColor = _veil.color;
        float startTime = Time.time;

        while (veilColor.a > 0)
        {
            veilColor.a = 1 - (Time.time - startTime) / (startTime + _veilStateChangeSpeed - startTime);
            _veil.color = veilColor;

            yield return null;
        }
    }
}
