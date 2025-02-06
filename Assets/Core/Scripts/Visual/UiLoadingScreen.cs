using UnityEngine;
using UnityEngine.UI;

public class UiLoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _progressBar;

    private void Start()
    {
        UpdateProgressBar(0);
    }

    public void UpdateProgressBar(float value) => _progressBar.fillAmount = value;
}
