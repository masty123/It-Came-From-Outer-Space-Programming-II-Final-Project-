using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Show Player's current lives.
 * @author Theeruth Borisuth 
 */
[RequireComponent(typeof(Text))]
public class LiveCounterUI : MonoBehaviour {

    private Text livesText;

    /*
     * Define Text to show.
     */
	void Awake () {
        livesText = GetComponent<Text>();
	}


    /*
     * Show Player's current lives from GameMaster.
     */
    void Update () {
        livesText.text = "LIVES: " + GameMaster.RemainingLives;
	}
}
