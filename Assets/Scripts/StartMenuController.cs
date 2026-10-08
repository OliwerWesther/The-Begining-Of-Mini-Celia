using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField]GameObject StartButtonObject;
    [SerializeField]GameObject ExitButtonObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            anim.SetBool("StartButton", false);
            StartButtonObject.SetActive(true);
            ExitButtonObject.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void OnStartClick()
    {
        anim.SetBool("StartButton", true);
        StartButtonObject.SetActive(false);
        ExitButtonObject.SetActive(false);
        //SceneManager.LoadScene("Outside");

        Time.timeScale = 1f;
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
