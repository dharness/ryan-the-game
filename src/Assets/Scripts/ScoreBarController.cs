using UnityEngine;
using System.Collections;

public class ScoreBarController : MonoBehaviour {

	void OnEnable() {
		EventManagerController.OnSetScore += SetScore;
	}


	void OnDisable() {
		EventManagerController.OnSetScore -= SetScore;
	}

	void SetScore(int newScore) {
		for (int i = 0; i <= 7; i++) {
			Transform child = transform.Find ("green_" + i);
			if (child) {
				SpriteRenderer sprite = child.gameObject.GetComponent<SpriteRenderer>();
				if (i < newScore) {
					sprite.sortingOrder = 1;
				} else {
					sprite.sortingOrder = -1;
				}
			}
		}
	}
}
