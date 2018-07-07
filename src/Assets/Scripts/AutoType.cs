using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AutoType : MonoBehaviour {

	public float letterPause = 0.2f;
	public AudioClip sound;

	string message;

	// Use this for initialization
	void Start () {
		message = GetComponent<Text>().text;
		GetComponent<Text>().text = "";
	}

	public void Play() {
		StartCoroutine(TypeText ());
	}

	IEnumerator TypeText () {
		foreach (char letter in message.ToCharArray()) {
			GetComponent<Text>().text += letter;
			if (sound)
				GetComponent<AudioSource>().PlayOneShot (sound);
			yield return 0;
			yield return new WaitForSeconds (letterPause);
		}
	}
}