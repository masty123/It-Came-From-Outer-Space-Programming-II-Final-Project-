using UnityEngine;
using UnityEngine.UI;

/*
 * Upgrade or increase player's status
 * @author Theeruth Borisuth 
 */
public class UpgradeMenu : MonoBehaviour {

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Text speedText;

    [SerializeField]
    private float healthIncrease = 1.3f;

    [SerializeField]
    private float movementSpeedIncrease = 1.3f;

    [SerializeField]
    private int upgradeCost = 50;

    private PlayerStats stats;
    private Player2Stats stats2;
    public int numberOfPlayer;

    void Start()
    {
        stats = PlayerStats.instance;
        if(numberOfPlayer == 2)
        {
           stats2 = Player2Stats.instance;
        }
        
    }

     void OnEnable()
    {
        UpdateValues();
    }

    /*
     * Update new status of player.
     */
    void UpdateValues()
    {
        if(stats ==null) stats = PlayerStats.instance;
        healthText.text = "HEALTH: "+ stats.maxHealth.ToString();
        speedText.text = "SPEED: "+ stats.movementSpeed.ToString();
    }

    /*
     * Increase player's maximum health.
     */
    public void UpgradeHealth()
    {   if (GameMaster.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }

      
        stats.maxHealth =  (int)(stats.maxHealth+healthIncrease);
        if (numberOfPlayer == 2)
        {
            stats2.maxHealth = (int)(stats2.maxHealth + healthIncrease);
        }
        GameMaster.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }

    /*
     * Increase player's movement speed.
     */
    public void UpgradeSpeed()
    {
        if (GameMaster.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }

        stats.movementSpeed = (int)(stats.movementSpeed+movementSpeedIncrease);
        if (numberOfPlayer == 2)
        {
            stats2.movementSpeed = (int)(stats2.movementSpeed + movementSpeedIncrease);
        }
        GameMaster.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }
}
