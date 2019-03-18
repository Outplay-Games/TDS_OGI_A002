using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {


	public AudioClip[] sounds;
	public LayerMask layerMask;
	public string weaponName = "Default";

	[Range(0.1f, 10f)]
	public float rateOfFire = 1;

	[Range(1, 5000)]
	public int magSize = 10;

	GameObject m_HitFxPrefab, m_BloodFxPrefab;
	GameObject m_Barrel;

	Animator m_Animator;
	AudioSource m_AudioSource;
	Transform m_Holder;

	Vector3 m_BulletOrigin;
	Vector3 m_BulletOriginOffset = new Vector3(0f, 0.5f, 0f);

	Magazine m_Magazine;
	ParticleSystem m_Muzzle;
	MuzzleFlashLight m_MuzzleFlash;
	RaycastHit m_Hit;

	bool m_CanFire = true;
	float m_Time;

	// Start is called before the first frame update
	public virtual void Start() {
		// Load HitFX for weapons
		m_HitFxPrefab = Resources.Load<GameObject>("Prefabs/HitFX/impact_softBody") as GameObject;
		m_BloodFxPrefab = Resources.Load<GameObject>("Prefabs/HitFX/impact_blood") as GameObject;

		m_Animator = GetComponent<Animator>();
		m_AudioSource = GetComponent<AudioSource>();

		m_Barrel = transform.Find("Barrel").gameObject;
		m_MuzzleFlash = m_Barrel.transform.Find("MuzzleFlashPointLight").GetComponent<MuzzleFlashLight>();

		m_Holder = transform.parent.parent.transform;
		m_Magazine = new Magazine(magSize);
		m_Muzzle = m_Barrel.GetComponentInChildren<ParticleSystem>();
		//flash = barrel.GetComponentInChildren<MuzzleFlashLight>();
		m_Muzzle.Stop();
		m_Time = rateOfFire;
	}

	public virtual void Update() {
		if (!m_CanFire) {
			m_Time -= Time.deltaTime;
		}

		if (m_Time <= 0f) {
			m_CanFire = true;
			m_Time = rateOfFire;
		}
	}

	public virtual bool Fire() {
		if (m_Magazine.Ammo > 0 && m_CanFire) {
			m_Muzzle.Play();
			m_MuzzleFlash.Activate();
			m_Animator.SetTrigger("Shoot");
			m_AudioSource.clip = sounds[Random.Range(0, sounds.Length)];
			m_AudioSource.Play();
			Raycast();
			m_Magazine.EjectBullet();
			m_CanFire = false;
			return true;
		}

		return false;
	}

	public virtual void Raycast() {
		m_BulletOrigin = m_Holder.position + m_BulletOriginOffset;

		if (Physics.Raycast(m_BulletOrigin, m_Holder.TransformDirection(Vector3.forward), out m_Hit, Mathf.Infinity, layerMask)) {
			Vector3 incomingVec = m_Hit.point - m_BulletOrigin;
			Vector3 reflectVec = Vector3.Reflect(incomingVec, m_Hit.normal);
			Debug.DrawLine(m_BulletOrigin, m_Hit.point, Color.red, 5f);
			Debug.DrawRay(m_Hit.point, reflectVec, Color.green, 5f);

			if (m_Hit.collider.CompareTag("Wall")) {
				Instantiate(m_HitFxPrefab, m_Hit.point, Quaternion.FromToRotation(Vector3.forward, m_Hit.normal));
			}

			if (m_Hit.collider.CompareTag("Body")) {
				Debug.Log("Hitting body!");
				Instantiate(m_BloodFxPrefab, m_Hit.point, Quaternion.FromToRotation(Vector3.forward, m_Hit.normal));
				var dir = (m_Hit.collider.transform.position - m_Holder.position).normalized;
				m_Hit.collider.GetComponent<Character>().TakeDamage(30, dir);
			}
		}
	}

	public virtual void Reload() {
		m_Magazine.Reload();
		Debug.Log("Reloading... Done!");
	}

	public virtual bool CanFire() {
		return m_CanFire;
	}
}
