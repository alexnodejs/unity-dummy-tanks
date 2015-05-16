using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	public GameObject Player;
	public List<Transform> Enemies;

	public Transform PlayerSpawnPoint;
	public List<Transform> EnemySpawnPoints;

	public Text enemyCount;
	public Text life;
	public RawImage youWin;
	
	public int PlayerLife;

	private GameObject spawnedPlayer;
	private bool quitting = false;

	// Use this for initialization
	void Start () {
		SpawnPlayer();
		SpawnEnemy();
		HideWinnerText();
	}

	void OnGUI() {
		UpdateUI();
	}

	public void PlayerKilled () {
		if (quitting) return;

		if (PlayerLife == 0)
			GameOver();
		
		if (PlayerLife > 0) {
			PlayerLife --;
			SpawnPlayer();
		}
	}

	public void EnemyKilled () {
		if (quitting) return;

		if (GetActiveEnemiesCount() == 0)
			SpawnEnemy();
	}

	private void SpawnEnemy() {
		if (Enemies.Count == 0) {
			Win();
			return;
		}

		for (var i = 0; i < EnemySpawnPoints.Count; i++) {
			if (Enemies.Count == 0) break;

			Transform enemy = Enemies[0];
			Enemies.RemoveAt(0);

			var spawnedEnemy = Instantiate(enemy, EnemySpawnPoints[i].position, Quaternion.identity) as GameObject;
		}
	}

	private int GetActiveEnemiesCount() {
		return GameObject.FindGameObjectsWithTag("Enemy").Length;
	}

	private void SpawnPlayer() {
		spawnedPlayer = Instantiate(Player, PlayerSpawnPoint.position, Quaternion.identity) as GameObject;
	}

	private void UpdateUI() {
		enemyCount.text = (GetActiveEnemiesCount() + Enemies.Count).ToString();
		life.text = PlayerLife.ToString();
	}

	private void ShowWinnerText() {
		youWin.gameObject.SetActive(true);
	}

	private void HideWinnerText() {
		youWin.gameObject.SetActive(false);
	}

	private void CleanSpawnedObjects() {
		foreach (var bonus in GameObject.FindGameObjectsWithTag("Bonus")) {
			Destroy(bonus);
		}

		Enemies.Clear();
		Destroy(spawnedPlayer);
	}

	private void Win() {
		ShowWinnerText();
		StartCoroutine(ChangeLevel());
	}

	private void GameOver() {
		CleanSpawnedObjects();
		Application.LoadLevel(0);
	}

	private IEnumerator ChangeLevel() {
		yield return new WaitForSeconds(3f);

		if (Application.loadedLevel < 3) {
			CleanSpawnedObjects();
			Application.LoadLevel(Application.loadedLevel + 1);
		} else {
			GameOver();
		}
	}
	
	private void OnApplicationQuit() {
		quitting = true;
	}
}
