using UnityEngine;

public abstract class Unit: MonoBehaviour, IDamageable {

	[SerializeField] private protected float moveSpeed = 10f, moveMaxSpeed, attackDistance, attackCD;
	[SerializeField] private protected int health, attackDamage;

	private protected AudioSource effectsSource;
	private protected Animator animator;

	private protected float attackTimer = 0;
	private protected bool isAttack = false;

	private protected virtual void Awake() {
	}

	private protected virtual void Start() {
		animator = GetComponent<Animator>();
		effectsSource = GetComponent<AudioSource>();
		effectsSource.volume = AudioManager.instance.GetEffectsVolume();
		attackTimer = attackCD;
	}

	private protected virtual void Update() {
	}

	private protected virtual void FixedUpdate() {
		CalculateCD();

	}

	private protected virtual void CalculateCD() {
		if(attackTimer < attackCD) {
			attackTimer += Time.fixedDeltaTime;
		} else if(attackTimer >= attackCD && !isAttack) {
			isAttack = true;
		}
	}

	private protected virtual void Move() {

	}

	public virtual void TakeDamage(int _amount) {
		animator.SetTrigger("TakeDamage");
		health -= _amount;
		if(health <= 0) {
			Debug.Log("Dead " + name);
			animator.SetTrigger("Dead");
			this.enabled = false;
		}
	}

	private protected virtual void Attack() {
		attackTimer = 0;
		isAttack = false;
	}

	private protected virtual void Dead() {
		GetComponent<FacePlayer>().enabled = false;
		Destroy(gameObject);
	}
}
