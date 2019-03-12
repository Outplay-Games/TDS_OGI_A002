using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public GameObject barrel;
	public Animator anim;
	public AudioSource aSource;

	public AudioClip[] sounds;

	public string weaponName = "Default";
	public float rof = 1;
	public int magSize = 10;

	Magazine magazine;
	ParticleSystem muzzle;
	bool canFire = true;
	float m_time;

	// Start is called before the first frame update
	void Start() {
		magazine = new Magazine(magSize);
		muzzle = barrel.GetComponentInChildren<ParticleSystem>();
		muzzle.Stop();
		m_time = rof;
	}

	public void Update() {
		if (!canFire) {
			m_time -= Time.deltaTime;
		}

		if (m_time <= 0f) {
			canFire = true;
			m_time = rof;
		}
	}

	public void Fire() {
		if (magazine.Ammo > 0 && canFire) {
			muzzle.Play();
			anim.SetTrigger("Shoot");
			aSource.clip = sounds[Random.Range(0, sounds.Length)];
			aSource.Play();
			magazine.EjectBullet();
			canFire = false;
		}
	}

	public void Reload() {
		magazine.Reload();
		Debug.Log("Reloading... Done!");
	}
}
