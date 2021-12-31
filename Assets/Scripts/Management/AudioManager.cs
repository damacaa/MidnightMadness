using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float volume = 1.0f;
    public  List<AudioClip> sounds;
    [HideInInspector]public List<AudioSource> sources;
    public static AudioManager instance;

    private void Awake()
    {
        
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        sources = new List<AudioSource>();
        for (int i = 0; i < sounds.Count; i++)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.clip = sounds[i];
            sources.Add(a);
        }
    }

    public void PlayOnce(string name)
    {
        AudioSource a = sources.Find(sources => sources.clip.name == name);
        a.Play();
    }

    public void PlayLoop(string name)
    {
        AudioSource a = sources.Find(sources => sources.clip.name == name);
        a.loop = true;
        a.Play();
        
    }

    public void Stop(string name)
    {
        AudioSource a = sources.Find(sources => sources.clip.name == name);
        a.Stop();

    }

    public void Pause(string name)
    {
        AudioSource a = sources.Find(sources => sources.clip.name == name);
        a.Pause();

    }

    public bool isPlaying(string name)
    {
        AudioSource a = sources.Find(sources => sources.clip.name == name);
        return a.isPlaying;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateVolume(float value)
    {
        this.volume = value;
        AudioListener.volume = value;
    }
}
