using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * This used to call and handle behavior of GameOver secen.
 * @author Theeruth Borisuth  
 */
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

    /*
     * Close appliction. 
     */
    public void Quit ()
    {
        audioManager.PlaySound(buttonPressSound);
        Debug.Log("Closing Application");
        Application.Quit();
    }

    /*
     * Load current scence again depend on selected mode like user play on MainLevel it will call MainLevel again.
     */
    public void Retry()
    {
        audioManager.PlaySound(buttonPressSound);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    /*
     * Play sound when mouse over button.
     */ 
    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound);
    }



}
