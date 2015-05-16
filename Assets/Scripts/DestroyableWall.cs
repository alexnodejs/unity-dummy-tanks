using UnityEngine;
using System.Collections;

public class DestroyableWall : MonoBehaviour {
	public int Life;

	void OnCollisionEnter(Collision other) {
		if (other.collider.CompareTag("Projectile")) {
			if (Life == 0) {
				Destroy(gameObject);
			} else {
				Life --;
			}
		}
	}
}
