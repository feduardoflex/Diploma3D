using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

	private float x, y, z;

	private void Awake() {
		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;
	}

	private void FixedUpdate() {
		HoverObject();
	}

	private void HoverObject() {
		transform.position = new Vector3(x, y + (Mathf.Sin(Time.time) * .2f), z);
	}
}
