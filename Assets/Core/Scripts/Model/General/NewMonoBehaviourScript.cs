using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) == true)
        {
            Debug.Log(SceneManager.GetActiveScene().name);


            SceneManager.LoadSceneAsync(SceneNames.LoadingScene);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }
}
