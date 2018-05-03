using UnityEngine;
using System;

public class HighScore : IComparable<HighScore>
{
	public int Score{ get; set;}
	public string Name{ get; set; }
	public DateTime Date{ get; set; }
	public int ID{ get; set; }

	public HighScore (int id, int score, string name, DateTime date) {
		this.ID = id;
		this.Score = score;
		this.Name = name;
		this.Date = date;
	}

	public int CompareTo(HighScore other){
		if (this.Score == other.Score)
			return this.Date.CompareTo (other.Date);
		return this.Score.CompareTo (other.Score);
	}
}

