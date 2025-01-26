using UnityEngine;

public class UiComposition : MonoBehaviour
{
    [field: SerializeField] public UiScreenVail ScreenVail { get; private set; }
    [field: SerializeField] public UiLoadingScreen LoadingScreen { get; private set; }
}
