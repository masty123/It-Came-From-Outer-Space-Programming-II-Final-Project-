using UnityEngine;
using System.Collections;
/*
 * This class is use when the player is shooting, the bullet trail will come out and destroy in one second.
 */
public class MoveTrail : MonoBehaviour {

	public int moveSpeed = 230;
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		Destroy (this.gameObject, 1 );
	}
}
