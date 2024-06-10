using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour, IInteractable {

	[SerializeField] private int healAmount;

	public void Interact() {
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TakeHeal(healAmount);
		Destroy(gameObject);
	}
}
