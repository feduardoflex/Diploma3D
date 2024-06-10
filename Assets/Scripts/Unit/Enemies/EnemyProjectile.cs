using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile: Enemy {

	[SerializeField] private GameObject projectile;

	[SerializeField] private float projectileAttackDistance;

	private protected virtual void Shoot() {
		agent.SetDestination(transform.position);
		float distance = Vector3.Distance(transform.position, player.position);
		if(distance <= projectileAttackDistance && distance > attackDistance) {
			Vector3 shootingPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			GameObject arrow = Instantiate(projectile, shootingPos, Quaternion.LookRotation(transform.position - player.position, Vector3.up));
			arrow.GetComponent<Projectile>().SetArrow(attackDamage, GetComponent<CapsuleCollider>());
			Debug.Log(name + " is attaking");
		}
	}

	private void StartShooting() {
		attackTimer = 0;
		isAttack = false;
		animator.SetTrigger("Shoot");
	}
	
	private protected override void ChasePlayer() {
		float distance = Vector3.Distance(transform.position, player.position);
		if(distance <= projectileAttackDistance * 0.9f && distance > attackDistance && CanSeePlayer()) {
			if(isAttack) {
				StartShooting();
			}
		} else if(distance <= attackDistance * 0.9f && CanSeePlayer()) {
			if(isAttack) {
				StartAttack();
			}
		} else {
			agent.SetDestination(player.position);
			animator.SetTrigger("Move");
		}
	}
}
