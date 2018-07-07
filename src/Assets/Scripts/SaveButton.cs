using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour {

	public GameObject clock;
	public GameObject input;

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			GetComponent<SpriteRenderer> ().sortingOrder *= -1;
		}
		if (Input.GetMouseButtonUp (0)) {
			GetComponent<SpriteRenderer> ().sortingOrder *= -1;
			StartCoroutine(SaveScore ());
		}
	}

	IEnumerator SaveScore() {
		string time = clock.GetComponent<ClockController> ().GetTime ().ToString ();
		string hour = time.Substring(0,2);
		string minute = time.Substring(time.Length - 2);
		string name = input.GetComponent<InputField> ().text;


		WWWForm form = new WWWForm();
		form.AddField("playerName", name);
		form.AddField("finishedAt", hour + ":" + minute);

		WWW w = new WWW("https://field-botany.hyperdev.space/api/scores", form);
		yield return w;
		if (!string.IsNullOrEmpty(w.error)) {
			print(w.error);
		}
		else {
			SceneManager.LoadScene ("ScoreBoard");
			print("Finished Uploading Scores");
		}
	}
}
