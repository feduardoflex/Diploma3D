using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour {

	public static PostProcessingManager instance;

	[SerializeField] private PostProcessProfile ppProfile;
	[SerializeField] private AnimationCurve vignetteCurve;
	
	[SerializeField] private Color levelFogColor;


	private Vignette vignette;

	private float vignetteIntensityStart;

	private void Awake() {
		if(instance != null && instance != this) {
			Destroy(this);
		} else {
			instance = this;
		}
		ppProfile.TryGetSettings(out vignette);
	}

	private void Start() {
		RenderSettings.fogColor = levelFogColor;
	}

	private void Update() {
		float vignetteIntensity = vignetteCurve.Evaluate(Time.realtimeSinceStartup - vignetteIntensityStart);
		vignette.intensity.value = vignetteIntensity;
	}

	public void TakeDamage() {
		vignetteIntensityStart = Time.realtimeSinceStartup;
	}
}
