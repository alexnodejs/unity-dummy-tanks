using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
		if (other.collider.CompareTag("Enemy") || other.collider.CompareTag("Player"))
			Destroy(other.gameObject);
        
		Destroy(gameObject);
    }
}
