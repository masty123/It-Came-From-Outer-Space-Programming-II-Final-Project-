using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOverUI : MonoBehaviour {

    [SerializeField]
    string mouseHoverSound = "ButtonHover";

    [SerializeField]
    string buttonPressSound = "ButtonPress";


    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No AudioManager found in the scene.");
        }
    }

    public void Quit ()
    {
        audioManager.PlaySound(buttonPressSound);
        Debug.Log("Closing Application");
        Application.Quit();
    }

    public void Retry()
    {
        audioManager.PlaySound(buttonPressSound);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound);
    }



}
