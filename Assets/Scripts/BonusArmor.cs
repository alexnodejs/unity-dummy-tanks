using UnityEngine;
using System.Collections;

public class BonusArmor : MonoBehaviour {

	public float BonusTime;

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			var controller = other.gameObject.GetComponent<PlayerTankController>();
			controller.SetInvulnerability(BonusTime);
			Destroy(gameObject);
		}
	}
}
