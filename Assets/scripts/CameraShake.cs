using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public Camera mainCam;

    float shakeAmount = 0;


    //check whether it there is a camera or not. If there is a camera, use it
	void Awake () {
        if (mainCam == null) mainCam = Camera.main;
	}


    //How much the camera shake. It can be modify in the shakeAmount variable.
    public void Shake(float amount, float length)
    {  
        shakeAmount = amount;
        InvokeRepeating("Doshake", 0, 0.01f);
        Invoke("StopShake", length);
    }

    /*
     * Start shaking the camera
     */
    void Doshake()
    {
        if (shakeAmount > 0 )
        {
            Vector3 camPos = mainCam.transform.position;


            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += offsetX;
            camPos.x += offsetY;

            mainCam.transform.position = camPos;
           
        }
    }

    //Stop checking
    void StopShake()
    {
        CancelInvoke("Doshake");
        mainCam.transform.localPosition = Vector3.zero;
    }
	

}
