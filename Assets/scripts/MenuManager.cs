﻿using UnityEngine;
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
        audioManager.StopSound("Music");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }

	public void StartGame2 () {
		audioManager.PlaySound(pressButtonSound);
        audioManager.StopSound("Music");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
       
    }


    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Debug.Log("Closing Application");
        audioManager.StopSound("Music");
        Application.Quit();
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }
}
