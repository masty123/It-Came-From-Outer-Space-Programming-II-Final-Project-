using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountdownText;

    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;

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
	
