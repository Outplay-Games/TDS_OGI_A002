using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {


	public GameObject followedCharacter;
	public Vector3 offset;

	// Start is called before the first frame update
	void Start() {
		transform.position = offset;
	}

	// Update is called once per frame
	void Update() {
		if (followedCharacter) {
			transform.position = new Vector3(followedCharacter.transform.position.x, transform.position.y, followedCharacter.transform.position.z);
		}
	}

	public void Scroll(float scroll) {
		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x, pos.y + scroll, pos.z);
		//transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x, pos.y + scroll, pos.z), Time.deltaTime * m_lerpSpeed);
	}
}
