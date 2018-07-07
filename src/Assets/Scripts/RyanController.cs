using UnityEngine;
using System.Collections;

public class RyanController : MonoBehaviour {


	Animator animator;
	Rigidbody2D rb;
	BoxCollider2D boxCollider;
	GameObject currentGnome;
	GameObject currentClock;
	public AudioSource huhSound;

	bool isHoldingItem = false;
	bool grounded = false;
	bool facingRight = true;
	float groundRadius = 0.2f;
	float grabRadius = 0.75f;
	bool isFrozen = false;
	public float jumpForce = 800f;
	public float speed = 10f;
	public Transform groundCheck;
	public Transform gnomeHolster;
	public Transform grabCenter;
	public LayerMask whatIsGround;
	public LayerMask whatCanBeGrabbed;

	void OnEnable() {
		EventManagerController.OnBedFull += HandleBedFull;
	}

	void OnDisable() {
		EventManagerController.OnBedFull -= HandleBedFull;
	}


	void Start () {
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		boxCollider = GetComponent<BoxCollider2D> ();
	}

	void Update () {
		if (!isFrozen) {
			if (grounded && Input.GetKeyDown (KeyCode.UpArrow)) {
				animator.SetBool ("Ground", false);
				rb.AddForce (new Vector2 (0, jumpForce));
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				Throw ();
			}

			if (Input.GetKey (KeyCode.Space)) {
				animator.SetBool ("isGrabbing", true);
				Grab ();
			} else {
				animator.SetBool ("isGrabbing", false);
			}
		}
	}

	void FixedUpdate () {
		if (!isFrozen) {
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

			animator.SetBool ("Ground", grounded);
			animator.SetFloat ("vSpeed", rb.velocity.y);

			float move = Input.GetAxis ("Horizontal");
			animator.SetFloat ("Speed", Mathf.Abs (move));
			rb.velocity = new Vector2 (move * speed, rb.velocity.y);

			if (move > 0 && !facingRight) {
				Flip ();
			} else if (move < 0 && facingRight) {
				Flip ();
			}
		}
	}

	/****************************************** PUBLIC METHODS ******************************************/

	public float GetHeight() {
		return boxCollider.size.y * 2;
	}

	public float GetWidth() {
		return boxCollider.size.x * 2;
	}

	/****************************************** PRIVATE METHODS ******************************************/

	void Grab() {
		if (!isHoldingItem) {
			Vector2 grabBox = new Vector2 (grabRadius, GetHeight());
			Collider2D[] gnomesToGrab = Physics2D.OverlapBoxAll (grabCenter.position, grabBox, 0f, whatCanBeGrabbed);

			foreach (Collider2D coll in gnomesToGrab) {
				coll.gameObject.transform.parent = gameObject.transform;
			
				GnomeController gnome = coll.gameObject.GetComponent<GnomeController> ();
				ClockController clock = coll.gameObject.GetComponent<ClockController> ();

				if (gnome != null) {
					Vector3 newGnomePosition = gnomeHolster.position;
					newGnomePosition.y += gnome.GetWidth ();
					coll.gameObject.transform.position = newGnomePosition;
					gnome.Flip (90);
					gnome.Freeze ();

					// Fix the scale
					Vector3 gnomeScale = new Vector3 (1.1f, 1.1f, 0.3f);
					coll.gameObject.transform.localScale = gnomeScale;

					currentGnome = coll.gameObject;
					animator.SetBool ("isCarrying", true);
					this.isHoldingItem = true;
				} else if (clock != null) {
					Vector3 newClockPosition = gnomeHolster.position;
					newClockPosition.y += clock.GetHeight () / 4;
					coll.gameObject.transform.position = newClockPosition;
					clock.FixRotation ();
					clock.Freeze ();

					// Fix the scale
					Vector3 clockScale = new Vector3 (0.26f, 0.26f, 0.26f);
					coll.gameObject.transform.localScale = clockScale;

					animator.SetBool ("isCarrying", true);
					this.isHoldingItem = true;
					currentClock = coll.gameObject;
					string name = currentClock.GetComponent<ClockController> ().GetName();
					EventManagerController.FireClockSelectedEvent (name);
				}
			}
		}
	}

	void Throw() {
		if (isHoldingItem) {
			if (huhSound != null) {
				huhSound.Play ();
			}

			if (currentGnome != null) {
				GnomeController gnome = currentGnome.GetComponent<GnomeController> ();
				currentGnome.transform.parent = null;
				gnome.UnFreeze ();
				gnome.isMidAir = true;
				Rigidbody2D gnomeRb = currentGnome.GetComponent<Rigidbody2D> ();
				float direction = facingRight ? 1f : -1f;
				gnomeRb.velocity = new Vector2 (7.5f * direction, 15f);
				isHoldingItem = false;
				currentGnome = null;
				animator.SetBool ("isCarrying", false);
			} else if (currentClock) {
				ClockController clock = currentClock.GetComponent<ClockController> ();
				currentClock.transform.parent = null;
				clock.UnFreeze ();
				Rigidbody2D clockRb = currentClock.GetComponent<Rigidbody2D> ();
				float direction = facingRight ? 1f : -1f;
				clockRb.velocity = new Vector2 (7.5f * direction, 15f);
				isHoldingItem = false;
				currentClock = null;
				animator.SetBool ("isCarrying", false);
				EventManagerController.FireClockSelectedEvent ("");
			}
		}
	}

	void Flip () {
		facingRight = !facingRight;
		transform.localScale = new Vector2(transform.localScale.x *-1, transform.localScale.y);
		Transform clock = transform.Find ("Clock");
		if (clock != null) {
			clock.localScale = new Vector2(clock.localScale.x *-1, clock.localScale.y);
		}
	}

	void HandleBedFull(){
		isFrozen = true;
	}
}