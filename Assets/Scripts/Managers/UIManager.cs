using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	[SerializeField] GameObject pauseMenu;
	[SerializeField] Button continueRestartButton;
	[SerializeField] TMP_Text continueRestartText;

	public static UIManager instance;

	private void Awake() {
		if(instance != null && instance != this) {
			Destroy(this);
		} else {
			instance = this;
		}
	}

	public void ShowPause() {
		pauseMenu.SetActive(true);
		continueRestartButton.onClick.AddListener(GameManager.instance.UnPause);
		continueRestartText.text = "Continue";
	}

	public void HidePause() {
		continueRestartButton.onClick.RemoveAllListeners();
		pauseMenu.SetActive(false);
	}

	public void PlayerDied() {
		pauseMenu.SetActive(true);
		continueRestartButton.onClick.AddListener(GameManager.instance.RestartLevel);
		continueRestartText.text = "Restart";
	}

	public void QuitGame() {
		GameManager.instance.QuitGame();
	}
}
