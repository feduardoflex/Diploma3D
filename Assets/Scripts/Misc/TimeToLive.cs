using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour {
	[SerializeField] private float timeToLive = 10f;

	private float timer = 0f;

	private void FixedUpdate() {
		timer += Time.fixedDeltaTime;
		if(timer >= timeToLive) {
			Destroy(gameObject);
		}
	}
}
