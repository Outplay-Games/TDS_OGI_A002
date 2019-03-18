using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public GameObject characterObject;
	public CameraBehavior cameraBehavior;
	public Camera camera;
	public LayerMask rayMask;
	public float scrollSpeed;

	private Character m_character;
	private Ray m_ray;
	private RaycastHit hit;

	private float m_mouseScroll;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

		m_mouseScroll = Input.GetAxis("Mouse ScrollWheel");

		if (cameraBehavior) {
			cameraBehavior.Scroll(m_mouseScroll * scrollSpeed);
		}

		if (characterObject) {
			if (!m_character) {
				m_character = characterObject.GetComponent<Character>();
			}

			if (Input.GetKey(KeyCode.Mouse0)) {
				m_character.Shoot();
			}

			if (Input.GetKeyDown(KeyCode.R)) {
				m_character.Reload();
			}

			if (Input.GetKeyDown(KeyCode.E)) {
				m_character.ToggleFlashlight();
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
