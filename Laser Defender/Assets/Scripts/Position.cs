using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
	
	// Update is called once per frame
	void OnDrawGizmos () {
		Gizmos.DrawWireSphere (transform.position, 1);
	}
}
