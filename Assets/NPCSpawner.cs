using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour {

	public GameObject NPCPrefab;
	public Transform[] spawnPoints;
	public float spawnTimer = 10f;

	float time;

	// Start is called before the first frame update
	void Start() {
		time = spawnTimer;
	}

	// Update is called once per frame
	void Update() {
		time -= Time.deltaTime;

		if (time <= 0f) {
			SpawnNPC();
			time = spawnTimer;
		}
	}

	void SpawnNPC() {
		Vector3 position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

		Instantiate(NPCPrefab, position, Quaternion.identity);
	}
}
