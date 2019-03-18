using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashLight : MonoBehaviour {

	bool isActive;
	float cd = 0.2f;
	float m_Time = 0.2f;

	// Start is called before the first frame update
	void Start() {
		gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		if (isActive) {
			m_Time -= Time.deltaTime;

			if (m_Time <= 0f) {
				m_Time = 0.1f;
				Deactivate();
			}
		}
	}

	public void Activate() {
		gameObject.SetActive(true);
		isActive = true;
	}

	public void Deactivate() {
		gameObject.SetActive(false);
		isActive = false;
	}
}
