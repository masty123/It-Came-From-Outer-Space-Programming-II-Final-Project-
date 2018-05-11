using UnityEngine;
using System.Collections;
/*
 *Parallax scrolling is a technique in computer graphics where background images move by the camera slower than 
 *foreground images, creating an illusion of depth in a 2D scene and adding to the immersion.
 */
public class Parallaxing : MonoBehaviour {

	public Transform [] background ; // Array (list) of all the back and forground parallaxed.
	private float[] parallaxScales;  // The proportion ofthe camaera's movement to move the backgrounds by.
	public float smoothing = 1f; 	 // How smooth the parallaxing is.

	private Transform cam;            //reference to the main cameras transform
	private Vector3 previousCamPos;  //the position of the camera in the previous frame

	// is called before start().
	void Awake () {
		// setting up the reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		// The previous frame had the current frame's camera position.
		previousCamPos = cam.position;

		parallaxScales = new float[background.Length];

		//assigning corresponding parallaxScales
		for (int i = 0; i < background.Length; i++) {
			parallaxScales[i] = background[i].position.z*-1;
		}
	}
	
	// Update is called once per frame
	void Update () {

		//for each background
		for (int i = 0 ; i < background.Length; i++){
			// the parallax is the opposite of the camera movement because the prevois frame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			// set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = background[i].position.x + parallax;

			// create a target position which is the background's current position with it's target position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, background[i].position.y, background[i].position.z);

			//fade between current position and the target position using lerp
			background[i].position = Vector3.Lerp(background[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		//set previous cam position to the camera position at the end of the frame.
		previousCamPos = cam.position;
	}
}
