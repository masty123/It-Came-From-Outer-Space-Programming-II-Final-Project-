﻿ using UnityEngine;
using System.Collections;
using UnitySampleAssets._2D;

/*
 * Player that contain playerStarts ,StatusIndicator.
 * @author Theeruth Borisuth 
 */
[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour {



	public int fallBoundary = -20 ;
    public int playerNumber;
    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";

    private AudioManager audioManager;

    [SerializeField]
    private StatusIndicator statusIndicator;

    private PlayerStats stats;

    void Start()
    {
        stats = PlayerStats.instance;
        stats.curHealth = stats.maxHealth;

        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on Player");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        GameMaster.gm.OnToggleUpgradeMenu += OnUpgradeMenuToggle;

        audioManager = AudioManager.instance;
        if (audioManager == null) Debug.LogError("No audiomanager in scene");

        InvokeRepeating("RegenHealth", 1f/stats.regenRate, 1f/stats.regenRate);
    }

    /*
     * Increasing health of player by 1.
     */
    void RegenHealth()
    {
        stats.curHealth += 1;
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }

    /*
     * Kill player if fall.
     */
    void Update(){
		if (transform.position.y <= fallBoundary) {
			DamagePlayer(99999); 
		}
	}

    /*
     * Stop any action when UpgradeMenu are called.
     */
    void OnUpgradeMenuToggle (bool active)
    {
        // handle what happens the upgrade menu is toggled.
        if (stats != null)
        {
            GetComponent<Platformer2DUserControl>().enabled = !active;
            Weapon _weapon = GetComponentInChildren<Weapon>();
            if (_weapon != null) _weapon.enabled = !active;
        }   
    }

    /*
     * Reduce player's health when enemy got any attack.
     * If the player hp has reach 0, kill the player.
     */
    public void DamagePlayer (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0) {
            //play death sound
            audioManager.PlaySound(deathSoundName);
            //kill player
            GameMaster.KillPlayer(this);
		}
        else
        {
            //play damage sound
            audioManager.PlaySound(damageSoundName);
        }
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }



}
