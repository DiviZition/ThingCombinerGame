using Nenn.InspectorEnhancements.Runtime.Attributes;
using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UiScreenVail : MonoBehaviour
{
    [SerializeField] private Image _veil;
    [SerializeField] private float _veilStateChangeSpeed;

    public readonly Subject<Unit> OnActivateVeilFinished = new Subject<Unit>();
    public readonly Subject<Unit> OnDeactivateVeilFinished = new Subject<Unit>();

    private void Start()
    {
        DeactivateVeil();
    }

    [MethodButton]
    public void ActivateVeil() => StartCoroutine(ShowTheVeilProcess());

    [MethodButton]
    public void DeactivateVeil() => StartCoroutine(RemoveTheVeilProcess());

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

        OnActivateVeilFinished.OnNext(Unit.Default);
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

        OnDeactivateVeilFinished.OnNext(Unit.Default);
    }
}
