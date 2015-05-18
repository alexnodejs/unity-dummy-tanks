using UnityEngine;
using System.Collections;

public class PlayerTankController : MonoBehaviour {

    [SerializeField] private float Speed = 20 ;
    public Rigidbody TankRigidbody;
    public Transform TankTransform;
    public Transform Shoot;
    public GameObject Projectile;

	public float armorTimer;
	private bool quitting = false;
	private bool armorOn = false;

	private float armorCurrentTimer;

	// Use this for initialization
	void Start ()
	{
        TankRigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update ()
	{

		MoveTank();
		if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject projectile = Instantiate(Projectile, Shoot.position, TankTransform.localRotation) as GameObject;
            Debug.Log(projectile.transform.localRotation.eulerAngles);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000);
        }

		if (armorOn) {
			Debug.Log("Armon true");
			UpdateTurnArmorOn ();
		}

	}

	void UpdateTurnArmorOn() {
		 
		Debug.Log("UpdateTurnArmorOn");
		if (armorCurrentTimer > 0) 
			armorCurrentTimer -= Time.deltaTime;

		if (armorCurrentTimer < 0) 
			armorCurrentTimer = 0;

  		if (armorCurrentTimer == 0) 
 			armorOn = false;

	}

	public void TurnArmorOn() {
		Debug.Log("TurnArmorOn");
		armorOn = true;
		armorCurrentTimer = armorTimer;
	}

	void MoveTank()
	{
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		var direction = new Vector3(horizontal, 0, vertical);
		var rotation = Quaternion.LookRotation(direction);

		if (!direction.Equals(Vector3.zero))
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);

		TankRigidbody.AddForce(direction * Speed);
	}

	private void OnDestroy()
	{
		if (quitting) 
			return;

		Debug.Log("destroy object");

		//if (armorOn) return;  //does not work
		if (!armorOn) {

		Debug.Log("armor");

		GameObject controller = GameObject.Find("Game Controller");
		controller.GetComponent<GameController>().PlayerKilled();
		}
	}

	private void OnApplicationQuit() {
		quitting = true;
	}
}
