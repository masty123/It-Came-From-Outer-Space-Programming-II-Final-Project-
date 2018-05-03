using UnityEngine;
using System.Collections;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
	private string connectionString;

	private List<HighScore> highscore = new List<HighScore>();

	public GameObject scorePrefab;

	public Transform scoreParent;

    public Text nameFromInput;

	// Use this for initialization
	void Start ()
	{

		connectionString = "URI=file:" + Application.dataPath + "/HighScoreDB.db";
		ShowScore ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    public void enterName() {
        if(nameFromInput.text != string.Empty)
        {
            insertScore(nameFromInput.text, GameMaster.Score);
            nameFromInput.text = string.Empty;
            ShowScore();
        }
    }

	private void  insertScore(string name, int newScore) {
		using(IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			dbConnection.Open();
			
			using (IDbCommand command = dbConnection.CreateCommand())
			{
				string sqlQuery = string.Format("INSERT INTO HighScores(Name,Score)VALUES('{0}','{1}')",name,newScore);
				command.CommandText = sqlQuery;
				command.ExecuteScalar();
				dbConnection.Close();
			}
		}
	}

	private void GetScores()
	{
		highscore.Clear ();
		using(IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			dbConnection.Open();

			using (IDbCommand command = dbConnection.CreateCommand())
			{
				string sqlQuery = "SELECT * FROM HighScores";
				command.CommandText = sqlQuery;

				using(IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						highscore.Add(new HighScore(reader.GetInt32(0),reader.GetInt32(2),reader.GetString(1),reader.GetDateTime(3)));
					}

					dbConnection.Close();
					reader.Close();
				}
			}
		}
	}

    private void clearBoard()
    {
        foreach(GameObject score in GameObject.FindGameObjectsWithTag("Score"))
        {
            Destroy(score);
        }
    }

	private void ShowScore(){
		GetScores ();
        clearBoard();
		highscore.Sort ();
		highscore.Reverse ();
		for (int i = 0; i < 10 ; i++){
			GameObject tmpObject = Instantiate(scorePrefab);
			HighScore tmpScore = highscore[i];

			tmpObject.GetComponent<ScoreScript>().setScore(tmpScore.Name, tmpScore.Score.ToString());
	
			tmpObject.transform.SetParent(scoreParent);

			tmpObject.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

		}
	}
}