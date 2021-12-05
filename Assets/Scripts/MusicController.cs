using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip musicClipWin;

    public AudioClip musicClipLose;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Lose()
    {
        Debug.Log("Death Detected");
        musicSource.clip = musicClipLose;
        musicSource.Play();
    }

    public void Win()
    {
        Debug.Log("Win Detected");
        musicSource.clip = musicClipWin;
        musicSource.Play();
    }
}
