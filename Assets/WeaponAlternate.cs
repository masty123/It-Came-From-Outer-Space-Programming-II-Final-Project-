using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAlternate : MonoBehaviour {

    float dirX, getGunUp;
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    bool facingRight = true;
    public Transform gun;
    Vector3 localScale;

	// Use this for initialization
	void Start () {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        dirX = Input.GetAxis("Horizontal");
        CheckIfGunUp();

        transform.position = new Vector2(Mathf.Clamp (transform.position.x, -8, -8), transform.position.y);
	}

    void FixedUpdate(){
        rb.velocity = new Vector2(dirX * moveSpeed, 0);
    }

    void LateUpdate(){
        CheckWhereToFace();
    }

    void CheckWhereToFace(){
        if (dirX > 0) facingRight = true;
        else if (dirX < 0) facingRight = false;
        if(((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0))){
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    void CheckIfGunUp(){
        if (Input.GetKey(KeyCode.UpArrow)) gun.rotation = Quaternion.Euler(0, 0, 45 * localScale.x);
        else gun.rotation = Quaternion.Euler(0, 0, 0);
    }


}
