using UnityEngine;
using System.Collections;
using UnitySampleAssets._2D;

[RequireComponent(typeof(Platformer2DUserControl))]
/*
 * Player2 that contain playerStarts ,StatusIndicator same as player but use to control player 2 graphic.
 * @author Charin   Tantrakul
 */
public class Player2 : MonoBehaviour
{



    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";

    private AudioManager audioManager;

    [SerializeField]
    private StatusIndicator statusIndicator;

    private Player2Stats stats;

    void Start()
    {
        stats = Player2Stats.instance;
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

        InvokeRepeating("RegenHealth", 1f / stats.regenRate, 1f / stats.regenRate);
    }

    void RegenHealth()
    {
        stats.curHealth += 1;
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }

    /*
     * Kill player2 if fall.
     */
    void Update()
    {
        if (transform.position.y <= fallBoundary)
        {
            DamagePlayer(99999);
        }
    }

    /*
     * Stop any action when UpgradeMenu are called.
     */
    void OnUpgradeMenuToggle(bool active)
    {
        // handle what happens the upgrade menu is toggled.
        if(stats != null)
        {
            GetComponent<Platformer2DUserControl>().enabled = !active;
            Weapon _weapon = GetComponentInChildren<Weapon>();
            if (_weapon != null) _weapon.enabled = !active;
        }
    }

    /*
      * Reduce player2's health when enemy got any attack.
      * If the player2 hp has reach 0, kill the player2.
      */
    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            //play death sound
            audioManager.PlaySound(deathSoundName);


            //kill player
            GameMaster.KillPlayer2(this);
        }
        else
        {
            //play damage sound
            audioManager.PlaySound(damageSoundName);
        }

        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);

    }



}
