using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TvController : MonoBehaviour {
	Text text;

	void OnEnable() {
		EventManagerController.OnClockSelected += HandleSelectedClock;
	}

	void OnDisable() {
		EventManagerController.OnClockSelected -= HandleSelectedClock;
	}
		
	void Start () {
		text = transform.Find ("Canvas").Find("Text").GetComponent<Text>();
	}

	void HandleSelectedClock(string name) {
		text.text = name;
	}
}
