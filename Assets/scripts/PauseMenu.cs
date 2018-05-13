using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Controller for pauseUI that handle button behavior in pauseUI.
 * @author Theeruth Borisuth 
 */
public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    [SerializeField]
    string mouseHoverSound = "ButtonHover";

    [SerializeField]
    string buttonPressSound = "ButtonPress";

    public GameObject pauseUI;

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in the scene.");
        }
    }


    //Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /*
     * Activate any activity in game.
     */
    public void Resume()
    {
        audioManager.PlaySound(buttonPressSound);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /*
     * Pause any activity in game and activate pauseUI.
     */
    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /*
     * Load MainMenu scene.
     */
    public void Menu()
    {
        audioManager.PlaySound(buttonPressSound);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");

    }

    /*
     *Close the application
     */
    public void Quit()
    {
        audioManager.PlaySound(buttonPressSound);
        Debug.Log("Closing Application");
        Application.Quit();
    }


    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound);
    }
}
