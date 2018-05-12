using UnityEngine;

/**
 *Class for enemy that contain EnemyStat in kind of annonymous class. 
 * @author Theeruth Borisuth 
 */
[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour {

	//Annonymous class
	[System.Serializable]
	public class EnemyStat {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp (value, 0, maxHealth); }
        }

        public int damage = 40;



        public void Init()
        {
            curHealth = maxHealth ;
        }


	}

	public EnemyStat enemyStats = new EnemyStat();

    public Transform deathParticles;

    public string deathSoundName = "Explosion";

    public int moneyDrop = 10;

    public int gainPoint = 100;

    public float shakeAmt = 0.1f;
    public float shakeLength = 0.1f;

    [Header("Optional:")]
    [SerializeField]
    private StatusIndicator statusIndicator;
  

    void Start()
    {
        enemyStats.Init();

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }

        GameMaster.gm.OnToggleUpgradeMenu += OnUpgradeMenuToggle;

        if(deathParticles == null)
        {
            Debug.LogError("No death referenced on Enemy"); 
        }
    }
	

	
    /*
     * Reduce enemy's health when enemy got any attack.
     * If the eneny hp has reach 0, kill the emeny.
     */
	public void DamageEnemy (int damage) {
        enemyStats.curHealth -= damage;
		if (enemyStats.curHealth <= 0) {
			GameMaster.KillEnemy(this);
		}

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.curHealth, enemyStats.maxHealth);
        }
    }

    /*
     * Deal damage to Player when Enemy hit the Player.
     */
    void OnCollisionEnter2D(Collision2D _colinfo)
    {
        Player _player = _colinfo.collider.GetComponent<Player>();
        Player2 _player2 = _colinfo.collider.GetComponent<Player2>();
        if (_player != null)
        {
            _player.DamagePlayer(enemyStats.damage);
            DamageEnemy(99999);

        }
        if (_player2 != null)
        {
            _player2.DamagePlayer(enemyStats.damage);
            DamageEnemy(99999);

        }
    }

    /*
     * Stop any action when UpgradeMenu are called.
     */ 
    void OnUpgradeMenuToggle(bool active)
    {
        // handle what happens the upgrade menu is toggled.
        GetComponent<EnemyAI>().enabled = !active;

    }

}
