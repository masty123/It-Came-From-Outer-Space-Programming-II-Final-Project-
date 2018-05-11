using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour {

    [SerializeField]
    string mouseHoverSound = "ButtonHover";

    [SerializeField]
    string buttonPressSound = "ButtonPress";


    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in the scene.");
        }
    }


    public void Menu()
    {   

        audioManager.PlaySound(buttonPressSound);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

    public void Quit()
    {
        audioManager.PlaySound(buttonPressSound);
        Debug.Log("Closing Application");
        Application.Quit();
    } 

    public void Resume()
    {
        audioManager.PlaySound(buttonPressSound);
        Time.timeScale = 1f;
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound);
    }
}
