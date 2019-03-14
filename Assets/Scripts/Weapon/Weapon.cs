using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public GameObject barrel;
	public GameObject hitFX;
	public Animator anim;
	public AudioSource aSource;
	public AudioClip[] sounds;

	public string weaponName = "Default";
	public float rof = 1;
	public int magSize = 10;

	Transform m_holder;
	Vector3 m_BulletOrigin;
	Vector3 m_BulletOriginOffset = new Vector3(0f, 0.5f, 0f);
	Magazine m_Magazine;
	ParticleSystem m_Muzzle;
	RaycastHit m_hit;
	bool m_CanFire = true;
	float m_Time;

	// Start is called before the first frame update
	void Start() {
		m_holder = transform.parent.parent.transform;
		m_Magazine = new Magazine(magSize);
		m_Muzzle = barrel.GetComponentInChildren<ParticleSystem>();
		m_Muzzle.Stop();
		m_Time = rof;
	}

	public void Update() {
		if (!m_CanFire) {
			m_Time -= Time.deltaTime;
		}

		if (m_Time <= 0f) {
			m_CanFire = true;
			m_Time = rof;
		}
	}

	public bool Fire() {
		if (m_Magazine.Ammo > 0 && m_CanFire) {
			m_Muzzle.Play();
			anim.SetTrigger("Shoot");
			aSource.clip = sounds[Random.Range(0, sounds.Length)];
			aSource.Play();
			Raycast();
			m_Magazine.EjectBullet();
			m_CanFire = false;
			return true;
		}

		return false;
	}

	public void Raycast() {
		m_BulletOrigin = m_holder.position + m_BulletOriginOffset;

		if (Physics.Raycast(m_BulletOrigin, m_holder.TransformDirection(Vector3.forward), out m_hit, Mathf.Infinity)) {
			Vector3 incomingVec = m_hit.point - m_BulletOrigin;
			Vector3 reflectVec = Vector3.Reflect(incomingVec, m_hit.normal);
			Debug.DrawLine(m_BulletOrigin, m_hit.point, Color.red, 5f);
			Debug.DrawRay(m_hit.point, reflectVec, Color.green, 5f);

			Instantiate(hitFX, m_hit.point, Quaternion.FromToRotation(Vector3.forward, m_hit.normal));
		}
	}

	public void Reload() {
		m_Magazine.Reload();
		Debug.Log("Reloading... Done!");
	}

	public bool CanFire() {
		return m_CanFire;
	}
}
