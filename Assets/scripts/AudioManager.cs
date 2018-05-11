using UnityEngine;

 
[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f,0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool loop = false;

    private AudioSource source;

    /*
     *seting an output audio 
     */
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }

    /*
     * play the audio
     */
    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume/ 2f));
        //source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }

    /*
     * stop the audio
     */
    public void Stop()
    {

        source.Stop();
    }
}

/*
 * AudioManager get any sound source 
 */
public class AudioManager : MonoBehaviour{

    public static AudioManager instance;
    
    /*
     * An array of individual sound that use in each situation like shooting, jumping and many more.
     */
     
    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this) Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(this); 
        }
    }

    /*
     * When start the application find sound and play it.
     */
    void Start()
    {
       for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
        PlaySound("Music");
    }

    /*
     * Play any sound in soucre by receive string of sound's name.
     */
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }


    /*
     * Stop any sound in soucre by receive string of sound's name.
     */
    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }
}
