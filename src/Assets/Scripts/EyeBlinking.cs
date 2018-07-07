using UnityEngine;
using System.Collections;

public class EyeBlinking : MonoBehaviour {

	void Start() {

		float startsIn = Random.Range (0.1f, 0.5f);
		InvokeRepeating("ShutEyes", startsIn, 0.75f);
		InvokeRepeating("OpenEyes", startsIn + 0.2f, 0.2f);
	}


	void ShutEyes() {
		GetComponent<SpriteRenderer> ().sortingOrder = -1;
	}

	void OpenEyes() {
		GetComponent<SpriteRenderer> ().sortingOrder = 1;
	}
}
