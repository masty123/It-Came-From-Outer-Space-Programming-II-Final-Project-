using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Controller of HowToPlay scene.
 * @author Theeruth Borisuth 
 */
public class HowToPlay : MonoBehaviour
{

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    AudioManager audioManager;

    /*
     * Call AudioManager when this class started.
     */
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found!");
        }
    }

    /*
     * Back to MainMenu scene.
     */
    public void Back()
    {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene("MainMenu");
    }

    /*
     * Play sound when mouse over the button.
     */
    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }
}
