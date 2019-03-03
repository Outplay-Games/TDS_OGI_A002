using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public float movementSpeed = 1f;

	GameObject m_BodyObject;
	Rigidbody m_Rigidbody;
	Vector3 velocity = new Vector3(0f, 0f, 0f);
	Vector3 m_EulerAngleVelocity;

	void Start() {
		// CHANGE THIS
		m_BodyObject = transform.GetChild(0).gameObject;
		// ^^
		m_EulerAngleVelocity = new Vector3(0, 100, 0);
		m_Rigidbody = GetComponent<Rigidbody>();
	}

	public void Move(float x, float z) {
		if (m_Rigidbody) {
			velocity.Set(x, 0f, z);
			velocity = velocity.normalized * movementSpeed * Time.deltaTime;
			m_Rigidbody.MovePosition(transform.position + velocity);
		}
	}

	public void LookAt(Vector3 point) {
		m_BodyObject.transform.LookAt(new Vector3(point.x, 0.5f, point.z));
	}
}
