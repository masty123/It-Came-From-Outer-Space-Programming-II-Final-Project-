using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Show Player's current score.
 */
public class ScoreCounter : MonoBehaviour {

    private Text MoneyText;

    /*
    * Define Text to show.
    */
    void Awake()
    {
        MoneyText = GetComponent<Text>();
    }

    /*
    * Show Player's current score from GameMaster.
    */
    void Update()
    {
        MoneyText.text = "SCORE: " + GameMaster.Score;
    }
}
