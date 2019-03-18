﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public int health = 100;

	public float movementSpeed = 1f;
	public Weapon m_weapon;
	public GameObject flashLight;
	public bool IsDead = false;

	GameObject m_BodyObject;
	Rigidbody m_Rigidbody;
	Vector3 m_Velocity = new Vector3(0f, 0f, 0f);
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
			m_Velocity.Set(x, 0f, z);
			m_Velocity = m_Velocity.normalized * movementSpeed * Time.deltaTime;
			m_Rigidbody.MovePosition(transform.position + m_Velocity);
		}
	}

	public void LookAt(Vector3 point) {
		//m_BodyObject.transform.LookAt(new Vector3(point.x, 0.5f, point.z));
		var offset = new Vector2(point.x - transform.position.x, point.z - transform.position.z);
		var angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, angle, 0);
	}

	public void Shoot() {
		if (m_weapon) {
			m_weapon.Fire();
		}
	}

	public void Reload() {
		if (m_weapon) {
			m_weapon.Reload();
		}
	}

	public void ToggleFlashlight() {
		flashLight.SetActive(!flashLight.activeSelf);
	}

	public void TakeDamage(int amount, Vector3 direction) {
		m_Rigidbody.AddForce(direction * 250f, ForceMode.Impulse);
		if (health <= 0) {
			Die();
		}
		Debug.Log(gameObject.name + " taking damage!");
		health -= amount;
	}

	public void Die() {
		IsDead = true;
		m_Rigidbody.isKinematic = false;
	}
}
