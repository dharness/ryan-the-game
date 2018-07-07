using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScoreItem {

	public string playerName;
	public string finishedAt;

	public static ScoreItem CreateFromJSON(string jsonString) {
		return JsonUtility.FromJson<ScoreItem>(jsonString);
	}

	public string ToString() {
		return playerName + ", " + finishedAt;
	}
}
