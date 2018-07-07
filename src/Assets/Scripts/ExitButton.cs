using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour {

	void OnMouseOver() {
		if (Input.GetMouseButtonUp (0)) {
			SceneManager.LoadScene ("Start");
		}	
	}
}
