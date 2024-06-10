using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraShake : MonoBehaviour {
	// Start is called before the first frame update

	public static PlayerCameraShake instance;

	private Vector3 startPos;

	private void Awake() {
		if(instance != null && instance != this) {
			Destroy(this);
		} else {
			instance = this;
		}

	}

	void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public IEnumerator Shake(float _duration, float _amount) {
		startPos = transform.localPosition;
		float time = 0f;
		while(time < _duration) {
			float x = Random.Range(-.5f, .5f) * _amount;
			float y = Random.Range(-.5f, .5f) * _amount;

			transform.localPosition = new Vector3(x, y, startPos.z);

			time += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = startPos;
	}
}
