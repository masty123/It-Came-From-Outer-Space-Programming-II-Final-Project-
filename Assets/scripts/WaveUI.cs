using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*
 * this class is used for announce the player when the wave is starting, and how many wave is/are the player been fighting.
 * @author Theeruth Borisuth 
 */
public class WaveUI : MonoBehaviour {

    [SerializeField]
    WaveSpawner spawner; //an enemy spawner class

    [SerializeField]
    Animator waveAnimator; // animation when a wave is happening

    [SerializeField]
    Text waveCountdownText; // countdown before the wave begin.

    [SerializeField]
    Text waveCountText; // count the wave

    private WaveSpawner.SpawnState previousState; //for indicate what wavespawner is doing.

    // Use this for initialization
    void Start () {
	    if(spawner == null)
        {
            Debug.LogError("No spawner referenced!");
        }

        if (waveAnimator == null)
        {
            Debug.LogError("No spawner referenced!");
        }

        if (waveCountdownText == null)
        {
            Debug.LogError("No spawner referenced!");
        }

        if (waveCountText == null)
        {
            Debug.LogError("No spawner referenced!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
        }

        previousState = spawner.State;

    }

    //update the wave number
    void UpdateCountingUI()
    {   
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);

            //Debug.Log("Counting");
        }
        waveCountdownText.text = ((int)spawner.WaveCountDown).ToString();
    }
    
    //tell that the wave is coming
    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);


            waveCountText.text = spawner.NextWave.ToString();

            //Debug.Log("Spawning");
        }
    }
}
	
