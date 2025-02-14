using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private AudioSource audiosource;
    public AudioClip[] songs;
    public float volume;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        if (! audiosource.isPlaying ) {
        	ChangeSong(Random.Range(0,songs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        audiosource.volume = volume;
        if (! audiosource.isPlaying ) {
        	ChangeSong(Random.Range(0,songs.Length));
        }
    }
    
    public void ChangeSong(int songPicked) {
    	audiosource.clip = songs[songPicked];
    	audiosource.Play();
    }
}
