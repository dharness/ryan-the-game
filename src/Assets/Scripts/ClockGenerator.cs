using UnityEngine;
using System.Collections;

public class ClockGenerator : MonoBehaviour {

	public GameObject clockPrefab;
	public float min=2f;
	public float max=3f;
	int count = 0;
	ScoreItemList scoreItemList;

	void Start () {
		StartCoroutine (LoadScores());
		min=transform.position.x-10;
		max=transform.position.x+10;
	}

	private IEnumerator LoadScores() {
		WWW myWww = new WWW("https://field-botany.hyperdev.space/api/scores");
		yield return myWww;
		ScoreItemList scoreItems = ScoreItemList.CreateFromJSON(myWww.text);
		this.scoreItemList = scoreItems;
		InvokeRepeating ("DropClock", 0f, 1.5f);
		DropClock();
	}


	void DropClock () {
		ScoreItem scoreItem = scoreItemList.scores[this.count++ % scoreItemList.scores.Count];
		GameObject clock = Instantiate (clockPrefab, transform.position, Quaternion.identity) as GameObject;
		float time = float.Parse(scoreItem.finishedAt);
		string name = scoreItem.playerName;

		ClockController clockController = clock.GetComponent<ClockController> ();
		clockController.stopTicking ();
		clockController.SetName (name);
		clockController.SetClock (time);
	}

	void Update() {
		transform.position =new Vector3(Mathf.PingPong(Time.time*2,max-min)+min, transform.position.y, transform.position.z);
	}
}
