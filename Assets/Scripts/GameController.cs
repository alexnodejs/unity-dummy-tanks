using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject Enemy;
	public GameObject Player;

	public Transform EnemySpawnPoint;
	public Transform PlayerSpawnPoint;

	public int EnemyCount;
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
}
