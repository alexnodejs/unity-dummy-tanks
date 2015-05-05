using UnityEngine;
using System.Collections;

public class BonusLife : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {

		if (other.CompareTag("Player")) {
			GameObject controller = GameObject.Find("Game Controller");
			controller.GetComponent<GameController>().PlayerLife ++;
			Destroy(gameObject);
		}
	}
}
