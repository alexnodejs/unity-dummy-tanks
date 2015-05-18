using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public List<GameObject> Enemies = new List<GameObject> ();

	public GameObject Player;

	public List<Transform> EnemySpawnPoints = new List<Transform>();
	public Transform PlayerSpawnPoint;

	public int EnemyCount;
	public int PlayerLife;

	private bool quitting = false;

	private GameObject spawnedPlayer;
 
	// Use this for initialization
	void Start () {

		SpawnPlayer();
		SpawnEnemy();
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
		}
	}

	public void EnemyKilled () {
		if (quitting) return;

		if (GetActiveEnemiesCount() == 0)
			SpawnEnemy();
	}

	private int GetActiveEnemiesCount() {
		return GameObject.FindGameObjectsWithTag("Enemy").Length;
	}

	private void SpawnPlayer() {
		spawnedPlayer = Instantiate(Player, PlayerSpawnPoint.position, Quaternion.identity) as GameObject;
	}

	private void SpawnEnemy() {
		if (Enemies.Count == 0) {
			Win();
			return;
		}

		for (var i = 0; i < Enemies.Count; i++) {
			if (Enemies.Count == 0) break;

			//int random = Random.Range(0, Enemies.Count - 1);
			//GameObject enemy = Enemies[i];
			//Enemies.RemoveAt(0);
			
			var spawnedEnemy = Instantiate(Enemies[i], EnemySpawnPoints[i].position, Quaternion.identity) as GameObject;
		} 
	}


	private void Win() {
		//print("WIN")
		//ShowWinnerText();
		//StartCoroutine(ChangeLevel());
	}

	public void GameOver() {
		Application.LoadLevel(0);
	}

	private void OnApplicationQuit() {
		quitting = true;
	}
}
