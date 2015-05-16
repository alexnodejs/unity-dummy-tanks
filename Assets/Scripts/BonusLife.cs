using UnityEngine;
using System.Collections;

public class BonusLife : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {

		if (other.CompareTag("Player") || other.CompareTag("Invulnerable")) {
			GameObject controller = GameObject.Find("Game Controller");
			controller.GetComponent<GameController>().PlayerLife ++;
			Destroy(gameObject);
		}
	}
}
