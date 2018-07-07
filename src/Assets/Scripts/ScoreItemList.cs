using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScoreItemList {
	
	public List<ScoreItem> scores = new List<ScoreItem>();

	public static ScoreItemList CreateFromJSON(string jsonString) {
		ScoreItemList scoreItemList = new ScoreItemList ();
		var delimeter = '|';
		string[] scoreStrings = jsonString.Split(delimeter);

		foreach (string scoreString in scoreStrings) {
			ScoreItem scoreItem = ScoreItem.CreateFromJSON (scoreString);
			scoreItemList.scores.Add (scoreItem);
		}
		return scoreItemList;
	}
}
