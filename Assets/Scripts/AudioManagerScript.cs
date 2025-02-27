using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    private AudioSource source;
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

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
