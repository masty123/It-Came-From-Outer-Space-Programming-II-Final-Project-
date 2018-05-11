using UnityEngine;
using UnityEngine.SceneManagement;

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


    public void StartGame () {
        audioManager.PlaySound(pressButtonSound);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	public void StartGame2 () {
		audioManager.PlaySound(pressButtonSound);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
	}

    public void HowToPlay()
    {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }


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
