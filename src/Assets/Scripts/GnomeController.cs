using UnityEngine;
using System.Collections;

public class GnomeController : MonoBehaviour {

	Rigidbody2D rb;
	BoxCollider2D boxCollider;
	public bool isMidAir = false;
	public bool isMoving = true;
	public float speed = 2f;
	private float direction = -1f;
	private bool bedTimerStarted = false;
	private float timeInBedSoFar = 0.0f;
	private float maxTimeInBed = 10f;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		boxCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		CheckBedTimer ();
	}

	/****************************************** PUBLIC METHODS ******************************************/

	public void Freeze() {
		rb.isKinematic = true;
		boxCollider.isTrigger = true;
		isMoving = false;
	}

	public void UnFreeze() {
		rb.isKinematic = false;
		boxCollider.isTrigger = false;
		isMoving = true;
	}

	public float GetWidth() {
		return boxCollider.size.x * 2;
	}

	public void MoveUp(float upBy) {
		transform.position = new Vector3 (transform.position.x, transform.position.y + upBy, transform.position.z);
	}

	public void MoveLeft(float leftBy) {
		transform.position = new Vector3 (transform.position.x - leftBy, transform.position.y, transform.position.z);
	}

	public void Flip(float degree) {
		transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z - degree);
	}

	public void StandUp() {
		transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0f);
	}

	public void BeInBed() {
		if (!bedTimerStarted) {
			bedTimerStarted = true;
		}
	}

	/****************************************** PRIVATE METHODS ******************************************/

	void Move() {
		if (isMoving && !isMidAir) {
			float velocity = this.speed * this.direction;
			rb.velocity = new Vector2 (velocity, rb.velocity.y);
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		this.direction = -1f * this.direction;
		var sprite = GetComponent<SpriteRenderer> ();
		sprite.flipX = !sprite.flipX;

		if (coll.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			isMidAir = false;
			StandUp ();
		}
	}

	void CheckBedTimer() {
		timeInBedSoFar += Time.deltaTime;
		if (bedTimerStarted && timeInBedSoFar >= maxTimeInBed) {
			UnFreeze ();
			StandUp ();
			bedTimerStarted = false;
			timeInBedSoFar = 0.0f;
		}
	}
}
