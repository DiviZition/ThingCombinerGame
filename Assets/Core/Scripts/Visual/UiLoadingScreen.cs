using UnityEngine;
using UnityEngine.UI;

public class UiLoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _progressBar;

    public void UpdateProgressBar(float value) => _progressBar.fillAmount = value;
}
