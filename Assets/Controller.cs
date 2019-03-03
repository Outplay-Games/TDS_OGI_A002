using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public GameObject characterObject;
	public Camera camera;
	public LayerMask rayMask;

	private Character m_character;
	private Ray m_ray;
	private RaycastHit hit;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (characterObject) {
			if (!m_character) {
				m_character = characterObject.GetComponent<Character>();
			}

			float x = Input.GetAxisRaw("Horizontal");
			float z = Input.GetAxisRaw("Vertical");

			if (x != 0f && z != 0f) {
				m_character.Move(x, z);
			} else if (x != 0f || z != 0f) {
				m_character.Move(x, z);
			}

			m_ray = camera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(m_ray, out hit, Mathf.Infinity, rayMask)) {
				if (hit.collider.CompareTag("Ground")) {
					m_character.LookAt(hit.point);
				}
			}
		}
	}
}
