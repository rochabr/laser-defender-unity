using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float health = 1000f;
	public float speed = 2.0f;
	public GameObject projectile;
	public float projectileSpeed, firingRate;

	private float xmin;
	private float xmax;

	void OnTriggerEnter2D(Collider2D collider){
		//getting the object that collided with the enemy
		EnemyWeakLaser enemyWeakLaser = collider.gameObject.GetComponent<EnemyWeakLaser> ();

		//if the collider is a PlayerWeakLaser
		if (enemyWeakLaser) {
			//decreases the enemy health by the power of the laser
			health -= enemyWeakLaser.GetDamage ();
			//destroys the laser object
			enemyWeakLaser.Hit ();
			if (health <= 0) {
				//if the enemy's health is below zero
				Destroy (gameObject);
			}
		}
	}

	void Start(){
		float zDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, zDistance));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, zDistance));

		xmin = leftEdge.x + this.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
		xmax = rightEdge.x - this.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
	}

	// Update is called once per frame
	void Update () {
		//instantiate projectile
		if (Input.GetMouseButtonDown (0)) {
			InvokeRepeating ("FireProjectile", 0.0000001f, firingRate);
		}

		if (Input.GetMouseButtonUp (0)) {
			CancelInvoke ("FireProjectile");
		}

		//OBS: Time.deltaTime considers rendering time
		float touchPosition = Input.mousePosition.x;
		if (touchPosition < Screen.width / 2) {
			//move left
			//transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * Time.deltaTime * speed;
		} else {
			//move right
			//transform.position += new Vector3 (+speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * Time.deltaTime * speed;
		}

		//fixing player's position inside screen by clamping its x position to the start and end
		float xClamp = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (xClamp, transform.position.y, transform.position.z);
	}

	void FireProjectile ()
	{
		Vector3 firingPosition = transform.position + new Vector3 (0, this.GetComponent<SpriteRenderer> ().bounds.size.y / 2f, 0); 
		GameObject beam = Instantiate (projectile, firingPosition, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, projectileSpeed, 0);
	}
}
