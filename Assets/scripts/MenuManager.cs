using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Handle all import behavior for MainMenu scene
 */
public class MenuManager : MonoBehaviour {

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found!");
        }
    }

    /*
     * Load MainLevel scence.
     */
    public void StartGame () {
        audioManager.PlaySound(pressButtonSound);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
     * Load 2Player scence.
     */
    public void StartGame2 () {
		audioManager.PlaySound(pressButtonSound);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
	}

    /*
     * Load HowToPlay scence.
     */
    public void HowToPlay()
    {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    /*
     * Close this application.
     */
    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Debug.Log("Closing Application");
        Application.Quit();
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }
}
