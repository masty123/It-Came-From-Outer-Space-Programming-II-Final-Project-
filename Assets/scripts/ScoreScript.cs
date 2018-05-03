using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

	public GameObject score;
	public GameObject playerName;


	public void setScore(string name, string score) {
		this.playerName.GetComponent<Text> ().text = name;
		this.score.GetComponent<Text> ().text = score;
	}
}

