using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CuteScene : MonoBehaviour {

	public AudioSource introSound;
	public AudioSource blop;
	public AudioSource gong;
	public GameObject cover;
	public GameObject autoType;
	bool audioDone = false;

	// Use this for initialization
	void Start () {
		gong.Play ();
		introSound.Play();
		Invoke ("SoundFinished", introSound.clip.length);
	}

	void Update () {
		if (Input.anyKeyDown && audioDone) {
			SceneManager.LoadScene ("Main");
		}
	}

	void SoundFinished() {
		audioDone = true;
		blop.Play ();
		AutoType a = autoType.GetComponent<AutoType> ();
		cover.GetComponent<SpriteRenderer> ().sortingOrder = -10;
		a.Play ();
	}
}
