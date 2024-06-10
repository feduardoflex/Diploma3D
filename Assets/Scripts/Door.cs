using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class Door : MonoBehaviour, IInteractable {

	private protected NavMeshSurface navMeshSurface;
	private protected NavMeshObstacle obstacle;

	private protected virtual void Awake() {
		obstacle = GetComponent<NavMeshObstacle>();
		navMeshSurface = GameObject.FindGameObjectWithTag("NavMeshSurface").GetComponent<NavMeshSurface>();
	}

	public virtual void Interact() {
		obstacle.carving = false;
		navMeshSurface.BuildNavMesh();
		gameObject.SetActive(false);
	}
}
