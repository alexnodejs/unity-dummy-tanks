using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public float Speed;
    public Rigidbody TankRigidbody;
    private Ray ray;
    RaycastHit hit;

	public float Timer;
	private float currentTimer;
	public GameObject Projectile;
	public Transform ShootPosition;

	public GameObject BonusLife;
	public GameObject BonusArmor;
	public int BonusChance;
	public int BonusArmorChance;

	private bool quitting = false;
 


	// Use this for initialization
	void Start () {
	
	}

	void Update() {
		if (currentTimer == 0) {
			Shoot();
			currentTimer = Timer;
		}

		if (currentTimer > 0)
			currentTimer -= Time.deltaTime;

		if (currentTimer < 0)
			currentTimer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        TankRigidbody.AddForce(transform.forward * Speed);

        ray.origin = transform.position;
        ray.direction = transform.forward;

        if(Physics.Raycast(ray, out hit, 3))
        {
            if (hit.collider.CompareTag("Wall"))
			{
				var rotation = transform.rotation.eulerAngles;
				rotation.y += 90;
				transform.rotation = Quaternion.Euler(rotation);
			}
        }
	}

	private void Shoot()
	{
		GameObject projectile = Instantiate(Projectile, ShootPosition.position, transform.rotation) as GameObject;
		projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000);
	}

	private void OnDestroy()
	{
		if (quitting) return;

		var random = Random.Range(0, 100);

//		if (BonusChance > random) {
//			GameObject bonus = Instantiate(BonusLife, transform.position, Quaternion.identity) as GameObject;
//		}
		Debug.Log("bonus armor shoot");
		if (BonusArmorChance > random) {
			GameObject bonus = Instantiate(BonusArmor, transform.position, Quaternion.identity) as GameObject;
		}

		GameObject controller = GameObject.Find("Game Controller");
		controller.GetComponent<GameController>().EnemyKilled();
	}

	private void OnApplicationQuit() {
		quitting = true;
	}
}
