using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Script for ScorePrefab that show score and name of player in database.
 */
public class ScoreScript : MonoBehaviour
{

	public GameObject score;
	public GameObject playerName;

    /*
     * Set name and score to this.
     */
	public void setScore(string name, string score) {
		this.playerName.GetComponent<Text> ().text = name;
		this.score.GetComponent<Text> ().text = score;
	}
}

