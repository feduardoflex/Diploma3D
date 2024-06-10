using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager: MonoBehaviour {

	public static AudioManager instance;

	private AudioSource ostSource;

	private float ostVolume = 0f, effectsVolume = 0f;

	private void Awake() {
		if(instance != null && instance != this) {
			Destroy(this);
		} else {
			instance = this;
		}
		ostSource = GetComponent<AudioSource>();
		ostSource.volume = ostVolume;
	}

	void Start() {

    }

    void Update() {
        
    }

	private void StopOST() {
		ostSource.Stop();
	}

	public float GetEffectsVolume() {
		return effectsVolume; 
	}
}
