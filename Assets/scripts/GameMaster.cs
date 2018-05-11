using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives;

    public static int RemainingLives
    {
        get { return _remainingLives; }
    }


    [SerializeField]
    private int startingMoney;
    public static int Money;
    public static int Score;

	void Awake () {
		if (gm == null) gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
	}

	public Transform playerPrefab;
    public Transform player2Prefab;
    public Transform spawnPoint;
	public float spawnDelay = 2;
	public Transform spawnPrefab;
    public string respawnCountdownSoundName = "RespawnCountdown";
    public string spawnSoundName = "Spawn";
    

    public string gameOverSoundName = "GameOver";

    public CameraShake cameraShake;

    [SerializeField]
    private GameObject gameOverUI;
     
    [SerializeField]
    private GameObject upgradeMenu;

    [SerializeField]
    private WaveSpawner waveSpawner;

    public delegate void UpgradeMenuCallBack(bool active);
    public UpgradeMenuCallBack OnToggleUpgradeMenu;

    //cache
    private AudioManager audioManager;

    /*
     * Define AudioManager, _remainingLive and Money.
     */
    void Start()
    {
        if (cameraShake == null)
        {
            Debug.LogError("No camera shake referenced on GameMaster");
        }
        _remainingLives = maxLives;
        Money = startingMoney;

        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null) Debug.LogError("No audiomanager in scene"); 

    }

    /*
     * When Player or Player2 die.Create new Player to continue the game.
     */ 
	public IEnumerator RespawnPlayer (int playerNum){
        audioManager.PlaySound(respawnCountdownSoundName);
        yield return new WaitForSeconds(spawnDelay);

        audioManager.PlaySound(spawnSoundName);
        if (playerNum == 1)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
	    else if (playerNum == 2)
        {
            Instantiate(player2Prefab, spawnPoint.position, spawnPoint.rotation);
        }
        GameObject clone = Instantiate  (spawnPrefab.gameObject, spawnPoint.position, spawnPoint.rotation);
		Destroy (clone, 3f);
	}

    /*
     * Destroy Player object.
     */
    public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlayer(1));
        }
    }

    /*
     * Destroy Player2 object.
     */
    public static void KillPlayer2(Player2 player)
    {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlayer(2));
        }
    }

    /*
     * Destroy Enemy object.
     */
    public static void KillEnemy (Enemy enemy)
    {
        gm._KillEnemy(enemy);
	}

    //non static method
    public void _KillEnemy(Enemy _enemy)
    {
        //Play some sound
        audioManager.PlaySound(_enemy.deathSoundName);

        //Gain some money
        Money += _enemy.moneyDrop;
        audioManager.PlaySound("Money");

        //Gain score
        Score += _enemy.gainPoint;

        //Add particles
        GameObject _clone = Instantiate (_enemy.deathParticles.gameObject, _enemy.transform.position, Quaternion.identity);
        Destroy(_clone, 5f);

        //Go cameraShake
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }

    /*
     * Open UpgradeMenu scene when press U on keyboard.
     */
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
    }

    /*
     * Activate the UpgradeMenu scene.
     */
    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        waveSpawner.enabled = !upgradeMenu.activeSelf;  
        OnToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }

    /*
     * This method will stop time, counting down from 2 seconds.
     */
    IEnumerator togglePause()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0f;       
    }

    /*
     * Activate the GameOver scene.
     */
    public void EndGame()
    {
        audioManager.PlaySound("GameOver");
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
        togglePause();
    }

}
