using UnityEngine;

/**
 * PlayerStat collect all status of player
 * Maximum health ,Movement speed and regenary rate.
 */
[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int maxHealth = 100;

    private int _curHealth;
    public int curHealth
    {
        get { return _curHealth; }
        set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public float regenRate = 2f;

    public float movementSpeed = 10f; 		// The fastest the player can travel in the x axis.


    public void Awake()
    {

        if (instance == null) { instance = this; }

        curHealth = maxHealth;
    }
}
