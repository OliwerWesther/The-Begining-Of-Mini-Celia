using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnterScene();
    }

    public void EnterScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + -1);
    }
}
