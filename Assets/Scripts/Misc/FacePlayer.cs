using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class FacePlayer : MonoBehaviour {

	private Transform player;

	private Vector3 direction;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	private void FixedUpdate() {
		if(Vector3.Distance(transform.position, player.position) <= 25f) {
			LookAtPlayer();
		}
	}

	private void LookAtPlayer() {
		direction = player.position - transform.position;
		direction.y = 0;
		if(direction != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation(direction);
		}
	}
}
