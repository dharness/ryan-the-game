using UnityEngine;
using System.Collections;

public class SaveMenu : MonoBehaviour {

	bool isHidden = true;

	void Start () {
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers) {
			r.enabled = false;
		}
	}

	void OnEnable() {
		EventManagerController.OnBedFull += HandleBedFull;
	}

	void OnDisable() {
		EventManagerController.OnBedFull -= HandleBedFull;
	}

	void HandleBedFull() {
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer r in renderers) {
			r.enabled = true;
		}
	}
}
