using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public GameObject Player;
	public List<Transform> Enemies;

	public Transform PlayerSpawnPoint;
	public List<Transform> EnemySpawnPoints;
	
	public int PlayerLife;

	private bool quitting = false;

	// Use this for initialization
	void Start () {
		GameObject player = Instantiate(Player, PlayerSpawnPoint.position, Quaternion.identity) as GameObject;
		GameObject enemy = Instantiate(Enemy, EnemySpawnPoint.position, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayerKilled () {
		if (quitting) return;

		if (PlayerLife == 0)
			GameOver();
		
		if (PlayerLife > 0) {
			PlayerLife --;
			GameObject player = Instantiate(Player, PlayerSpawnPoint.position, Quaternion.identity) as GameObject;
		}
	}

	public void EnemyKilled () {
		if (quitting) return;

		if (EnemyCount == 0)
			GameOver();

		if (EnemyCount > 0) {
			EnemyCount --;
			GameObject enemy = Instantiate(Enemy, EnemySpawnPoint.position, Quaternion.identity) as GameObject;
		}
	}

	public void GameOver() {
		Application.LoadLevel(0);
	}

	private void OnApplicationQuit() {
		quitting = true;
	}

	private void SpawnEnemy() {
		if (Enemies.Count == 0)
			GameOver();

		Instantiate(Enemy, EnemySpawnPoint.position, Quaternion.identity) as GameObject;
	}
}
