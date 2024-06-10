using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class DoorLocked : Door {

	[SerializeField] private MeshRenderer[] lockMaterial;
	[SerializeField] private KeyType keyRequired;

	private PlayerController playerController;

	private protected override void Awake() {
		base.Awake();
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	private void Start() {
		if((int)keyRequired == 0) {
			lockMaterial[0].material.color = Color.red;
			lockMaterial[1].material.color = Color.red;
		} else if ((int)keyRequired == 1) {
			lockMaterial[0].material.color = Color.blue;
			lockMaterial[1].material.color = Color.blue;
		} else if ((int)keyRequired == 2) {
			lockMaterial[0].material.color = Color.yellow;
			lockMaterial[1].material.color = Color.yellow;
		} else if((int)keyRequired == 3) {
			lockMaterial[0].material.color = Color.green;
			lockMaterial[1].material.color = Color.green;
		}
	}

	public override void Interact() {
        if (playerController.GetKey(keyRequired)) {
			base.Interact();
		}
	}
}
