using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public static AudioManagerScript instance;

    public AudioClip die;
    public AudioClip collide;
    public AudioClip jump;
    public AudioClip highscore;
    public AudioClip pickupitem;
    public AudioClip chooselevel;
    public AudioClip swapbuttons;
    public AudioClip spawn;
    public AudioClip useitem;
    public AudioClip winlevel;

    public AudioClip menuMusic;
    public AudioClip levelMusic;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void PlaySFX(AudioClip sound)
    {
        sfxSource.PlayOneShot(sound);
    }

    public void PlayMusic(AudioClip sound)
    {
        musicSource.clip = sound;
        musicSource.Play();
    }
}
