using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
/*
 * @author Theeruth Borisuth
 */
public class Tilling : MonoBehaviour {

	public int offsetX = 2 ;   //the offset so that we don't get any weird errors

	// these are used for checking if we need to instantiate stuff
	public bool hasARightBuddy = false; //for checking when there is no more scene at the right
    public bool hasALeftBuddy = false ; //for checking when there is no more scene at the left

	public bool reverseScale = false ; //used if the object is not tilable

	private float spriteWidth = 0f;  // the width of our element
	private Camera cam ;   //add more tiling by, checking the camera position
	private Transform myTransform; //background transform.

    //Contructor
	void Awake () {
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer> ();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		//does it still need buddies? if not do nothing
		if (hasALeftBuddy == false || hasARightBuddy == false) {
			float camHorizonalExtend = cam.orthographicSize * Screen.width/Screen.height;

			// calculate the x position where the camera can se the edge of the sprite (element)
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizonalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizonalExtend;

			//checking if we can see the edge of the element and then calling MakeNewBuddy if we can
			if(cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
			{
				MakeNewBody (1);
				hasARightBuddy = true ;
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
			{
				MakeNewBody (-1);
				hasALeftBuddy = true ;
			}
		}
	}

    /*
     * A  function that creates a buddy on the side required
     */
	void MakeNewBody (int direction)
	{
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * direction, myTransform.position.y, myTransform.position.z);
		// instantating our new body and storing him in a variable
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;
		if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		} 
		newBuddy.parent = myTransform.parent;
		newBuddy.GetComponent<Tilling> ().hasALeftBuddy = true;
		if (direction > 0) {
			newBuddy.GetComponent<Tilling> ().hasALeftBuddy = true;
		} else {
			newBuddy.GetComponent<Tilling>().hasARightBuddy = true ;
		}
	}

}
