using UnityEngine;
using System.Collections;

public class BonusArmor : MonoBehaviour {
	
	private void OnTriggerEnter(Collider other) {

		if (other.CompareTag("Player")) {
			Debug.Log("bonus armor player collision");
		    var player = other.gameObject.GetComponent<PlayerTankController>();
			player.TurnArmorOn();
			Destroy(gameObject);
		}
	}
}
