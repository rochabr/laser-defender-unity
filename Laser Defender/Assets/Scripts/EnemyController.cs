using UnityEngine;
using System.Collections;
using System;

public class EnemyController : MonoBehaviour {

	public float health = 150f;
	public GameObject projectile;
	public float projectileSpeed, firingRate;

	void OnTriggerEnter2D(Collider2D collider){
		//getting the object that collided with the enemy
		PlayerWeakLaser playerWeakLaser = collider.gameObject.GetComponent<PlayerWeakLaser> ();

		//if the collider is a PlayerWeakLaser
		if (playerWeakLaser) {
			//decreases the enemy health by the power of the laser
			health -= playerWeakLaser.GetDamage ();
			//destroys the laser object
			playerWeakLaser.Hit ();
			if (health <= 0) {
				//if the enemy's health is below zero
				Destroy (gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating ("FireProjectile", 0.0000001f, UnityEngine.Random.Range(1, 5));
	}

	// Update is called once per frame
	void Update () {
		
	}

	void FireProjectile ()
	{
		Vector3 firingPosition = transform.position - new Vector3 (0, this.GetComponent<SpriteRenderer> ().bounds.size.y / 2f, 0); 
		GameObject beam = Instantiate (projectile, firingPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, -10, 0);
	}
}
