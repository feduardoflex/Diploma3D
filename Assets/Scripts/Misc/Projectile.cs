using UnityEngine;

public class Projectile : MonoBehaviour {

	private Rigidbody rb;

	private CapsuleCollider parentCollider;

	private int damage;
	private bool hit = false;

    private void Start() {
		rb = GetComponent<Rigidbody>();
		rb.AddForce(-transform.forward * 300f);
		Physics.IgnoreCollision(GetComponent<Collider>(), parentCollider);
	}

	private void OnTriggerEnter(Collider other) {
		if(!hit) {
			this.transform.parent = other.gameObject.transform;
			hit = true;
			rb.velocity = Vector3.zero;
			if(other.GetComponent<IDamageable>() != null) {
				other.GetComponent<IDamageable>().TakeDamage(damage);
			}
			GetComponent<TrailRenderer>().enabled = false;
		}
	}

	private void FixedUpdate() {
		if(!hit) {
			if(rb.velocity != Vector3.zero) {
				rb.MoveRotation(Quaternion.LookRotation(rb.velocity));
			}
		}
	}

	public void SetArrow(int _damageAmount, CapsuleCollider _collider) {
		damage = _damageAmount;
		parentCollider = _collider;
	}
}
