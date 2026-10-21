using UnityEngine;

public class MenuController : MonoBehaviour
{
    

    [Header("Used references")]
    [Tooltip("The button to start.")]
    public GameObject startButton;
    [Tooltip("The button to create a new world.")]
    public GameObject createWorldButton;
    [Tooltip("The button to close the application")]
    public GameObject exitButton;
    [Tooltip("The button to load the JSON save file.")]
    public GameObject loadButton;
    [Tooltip("The button to go back from the starting menu to first menu.")]
    public GameObject backButton;
    [Tooltip("The object that holds all button to turn on or off.")]
    public GameObject MenuCanvas;
    [Tooltip("The button to save the game.")]
    public GameObject saveButton;

    [Header("Menu")]
    [Tooltip("1 if true, -1 if false.")]
    public float onOff = 1;


    private void Start()
    {
        startButton.SetActive(true);
        exitButton.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onOff == 1)
            {
                MenuCanvas.SetActive(true);
                onOff *= -1;

                //true
                exitButton.SetActive(true);
                saveButton.SetActive(true);
                loadButton.SetActive(true);

                //false
                backButton.SetActive(false);
                createWorldButton.SetActive(false);


            }
            else if (onOff == -1)
            {
                MenuCanvas.SetActive(false);
                onOff *= -1;
            }
            
        }
    }

    public void OnStartClick()
    {
        //true
        backButton.SetActive(true);
        createWorldButton.SetActive(true);
        loadButton.SetActive(true);

        //false
        startButton.SetActive(false);
        exitButton.SetActive(false);
    }

    public void OnBackClick()
    {
        //true
        startButton.SetActive(true);
        exitButton.SetActive(true);

        //false
        backButton.SetActive(false);
        createWorldButton.SetActive(false);
        loadButton.SetActive(false);
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
