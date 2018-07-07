using UnityEngine;
using System.Collections;
using System;

public class ClockController : MonoBehaviour {

	float currentMinute = DateTime.Now.Minute;
	public float displayTime = 1158;
	public float gameEndTime = 1200;
	bool isStoppedTicking = false;
	BoxCollider2D boxCollider;
	Rigidbody2D rb;

	void Start() {
		if (!isStoppedTicking) {
			SetClock (displayTime);
		}
		boxCollider = GetComponent<BoxCollider2D> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		float minute = DateTime.Now.Minute;
		if (minute > currentMinute && !isStoppedTicking) {
			currentMinute = minute;
			displayTime++;
			SetClock (displayTime);
		}
	}

	public float GetTime() {
		return displayTime;
	}

	public float GetHeight() {
		return boxCollider.size.y * 2;
	}

	public void Freeze() {
		rb.isKinematic = true;
	}

	public void UnFreeze() {
		rb.isKinematic = false;
	}

	public void FixRotation() {
		transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0f);
	}

	public void stopTicking() {
		isStoppedTicking = true;
	}

	public string GetName() {
		return name;
	}

	public void SetName(string name) {
		this.name = name;
	}

	public void SetClock(float setTo) {
		for (float i = displayTime; i <= gameEndTime; i++) {
			Transform child = transform.Find (i.ToString());
			if (child != null) {
				SpriteRenderer sprite = child.gameObject.GetComponent<SpriteRenderer>();
				sprite.sortingOrder = 0;
				if (i == setTo) {
					sprite.sortingOrder = (int) i;
				}
			}
		}
	}
}
