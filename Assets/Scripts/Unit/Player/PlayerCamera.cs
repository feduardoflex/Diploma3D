using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour {

	[SerializeField] private TMP_Text interactText;
	[SerializeField] private Transform cam;
	[SerializeField] private PlayerController playerController;

	[SerializeField] private LayerMask interactableMask;

	[SerializeField] private float sensY = 1, sensX = 1;

	private RaycastHit hit;
	private LayerMask attackLayer;

	private float xRot, yRot, moveY, moveX;

	private void Awake() {

	}

	void Start() {
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		attackLayer = LayerMask.GetMask("Damagable");
	}

	void Update() {
		CameraRotate();
		Interact();

		if(Input.GetMouseButtonDown(0)) {
			if(playerController.IsAttack() && !playerController.IsBlocking()) {
				Attack();
			}
		}
	}

	private void CameraRotate() {
		moveY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
		moveX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;

		yRot += moveX;
		xRot -= moveY;
		xRot = Mathf.Clamp(xRot, -90f, 90f);

		transform.rotation = Quaternion.Euler(xRot, yRot, 0);
		cam.rotation = Quaternion.Euler(0, yRot, 0);
	}

	private void Interact() {
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, interactableMask)) {
			interactText.text = "\"E\" to interact";
			if(Input.GetKeyDown(KeyCode.E)) {
				hit.collider.GetComponent<IInteractable>().Interact();
			}
		} else {
			interactText.text = "";
		}
	}

	private void Attack() {
		playerController.StartAttack();
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f, attackLayer)) {
			hit.collider.GetComponent<IDamageable>().TakeDamage(playerController.GetDamage());
		}
	}
}
