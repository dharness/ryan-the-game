using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnterButton : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			GetComponent<SpriteRenderer> ().sortingOrder *= -1;
		}
		if (Input.GetKeyUp(KeyCode.Return)) {
			GetComponent<SpriteRenderer> ().sortingOrder *= -1;
			SceneManager.LoadScene ("IntroCutScene");
		}
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			GetComponent<SpriteRenderer> ().sortingOrder *= -1;
		}
		if (Input.GetMouseButtonUp (0)) {
			GetComponent<SpriteRenderer> ().sortingOrder *= -1;
			SceneManager.LoadScene ("IntroCutScene");
		}
	}
}
