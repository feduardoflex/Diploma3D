using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	private bool pauseMenuOpen = false;

	private void Awake() {
		if(instance != null && instance != this) {
			Destroy(this);
		} else {
			instance = this;
		}
	}

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1f;
	}

	void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
			if(!pauseMenuOpen) {
				Pause();
			} else {
				UnPause();
			}
		}
    }

	public void Pause() {
		pauseMenuOpen = true;
		Cursor.lockState = CursorLockMode.Confined;
		UIManager.instance.ShowPause();
		Cursor.visible = true;
		Time.timeScale = 0f;
	}

	public void UnPause() {
		pauseMenuOpen = false;
		Cursor.lockState = CursorLockMode.Locked;
		UIManager.instance.HidePause();
		Cursor.visible = false;
		Time.timeScale = 1f;
	}

	public void RestartLevel() {
		SceneManager.LoadScene(0);
	}

	public void PlayerDeath() {
		pauseMenuOpen = true;
		Cursor.lockState = CursorLockMode.Confined;
		UIManager.instance.PlayerDied();
		Cursor.visible = true;
		Time.timeScale = 0f;
	}

	public void QuitGame() {
		Application.Quit();
	}
}
