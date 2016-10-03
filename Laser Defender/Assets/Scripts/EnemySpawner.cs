using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float speed = 5f;

	public float width = 10f;
	public float height = 5f;

	private bool isMovingRight = false;
	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
		float cameraDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, cameraDistance));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, cameraDistance));

		xmin = leftEdge.x;// + this.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
		xmax = rightEdge.x;// - this.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

		foreach (Transform child in transform){
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			//changing the current enemy's transform to the enemy formation transform
			enemy.transform.parent = child;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//move enemies from left to right
		if (isMovingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (width / 2.0f);
		float leftEdgeOfFormation = transform.position.x - (width / 2.0f);

		if (leftEdgeOfFormation < xmin) {
			isMovingRight = true;
		} else if (rightEdgeOfFormation > xmax) {
			isMovingRight = false;
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
}
