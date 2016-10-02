using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float touchPosition = Input.mousePosition.x;
		if (touchPosition < Screen.width / 2) {
			//move left
			transform.position += new Vector3 (-speed, 0, 0);
		} else {
			//move right
			transform.position += new Vector3 (+speed, 0, 0);
		}
	}
}
