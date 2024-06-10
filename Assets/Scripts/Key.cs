using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable {

	[SerializeField] private MeshRenderer keyMaterial;

	[SerializeField] private KeyType keyColor;

	private void Start() {
		if((int)keyColor == 0) {
			keyMaterial.material.color = Color.red;
		} else if((int)keyColor == 1) {
			keyMaterial.material.color = Color.blue;
		} else if((int)keyColor == 2) {
			keyMaterial.material.color = Color.yellow;
		} else if((int)keyColor == 3) {
			keyMaterial.material.color = Color.green;
		}
	}

	public void Interact() {
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AddKey(keyColor);
		Destroy(gameObject);
	}
}
