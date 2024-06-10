using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy {

	[SerializeField] Transform[] waypoints;

	[SerializeField] private float waypointTimer = 0f, waypointTimerCD = 15f;

	private int currentWaypoint = 0;

	private protected override void Start() {
		base.Start();
		waypointTimer = waypointTimerCD;
	}

	private protected override void FixedUpdate() {
		base.FixedUpdate();
		if(!seenPlayer && waypointTimer >= waypointTimerCD) {
			ChangePatrolPos();
		}
	}

	private protected override void CalculateCD() {
		base.CalculateCD();
		if(!seenPlayer && waypointTimer < waypointTimerCD) {
			waypointTimer += Time.fixedDeltaTime;
		}
	}

	private protected override bool CanSeePlayer() {
		if(Physics.Raycast(transform.position, -(transform.position - player.position), out hit, viewRange, masks) && hit.collider.GetComponent<PlayerController>() != null) {
			seenPlayer = true;
			agent.speed = moveMaxSpeed;
			return true;
		}
		return false;
	}

	private void ChangePatrolPos() {
		if(currentWaypoint >= waypoints.Length - 1) {
			currentWaypoint = 0;
		} else {
			currentWaypoint++;
		}
		waypointTimer = 0;
		agent.SetDestination(waypoints[currentWaypoint].position);
	}
}
