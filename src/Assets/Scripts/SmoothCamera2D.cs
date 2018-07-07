using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {


	bool isZooming = false;
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public Vector3 maxBounds;
	public Vector3 minBounds;
	public Transform clock;
	Camera _camera;

	void OnEnable() {
		EventManagerController.OnBedFull += HandleBedFull;
	}

	void OnDisable() {
		EventManagerController.OnBedFull -= HandleBedFull;
	}


	void Start() {
		_camera = GetComponent<Camera> ();
	}

	void Update () {
		if (target && !isZooming) {
			Vector3 point = _camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.21f, point.z));
			Vector3 destination = transform.position + delta;
			Vector3 unClampedPosition = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			Vector3 clampedPosition = new Vector3 (
				                          Mathf.Clamp (unClampedPosition.x, minBounds.x, maxBounds.x),
				                          Mathf.Clamp (unClampedPosition.y, minBounds.y, maxBounds.y),
				                          Mathf.Clamp (unClampedPosition.z, minBounds.z, maxBounds.z)
			                          );
			transform.position = clampedPosition;
		} else if (isZooming) {
			Vector3 clockPosition = new Vector3(clock.position.x + 1.0f, -2f, transform.position.z);
			transform.position = Vector3.MoveTowards (transform.position, clockPosition, 0.1f);
			if (_camera.orthographicSize > 1) {
				_camera.orthographicSize -= .03f;	
			}
		}

	}

	void HandleBedFull() {
		StartCoroutine(ZoomInOnClock (1.3f));
	}

	IEnumerator ZoomInOnClock(float time) {
		yield return new WaitForSeconds(time);
		isZooming = true;
	}
}