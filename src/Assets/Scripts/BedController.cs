using UnityEngine;
using System.Collections;

public class BedController : MonoBehaviour {

	BoxCollider2D boxCollider;
	private int gnomesOntheBedCount = 0;
	public LayerMask ThingsOnTheBed;

	void Start() {
		boxCollider = GetComponent<BoxCollider2D> ();
	}

	void FixedUpdate() {

		float centerY = transform.position.y + boxCollider.offset.y;
		Vector2 centerPoint = new Vector2 (transform.position.x, centerY);

		Vector2 bedBox = new Vector2 (boxCollider.size.x * 3.7f, 20f);
		Collider2D[] gnomesOnTheBed = Physics2D.OverlapBoxAll (centerPoint, bedBox, 0f, ThingsOnTheBed);
		gnomesOntheBedCount = gnomesOnTheBed.Length;
		EventManagerController.FireSetScoreEvent(gnomesOntheBedCount);

		foreach (Collider2D coll in gnomesOnTheBed) {
			GnomeController gnome = coll.gameObject.GetComponent<GnomeController> ();
			gnome.BeInBed ();
		}

		if (gnomesOntheBedCount == 7) {
			EventManagerController.FireBedFullEvent ();
		}
	}
}
