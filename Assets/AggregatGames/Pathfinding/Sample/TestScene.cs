using UnityEngine;
using System.Collections;
using AggregatGames.AI.Pathfinding;

public class TestScene : MonoBehaviour {
	public CharacterController cc;

	public Transform target;
	public Pathfinder pathfinder;
	public PathKnot[] knots;
	public int knotIndex = -1;

	public float rotationSpeed = 20f;
	public float walkingSpeed = 2f;
	public float reachedKnot = 1f;

	void Update() {
		walkTo(target);
	}

	public void walkTo(Transform target) {
		if (knotIndex == -1 || knots == null || pathfinder.target != target.position) {
			pathfinder.findPath(transform.position, target.position, foundPath);
		} else if (knotIndex != -3 && knotIndex != -2) {

			Vector3 lookPos = knots[knotIndex].position - transform.position;
			lookPos.y = 0;
			Quaternion rotation = Quaternion.LookRotation(lookPos);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
			cc.SimpleMove(transform.root.forward*walkingSpeed);
			if (Vector3.Distance(transform.position, knots[knotIndex].position) < reachedKnot) knotIndex = ((knotIndex+1 < knots.Length)? knotIndex+1:-2);
		}
	}

	public void foundPath(Pathinfo info) {
		if (info.foundPath) {
			knots = info.path.getPath();
			knotIndex = 0;
		} else {
			Debug.Log("no path could be found for "+pathfinder.identification);
			knotIndex = -3;
		}
	}
}
