using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public float multiplier = 1.4f;
    public float duration = 5f;

    public GameObject pickupEffect;

    private PlayerStats stats;
    private Player2Stats stats2;

    void Start()
    {
        stats = PlayerStats.instance;
        stats2 = Player2Stats.instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
        else if (other.CompareTag("Player2"))
        {
            StartCoroutine(PickupForPlayer2(other));
        }
    }



    IEnumerator Pickup(Collider2D player)
    {
        // Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);
        // Apply effect to the player
        player.transform.localScale *= multiplier;
        player.GetComponent<PlayerStats>();
        stats.maxHealth = (int)(stats.maxHealth * multiplier);
        stats.curHealth = stats.maxHealth;
        
        //Disable texture
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;


      

        //Wait x amount of seconds
        yield return new WaitForSeconds(duration);

        //Reverse the effect on our player
        Debug.Log("reversing");
        if(player != null) player.transform.localScale /= multiplier;
        float temp = stats.curHealth;
        int result = (int)Mathf.Ceil((temp /= multiplier));
        stats.curHealth = result;
        stats.maxHealth = result;
    
        Debug.Log(result);
        Destroy(gameObject);
    }


    IEnumerator PickupForPlayer2(Collider2D player)
    {
        // Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);
        // Apply effect to the player
        player.transform.localScale *= multiplier;
        player.GetComponent<Player2Stats>();
        stats2.maxHealth = (int)(stats.maxHealth * multiplier);
        stats2.curHealth = stats2.maxHealth;

        //Disable texture
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;




        //Wait x amount of seconds
        yield return new WaitForSeconds(duration);

        //Reverse the effect on our player
        Debug.Log("reversing");
        player.transform.localScale /= multiplier;
        float temp = stats.curHealth;
        int result = (int)Mathf.Ceil((temp /= multiplier));
        stats2.curHealth = result;
        stats2.maxHealth = result;

        Debug.Log(result);
        Destroy(gameObject);
    }
}
