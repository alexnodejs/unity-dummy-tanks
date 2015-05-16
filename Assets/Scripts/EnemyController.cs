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

	public GameObject BonusArmor;
	public GameObject BonusLife;
	public int BonusChance;

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

		do {
			var lifeRandom = Random.Range(0, 100);
			if (BonusChance > lifeRandom) {
				GameObject bonus = Instantiate(BonusLife, transform.position, Quaternion.identity) as GameObject;
				break;
			}
			
			var armorRandom = Random.Range(0, 100);
			if (BonusChance > armorRandom) {
				GameObject bonus = Instantiate(BonusArmor, transform.position, Quaternion.identity) as GameObject;
				break;
			}
		} while(false);

		GameObject controller = GameObject.Find("Game Controller");
		controller.GetComponent<GameController>().EnemyKilled();
	}

	private void OnApplicationQuit() {
		quitting = true;
	}
}
