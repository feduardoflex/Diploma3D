using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController: Unit {

	[SerializeField] private Animator animatorSword;
	[SerializeField] private Animator animatorShield;
	[SerializeField] private Slider[] slidersShield;
	[SerializeField] private Slider sliderWeapon;
	[SerializeField] private Slider sliderHealthBar;
	//[SerializeField] private Image indicatorShield;
	//[SerializeField] private Transform attackPosition;
	[SerializeField] private int healthMax;

	//private Rigidbody rb;
	private CharacterController characterController;

	private Vector3 movement, movementVelocity;
	private LayerMask damagableLayer;

	private float moveX, moveY, gravity = -9.81f, blockingTimer = 0f, blockingCD = 5f;
	private int[] items;
	private bool[] keys;
	private int blocking = 0, blockingMax = 3;
	private bool isBlocking = false;

	private protected override void Awake() {
		base.Awake();
		characterController = GetComponent<CharacterController>();
		items = new int[1];
		items[0] = 0;
		keys = new bool[5];
		for (int i = 0; i < 5; i++) {
			keys[i] = false;
		}
		damagableLayer = LayerMask.GetMask("Damagable");
	}

	private protected override void Start() {
		base.Start();
		blocking = blockingMax;
		//rb = GetComponent<Rigidbody>();
	}

	private protected override void Update() {
		base.Update();
		//if(Input.GetMouseButtonDown(0)) {
		//	if(!isBlocking && isAttack) {
		//		Attack();
		//	}
		//}
		if(Input.GetMouseButtonDown(1)) {
			if(blocking > 0) {
				StartBlocking();
			}
		}
		if(Input.GetMouseButtonUp(1)) {
			if(isBlocking) {
				StopBlocking();
			}
		}
		Move();
	}

	private protected override void FixedUpdate() {
		base.FixedUpdate();
	}

	private protected override void CalculateCD() {
		base.CalculateCD();
		if(blockingTimer >= blockingCD) {
			blocking++;
			slidersShield[blocking - 1].value = 1f;
			blockingTimer = 0;
		}
		if(blocking < blockingMax) {
			blockingTimer += Time.deltaTime;
			slidersShield[blocking].value = blockingTimer / blockingCD;
		}
		sliderWeapon.value = attackTimer / attackCD;
	}

	private protected override void Move() {
		base.Move();
		moveX = Input.GetAxisRaw("Horizontal");
		moveY = Input.GetAxisRaw("Vertical");
		movement = transform.forward * moveY + transform.right * moveX;
		//rb.AddForce(movement.normalized * moveSpeed, ForceMode.Force);
		characterController.Move(movement.normalized * moveSpeed * Time.deltaTime);
		movementVelocity.y += gravity * Time.deltaTime;
		characterController.Move(movementVelocity * Time.deltaTime);
	}

	public override void TakeDamage(int _amount) {
		if(blocking > 0 && isBlocking) {
			animatorShield.SetTrigger("Attacked");
			blocking--;
			if(blocking == 0) {
				StopBlocking();
			}
		} else {
			health -= _amount;
			sliderHealthBar.value = health;
			PostProcessingManager.instance.TakeDamage();
			StartCoroutine(PlayerCameraShake.instance.Shake(.2f, .1f));
			if(health <= 0) {
				Debug.Log("Dead " + name);
				GameManager.instance.PlayerDeath();
			}
		}
		for(int i = blockingMax - 1; i > blocking; i--) {
			slidersShield[i].value = 0f;
		}
	}

	public void SetItem(int _index) {
		items[_index]++;
		Debug.Log("Item " + _index + " added to inventory");
	}

	public void AddKey(KeyType _color) {
		keys[(int)_color] = true;
		Debug.Log("Key " + (int)_color + " added to inventory");
	}

	public bool GetKey(KeyType _color) {
		return keys[(int)_color];
	}

	private protected override void Attack() {
		base.Attack();
		animatorSword.SetTrigger("Attack");
		//Collider[] hits = Physics.OverlapBox(attackPosition.position, new Vector3(.5f, .5f, .8f * attackDistance), transform.rotation, damagableLayer);
		//foreach(Collider hit in hits) {
		//	if(hit.GetComponent<IDamageable>() != null) {
		//		hit.GetComponent<IDamageable>().TakeDamage(attackDamage);
		//		Debug.Log(name + " is attacking " + hit.name + " by " + attackDamage + " damage");
		//	}
		//}

	}

	public void StartAttack() {
		Attack();
	}

	public int GetDamage() {
		return attackDamage;
	}

	public bool IsAttack() {
		return isAttack;
	}

	public bool IsBlocking() {
		return isBlocking;
	}

	private void StartBlocking() {
		isBlocking = true;
		//Color newColor = indicatorShield.color;
		//newColor.a = 0.7843137254901961f;
		//indicatorShield.color = newColor;
		moveSpeed = moveMaxSpeed / 2;
		animatorShield.SetBool("Block", true);
	}

	private void StopBlocking() {
		isBlocking = false;
		//Color newColor = indicatorShield.color;
		//newColor.a = 0.196078431372549f;
		//indicatorShield.color = newColor;
		moveSpeed = moveMaxSpeed;
		animatorShield.SetBool("Block", false);
	}

	public void TakeHeal(int _amount) {
		if(health + _amount > healthMax) {
			health = healthMax;
		} else {
			health += _amount;
		}
		sliderHealthBar.value = health;
	}
}