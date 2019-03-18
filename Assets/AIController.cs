using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_STATE {
	POINTA,
	POINTB,
	NONE
}

public class AIController : MonoBehaviour {

	public float visionDistance = 5f;
	public GameObject player;
	public AIWeapon weapon;
	public bool shoot = true;
	public LayerMask layerMask;


	NavMeshAgent m_NavMeshAgent;
	Character m_Character;

	void Start() {
		m_NavMeshAgent = GetComponent<NavMeshAgent>();
		m_NavMeshAgent.updateRotation = false;
		m_Character = GetComponent<Character>();
		player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update() {
		if (!m_Character.IsDead) {
			if (Vector3.Distance(transform.position, player.transform.position) <= visionDistance) {
				RaycastHit hit;

				Vector3 targetDirection = (player.transform.position - transform.position).normalized;
				Ray ray = new Ray(transform.position, targetDirection);
				if (Physics.Raycast(ray, out hit, visionDistance, layerMask)) {
					if (hit.collider.CompareTag("Body")) {
						m_NavMeshAgent.ResetPath();
						m_NavMeshAgent.updateRotation = false;
						RotateTowards(player.transform.position);
						if (shoot)
							weapon.Fire();
					}
				}
			} else {
				WalkTowards(player);
			}
		} else {
			m_NavMeshAgent.enabled = false;
			Destroy(gameObject, 5f);
		}
	}

	void RotateTowards(Vector3 point) {
		var targetPosition = point;
		var targetPoint = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
		var _direction = (targetPoint - transform.position).normalized;
		var _lookRotation = Quaternion.LookRotation(_direction);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookRotation, 360);
	}

	void WalkTowards(GameObject o) {
		m_NavMeshAgent.ResetPath();
		m_NavMeshAgent.updateRotation = true;
		m_NavMeshAgent.destination = o.transform.position;
	}
}
