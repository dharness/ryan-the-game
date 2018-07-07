using UnityEngine;
using System.Collections;

public class EventManagerController : MonoBehaviour {

	public delegate void SetScoreEvent(int score);
	public static event SetScoreEvent OnSetScore;

	public delegate void BedFullEvent();
	public static event BedFullEvent OnBedFull;

	public delegate void ClockSelectedEvent(string name);
	public static event ClockSelectedEvent OnClockSelected;

	public static void FireSetScoreEvent(int newScore) {
		if (OnSetScore != null) {
			OnSetScore (newScore);
		}
	}

	public static void FireBedFullEvent() {
		if (OnBedFull != null) {
			OnBedFull ();
		}
	}

	public static void FireClockSelectedEvent(string name) {
		if (OnClockSelected != null) {
			OnClockSelected (name);
		}
	}
}
