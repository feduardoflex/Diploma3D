using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Unit {

	[SerializeField] private protected float viewRange;

	[SerializeField] private protected LayerMask masks;

	private protected NavMeshAgent agent;
	private protected Transform player;

	private protected RaycastHit hit;

	private protected bool seenPlayer = false;

	private protected override void Awake() {
		base.Awake();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();
	}

	private protected override void Start() {
		base.Start();
		agent.speed = moveSpeed;
	}

	private protected override void FixedUpdate() {
		base.FixedUpdate();
		if(seenPlayer) {
			ChasePlayer();
		}
		if(!seenPlayer) {
			CanSeePlayer();
		}
	}

	private protected override void Update() {
		base.Update();
    }

	private protected virtual void StartAttack() {
		attackTimer = 0;
		isAttack = false;
		animator.SetTrigger("Attack");
	}

	private protected override void Attack() {
		agent.SetDestination(transform.position);
        if (Vector3.Distance(transform.position, player.position) <= attackDistance) {
			player.GetComponent<IDamageable>().TakeDamage(attackDamage);
			Debug.Log(name + " is attacking");
		}
	}

	private protected virtual void ChasePlayer() {
		float distance = Vector3.Distance(transform.position, player.position);
		if(distance <= attackDistance * 0.9f && CanSeePlayer()) {
			if(isAttack) {
				StartAttack();
			}
		} else {
			agent.SetDestination(player.position);
			animator.SetTrigger("Move");
		}
	}

	private protected virtual bool CanSeePlayer() {
		if(Physics.Raycast(transform.position, -(transform.position - player.position), out hit, viewRange, masks) && hit.collider.GetComponent<PlayerController>() != null) {
			seenPlayer = true;
			return true;
		}
		return false;
	}

	public override void TakeDamage(int _amount) {
		base.TakeDamage(_amount);
		attackTimer = 0f;
	}
}
