using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 2.0f;
	public GameObject projectile;
	public float projectileSpeed, firingRate;

	private float xmin;
	private float xmax;

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
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, projectileSpeed, 0);
	}
}
