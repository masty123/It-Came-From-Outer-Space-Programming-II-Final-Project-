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

    private bool pause = true;





    public delegate void UpgradeMenuCallBack(bool active);
    public UpgradeMenuCallBack OnToggleUpgradeMenu;

    //cache
    private AudioManager audioManager;

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

 

	public IEnumerator RespawnPlayer (){
        audioManager.PlaySound(respawnCountdownSoundName);
        yield return new WaitForSeconds(spawnDelay);

        audioManager.PlaySound(spawnSoundName);
		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate  (spawnPrefab.gameObject, spawnPoint.position, spawnPoint.rotation);
		Destroy (clone, 3f);
	}

    public IEnumerator RespawnPlayer2()
    {
        audioManager.PlaySound(respawnCountdownSoundName);
        yield return new WaitForSeconds(spawnDelay);

        audioManager.PlaySound(spawnSoundName);
        Instantiate(player2Prefab, spawnPoint.position, spawnPoint.rotation);
        GameObject clone = Instantiate(spawnPrefab.gameObject, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone, 3f);
    }

    //make it static so, we don't want to reference the gamemaster everytime we want to kill the player
    public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm.RespawnPlayer());
        }
    }

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
            gm.StartCoroutine(gm.RespawnPlayer2());
        }
    }

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

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
    }

    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        waveSpawner.enabled = !upgradeMenu.activeSelf;  
        OnToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }

    void togglePause()
    {
            Time.timeScale = 0f;       
    }

    public void EndGame()
    {
        audioManager.PlaySound("GameOver");
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
        togglePause();

    }

}
