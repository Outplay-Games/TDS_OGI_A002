using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine {

	private int m_magSize;
	private int m_ammo;

	public int Ammo {
		get {
			return m_ammo;
		}
	}

	public int MagSize {
		get {
			return m_magSize;
		}
	}

	public Magazine(int magSize) {
		this.m_magSize = magSize;
	}

	public void Reload() {
		m_ammo = MagSize;
	}

	public void EjectBullet() {
		m_ammo -= 1;
	}
}
