using UnityEngine;
using System.Collections;

public class TurretControl : MonoBehaviour {

	//debug
	public bool shoot;

	//Trasforms
	public Transform target;//Player
	public Transform shootPoint; //BarrelEnd

	//floats
	public float coolDown = 1;
	public float distance;
	public float shootRadius = 5;
	public float bulletSpeed = 10;
	//Cooldown limits firerate BulletSpeed is speed of the bullet
	//Distance is distance to player ShootRadius is max range

	//Gameobjects
	private GameObject barrel, tBase;//Barrel and Base nuff said
	public GameObject bullet;


	// Use this for initialization
	void Start () {
		barrel = (GameObject) GameObject.FindWithTag ("TurretBarrel");
		tBase = (GameObject) GameObject.FindWithTag ("Turret");
	}

	// Update is called once per frame
	void Update(){
		LookAt2D (barrel);		
		distance = Vector3.Distance (target.position, tBase.transform.position);
		if (distance <= shootRadius) {
			Attack ();
			shoot = true;
		}
	}
	//LookAt script to oriente barrel
	void LookAt2D(GameObject a){
		Vector3 aDir = a.transform.position;
		Vector3 dir = target.position - aDir;
		float angle = (Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg) - 90;
		if(angle <= 90 && angle >= -90)
			a.transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}
	//Shooting the bullet object
	void Attack ()
	{
		coolDown -= Time.deltaTime;
		if (coolDown <= 0) {

			Vector2 direction = target.position - barrel.transform.position;
			direction.Normalize ();

			GameObject bulletClone;
			bulletClone = Instantiate (bullet, shootPoint.position, shootPoint.rotation) as GameObject;
			bulletClone.GetComponent<Rigidbody2D>().velocity =  direction * bulletSpeed;
			coolDown = 1;
			Destroy (bulletClone, 2.0f);

		}
	}



}
